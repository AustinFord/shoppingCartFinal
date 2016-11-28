using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCartV2.Models;

namespace ShoppingCartV2.Controllers
{
    [HandleError]
    public partial class HomeController : Controller
    {
        // Text for Site Heading
        string siteHeading = "The Groceries Store";

        // Text for Order View Heading
        string orderHeading = "Groceries Order";

        // Text for View Heading for each Tab
        string[] tabHeadings = { "Home", "Fruits","Vegatables","Meats","Cheeses","Cereal",
                                         "Check-Out", "Admin", "About" };

        // View label displayed on each Tab
        string[] tabLabels = { "Home", "Fruits","Vegatables","Meats","Cheeses","Cereal",
                                       "Check-Out", "Admin", "About" };

        // View method name for each Tab
        string[] tabViews = { "Index", "Tab1Orders","Tab2Orders","Tab3Orders", "Tab4Orders", "Tab5Orders",
                                       "CheckOut", "Admin", "About" };

        // Text for View Heading of any Options columns
        string[] optionsColumnHeading = { "Fruits", "Vegatables", "Meats", "Cheeses", "Cereal" };

        // The tax rate is 5%
        decimal taxRate = 0.05M;

        // Email address of the company that will be in the "From" box
        //  of the confirmation message sent
        string websiteEmail = "order@GroceriesStore.com";


        public ActionResult Index()
        {
            Session["PageHeading"] = siteHeading;
            ViewBag.Title = "Welcome!";
            ViewBag.Message = "Modify this text to jump-start your Shopping Cart application.";
            ViewBag.Title = "Welcome to the Fun Cake Store!";
            ViewBag.Message = "<img src=\"/Images/logo.jpg\">";
            ViewBag.Message += "<br />This is the Cake Store of your Dreams!";

            return View();
        }

        public ActionResult About()
        {
            Session["PageHeading"] = siteHeading;
            ViewBag.Title = "About " + siteHeading;
            ViewBag.Message = "Your app description page.";

            return View();
        }

        // Action Method for First Product View
        public ActionResult Tab1Orders()
        {
            int tabNum = 1;
            Session["PageHeading"] = orderHeading;
            Session["ProductType"] = tabViews[tabNum];
            ViewBag.Message = Session["Message"] = tabHeadings[tabNum] + " Orders:";
            return View(LoadViewTableData(Session["ProductType"].ToString(), tabNum));
        }

        // Action Method for First Product View
        [HttpPost]
        public ActionResult Tab1Orders(string button, FormCollection collection)
        {
            int tabNum = 1;
            string pType = Session["ProductType"].ToString();
            int amount = Int32.Parse(Session[pType + "ItemAmount"].ToString());

            LoadSubmission(pType, amount, collection);

            for (int i = 1; i <= amount; i++)
            {
                int value;
                if (!Int32.TryParse(collection[i - 1], out value) || value < 0)
                {
                    ViewBag.Message = "<div style=\"color:#800\">" +
                                       "Error: Invalid entry in Item #" + i +
                                        "</div><br />" + Session["Message"].ToString();
                    return View(pType, LoadViewTableData(pType, tabNum));
                }
            }

            if (button == "Save And Go To Checkout")
            {
                return RedirectToAction("CheckOut");
            }
            else
            {
                // This is the View for the next product page
                return RedirectToAction(tabViews[tabNum + 1]);
            }
        }
    }
}

