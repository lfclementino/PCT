using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PCT
{
    public partial class frmTrecho : Form
    {
        public enum TipoDeTrecho
        {
            Horizontal,
            Vertical
        }
        public enum TipoJanela
        {
            Novo,
            Editar
        }

        private TipoDeTrecho TipoHV;
        private clsHorizontal oTrechoH;
        private clsVertical oTrechoV;

        public clsHorizontal TrechoH
        {
            get
            {
                return oTrechoH;
            }
            set
            {
                oTrechoH = value;
            }
        }
        public clsVertical TrechoV
        {
            get
            {
                return oTrechoV;
            }
            set
            {
                oTrechoV = value;
            }
        }

        public frmTrecho(TipoJanela TipoJanela, TipoDeTrecho TipoTrecho, object oTrecho)
        {
            InitializeComponent();

            TipoHV = TipoTrecho;
            ArrumaBoxes(TipoTrecho);

            bool Editando = false;
            if (TipoJanela == TipoJanela.Editar)
            {
                Editando = true;
                this.Text = "Editando Trecho";
            }
            else
            {
                Nome = "NovoTrecho" + TipoTrecho.ToString();
                this.Text = "Adicionar Trecho " + TipoTrecho.ToString();
            }

            switch (TipoTrecho)
            {
                case TipoDeTrecho.Horizontal:
                    TrechoH = (clsHorizontal)oTrecho;

                    if (Editando)
                    {
                        Nome = TrechoH.Nome;
                        D = TrechoH.D;
                        L = TrechoH.L;
                        FLOWG = TrechoH.FLOWG;
                        FLOWL = TrechoH.FLOWL;
                        RHOG = TrechoH.RHOG;
                        RHOL = TrechoH.RHOL;
                        VISCG = TrechoH.VISCG;
                        VISCL = TrechoH.VISCL;
                        SIGMA = TrechoH.SIGMA;
                        RF = TrechoH.RF;
                        EPS = TrechoH.EPS;
                        ArrumaDecimais();
                    }

                    break;
                case TipoDeTrecho.Vertical:
                    TrechoV = (clsVertical)oTrecho;

                    if (Editando)
                    {
                        Nome = TrechoV.Nome;
                        D = TrechoV.D;
                        L = TrechoV.L;
                        FLOWG = TrechoV.FLOWG;
                        FLOWL = TrechoV.FLOWL;
                        RHOG = TrechoV.RHOG;
                        RHOL = TrechoV.RHOL;
                        VISCG = TrechoV.VISCG;
                        VISCL = TrechoV.VISCL;
                        SIGMA = TrechoV.SIGMA;
                        EPS = TrechoV.EPS;
                        P = TrechoV.P;

                        ArrumaDecimais();
                    }
                    break;
            }

            ArrumaNUD();

            txtNome.LostFocus += new EventHandler(txtNome_LostFocus);

            txtNome.KeyDown += new KeyEventHandler(frmTrecho_KeyDown);
            txtNome.Enter += new EventHandler(txtNome_Enter);
            VerificaCampos();
        }

        void txtNome_Enter(object sender, EventArgs e)
        {
            txtNome.SelectAll();
        }

        void txtNome_LostFocus(object sender, EventArgs e)
        {
            VerificaCampos();
        }

        private void VerificaCampos()
        {
            bool Valor = true;

            foreach (Control Controle in gbTrecho.Controls)
            {
                if (Controle is NumericUpDown)
                {
                    if (Controle.Name != "numEPS")
                    {
                        if (((NumericUpDown)Controle).Value == 0 && ((NumericUpDown)Controle).Visible == true)
                        {
                            Valor = false;
                        }
                    }
                }
            }

            if (String.IsNullOrEmpty(txtNome.Text))
            {
                Valor = false;
            }

            btOK.Enabled = Valor;
        }

        private void ArrumaDecimais()
        {
            foreach (Control Controle in gbTrecho.Controls)
            {
                if (Controle is NumericUpDown)
                {
                    int Decimais = NumberDecimalPlaces(((NumericUpDown)Controle).Value);

                    ((NumericUpDown)Controle).DecimalPlaces = Decimais;
                    ((NumericUpDown)Controle).Increment = Convert.ToDecimal(Math.Pow(10, -Decimais));
                }
            }
        }

        private void ArrumaNUD()
        {
            foreach (Control Controle in gbTrecho.Controls)
            {
                if (Controle is NumericUpDown)
                {
                    ((NumericUpDown)Controle).Enter += new EventHandler(frmTrecho_GotFocus);
                    ((NumericUpDown)Controle).LostFocus += new EventHandler(frmTrecho_LostFocus);
                    ((NumericUpDown)Controle).KeyDown += new KeyEventHandler(frmTrecho_KeyDown);
                }
            }
        }

        void frmTrecho_GotFocus(object sender, EventArgs e)
        {
            ((NumericUpDown)sender).Select(0, ((NumericUpDown)sender).Value.ToString().Length);
        }

        void frmTrecho_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        void frmTrecho_LostFocus(object sender, EventArgs e)
        {
            int decimalcases = NumberDecimalPlaces(((NumericUpDown)sender).Value);

            if (NumberDecimalPlaces(((NumericUpDown)sender).Value) != ((NumericUpDown)sender).DecimalPlaces)
            {
                ((NumericUpDown)sender).DecimalPlaces = decimalcases;
                ((NumericUpDown)sender).Increment = Convert.ToDecimal(Math.Pow(10, -decimalcases));
            }

            VerificaCampos();
        }

        private int NumberDecimalPlaces(decimal dec)
        {
            string testdec = Convert.ToString(dec);
            int s = (testdec.IndexOf(",")); // the first numbers plus decimal point
            if (s == -1)
            {
                return 0;
            }
            else
            {
                return ((testdec.Length) - s - 1);     //total length minus beginning numbers and decimal = number of decimal points
            }
        }

        private void ArrumaBoxes(TipoDeTrecho Tipo)
        {
            switch (Tipo)
            {
                case TipoDeTrecho.Horizontal:
                    lbP.Visible = false;
                    lbuP.Visible = false;
                    numP.Visible = false;
                    lbEPS.Visible = true;
                    lbuEPS.Visible = true;
                    numEPS.Visible = true;

                    lbRF.Visible = true;
                    numRF.Visible = true;

                    break;
                case TipoDeTrecho.Vertical:
                    lbP.Visible = true;
                    lbuP.Visible = true;
                    numP.Visible = true;
                    lbEPS.Visible = true;
                    lbuEPS.Visible = true;
                    numEPS.Visible = true;

                    lbRF.Visible = false;
                    numRF.Visible = false;

                    break;
            }
        }

        public double D
        {
            get
            {
                return Convert.ToDouble(numD.Value);
            }
            set
            {
                numD.Value = Convert.ToDecimal(value);
            }
        }
        public double L
        {
            get
            {
                return Convert.ToDouble(numL.Value);
            }
            set
            {
                numL.Value = Convert.ToDecimal(value);
            }
        }
        public double FLOWG
        {
            get
            {
                return Convert.ToDouble(numFLOWG.Value);
            }
            set
            {
                numFLOWG.Value = Convert.ToDecimal(value);
            }
        }
        public double FLOWL
        {
            set
            {
                numFLOWL.Value = Convert.ToDecimal(value);
            }
            get
            {
                return Convert.ToDouble(numFLOWL.Value);
            }
        }
        public double RHOG
        {
            get
            {
                return Convert.ToDouble(numRHOG.Value);
            }
            set
            {
                numRHOG.Value = Convert.ToDecimal(value);
            }
        }
        public double RHOL
        {
            set
            {
                numRHOL.Value = Convert.ToDecimal(value);
            }
            get
            {
                return Convert.ToDouble(numRHOL.Value);
            }
        }
        public double VISCG
        {
            set
            {
                numVISCG.Value = Convert.ToDecimal(value);
            }
            get
            {
                return Convert.ToDouble(numVISCG.Value);
            }
        }
        public double VISCL
        {
            set
            {
                numVISCL.Value = Convert.ToDecimal(value);
            }
            get
            {
                return Convert.ToDouble(numVISCL.Value);
            }
        }
        public double SIGMA
        {
            get
            {
                return Convert.ToDouble(numSIGMA.Value);
            }
            set
            {
                numSIGMA.Value = Convert.ToDecimal(value);
            }
        }
        public double RF
        {
            set
            {
                numRF.Value = Convert.ToDecimal(value);
            }
            get
            {
                return Convert.ToDouble(numRF.Value);
            }
        }
        public double P
        {
            set
            {
                numP.Value = Convert.ToDecimal(value);
            }
            get
            {
                return Convert.ToDouble(numP.Value);
            }
        }
        public double EPS
        {
            set
            {
                numEPS.Value = Convert.ToDecimal(value);
            }
            get
            {
                return Convert.ToDouble(numEPS.Value);
            }
        }

        public string Nome
        {
            get
            {
                return txtNome.Text;
            }
            set
            {
                txtNome.Text = value;
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            switch (TipoHV)
            {
                case TipoDeTrecho.Horizontal:

                    TrechoH.Nome = Nome;
                    TrechoH.D = D;
                    TrechoH.L = L;
                    TrechoH.FLOWG = FLOWG;
                    TrechoH.FLOWL = FLOWL;
                    TrechoH.RHOG = RHOG;
                    TrechoH.RHOL = RHOL;
                    TrechoH.VISCG = VISCG;
                    TrechoH.VISCL = VISCL;
                    TrechoH.SIGMA = SIGMA;
                    TrechoH.RF = RF;
                    TrechoV.EPS = EPS;
                    break;
                case TipoDeTrecho.Vertical:
                    TrechoV.Nome = Nome;
                    TrechoV.D = D;
                    //TrechoV.L = L;

                    if (EPS == 0.0973582)
                    {
                        TrechoV.L = L / 100;
                    }
                    else
                    {
                        TrechoV.L = L;
                    }

                    TrechoV.FLOWG = FLOWG;
                    TrechoV.FLOWL = FLOWL;
                    TrechoV.RHOG = RHOG;
                    TrechoV.RHOL = RHOL;
                    TrechoV.VISCG = VISCG;
                    TrechoV.VISCL = VISCL;
                    TrechoV.SIGMA = SIGMA;
                    TrechoV.EPS = EPS;
                    TrechoV.P = P;
                    break;
            }
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
