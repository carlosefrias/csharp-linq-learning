using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    public class Car
    {
        public int Year { get; set; }
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public string Displacement{ get; set; }
        public int Cylinders { get; set; }
        public int City { get; set; }
        public int Highway { get; set; }
        public int Combined { get; set; }

        internal static Car LoadCarFromCSV(string line)
        {
            var strArray = line.Split(',');
            return new Car()
            {
                Year = int.Parse(strArray[0]),
                Manufacturer = strArray[1],
                Name = strArray[2],
                Displacement = strArray[3],
                Cylinders = int.Parse(strArray[4]),
                City = int.Parse(strArray[5]),
                Highway = int.Parse(strArray[6]),
                Combined = int.Parse(strArray[7])
            };
        }
    }
}
