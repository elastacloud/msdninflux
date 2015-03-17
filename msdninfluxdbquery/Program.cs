using InfluxDB.Net;
using InfluxDB.Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msdninfluxdbquery
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
            Console.ReadKey();
        }

        private static async void Run()
        { 
            var _client = new InfluxDb("http://influxmsdn.cloudapp.net:8086", "root", "root");
            List<Serie> series = await _client.QueryAsync("msdncpus", "select mean(cpuusage) from cpustatus where machine =~ /Machine.*0/i group by time(1m)", TimeUnit.Milliseconds);

            foreach (var item in series)
	        {
                Console.WriteLine(item.Name);
                foreach (var point in item.Points)
	            {
                    for (int i = 0; i < item.Columns.Length; i++)
                    {
                        Console.WriteLine(item.Columns[i] +":" + point[i].ToString());
                    }
                    Console.WriteLine("=====================");
	            }
	        }
        }
    }
}
