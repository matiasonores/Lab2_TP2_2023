using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab2Tp2.clases;

namespace EjercicioHotel
{
    internal partial class VentanadeReservas : Form
    {

        List<DateTime> diasSeleccionados = new List<DateTime>();
        List<DateTime> diasReservados = new List<DateTime>();
        DateTime hoy = DateTime.Now;
        private int dias = 0;

        public List<Reserva> reservas;
        public Propiedad propiedad;
        public double Costo { get; set; }

        public VentanadeReservas(List<Reserva> reservas, Propiedad prop)
        {
            InitializeComponent();

            this.reservas = reservas;
            this.propiedad = prop;

            this.monthCalendar1.MinDate = DateTime.Today;
            this.monthCalendar1.MaxDate = new DateTime(2023, 12, 31);

            this.lblErrorReserva.Text = "";
            this.button1.Enabled = false;

            if (propiedad is Casa)
            {
                lnumerohab.Visible = false;
                nCantPlazas.Visible = false;
                //MarcarDiasReservadosCasa();
            }
        }
        public VentanadeReservas()
        {
            InitializeComponent();
        }

        public void MarcarDiasReservadosCasa()
        {
            foreach (Reserva item in this.reservas)
            {
                if (item.UnaPropiedad.Codigo == propiedad.Codigo)
                {
                    DateTime date = item.Entrada;
                    while (date != item.Salida)
                    {
                        diasReservados.Add(date);

                        monthCalendar1.AddBoldedDate(date);

                        date = date.AddDays(1);
                    }
                }
            }
        }

        public bool ValidarDiasSeleccionadosCasa()
        {
            bool valido = true;

            foreach (DateTime dia in diasSeleccionados)
            {
                if (diasReservados.Contains(dia))
                {
                    valido = false;
                    break;
                }
            }

            this.button1.Enabled = valido;

            return valido;
        }

        public bool ValidarDisponibilidadHotel()
        {
            bool disponible = false;

            int cantPlazasReserva = Convert.ToInt32(nCantPlazas.Value);

            List<Habitacion> habitaciones = ((Hotel)propiedad).VerHabitaciones.FindAll(h => h.Plazas == cantPlazasReserva);

            int cantHabDisponibles = habitaciones.Count;

            List<Reserva> reservasHotel = this.reservas.FindAll(r =>
                r is ReservaHabitacion &&
                r.UnaPropiedad.Codigo == propiedad.Codigo &&
                ((ReservaHabitacion)r).VerHabitacion.Plazas == cantPlazasReserva
            );

            if (reservasHotel.Count == 0 && cantHabDisponibles > 0)
            {
                disponible = true;
            }
            else
            {
                foreach (ReservaHabitacion reserva in reservasHotel)
                {
                    List<DateTime> interseccion = diasSeleccionados.Intersect(reserva.DiasReservados).ToList();

                    if (interseccion.Count > 0)
                    {
                        cantHabDisponibles--;
                    }
                }

                if (cantHabDisponibles > 0)
                {
                    disponible = true;
                }
            }

            this.button1.Enabled = disponible;

            return disponible;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            diasSeleccionados.Clear();

            dias = Convert.ToInt32(numericUpDown1.Value);
            DateTime fechaInicio = monthCalendar1.SelectionStart;

            monthCalendar1.SelectionEnd = fechaInicio.AddDays(dias);
            DateTime fechaSalida = monthCalendar1.SelectionEnd;

            for (int i = 0; i < dias; i++)
            {
                diasSeleccionados.Add(fechaInicio.AddDays(i));
            }

            if (propiedad is Casa)
            {
                if (ValidarDiasSeleccionadosCasa() && diasSeleccionados.Count > ((Casa)propiedad).DiasMinimos)
                {
                    lblErrorReserva.Text = "";
                    lEntrada.Text = fechaInicio.ToShortDateString();
                    lSalida.Text = fechaSalida.ToShortDateString();
                    
                }
                else
                {
                    lblErrorReserva.Text = "Alguno de los días seleccionados no esta disponible";
                }
            }
            else if (propiedad is Hotel)
            {
                if (ValidarDisponibilidadHotel())
                {
                    lblErrorReserva.Text = "";
                    lEntrada.Text = fechaInicio.ToShortDateString();
                    lSalida.Text = fechaSalida.ToShortDateString();
                }
                else
                {
                    lblErrorReserva.Text = String.Format("No hay habitaciones de {0} plazas en las fechas seleccionadas.", nCantPlazas.Value);
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            diasSeleccionados.Clear();

            dias = Convert.ToInt32(numericUpDown1.Value);
            DateTime fechaInicio = monthCalendar1.SelectionStart;
            monthCalendar1.SelectionEnd = fechaInicio.AddDays(dias);

            for (int i = 0; i < dias; i++)
            {
                diasSeleccionados.Add(fechaInicio.AddDays(i));
            }

            if (propiedad is Casa)
            {
                if (ValidarDiasSeleccionadosCasa() && diasSeleccionados.Count > ((Casa)propiedad).DiasMinimos)
                {
                    lblErrorReserva.Text = "";
                    lEntrada.Text = fechaInicio.ToShortDateString();
                    lSalida.Text = fechaInicio.AddDays(dias).ToShortDateString();
                    lMonto.Text = String.Format("$ {0}", propiedad.CalcularPrecio(dias,0));
                    Costo = propiedad.CalcularPrecio(dias, 0);
                }
                else
                {
                    lSalida.Text = "";
                    lblErrorReserva.Text = "Alguno de los días seleccionados no esta disponible";
                    button1.Enabled = false;
                }
            }

            else if (propiedad is Hotel)
            {

                if (ValidarDisponibilidadHotel())
                {
                    lblErrorReserva.Text = "";
                    lEntrada.Text = fechaInicio.ToShortDateString();
                    lSalida.Text = fechaInicio.AddDays(dias).ToShortDateString();
                    int plazas = Convert.ToInt32(nCantPlazas.Value);
                    lMonto.Text = String.Format("$ {0}", ((Hotel)propiedad).CalcularPrecio(dias,plazas));
                    Costo= propiedad.CalcularPrecio(dias, plazas);
                }
                else
                {
                    lSalida.Text = "";
                    lblErrorReserva.Text = String.Format("No hay habitaciones de {0} plazas en las fechas seleccionadas.", nCantPlazas.Value);
                    button1.Enabled=false;
                }
            }
        }

        private void nCantPlazas_ValueChanged(object sender, EventArgs e)
        {
            if (ValidarDisponibilidadHotel())
            {
                lblErrorReserva.Text = "";
                int plazas = Convert.ToInt32(nCantPlazas.Value);
                lMonto.Text = String.Format("$ {0}", ((Hotel)propiedad).CalcularPrecio(dias,plazas));
                Costo = propiedad.CalcularPrecio(dias, plazas);
            }
            else
            {
                lSalida.Text = "";
                lblErrorReserva.Text = String.Format("No hay habitaciones de {0} plazas en las fechas seleccionadas.", nCantPlazas.Value);
            }
        }
        private void FullScreenImage(Image imageToShow)
        {
            Form fullScreenForm = new Form()
            {
                WindowState = FormWindowState.Maximized,
                FormBorderStyle = FormBorderStyle.None,
                BackgroundImage = imageToShow,
                BackgroundImageLayout = ImageLayout.Zoom,
            };

            fullScreenForm.Click += fullScreen_Click;

            fullScreenForm.ShowDialog();
        }

        private void fullScreen_Click(object sender, EventArgs e)
        {
            ((Form)sender).Close();
        }
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            FullScreenImage(((PictureBox)sender).Image);
        }
        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            FullScreenImage(((PictureBox)sender).Image);
        }
        private void pictureBox3_DoubleClick(object sender, EventArgs e)
        {
            FullScreenImage(((PictureBox)sender).Image);
        }
        private void pictureBox4_DoubleClick(object sender, EventArgs e)
        {
            FullScreenImage(((PictureBox)sender).Image);
        }
    }
}
