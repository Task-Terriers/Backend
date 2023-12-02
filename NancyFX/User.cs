using System;

namespace NancyFX
{
    internal class Users
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string ProfileURL { get; set; }
        public string Major { get; set; }
        public string Minor { get; set; }
        public string[] ClassesTaken { get; set; }
    }
}
