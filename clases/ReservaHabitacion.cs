using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Tp2.clases
{
    [Serializable]
    internal class ReservaHabitacion : Reserva
    {
        private Habitacion unaHabitacion;
        private List<DateTime> diasReservados = new List<DateTime>();

        public ReservaHabitacion(List<Cliente> lista, DateTime entrada, int dias, Propiedad prop, double precio, int cantPlazas)
            : base(lista, entrada, dias, prop, precio)
        {
            foreach (Habitacion hab in ((Hotel)prop).VerHabitaciones)
            {
                if (hab.Plazas == cantPlazas)
                {
                    unaHabitacion = hab;
                }
            }

            DateTime date = this.Entrada;
            while (date != this.Salida)
            {
                diasReservados.Add(date);
                date = date.AddDays(1);
            }
        }
        public ReservaHabitacion(Cliente clien, DateTime entrada, int dias, Propiedad prop, double precio, int cantPlazas)
           : base(clien, entrada, dias, prop, precio)
        {
            foreach (Habitacion hab in ((Hotel)prop).VerHabitaciones)
            {
                if (hab.Plazas == cantPlazas)
                {
                    unaHabitacion = hab;
                }
            }

            DateTime date = this.Entrada;
            while (date != this.Salida)
            {
                diasReservados.Add(date);
                date = date.AddDays(1);
            }
        }
        public Habitacion VerHabitacion
        {
            get { return unaHabitacion; }
        }

        public List<DateTime> DiasReservados
        {
            get
            {
                return diasReservados;
            }
        }

        public override string ToString()
        {
            string linea = String.Format("{0} - Plazas: {1}", base.ToString(), unaHabitacion.Plazas);
            return linea;
        }
    }
}

