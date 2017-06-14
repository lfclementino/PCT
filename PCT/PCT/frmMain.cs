using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace PCT
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            Botoes();

            clsFundo.Resize(this);

            //foreach (Control c in this.Controls)
            //{
            //    if (c is MdiClient)
            //    {
            //        c.BackColor = Color.White;
            //        c.SendToBack();
            //    }
            //}
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsCalculo Calculo = new clsCalculo();
            Calculo.Trechos = new ArrayList();
            frmJanelaCalculo JanelaMDI = new frmJanelaCalculo(this,Calculo);
            JanelaMDI.Arquivo = null;
            JanelaMDI.SetStatus(frmJanelaCalculo.Modo.Novo);
            JanelaMDI.MdiParent = this;
            JanelaMDI.Show();
        }

        private frmJanelaCalculo Ativa;

        public void JanelaAtiva(frmJanelaCalculo aAtiva)
        {
            Ativa = aAtiva;

            Botoes();
        }

        private void Botoes()
        {
            salvarToolStripMenuItem.Enabled = (Ativa != null);
            salvarComotoolStripMenuItem.Enabled = (Ativa != null);
        }

        public enum TipoSalvar
        {
            Salvar,
            SalvarComo
        }

        public void SalvarXML(TipoSalvar Tipo)
        {
            string OndeSalvar;

            switch (Tipo)
            {
                case TipoSalvar.Salvar:
                    if (Ativa.Arquivo != null)
                    {
                        OndeSalvar = Ativa.Arquivo;

                        XmlSerializer writer = new XmlSerializer(typeof(clsCalculo));

                        StreamWriter PCTfile = new StreamWriter(OndeSalvar);

                        writer.Serialize(PCTfile, Ativa.DataCalculo);
                        PCTfile.Close();

                        Ativa.Arquivo = OndeSalvar;

                        Ativa.SetStatus(frmJanelaCalculo.Modo.Salvo);
                    }
                    else
                    {
                        SalvarXML(TipoSalvar.SalvarComo);
                    }
                    break;
                case TipoSalvar.SalvarComo:
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = "Arquivo do PCT (*.pct)|*.pct|Todos os arquivos (*.*)|*.*";
                    //dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    dialog.RestoreDirectory = true;
                    dialog.FilterIndex = 1;
                    dialog.DefaultExt = "pct";
                    dialog.AddExtension = true;
                    dialog.Title = "Salvar Como ...";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        OndeSalvar = dialog.FileName;

                        XmlSerializer writer = new XmlSerializer(typeof(clsCalculo));

                        StreamWriter PCTfile = new StreamWriter(OndeSalvar);

                        writer.Serialize(PCTfile, Ativa.DataCalculo);
                        PCTfile.Close();

                        Ativa.Arquivo = OndeSalvar;

                        Ativa.SetStatus(frmJanelaCalculo.Modo.Salvo);
                    }
                    else
                    {
                        return;
                    }
                    break;
            }
        }

        private void salvatrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SalvarXML(TipoSalvar.Salvar);
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbreXml();
        }

        private bool VerificaArquivo(string file)
        {
            StreamReader tr = new StreamReader(file, Encoding.Default);
            string temp = tr.ReadToEnd();
            if (temp.Contains("PCT") && temp.Contains("v 1.0.0"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ArquivoJaAberto(string Caminho)
        {
            bool Resp = false;

            foreach (Form Janela in Application.OpenForms)
            {
                if (Janela is frmJanelaCalculo)
                {
                    if (((frmJanelaCalculo)Janela).Arquivo == Caminho)
                    {
                        Resp = true;
                    }
                }
            }

            return Resp;
        }

        private void AbreXml()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Arquivo do PCT (*.pct)|*.pct|Todos os arquivos (*.*)|*.*";
            //dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            dialog.RestoreDirectory = true;
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;
            dialog.Title = "Abrir";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (VerificaArquivo(dialog.FileName))
                {
                    if (!ArquivoJaAberto(dialog.FileName))
                    {
                        XmlSerializer writer = new XmlSerializer(typeof(clsCalculo));

                        StreamReader PCTreader = new StreamReader(dialog.FileName);

                        clsCalculo perfil = (clsCalculo)writer.Deserialize(PCTreader);
                        PCTreader.Close();
                        PCTreader.Dispose();

                        frmJanelaCalculo JanelaMDI = new frmJanelaCalculo(this, perfil);
                        JanelaMDI.Arquivo = dialog.FileName;
                        JanelaMDI.SetStatus(frmJanelaCalculo.Modo.Aberto);
                        JanelaMDI.MdiParent = this;
                        JanelaMDI.Show();
                    }
                    else
                    {
                        MessageBox.Show("O Arquivo " + dialog.FileName + " já está aberto pelo PCT.", "PCT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("O Arquivo " + dialog.FileName + " não pode ser aberto pelo PCT.\r\n\r\nVerifique o arquivo e tente novamente.", "PCT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            clsFundo.Resize(this);
        }

        private void salvarComotoolStripMenuItem_Click(object sender, EventArgs e)
        {
            SalvarXML(TipoSalvar.SalvarComo);
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout Sobre = new frmAbout();
            Sobre.ShowDialog();
        }
    }
}
