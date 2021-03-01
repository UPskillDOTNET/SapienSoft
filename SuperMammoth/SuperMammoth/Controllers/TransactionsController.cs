using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperMammoth.Models;
using System.Net.Http;
using System.Net;
using System.Net.Http.Json;
using SuperMammoth.Globals;

namespace SuperMammoth.Controllers
{
    public class TransactionsController : Controller
    {
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
                        transaction = transaction.OrderByDescending(t => t.Date);
                        break;
                    case "TransType":
                        transaction = transaction.OrderBy(t => t.TransactionTypeId);
                        break;
                    case "TransType_desc":
                        transaction = transaction.OrderByDescending(t => t.TransactionTypeId);
                        break;
                    default:
                        transaction = transaction.OrderBy(t => t.Date);
                        break;
                }
                int pageSize = 5;
                return View(PaginatedList<Transaction>.CreateAsync(transaction, pageNumber ?? 1, pageSize));
                
            }
        }

        // GET: TransactionsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TransactionsController/Create
        public ActionResult Create()
        {            
            
                return View();
            
        }

        // POST: TransactionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
