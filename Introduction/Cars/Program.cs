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
                        group car by car.Manufacturer.ToUpper() into manufacturer
                        orderby manufacturer.Key
                        select manufacturer;

            var query2 = cars.GroupBy(c => c.Manufacturer.ToUpper())
                            .OrderBy(g => g.Key);

            foreach (var group in query2)
            {
                Console.WriteLine(group.Key);
                foreach (var car in group.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
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
