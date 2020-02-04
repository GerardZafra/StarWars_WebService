using System;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.IO;
using SharpTrooper.Entities;
using System.Collections.Generic;
using System.Collections.Specialized;

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
            usuari u = new usuari(name, pass, type);
            context.usuaris.Add(u);
            context.SaveChanges();
        }

        public void ExistUser()
        {
            //List<usuari> llista = context.usuaris.Sel 
        }  

        public People GetPeople(int id)
        {
            People p = (People)MakeRequest(string.Concat(apiUrl, "people/", id), typeof(People));
            return p;
        }

        public SharpEntityResults<People> GetAllPeople(int pageNumber)
        {
            SharpEntityResults<People> result = MakeRequestList<People>(string.Format("{0}{1}{2}", apiUrl, "people/", "?page=" + pageNumber), typeof(List<People>));
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
