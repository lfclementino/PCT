using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
using System.Drawing;

namespace PCT
{
    [Serializable]
    public class clsCalculo
    {
        public clsCalculo()
        {
            //Trechos = new ArrayList();
        }

        private ArrayList oTrechos;

        public string Sobre = "PCT - v 1.0.0 - 2010";

        [XmlArrayItem(typeof(clsHorizontal))]
        [XmlArrayItem(typeof(clsVertical))]
        public ArrayList Trechos
        {
            get
            {
                return oTrechos;
            }
            set
            {
                oTrechos = value;
            }
        }
    }
}
