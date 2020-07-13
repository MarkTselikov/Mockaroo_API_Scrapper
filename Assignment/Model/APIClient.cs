using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Model
{
    class APIClient
    {
        RestClient client;

        public APIClient()
        {
            client = new RestClient("https://my.api.mockaroo.com/");
        }


        /*
         *  This method returns a list of users as list of objects that are parsed from a JSON file received from the API. 
         *  Both RestSharp and Json.Net libraries are used in this method.
         */
        public List<User> GetUsers()
        {
            var request = new RestRequest("users.json?key=c69dde60", DataFormat.Json);
            var response = client.Get<User>(request);

            List<User> users = JsonConvert.DeserializeObject<List<User>>(response.Content);
            return users;
        }


        /*
         * This method gets a user by their specified ID.
         * Please note that this method uses API to get a specific user, but doesn't get all the users and filter them locally. 
         */
        public User GetUserByID(int id)
        {
            var request = new RestRequest("users/{id}.json?key=c69dde60", DataFormat.Json);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            var response = client.Get<User>(request);

            User user = JsonConvert.DeserializeObject<User>(response.Content);
            return user;
        }


        /*
         * This method gets users by sending a request to the API and then filters them by the IP locally.
         * 
         * Note: this case is probably not working since API generates new IP addresses every time a request is made.
         * I've made this piece of code to show how I would filter the users locally in case API wasn't desinged to do so.
         */
        public List<User> GetUserByIP(String ip)
        {
            List<User> users = GetUsers();
            users = users.Where(u => u.IPAddress.Equals(ip)).ToList();
            return users;
        }
    }
}
