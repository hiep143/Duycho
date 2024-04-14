using Microsoft.AspNetCore.Mvc;
using DTTT2.Models;

namespace DTTT2.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public IActionResult CalculateImpedance(double Zin, double RL, double Frequency, string MatchingType, string ConnectionType, double QualityFactor)
        {
            try
            {
                // Tạo một đối tượng IndexModel và truyền các giá trị từ request
                var model = new IndexModel(Zin, RL, Frequency, MatchingType, ConnectionType, QualityFactor);

                // Gọi phương thức CalculateImpedance để tính toán
                model.CalculateImpedance();

                // Trả về kết quả tính toán dưới dạng JSON
                return Json(model.ImpedanceResults);
            }
            catch (Exception ex)
            {
                // Trả về view "Error" nếu có lỗi xảy ra
                return View("Error", ex);
            }
        }
    }
}
