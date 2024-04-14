using System;

namespace DTTT2.Models
{
    public class IndexModel
    {
        public double[][]? ImpedanceResults { get; set; }
        public double Zin { get; set; } // Độ kháng đầu vào
        public double RL { get; set; } // Độ kháng tải
        public double Frequency { get; set; } // Tần số

        public string? MatchingType { get; set; } // Loại mạch
        public string? ConnectionType { get; set; } // Loại kết nối

        public double QualityFactor { get; set; } // Hệ số chất lượng

        public void CalculateImpedance()
        {
            double pi = Math.PI;
            double w = 2 * pi * Frequency * Math.Pow(10, 6);

            // Thực hiện tính toán độ kháng dựa trên các giá trị và loại mạch được cung cấp
            // (Đoạn mã tính toán đã được đưa vào trong phương thức này)
        }
    }
}
