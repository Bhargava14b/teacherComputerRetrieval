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
            var helper = new TeacherComputerRetrievalHelper();
            helper.RetrieveTeacherComputers();
        }

    }
}
