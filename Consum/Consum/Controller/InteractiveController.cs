using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Net;
using SharpTrooper.Entities;

namespace Controller
{
    public class InteractiveController
    {

        MainRepository mr;
        FormLogin fL;
        FormInteractive fi;
        InformationForm ObjectForm;

        Planet Endor, Tatooine, Kashiik;
        People Luke, yoda, darth, chewbacca;
        Starship estrellaMort, milenari;

        public InteractiveController(MainRepository mR, FormLogin fl)
        {
            mr = mR;
            fL = fl;
            fi = new FormInteractive();

            ObjectForm = new InformationForm();
            GetRequest();
            InitListeners();
            InitProgram();
        }


        private void InitProgram()
        {
            fi.Show();
        }

        private void InitListeners()
        {
            fi.FormClosed += Fi_FormClosed;
            //Inicio Listeners Enter al hover
            fi.darthvader.MouseHover += Fi_DarthVader_MouseHover;
            fi.yoda.MouseHover += Yoda_MouseHover;
            fi.LukeSkywalker.MouseHover += LukeSkywalker_MouseHover;
            fi.chewbacca.MouseHover += Chewbacca_MouseHover;
            fi.Tatooine.MouseHover += Tatooine_MouseHover;
            fi.Kashiik.MouseHover += Kashiik_MouseHover;
            fi.estrellaMort.MouseHover += EstrellaMort_MouseHover;
            fi.endor.MouseHover += Endor_MouseHover;
            fi.halconMilenario.MouseHover += HalconMilenario_MouseHover;

        }

        private void HalconMilenario_MouseHover(object sender, EventArgs e)
        {
            try
            {
                //Elimino el primer listener per a que no salti una altre vegada
                fi.halconMilenario.MouseHover -= HalconMilenario_MouseHover;
                //Instancio el listener leave 
                fi.halconMilenario.MouseLeave += HalconMilenario_MouseLeave;
                //PictureBox pbox = ObjectForm.BackgroundImage;
                //pbox.Load("https://www.guioteca.com/curiosidades/files/2016/10/Halc%C3%B3n_Milenario.jpg");
                ObjectForm.Show();
                ObjectForm.Left = 400;
                ObjectForm.Top = 100;
                SetRequestForm(milenari.name, milenari.model, milenari.length + " m", milenari.max_atmosphering_speed + " Atm Speed");
            }
            catch (Exception) { };


        }



        private void HalconMilenario_MouseLeave(object sender, EventArgs e)
        {
            //Elimino el listener leave per a que no salit una altre vegada
            fi.halconMilenario.MouseLeave -= HalconMilenario_MouseLeave;
            //Repetim el procés
            fi.halconMilenario.MouseHover += HalconMilenario_MouseHover;
            ObjectForm.Hide();
        }

        private void Endor_MouseHover(object sender, EventArgs e)
        {
            try
            {
                //Elimino el primer listener per a que no salti una altre vegada
                fi.endor.MouseHover -= Endor_MouseHover;
                //Instancio el listener leave 
                fi.endor.MouseLeave += Endor_MouseLeave; ; ;
                ObjectForm.Show();
                ObjectForm.Left = 100;
                ObjectForm.Top = 250;
                SetRequestForm(Endor.name, " People: " + Endor.population, Endor.orbital_period + " Orb. Per.", Endor.terrain + " Surface");
            }
            catch (Exception) { }

        }

        private void Endor_MouseLeave(object sender, EventArgs e)
        {
            //Elimino el listener leave per a que no salit una altre vegada
            fi.endor.MouseLeave -= Endor_MouseLeave;
            //Repetim el procés
            fi.endor.MouseHover += Endor_MouseHover;
            ObjectForm.Hide();
        }

        private void EstrellaMort_MouseHover(object sender, EventArgs e)
        {
            try
            {
                //Elimino el primer listener per a que no salti una altre vegada
                fi.estrellaMort.MouseHover -= EstrellaMort_MouseHover;
                //Instancio el listener leave 
                fi.estrellaMort.MouseLeave += EstrellaMort_MouseLeave; ;
                ObjectForm.Show();
                ObjectForm.Left = 200;
                ObjectForm.Top = 350;
                SetRequestForm(estrellaMort.name, estrellaMort.model, estrellaMort.length + " m", estrellaMort.max_atmosphering_speed + " Atm Speed");
            }
            catch (Exception) { }

        }

        private void EstrellaMort_MouseLeave(object sender, EventArgs e)
        {
            //Elimino el listener leave per a que no salit una altre vegada
            fi.estrellaMort.MouseLeave -= EstrellaMort_MouseLeave;
            //Repetim el procés
            fi.estrellaMort.MouseHover += EstrellaMort_MouseHover;
            ObjectForm.Hide();
        }

        private void Kashiik_MouseHover(object sender, EventArgs e)
        {
            try
            {
                //Elimino el primer listener per a que no salti una altre vegada
                fi.Kashiik.MouseHover -= Kashiik_MouseHover;
                //Instancio el listener leave 
                fi.Kashiik.MouseLeave += Kashiik_MouseHoverLeave;
                ObjectForm.Show();
                ObjectForm.Left = 500;
                ObjectForm.Top = 200;
                SetRequestForm(Kashiik.name, " People: " + Kashiik.population, Kashiik.orbital_period + " Orb. Per.", Kashiik.terrain + " Surface");
            }
            catch (Exception) { }

        }

        private void Kashiik_MouseHoverLeave(object sender, EventArgs e)
        {
            //Elimino el listener leave per a que no salit una altre vegada
            fi.Kashiik.MouseLeave -= Kashiik_MouseHoverLeave;
            //Repetim el procés
            fi.Kashiik.MouseHover += Kashiik_MouseHover;
            ObjectForm.Hide();
        }

        //TATOOINE
        private void Tatooine_MouseHover(object sender, EventArgs e)
        {
            try
            {
                //Elimino el primer listener per a que no salti una altre vegada
                fi.Tatooine.MouseHover -= Tatooine_MouseHover;
                //Instancio el listener leave 
                fi.Tatooine.MouseLeave += Tatooine_MouseHoverLeave;
                ObjectForm.Show();
                ObjectForm.Left = 600;
                ObjectForm.Top = 200;
                SetRequestForm(Tatooine.name, " People: " + Tatooine.population, Tatooine.orbital_period + " Orb. Per.", Tatooine.terrain + " Surface");
            }
            catch (Exception) { }

        }

        private void Tatooine_MouseHoverLeave(object sender, EventArgs e)
        {
            //Elimino el listener leave per a que no salit una altre vegada
            fi.Tatooine.MouseLeave -= Tatooine_MouseHoverLeave;
            //Repetim el procés
            fi.Tatooine.MouseHover += Tatooine_MouseHover;
            ObjectForm.Hide();
        }

        //ChewBacca
        private void Chewbacca_MouseHover(object sender, EventArgs e)
        {
            try
            {
                //Elimino el primer listener per a que no salti una altre vegada
                fi.chewbacca.MouseHover -= Chewbacca_MouseHover;
                //Instancio el listener leave 
                fi.chewbacca.MouseLeave += Chewbacca_MouseHoverLeave;
                ObjectForm.Show();
                ObjectForm.Left = 550;
                ObjectForm.Top = 200;
                SetRequestForm(chewbacca.name, chewbacca.gender, "Height: " + chewbacca.height, "Birth Year:" + chewbacca.birth_year);
            }
            catch (Exception) { }
        }

        private void Chewbacca_MouseHoverLeave(object sender, EventArgs e)
        {
            //Elimino el listener leave per a que no salit una altre vegada
            fi.chewbacca.MouseLeave -= Chewbacca_MouseHoverLeave;
            //Repetim el procés
            fi.chewbacca.MouseHover += Chewbacca_MouseHover;
            ObjectForm.Hide();
        }

        //LUKE SKYWALKER
        private void LukeSkywalker_MouseHover(object sender, EventArgs e)
        {
            try
            {
                //Elimino el primer listener per a que no salti una altre vegada
                fi.LukeSkywalker.MouseHover -= LukeSkywalker_MouseHover;
                //Instancio el listener leave 
                fi.LukeSkywalker.MouseLeave += luke_MouseHoverLeave;
                ObjectForm.Show();
                ObjectForm.Left = 450;
                ObjectForm.Top = 200;
                SetRequestForm(Luke.name, Luke.gender, "Height: " + Luke.height, "Birth Year:" + Luke.birth_year);
            }
            catch (Exception) { }

        }

        private void luke_MouseHoverLeave(object sender, EventArgs e)
        {
            //Elimino el listener leave per a que no salit una altre vegada
            fi.LukeSkywalker.MouseLeave -= luke_MouseHoverLeave;
            //Repetim el procés
            fi.LukeSkywalker.MouseHover += LukeSkywalker_MouseHover;
            ObjectForm.Hide();
        }




        //YODA
        private void Yoda_MouseHover(object sender, EventArgs e)
        {
            try
            {
                //Elimino el primer listener per a que no salti una altre vegada
                fi.yoda.MouseHover -= Yoda_MouseHover;
                //Instancio el listener leave 
                fi.yoda.MouseLeave += yoda_MouseHoverLeave;
                ObjectForm.Show();
                ObjectForm.Left = 500;
                ObjectForm.Top = 120;
                SetRequestForm(yoda.name, yoda.gender, "Height: " + yoda.height, "Birth Year:" + yoda.birth_year);
            }
            catch (Exception) { }

        }

        private void yoda_MouseHoverLeave(object sender, EventArgs e)
        {
            //Elimino el listener leave per a que no salit una altre vegada
            fi.yoda.MouseLeave -= yoda_MouseHoverLeave;
            //Repetim el procés
            fi.yoda.MouseHover += Yoda_MouseHover;
            ObjectForm.Hide();
        }

        int control = 1;
        /// DARTH VADER
        private void Fi_DarthVader_MouseHover(object sender, EventArgs e)
        {
            try
            {
                fi.darthvader.MouseHover -= Fi_DarthVader_MouseHover;
                //Instancio el listener leave             
                fi.darthvader.MouseLeave += Fi_DarthVader_MouseHoverLeave;
                //Elimino el primer listener per a que no salti una altre vegada            
                ObjectForm.Show();
                ObjectForm.Left = 300;
                ObjectForm.Top = 200;
                SetRequestForm(darth.name, darth.gender, "Height: " + darth.height, "Birth Year:" + darth.birth_year);
            }
            catch (Exception) { }

        }

        private void Fi_DarthVader_MouseHoverLeave(object sender, EventArgs e)
        {
            //Elimino el listener leave per a que no salit una altre vegada
            fi.darthvader.MouseLeave -= Fi_DarthVader_MouseHoverLeave;
            //Repetim el procés            
            fi.darthvader.MouseHover += Fi_DarthVader_MouseHover;
            ObjectForm.Hide();
        }



        private void Fi_FormClosed(object sender, FormClosedEventArgs e)
        {
            fL.Show();
        }


        //Requests del objectes filtrats per Nom
        private void GetRequest()
        {
            Endor = mr.GetPlanet("https://swapi.co/api/planets/7/");
            Tatooine = mr.GetPlanet("https://swapi.co/api/planets/1/");
            Kashiik = mr.GetPlanet("https://swapi.co/api/planets/14/");

            milenari = mr.GetStarship("https://swapi.co/api/starships/10/");
            estrellaMort = mr.GetStarship("https://swapi.co/api/starships/9/");

            darth = mr.GetPeople("https://swapi.co/api/people/4/");
            chewbacca = mr.GetPeople("https://swapi.co/api/people/13/");
            yoda = mr.GetPeople("https://swapi.co/api/people/20/");
            Luke = mr.GetPeople("https://swapi.co/api/people/1/");
        }

        private void SetRequestForm(string text1, string text2, string text3, string text4)
        {
            ObjectForm.text1.Text = text1;
            ObjectForm.text2.Text = text2;
            ObjectForm.text3.Text = text3;
            ObjectForm.text4.Text = text4;
        }
    }


}