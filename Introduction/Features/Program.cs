using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features
{
    class Program
    {
        static void Main(string[] args)
        {
            //Func<int, int> Square = x => x * x; //Lambda Expression
            int square(int x) => x * x;
            //Func<int, int, int> add = (x, y) => x + y;
            int add(int x, int y) => x + y;
            //Action<int> write = x => Console.WriteLine(x);
            void write(int x) => Console.WriteLine(x);
            int n = 5, x1 = 5, y1 = 16;
            //Console.WriteLine($"Square of {n} is {Square(n)}");
            //Console.WriteLine($"{x1} plus {y1} is {add(x1, y1)}");
            write(square(add(x1, y1)));
            var developers = new Employee[]
            {
                new Employee() {Id = 1, Name = "Scott"},
                new Employee() {Id = 2, Name = "Chirs"}
            };
            var sales = new List<Employee>()
            {
                new Employee() {Id = 3, Name = "Alex"}
            };

            //Console.WriteLine(sales.Count());

            //foreach (var person in developers.Where(NameStartsWithS))
            //foreach (var person in developers.Where(e => e.Name.StartsWith("S")))
            var query = developers.Where(e => e.Name.Length == 5)
                                .OrderBy(e => e.Name);
            var query2 = from person in developers
                        where person.Name.Length == 5
                        orderby person.Name
                        select person;

            foreach (var person in query2)
            {
                Console.WriteLine(person.Name);
            }

            Console.ReadKey();
        }

        private static bool NameStartsWithS(Employee employee)
        {
            return employee.Name.StartsWith("S");
        }
    }
}
