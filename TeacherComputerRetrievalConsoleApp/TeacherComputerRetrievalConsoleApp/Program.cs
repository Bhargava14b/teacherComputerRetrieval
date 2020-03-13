using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherComputerRetrievalConsoleApp
{
    public class Program
    {
        static List<string> routes = new List<string>();
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the list of routes comma seperated...");

            string input = Console.ReadLine();


            while (!ValidateInputRoutes(input))
            {
                Console.WriteLine("Invalid route structure. Please enter the list of routes comma seperated...");
                input = Console.ReadLine();
            }
            routes = !string.IsNullOrEmpty(input) ? input.Split(',').Select(x => x.Trim()).ToList() : routes;

            string listOfRoutes = string.Join("\n", routes.Select(x => "From " + x[0] + " To " + x[1] + ", Distance is " + x[2]));

            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("List of routes and distance between them:");
            Console.WriteLine(listOfRoutes);
            Console.WriteLine("------------------------------------------------------------------------------------------");

            var questions = GetQuestions();
            Console.WriteLine("Select from below actions:\n" + string.Join("\n", questions.Select(x => x.Key + ". " + x.Value)));
            int action = 0;
            string strAction = Console.ReadLine();
            while (!int.TryParse(strAction, out action) || !questions.Any(x => x.Key.ToString() == strAction))
            {
                Console.WriteLine("Invalid action! Please try again.");
                strAction = Console.ReadLine();
            }

            if (action == 1)
            {
                Console.WriteLine("Please enter a route to get the distance:");
                string distanceRoute = Console.ReadLine();
                int outDistance = 0;
                while (!ValidateDistanceRoute(distanceRoute, out outDistance))
                {
                    distanceRoute = Console.ReadLine();
                }

                Console.WriteLine("Total Distance between this route is:" + outDistance.ToString());

                Console.ReadLine();
            }
            else if (action == 2)
            {
                Console.WriteLine("Please enter the starting academy:");
                string starting = Console.ReadLine();
                while (string.IsNullOrEmpty(starting))
                {
                    Console.WriteLine("Please enter the starting academy:");
                    starting = Console.ReadLine();
                }

                Console.WriteLine("Please enter the ending academy:");
                string ending = Console.ReadLine();
                while (string.IsNullOrEmpty(ending))
                {
                    Console.WriteLine("Please enter the ending academy:");
                    starting = Console.ReadLine();
                }

                Console.WriteLine("Do you want to restrict the number of stops.. yes/no?:");
                string restrictToNumberOfStops = Console.ReadLine();
                while (string.IsNullOrEmpty(restrictToNumberOfStops))
                {
                    Console.WriteLine("Do you want to restrict the number of stops.. yes/no?:");
                    restrictToNumberOfStops = Console.ReadLine();
                }
                if (restrictToNumberOfStops.ToUpper() == "YES")
                {
                    Console.WriteLine("Please enter the number of stops:");
                    string strNumberofStops = Console.ReadLine();
                    while (string.IsNullOrEmpty(strNumberofStops))
                    {
                        Console.WriteLine("Please enter the number of stops:");
                        strNumberofStops = Console.ReadLine();
                    }

                    int outNumberOfStops = 0;
                    while (!int.TryParse(strNumberofStops, out outNumberOfStops))
                    {
                        Console.WriteLine("Please enter a valid number:");
                        strNumberofStops = Console.ReadLine();
                    }

                }



            }
            else if (action == 3)
            {

            }
        }

        public static Dictionary<int, string> GetQuestions()
        {
            return new Dictionary<int, string>()
            {
                { 1,  "Find total distance of a input route" },
                { 2, "Find Number of different routes between two academies" },
                { 3, "Find shortest route between two academies"}
            };
        }

        public static bool ValidateInputRoutes(string routes)
        {
            if (string.IsNullOrEmpty(routes))
                return false;
            else if (!routes.Split(',').Any(x => ValidateRoute(x)))
                return false;

            return true;
        }

        public static bool ValidateRoute(string route)
        {
            int outDistance = 0;
            if (string.IsNullOrEmpty(route))
                return false;
            else if (route.Length != 3)
                return false;
            else if (!int.TryParse(route[2].ToString(), out outDistance))
                return false;

            return true;
        }

        public static bool ValidateDistanceRoute(string route, out int totalDistance)
        {
            totalDistance = 0;
            if (string.IsNullOrEmpty(route))
            {
                Console.WriteLine("Please enter a route to get the distance:");
                return false;
            }
            else if (!route.Contains("-"))
            {
                Console.WriteLine("Invalid Route. Please enter a route to get the distance:");
                return false;
            }

            string[] strRoutes = route.Split('-');
            for (int i = 0; i < strRoutes.Length - 1; i++)
            {
                if (i + 1 >= strRoutes.Length)
                    return false;

                string relRoute = routes.FirstOrDefault(x => x.Contains(strRoutes[i] + strRoutes[i + 1]));
                if (relRoute != null)
                {
                    totalDistance += Convert.ToInt32(relRoute[2].ToString());
                }
                else
                {
                    Console.WriteLine("NO SUCH ROUTE");
                    return false;
                }
            }


            return true;
        }

        //public static int GetTotalDistanceOfRoute(string route)
        //{

        //}
    }

    public class RouteValidator
    {

    }
}
