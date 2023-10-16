using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_Laboratorio2_2023.Modelos
{
    public class Hotel : Propiedad
    {
        public int Estrellas { get; set; }
        public int NroHabitaciones { get; set; }
        public int NroCamas { get; set; }


        public Habitaciones[] habitaciones;

        public int PersonasAdmitidas { get; set; }
        public Hotel(DateTime inicio,DateTime fin,int cantHab,int cantCamas ): base(inicio,fin)
        {
            habitaciones = new Habitaciones[cantHab];
            int nroHabitacion = 1;
            for (int i = 0; i < cantHab; i++)
            {
                habitaciones[i] = new Habitaciones(nroHabitacion, cantCamas);
                nroHabitacion++;
            }
        }
        public override void CalcularCosto()
        {
            Costo = PrecioBase * Estrellas * NroCamas;
        }

        //public void Calcular
    }
}
