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
            var cars = ProcessFile1("fuel.csv");

            //var query = cars.OrderByDescending(c => c.Combined)
            //                .ThenBy(c => c.Name);
            var query = from car in cars
                        where car.Manufacturer.Equals("BMW") && car.Year == 2016
                        orderby car.Combined descending, car.Name
                        select car;
            var top = cars.Where(c => c.Manufacturer.Equals("BMW") && c.Year == 2016)
                            .OrderByDescending(c => c.Combined)
                            .ThenBy(c => c.Name).First();
            Console.WriteLine(top.Name);
            var isFordManufacturer = cars.Any(c => c.Manufacturer == "Ford");
            Console.WriteLine(isFordManufacturer);

            foreach (var car in query.Take(10))
            {
                Console.WriteLine($"{car.Name} : {car.Combined}");
            }
            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }

        private static List<Car> ProcessFile(string path)
        {
            return File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 0)
                .Select(line => Car.LoadCarFromCSV(line))
                .ToList();
        }
        private static List<Car> ProcessFile1(string path)
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
