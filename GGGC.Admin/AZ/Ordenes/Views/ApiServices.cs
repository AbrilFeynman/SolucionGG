using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace GGGC.Admin.AZ.Ordenes.Views
{
    public class ApiServices
    {
        public async Task<bool> Clan(string codigo, int renglon)
        {
            var client = new HttpClient();
            var model = new OrdenItem
            {
                Numero_De_Documento = codigo,
                Renglon = renglon
            };

            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("https://funcionggsolution.azurewebsites.net/api/SaveData", content);
            return response.IsSuccessStatusCode;
        }

    }
}
