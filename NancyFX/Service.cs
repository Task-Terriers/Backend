using System;

namespace NancyFX
{
    public class Services
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
