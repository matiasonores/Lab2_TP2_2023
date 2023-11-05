using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Tp2.clases
{
    [Serializable]
    internal class Hotel : Propiedad
    {
        private List<Habitacion> habitaciones = new List<Habitacion>();

        protected double valorBase;
        public int Unaplaza { get; set; }
        public int Dosplazas { get; set; }
        public int Tresplazas { get; set; }

        public List<Habitacion> VerHabitaciones
        {
            get { return habitaciones; }
        }

        public Hotel(int num, string nombre, double valorBase, string direccion, string loc, int unaplaza, int dosplazas, int tresplazas, bool[] servicios, Image img1, Image img2, Image img3, Image img4) : base(num, nombre, valorBase, direccion, loc, img1, img2, img3, img4)
        {

            AgregarServicios(servicios);
            for (int i = 0; i < unaplaza; i++)
            {
                AltaHabitacion(1);
            }
            for (int i = 0; i < dosplazas; i++)
            {
                AltaHabitacion(2);
            }
            for (int i = 0; i < tresplazas; i++)
            {
                AltaHabitacion(3);
            }

            Unaplaza = unaplaza;
            Dosplazas = dosplazas;
            Tresplazas = tresplazas;

        }

        public Habitacion AltaHabitacion(int cantPlazas)
        {
            Habitacion unaHab = new Habitacion(cantPlazas, this);
            habitaciones.Add(unaHab);
            return unaHab;
        }

        public override double CalcularPrecio(int dias, int plazas)
        {
            double valor = base.ValorBase * dias;

            if (plazas == 1)
            {
                return valor;
            }
            else if (plazas == 2)
            {
                valor = valor * 1.8;
            }
            else if (plazas == 3)
            {
                valor = valor * 2.5;
            }
            return valor;
        }



    }
}
