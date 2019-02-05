using Xunit;
using CarPark.Api;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System;

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
            HttpResponseMessage result=new HttpResponseMessage();
            try
            {
                HttpContent content = CreateHttpContent<CarPark.Api.Infrastructure.Service.applicationlogin>(new Api.Infrastructure.Service.applicationlogin() { AppName = "CarParkApi", password = "manycars" });
                var response = _client.PostAsync(apiurl + path, content);
                var responseString = response.Result;
                result = responseString;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {

                if(result.StatusCode==System.Net.HttpStatusCode.OK)
                {
                  //this codeblock will run when succesfull test. A succesful login
                }
                else if(result.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    throw new Exception("No content code 204");
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception("Bad request code 500");
                }

            }
            //var content = new FormUrlEncodedContent(values);
          
 
           

       
        }

        [Fact]
        public void AuthenticateAppFail()
        {
            string path = "/Authenticate";
            HttpResponseMessage result = new HttpResponseMessage();
            try
            {
                HttpContent content = CreateHttpContent<CarPark.Api.Infrastructure.Service.applicationlogin>(new Api.Infrastructure.Service.applicationlogin() { AppName = "CarParkApi", password = "wrongpass" });
                var response = _client.PostAsync(apiurl + path, content);
                var responseString = response.Result;
                result = responseString;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {

                if (result.StatusCode== System.Net.HttpStatusCode.OK)
                {
                    throw new Exception("StatusCode=200. this test was not supposed to pass!");
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    //this codeblock will run when succesfull test. A failed login which turned sent back an empty content
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception("Bad request code 500");
                }

            }
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
