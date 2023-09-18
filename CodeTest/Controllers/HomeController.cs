using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PruebaIngreso.Models;
using Quote.Contracts;
using Quote.Models;
using RestSharp;

namespace PruebaIngreso.Controllers
{
    public class HomeController : Controller
    {


        private readonly IQuoteEngine quote;

        private String codigos = "E-E10-PF2SHOW500"; //Codigo para realizar llamada a la Api   :  E-U10-UNILATIN 204 : E-U10-DSCVCOVE 404   : E-E10-PF2SHOW500

        public HomeController(IQuoteEngine quote)
        {
            this.quote = quote;
         }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            var request = new TourQuoteRequest
            {
                adults = 1,
                ArrivalDate = DateTime.Now.AddDays(1),
                DepartingDate = DateTime.Now.AddDays(2),
                getAllRates = true,
                GetQuotes = true,
                RetrieveOptions = new TourQuoteRequestOptions
                {
                    GetContracts = true,
                    GetCalculatedQuote = true,
                },
                Language = Language.Spanish,
                TourCode = "",
            };

            var result = this.quote.Quote(request);
            var tour = result.Tours.FirstOrDefault();
            ViewBag.Message = "Test 1 Correcto";
            return View(tour);
        }



        public ActionResult Test2()
        {
            ViewBag.Message = "Test 2 Correcto";
            return View();
        }

        public async Task<ActionResult> Test3()
        {
            try
            {               
                var url = "https://codetest.free.beeceptor.com/margin/"+codigos+"";

                dynamic ObtenerResultadoApi = await LLamarApiTest(url);

                if (ObtenerResultadoApi != null)
                    ViewBag.Resultado = ObtenerResultadoApi;
                else
                    ViewBag.Resultado = @"{""margin"":0.0}";

                return View();

            }
            catch (Exception err)
            {

                throw;
            }
      
        }

        public async Task<dynamic> LLamarApiTest(string url) {
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
                return null;
            }
        }

        public async Task<ActionResult> Test4()
        {

            var url = "https://codetest.free.beeceptor.com/margin/" + codigos + "";
            var decorador = new Decorador();
            dynamic ObtenerResultadoApi = await decorador.getResultApiAsync(url);
            ViewBag.Resultado = ObtenerResultadoApi;
            return View();
        }
    }
}