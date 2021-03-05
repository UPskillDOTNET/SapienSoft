using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperMammoth.Models;
using SuperMammoth.Models.PayPal;
using System.Net.Http;
using System.Net;
using System.Net.Http.Json;
using SuperMammoth.Globals;

namespace SuperMammoth.Controllers
{
    public class TransactionsController : Controller
    {
       
        public PayPalPaymentCreatedResponse created { get; set; }
        
        // GET: TransactionsController
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            // Check for session and retrieve role
            var userSession = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");
            if (userSession == null)
            {
                ViewBag.Role = "";
            }
            else
            {
                if (userSession.Roles.Contains("Administrator") == true)
                    ViewBag.Role = "Administrator";
                else if (userSession.Roles.Contains("User") == true)
                    ViewBag.Role = "User";
            }

            IEnumerable<Transaction> transaction = null;
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler))
            {
                var temp = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");

                client.BaseAddress = new Uri("https://localhost:44398/api/");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + temp.Token);
                var response = client.GetAsync("transactions/user");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadFromJsonAsync<IList<Transaction>>();
                    read.Wait();
                    transaction = read.Result;
                }
                else
                {
                    //erro
                    transaction = Enumerable.Empty<Transaction>();
                    ModelState.AddModelError(string.Empty, "Server error occured");
                }


                ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
                ViewData["TransactionTypeIdSortParm"] = sortOrder == "TransType" ? "TransType_desc" : "TransType";
                ViewData["CurrentFilter"] = searchString;
                ViewData["CurrentSort"] = sortOrder;

                if (searchString != null)
                {
                    pageNumber = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    transaction = transaction.Where(t => t.TransactionType.Name.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "date_desc":
                        transaction = transaction.OrderBy(t => t.Date);
                        break;
                    case "TransType":
                        transaction = transaction.OrderBy(t => t.TransactionTypeId);
                        break;
                    case "TransType_desc":
                        transaction = transaction.OrderByDescending(t => t.TransactionTypeId);
                        break;
                    default:
                        transaction = transaction.OrderByDescending(t => t.Date);
                        break;
                }
                int pageSize = 10;
                return View(PaginatedList<Transaction>.CreateAsync(transaction, pageNumber ?? 1, pageSize));
                
            }
        }


        //All transactions
        public ActionResult AdminIndex(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            // Check for session and retrieve role
            var userSession = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");
            if (userSession == null)
            {
                ViewBag.Role = "";
            }
            else
            {
                if (userSession.Roles.Contains("Administrator") == true)
                    ViewBag.Role = "Administrator";
                else if (userSession.Roles.Contains("User") == true)
                    ViewBag.Role = "User";
            }

            IEnumerable<Transaction> transaction = null;
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler))
            {
                var temp = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");

                client.BaseAddress = new Uri("https://localhost:44398/api/");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + temp.Token);
                var response = client.GetAsync("transactions");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadFromJsonAsync<IList<Transaction>>();
                    read.Wait();
                    transaction = read.Result;
                    foreach(var item in transaction)
                    {
                        var userId = item.UserId;
                        var response2 = client.GetAsync("users/ById/"+ userId);
                        response2.Wait();
                        var result2 = response2.Result;
                        if(result2.IsSuccessStatusCode)
                        {
                            var read2 = result2.Content.ReadAsStringAsync();
                            string userName = read2.Result;
                            item.UserId = userName;

                        }
                    }
                }
                else
                {
                    //erro
                    transaction = Enumerable.Empty<Transaction>();
                    ModelState.AddModelError(string.Empty, "Server error occured");
                }


                ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
                ViewData["TransactionTypeIdSortParm"] = sortOrder == "TransType" ? "TransType_desc" : "TransType";
                ViewData["CurrentFilter"] = searchString;
                ViewData["CurrentSort"] = sortOrder;

                if (searchString != null)
                {
                    pageNumber = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    transaction = transaction.Where(t => t.TransactionType.Name.Contains(searchString) || t.UserId.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "date_desc":
                        transaction = transaction.OrderBy(t => t.Date);
                        break;
                    case "TransType":
                        transaction = transaction.OrderBy(t => t.TransactionTypeId);
                        break;
                    case "TransType_desc":
                        transaction = transaction.OrderByDescending(t => t.TransactionTypeId);
                        break;
                    default:
                        transaction = transaction.OrderByDescending(t => t.Date);
                        break;
                }
                int pageSize = 10;
                return View(PaginatedList<Transaction>.CreateAsync(transaction, pageNumber ?? 1, pageSize));

            }
        }


        // GET: TransactionsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TransactionsController/Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create( Transaction transaction)
        {
            var userSession = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");
            if (userSession == null)
            {
                ViewBag.Role = "";
            }
            else
            {
                if (userSession.Roles.Contains("Administrator") == true)
                    ViewBag.Role = "Administrator";
                else if (userSession.Roles.Contains("User") == true)
                    ViewBag.Role = "User";
            }
            RegisterModel currentUser = null;
            

            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler))
            {

                client.BaseAddress = new Uri("https://localhost:44398/api/");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + userSession.Token);

                //Lets get our user info 

                var response = client.GetAsync("user/info");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadFromJsonAsync<RegisterModel>();
                    read.Wait();
                    currentUser = read.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error occured");
                }


                var PM = currentUser.PaymentMethodId.ToString();
               if (ViewBag.Role == "Administrator")
                {
                    response = client.PostAsJsonAsync("transactions/user/addfunds", transaction);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        response.Wait();
                        return RedirectToAction("AdminIndex", "Transactions");
                    }
                    else return BadRequest("Problems comunicating with Medusa1");
                }
                else if (PM == "1")
                {

                    response = client.PostAsJsonAsync("transactions/user/addfunds/paypal", transaction);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var temp = response.Result.Content.ReadFromJsonAsync<PayPalPaymentCreatedResponse>();
                        temp.Wait();
                        PayPalPaymentCreatedResponse created = temp.Result;
                        TempData["data"]= created.id.ToString();     
                        return Redirect(created.links.ElementAt(1).href.ToString());
                        
                    }
                    else return BadRequest("Problems comunicating with Medusa2");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Sorry, this payment method is not implemented yet.");
                }
                return View();
            } 
        }

       



        [HttpPost]

        public IActionResult Execute()
        {
            string temp = TempData["data"] as string;
            var userSession = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");
            if (userSession == null)
            {
                ViewBag.Role = "";
            }
            else
            {
                if (userSession.Roles.Contains("Administrator") == true)
                    ViewBag.Role = "Administrator";
                else if (userSession.Roles.Contains("User") == true)
                    ViewBag.Role = "User";
            }
           
            

            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler))
            {

                client.BaseAddress = new Uri("https://localhost:44398/api/");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + userSession.Token);

                

                var response = client.PostAsJsonAsync("transactions/user/addfunds/paypal/execute?paymentId=" + temp, "");
                if (response.Result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index"); ;

                }
                else return BadRequest("Problems comunicating with Medusa2");

            }

            }
        // GET: TransactionsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TransactionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TransactionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
