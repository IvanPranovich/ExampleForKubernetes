using System.Collections.Generic;

namespace ExampleForKubernetes.Models
{
    public class HomeIndexModel
    {
        public int RedisCounter { get; set; }
        public List<string> Cities { get; set; }

        public string Config { get; set; }
    }
}