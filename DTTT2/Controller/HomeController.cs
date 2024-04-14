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
                // Gọi phương thức CalculateImpedance từ lớp ImpedanceMatching và truyền giá trị Q
                double[][] impedance = ImpedanceMatching.CalculateImpedance(Zin, RL, Frequency, MatchingType, ConnectionType, QualityFactor);

                // Trả về view có tên "Result" với dữ liệu tính toán
                return View("Result", impedance);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về view lỗi
                return View("Error", ex);
            }
        }
    }
}
