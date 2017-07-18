using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cassandra;
using Nito.AsyncEx;

namespace CassandraScratch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TryConnect();
            AsyncContext.Run(TryConnectAsync);
        }

        private static async Task TryConnectAsync()
        {
            try
            {
                var cluster = BuildCluster();
                using (var session = await cluster.ConnectAsync())
                {
                    Console.WriteLine("Async Connected");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Async Failed");
            }
        }

        private static void TryConnect()
        {
            try
            {
                var cluster = BuildCluster();
                using (var session = cluster.Connect())
                {
                    Console.WriteLine("Sync Connected");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Sync Failed");
            }
        }

        private static Cluster BuildCluster()
        {
            var builder = Cluster.Builder().AddContactPoints(new List<string> {"127.0.0.1"});
            var cluster = builder.Build();
            return cluster;
        }

    }
}
