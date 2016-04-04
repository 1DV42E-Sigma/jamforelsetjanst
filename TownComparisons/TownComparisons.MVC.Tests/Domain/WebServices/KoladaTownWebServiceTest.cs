using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TownComparisons.Domain.WebServices;
using TownComparisons.Domain.Models;
using TownComparisons.Domain.Entities;
using TownComparisons.Domain.Helpers;
using System.Collections.Generic;

namespace TownComparisons.MVC.Tests.Domain.WebServices
{
    [TestClass]
    public class KoladaTownWebServiceTest
    {
        private KoladaTownWebService _webService;

        [TestInitialize]
        public void SetUp()
        {
            _webService = new KoladaTownWebService();
            
        }

        /// <summary>
        /// Test GetOrganisationalUnitById method
        /// </summary>
        [TestMethod]
        public void Test_KoladaTownWebService_GetOrganisationalUnitById()
        {
            string UnitId = "V15E128300201";
            var expected = new OrganisationalUnit("Kolada", "V15E128300201", "Elinebergsskolan");

            _webService = new KoladaTownWebService();
            var actual = _webService.GetOrganisationalUnit(UnitId);

            Assert.AreEqual(actual.Name, expected.Name);
            Assert.AreEqual(actual.OrganisationalUnitId, expected.OrganisationalUnitId);

        }

        /// <summary>
        /// Test that method will read data from Database from known ID
        /// </summary>
        [TestMethod()]
        public void Test_KoladaTownWebService_GetOrganisationalUnitTest()
        {
            string UnitId = "V15E108000901";
            var expected = new OrganisationalUnitInfo();
            expected.Id = 3;
            expected.Name = "Fridlevstads skola F-6";
            expected.OrganisationalUnitId = "V15E108000901";

            _webService = new KoladaTownWebService();
            var actual = _webService.GetOrganisationalUnit(UnitId);

            Assert.AreEqual(actual.Name, expected.Name);
            Assert.AreEqual(actual.OrganisationalUnitId, expected.OrganisationalUnitId);
        }

        /// <summary>
        /// Test if it is possible to assign value to properties of instance of class OrganisationalUnitInfo
        /// </summary>
        [TestMethod]
        public void Test_Settings_DeclareOrganisationalUnitInfo()
        {
            int id = 4;
            string name = "testName";

            var ouTest = new OrganisationalUnitInfo();
            try
            {
                ouTest.Id = id;
                ouTest.Name = name;
            }
            catch
            {
                Assert.Fail();
            }
           
        }
        /// <summary>
        /// Checks if getAllPropertyQueries returns list of objects of correct type
        /// </summary>
        [TestMethod]
        public void Test_GetAllPropertyQueries()
        {
            
            var actual = _webService.GetAllPropertyQueries();
            var t = actual[0];

            if (!(t is PropertyQueryGroup))
            {
                Assert.Fail();
            }
        }
        /// <summary>
        /// Checks if method getAllPropertyQueries returns property that has an
        /// additional list of type PropertyQuery.
        /// Nesting of objects.
        /// </summary>
        [TestMethod]
        public void Test_GetAllPropertyQueries_getQueries()
        {
            PropertyQueryGroup propQueryGroup = new PropertyQueryGroup();

            var actual = _webService.GetAllPropertyQueries();
            var t = actual[0].Queries[0];

            if (!(t is PropertyQuery))
            {
                Assert.Fail();
            }
        }
        /// <summary>
        /// Checks if QueryGroupId has datatype string
        /// </summary>
        [TestMethod]
        public void Test_GetAllPropertyQueries_getId()
        {
            PropertyQueryGroup propQueryGroup = new PropertyQueryGroup();

            var actual = _webService.GetAllPropertyQueries();
            var t = actual[0].QueryGroupId;


            if (t.GetType() != typeof(string))
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Checks if Title has datatype string
        /// </summary>
        [TestMethod]
        public void Test_GetAllPropertyQueries_getTitle()
        {
            PropertyQueryGroup propQueryGroup = new PropertyQueryGroup();

            var actual = _webService.GetAllPropertyQueries();
            var t = actual[0].Title;

            if (t.GetType() != typeof(string))
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Checks if WebServiceName has datatype string
        /// </summary>
        [TestMethod]
        public void Test_GetAllPropertyQueries_getWebServiceName()
        {
            PropertyQueryGroup propQueryGroup = new PropertyQueryGroup();

            var actual = _webService.GetAllPropertyQueries();
            var t = actual[0].WebServiceName;

            if (t.GetType() != typeof(string))
            {
                Assert.Fail();
            }
        }


    }
}
