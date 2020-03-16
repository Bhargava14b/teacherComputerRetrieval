using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeacherComputerRetrievalConsoleApp;

namespace TCR.UnitTest
{
    [TestClass]
    public class ComputerRetrievalUnitTest
    {
        [TestMethod]
        public void DistanceBetweenAcademiesABC()
        {
            TeacherComputerRetrievalHelper obj = new TeacherComputerRetrievalHelper();
            bool result = obj.ValidateRoute("AB3");
            Assert.AreEqual(result, true);
        }
    }
}
