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
    public partial class FVerReservas : Form
    {
        public FVerReservas()
        {
            InitializeComponent();
        }

       

        private void progressBar1_Click(object sender, EventArgs e)
        {
            ProgressBar progressBar = new ProgressBar();
        }



        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.1;
            if (this.Opacity == 0)
            {
                timer2.Stop();
                this.Close();
            }
        }

    }
}
