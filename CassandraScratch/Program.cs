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
            AsyncContext.Run(() => TryConnectAsync());
        }

        private static Task TryConnectAsync()
        {
            TryConnect();
            return Task.FromResult(1);
        }

        private static void TryConnect()
        {
            try
            {
                using (var session = Connect())
                {
                    Console.WriteLine("Connected");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed");
            }
        }

        private static ISession Connect()
        {
            var builder = Cluster.Builder().AddContactPoints(new List<string> {"127.0.0.1"});
            var cluster = builder.Build();
            return cluster.Connect("test");
        }
    }
}
