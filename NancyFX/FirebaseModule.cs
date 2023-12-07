using Nancy;
using FireSharp.Interfaces;
using Nancy.ModelBinding;
using System.Runtime.CompilerServices; // For model binding

namespace NancyFX {
    // Define a module in NancyFX to handle Firebase-related HTTP API requests
    public class FirebaseModule : NancyModule
    {
        // Constructor for the FirebaseModule, setting the base route to '/api'
        public FirebaseModule() : base("/api")
        {
            // Define a POST endpoint for adding a new user
            Post("/userAdd", async _ =>
            {
                // Bind the request body to a UsersRequest object
                var userRequest = this.Bind<UsersRequest>();
                // Call the AddUser function and return the newly created user
                var user = await Functions.AddUser(Program.client, userRequest.id, userRequest.firstName, userRequest.lastName, userRequest.description, 
                        userRequest.email, userRequest.profilePicture, userRequest.major, userRequest.minor, 
                        userRequest.coursesTaken, userRequest.serviceLink);
                return Response.AsJson(user, HttpStatusCode.Created);
            });

            // Define a PUT endpoint for updating an existing user
            Put("/userChange/{id}", async parameters =>
            {
                // Extract user ID from the route parameters
                string userId = parameters.id;
                // Bind the request body to a UsersRequest object
                var userRequest = this.Bind<UsersRequest>();
                // Call the ChangeUser function and return the updated user
                var user = await Functions.ChangeUser(Program.client, userId, userRequest.firstName, userRequest.lastName, userRequest.description, 
                        userRequest.email, userRequest.profilePicture, userRequest.major, userRequest.minor, 
                        userRequest.coursesTaken, userRequest.serviceLink);
                return Response.AsJson(user);
            });

            // Define a GET endpoint to retrieve a specific user
            Get("/userGet/{id}", async parameters =>
            {
                // Extract user ID from the route parameters
                string userId = parameters.id;
                // Call the GetSpecificUser function and return the user details
                var user = await Functions.GetSpecificUser(Program.client, userId);
                return Response.AsJson(user);
            });

            // Define a POST endpoint for adding a new service
            Post("/serviceAdd", async _ =>
            {
                // Bind the request body to a ServicesRequest object
                var serviceRequest = this.Bind<ServicesRequest>();
                // Call the AddService function and return the newly created service
                var service = await Functions.AddService(Program.client, serviceRequest.serviceId, serviceRequest.serviceName, serviceRequest.shortServiceDescription, 
                            serviceRequest.price, serviceRequest.userId, serviceRequest.location, 
                            serviceRequest.serviceType, serviceRequest.review, serviceRequest.deleted);
                return Response.AsJson(service, HttpStatusCode.Created);
            });

            // Define a GET endpoint to retrieve a specific service
            Get("/serviceGet/{id}", async parameters =>
            {
                // Extract service ID from the route parameters
                int serviceId = parameters.id;
                // Call the GetSpecificService function and return the service details
                var service = await Functions.GetSpecificService(Program.client, serviceId);
                return Response.AsJson(service);
            });

            // Define a GET endpoint to list services and their associated user details
            Get("/serviceList", async _ =>
            {
                // Call the GetServiceUserDetails function and return the list of service details
                var details = await Functions.GetServiceUserDetails(Program.client);
                return Response.AsJson(details);
            });

            // Define a GET endpoint to check if a user exists
            Get("/userExists/{id}", async parameters =>
            {
                // Extract user ID from the route parameters
                string userId = parameters.id;
                // Call the UserExists function and return a boolean indicating user existence
                bool exists = await Functions.UserExists(Program.client, userId);
                return Response.AsJson(new { exists });
            });
        }
    }

}
