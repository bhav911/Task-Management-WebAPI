using MVCCrud.Models.Context;
using MVCCrud.Models.CustomModel;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MVCCrud.Common
{
    public class WebApiHelper
    {
        public static async Task<string> HttpClientRequestResponseGet(string url)
        {   
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53766/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return responseData;
                }
                return null;
            }
        }

        public static async Task<string> HttpClientRequestResponsePost(string url, Tasks newTask, int[] studentList, string method, LoginModel credentials, NewRegistrationModel newPerson)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53766/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = new HttpResponseMessage();
                switch (method)
                {
                    case "createTask":
                        response = await client.PostAsJsonAsync(url, newTask);
                        break;
                    case "AssignTask":
                        response = await client.PostAsJsonAsync(url, studentList);
                        break;
                    case "SignIn":
                        response = await client.PostAsJsonAsync(url, credentials);
                        break;
                    case "SignUp":
                        response = await client.PostAsJsonAsync(url, newPerson);
                        break;
                }

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return responseData;
                }
                return null;
            }
        }
    }
}
