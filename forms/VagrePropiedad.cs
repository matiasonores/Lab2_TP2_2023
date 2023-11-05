using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjercicioHotel
{
    public partial class VagrePropiedad : Form
    {
        public VagrePropiedad()
        {
            InitializeComponent();
        }

        private void rBHotel_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = true;            
            label2.Visible = false;
            nUDPlazas.Visible = false;
            tbdiasminimos.Visible = false;
            label8.Visible = false;
        }

        private void rBCasa_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = false;            
            label2.Visible = true;
            nUDPlazas.Visible = true;
            tbdiasminimos.Visible = true;
            label8.Visible = true;
        }

        bool clickeado = false;
        public bool Clickeado
        {
            get { return clickeado; }
        }
        private void btAgregarImagen_Click(object sender, EventArgs e)
        {
            clickeado = !clickeado;
            int n = 1;
            if(Clickeado)
            {
                if (n == 1)
                {
                    if (oFD2.ShowDialog() == DialogResult.OK)
                    {
                        Image img1 = Image.FromFile(oFD2.FileName);
                        pictureBox1.Image = img1;
                        n++;
                    }
                }
                if (n == 2)
                {
                    if (oFD2.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox2.Image = Image.FromFile(oFD2.FileName);
                        n++;
                    }
                }
                if (n == 3)
                {
                    if (oFD2.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox3.Image = Image.FromFile(oFD2.FileName);
                        n++;
                    }
                }
                if (n == 4)
                {
                    if (oFD2.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox4.Image = Image.FromFile(oFD2.FileName);
                        n = 1;
                    }
                }


            }
        }
    }
}
