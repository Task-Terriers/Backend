using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NancyFX
{
    public static class Functions
    {
        public static async Task AddUser(IFirebaseClient client, int id, string firstName, string lastName, string description, 
                                  string email, string profilePicture, string major, string minor, string[] coursesTaken)
        {
            var user = new Users
            {
                id = id,
                firstName = firstName,
                lastName = lastName,
                description = description,
                email = email,
                profilePicture = profilePicture,
                major = major,
                minor = minor,
                coursesTaken = coursesTaken
            };

            SetResponse response = await client.SetAsync("Users/" + user.id, user);
            Users result = response.ResultAs<Users>(); // Assuming you need the result for further processing
        }

        public static async Task AddService(IFirebaseClient client, int serviceId, string serviceName, string shortServiceDescription, decimal price, int userId, 
                                     string location, string serviceType, double review, bool deleted) {
            var service = new Services
            {
                serviceId = serviceId,
                serviceName = serviceName,
                shortServiceDescription = shortServiceDescription,
                price = price,
                userId = userId,
                location = location,
                serviceType = serviceType,
                review = review,
                deleted = deleted
            };

            SetResponse response = await client.SetAsync("Services/" + service.serviceId, service);
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
                    Console.WriteLine($"User Data retrieved: {userData.firstName}");
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

