using System;
using System.Collections.Generic;
using System.Text;

namespace PCT
{
    [Serializable]
    public class clsVertical
    {
        // D - Diâmetro - m
        // L - Comprimento da Tubulação - m
        // FLOWG - Vazão de Gás - kg/s
        // FLOWL - Vazão de Líquido - kg/s
        // RHOG - Densidade do Gás - kg/m³
        // RHOL - Desnidade do Líquido - kg/m³
        // VISCG - Viscosidade do Gás - N.s/m²
        // VISCL - Viscosidade do Líquido - N.s/m²
        // SIGMA - Tensão Interfacial - N/m
        // EPS - Rugosidade do Tubo - m
        // P - Pressão do Sistema - N/m²

        private string mNome;

        private double mD;
        private double mL;
        private double mFLOWG;
        private double mFLOWL;
        private double mRHOG;
        private double mRHOL;
        private double mVISCG;
        private double mVISCL;
        private double mSIGMA;
        private double mEPS;
        private double mP;

        private double mPC;

        public string Nome
        {
            get
            {
                return mNome;
            }
            set
            {
                mNome = value;
            }
        }

        public double D
        {
            get
            {
                return mD;
            }
            set
            {
                mD = value;
            }
        }
        public double L
        {
            get
            {
                return mL;
            }
            set
            {
                mL = value;
            }
        }
        public double FLOWG
        {
            get
            {
                return mFLOWG;
            }
            set
            {
                mFLOWG = value;
            }
        }
        public double FLOWL
        {
            set
            {
                mFLOWL = value;
            }
            get
            {
                return mFLOWL;
            }
        }
        public double RHOG
        {
            get
            {
                return mRHOG;
            }
            set
            {
                mRHOG = value;
            }
        }
        public double RHOL
        {
            set
            {
                mRHOL = value;
            }
            get
            {
                return mRHOL;
            }
        }
        public double VISCG
        {
            set
            {
                mVISCG = value;
            }
            get
            {
                return mVISCG;
            }
        }
        public double VISCL
        {
            set
            {
                mVISCL = value;
            }
            get
            {
                return mVISCL;
            }
        }
        public double SIGMA
        {
            get
            {
                return mSIGMA;
            }
            set
            {
                mSIGMA = value;
            }
        }
        public double EPS
        {
            set
            {
                mEPS = value;
            }
            get
            {
                return mEPS;
            }
        }
        public double P
        {
            get
            {
                return mP;
            }
            set
            {
                mP = value;
            }
        }

        public double PC
        {
            get
            {
                return mPC;
            }
            set
            {
                mPC = value;
            }
        }
    }
}
