using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjercicioHotel
{
    public partial class VerReservas : Form
    {
        public VerReservas()
        {
            InitializeComponent();
        }


        
        public int IndiceDG { get; set; }
       
       


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            IndiceDG = e.RowIndex;
        }


    }
}
