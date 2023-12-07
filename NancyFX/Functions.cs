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
        // Method to add a new user to Firebase
        public static async Task<Users> AddUser(IFirebaseClient client, string id, string firstName, string lastName, string description, 
                                                string email, string profilePicture, string major, string minor, string coursesTaken, string serviceLink)
        {
            // Create a new user object
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
                coursesTaken = coursesTaken,
                serviceLink = serviceLink
            };

            // Set the user data in Firebase and return the result
            SetResponse response = await client.SetAsync("Users/" + user.id, user);
            return response.ResultAs<Users>(); // Return the user object
        }

        // Method to update an existing user's information in Firebase
        public static async Task<Users> ChangeUser(IFirebaseClient client, string id, string? firstName = null, string? lastName = null, 
                                           string? description = null, string? email = null, string? profilePicture = null, 
                                           string? major = null, string? minor = null, string? coursesTaken = null, string? serviceLink = null)
        {
            // Retrieve the existing user data
            FirebaseResponse response = await client.GetAsync($"Users/{id}");
            var user = response.ResultAs<Users>();

            if (user != null)
            {
                // Update user fields if new values are provided
                user.firstName = firstName ?? user.firstName;
                user.lastName = lastName ?? user.lastName;
                user.description = description ?? user.description;
                user.email = email ?? user.email;
                user.profilePicture = profilePicture ?? user.profilePicture;
                user.major = major ?? user.major;
                user.minor = minor ?? user.minor;
                user.coursesTaken = coursesTaken ?? user.coursesTaken;
                user.serviceLink = serviceLink ?? user.serviceLink;

                // Save the updated user data in Firebase
                SetResponse updateResponse = await client.SetAsync($"Users/{id}", user);
                return updateResponse.ResultAs<Users>();
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        // Method to retrieve a specific user's details from Firebase
        public static async Task<Users> GetSpecificUser(IFirebaseClient client, string userId)
        {
            try
            {
                string path = $"Users/{userId}";
                FirebaseResponse response = await client.GetAsync(path);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return response.ResultAs<Users>(); // Return the user object
                }
                else
                {
                    Console.WriteLine($"Error retrieving user data: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception retrieving user data: {ex.Message}");
                return null;
            }
        }

        // Method to add a new service to Firebase
        public static async Task<Services> AddService(IFirebaseClient client, int serviceId, string serviceName, string shortServiceDescription, decimal price, string userId, 
                                                      string location, string serviceType, double review, bool deleted)
        {
            // Create a new service object
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
                deleted = deleted,
            };

            // Set the service data in Firebase and return the result
            SetResponse response = await client.SetAsync("Services/" + service.serviceId, service);
            return response.ResultAs<Services>(); // Return the service object
        }

        // Method to retrieve a specific service's details from Firebase
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

        // Method to retrieve service details along with user information from Firebase
        public static async Task<IEnumerable<ServiceCardInfo>> GetServiceUserDetails(IFirebaseClient client)
        {
            try
            {
                string path = "Services/";
                FirebaseResponse serviceResponse = await client.GetAsync(path);
                var serviceData = serviceResponse.ResultAs<Dictionary<string, Services>>(); // Deserialize data to a dictionary

                var details = new List<ServiceCardInfo>();

                // Loop through each service and retrieve corresponding user details
                if (serviceData != null)
                {
                    foreach (var serviceEntry in serviceData)
                    {
                        var service = serviceEntry.Value;
                        if (service != null)
                        {
                            FirebaseResponse userResponse = await client.GetAsync($"Users/{service.userId}");
                            var userData = userResponse.ResultAs<Users>();

                            if (userData != null)
                            {
                                details.Add(new ServiceCardInfo
                                {
                                    FirstName = userData.firstName,
                                    LastName = userData.lastName,
                                    Major = userData.major,
                                    ServiceId = service.serviceId,
                                    ServiceName = service.serviceName,
                                    Price = service.price,
                                    Review = service.review,
                                    description = service.shortServiceDescription,
                                    serviceType = service.serviceType
                                });
                            }
                        }
                    }
                }

                return details;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving service user details: {ex.Message}");
                throw;
            }
        }

        // Method to check if a user exists in Firebase
        public static async Task<bool> UserExists(IFirebaseClient client, string userId)
        {
            try
            {
                FirebaseResponse response = await client.GetAsync($"Users/{userId}");
                if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Body != "null")
                {
                    return true; // User exists
                }
                return false; // User does not exist or an empty response
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking user existence: {ex.Message}");
                return false; // Handle error scenario
            }
        }

    }
}
