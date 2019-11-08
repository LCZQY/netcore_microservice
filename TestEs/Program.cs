using Elasticsearch.Net;
using Nest;
using System;

namespace TestEs
{
    class Program
    {
        static void Main(string[] args)
        {
            var nodes = new Uri[] {
                new Uri("http://127.0.0.1:9200"),
                new Uri("http://127.0.0.1:9201"),
                new Uri("http://127.0.0.1:9202"),
            };
            var pool = new StaticConnectionPool(nodes);
            var settings = new ConnectionSettings(pool);
            var client = new ElasticClient(settings);

            var studnet = new Student { Age = 20 , Name ="郑强勇" , Sex = true, Weight = 2300 };
            client.CreateDocument<Student>(studnet);

        }
    }

    public class Student
    { 
        public string Name { get; set; }

        public bool Sex { get; set; }

        public int Age { get; set; }

        public decimal Weight { get; set; }

    }
}
