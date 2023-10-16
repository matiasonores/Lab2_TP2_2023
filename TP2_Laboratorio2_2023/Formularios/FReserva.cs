using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TP2_Laboratorio2_2023.Modelos;

namespace TP2_Laboratorio2_2023.Formularios
{
    public partial class FReserva : Form
    {
        public FReserva()
        {
            InitializeComponent();
            cbClientes.Items.Add("Alumno generico");
        }

        //Si se genera el formulario con un objeto Reserva, es para editarlo
        public FReserva(Reserva reserva)
        {
            InitializeComponent();
            this.Text = "Editar Reserva";
            lbReserva.Text = "Editar Reserva";
            //tbCliente.Text = reserva.Cliente.Nombre.ToString();
            cbClientes.Items.Add ("Alumno generico");
            //tbPropiedad.Text = reserva.Propiedad.Nombre.ToString();
            tbPropiedad.Text = "UTN FRP";
            //dateDesde.Value = Convert.ToDateTime(reserva.FechaDesde);
            dateDesde.Value = DateTime.Now.AddDays(-5);
            //dateHasta.Value = Convert.ToDateTime(reserva.FechaHasta);
            dateHasta.Value = DateTime.Now.AddDays(5);
        }
    }
}
