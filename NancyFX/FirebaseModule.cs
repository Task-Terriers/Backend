using Nancy;
using FireSharp.Interfaces;
using Nancy.ModelBinding; // For model binding

namespace NancyFX {
    public class FirebaseModule : NancyModule
    {
        public FirebaseModule() : base("/api")
        {
            // Add a new user
            Post("/users", async _ =>
            {
                var userRequest = this.Bind<UsersRequest>(); // Bind request data to UsersRequest model
                await Functions.AddUser(Program.client, userRequest.Id, userRequest.UserName, userRequest.Description, 
                            userRequest.Email, userRequest.ProfileURL, userRequest.Major, userRequest.Minor, 
                            userRequest.ClassesTaken);
                return HttpStatusCode.Created;
            });

            // Get a specific user
            Get("/users/{id:int}", async parameters =>
            {
                int userId = parameters.id;
                await Functions.GetUsers(Program.client, userId);
                return HttpStatusCode.OK;
            });

            // Add a new service
            Post("/services", async _ =>
            {
                var serviceRequest = this.Bind<ServicesRequest>(); // Bind request data to ServicesRequest model
                await Functions.AddService(Program.client, serviceRequest.ID, serviceRequest.Price, serviceRequest.UserId, 
                                serviceRequest.Location, serviceRequest.ServiceType, serviceRequest.Review, 
                                serviceRequest.Deleted, serviceRequest.ServiceName);
                return HttpStatusCode.Created;
            });

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
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string ProfileURL { get; set; }
        public string Major { get; set; }
        public string Minor { get; set; }
        public string[] ClassesTaken { get; set; }
    }

    public class ServicesRequest
    {
            public int ID { get; set; }
            public decimal Price { get; set; }
            public int UserId { get; set; }
            public string Location { get; set; }
            public string ServiceType { get; set; }
            public double Review { get; set; }
            public bool Deleted { get; set; }
            public string ServiceName { get; set; }
    }

}
