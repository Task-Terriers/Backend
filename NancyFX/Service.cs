using System;

namespace NancyFX
{
    internal class Services
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
