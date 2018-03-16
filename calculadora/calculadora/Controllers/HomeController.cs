using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace calculadora.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Session["primeiroOperador"] = true;
            Session["iniciaOperando"] = true;
            ViewBag.Display = "0";
            return View();
        }

        // POST: Home
        [HttpPost]
        public ActionResult Index(string bt, string display)
        {
            switch (bt)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":
                    if ((bool) Session["iniciaOperando"] || display.Equals("0"))
                        display = bt;
                    else
                        display += bt;
                    Session["iniciaOperando"] = false;
                    break;
                case "+/-":
                    display = Convert.ToDouble(display)*-1+"";
                    break;
                case ",":
                    if(!display.Contains(","))
                        display += ",";
                    break;
                case "+":
                case "-":
                case "X":
                case ":":
                case "=":
                    if ((bool)Session["primeiroOperador"]){
                        Session["primeiroOperando"] = display;
                        Session["iniciaOperando"] = true;
                        Session["operadorAnterior"] = bt;
                        Session["primeiroOperador"] = false;
                    }else{
                        double operando1 = Convert.ToDouble((String)Session["primeiroOperando"]);
                        double operando2 = Convert.ToDouble(display);

                        switch ((String) Session["operadorAnterior"]){
                            case "+":
                                display = operando1 + operando2 + "";
                                break;
                            case "-":
                                display = operando1 - operando2 + "";
                                break;
                            case "X":
                                display = operando1 * operando2 + "";
                                break;
                            case ":":
                                display = operando1 / operando2 + "";
                                break;
                        }
                        if (bt.Equals("="))
                            Session["primeiroOperador"] = true;
                        else{
                            Session["operadorAnterior"] = bt;
                            Session["primeiroOperador"] = false;
                        }
                        Session["primeiroOperando"] = display;
                        Session["operadorAnterior"] = bt;
                        Session["iniciaOperando"] = true;
                    }
                    break;
                case "C":
                    display = "0";
                    Session["iniciaOperando"] = true;
                    Session["primeiroOperador"] = true;
                    break;
                
            }

            ViewBag.Display = display;

            return View();
        }
    }
}