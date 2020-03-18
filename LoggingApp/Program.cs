using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace LoggingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupLogging();
            var batman = new SuperHero
            {
                Name = "Batman",
                RealName = "Bruce Wayne",
                SuperPowers = new List<string>()
                {
                    "Deception","Super rich","Intelligence"
                },
                Gender = 'M'
            };
            var supergirl = new SuperHero
            {
                Name = "Supergirl",
                RealName = "Kara Zor-El",
                SuperPowers = new List<string>()
                {
                    "Super Speed","Telescopic Vision","Super Indurance"
                },
                Gender = 'F'
            };
            var wonderWoman = new SuperHero
            {
                Name = "Wonder Woman",
                RealName = "Diana",
                SuperPowers = new List<string>()
                {
                    "God","Intelligence"
                },
                Gender = 'F'
            };

            var allHeros = new List<SuperHero>();
            allHeros.Add(batman);
            allHeros.Add(supergirl);
            allHeros.Add(wonderWoman);


            var femaleHeros = allHeros.Where(x => x.Gender == 'F');

            Log.Information("female heros found: {count}", femaleHeros.Count());

            try
            {
                femaleHeros.ToList()[4].Name="This will fail to execute";
            }
            catch (Exception ex)
            {
                Log.Error(ex,"Exception while reading heros. femaleHeros:@{femaleHero}",femaleHeros);
            }


        }

        private static void SetupLogging() => Log.Logger = new LoggerConfiguration()
                                    .MinimumLevel.Debug()
                                    .WriteTo.Console()
                                    .WriteTo.File(ConfigurationManager.AppSettings["logLocation"],
                                                    rollingInterval: RollingInterval.Day)
                                    .CreateLogger();

    }

    public class SuperHero
    {
        public string Name { get; set; }
        public string RealName { get; set; }

        public List<string> SuperPowers { get; set; }

        public char Gender { get; set; }

        public override string ToString()
        {
            return $"Name:{this.Name}:RealName:{this.RealName}:SuperPowers:{String.Join(",",this.SuperPowers)}";
        }

    }

}
