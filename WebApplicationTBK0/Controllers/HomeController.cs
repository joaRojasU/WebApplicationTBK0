using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transbank.Webpay;

namespace WebApplicationTBK0.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;


            var amount = 2000; //Valor de prueba fijo, Se tiene que llamar el valor desde la APP

            var buyOrder = new Random().Next(100000, 999999999).ToString(); //Este objeto genera un numero de orden de compra
            var sessionId = "sessionId";

            string returnUrl = "https://localhost:8080/home/Return";
            string finalUrl = "https://localhost:8080/home/Final";

            var initResult = transaction.initTransaction(amount, buyOrder, sessionId, returnUrl, finalUrl);

            var tokenWs = initResult.token;
            var formAction = initResult.url;

            ViewBag.Amount = amount;
            ViewBag.BuyOrder = buyOrder;
            ViewBag.TokenWS = tokenWs;
            ViewBag.FormAction = formAction;

            return View();
        }
    }
}
