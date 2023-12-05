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
            Post("/users", async _ =>
            {
                var userRequest = this.Bind<UsersRequest>(); // Bind request data to UsersRequest model
                var user = await Functions.AddUser(Program.client, userRequest.id, userRequest.firstName, userRequest.lastName, userRequest.description, 
                        userRequest.email, userRequest.profilePicture, userRequest.major, userRequest.minor, 
                        userRequest.coursesTaken);
                return Response.AsJson(user, HttpStatusCode.Created);
            });

            // Get a specific user
            Get("/users/{id:int}", async parameters =>
            {
                int userId = parameters.id;
                var user = await Functions.GetUsers(Program.client, userId);
                return Response.AsJson(user);
            });

            // Add a new service
            Post("/services", async _ =>
            {
                var serviceRequest = this.Bind<ServicesRequest>(); // Bind request data to ServicesRequest model
                var service = await Functions.AddService(Program.client, serviceRequest.serviceId, serviceRequest.serviceName, serviceRequest.shortServiceDescription, 
                            serviceRequest.price, serviceRequest.userId, serviceRequest.location, 
                            serviceRequest.serviceType, serviceRequest.review, serviceRequest.deleted);
                return Response.AsJson(service, HttpStatusCode.Created);
            });

            // Get all services
            Get("/services", async _ =>
            {
                var services = await Functions.GetServices(Program.client);
                return Response.AsJson(services);
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
        public string[] coursesTaken { get; set; }
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
