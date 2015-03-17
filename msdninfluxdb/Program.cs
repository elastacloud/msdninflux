using InfluxDB.Net;
using InfluxDB.Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msdninfluxdb
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
            Console.WriteLine("Running. Press ANY key to exit");
            Console.ReadKey();
        }

        private static async void Run()
        {
            try
            {
                var _client = new InfluxDb("http://influxmsdn.cloudapp.net:8086", "root", "root");

                var rand = new Random();
                for (int i = 1; i < 100; i++)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        Serie serie = new Serie.Builder("cpustatus")
                                        .Columns("machine", "cpuusage")
                                        .Values("Machine" + j, rand.Next(0, i))
                                        .Build();
                        InfluxDbApiResponse writeResponse = await _client.WriteAsync("msdncpus", TimeUnit.Milliseconds, serie);
                        if (writeResponse.Success)
                        {
                            Console.WriteLine("Data Point added " + (i*100+j).ToString());
                        }
                        else
                        {
                            Console.WriteLine("unable to write point!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
