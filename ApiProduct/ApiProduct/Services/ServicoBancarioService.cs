using ApiProduct.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiProduct.Services
{
    public class ServicoBancarioService
    {


        public static async Task<string> RetornaServicosBancariosGet(string url, HttpClient client)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
                
    }
}
