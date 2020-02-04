using Model;
using SharpTrooper.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using View;

namespace Controller
{
    public class AdminController
    {
        public List<People> people = new List<People>();
        public List<Film> films = new List<Film>();
        public List<Starship> starships = new List<Starship>();
        public List<Vehicle> vehicles = new List<Vehicle>();
        public List<Specie> species = new List<Specie>();
        public List<Planet> planets = new List<Planet>();

        MainRepository mr;
        public Form1 f { get; set; }
        FormLogin fL;

        public AdminController(MainRepository mR, FormLogin fl, Form1 f1)
        {
            mr = mR;
            f = f1;
            fL = fl;
            InitListeners();
            InitHeaders();
        }

        private void InitHeaders()
        {
            //Planets
            f.planetsList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            f.planetResidentsList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            f.planetFilmsList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            //Vehicles
            f.vehiclesList.Columns.Add("Vehicles");
            f.vehiclesList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            f.vehiclePilotsList.Columns.Add("Pilots");
            f.vehiclePilotsList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            f.vehicleFilmsList.Columns.Add("Appears in");
            f.vehicleFilmsList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            //Starships
            f.starshipsList.Columns.Add("Starships");
            f.starshipsList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            f.starshipPilotsList.Columns.Add("Pilots");
            f.starshipPilotsList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            f.starshipFilmsList.Columns.Add("Appears in");
            f.starshipFilmsList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            //Films
            f.filmsList.Columns.Add("Films");
            f.filmsList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            f.filmsSpeciesList.Columns.Add("Species appearing");
            f.filmsSpeciesList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            f.filmsPlanetsList.Columns.Add("Planets appearing");
            f.filmsPlanetsList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            f.filmsCharactersList.Columns.Add("Characters appearing");
            f.filmsCharactersList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            f.filmsStarshipsList.Columns.Add("Starships appearing");
            f.filmsStarshipsList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            f.filmsVehiclesList.Columns.Add("Vehicles appearing");
            f.filmsVehiclesList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            //People
            f.peopleList.Columns.Add("Characters");
            f.peopleList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            f.peopleStarshipsList.Columns.Add("Starships piloted");
            f.peopleStarshipsList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            f.peopleFilmsList.Columns.Add("Films which has been in");
            f.peopleFilmsList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            f.peopleVehiclesList.Columns.Add("Vehicles piloted");
            f.peopleVehiclesList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            //Species
            f.speciesList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            f.speciesList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            f.speciesPeopleList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            f.speciesFilmsList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        public void InitListeners()
        {
            f.peopleSearchB.Click += PeopleSearchB_Click;
            f.FormClosed += F_FormClosed;
            f.tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            f.filmsList.ItemSelectionChanged += FilmsList_ItemSelectionChanged;
            f.filmSearchB.Click += FilmSearchB_Click;
            f.peopleList.ItemSelectionChanged += PeopleList_ItemSelectionChanged;
            f.starshipsList.ItemSelectionChanged += StarshipsList_ItemSelectionChanged;
            f.starshipsSearchB.Click += StarshipsSearchB_Click;
            f.vehiclesList.ItemSelectionChanged += VehiclesList_ItemSelectionChanged;
            f.vehiclesSearchB.Click += VehiclesSearchB_Click;
            f.planetsList.ItemSelectionChanged += PlanetsList_ItemSelectionChanged;
            f.planetsSearchB.Click += PlanetsSearchB_Click;
            f.speciesList.ItemSelectionChanged += SpeciesList_ItemSelectionChanged;
            f.filtreButSpecie.Click += SpecieSearchB_Click;
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (f.tabControl.SelectedTab.Text.Equals("Films"))
            {
                if (films.Count == 7)
                    populateFilmsList();
                else
                    populateFilms();
            }
            else if (f.tabControl.SelectedTab.Text.Equals("Starships"))
            {
                if (starships.Count == 37)
                    populateStarshipsLists();
                else
                    populateStarships();
            }
            else if (f.tabControl.SelectedTab.Text.Equals("Vehicles"))
            {
                if (vehicles.Count == 39)
                    populateVehiclesLists();
                else
                    populateVehicles();
            }
            else if (f.tabControl.SelectedTab.Text.Equals("Planets"))
            {
                if (planets.Count == 61)
                    populatePlanetsLists();
                else
                    populatePlanets();
            }
            else if (f.tabControl.SelectedTab.Text.Equals("Species"))
            {
                if (species.Count == 37)
                    populateSpeciesLists();
                else
                    populateSpecies();
            }
        }

        private void PlanetsList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            f.planetResidentsList.Items.Clear();
            f.planetFilmsList.Items.Clear();
            string name = e.Item.Text;
            Planet p = planets.Where(x => x.name.Equals(name)).Single();
            f.planetNameL.Text = p.name;
            f.planetRotatL.Text = p.rotation_period;
            f.planetOrbitL.Text = p.orbital_period;
            f.planetDiameterL.Text = p.diameter;
            f.planetClimateL.Text = p.climate;
            f.planetGravityL.Text = p.gravity;
            f.planetTerrainL.Text = p.terrain;
            f.planetWaterL.Text = p.surface_water;
            f.planetPopulatL.Text = p.population;
            foreach (string st in p.residents)
            {
                People pe;
                try
                {
                    pe = people.Where(x => x.url.Equals(st)).Single();

                }
                catch (NullReferenceException)
                {
                    pe = mr.GetPeople(st);
                    people.Add(pe);
                }
                f.planetResidentsList.Items.Add(pe.name);
            }
            f.planetResidentsList.Sort();
            foreach (string st in p.films)
            {
                Film fi = films.Where(x => x.url.Equals(st)).DefaultIfEmpty().Single();
                if (fi == null)
                {
                    fi = mr.GetFilm(st);
                    films.Add(fi);
                }
                f.planetFilmsList.Items.Add(fi.title);
            }
            f.planetFilmsList.Sort();
            Cursor.Current = Cursors.Default;
        }

        private void SpeciesList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            f.speciesPeopleList.Items.Clear();
            f.speciesFilmsList.Items.Clear();

            string name = e.Item.Text;
            Specie p = species.Where(x => x.name.Equals(name)).First();
            f.SpecieName1.Text = p.name;
            f.SpecieClassification1.Text = p.classification;
            f.SpecieEyeColor1.Text = p.eye_colors;
            f.SpecieHairColor1.Text = p.hair_colors;
            f.SpecieLanguage1.Text = p.language;
            f.SpecieSkinColor1.Text = p.skin_colors;
            f.specieAveragePane.Text = p.average_lifespan;
            f.SpecieAverageHeigth1.Text = p.average_height;
            f.SpecieDessignation1.Text = p.designation;
            f.SpecieSkinColor1.Text = p.skin_colors;

            Planet pl;
            try
            {
                pl = planets.Where(x => x.url.Equals(p.homeworld)).DefaultIfEmpty().Single();
                if (pl == null)
                {
                    pl = mr.GetPlanet(p.homeworld);
                    planets.Add(pl);
                }
            }
            catch (NullReferenceException)
            {
                pl = mr.GetPlanet(p.homeworld);
                planets.Add(pl);
            }
            try
            {
                f.SpecieHomeWorld1.Text = pl.name;
            }
            catch (NullReferenceException)
            {

            }

            foreach (string st in p.people)
            {
                People p1;
                try
                {
                    p1 = people.Where(x => x.url.Equals(st)).DefaultIfEmpty().Single();
                    if (p1 == null)
                    {
                        p1 = mr.GetPeople(st);
                        people.Add(p1);
                    }
                    f.speciesPeopleList.Items.Add(p1.name);
                }
                catch (NullReferenceException) { }
            }
            f.speciesPeopleList.Sort();

            foreach (string st in p.films)
            {
                Film f1;
                try
                {
                    f1 = films.Where(x => x.url.Equals(st)).DefaultIfEmpty().Single();
                    if (f1 == null)
                    {
                        f1 = mr.GetFilm(st);
                        films.Add(f1);
                    }
                    f.speciesFilmsList.Items.Add(f1.title);
                }
                catch (NullReferenceException) { }
            }
            f.speciesFilmsList.Sort();
            Cursor.Current = Cursors.Default;
        }

        private void VehiclesList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            f.vehiclePilotsList.Items.Clear();
            f.vehicleFilmsList.Items.Clear();
            string name = e.Item.Text;
            Vehicle v = vehicles.Where(x => x.name.Equals(name)).Single();
            f.vehicleNameL.Text = v.name;
            f.vehicleModelL.Text = v.model;
            f.vehicleManL.Text = v.manufacturer;
            f.vehicleCostL.Text = v.cost_in_credits;
            f.vehicleLenghtL.Text = v.length;
            f.vehicleSpeedL.Text = v.max_atmosphering_speed;
            f.vehicleCrewL.Text = v.crew;
            f.vehiclePassL.Text = v.passengers;
            f.vehicleCargoL.Text = v.cargo_capacity;
            f.vehicleConsumL.Text = v.consumables;
            f.vehicleClassL.Text = v.vehicle_class;
            foreach (string st in v.pilots)
            {
                People p;
                try
                {
                    p = people.Where(x => x.url.Equals(st)).Single();

                }
                catch (NullReferenceException)
                {
                    p = mr.GetPeople(st);
                    people.Add(p);
                }
                f.vehiclePilotsList.Items.Add(p.name);
            }
            f.vehiclePilotsList.Sort();
            foreach (string st in v.films)
            {
                Film fi = films.Where(x => x.url.Equals(st)).DefaultIfEmpty().Single();
                if (fi == null)
                {
                    fi = mr.GetFilm(st);
                    films.Add(fi);
                }
                f.vehicleFilmsList.Items.Add(fi.title);
            }
            f.vehicleFilmsList.Sort();
            Cursor.Current = Cursors.Default;
        }
        private void StarshipsList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            f.starshipFilmsList.Items.Clear();
            f.starshipPilotsList.Items.Clear();
            string name = e.Item.Text;
            Starship s = starships.Where(x => x.name.Equals(name)).Single();
            f.starshipNameL.Text = s.name;
            f.starshipModelL.Text = s.model;
            f.starshipManufaL.Text = s.manufacturer;
            f.starshipCostL.Text = s.cost_in_credits;
            f.starshipLenghtL.Text = s.length;
            f.starshipSpeedL.Text = s.max_atmosphering_speed;
            f.starshipHyperdriveL.Text = s.hyperdrive_rating;
            f.starshipCrewL.Text = s.crew;
            f.starshipPassenL.Text = s.passengers;
            f.starshipCargoCapL.Text = s.cargo_capacity;
            f.starshipConsumaL.Text = s.consumables;
            f.starshipMgltL.Text = s.MGLT;
            f.starshipClassL.Text = s.starship_class;
            foreach (string st in s.pilots)
            {
                People p;
                try
                {
                    p = people.Where(x => x.url.Equals(st)).Single();

                }
                catch (NullReferenceException)
                {
                    p = mr.GetPeople(st);
                    people.Add(p);
                }
                f.starshipPilotsList.Items.Add(p.name);
            }
            f.starshipPilotsList.Sort();

            foreach (string st in s.films)
            {
                Film fi = films.Where(x => x.url.Equals(st)).DefaultIfEmpty().Single();
                if (fi == null)
                {
                    fi = mr.GetFilm(st);
                    films.Add(fi);
                }
                f.starshipFilmsList.Items.Add(fi.title);
            }
            f.starshipFilmsList.Sort();
            Cursor.Current = Cursors.Default;
        }

        private void PeopleList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            f.peopleFilmsList.Items.Clear();
            f.peopleStarshipsList.Items.Clear();
            f.peopleVehiclesList.Items.Clear();
            string name = e.Item.Text;
            People p = people.Where(x => x.name.Equals(name)).First();
            f.peopleHeightL.Text = p.height;
            f.peopleWeightL.Text = p.mass;
            f.peopleHairL.Text = p.hair_color;
            f.peopleSkinL.Text = p.skin_color;
            f.peopleEyeL.Text = p.eye_color;
            f.peopleBirthL.Text = p.birth_year;
            f.peopleGenderL.Text = p.gender;
            Planet pl;
            try
            {
                pl = planets.Where(x => x.url.Equals(p.homeworld)).DefaultIfEmpty().Single();
                if (pl == null)
                {
                    pl = mr.GetPlanet(p.homeworld);
                    planets.Add(pl);
                }
            }
            catch (NullReferenceException)
            {
                pl = mr.GetPlanet(p.homeworld);
                planets.Add(pl);
            }
            f.peopleHomeworldL.Text = pl.name;

            try
            {
                Specie s;
                s = species.Where(x => x.url.Equals(p.species.FirstOrDefault())).DefaultIfEmpty().Single();
                if (s == null)
                {
                    s = mr.GetSpecie(p.species.FirstOrDefault());
                    species.Add(s);
                }
                f.peopleSpecieL.Text = s.name;
            }
            catch (NullReferenceException)
            {

            }

            foreach (string st in p.starships)
            {
                Starship sh;
                try
                {
                    sh = starships.Where(x => x.url.Equals(st)).DefaultIfEmpty().Single();
                    if (sh == null)
                    {
                        sh = mr.GetStarship(st);
                        starships.Add(sh);
                    }
                    f.peopleStarshipsList.Items.Add(sh.name);
                }
                catch (NullReferenceException)
                {

                }
            }
            f.peopleStarshipsList.Sort();
            foreach (string st in p.films)
            {
                Film fi;
                try
                {
                    fi = films.Where(x => x.url.Equals(st)).DefaultIfEmpty().Single();
                    if (fi == null)
                    {
                        fi = mr.GetFilm(st);
                        films.Add(fi);
                    }
                    f.peopleFilmsList.Items.Add(fi.title);
                }
                catch (NullReferenceException)
                {

                }
            }
            f.peopleFilmsList.Sort();
            foreach (string st in p.vehicles)
            {
                Vehicle v;
                try
                {
                    v = vehicles.Where(x => x.url.Equals(st)).DefaultIfEmpty().Single();
                    if (v == null)
                    {
                        v = mr.GetVehicle(st);
                        vehicles.Add(v);
                    }
                    f.peopleVehiclesList.Items.Add(v.name);
                }
                catch (NullReferenceException)
                {

                }
            }
            f.peopleVehiclesList.Sort();
            Cursor.Current = Cursors.Default;
        }

        private void FilmsList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            f.filmsSpeciesList.Items.Clear();
            f.filmsCharactersList.Items.Clear();
            f.filmsPlanetsList.Items.Clear();
            f.filmsStarshipsList.Items.Clear();
            f.filmsVehiclesList.Items.Clear();
            string title = e.Item.Text;
            Film fi = films.Where(x => x.title.Equals(title)).First();
            f.filmNumberL.Text = fi.episode_id.ToString();
            f.filmNameL.Text = fi.title;
            f.filmDirectorL.Text = fi.director;
            f.filmOpenCrawlL.Text = fi.opening_crawl;
            foreach (string s in fi.species)
            {
                Specie sp;
                try
                {
                    sp = species.Where(x => x.url.Equals(s)).DefaultIfEmpty().Single();
                    if (sp == null)
                    {
                        sp = mr.GetSpecie(s);
                        species.Add(sp);
                    }
                    f.filmsSpeciesList.Items.Add(sp.name);
                }
                catch (NullReferenceException)
                {

                }
            }
            f.filmsSpeciesList.Sort();
            foreach (string s in fi.characters)
            {
                People p;
                try
                {
                    p = people.Where(x => x.url.Equals(s)).Single();

                }
                catch (NullReferenceException)
                {
                    p = mr.GetPeople(s);
                    people.Add(p);
                } catch (InvalidOperationException)
                {
                    p = mr.GetPeople(s);
                    people.Add(p);
                }
                f.filmsCharactersList.Items.Add(p.name);
            }
            f.filmsCharactersList.Sort();
            foreach (string s in fi.planets)
            {
                Planet p;
                try
                {
                    p = planets.Where(x => x.url.Equals(s)).DefaultIfEmpty().Single();
                    if (p == null)
                    {
                        p = mr.GetPlanet(s);
                        planets.Add(p);
                    }
                    f.filmsPlanetsList.Items.Add(p.name);
                }
                catch (NullReferenceException)
                {

                }
            }
            f.filmsList.Sort();
            foreach (string s in fi.starships)
            {
                Starship sh;
                try
                {
                    sh = starships.Where(x => x.url.Equals(s)).DefaultIfEmpty().Single();
                    if (sh == null)
                    {
                        sh = mr.GetStarship(s);
                        starships.Add(sh);
                    }
                    f.filmsStarshipsList.Items.Add(sh.name);
                }
                catch (NullReferenceException)
                {

                }
            }
            f.filmsStarshipsList.Sort();
            foreach (string s in fi.vehicles)
            {
                Vehicle v;
                try
                {
                    v = vehicles.Where(x => x.url.Equals(s)).DefaultIfEmpty().Single();
                    if (v == null)
                    {
                        v = mr.GetVehicle(s);
                        vehicles.Add(v);
                    }
                    f.filmsVehiclesList.Items.Add(v.name);
                }
                catch (NullReferenceException)
                {

                }
            }
            f.filmsVehiclesList.Sort();
            Cursor.Current = Cursors.Default;
        }

        private void PlanetsSearchB_Click(object sender, EventArgs e)
        {
            string name = f.planetFilterTb.Text;
            if (!name.Equals(""))
            {
                f.planetsList.Items.Clear();
                List<Planet> lp = planets.Where(a => a.name.ToLower().Contains(name.ToLower())).ToList();
                foreach (Planet p in lp)
                {
                    f.planetsList.Items.Add(p.name);
                }
            }
            else
            {
                f.planetsList.Items.Clear();
                foreach (Planet p in planets)
                {
                    f.planetsList.Items.Add(p.name);
                }
            }
        }

        private void SpecieSearchB_Click(object sender, EventArgs e)
        {
            string name = f.filtreSpeciestxt.Text;
            if (!name.Equals(""))
            {
                f.speciesList.Items.Clear();
                List<Specie> lp = species.Where(a => a.name.ToLower().Contains(name.ToLower())).ToList();
                foreach (Specie p in lp)
                {
                    f.speciesList.Items.Add(p.name);
                }
            }
            else
            {
                f.speciesList.Items.Clear();
                foreach (Specie p in species)
                {
                    f.speciesList.Items.Add(p.name);
                }
            }
        }

        private void VehiclesSearchB_Click(object sender, EventArgs e)
        {
            string name = f.vehicleFilterTb.Text;
            if (!name.Equals(""))
            {
                f.vehiclesList.Items.Clear();
                List<Vehicle> lv = vehicles.Where(a => a.name.ToLower().Contains(name.ToLower())).ToList();
                foreach (Vehicle v in lv)
                {
                    f.vehiclesList.Items.Add(v.name);
                }
            }
            else
            {
                f.vehiclesList.Items.Clear();
                foreach (Vehicle v in vehicles)
                {
                    f.vehiclesList.Items.Add(v.name);
                }
            }
        }

        private void StarshipsSearchB_Click(object sender, EventArgs e)
        {
            string name = f.filterStarshipsTB.Text;
            if (!name.Equals(""))
            {
                f.starshipsList.Items.Clear();
                List<Starship> ls = starships.Where(a => a.name.ToLower().Contains(name.ToLower())).ToList();
                foreach (Starship s in ls)
                {
                    f.starshipsList.Items.Add(s.name);
                }
            }
            else
            {
                f.starshipsList.Items.Clear();
                foreach (Starship s in starships)
                {
                    f.starshipsList.Items.Add(s.name);
                }
            }
        }

        private void PeopleSearchB_Click(object sender, EventArgs e)
        {
            string name = f.peopleFilterTB.Text;
            if (!name.Equals(""))
            {
                f.peopleList.Items.Clear();
                List<People> lp = people.Where(a => a.name.ToLower().Contains(name.ToLower())).ToList();
                foreach (People p in lp)
                {
                    f.peopleList.Items.Add(p.name);
                }
            }
            else
            {
                f.peopleList.Items.Clear();
                foreach (People p in people)
                {
                    f.peopleList.Items.Add(p.name);
                }
            }
        }

        private void FilmSearchB_Click(object sender, EventArgs e)
        {
            string name = f.filmFilterTB.Text;
            if (!name.Equals(""))
            {
                f.filmsList.Items.Clear();
                List<Film> lf = films.Where(a => a.title.ToLower().Contains(name.ToLower())).ToList();
                foreach (Film fi in lf)
                {
                    f.filmsList.Items.Add(fi.title);
                }
            }
            else
            {
                f.filmsList.Items.Clear();
                foreach (Film fi in films)
                {
                    f.filmsList.Items.Add(fi.title);
                }
            }
        }

        private void populatePlanets()
        {
            Cursor.Current = Cursors.WaitCursor;
            int i = 1;
            bool hasResults = true;
            while (hasResults)
            {
                try
                {
                    foreach (Planet p in mr.GetAllPlanets(i).results)
                    {
                        Planet pl = planets.Where(x => x.url.Equals(p.url)).DefaultIfEmpty().Single();
                        if (pl == null)
                        {
                            planets.Add(p);
                        }
                    }
                    i++;
                }
                catch (NullReferenceException)
                {
                    hasResults = false;
                    if (i == 1)
                    {
                        MessageBox.Show("Couldn't get the planets!");
                    }
                }
                catch (InvalidOperationException)
                {
                    i++;
                }
            }

            populatePlanetsLists();
            Cursor.Current = Cursors.Default;
        }

        private void populateVehicles()
        {
            Cursor.Current = Cursors.WaitCursor;
            int i = 1;
            bool hasResults = true;
            while (hasResults)
            {
                try
                {
                    foreach (Vehicle v in mr.GetAllVehicles(i).results)
                    {
                        Vehicle ve = vehicles.Where(x => x.url.Equals(v.url)).DefaultIfEmpty().Single();
                        if (ve == null)
                        {
                            vehicles.Add(v);
                        }
                    }
                    i++;
                }
                catch (NullReferenceException)
                {
                    hasResults = false;
                    if (i == 1)
                    {
                        MessageBox.Show("Couldn't get the vehicles!");
                    }
                }
                catch (InvalidOperationException)
                {
                    i++;
                }
            }

            populateVehiclesLists();
            Cursor.Current = Cursors.Default;
        }

        private void populateStarships()
        {
            Cursor.Current = Cursors.WaitCursor;
            int i = 1;
            bool hasResults = true;
            while (hasResults)
            {
                try
                {
                    foreach (Starship s in mr.GetAllStarships(i).results)
                    {
                        Starship sh = starships.Where(x => x.url.Equals(s.url)).DefaultIfEmpty().Single();
                        if (sh == null)
                        {
                            starships.Add(s);
                        }
                    }
                    i++;
                }
                catch (NullReferenceException)
                {
                    hasResults = false;
                    if (i == 1)
                    {
                        MessageBox.Show("Couldn't get the starships!");
                    }
                }
                catch (InvalidOperationException)
                {
                    i++;
                }
            }

            populateStarshipsLists();
            Cursor.Current = Cursors.Default;
        }

        private void populateSpecies()
        {
            Cursor.Current = Cursors.WaitCursor;
            int i = 1;
            bool hasResults = true;
            while (hasResults)
            {
                try
                {
                    foreach (Specie s in mr.GetAllSpecies(i).results)
                    {
                        species.Add(s);
                    }
                    i++;
                }
                catch (NullReferenceException)
                {
                    hasResults = false;
                    if (i == 1)
                    {
                        MessageBox.Show("Couldn't get the species!");
                    }
                }
                catch (InvalidOperationException)
                {
                    i++;
                }
            }
            populateSpeciesLists();
            Cursor.Current = Cursors.Default;
        }

        private void populateFilms()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                films = mr.GetAllFilms().results;
                populateFilmsList();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Couldn't get the films!");
            }
            Cursor.Current = Cursors.Default;
        }

        public void populatePeople()
        {
            Cursor.Current = Cursors.WaitCursor;
            int i = 1;
            bool hasResults = true;
            while (hasResults)
            {
                try
                {
                    foreach (People p in mr.GetAllPeople(i).results)
                    {
                        people.Add(p);
                    }
                    i++;
                }
                catch (NullReferenceException)
                {
                    hasResults = false;
                    if (i == 1)
                    {
                        MessageBox.Show("Couldn't get the characters");
                    }
                }
                catch (InvalidOperationException)
                {
                    i++;
                }
            }
            Cursor.Current = Cursors.Default;
        }

        public void populatePeopleList()
        {
            try
            {
                foreach (People p in people.OrderBy(x => x.name))
                {
                    f.peopleList.Items.Add(p.name);
                }
            }
            catch (ObjectDisposedException)
            {

            }
        }

        private void populateSpeciesLists()
        {
            foreach (Specie p in species.OrderBy(x => x.name))
            {
                f.speciesList.Items.Add(p.name);
            }
        }

        private void populateFilmsList()
        {
            foreach (Film fi in films.OrderBy(x => x.title))
            {
                f.filmsList.Items.Add(fi.title);
            }
        }

        private void populatePlanetsLists()
        {
            foreach (Planet p in planets.OrderBy(x => x.name))
            {
                f.planetsList.Items.Add(p.name);
            }
        }

        private void populateVehiclesLists()
        {
            foreach (Vehicle v in vehicles.OrderBy(x => x.name))
            {
                f.vehiclesList.Items.Add(v.name);
            }
        }

        private void populateStarshipsLists()
        {
            foreach (Starship s in starships.OrderBy(x => x.name))
            {
                f.starshipsList.Items.Add(s.name);
            }
        }

        private void F_FormClosed(object sender, FormClosedEventArgs e)
        {
            f.Hide();
            fL.Show();
        }
    }
}
