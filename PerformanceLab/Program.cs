using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace PerformanceLab
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                const int iterations = 500000;

                Console.WriteLine("Comparing Property Get Methods");
                long direct = TestGetPropertiesDirect(iterations);
                long cached = TestGetPropertiesCached(iterations);
                Console.WriteLine(
                    "Cached access is {0} times faster than reflection access",
                    1.0 * direct / cached);

                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("Done, press r to go again!");
                if (Console.ReadKey().KeyChar != 'r')
                {
                    return;
                }
            }
        }

        private static long TestGetPropertiesDirect(int iterations)
        {
            Console.WriteLine("TestGetPropertiesDirect...");

            List<Type> types = GetTestTypes();
            Stopwatch sw = Stopwatch.StartNew();
            int gets = 0;
            for (int i = 0; i < iterations; i++)
            {
                foreach (Type type in types)
                {
                    List<PropertyInfo> properties = type.GetProperties().ToList();
                    gets++;
                }
            }

            Console.WriteLine(
                "  {0} iterations took {1} ms, {2} iterations/s",
                gets,
                sw.ElapsedMilliseconds,
                gets / (sw.ElapsedMilliseconds / 1000.0));

            return sw.ElapsedMilliseconds;
        }

        private static Dictionary<Type, List<PropertyInfo>> _propertyDictionary = new Dictionary<Type, List<PropertyInfo>>();

        private static long TestGetPropertiesCached(int iterations)
        {
            Console.WriteLine("TestGetPropertiesCached...");

            Stopwatch sw = Stopwatch.StartNew();
            List<Type> types = GetTestTypes();
            int gets = 0;
            for (int i = 0; i < iterations; i++)
            {
                foreach (Type type in types)
                {
                    List<PropertyInfo> properties = GetCachedProperties(type);
                    gets++;
                }
            }

            Console.WriteLine(
                "  {0} gets took {1} ms, {2} iterations/s",
                gets,
                sw.ElapsedMilliseconds,
                gets / (sw.ElapsedMilliseconds / 1000.0));

            return sw.ElapsedMilliseconds;
        }

        private static List<PropertyInfo> GetCachedProperties(Type type)
        {
            List<PropertyInfo> properties;
            if (_propertyDictionary.TryGetValue(type, out properties) == false)
            {
                properties = type.GetProperties().ToList();
                _propertyDictionary.Add(type, properties);
            }

            return properties;
        }

        // Returns a list of types to use for the subsequent tests
        private static List<Type> GetTestTypes()
        {
            return typeof(Kafala.Web.ViewModels.Donor.CreateDonorViewModel).Assembly.GetTypes().ToList();
        }
    }
}