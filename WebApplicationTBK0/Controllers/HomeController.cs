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

            string returnUrl = "http://localhost:8080/home/Return";
            string finalUrl = "http://localhost:8080/home/Final";

            var initResult = transaction.initTransaction(amount, buyOrder, sessionId, returnUrl, finalUrl);

            var tokenWs = initResult.token;
            var formAction = initResult.url;

            ViewBag.Amount = amount;
            ViewBag.BuyOrder = buyOrder;
            ViewBag.TokenWS = tokenWs;
            ViewBag.FormAction = formAction;

            return View();
        }

        public ActionResult Return()
        {
            var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;
            string tokenWs = Request.Form["token_ws"];

            var result = transaction.getTransactionResult(tokenWs);
            var output = result.detailOutput[0];
            if (output.responseCode == 0)
                {
                ViewBag.UrlRedirection = result.urlRedirection;
                ViewBag.TokenWs = tokenWs;
                ViewBag.ResponseCode = output.responseCode;
                ViewBag.Amount = output.amount;
                ViewBag.AuthorizationCode = output.authorizationCode; 
                }   

            return View();
        }
        public ActionResult Final()
        {
            return View();
        }
    }
}
