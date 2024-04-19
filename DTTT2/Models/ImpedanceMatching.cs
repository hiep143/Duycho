using System;
using UnitsNet;
using System.Numerics;
using UnitsNet.Units;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace DTTT2.Models
{
    public static class ImpedanceMatching
    {
        public static double[][] CalculateImpedance(
            double ZinValue,
            double RLValue,
            double FrequencyValue,
            string MatchingType,
            string ConnectionType,
            double QualityFactor)

    

    // Tiếp tục với các tính toán cần thiết ở đây và trả về một mảng double[][] hoặc giá trị phù hợp.

        {
        
            ElectricResistance Zin = ElectricResistance.FromOhms(ZinValue);
            
            ElectricResistance RL = ElectricResistance.FromOhms(RLValue);
            Frequency Frequency = Frequency.FromGigahertz(FrequencyValue);
            double pi = Math.PI;
            double w = Frequency.Hertz*2*pi;
            

            if (MatchingType == "L")
            {
                if (ConnectionType == "DC feed")
                {
                    if (Zin > RL) // DC feed
                    {
                        Capacitance C = Capacitance.FromFarads(Math.Sqrt(Zin.Ohms/RL.Ohms-1)/(w*Zin.Ohms));
                        ElectricInductance L = ElectricInductance.FromHenries((RL.Ohms/(w))*Math.Sqrt(Zin.Ohms/RL.Ohms-1));

                        return new double[][] { new double[] { L.Nanohenries }, new double[] { C.Picofarads } };
                    }
                    else
                    {
                        double Qp = Math.Sqrt(RL.Value / Zin.Value - 1);
                        Capacitance C = Capacitance.FromFarads(Qp / (RL.Ohms * w));
                        Capacitance Cs = Capacitance.FromFarads((C.Farads * (Math.Pow(Qp, 2) + 1)) / Math.Pow(Qp, 2));
                        ElectricInductance L = ElectricInductance.FromHenries(1/(Cs.Farads*Math.Pow(w,2)));
                        return new double[][] { new double[] { L.Nanohenries }, new double[] { C.Picofarads } };
                    }
                }
                else // DC block
                {
                    if (Zin.Value > RL.Value)
                    {
                        Capacitance C = Capacitance.FromFarads(1/(w*RL.Ohms*Math.Sqrt(Zin.Ohms/RL.Ohms-1)));
                        ElectricInductance L = ElectricInductance.FromHenries(Zin.Ohms/(w*Math.Sqrt(Zin.Ohms/RL.Ohms-1)));
                        return new double[][] { new double[] { L.Nanohenries }, new double[] { C.Picofarads } };
                    }
                    else
                    {
                        double Qp = Math.Sqrt(RL.Ohms / Zin.Ohms - 1);
                        ElectricInductance L = ElectricInductance.FromHenries(RL.Ohms / (Qp * w * Math.PI));
                        ElectricInductance Ls = ElectricInductance.FromHenries(L.Henries*Math.Pow(Qp, 2)/(1+Math.Pow(Qp, 2)));
                        Capacitance C = Capacitance.FromFarads(1/(Ls.Henries*Math.Pow(w, 2)));
                        return new double[][] { new double[] { L.Nanohenries }, new double[] { C.Picofarads } };
                    }
                }
            }
            else if (MatchingType == "Pi")
            {
                if (Zin > RL)
                {
                
                    if (ConnectionType == "DC feed")
                    {
                       double Qp = Math.Sqrt(RL.Value / Zin.Value - 1);
                        Capacitance C = Capacitance.FromFarads(Qp / (RL.Value * Frequency.Hertz * 2 * Math.PI));
                        Capacitance Cs = Capacitance.FromFarads((C.Farads * (Math.Pow(Qp, 2) + 1)) / Math.Pow(Qp, 2));
                        ElectricInductance L = ElectricInductance.FromHenries((Zin.Ohms+1/(2*Math.PI*Frequency.Hertz*Cs.Farads))/(2*Math.PI*Frequency.Hertz));
                        return new double[][] { new double[] { L.Nanohenries }, new double[] { C.Picofarads } };
                    }
                    else // DC block
                    {

                    double Qp = Math.Sqrt(RL.Value / Zin.Value - 1);
                        Capacitance C = Capacitance.FromFarads(Qp / (RL.Value * Frequency.Hertz * 2 * Math.PI));
                        Capacitance Cs = Capacitance.FromFarads((C.Farads * (Math.Pow(Qp, 2) + 1)) / Math.Pow(Qp, 2));
                        ElectricInductance L = ElectricInductance.FromHenries((Zin.Ohms+1/(2*Math.PI*Frequency.Hertz*Cs.Farads))/(2*Math.PI*Frequency.Hertz));
                        return new double[][] { new double[] { L.Nanohenries }, new double[] { C.Picofarads } };
                }
                }
                else //Zin < RL
                {
                   

                    if (ConnectionType == "DC feed")
                    {
                       double Qp = Math.Sqrt(RL.Value / Zin.Value - 1);
                        Capacitance C = Capacitance.FromFarads(Qp / (RL.Value * Frequency.Hertz * 2 * Math.PI));
                        Capacitance Cs = Capacitance.FromFarads((C.Farads * (Math.Pow(Qp, 2) + 1)) / Math.Pow(Qp, 2));
                        ElectricInductance L = ElectricInductance.FromHenries((Zin.Ohms+1/(2*Math.PI*Frequency.Hertz*Cs.Farads))/(2*Math.PI*Frequency.Hertz));
                        return new double[][] { new double[] { L.Nanohenries }, new double[] { C.Picofarads } };
                    }
                    else // DC block
                    {

                        double Qp = Math.Sqrt(RL.Value / Zin.Value - 1);
                        Capacitance C = Capacitance.FromFarads(Qp / (RL.Value * Frequency.Hertz * 2 * Math.PI));
                        Capacitance Cs = Capacitance.FromFarads((C.Farads * (Math.Pow(Qp, 2) + 1)) / Math.Pow(Qp, 2));
                        ElectricInductance L = ElectricInductance.FromHenries((Zin.Ohms+1/(2*Math.PI*Frequency.Hertz*Cs.Farads))/(2*Math.PI*Frequency.Hertz));
                        return new double[][] { new double[] { L.Nanohenries }, new double[] { C.Picofarads } };
                    }
                }
            }
            else if (MatchingType == "T")
            {
                ElectricResistance Rv = ElectricResistance.FromOhms(Math.Min(Zin.Ohms, RL.Ohms)*(QualityFactor*QualityFactor+1));
                double Q1 = Math.Sqrt(Rv.Ohms/Zin.Ohms-1);
                double Q2 = Math.Sqrt(Rv.Ohms/RL.Ohms-1);
                double Xp1 = Rv.Ohms/Q1;
                double Xs1 = Zin.Ohms * Q1;
                double Xp2 = Rv.Ohms/Q2;
                double Xs2 = RL.Ohms * Q2;
                double Xs = (Xs1 * Xs2) /(Xs1 + Xs2);
                if (ConnectionType == "DC feed")
                {
                    ElectricInductance L1 = ElectricInductance.FromHenries(Q1*Zin.Ohms/(2*Math.PI*Frequency.Hertz));
                    ElectricInductance L2 = ElectricInductance.FromHenries(Xp2/(2*Math.PI*Frequency.Hertz));
                    Capacitance C = Capacitance.FromFarads(1/(Xs*2*Math.PI*Frequency.Hertz));
                    return new double[][] { new double[] { L1.Nanohenries, L2.Nanohenries }, new double[] { C.Picofarads } };

                }

            }



            // Invalid matching type or unknown case
            return new double[0][];
        }
    }
}