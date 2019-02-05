using Xunit;
using CarPark.Api;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace CarPark.Test
{
    public class UnitTest1
    {
        private static readonly HttpClient _client = new HttpClient();
        private string apiurl= "http://localhost:58366/api/values";

        private HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
        //private HttpClient initClient()
        //{
        //    if (_client == null)
        //        _client = new HttpClient();

        //    return _client;
        //}
        [Fact]
        public void Test1()
        {

        }

        [Fact]
        public void AuthenticateAppSuccess()
        {
            string path = "/Authenticate";
            var values = new Dictionary<string, string>
            {
                { "username", "hello" },
                { "password", "world" }
            };

            //var content = new FormUrlEncodedContent(values);
            HttpContent content = CreateHttpContent<CarPark.Api.Infrastructure.Service.applicationlogin>(new Api.Infrastructure.Service.applicationlogin() { AppName = "CarParkApi", password = "manycars" });
 
             var response = _client.PostAsync(apiurl+path, content);

            var responseString = response.Result;
        }

        [Fact]
        public void AuthenticateAppFail()
        {

        }

        [Fact]
        public void LoginSuccess()
        {

        }
        [Fact]
        public void LoginFailed()
        {

        }

        [Fact]
        public void LogoutSuccess()
        {

        }
        [Fact]
        public void LogoutFailed()
        {

        }


    }
}
