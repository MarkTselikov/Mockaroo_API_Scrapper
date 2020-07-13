using Assignment.Model;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Assignment
{
    class Program
    {
        static APIClient client;

        static void Main(string[] args)
        {

            client = new APIClient();
            ShowMenu();
        }


        /*
         * This is a simple method that displays a menu and loops until exit is selected. 
         * It uses console for inputs and switch-case to navigate the options.
         */
        public static void ShowMenu()
        {
            var choice = -1;
            while (choice != 0)
            {
                Console.WriteLine("Select an option from the menu below:");
                Console.WriteLine("\t1. Get all the users.");
                Console.WriteLine("\t2. Get a user by ID.");
                Console.WriteLine("\t3. Get a user by IP address.");
                Console.WriteLine("\t0. Exit.");

                choice = int.Parse(Console.ReadLine());
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        List<User> users = client.GetUsers();
                        foreach (var u in users)
                        {
                            Console.WriteLine(u);
                        }
                        break;

                    case 2:
                        int id;
                        Console.WriteLine("Please enter the ID:");
                        id = int.Parse(Console.ReadLine());

                        User user = client.GetUserByID(id);
                        if(user != null)
                            Console.WriteLine(user);
                        else
                            Console.WriteLine("No user with ID = {0} exists.", id);
                        break;

                    
                    case 3:
                        String ip;
                        Console.WriteLine("Please enter the IP address:");
                        ip = Console.ReadLine();

                        List<User> userList = client.GetUserByIP(ip);
                        if(userList.Count > 0)
                        {
                            Console.WriteLine(userList.Count);
                            foreach (var item in userList)
                            {
                                Console.WriteLine(item);
                            }
                            
                        }
                        else
                            Console.WriteLine("No users with IP address {0} exist.", ip);
                        break;

                    default:
                        break;
                }
                Console.WriteLine();
            }

        }
    }
}
