using Nancy;
using FireSharp.Interfaces;
using Nancy.ModelBinding;
using System.Runtime.CompilerServices; // For model binding

namespace NancyFX {
public class FirebaseModule : NancyModule
    {
        public FirebaseModule() : base("/api")
        {
            // Add a new user
            Post("/usersAdd", async _ =>
            {
                var userRequest = this.Bind<UsersRequest>();
                var user = await Functions.AddUser(Program.client, userRequest.id, userRequest.firstName, userRequest.lastName, userRequest.description, 
                        userRequest.email, userRequest.profilePicture, userRequest.major, userRequest.minor, 
                        userRequest.coursesTaken);
                return Response.AsJson(user, HttpStatusCode.Created);
            });

            // Change an existing user
            Patch("/usersChange/{id:int}", async parameters =>
            {
                int userId = parameters.id;
                var userRequest = this.Bind<UsersRequest>();
                var user = await Functions.ChangeUser(Program.client, userId, userRequest.firstName, userRequest.lastName, userRequest.description, 
                        userRequest.email, userRequest.profilePicture, userRequest.major, userRequest.minor, 
                        userRequest.coursesTaken);
                return Response.AsJson(user);
            });

            // Get a specific user
            Get("/usersGet/{id:int}", async parameters =>
            {
                int userId = parameters.id;
                var user = await Functions.GetSpecificUser(Program.client, userId);
                return Response.AsJson(user);
            });

            // Add a new service
            Post("/servicesAdd", async _ =>
            {
                var serviceRequest = this.Bind<ServicesRequest>();
                var service = await Functions.AddService(Program.client, serviceRequest.serviceId, serviceRequest.serviceName, serviceRequest.shortServiceDescription, 
                            serviceRequest.price, serviceRequest.userId, serviceRequest.location, 
                            serviceRequest.serviceType, serviceRequest.review, serviceRequest.deleted);
                return Response.AsJson(service, HttpStatusCode.Created);
            });

            // Get a specific service
            Get("/servicesGet/{id:int}", async parameters =>
            {
                int serviceId = parameters.id;
                var service = await Functions.GetSpecificService(Program.client, serviceId);
                return Response.AsJson(service);
            });

            Get("/service-user-details", async _ =>
            {
                var details = await Functions.GetServiceUserDetails(Program.client);
                return Response.AsJson(details);
            });

        }
    }


    // Define request models for binding

    public class UsersRequest
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName {get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public string profilePicture { get; set; }
        public string major { get; set; }
        public string minor { get; set; }
        public string coursesTaken { get; set; }
    }

    public class ServicesRequest
    {
            public int serviceId { get; set; }
            public string serviceName { get; set; }
            public string shortServiceDescription { get; set; }
            public decimal price { get; set; }
            public int userId { get; set; }
            public string location { get; set; }
            public string serviceType { get; set; }
            public double review { get; set; }
            public bool deleted { get; set; }
 
    }

}
