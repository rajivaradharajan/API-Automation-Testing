using APIAutomationCust.Pages;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace APIAutomationCust
{
    public class CustAPI
    {
        readonly static string email = "user17@sector36.com";
        readonly static string password = "user@12023";
        static string auth = "{\"email\": \"user17@sector36.com\",\"password\": \"user@12023\"}";
        static string baseUrl = "http://api.qaauto.co.nz/api";
        static string version = "v1";
        //dynamically fetch token value from response refer auth()
         string token = "";
        //string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwOlwvXC9hcGkucWFhdXRvLmNvLm56XC9hcGlcL3YxXC9hdXRoXC9sb2dpbiIsImlhdCI6MTYyOTI2NjM2NSwiZXhwIjoxNjI5MjY5OTY1LCJuYmYiOjE2MjkyNjYzNjUsImp0aSI6Inp2dmtkVmo5dHVDeVNRR3YiLCJzdWIiOjU0LCJwcnYiOiIyM2JkNWM4OTQ5ZjYwMGFkYjM5ZTcwMWM0MDA4NzJkYjdhNTk3NmY3In0.-sIvvAIhKyq02_9K5z6pU_AtPGTDy_vt1OW9_CqfNoA";
         string postbody ="{\"id\": \"1\",\"company_name\": \"test\", \"vat_number\": \"test\",\"phone\": \"1231231231\", \"website\": \"wwww\", \"currency\": \"string\", \"country\": \"string\", \"default_language\": \"string\"}";
       

        [OneTimeSetUp]

        public void OneTimeSetUp()
        {
            //bad style and difficult to read in json format
            //auth = "{\"email\": \"user17@sector36.com\",\"password\": \"user@12023\"}";

            //good style - create class user and declare email and password , then create object and set values 
            Login user = new Login(email, password);
            // Convert json to string - newtonsoft.com or Serialize above object user to json and store in auth -add newtonjson and restsharp pack
            auth = JsonConvert.SerializeObject(user);
            //to check if its serialised .. create a funtion called dummy test code below to verify by printing it
           //call auth method
            Auth();
        }

        //[Test]

        //public void dummy()
        //{
        //    TestContext.WriteLine(auth);
        //}

        [Test]
        public void Auth()
        {
            //setting up client
            var client = new RestClient($"{baseUrl}/{version}/auth/login");
            //what type of request to send
            var request = new RestRequest(Method.POST);

            //Add header info in restsharp
            request.AddHeader("Content-Type", "application/json");

            //To set body info
            request.AddParameter("application/json", auth, ParameterType.RequestBody);

            //executes request
            IRestResponse response = client.Execute(request);

            string jsonData = response.Content;
            //deserialize json and store in data {got below code from copying the accesstoken from {} and put in jsontocharp.com and create class accesstoken and paste those
            AccessToken data = JsonConvert.DeserializeObject<AccessToken>(jsonData);
            //print to verify deserialize or print just the accesstoken from response in o/p
            TestContext.WriteLine(data.access_token);
            //to generate new token , get from response and pass to token
            token = data.access_token;
            //print full response
            //TestContext.WriteLine(response.Content);

        }
    

        [Test]
        public void GetAllCust()
        {
            
            //setting up client
            var client = new RestClient($"{baseUrl}/{version}/customers");

            //To send the GET?PUT?POST?DEL
            var request = new RestRequest(Method.GET);

            //Add header info in restsharp
            //request.AddHeader("Content-Type", "application/json");

            // Add auth info
            //request.AddHeader("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwOlwvXC9hcGkucWFhdXRvLmNvLm56XC9hcGlcL3YxXC9hdXRoXC9sb2dpbiIsImlhdCI6MTYyOTI2NjM2NSwiZXhwIjoxNjI5MjY5OTY1LCJuYmYiOjE2MjkyNjYzNjUsImp0aSI6Inp2dmtkVmo5dHVDeVNRR3YiLCJzdWIiOjU0LCJwcnYiOiIyM2JkNWM4OTQ5ZjYwMGFkYjM5ZTcwMWM0MDA4NzJkYjdhNTk3NmY3In0.-sIvvAIhKyq02_9K5z6pU_AtPGTDy_vt1OW9_CqfNoA");
            request.AddHeader("Authorization", $"Bearer {token}");

            //executes request
            IRestResponse response = client.Execute(request);
            TestContext.WriteLine(response.Content);
        }

        [Test]
        public void GetSingleCust()
        {

            //setting up client
            var client = new RestClient($"{baseUrl}/{version}/customers/1");

            //To send the GET?PUT?POST?DEL
            var request = new RestRequest(Method.GET);

            //Add header info in restsharp
            //request.AddHeader("Content-Type", "application/json");

            // Add auth info
            //request.AddHeader("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwOlwvXC9hcGkucWFhdXRvLmNvLm56XC9hcGlcL3YxXC9hdXRoXC9sb2dpbiIsImlhdCI6MTYyOTI2NjM2NSwiZXhwIjoxNjI5MjY5OTY1LCJuYmYiOjE2MjkyNjYzNjUsImp0aSI6Inp2dmtkVmo5dHVDeVNRR3YiLCJzdWIiOjU0LCJwcnYiOiIyM2JkNWM4OTQ5ZjYwMGFkYjM5ZTcwMWM0MDA4NzJkYjdhNTk3NmY3In0.-sIvvAIhKyq02_9K5z6pU_AtPGTDy_vt1OW9_CqfNoA");
            request.AddHeader("Authorization", $"Bearer {token}");
            //executes request
            IRestResponse response = client.Execute(request);
            TestContext.WriteLine(response.Content);
        }

        //public void createcusttest()
        //{
        //    Customer expectedcustomer = new Customer();
        //    set data
        //     string jsonBody = JsonConvert.SerializeObject(expectedcustomer);
        //    string responseData = CreateCust(jsonBody);
        //    Customer actualcustomer = JsonConvert.DeserializeObject<Customer>(responseData);

        //    assert-- expected cust with actual cust
        // }

        [Test]
        public void CreateCust()
        {

            //setting up client
            var client = new RestClient($"{baseUrl}/{version}/customers");

            //To send the GET?PUT?POST?DEL
            var request = new RestRequest(Method.POST);

            //Add header info in restsharp
            request.AddHeader("Content-Type", "application/json");

            // Add auth info
            //request.AddHeader("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwOlwvXC9hcGkucWFhdXRvLmNvLm56XC9hcGlcL3YxXC9hdXRoXC9sb2dpbiIsImlhdCI6MTYyOTI2NjM2NSwiZXhwIjoxNjI5MjY5OTY1LCJuYmYiOjE2MjkyNjYzNjUsImp0aSI6Inp2dmtkVmo5dHVDeVNRR3YiLCJzdWIiOjU0LCJwcnYiOiIyM2JkNWM4OTQ5ZjYwMGFkYjM5ZTcwMWM0MDA4NzJkYjdhNTk3NmY3In0.-sIvvAIhKyq02_9K5z6pU_AtPGTDy_vt1OW9_CqfNoA");
            request.AddHeader("Authorization", $"Bearer {token}");
           //Post body
            request.AddParameter("application/json", postbody, ParameterType.RequestBody);
            //executes request
            IRestResponse response = client.Execute(request);
            TestContext.WriteLine(response.Content);
        }

        [Test]
        public void DeleteCust()
        {

            //setting up client
            var client = new RestClient($"{baseUrl}/{version}/customers/1");

            //To send the GET?PUT?POST?DEL
            var request = new RestRequest(Method.DELETE);

            //Add header info in restsharp
            request.AddHeader("Content-Type", "application/json");

            // Add auth info
            //request.AddHeader("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwOlwvXC9hcGkucWFhdXRvLmNvLm56XC9hcGlcL3YxXC9hdXRoXC9sb2dpbiIsImlhdCI6MTYyOTI2NjM2NSwiZXhwIjoxNjI5MjY5OTY1LCJuYmYiOjE2MjkyNjYzNjUsImp0aSI6Inp2dmtkVmo5dHVDeVNRR3YiLCJzdWIiOjU0LCJwcnYiOiIyM2JkNWM4OTQ5ZjYwMGFkYjM5ZTcwMWM0MDA4NzJkYjdhNTk3NmY3In0.-sIvvAIhKyq02_9K5z6pU_AtPGTDy_vt1OW9_CqfNoA");
            request.AddHeader("Authorization", $"Bearer {token}");

            //Post body
            request.AddParameter("application/json", postbody, ParameterType.RequestBody);
            //executes request
            IRestResponse response = client.Execute(request);
            TestContext.WriteLine(response.Content);
        }

        string updatebody = "{\"id\": \"1\",\"company_name\": \"test\", \"vat_number\": \"test\",\"phone\": \"1231231231\", \"website\": \"wwww\", \"currency\": \"string\", \"country\": \"string\", \"default_language\": \"string\"}";
        [Test]
        public void UpdateCust()
        {

            //setting up client
            var client = new RestClient($"{baseUrl}/{version}/customers/1");

            //To send the GET?PUT?POST?DEL
            var request = new RestRequest(Method.DELETE);

            //Add header info in restsharp
            request.AddHeader("Content-Type", "application/json");

            // Add auth info
            //request.AddHeader("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwOlwvXC9hcGkucWFhdXRvLmNvLm56XC9hcGlcL3YxXC9hdXRoXC9sb2dpbiIsImlhdCI6MTYyOTI2NjM2NSwiZXhwIjoxNjI5MjY5OTY1LCJuYmYiOjE2MjkyNjYzNjUsImp0aSI6Inp2dmtkVmo5dHVDeVNRR3YiLCJzdWIiOjU0LCJwcnYiOiIyM2JkNWM4OTQ5ZjYwMGFkYjM5ZTcwMWM0MDA4NzJkYjdhNTk3NmY3In0.-sIvvAIhKyq02_9K5z6pU_AtPGTDy_vt1OW9_CqfNoA");
            request.AddHeader("Authorization", $"Bearer {token}");

            //Post body
            request.AddParameter("application/json", updatebody, ParameterType.RequestBody);
            //executes request
            IRestResponse response = client.Execute(request);
            TestContext.WriteLine(response.Content);
        }
    }
}