using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    [Authorize]
    public class MyOrderController : Controller
    {
        IOrderService orderService;
        IRepository<Customer> customers;
        public MyOrderController(IOrderService OrderService, IRepository<Customer> Customers)
        {
            this.orderService = OrderService;
            this.customers = Customers;
        }
        // GET: ViewMyOrder
        public ActionResult Index()
        {
            List<Order> orders = orderService.GetOrderList().Where(u => u.Email == User.Identity.Name).ToList();
            return View(orders);
        }
        public ActionResult Detail(string Id)
        {
            Order order = orderService.GetOrder(Id);
            return View(order);
        }
        public ActionResult NewList()
        {
            return View();
        }
        public ActionResult Datatable()
        {
            var orderList = orderService.GetOrderList();
            return Json(new { data = orderList }, JsonRequestBehavior.AllowGet);
        }
    }
}