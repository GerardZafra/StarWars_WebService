using System;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.IO;
using SharpTrooper.Entities;
using System.Collections.Generic;
using System.Collections.Specialized;
using Model2;
using System.Windows.Forms;
using System.Data.Entity.Infrastructure;

namespace Model
{
    public class MainRepository
    {

        private string apiUrl = "http://swapi.co/api/";
        public ConsumStarWarsEntities1 context;

        public MainRepository()
        {
            this.context = new ConsumStarWarsEntities1();
        }

        public void AddUser(string name, string pass, bool type)
        {
            try
            {
                usuari u = new usuari(name, pass, type);
                context.usuaris.Add(u);
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("This user already exists!");
            }
        }

        public int ExistUser(string name, string pass)
        {
            usuari user = context.usuaris.Where(x => x.name.Equals(name) && x.password.Equals(pass)).SingleOrDefault();

            if (user != null)
            {
                if (user.userType == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }

            return -1;
        }

        public People GetPeople(string url)
        {
            People p = (People)MakeRequest(url, typeof(People));
            return p;
        }

        public Planet GetPlanet(string url)
        {
            Planet p = (Planet)MakeRequest(url, typeof(Planet));
            return p;
        }

        public Film GetFilm(string url)
        {
            Film f = (Film)MakeRequest(url, typeof(Film));
            return f;
        }

        public Specie GetSpecie(string url)
        {
            Specie s = (Specie)MakeRequest(url, typeof(Specie));
            return s;
        }

        public Starship GetStarship(string url)
        {
            Starship s = (Starship)MakeRequest(url, typeof(Starship));
            return s;
        }

        public Vehicle GetVehicle(string url)
        {
            Vehicle s = (Vehicle)MakeRequest(url, typeof(Vehicle));
            return s;
        }

        public SharpEntityResults<People> GetAllPeople(int pageNumber)
        {
            SharpEntityResults<People> result = null;
            try
            {
                result = MakeRequestList<People>(string.Format("{0}{1}{2}", apiUrl, "people/", "?page=" + pageNumber), typeof(List<People>));
            }
            catch (NullReferenceException)
            {
            }
            return result;
        }

        public SharpEntityResults<Film> GetAllFilms()
        {
            SharpEntityResults<Film> result = null;
            try
            {
                result = MakeRequestList<Film>(string.Format("{0}{1}", apiUrl, "films/"), typeof(List<Film>));
            }
            catch (NullReferenceException)
            {
            }
            return result;
        }

        public SharpEntityResults<Starship> GetAllStarships(int pageNumber)
        {
            SharpEntityResults<Starship> result = null;
            try
            {
                result = MakeRequestList<Starship>(string.Format("{0}{1}{2}", apiUrl, "starships/", "?page=" + pageNumber), typeof(List<Starship>));
            }
            catch (NullReferenceException)
            {
            }
            return result;
        }

        public SharpEntityResults<Vehicle> GetAllVehicles(int pageNumber)
        {
            SharpEntityResults<Vehicle> result = null;
            try
            {
                result = MakeRequestList<Vehicle>(string.Format("{0}{1}{2}", apiUrl, "vehicles/", "?page=" + pageNumber), typeof(List<Vehicle>));
            }
            catch (NullReferenceException)
            {
            }
            return result;
        }

        public SharpEntityResults<Specie> GetAllSpecies(int pageNumber)
        {
            SharpEntityResults<Specie> result = null;
            try
            {
                result = MakeRequestList<Specie>(string.Format("{0}{1}{2}", apiUrl, "species/", "?page=" + pageNumber), typeof(List<Specie>));
            }
            catch (NullReferenceException)
            {
            }
            return result;
        }

        public SharpEntityResults<Planet> GetAllPlanets(int pageNumber)
        {
            SharpEntityResults<Planet> result = null;
            try
            {
                result = MakeRequestList<Planet>(string.Format("{0}{1}{2}", apiUrl, "planets/", "?page=" + pageNumber), typeof(List<Planet>));
            }
            catch (NullReferenceException)
            {
            }
            return result;
        }

        public static object MakeRequest(string requestUrl, Type JSONResponseType)
        //  requestUrl: Url completa del Web Service, amb l'opció sol·licitada
        //  JSONRensponseType:  tipus d'objecte que torna el Web Service (typeof(tipus))
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest; //WebRequest WR = WebRequest.Create(requestUrl);

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                    Stream stream1 = response.GetResponseStream();
                    StreamReader sr = new StreamReader(stream1);
                    string strsb = sr.ReadToEnd();
                    object objResponse = JsonConvert.DeserializeObject(strsb, JSONResponseType);
                    return objResponse;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static SharpEntityResults<T> MakeRequestList<T>(string requestUrl, Type JSONResponseType) where T : SharpEntity
            //  requestUrl: Url completa del Web Service, amb l'opció sol·licitada
            //  JSONRensponseType:  tipus d'objecte que torna el Web Service (typeof(tipus))
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest; //WebRequest WR = WebRequest.Create(requestUrl);

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                    Stream stream1 = response.GetResponseStream();
                    StreamReader sr = new StreamReader(stream1);
                    string strsb = sr.ReadToEnd();
                    SharpEntityResults<T> swapiResponse = JsonConvert.DeserializeObject<SharpEntityResults<T>>(strsb);

                    return swapiResponse;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

    }
}
