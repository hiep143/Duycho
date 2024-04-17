using System;

namespace DTTT2.Models
{
    public static class ImpedanceMatching
    {
        public static double[][] CalculateImpedance(double Zin, double RL, double Frequency, string MatchingType, string ConnectionType, double QualityFactor)
        {
          
            double pi = Math.PI;
            double w = 2 * pi * Frequency * Math.Pow(10, 6);

            if (MatchingType == "L")
            {
                if (ConnectionType == "DC feed")
                {
                    if (Zin > RL) // DC feed
                    {
                        double L = RL * Math.Sqrt(Zin / RL - 1) / w;
                        double C = Math.Sqrt(Zin / RL - 1) / (w * Zin);
                        return new double[][] { new double[] { L }, new double[] { C } };
                    }
                    else
                    {
                        double L = Zin * Math.Sqrt(RL / Zin - 1) / w;
                        double C = Math.Sqrt(RL / Zin - 1) / (w * RL);
                        return new double[][] { new double[] { L }, new double[] { C } };
                    }
                }
                else // DC block
                {
                    if (Zin > RL)
                    {
                        double L = Zin / (w * Math.Sqrt(Zin / RL - 1));
                        double C = 1 / (w * RL * Math.Sqrt(Zin / RL - 1));
                        return new double[][] { new double[] { L }, new double[] { C } };
                    }
                    else
                    {
                        double L = RL / (w * Math.Sqrt(RL / Zin - 1));
                        double C = 1 / (w * Zin * Math.Sqrt(RL / Zin - 1));
                        return new double[][] { new double[] { L }, new double[] { C } };
                    }
                }
            }
            else if (MatchingType == "Pi")
            {
                if (Zin > RL)
                {
                    double Q1 = QualityFactor;
                    double Rv = Zin / (QualityFactor * QualityFactor + 1);
                    double Xp1 = Zin / Q1;
                    double Xs1 = Rv * Q1;
                    double Q2 = Math.Sqrt(RL / Rv - 1);
                    double Xp2 = RL / Q2;
                    double Xs2 = Rv * Q2;
                    if (ConnectionType == "DC feed")
                    {
                        double C1 = 1 / (w * Xp1);
                        double C2 = 1 / (w * Xp2);
                        double L = (Xs1 + Xs2) / w;
                        return new double[][] { new double[] { C1, C2 }, new double[] { L, L } };
                    }
                    else // DC block
                    {

                    double L1 = Xp1 / w;
                    double L2 = Xp2 / w;
                    double C = 1 / (w * (Xp1 + Xp2));

                    return new double[][] { new double[] { L1, L2 }, new double[] { C, C } };
                }
                }
                else //Zin < RL
                {
                    //Tính từ tải đến trở ảo
                    double Q2 = QualityFactor;
                    double Rv = RL / (QualityFactor * QualityFactor + 1);
                    double Xp2 = RL / Q2;
                    double Xs2 = Rv * Q2;
                    //Tính từ trở ảo đến nguồn
                    double Q1 = Math.Sqrt(Zin / Rv - 1);
                    double Xp1 = Zin / Q1;
                    double Xs1 = Rv * Q1;

                    if (ConnectionType == "DC feed")
                    {
                        double C1 = 1 / (w * Xp1);
                        double C2 = 1 / (w * Xp2);
                        double L = (Xs1 + Xs2) / w;
                        return new double[][] { new double[] { C1, C2 }, new double[] { L, L } };
                    }
                    else // DC block
                    {

                        double L1 = Xp1 / w;
                        double L2 = Xp2 / w;
                        double C = 1 / (w * (Xp1 + Xp2));

                        return new double[][] { new double[] { L1, L2 }, new double[] { C, C } };
                    }
                }
            }
            else if (MatchingType == "T")
            {
                if (Zin < RL)
                {
                    //Tính từ nguồn đến trở ảo
                    double Q1 = QualityFactor;
                    double Rv = Zin * (Q1 * Q1 + 1);
                    double Xp1 = Rv / Q1;
                    double Xs1 = Zin * Q1;
                    //Tính từ trở ảo đến tải
                    double Q2 = Math.Sqrt(Rv / RL - 1);
                    double Xp2 = Rv / Q2;
                    double Xs2 = Q2 * RL;
                    double Xs = (Xs1 * Xs2) / (Xs1 + Xs2);
                    double L1 = Xp1 / w;
                    double L2 = Xp2 / w;
                    double C = 1 / (Xs * w);

                    return new double[][] { new double[] { L1, L2 }, new double[] { C, C } };
                }
                else //Zin>RL
                {
                    //Tính từ tải đến trở ảo
                    double Q2 = QualityFactor;
                    double Rv = RL * (Q2 * Q2 + 1);
                    double Xp2 = Rv / Q2;
                    double Xs2 = RL * Q2;
                    //Tính từ trở ảo đến nguồn
                    double Q1 = Math.Sqrt(Rv / Zin - 1);
                    double Xp1 = Rv / Q1;
                    double Xs1 = Q1 * Zin;

                    double Xs = (Xs1 * Xs2) / (Xs1 + Xs2);
                    double L1 = Xp1 / w;
                    double L2 = Xp2 / w;
                    double C = 1 / (Xs * w);

                    return new double[][] { new double[] { L1, L2 }, new double[] { C, C } };
                }
            }



            // Invalid matching type or unknown case
            return new double[0][];
        }
    }
}
