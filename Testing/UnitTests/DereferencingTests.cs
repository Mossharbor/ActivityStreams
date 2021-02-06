using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    class DereferencingTests
    {
        /*/// <summary>
        /// This function validation that we canuse replacement strings from output json
        /// </summary>
        /// <param name="jsonToCheck">the json we are going to parse for this test</param>
        [Ignore]
        [DataTestMethod]
        [DataRow("TestFiles\\Test.json")]
        [DataRow("TestFiles\\Test2.json")]
        public void SubstituteSecretFromConnectionManagerFunctions(string jsonToCheck)
        {
             ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example100.json")).Build();

            string tokenString = string.Empty;
            string subscriptionId = string.Empty;
            bool foundkey = false;

            var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) =>
            {
                    // Mock the http client call to the cognitive token service to get a jwt token
                    tokenString = "TestToken";

                    if (!foundkey)
                    {
                        string key = request.Headers.GetValues("Auth").FirstOrDefault();
                        Assert.AreEqual(key, "bogusKey");
                        foundkey = true;
                    }

                    request.SetConfiguration(configuration);
                    var response = request.CreateResponse(HttpStatusCode.OK, tokenString);
                    return Task.FromResult(response);
            });

            var mockFactory = new Mock<IHttpClientFactory>();
            var client = new HttpClient(clientHandlerStub);
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            //TODO resolve http query to object

            Assert.AreEqual("\"TestToken\"", t, "the token value that was replced was incorrect");

            if (!foundkey)
            {
                Assert.Fail("Did not find the key in the query for the token retreval");
            }
        }*/
    }
}
