using GameStore.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Show()
        {
            var order = GetOrder();

            return View(order);
        }

        public ActionResult Edit(int gameId)
        {
            var orderViewModel = GetOrder();
            if (orderViewModel.OrderDetails.Any(o => o.GameId == gameId))
            {
                orderViewModel.OrderDetails.First(d => d.GameId == gameId).Quantity++;
            }
            else
            {
                orderViewModel.OrderDetails.Add(new OrderDetailsViewModel{GameId = gameId, Quantity = 1});
            }

            return RedirectToAction("Show");
        }

        private OrderViewModel GetOrder()
        {
            if (Session["order"] == null)
            {
                Session["order"] = new OrderViewModel();
            }

            return Session["order"] as OrderViewModel;
        }
    }
}