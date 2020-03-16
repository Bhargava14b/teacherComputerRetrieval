using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherComputerRetrievalConsoleApp
{
    public class TeacherComputerRetrievalHelper
    {
        List<string> routes = new List<string>();

        public void RetrieveTeacherComputers()
        {
            PrintWelComeMessage();

            routes = ReadInputRouteList();

            string listOfRoutes = string.Join("\n", routes.Select(x => "From " + x[0] + " To " + x[1] + ", Distance is " + x[2]));

            PrintDashLine();
            Console.WriteLine(AppResources.InputRoutesList);
            Console.WriteLine(listOfRoutes);
            PrintDashLine();

            PerformActions();
        }
        public List<string> ReadInputRouteList()
        {
            List<string> routes = new List<string>();
            Console.WriteLine(AppResources.InputRoutesText);
            string input = Console.ReadLine();
            while (!ValidateInputRoutes(input))
            {
                Console.WriteLine(AppResources.InvalidRouteStructure + AppResources.InputRoutesText);
                input = Console.ReadLine();
            }
            routes = !string.IsNullOrEmpty(input) ? input.Split(',').Select(x => x.Trim()).ToList() : routes;

            return routes;
        }

        public void PrintWelComeMessage()
        {
            PrintDashLine();
            Console.WriteLine(AppResources.WelcomeText);
            PrintDashLine();
        }

        public void PrintDashLine()
        {
            Console.WriteLine(AppResources.DashLine);
        }

        public void PerformActions()
        {
            var questions = GetQuestions();
            Console.WriteLine(AppResources.SelectAction + "\n" + string.Join("\n", questions.Select(x => x.Key + ". " + x.Value)));
            Console.WriteLine(AppResources.YourInput);
            int action = 0;
            string strAction = Console.ReadLine();
            while (!int.TryParse(strAction, out action) || !questions.Any(x => x.Key.ToString() == strAction))
            {
                Console.WriteLine(AppResources.InvalidActionTryAgain);
                strAction = Console.ReadLine();
            }

            if (action == 1)
            {
                Console.WriteLine(AppResources.EnterRouteToGetDistance);
                int outDistance = GetDistanceBetweenRoutes();
                Console.WriteLine(AppResources.TotalDistanceBWRoutes + outDistance.ToString());

                string strInput = Console.ReadLine();
                while (string.IsNullOrEmpty(strInput) || strInput.ToUpper() != "YES")
                {
                    Console.WriteLine(AppResources.YouWantToProceed);
                    strInput = Console.ReadLine();
                }
                if (strInput.ToUpper() == "YES")
                    PerformActions();
            }
            else if (action == 2)
            {
                PrintPossibleRoutesWithStops(false);
            }
            else if (action == 3)
            {
                PrintPossibleRoutesWithStops(true);
            }
            else if (action == 4)
            {
                GetShortestDistanceRoute();
            }
            else if (action == 5)
            {
                GetAllPossibleRoutesHavingDistance();
            }
        }

        public Dictionary<int, string> GetQuestions()
        {
            return new Dictionary<int, string>()
            {
                { 1, AppResources.FindDistance },
                { 2, AppResources.FindNumberOfRoutesWithMaxStops },
                { 3, AppResources.FindNumberOfRoutesWithExactStops },
                { 4, AppResources.FindShortestRoute },
                { 5, AppResources.FindPossibleRoutesHavingDistance }
            };
        }

        public int GetDistanceBetweenRoutes()
        {
            string distanceRoute = Console.ReadLine();
            int outDistance = 0;
            while (!ValidateAndGetDistanceOfRoute(distanceRoute.ToUpper(), out outDistance))
            {
                distanceRoute = Console.ReadLine();
            }
            return outDistance;
        }

        public bool ValidateAndGetDistanceOfRoute(string route, out int totalDistance)
        {
            totalDistance = 0;
            if (string.IsNullOrEmpty(route))
            {
                Console.WriteLine(AppResources.EnterRouteToGetDistance);
                return false;
            }
            else if (!route.Contains("-"))
            {
                Console.WriteLine(AppResources.InvalidRoute + AppResources.EnterRouteToGetDistance);
                return false;
            }

            string[] strRoutes = route.Split('-');
            for (int i = 0; i < strRoutes.Length - 1; i++)
            {
                if (i + 1 >= strRoutes.Length)
                    return false;

                if (string.IsNullOrEmpty(strRoutes[i]) || string.IsNullOrEmpty(strRoutes[i + 1]))
                    continue;

                string relRoute = routes.FirstOrDefault(x => x.Contains(strRoutes[i] + strRoutes[i + 1]));
                if (relRoute != null)
                {
                    totalDistance += Convert.ToInt32(relRoute[2].ToString());
                }
                else
                {
                    Console.WriteLine(AppResources.NoSuchRoute);
                    return false;
                }
            }


            return true;
        }

        public void PrintPossibleRoutesWithStops(bool isExactStops)
        {
            Console.WriteLine(AppResources.EnterStartingAcademy);
            string starting = Console.ReadLine();
            while (string.IsNullOrEmpty(starting))
            {
                Console.WriteLine(AppResources.EnterStartingAcademy);
                starting = Console.ReadLine();
            }

            Console.WriteLine(AppResources.EnterEndingAcademy);
            string ending = Console.ReadLine();
            while (string.IsNullOrEmpty(ending))
            {
                Console.WriteLine(AppResources.EnterEndingAcademy);
                starting = Console.ReadLine();
            }

            int outNumberOfStops = 0;
            string strInvalidNumberMsg = isExactStops ? AppResources.EnterExactNumberOfStops : AppResources.EnterMaxNumberOfStops;
            Console.WriteLine(strInvalidNumberMsg);
            string strNumberofStops = Console.ReadLine();
            while (string.IsNullOrEmpty(strNumberofStops))
            {
                Console.WriteLine(strInvalidNumberMsg);
                strNumberofStops = Console.ReadLine();
            }

            while (!int.TryParse(strNumberofStops, out outNumberOfStops))
            {
                Console.WriteLine(AppResources.EnterValidNumber);
                strNumberofStops = Console.ReadLine();
            }

            var allPossibleRoutes = GetAllPossibleRoutes(starting.ToUpper(), ending.ToUpper());

            if (isExactStops)
            {
                string[] restrictedRoutes = allPossibleRoutes.Where(x => x.Split('-').Length == outNumberOfStops + 1).ToArray();
                Console.WriteLine(string.Format(AppResources.PossibleRoutesBW, starting.ToUpper(), ending.ToUpper(), restrictedRoutes.Length));
                foreach (string strR in restrictedRoutes)
                    Console.WriteLine(strR);
            }
            else
            {
                string[] restrictedRoutes = allPossibleRoutes.Where(x => x.Split('-').Length <= outNumberOfStops + 1).ToArray();
                Console.WriteLine(string.Format(AppResources.PossibleRoutesBW, starting.ToUpper(), ending.ToUpper(), restrictedRoutes.Length));
                foreach (string strR in restrictedRoutes)
                    Console.WriteLine(strR);
            }

            string strInput = Console.ReadLine();
            while (string.IsNullOrEmpty(strInput) || strInput.ToUpper() != "YES")
            {
                Console.WriteLine(AppResources.YouWantToProceed);
                strInput = Console.ReadLine();
            }
            if (strInput.ToUpper() == "YES")
                PerformActions();
        }

        public List<string> GetAllPossibleRoutes(string starting, string ending)
        {
            Dictionary<string, string[]> lstRoutesVisited = new Dictionary<string, string[]>();

            string[] possibleRoutes = GetRoutesBetweenAcademies(starting.ToUpper(), ending.ToUpper(), ref lstRoutesVisited);
            List<string> allPossibleRoutes = new List<string>();

            if (starting.ToUpper() == ending.ToUpper())
            {
                string[] possibleSubtypeRoutes = GetRoutesBetweenAcademies(ending.ToUpper(), ending.ToUpper(), ref lstRoutesVisited);

                for (int i = 0; i < possibleSubtypeRoutes.Length; i++)
                {
                    for (int j = 0; j < possibleRoutes.Length; j++)
                    {
                        allPossibleRoutes.Add(possibleRoutes[j]);
                        allPossibleRoutes.Add(possibleRoutes[j].Substring(0, possibleRoutes[j].Length - 2) + "-" + possibleSubtypeRoutes[i]);
                    }
                }
            }
            else
            {
                allPossibleRoutes = possibleRoutes.ToList();
            }


            allPossibleRoutes = allPossibleRoutes.Distinct().ToList();
            return allPossibleRoutes;
        }

        public string[] GetRoutesBetweenAcademies(string starting, string ending, ref Dictionary<string, string[]> lstRoutesVisited)
        {
            if (lstRoutesVisited.ContainsKey(starting + ending))
                return lstRoutesVisited[starting + ending];
            else
                lstRoutesVisited.Add(starting + ending, new string[] { });

            string[] routestWithStartPoint = routes.Where(x => x[0].ToString() == starting).ToArray();
            List<string> lstRoutes = new List<string>();
            for (int i = 0; i < routestWithStartPoint.Length; i++)
            {
                if (routestWithStartPoint[i][1].ToString() == ending)
                {
                    lstRoutes.Add(starting + "-" + routestWithStartPoint[i][1]);
                }
                else
                {
                    string[] childRoutes = GetRoutesBetweenAcademies(routestWithStartPoint[i][1].ToString(), ending, ref lstRoutesVisited);
                    foreach (string s in childRoutes)
                    {
                        lstRoutes.Add(starting + "-" + s);
                    }
                }
            }

            if (lstRoutesVisited.ContainsKey(starting + ending))
                lstRoutesVisited[starting + ending] = lstRoutes.ToArray();

            return lstRoutes.ToArray();
        }

        public bool ValidateInputRoutes(string routes)
        {
            if (string.IsNullOrEmpty(routes))
                return false;
            else if (!routes.Split(',').Any(x => ValidateRoute(x.Trim())))
                return false;

            return true;
        }

        public bool ValidateRoute(string route)
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

        public void GetShortestDistanceRoute()
        {
            Console.WriteLine(AppResources.EnterStartingAcademy);
            string starting = Console.ReadLine();
            while (string.IsNullOrEmpty(starting))
            {
                Console.WriteLine(AppResources.EnterStartingAcademy);
                starting = Console.ReadLine();
            }

            Console.WriteLine(AppResources.EnterEndingAcademy);
            string ending = Console.ReadLine();
            while (string.IsNullOrEmpty(ending))
            {
                Console.WriteLine(AppResources.EnterEndingAcademy);
                starting = Console.ReadLine();
            }


            Dictionary<string, string[]> lstRoutesVisited = new Dictionary<string, string[]>();
            string[] possibleRoutes = GetRoutesBetweenAcademies(starting.ToUpper(), ending.ToUpper(), ref lstRoutesVisited);

            Dictionary<string, int> allPossibleRouteDistances = new Dictionary<string, int>();
            foreach (string strRoute in possibleRoutes)
            {
                int distance = 0;
                string[] strRoutes = strRoute.Split('-');
                for (int i = 0; i < strRoutes.Length - 1; i++)
                {
                    if (i + 1 >= strRoutes.Length)
                        break;

                    string relRoute = routes.FirstOrDefault(x => x.Contains(strRoutes[i] + strRoutes[i + 1]));
                    if (relRoute != null)
                    {
                        distance += Convert.ToInt32(relRoute[2].ToString());
                    }
                }
                allPossibleRouteDistances.Add(strRoute, distance);
            }

            var shortestDistance = allPossibleRouteDistances.OrderBy(x => x.Value).FirstOrDefault();
            Console.WriteLine(string.Format(AppResources.ShortestDistanceRoute, starting.ToUpper(), ending.ToUpper(), (shortestDistance.Equals(new KeyValuePair<string, int>()) ? "0" : (shortestDistance.Value + " (" + shortestDistance.Key + ")"))));

            string strInput = Console.ReadLine();
            while (string.IsNullOrEmpty(strInput) || strInput.ToUpper() != "YES")
            {
                Console.WriteLine(AppResources.YouWantToProceed);
                strInput = Console.ReadLine();
            }
            if (strInput.ToUpper() == "YES")
                PerformActions();
        }

        public void GetAllPossibleRoutesHavingDistance()
        {
            Console.WriteLine(AppResources.EnterStartingAcademy);
            string starting = Console.ReadLine();
            while (string.IsNullOrEmpty(starting))
            {
                Console.WriteLine(AppResources.EnterStartingAcademy);
                starting = Console.ReadLine();
            }

            Console.WriteLine(AppResources.EnterEndingAcademy);
            string ending = Console.ReadLine();
            while (string.IsNullOrEmpty(ending))
            {
                Console.WriteLine(AppResources.EnterEndingAcademy);
                starting = Console.ReadLine();
            }

            int outDistance = 0;
            Console.WriteLine(AppResources.EnterDistance);
            string strDistance = Console.ReadLine();
            while (string.IsNullOrEmpty(strDistance))
            {
                Console.WriteLine(AppResources.EnterDistance);
                strDistance = Console.ReadLine();
            }

            while (!int.TryParse(strDistance, out outDistance))
            {
                Console.WriteLine(AppResources.EnterValidNumber);
                strDistance = Console.ReadLine();
            }

            Dictionary<string, string[]> lstRoutesVisited = new Dictionary<string, string[]>();
            string[] possibleRoutes = GetAllPossibleRoutes(starting.ToUpper(), ending.ToUpper()).ToArray();

            Dictionary<string, int> allPossibleRouteDistances = new Dictionary<string, int>();
            foreach (string strRoute in possibleRoutes)
            {
                int distance = 0;
                string[] strRoutes = strRoute.Split('-');
                for (int i = 0; i < strRoutes.Length - 1; i++)
                {
                    if (i + 1 >= strRoutes.Length)
                        break;

                    string relRoute = routes.FirstOrDefault(x => x.Contains(strRoutes[i] + strRoutes[i + 1]));
                    if (relRoute != null)
                    {
                        distance += Convert.ToInt32(relRoute[2].ToString());
                    }
                }
                allPossibleRouteDistances.Add(strRoute, distance);
            }

            var possibleRoutesHavingDistance = allPossibleRouteDistances.Where(x => x.Value < outDistance).ToList();
            Console.WriteLine(string.Format(AppResources.NumberOfPossibleRoutes, starting.ToUpper(), ending.ToUpper(), outDistance, possibleRoutesHavingDistance.Count()));
            Console.WriteLine(AppResources.ListOfPossibleRoutesText);
            foreach (var str in possibleRoutesHavingDistance)
                Console.WriteLine(str.Key);

            string strInput = Console.ReadLine();
            while (string.IsNullOrEmpty(strInput) || strInput.ToUpper() != "YES")
            {
                Console.WriteLine(AppResources.YouWantToProceed);
                strInput = Console.ReadLine();
            }
            if (strInput.ToUpper() == "YES")
                PerformActions();
        }
    }
}
