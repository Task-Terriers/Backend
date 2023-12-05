using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace NancyFX
{
    public static class Functions
    {
        public static async Task<Users> AddUser(IFirebaseClient client, int id, string firstName, string lastName, string description, 
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
            return response.ResultAs<Users>(); // Return the user object
        }

        public static async Task<Services> AddService(IFirebaseClient client, int serviceId, string serviceName, string shortServiceDescription, decimal price, int userId, 
                                                      string location, string serviceType, double review, bool deleted)
        {
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
            return response.ResultAs<Services>(); // Return the service object
        }

        public static async Task<object> GetServices(IFirebaseClient client)
        {
            try
            {
                string path = "Services/";
                FirebaseResponse response = await client.GetAsync(path);
                var serviceData = response.ResultAs<Dictionary<string, Services>>();
                return serviceData != null ? serviceData.Values.ToList() : new { message = "No service data found." };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving service data: {ex.Message}");
                return new { error = "An error occurred while retrieving data." };
            }
        }

        public static async Task<Users> GetUsers(IFirebaseClient client, int userId)
        {
            try
            {
                string path = $"Users/{userId}";
                FirebaseResponse response = await client.GetAsync(path);
                return response.ResultAs<Users>(); // Return the user object
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving user data: {ex.Message}");
                return null; // Or handle the error as you see fit
            }
        }
    }
}
