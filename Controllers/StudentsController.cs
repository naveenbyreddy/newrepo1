using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using studentdetails.Models;
using System.Collections.Specialized;
using System.Text;

namespace studentdetails.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Students
        public ActionResult Index()
        {
            IEnumerable<StudDetails> studdata = null;
            using (WebClient webClient = new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44377/api/";

                var json = webClient.DownloadString("Students");
                var list = JsonConvert.DeserializeObject<List<StudDetails>>(json);
                studdata = list.ToList();
                return View(studdata);
            }
        }

        public ActionResult Details(int id)
        {

            StudDetails studdata;
            using (WebClient webClient = new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44377/api/";

                var json = webClient.DownloadString("Students/" + id);
                //  var list = emp 
                studdata = JsonConvert.DeserializeObject<StudDetails>(json);
            }
            return View(studdata);
        }

        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(StudDetails model)
        {

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.BaseAddress = "https://localhost:44377/api/";
                    var url = "Students/POST";
                    //webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string data = JsonConvert.SerializeObject(model);
                    var response = webClient.UploadString(url, data);
                    JsonConvert.DeserializeObject<StudDetails>(response);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            StudDetails studdata;
            using (WebClient webClient = new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44377/api/";

                var json = webClient.DownloadString("Students/" + id);
                //  var list = emp 
                studdata = JsonConvert.DeserializeObject<StudDetails>(json);
            }
            return View(studdata);

        }


        public ActionResult Edit(int id)
        {
            StudDetails studdata;
            using (WebClient webClient = new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44377/api/";

                var json = webClient.DownloadString("Students/" + id);
                //  var list = emp 
                studdata = JsonConvert.DeserializeObject<StudDetails>(json);
            }


            return View(studdata);
        }
        [HttpPost]
        public ActionResult Delete(int id, StudDetails model)
        {

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    NameValueCollection nv = new NameValueCollection();
                    string url = "https://localhost:44377/api/Students/" + id;
                    var response = Encoding.ASCII.GetString(webClient.UploadValues(url, "Delete", nv));

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(int id, StudDetails model)
        {

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.BaseAddress = "https://localhost:44377/api/Students/" + id;
                    //var url = "Values/Put/" + id;
                    //webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string data = JsonConvert.SerializeObject(model);
                    var response = webClient.UploadString(webClient.BaseAddress, "PUT", data);
                    StudDetails modeldata = JsonConvert.DeserializeObject<StudDetails>(response);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }
    }
}
