using MyShop.Core.Models;
using MyShop.Core.VIewModels;
using MyShop.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class CompanyManagerController : Controller
    {
        DataContext context = new DataContext();
        // GET: CompanyManager
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            ViewBag.CompanyState = new SelectList(context.States, "Id", "State");
            ViewBag.Country = new SelectList(context.Countries, "Id", "CountryName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Register(CompanyViewModel company)
        
        {
            try
            {
                var stateList = context.States.ToList();
                var countryList = context.Countries.ToList();
                if (ModelState.IsValid)
                {
                    
                    var isEmailAlreadyExists = context.Companies.Any(x => x.ContactPersonEmail == company.ContactPersonEmail || x.CompanyPhone == company.CompanyPhone);
                    if (isEmailAlreadyExists)
                    {
                        ModelState.AddModelError("ContactPersonEmail", "Email already exists");
                        ModelState.AddModelError("CompanyPhone", "Phone No. Already Exists");

                        ViewBag.CompanyState = new SelectList(stateList, "Id", "State");

                        ViewBag.Country = new SelectList(countryList, "Id", "CountryName");
                        return View(company);
                    }

                    Company objCompany = new Company(company);
                    context.Companies.Add(objCompany);
                    context.SaveChanges();
                    ViewBag.Message = "Success!!! Your Registration request has been sent to admin, you will get notified soon";
                    ViewBag.CompanyState = new SelectList(stateList, "Id", "State");
                    ViewBag.Country = new SelectList(countryList, "Id", "CountryName");
                    return View("Success");
                }
                ViewBag.CompanyState = new SelectList(stateList, "Id", "State");
                ViewBag.Country = new SelectList(countryList, "Id", "CountryName");
            }
            catch (Exception ex)
            {
                var stateList = context.States.ToList();
                ViewBag.CompanyState = new SelectList(stateList, "Id", "State");
                var countryList = context.Countries.ToList();
                ViewBag.Country = new SelectList(countryList, "Id", "CountryName");
                return View("Error", new HandleErrorInfo(ex, "CompanyManager", "Register"));
            }
            return View(company);
        }
        public ActionResult Success()
        {
            return View();
        }
        public JsonResult CheckUsernameAvailability(string userdata)
        {
            System.Threading.Thread.Sleep(200);
            var SeachData = context.Companies.Where(x => x.ContactPersonEmail == userdata).SingleOrDefault();
            if (SeachData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }

        }
        public JsonResult CheckPhoneAvailability(string userdata)
        {
            System.Threading.Thread.Sleep(200);
            var SeachData = context.Companies.Where(x => x.CompanyPhone == userdata).SingleOrDefault();
            if (SeachData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }

        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(CompanyLoginViewModel loginViewModel)
        {
            Company obj = new Company();
            if (ModelState.IsValid)
            {

                var objCompany = context.Companies.Where(x => x.ContactPersonEmail == loginViewModel.ContactPersonEmail && x.Password == loginViewModel.Password).FirstOrDefault();
             

                    if (objCompany != null)
                    {
                        if (objCompany.IsActive == false)
                        {
                            ViewBag.Message = "Your account is not active yet. Please contact Admin";
                            return View();
                        }
                        else
                        {
                            
                            Session["userInfo"] = objCompany;

                            return RedirectToAction("Dashboard");
                        }

                    }
                    else
                    {
                        ViewBag.Message= "Invalid EmailId/Password. If new user please Register";
                        return View("Login");
                    }
                
            }
            return View();
        }
        public ActionResult Dashboard()
        {
            if (Session["userInfo"] != null)
            {
                Company obj = (Company)Session["userInfo"];
                var objCompany = context.Companies.Where(x => x.Id == obj.Id).FirstOrDefault();

                var state = context.States.Where(x => x.Id == obj.StatesId).FirstOrDefault();

                var country = context.Countries.Where(x => x.Id == obj.CountryId).FirstOrDefault();
                CompanyViewModel vm = new CompanyViewModel(objCompany);
                vm.CompanyState = state.State;
                vm.Country = country.CountryName;
                return View(vm);
            }
            else
            {
                return View("Login");
            }

        }
        public ActionResult Edit()
        {
            if (Session["userInfo"] != null)
            {
                Company obj = (Company)Session["userInfo"];
                var objCompany = context.Companies.Where(x => x.Id == obj.Id).FirstOrDefault();

                var state = context.States.Where(x => x.Id == obj.StatesId).FirstOrDefault();

                var country = context.Countries.Where(x => x.Id == obj.CountryId).FirstOrDefault();

                var stateList = context.States.ToList();
                ViewBag.CompanyState = new SelectList(stateList, "Id", "State", obj.CountryId);


                var countryList = context.Countries.ToList();
                ViewBag.Country = new SelectList(countryList, "Id", "CountryName");
                CompanyEditViewModel vem = new CompanyEditViewModel(objCompany);
                return View(vem);
            }
            else
            {
                return View("Login");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompanyEditViewModel company)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var obj = context.Companies.Where(x => x.ContactPersonEmail == company.ContactPersonEmail).FirstOrDefault();
                    obj.CompanyName = company.CompanyName;
                    obj.ContactPersonName = company.ContactPersonName;
                    obj.ContactPersonPMobile = company.ContactPersonName;
                    obj.CompanyPhone = company.CompanyPhone;
                    obj.CompanyAddress = company.CompanyAddress;
                    obj.CountryId = company.Country;
                    obj.StatesId = company.CompanyState;
                    context.Entry(obj).State = EntityState.Modified;

                    context.SaveChanges();
                    var stateList = context.States.ToList();
                    ViewBag.CompanyState = new SelectList(stateList, "Id", "State");
                    var countryList = context.Countries.ToList();
                    ViewBag.Country = new SelectList(countryList, "Id", "CountryName");
                    return RedirectToAction("Dashboard");
                }

            }
            catch (Exception ex)
            {
                var stateList = context.States.ToList();
                ViewBag.CompanyState = new SelectList(stateList, "Id", "State");
                var countryList = context.Countries.ToList();
                ViewBag.Country = new SelectList(countryList, "Id", "CountryName");
                return View("Error", new HandleErrorInfo(ex, "CompanyManager", "Register"));
            }
            return View("Dashboard");
        }
        public ActionResult ChangePassword()
        {
            if (Session["userInfo"] != null)
            {
                Company obj = (Company)Session["userInfo"];
                var Email = context.Companies.Where(x => x.ContactPersonEmail == obj.ContactPersonEmail).FirstOrDefault();
                ChangePasswordViewModel cp = new ChangePasswordViewModel(Email);
                return View(cp);
            }
            else
            {
                return View("Login");
            }

        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {

            if (ModelState.IsValid)
            {
                var password = context.Companies.Where(x => x.ContactPersonEmail == model.ContactPersonEmail).FirstOrDefault();
                if (model.OldPassword == password.Password)
                {
                    password.Password = model.NewPassword;
                    context.Entry(password).State = EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("Dashboard");
                }

            }

            return View();
        }
        public ActionResult Logout()
        {
            if (Session["userInfo"] != null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Login");
            }

        }
    }
}