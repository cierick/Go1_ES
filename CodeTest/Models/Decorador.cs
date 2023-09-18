using Newtonsoft.Json;
using Quote.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PruebaIngreso.Models
{
    public class Decorador : IMarginProvider
    {
        public async Task<dynamic> getResultApiAsync(string url)
        {
            String margin = @"{""margin"":0.0}";
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    result.EnsureSuccessStatusCode();
                    string resultContentString = await result.Content.ReadAsStringAsync();
                    var resultContent = JsonConvert.DeserializeObject<dynamic>(resultContentString);

                    if (result.StatusCode == HttpStatusCode.OK)
                        return resultContent;
                    else
                        return margin;
                }
            }
            catch (Exception err)
            {
                return margin;
            }

        }
    }
}