using System;

namespace DTTT2.Models
{
    public class IndexModel
    {
        public IndexModel(double zin, double rL, double frequency, string matchingType, string connectionType, double qualityFactor)
        {
            Zin = zin;
            RL = rL;
            Frequency = frequency;
            MatchingType = matchingType;
            ConnectionType = connectionType;
            QualityFactor = qualityFactor;
        }

        public double[][]? ImpedanceResults { get; set; }
        public double Zin { get; set; } // Độ kháng đầu vào
        public double RL { get; set; } // Độ kháng tải
        public double Frequency { get; set; } // Tần số

        public string MatchingType { get; set; } // Loại mạch
        public string ConnectionType { get; set; } // Loại kết nối

        public double QualityFactor { get; set; } // Hệ số chất lượng

        public void CalculateImpedance()
        {
            ImpedanceResults = ImpedanceMatching.CalculateImpedance(Zin, RL, Frequency, MatchingType, ConnectionType, QualityFactor);

            // Thực hiện tính toán độ kháng dựa trên các giá trị và loại mạch được cung cấp
            // (Đoạn mã tính toán đã được đưa vào trong phương thức này)
        }
    }
}
