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

        public static async Task<Users> ChangeUser(IFirebaseClient client, int id, string? firstName = null, string? lastName = null, 
                                           string? description = null, string? email = null, string? profilePicture = null, 
                                           string? major = null, string? minor = null, string[]? coursesTaken = null)
        {
            FirebaseResponse response = await client.GetAsync($"Users/{id}");
            var user = response.ResultAs<Users>();

            if (user != null)
            {
                // Update fields if they are provided
                user.firstName = firstName ?? user.firstName;
                user.lastName = lastName ?? user.lastName;
                user.description = description ?? user.description;
                user.email = email ?? user.email;
                user.profilePicture = profilePicture ?? user.profilePicture;
                user.major = major ?? user.major;
                user.minor = minor ?? user.minor;
                user.coursesTaken = coursesTaken ?? user.coursesTaken;

                SetResponse updateResponse = await client.SetAsync($"Users/{id}", user);
                return updateResponse.ResultAs<Users>();
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public static async Task<Users> GetSpecificUser(IFirebaseClient client, int userId)
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

        public static async Task<Services> GetSpecificService(IFirebaseClient client, int serviceId)
        {
            FirebaseResponse response = await client.GetAsync($"Services/{serviceId}");
            var service = response.ResultAs<Services>();

            if (service != null)
            {
                return service;
            }
            else
            {
                throw new Exception("Service not found");
            }
        }
    }
}
