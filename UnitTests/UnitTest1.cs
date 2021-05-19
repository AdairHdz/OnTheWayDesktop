using System;
using System.Net;
using System.Threading.Tasks;
using BusinessLayer.BusinessEntities;
using DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RestSharp;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {

        public static IRestClient MockRestClient<T>(HttpStatusCode httpStatusCode, string json) where T: new()
        {
            var data = JsonConvert.DeserializeObject<T>(json);
            var response = new Mock<IRestResponse<T>>();
            response.Setup(_ => _.StatusCode).Returns(httpStatusCode);
            response.Setup(_ => _.Data).Returns(data);


            var mockedRestClient = new Mock<IRestClient>();
            mockedRestClient.Setup(x => x.Execute<T>(It.IsAny<IRestRequest>()))
                .Returns(response.Object);
            
            return mockedRestClient.Object;
        }       

        [TestMethod]
        public void SecondTest()
        {
            var client = new RestClient("http://192.168.100.41:8080/v1");            
            var request = new RestRequest("requesters/123");            
            var response = client.Get<ServiceRequester>(request);       
            Console.WriteLine(response);
            Console.WriteLine(response);
        }
    }
}
