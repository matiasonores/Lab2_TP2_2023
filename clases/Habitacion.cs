using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Tp2.clases
{
    [Serializable]
    class Habitacion
    {
        private int plazas;
        private double precio;
        Hotel hotel;

        public Habitacion(int plaza, Hotel hotel)
        {
            this.plazas = plaza;
            this.hotel = hotel;
        }

        public Hotel Hotel
        {
            get { return hotel; }
        }

        public int Plazas
        {
            get { return plazas; }
        }

        public double CalcularCosto(double pre)
        {
            if (plazas == 2)
            {
                precio = pre * 0.8;
            }
            if (plazas == 3)
            {
                precio = pre * 1.4;
            }

            return this.precio;
        }

    }
}