using System;

namespace NancyFX
{
    internal class Users
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public string profilePicture { get; set; }
        public string major { get; set; }
        public string minor { get; set; }
        public string[] coursesTaken { get; set; }
    }
}
