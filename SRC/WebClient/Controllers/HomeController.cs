using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebClient.GetFornameDto;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly Uri WebAPIURL;
        public HomeController()
        {
            WebAPIURL = new Uri(ConfigurationManager.AppSettings["WepApi"]);
        }
        public ActionResult Index()
        {
            FetchFornameDto _FetchFornameDto = new FetchFornameDto();
            List<Patient> AllPatients=new List<Patient>();

            Tuple<List<FornameModel>, Patient,List<Patient>> _formModel;
            List<FornameModel> _FornameModel = _FetchFornameDto.FetchForname().ToList();

            //Calling Web API to collect All list of Patients
            using (HttpClient httpclient = new HttpClient())
            {
                httpclient.BaseAddress = WebAPIURL;
                var Response = httpclient.GetAsync("api/AllPatients");
                Response.Wait();
                var Result = Response.Result;
                if (Result.IsSuccessStatusCode)
                {
                    AllPatients = (new JavaScriptSerializer()).Deserialize<List<Patient>>(Result.Content.ReadAsStringAsync().Result); 
                }

            }



            _formModel = new Tuple<List<FornameModel>, Patient, List<Patient>>(_FornameModel, new Patient() {DateofBirth=null },AllPatients);


            return View(_formModel);
        }

        [HttpPost]
        public ActionResult SavePatient(Patient patient)
        {
            if (ModelState.IsValid)
            {
                patient.DateofBirth = patient.DateofBirth ?? DateTime.MaxValue;
                HttpContent content = null;

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = WebAPIURL;
                    HttpResponseMessage response = client.PostAsJsonAsync("api/SavePatient", patient).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return null;
        }
    }
}