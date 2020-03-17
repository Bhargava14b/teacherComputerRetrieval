using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeacherComputerRetrievalConsoleApp;

namespace TCR.UnitTest
{
    [TestClass]
    public class ComputerRetrievalUnitTest
    {
        TeacherComputerRetrievalHelper tcrHelper;
        public ComputerRetrievalUnitTest()
        {

        }

        #region TestInitialize

        [TestInitialize]
        public void TestInitialize()
        {
            tcrHelper = new TeacherComputerRetrievalHelper();
            tcrHelper.routes = new System.Collections.Generic.List<string>()
            {
               "AB5", "BC4", "CD8", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7"
            };
        }

        #endregion

        [TestMethod]
        public void GetDistanceBetweenRoutes()
        {
            int result = tcrHelper.GetDistanceBetweenRoutes("A-B-C");
            Assert.AreEqual(result, 9);
        }

        [TestMethod]
        public void GetNumberOfTripBetweenAcademiesWithMaxStops()
        {
            string[] outPut = new string[] { "C-D-C", "C-E-B-C" };
            string[] result = tcrHelper.GetNumberOfTripBetweenAcademiesWithMaxStops("C", "C", 3);
            Assert.AreEqual(result.Length, outPut.Length);
        }

        [TestMethod]
        public void GetNumberOfTripBetweenAcademiesWithExactStops()
        {
            string[] outPut = new string[] { "A-B-C-D-C", "A-D-C-D-C", "A-D-E-B-C" };
            string[] result = tcrHelper.GetNumberOfTripBetweenAcademiesWithExactStops("A", "C", 4);
            Assert.AreEqual(result.Length, outPut.Length);
        }

        [TestMethod]
        public void GetLengthOfShortestDistanceRoute()
        {
            int outPut = 9;
            var result = tcrHelper.GetShortestDistanceRoute("A", "C");
            Assert.AreEqual(result.Value, outPut);
        }


        [TestMethod]
        public void GetAllPossibleRoutesHavingDistance()
        {
            int outPut = 7;
            var result = tcrHelper.GetAllPossibleRoutesHavingDistance("C", "C", 30);
            Assert.AreEqual(result.Count, outPut);
        }
    }
}
