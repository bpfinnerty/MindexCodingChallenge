using System.Net;
using System.Net.Http;
using System.Text;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
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
        public void CreateCompensation_Returns_Created()
        {
            // Arrange
            var compensation = new Compensation()
            {
                CompensationId = "b7839309-3348-463b-a7e3-5de1c168beb3",
                Salary = 86000,
                EffectiveDate = "2022-01-01",
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.AreEqual(compensation.CompensationId, newCompensation.CompensationId);
            Assert.AreEqual(compensation.Salary, newCompensation.Salary);
            Assert.AreEqual(compensation.EffectiveDate, newCompensation.EffectiveDate);
        }

        [TestMethod]
        public void CreateCompensation_Returns_BadRequest()
        {
            // Arrange a bad compensation. I.E if there is no employee with this ID we should not
            // Add this compensation to our database
            var compensationBad = new Compensation()
            {
                CompensationId = "badId",
                Salary = 86000,
                EffectiveDate = "2022-01-01",
            };

            var requestContent = new JsonSerialization().ToJson(compensationBad);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;
            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }


        [TestMethod]
        public void GetCompensation_Returns_Ok()
        {
            // Arrange a bad compensation. I.E if there is no employee with this ID we should not
            // Add this compensation to our database
            var compensation = new Compensation()
            {
                CompensationId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                Salary = 86000,
                EffectiveDate = "2022-01-01",
            };
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;
            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            var getResponse = getRequestTask.Result;
            Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode);
            var newCompensation = getResponse.DeserializeContent<Compensation>();

            Assert.AreEqual(compensation.CompensationId, newCompensation.CompensationId);
            Assert.AreEqual(compensation.Salary, newCompensation.Salary);
            Assert.AreEqual(compensation.EffectiveDate, newCompensation.EffectiveDate);
        }

        [TestMethod]
        public void GetCompensation_Returns_NotFound()
        {
            // Arrange
            var employeeId = "03aa1462-ffa9-4978-901b-7c001562cf6f";
            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            var getResponse = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
    }
}