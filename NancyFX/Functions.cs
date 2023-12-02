using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NancyFX
{
    public static class Functions
    {
        public static async Task AddUser(IFirebaseClient client, int id, string userName, string description, 
                                  string email, string profileURL, string major, string minor, string[] classesTaken)
        {
            var user = new Users
            {
                ID = id,
                UserName = userName,
                Description = description,
                Email = email,
                ProfileURL = profileURL,
                Major = major,
                Minor = minor,
                ClassesTaken = classesTaken
            };

            SetResponse response = await client.SetAsync("Users/" + user.ID, user);
            Users result = response.ResultAs<Users>(); // Assuming you need the result for further processing
        }

        public static async Task AddService(IFirebaseClient client, int id, decimal price, int userId, 
                                     string location, string serviceType, double review, bool deleted, string serviceName)
        {
            var service = new Services
            {
                ID = id,
                Price = price,
                UserId = userId,
                Location = location,
                ServiceType = serviceType,
                Review = review,
                Deleted = deleted,
                ServiceName = serviceName
            };

            SetResponse response = await client.SetAsync("Services/" + service.ID, service);
            Services result = response.ResultAs<Services>(); // Assuming you need the result for further processing
        }

        public static async Task<object> GetServices(IFirebaseClient client)
        {
            try
            {
                string path = "Services/";

                FirebaseResponse response = await client.GetAsync(path);
                var serviceData = response.ResultAs<Dictionary<string, Services>>();

                if (serviceData != null)
                {
                    // Convert the Dictionary to a List or another suitable format for JSON serialization
                    return serviceData.Values.ToList();
                }
                else
                {
                    return new { message = "No service data found." };
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an error message
                Console.WriteLine($"Error retrieving service data: {ex.Message}");
                return new { error = "An error occurred while retrieving data." };
            }
        }


        public static async Task GetUsers(IFirebaseClient client, int userId)
        {
            try
            {
                string path = $"Users/{userId}";

                FirebaseResponse response = await client.GetAsync(path);
                Users userData = response.ResultAs<Users>();

                if (userData != null)
                {
                    Console.WriteLine($"User Data retrieved: {userData.UserName}");
                }
                else
                {
                    Console.WriteLine("No user data found at the specified path.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving user data: {ex.Message}");
            }
        }
    }
}
