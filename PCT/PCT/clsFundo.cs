using System;
using System.Collections.Generic;
using System.Text;

namespace PCT
{
    class clsFundo
    {
        static private frmFundo frmIm = null;

        static private frmFundo GetImagem()
        {
            if (frmIm == null)
            {
                frmIm = new frmFundo();
                GetImagem().Text = "Imagem Fundo";
                GetImagem().Enabled = false;
            }
            return frmIm;
        }

        static internal void Show(frmMain oMDI)
        {
            GetImagem().MdiParent = oMDI;
            GetImagem().Show();
        }

        static internal void Resize(frmMain oMDI)
        {
            if (frmIm == null)
            {
                frmIm = new frmFundo();
                frmIm.MdiParent = oMDI;
                frmIm.Show();
                frmIm.Location = oMDI.ClientRectangle.Location;
                frmIm.Enabled = false;
            }
            else
            {
                //int gapheigth = 16;
                //int gapwidth = -4;
                int gapheigth = 27;
                int gapwidth = -4;
                int diffheight = (oMDI.Size.Height - oMDI.ClientRectangle.Size.Height) + gapheigth;
                int diffwidth = (oMDI.Size.Width - oMDI.ClientRectangle.Size.Width) + gapwidth;

                frmIm.Height = oMDI.ClientRectangle.Height - diffheight;
                frmIm.Width = oMDI.ClientRectangle.Width - diffwidth;
            }
        }

        static internal void SendToBack()
        {
            GetImagem().SendToBack();
        }

        static internal void Close()
        {
            GetImagem().Close();
        }
    }
}
