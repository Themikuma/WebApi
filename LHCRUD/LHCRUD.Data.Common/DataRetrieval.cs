using LHCRUD.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LHCRUD.Data.Common
{
    /// <summary>
    /// Utility class used to retrieve data used strictly for seeding
    /// </summary>
    public static class DataRetrieval
    {
        static readonly HttpClient client = new HttpClient();
        public static async Task<List<Book>> GetBooks(int count)
        {
            string responseBody = await client.GetStringAsync($"https://fakerapi.it/api/v1/books?_quantity={count}");
            var response = JsonConvert.DeserializeObject<Response>(responseBody);

            Console.WriteLine(responseBody);
            return response.Data;
        }

        class Response
        {
            public string Status { get; set; }
            public int Code { get; set; }
            public int Total { get; set; }
            public List<Book> Data { get; set; }
        }
    }
}
