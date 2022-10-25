using System.Net;
using System.Net.Http;
using System.Text;
using System;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration
{
    [TestClass]
    public class ReportingStructureControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }


        [TestMethod]
        public void GetReportingStructure_Returns_Ok()
        {
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reportingStructure/{employeeId}");
            var getResponse = getRequestTask.Result;
            Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode);
            var reportingStructure = getResponse.DeserializeContent<ReportingStructure>();
            Console.WriteLine(new JsonSerialization().ToJson(reportingStructure));
            Assert.AreEqual(4, reportingStructure.NumberOfReports);
        }

        [TestMethod]
        public void GetReportingStructure_Returns_NotFound()
        {
            // Arrange
            var employeeId = "badId";
            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reportingStructure/{employeeId}");
            var getResponse = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
    }
}