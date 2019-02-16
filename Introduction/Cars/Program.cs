using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessCars("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            var query = from car in cars
                        group car by car.Manufacturer into carGroup
                        select new
                        {
                            Name = carGroup.Key.ToUpper(),
                            Max = carGroup.Max(c => c.Combined),
                            Min = carGroup.Min(c => c.Combined),
                            Avg = carGroup.Average(c => c.Combined),
                            Count = carGroup.Count()
                        } into result
                        orderby result.Max descending
                        select result;
            var query2 = cars.GroupBy(c => c.Manufacturer)
                            .Select(g => new
                            {
                                Name = g.Key.ToUpper(),
                                Max = g.Max(c => c.Combined),
                                Min = g.Min(c => c.Combined),
                                Avg = g.Average(c => c.Combined),
                                Count = g.Count(),
                            })
                            .OrderByDescending(a => a.Max);

            foreach (var item in query2)
            {
                Console.WriteLine($"{item.Name}:");
                Console.WriteLine($"\tMax: {item.Max}");
                Console.WriteLine($"\tMin: {item.Min}");
                Console.WriteLine($"\tAvg: {item.Avg}");
                Console.WriteLine($"\tCount: {item.Count}");
            }

            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
        }

        private static List<Manufacturer> ProcessManufacturers(string source)
        {
            return File.ReadAllLines(source)
                        .Where(line => line.Length > 0)
                        .Select(line => 
                        {
                            var vals = line.Split(',');
                            return new Manufacturer ( )
                            {
                                Name = vals[0],
                                Headquarters = vals[1],
                                Year = int.Parse(vals[2])
                            };
                        })
                        .ToList();
        }

        private static List<Car> ProcessFile(string path)
        {
            return File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 0)
                .Select(line => Car.LoadCarFromCSV(line))
                .ToList();
        }
        private static List<Car> ProcessCars(string path)
        {
            var query = File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 0)
                .ToCar();
            return query.ToList();
        }
        private static List<Car> ProcessFile2(string path)
        {
            var query = from line in File.ReadAllLines(path).Skip(1)
                    where line.Length > 0
                    select Car.LoadCarFromCSV(line);                   

            return query.ToList();
        }
    }

    public static class CarExtension
    {
        public static IEnumerable<Car> ToCar(this IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                yield return Car.LoadCarFromCSV(line);
            }
        }
    }
}
