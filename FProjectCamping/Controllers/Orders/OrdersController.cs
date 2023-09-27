using FProjectCamping.Models.EFModels;
using FProjectCamping.Models.Services;
using FProjectCamping.Models.ViewModels.Orders;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FProjectCamping.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderService _orderService = new OrderService();

        // GET: Orders
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Pay(int orderId)
        {
            var model = _orderService.GetOrder(orderId);
            return View(model);
        }

        [HttpPost]
        public ActionResult CallBack(CallbackReq req)
        {           
            // 付款成功
            if (req.IsSuccessed)
            {
                _orderService.UpdateStatus(req.OrderNumber, Common.OrderStatusEnum.Payment);
                return RedirectToAction("Index", "Member");
            }
            else // 例外狀況: 付款失敗
            {
                _orderService.UpdateStatus(req.OrderNumber, Common.OrderStatusEnum.Failed);
                return RedirectToAction("Pay", new { orderId = req.OrderId });
            }

        }
    }
}