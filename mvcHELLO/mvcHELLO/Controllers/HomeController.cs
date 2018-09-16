using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvcHELLO.Models;
using Microsoft.Extensions.Configuration;
using mvcHELLO.DAL;
using Microsoft.AspNetCore.Http; //ci serve per la sessione e per salvare i valori nella cache

namespace mvcHELLO.Controllers
{
    public class HomeController : Controller // ritorna tutte le pagine, gestisce i link
    {
        private readonly IConfiguration configuration;
        public HomeController(IConfiguration config)
        {
            this.configuration = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Page2(Person person)
        {
            //inviamo il valore al DB
            //e facciamo la validazione 

            //string connStr = configuration.GetConnectionString("MyConnString");

            DALPerson dp = new DALPerson(configuration);
            int uID = dp.addUser(person);

            person.UID = uID;
            //salviamo l'uid della sessione
            HttpContext.Session.SetString("uID", uID.ToString()); //scrive sulla sessione

            string strUID = HttpContext.Session.GetString("uID"); //legge dalla sessione

            return View(person);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
