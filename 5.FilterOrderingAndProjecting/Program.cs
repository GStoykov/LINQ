using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5.FilterOrderingAndProjecting
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessCars("fuel.csv");
            var manifacturers = ProcessManifacturers("manufacturers.csv");

            //var query = cars.Where(c => c.Manifacturer == "BMW" && c.Year == 2016)
            //                .OrderByDescending(c => c.Combined)
            //                .ThenBy(c => c.Name);

            var query = from car in cars
                        join manifacturer in manifacturers
                            on new { car.Manifacturer, car.Year } 
                            equals new { Manifacturer = manifacturer.Name, manifacturer.Year }
                        //where car.Manifacturer == "BMW" && car.Year == 2016
                        orderby car.Combined descending, car.Name ascending
                        select new // Anonymous types
                        {
                            manifacturer.Headquarters,
                            car.Name,
                            car.Combined
                        };

            var query2 = cars.Join(manifacturers,
                                        c => new { c.Manifacturer, c.Year },
                                        m => new { Manifacturer = m.Name, m.Year },
                                        (c, m) => new
                                        {
                                            m.Headquarters,
                                            c.Name,
                                            c.Combined
                                        })
                                .OrderByDescending(c => c.Combined)
                                .ThenByDescending(c => c.Name);
                                        
            foreach (var car in query2.Take(10))
            {
                Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            }


            // extension method anonymous type
            //var result = cars.Select(c => new { c.Manifacturer, c.Name, c.Combined });

            //foreach (var car in query.Take(10))
            //{
            //    Console.WriteLine($"{car.Manifacturer} {car.Name} : {car.Combined}");
            //}

            //--------

            //// collection of strings which are collections of characters
            //var result = cars.SelectMany(c => c.Name);

            //foreach (var name in result)
            //{
            //    Console.WriteLine(name);
            //}


        }

        private static List<Manifacturer> ProcessManifacturers(string path)
        {
            var query = File.ReadAllLines(path)
                            .Where(l => l.Length > 1)
                            .Select(l =>
                            {
                                var columns = l.Split(',');
                                return new Manifacturer
                                {
                                    Name = columns[0],
                                    Headquarters = columns[1],
                                    Year = int.Parse(columns[2])
                                };
                            });

            return query.ToList();
        }

        private static List<Car> ProcessCars(string path)
        {
            var query = File.ReadAllLines(path)
                            .Skip(1)
                            .Where(l => l.Length > 1)
                            .ToCar();

            //var query = File.ReadAllLines(path)
            //        .Skip(1)
            //        .Where(line => line.Length > 1)
            //        .Select(Car.ParseFromCsv)
            //        .ToList();

            return query.ToList();
        }

    }

    public static class CarExtensions
    {
        public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
        {

            foreach (var line in source)
            {
                var columns = line.Split(',');

                yield return new Car
                {
                    Year = int.Parse(columns[0]),
                    Manifacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3]),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])
                };
            }
        }
    }
}
