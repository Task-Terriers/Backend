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

        private static Dictionary<string, string> LoadEnvFile(string filePath)
        {
            var envVariables = new Dictionary<string, string>();

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split('=', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                    continue;

                envVariables[parts[0].Trim()] = parts[1].Trim();
            }

            return envVariables;
        }

        public static async Task Main(string[] args)
        {

            var envVariables = LoadEnvFile(".env"); // Assuming .env file is in the root directory

            // Extract necessary values
            string authSecret = envVariables["AuthSecret"];
            string basePath = envVariables["BasePath"];  // Replace with your actual environment variable key

            IFirebaseConfig config = new FirebaseConfig
            {
                // // Use the secret key from .env file
                // AuthSecret = authSecret,
                // BasePath = basePath
                // // Add other configuration details if necessary

                AuthSecret = "mKrgFEbL8PmuavACKmhJ6q2XeQCSuqin5qGNKJMl",
                BasePath = "https://taskterriers-39683-default-rtdb.firebaseio.com/"
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
            using (var host = new NancyHost(new Uri("http://192.168.1.127:1234"), new CustomBootstrapper(), hostConfigs))
            {
                host.Start();
                Console.WriteLine("NancyFX is running on http://192.168.1.127:1234");

                // Perform Firebase operations
                // await GetUsers(client);

                // Keep the server running indefinitely
                await Task.Delay(Timeout.Infinite);
            }
        }
    }
}
