using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using _3.Features.MyLinq;

namespace _3.Features
{
    class Program
    {
        static void Main(string[] args)
        {
            //Func<int, int> square = x => x * x;
            //Console.WriteLine(square(3));

            //Func<int, int, int> add = (x, y) => x + y;
            //Console.WriteLine(add(3, 4));

            //Func<int, int, int> subtraction = (x, y) =>
            //{
            //    int temp = x - y;
            //    return temp;
            //};
            //Console.WriteLine(subtraction(5,2));


            // ---------------------------
            var developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Scott" },
                new Employee { Id = 2, Name = "Chris" }
            };

            var sales = new List<Employee>()
            {
                new Employee { Id=3, Name = "Alex" }
            };

            foreach (var employee in developers.Where(e => e.Name.Length == 5)
                                               .OrderBy(e => e.Name))
            {
                Console.WriteLine(employee.Name);
            }

            //IEnumerator<Employee> enumerator = developers.GetEnumerator();
            //while (enumerator.MoveNext())
            //{
            //    Console.WriteLine(enumerator.Current.Name);
            //}
        }
    }
}
