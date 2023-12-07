using System;

namespace NancyFX {
    public class UsersRequest
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName {get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public string profilePicture { get; set; }
        public string major { get; set; }
        public string minor { get; set; }
        public string coursesTaken { get; set; }
        public string serviceLink {get; set; }
    }

    public class ServicesRequest
    {
            public int serviceId { get; set; }
            public string serviceName { get; set; }
            public string shortServiceDescription { get; set; }
            public decimal price { get; set; }
            public string userId { get; set; }
            public string location { get; set; }
            public string serviceType { get; set; }
            public double review { get; set; }
            public bool deleted { get; set; }
    }
}
    