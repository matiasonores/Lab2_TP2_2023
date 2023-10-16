using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TP2_Laboratorio2_2023.Formularios;
using TP2_Laboratorio2_2023.Modelos;

namespace TP2_Laboratorio2_2023
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        

        private void btnEditarReserva_Click(object sender, EventArgs e)
        {
            int idReserva;
            idReserva = 0;
            //FReserva fReserva = new FReserva(idReserva);
            Reserva reserva = new Reserva();
            FReserva fReserva = new FReserva(reserva);
            fReserva.ShowDialog();
        }

        private void btnCliente_Click_1(object sender, EventArgs e)
        {
            FCliente fCliente = new FCliente();

            fCliente.ShowDialog();
        }

        private void btnReserva_Click(object sender, EventArgs e)
        {
            FReserva fReserva = new FReserva();
            fReserva.ShowDialog();
        }
    }
}
