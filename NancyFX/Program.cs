using System;
using Nancy.Hosting.Self;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Threading.Tasks;

namespace NancyFX
{
    class Program
    {
        public static IFirebaseClient client { get; private set; }

        public static async Task Main(string[] args)
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                // DONT FORGET TO ADD SECRET API KEY FOUND IN IMESSAGES
            };

            client = new FireSharp.FirebaseClient(config);

            if (client != null)
            {
                Console.WriteLine("Connected to Firebase");
            }

            // Set up NancyFX host configuration
            var hostConfigs = new HostConfiguration
            {
                UrlReservations = new UrlReservations { CreateAutomatically = true }
            };

            // Start the NancyFX server
            using (var host = new NancyHost(new Uri("http://10.239.204.139:1234"), new CustomBootstrapper(), hostConfigs))
            {
                host.Start();
                Console.WriteLine("NancyFX is running on http://10.239.204.139:1234");

                // Perform Firebase operations
                // await GetUsers(client);

                // Keep the server running indefinitely
                await Task.Delay(Timeout.Infinite);
            }
        }
    }
}
