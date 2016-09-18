
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using WebApiLab01.Models;

namespace WebApiLab01
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach(var a in GetAllTodos())
            {
                Console.WriteLine($"Todo key: {a.Key} name: {a.Name} is comp: {a.IsComplete}");
            }

            Console.ReadKey();
        }

        static IList<TodoItem> GetAllTodos()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3375/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("/api/todo").Result;
                if(response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    var todos = JsonConvert.DeserializeObject<List<TodoItem>>(data);

                    return todos;
                }
            }

            return new List<TodoItem>(0);
        }
    }
}
