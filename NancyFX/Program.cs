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
        // Public static property to hold the Firebase client instance
        public static IFirebaseClient client { get; private set; }

        // Entry point of the program
        public static async Task Main(string[] args)
        {
            // Configuration for Firebase connection
            IFirebaseConfig config = new FirebaseConfig
            {
                // AuthSecret and BasePath need to be provided for Firebase connection
                // AuthSecret = authSecret,
                // BasePath = basePath

                AuthSecret = "mKrgFEbL8PmuavACKmhJ6q2XeQCSuqin5qGNKJMl",

                BasePath = "https://taskterriers-39683-default-rtdb.firebaseio.com/"

            };

            // Create a Firebase client instance with the configuration
            client = new FireSharp.FirebaseClient(config);

            // Check if the client is connected to Firebase
            if (client != null)
            {
                Console.WriteLine("Connected to Firebase");
            }

            // Configure NancyFX hosting settings
            var hostConfigs = new HostConfiguration
            {
                // Automatically create URL reservations to avoid permission issues
                UrlReservations = new UrlReservations { CreateAutomatically = true }
            };

            // Initialize and start the NancyFX server
            using (var host = new NancyHost(new Uri("http://10.239.126.215:1234"), new CustomBootstrapper(), hostConfigs))
            {
                host.Start();
                Console.WriteLine("NancyFX is running on http://10.239.126.215:1234");

                // Here you can perform Firebase operations such as retrieving users
                // await GetUsers(client);

                // Keep the server running indefinitely
                await Task.Delay(Timeout.Infinite);
            }
        }
    }
}
