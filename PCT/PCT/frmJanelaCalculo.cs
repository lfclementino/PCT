using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

namespace PCT
{
    public partial class frmJanelaCalculo : Form
    {
        private clsCalculo Calculo;

        public clsCalculo DataCalculo
        {
            get
            {
                return Calculo;
            }
            set
            {
                Calculo = value;
            }
        }

        private frmMain oMain;

        public enum Modo
        {
            Novo,
            Aberto,
            Salvo,
            Alterado
        }

        private Modo Status;

        public void SetStatus(Modo Stat)
        {
            Status = Stat;
        }

        public frmJanelaCalculo(frmMain iMain, clsCalculo iCalculo)
        {
            InitializeComponent();

            oMain = iMain;

            Calculo = iCalculo;

            AtualizaTabela();
        }

        private string mArquivo;

        public string Arquivo
        {
            get
            {
                return mArquivo;
            }
            set
            {
                mArquivo = value;
                if (Arquivo != null)
                {
                    FileInfo fi = new FileInfo(value);
                    this.Text = fi.Name;
                }
            }
        }

        private void AtualizaTabela()
        {
            double Total = 0;

            DataTable Tabela = new DataTable();

            DataColumn ColTrechosTipo = new DataColumn(" ");
            DataColumn ColTrechosNome = new DataColumn("Trechos");
            DataColumn ColTrechosPC = new DataColumn("Perda de Carga (N/m²)");
            Tabela.Columns.Add(ColTrechosTipo);

            Tabela.Columns.Add(ColTrechosNome);
            Tabela.Columns.Add(ColTrechosPC);

            for (int i = 0; i < Calculo.Trechos.Count; i++)
            {
                DataRow Row = Tabela.NewRow();

                if (Calculo.Trechos[i] is clsHorizontal)
                {
                    Row[0] = "H";
                    Row[1] = ((clsHorizontal)Calculo.Trechos[i]).Nome;
                    Row[2] = ((clsHorizontal)Calculo.Trechos[i]).PC.ToString();
                    Total += ((clsHorizontal)Calculo.Trechos[i]).PC;
                }
                else
                {
                    Row[0] = "V";
                    Row[1] = ((clsVertical)Calculo.Trechos[i]).Nome;
                    Row[2] = ((clsVertical)Calculo.Trechos[i]).PC.ToString();
                    Total += ((clsVertical)Calculo.Trechos[i]).PC;
                }

                Tabela.Rows.Add(Row);
            }

            gridTrechos.DataSource = Tabela;

            gridTrechos.Columns[0].Width = 20;
            gridTrechos.Columns[1].Width = 350;
            gridTrechos.Columns[2].Width = 260;

            //if (gridTrechos.Rows.Count > 0)
            //    gridTrechos.Rows[gridTrechos.Rows.Count - 1].Selected = true;

            StatusTotal.Text = "Total : " + Total.ToString() + " N/m²";

            BotaoRem();
        }

        private double PRDROP(double D, double U, double RHO, double VISC, double EPS, double L)
        {
            // Função calcula a perda de carga em uma tubulação contendo um flúido Newtoniano em Pa
            // D - Diâmetro interno da tubulação - m
            // U - Velocidade Superficial - m/s
            // RHO - Densidade - kg/m³
            // VISC - Viscosidade - N.s/m²
            // EPS - Rugosidade da tubulação - m
            // L - Comprimento da tubulação - m
            // FF - Fator de atrito de Fanning

            double RE;
            double FF;
            double RF;
            double T1;
            double T2;
            double TERM;
            double rPRDROP;

            RE = D * U * RHO / VISC;
            if (RE <= 2100)
            {
                FF = 16 / RE;
            }
            else
            {
                RF = EPS / D;
                if (RE <= 4000)
                {
                    T1 = RF / 3.7;
                    T2 = 5.02 / RE;
                    TERM = Math.Log10(T1 - T2 * Math.Log10(T1 + 13 / RE));
                    FF = 1 / Math.Pow((4 * Math.Log10(T1) + T2 * TERM), 2);
                }
                else
                {
                    FF = 1 / Math.Pow((3.6 * Math.Log10(RE / (0.135 * (RE * RF + 6.5)))), 2);
                }
            }
            rPRDROP = 2 * FF * RHO * Math.Pow(U, 2) * L / D;

            return rPRDROP;
        }

        private double IGL(double UL, double UG, double RHOL, double RHOG, double VISCL, double VISCG, double SIGMA)
        {
            // Estima o Regime de Escoamento para escoamento Gas-Líquido em Tubulação Horizontal
            // Método Baseado no Mapa de Fluxo Padrão de Mandhane, Gregory and Aziz et al (1974)

            // UL - Velocidade Superficial do Líquido - m/s
            // UG - Velocidade SUperficial do Gás - m/s
            // RHOL - Densidade do Líquido - kg/m³
            // RHOG - Densidade do Gás - kg/m³
            // VISCL - Viscosidade do Líquido - N.s/m²
            // VISCG - Viscosidade do Gás - N.s/m²
            // SIGMA - Tensão Superficial - N/m

            // Respostas possíveis:
            // 0 - Erro
            // 1 - Bolhas Dispersas ( Dispersed Bubbles )
            // 2 - Anular ( Annular Mist )
            // 3 - Pistonado ( Elongated Bubbles )
            // 4 - Estratificado liso ( Stratified )
            // 5 - Slug
            // 6 - Estratificado Ondulado ( Wave )

            double X;
            double X1;
            double Y1;
            double TERM;
            double rIGL = 0;
            double TERM1;
            double TERM2;
            double TERM3;
            double TERM4;

            X = Math.Pow((RHOL * 7.24 * Math.Pow(10, -5) / SIGMA), 0.25);
            X1 = Math.Pow((RHOG / 1.2936), 0.333) * X * Math.Pow((VISCG / (1.8 * Math.Pow(10, -5))), 0.2);
            Y1 = X * Math.Pow(VISCL / Math.Pow(10, -3), 0.2);
            TERM = 70.104 * Math.Pow((UL / 4.2672), 0.206) * X1;
            if (UL >= (4.2672 * Y1))
            {
                if (UG <= TERM)
                {
                    rIGL = 1;
                }
                else
                {
                    rIGL = 2;
                }
            }
            else
            {
                TERM1 = 0.762 * Math.Pow((UL / 1.46304), 0.248);
                if (UL <= 0.03048)
                {
                    TERM1 = 4.2672 * Math.Pow((UL / 0.03048), -0.368);
                }
                else
                {
                    if (UL <= 0.06096)
                    {
                        TERM1 = 4.2672 * Math.Pow((UL / 0.03048), -0.415);
                    }
                    else
                    {
                        if (UL <= 0.3505)
                        {
                            TERM1 = 3.2004 * Math.Pow((UL / 0.06096), -0.816);
                        }
                        else
                        {
                            if (UL <= 1.46304)
                            {
                                TERM1 = 0.762;
                            }
                        }
                    }
                }
                TERM2 = 30.48 * Math.Pow((UL / 0.762), 0.463);
                if (UL <= 0.03048)
                {
                    TERM2 = 21.336 * Math.Pow((UL / 0.03048), -.0675);
                }
                else
                {
                    if (UL <= 9.144 * Math.Pow(10, -2))
                    {
                        TERM2 = 18.288 * Math.Pow(UL / 0.3048, -0.415);
                    }
                    else
                    {
                        if (UL <= 0.17069)
                        {
                            TERM2 = 11.5824 * Math.Pow(UL / (9.144 * Math.Pow(10, -2)), 0.0813);
                        }
                        else
                        {
                            if (UL <= 0.3048)
                            {
                                TERM2 = 12.192 * Math.Pow(UL / 0.17069, 0.385);
                            }
                            else
                            {
                                if (UL <= 0.762)
                                {
                                    TERM2 = 15.24 * Math.Pow(UL / 0.3048, 0.756);
                                }
                            }
                        }
                    }
                }
                TERM3 = 0.3 * Y1;
                TERM4 = 0.5 / Y1;
                TERM1 = TERM1 * X1;
                TERM2 = TERM2 * X1;

                if ((UG > TERM1) || (UL < TERM4))
                {
                    if ((UG > TERM1) || (UL >= TERM4))
                    {
                        if ((UG < TERM1) || (UG > TERM2) || (UL <= TERM3))
                        {
                            if ((UG <= TERM1) && (UG <= TERM2) && (UL <= TERM3))
                            {
                                rIGL = 6;
                            }
                        }
                        else
                        {
                            rIGL = 5;
                        }
                    }
                    else
                    {
                        rIGL = 4;
                    }
                }
                else
                {
                    rIGL = 3;
                }
            }

            return rIGL;
        }

        private double G = 9.81;

        private double ArrumaSinal(double N1, double N2)
        {
            if ((N2 < 0) && (N1 < 0))
                return N1;
            else if ((N2 < 0) && (N1 >= 0))
                return -1 * N1;
            else if ((N2 >= 0) && (N1 >= 0))
                return N1;
            else
                return -1 * N1;
        }

        private double DPGLH(clsHorizontal TrechoH)
        {
            int ItMax = 50;
            int Iter;

            double Area;
            double USL;
            double USG;
            double GT;
            double LAMBDA;
            double FR;
            double RL;
            double RE;
            double Delta;
            double Beta;
            double F;
            double DBDT;
            double FP;
            double DRL;
            double VISCTP;
            double RHOTP;
            double UTP;
            double rDPGLH = 0;
            double IFR;
            double XMG;
            double C;
            double TERM;
            double UT;
            double REL;
            double REG;
            double DELPL;
            double DELPG;
            double X;

            Area = (Math.PI * Math.Pow(TrechoH.D, 2)) / 4;
            USL = TrechoH.FLOWL / (Area * TrechoH.RHOL);
            USG = TrechoH.FLOWG / (Area * TrechoH.RHOG);
            GT = (TrechoH.FLOWL + TrechoH.FLOWG) / Area;

            if (GT <= 2700)
            {
                // Identificando o Padrão de Escoamento

                IFR = IGL(USL, USG, TrechoH.RHOL, TrechoH.RHOG, TrechoH.VISCL, TrechoH.VISCG, TrechoH.SIGMA);

                if ((IFR != 4) && (IFR != 6))
                {
                    // Usando Correlação de Lockhart-Martinelli

                    REL = TrechoH.D * USL * TrechoH.RHOL / TrechoH.VISCL;
                    REG = TrechoH.D * USG * TrechoH.RHOG / TrechoH.VISCG;

                    if ((REL <= 1000) && (REG <= 1000))
                        C = 5;
                    else if ((REL > 1000) && (REG <= 1000))
                        C = 10;
                    else if ((REL <= 1000) && (REG > 1000))
                        C = 10;
                    else
                        C = 20;
                    DELPL = PRDROP(TrechoH.D, USL, TrechoH.RHOL, TrechoH.VISCL, TrechoH.EPS, TrechoH.L);
                    DELPG = PRDROP(TrechoH.D, USG, TrechoH.RHOG, TrechoH.VISCG, TrechoH.EPS, TrechoH.L);
                    X = Math.Sqrt(DELPL / DELPG);
                    rDPGLH = DELPL * (1 + C / X + 1 / Math.Pow(X, 2));
                }
                else
                {
                    // Usando Correlação de Hoogendoorn para escoamento Ondulado ou Estratificado

                    XMG = TrechoH.FLOWG / (TrechoH.FLOWL + TrechoH.FLOWG);
                    C = 9.5 * Math.Pow(XMG, 0.5) - 62.6 * Math.Pow(XMG, 1.3);
                    TERM = (1 + 230 * Math.Pow(XMG, 0.84)) * Math.Pow(1.38 * Math.Pow(10, -3) * TrechoH.RHOL / TrechoH.RHOG, C);
                    UT = (TrechoH.FLOWG + TrechoH.FLOWL) / (Area * TrechoH.RHOL);
                    rDPGLH = PRDROP(TrechoH.D, UT, TrechoH.RHOL, TrechoH.VISCL, TrechoH.EPS, TrechoH.L);
                }
            }
            else
            {
                // Cálculo do Holdup usando Correlação de Hughmark

                LAMBDA = USL / (USL + USG);
                FR = Math.Pow(USG + USL, 2) / (G * TrechoH.D);
                Iter = 0;
                RL = 1;
                while (Iter <= ItMax)
                {
                    RE = TrechoH.D * GT / (RL * TrechoH.VISCL + (1 - RL) * TrechoH.VISCG);
                    Delta = Math.Pow(RE, 1 / 6) * Math.Pow(FR, 0.125) / Math.Pow(LAMBDA, 0.25);
                    if (Delta <= 10)
                    {
                        Beta = -0.16367 + Delta * (0.31037 + Delta * (-0.03525 + Delta * 0.001366));
                    }
                    else
                    {
                        Beta = 0.75545 + Delta * (0.003585 - Delta * 0.1463 * Math.Pow(10, -4));
                    }
                    F = RL - 1 + Beta * (1 - LAMBDA);
                    if (Math.Abs(F) <= Math.Pow(10, -5))
                    {
                        break;
                    }
                    else
                    {
                        if (Delta <= 10)
                        {
                            DBDT = 0.31037 + Delta * (-0.0705 + Delta * 0.004098);
                        }
                        else
                        {
                            DBDT = 0.003585 - 0.2872 * Math.Pow(10, -4) * Delta;
                        }
                        FP = 1 * ((-Delta / 6) * (TrechoH.VISCL - TrechoH.VISCG) / (RL * TrechoH.VISCL - (1 - RL) * TrechoH.VISCG) * DBDT) * (1 - LAMBDA);
                        DRL = -F / FP;
                        if (Math.Abs(DRL) > (TrechoH.RF * RL))
                        {
                            DRL = ArrumaSinal((TrechoH.RF * RL), DRL);
                        }
                        RL = RL + DRL;
                    }
                    Iter = Iter + 1;
                }

                VISCTP = TrechoH.VISCL * Math.Exp(2.5 / (1 - 39 * (1 - RL) / 64));
                RHOTP = LAMBDA * TrechoH.RHOL + (1 - LAMBDA) * TrechoH.RHOG;
                UTP = GT / RHOTP;
                rDPGLH = PRDROP(TrechoH.D, UTP, RHOTP, VISCTP, TrechoH.EPS, TrechoH.L);
            }

            return Math.Round(rDPGLH, 4, MidpointRounding.AwayFromZero);
        }

        private double DPGLV(clsVertical TrechoV)
        {
            double NLB;
            double NLS;
            double NLM;
            double NGU;
            double ND;
            double NL;
            double NLU;
            double NSEC;
            double NHOLD;
            double LAMBDA;
            double P;
            double AREA;
            double USG;
            double USL;
            double UNS;
            double T1;
            double IFLOW;
            double RL;
            double RE;
            double T2;
            double T3;
            double FF;
            double DELP = 0;
            double SQD;
            double AN1;
            double AN2;
            double UR;
            double GAMA;
            double CGAMA;
            double GMAX;
            double DELPS = 0;
            double ANW;
            double TERM;
            double DELPM;
            double ALFA;
            double RHONS;
            double ACCN;
            double ALNSEC;
            double PSI;
            double ALNL;
            double CN;
            double ALNH;
            double VISCNS;
            double EPSD;

            P = 1000 * TrechoV.P;
            AREA = Math.PI * Math.Pow(TrechoV.D, 2) / 4;
            USG = TrechoV.FLOWG / (AREA * TrechoV.RHOG);
            USL = TrechoV.FLOWL / (AREA * TrechoV.RHOL);
            UNS = USL + USG;
            LAMBDA = USL / UNS;
            T1 = TrechoV.EPS / (3.7 * TrechoV.D);

            if (TrechoV.D < 0.089)
            {
                // Correlação de Hagedorn e Brown
                // Cálculo do HoldUp

                ND = 3.134 * TrechoV.D * Math.Sqrt(TrechoV.RHOL / TrechoV.SIGMA);
                NL = 1.7696 * TrechoV.VISCL / Math.Pow((TrechoV.RHOL * Math.Pow(TrechoV.SIGMA, 3)), 0.25);
                NLU = 0.5652 * USL * Math.Pow((TrechoV.RHOL / TrechoV.SIGMA), 0.25);
                NGU = 0.5652 * USG * Math.Pow((TrechoV.RHOL / TrechoV.SIGMA), 0.25);
                NSEC = NGU * Math.Pow(NL, 0.38) / Math.Pow(ND, 2.14);
                ALNSEC = Math.Log(NSEC);
                PSI = 1 + Math.Exp(6.6598 + ALNSEC * (8.8173 + ALNSEC * (3.7693 + ALNSEC * 0.5359)));

                if (NSEC < 0.01)
                    PSI = 1;
                else if (NSEC > 0.09)
                    PSI = 1.82;

                ALNL = Math.Log(NL);
                CN = Math.Exp(-4.895 - ALNL * (1.0775 + ALNL * (0.80822 + ALNL * (0.1597 + ALNL * 0.01019))));

                if (NL < 0.002)
                    CN = 0.00195;
                else if (NL > 0.4)
                    CN = 0.0115;

                NHOLD = (NLU / Math.Pow(NGU, 0.575)) * Math.Pow((P / (101.3 * Math.Pow(10, 3))), 0.1) * CN * (Math.Pow(10, 6) / ND);
                ALNH = Math.Log(NHOLD);
                RL = PSI * Math.Exp(-3.6372 + ALNH * (0.8813 + ALNH * (-0.1335 + ALNH * (0.018534 - ALNH * 0.001066))));

                if (NHOLD > 4000)
                    RL = PSI;
                else if (NHOLD < 0.1)
                    RL = 0.02633;

                if (RL < LAMBDA)
                    RL = LAMBDA;

                ACCN = (TrechoV.FLOWL + TrechoV.FLOWG) * USG / (AREA * P);
                VISCNS = LAMBDA * TrechoV.VISCL + (1 - LAMBDA) * TrechoV.VISCG;
                RE = (TrechoV.FLOWL + TrechoV.FLOWG) * TrechoV.D / (AREA * VISCNS);
                T2 = 5.02 / RE;
                T3 = Math.Log10(T1 - T2 * Math.Log10(T1 + 13 / RE));
                FF = FRICTF(T1, T2, T3);
                DELP = (2 * FF * (TrechoV.RHOL * Math.Pow(LAMBDA, 2) / RL + TrechoV.RHOG * Math.Pow((1 - LAMBDA), 2) / (1 - RL)) * Math.Pow(UNS, 2) / TrechoV.D + 9.81 * (RL * TrechoV.RHOL + (1 - RL) * TrechoV.RHOG)) * TrechoV.L / (1 - ACCN);
            }
            else
            {
                // Correlação de Orkiszewski

                NLB = 1.071 - 0.7277 * Math.Pow(UNS, 2) / TrechoV.D;

                if (NLB < 0.13)
                    NLB = 0.13;

                NLS = 50 + 20.417 * USL * Math.Pow((TrechoV.RHOL / TrechoV.SIGMA), 0.25);
                NLM = 75 + 54.77 * Math.Pow((USL * Math.Pow((TrechoV.RHOL / TrechoV.SIGMA), 0.25)), 0.75);
                NGU = 0.5652 * USG * Math.Pow((TrechoV.RHOL / TrechoV.SIGMA), 0.25);

                // Identificando Padrão de Escoamento
                // 1 - Bolhas (Bubble Flow)
                // 2 - Slug (Slug Flow)
                // 3 - Mist (Mist Flow)
                // 4 - Transição de Slug para Mist

                if (USG / UNS < NLB)
                    IFLOW = 1;
                else if (USG / UNS > NLB && NGU <= NLS)
                    IFLOW = 2;
                else if (NLM >= NGU && NGU > NLS)
                    IFLOW = 3;
                else
                    IFLOW = 4;

                if (IFLOW == 1)
                {
                    // Bubble Flow

                    RL = 0.5 - 2.0505 * UNS + Math.Sqrt(Math.Pow((0.5 + 2.0505 * UNS), 2) - 4.1011 * USG);
                    RE = TrechoV.D * USL * TrechoV.RHOL / (TrechoV.VISCL * RL);
                    T2 = 5.02 / RE;
                    T3 = Math.Log10(T1 - T2 * Math.Log10(T1 + 13 / RE));
                    FF = FRICTF(T1, T2, T3);

                    DELP = 2 * FF * Math.Pow((USL / RL), 2) * TrechoV.RHOL * TrechoV.L / TrechoV.D;
                }
                if (IFLOW == 2 || IFLOW == 3)
                {
                    // Slug Flow

                    SQD = Math.Sqrt(TrechoV.D);
                    AN1 = 0.572 * Math.Pow(10, 5) * (-0.35 + Math.Sqrt(0.1225 + 0.08932 * UNS / SQD));
                    AN2 = 0.572 * Math.Pow(10, 5) * (-0.546 + Math.Sqrt(0.2981 + 0.03349 * UNS / SQD));
                    RE = TrechoV.D * UNS * TrechoV.RHOL / TrechoV.VISCL;
                    UR = 0;

                    if (RE > AN1)
                        UR = (3.5955 + 9.0294 * Math.Pow(10, -5) * RE) * SQD;
                    else if (RE < AN2)
                        UR = (1.7098 + 9.0294 * Math.Pow(10, -5) * RE) * SQD;

                    if (UR <= 0)
                    {
                        GAMA = (2.5775 + 8.9805 * Math.Pow(10, -5) * RE) * SQD;
                        UR = 0.1524 * (GAMA + Math.Sqrt(8.3391 * Math.Pow(GAMA, 2) + 1.2012 * Math.Pow(10, 5) * TrechoV.VISCL / (TrechoV.RHOL * SQD)));
                    }

                    if (UNS <= 3.048)
                    {
                        CGAMA = 2.3642 * Math.Pow(10, -3) * Math.Log10(Math.Pow(10, 3) * TrechoV.VISCL + 1) / Math.Pow(TrechoV.D, 1.415) - 0.4285 + 0.167 * Math.Log10(UNS) + 0.113 * Math.Log10(TrechoV.D);

                        if (CGAMA < (-0.2133 * UNS))
                        {
                            CGAMA = -0.2133 * UNS;
                        }
                    }
                    else
                    {
                        CGAMA = 5.3745 * Math.Pow(10, -3) * Math.Log10(Math.Pow(10, 3) * TrechoV.VISCL + 1) / Math.Pow(TrechoV.D, 1.371) - 0.1326 + 0.569 * Math.Log10(TrechoV.D) - Math.Log10(UNS / 0.3048) * (1.5466 * Math.Pow(10, -3) * Math.Log10(Math.Pow(10, 3) * TrechoV.VISCL + 1) / Math.Pow(TrechoV.D, 1.571) + 0.7221 - 0.63 * Math.Log10(TrechoV.D));
                        GMAX = UR * (0.06243 * (TrechoV.FLOWG + TrechoV.FLOWL) / AREA - UNS) / ((UR + UNS) * (UR + UNS + 0.3048));

                        if (CGAMA < GMAX)
                            CGAMA = GMAX;
                    }

                    T2 = 5.02 / RE;
                    T3 = Math.Log10(T1 - T2 * Math.Log10(T1 + 13 / RE));
                    FF = FRICTF(T1, T2, T3);
                    DELP = 2 * FF * Math.Pow(UNS, 2) * TrechoV.RHOL * TrechoV.L / TrechoV.D * (USL + UR) / (UNS + UR + CGAMA);

                    if (IFLOW != 2)
                    {
                        DELPS = DELP;
                        IFLOW = 4;
                    }
                }
                if (IFLOW == 4)
                {
                    // Mist FLow

                    RE = TrechoV.D * USG * TrechoV.RHOG / TrechoV.VISCG;

                    // Correção para a Rugosidade

                    ANW = Math.Pow((USG * TrechoV.VISCL / TrechoV.SIGMA), 2) * (TrechoV.RHOG / TrechoV.RHOL);
                    TERM = TrechoV.SIGMA / (TrechoV.RHOG * Math.Pow(USG, 2) * TrechoV.D);

                    if (ANW > 0.005)
                        EPSD = 79247.3 * TERM * Math.Pow(ANW, 0.302);
                    else
                        EPSD = 34 * TERM;

                    T1 = EPSD / 3.7;
                    T2 = 5.02 / RE;
                    T3 = Math.Log10(T1 - T2 * Math.Log10(T1 + 13 / RE));
                    FF = FRICTF(T1, T2, T3);
                    DELP = 2 * FF * TrechoV.RHOG * Math.Pow(USG, 2) * TrechoV.L / TrechoV.D;

                    if (IFLOW != 4)
                    {
                        DELPM = DELP;

                        // Transiçãao de Slug para Mist Flow

                        ALFA = (NLM - NGU) / (NLM - NLS);
                        DELP = ALFA * DELPS + (1 - ALFA) * DELPM;
                    }
                }

                RHONS = LAMBDA * TrechoV.RHOL + (1 - LAMBDA) * TrechoV.RHOG;
                ACCN = (TrechoV.FLOWG + TrechoV.FLOWL) * USG / (AREA * P);
                DELP = (DELP + RHONS * TrechoV.L * 9.81) / (1 - ACCN);
            }

            return Math.Round(DELP, 4, MidpointRounding.AwayFromZero);
        }

        private double FRICTF(double T1, double T2, double T3)
        {
            return 1 / Math.Pow((4 * Math.Log10(T1) + T2 * T3), 2);
        }

        private void TituloAlterado()
        {
            if (this.Text.Substring(this.Text.Length - 1) != "*")
            {
                FileInfo fi = new FileInfo(Arquivo);
                this.Text = fi.Name + "*";
            }
        }

        private void EditarTrecho(int Index)
        {
            if (Calculo.Trechos[Index] is clsHorizontal)
            {
                frmTrecho EditarTrecho = new frmTrecho(frmTrecho.TipoJanela.Editar, frmTrecho.TipoDeTrecho.Horizontal, Calculo.Trechos[Index]);
                DialogResult DR = EditarTrecho.ShowDialog();
                if (DR == DialogResult.OK)
                {
                    ((clsHorizontal)Calculo.Trechos[Index]).PC = DPGLH((clsHorizontal)Calculo.Trechos[Index]);

                    AtualizaTabela();

                    gridTrechos.Rows[Index].Selected = true;

                    if (Status != Modo.Novo)
                    {
                        SetStatus(Modo.Alterado);

                        TituloAlterado();
                    }
                }
            }
            else
            {
                frmTrecho EditarTrecho = new frmTrecho(frmTrecho.TipoJanela.Editar, frmTrecho.TipoDeTrecho.Vertical, Calculo.Trechos[Index]);
                DialogResult DR = EditarTrecho.ShowDialog();
                if (DR == DialogResult.OK)
                {
                    ((clsVertical)Calculo.Trechos[Index]).PC = DPGLV((clsVertical)Calculo.Trechos[Index]);

                    AtualizaTabela();

                    gridTrechos.Rows[Index].Selected = true;

                    if (Status != Modo.Novo)
                    {
                        SetStatus(Modo.Alterado);

                        TituloAlterado();
                    }
                }
            }
        }

        private void gridTrechos_DoubleClick(object sender, EventArgs e)
        {
            if (gridTrechos.SelectedRows.Count > 0)
            {
                EditarTrecho(gridTrechos.CurrentRow.Index);
            }
        }

        private void frmJanelaCalculo_Activated(object sender, EventArgs e)
        {
            oMain.JanelaAtiva(this);
        }

        private void frmJanelaCalculo_Deactivate(object sender, EventArgs e)
        {
            oMain.JanelaAtiva(null);
        }

        private void frmJanelaCalculo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Status == Modo.Novo)
            {
                DialogResult DR;
                DR = MessageBox.Show("O arquivo ainda foi salvo. Deseja salvar agora ?", "PCT", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (DR == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (DR == DialogResult.Yes)
                {
                    oMain.SalvarXML(frmMain.TipoSalvar.SalvarComo);
                    if (Arquivo == null)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        Status = Modo.Salvo;
                    }
                }


            }
            else if (Status == Modo.Alterado)
            {
                DialogResult DR;
                DR = MessageBox.Show("O arquivo '" + Arquivo + "' foi alterado. Deseja salvar as alterações ?", "PCT", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (DR == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (DR == DialogResult.Yes)
                {
                    oMain.SalvarXML(frmMain.TipoSalvar.Salvar);

                    Status = Modo.Salvo;
                }
            }
        }

        private void BotaoRem()
        {
            if (gridTrechos.SelectedRows.Count > 0)
            {
                btToolRem.Enabled = true;
                btToolCopy.Enabled = true;
            }
            else
            {
                btToolRem.Enabled = false;
                btToolCopy.Enabled = false;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.ctxtAdd.Show(this, this.PointToClient(Cursor.Position));
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsHorizontal TrechoHorizontal = new clsHorizontal();
            frmTrecho NovoTrecho = new frmTrecho(frmTrecho.TipoJanela.Novo, frmTrecho.TipoDeTrecho.Horizontal, TrechoHorizontal);
            DialogResult DR = NovoTrecho.ShowDialog();
            if (DR == DialogResult.OK)
            {
                TrechoHorizontal.PC = DPGLH(TrechoHorizontal);

                Calculo.Trechos.Add(TrechoHorizontal);

                if (Status != Modo.Novo)
                {
                    SetStatus(Modo.Alterado);

                    TituloAlterado();
                }

                AtualizaTabela();
            }
            else
            {

            }
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsVertical TrechoVertical = new clsVertical();
            frmTrecho NovoTrecho = new frmTrecho(frmTrecho.TipoJanela.Novo, frmTrecho.TipoDeTrecho.Vertical, TrechoVertical);
            DialogResult DR = NovoTrecho.ShowDialog();
            if (DR == DialogResult.OK)
            {
                TrechoVertical.PC = DPGLV(TrechoVertical);

                Calculo.Trechos.Add(TrechoVertical);

                if (Status != Modo.Novo)
                {
                    SetStatus(Modo.Alterado);

                    TituloAlterado();
                }

                AtualizaTabela();
            }
            else
            {

            }
        }

        private void btToolRem_Click(object sender, EventArgs e)
        {
            Calculo.Trechos.RemoveAt(gridTrechos.CurrentRow.Index);

            if (Status != Modo.Novo)
            {
                SetStatus(Modo.Alterado);

                TituloAlterado();
            }

            AtualizaTabela();
        }

        private void btToolCopy_Click(object sender, EventArgs e)
        {
            object Item = new object();
            Item = Calculo.Trechos[gridTrechos.CurrentRow.Index];

            if (Item is clsHorizontal)
            {
                clsHorizontal CopyH = new clsHorizontal();

                CopyH.Nome = ((clsHorizontal)Item).Nome;
                CopyH.D = ((clsHorizontal)Item).D;
                CopyH.L = ((clsHorizontal)Item).L;
                CopyH.FLOWG = ((clsHorizontal)Item).FLOWG;
                CopyH.FLOWL = ((clsHorizontal)Item).FLOWL;
                CopyH.RHOG = ((clsHorizontal)Item).RHOG;
                CopyH.RHOL = ((clsHorizontal)Item).RHOL;
                CopyH.VISCG = ((clsHorizontal)Item).VISCG;
                CopyH.VISCL = ((clsHorizontal)Item).VISCL;
                CopyH.SIGMA = ((clsHorizontal)Item).SIGMA;
                CopyH.RF = ((clsHorizontal)Item).RF;
                CopyH.PC = ((clsHorizontal)Item).PC;

                CopyH.EPS = ((clsVertical)Item).EPS;

                Calculo.Trechos.Add(CopyH);
            }
            else
            {
                clsVertical CopyV = new clsVertical();

                CopyV.Nome = ((clsVertical)Item).Nome;
                CopyV.D = ((clsVertical)Item).D;
                CopyV.L = ((clsVertical)Item).L;
                CopyV.FLOWG = ((clsVertical)Item).FLOWG;
                CopyV.FLOWL = ((clsVertical)Item).FLOWL;
                CopyV.RHOG = ((clsVertical)Item).RHOG;
                CopyV.RHOL = ((clsVertical)Item).RHOL;
                CopyV.VISCG = ((clsVertical)Item).VISCG;
                CopyV.VISCL = ((clsVertical)Item).VISCL;
                CopyV.SIGMA = ((clsVertical)Item).SIGMA;
                CopyV.EPS = ((clsVertical)Item).EPS;
                CopyV.P = ((clsVertical)Item).P;
                CopyV.PC = ((clsVertical)Item).PC;

                Calculo.Trechos.Add(CopyV);
            }



            if (Status != Modo.Novo)
            {
                SetStatus(Modo.Alterado);

                TituloAlterado();
            }

            AtualizaTabela();
        }

        private void gridTrechos_SelectionChanged(object sender, EventArgs e)
        {
            BotaoRem();
        }

    }
}
