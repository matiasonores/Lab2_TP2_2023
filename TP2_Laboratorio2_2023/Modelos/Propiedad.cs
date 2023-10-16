using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_Laboratorio2_2023.Modelos
{
    public abstract class Propiedad
    {
        public int IdPropiedad { get; set; }
        public ArrayList Reservas { get; set; }
        public double PrecioBase { get; set; }
        public double Costo { get; set; }

        public bool estaReservado = false;
        public DateTime FechaInicioReserva { get; set; } //Min
        public DateTime FechaFinReserva { get; set; } //Max
        public ArrayList FechasReservadas { get; set; }
        public ArrayList FechasDisponibles { get; set; }

        public string Direccion { get; set; }
        public ArrayList Imagenes { get; set; } //Guardamos la coleccion de rutas de las imagenes cargadas en el sistema

        public Propiedad()
        {
            
        }
        public Propiedad(DateTime fechaInicioReserva, DateTime fechaFinReserva, DateTime[] fechasReservadas)
        {
            this.estaReservado = true;
            FechaInicioReserva = fechaInicioReserva;
            FechaFinReserva = fechaFinReserva;
            Reservas = new ArrayList();
        }

        public void AgregarReserva(Reserva reserva)
        {
            //Recorres la reserva y agregas las fechas reservadas al arreglo de fechas reservadas

            if (ReservarFecha(reserva.Fechas))
            {
                Reservas.Add(reserva);
            }
        }

        private bool ReservarFecha(ArrayList fechas)
        {
            bool puedeReservar = true;

            //Recorremos las fechas disponibles de la propiedad: Es decir, tenemos disponible del 1 al 10
            foreach(DateTime fechaReservada in FechasReservadas)
            {
                //Por cada fecha disponible, vamos a recorrer el listado de fechas de la reserva: Por ejemplo, del 1 al 3
                foreach (DateTime fecha in fechas)
                {
                    //Si encontramos una coincidencia, es porque la propiedad ya fue reservada
                    if (fecha == fechaReservada)
                    {
                        puedeReservar = false;
                        //Lanzar exception de fecha
                        throw new Exception();
                    }
                }
            }

            //Si llegó hasta este punto sin saltar el exception
            if (puedeReservar)
            {
                //Agregamos las fechas de la reserva a las fechas reservadas
                foreach(DateTime fecha in fechas)
                {
                    FechasReservadas.Add(fecha);
                }
            }
            return puedeReservar;
        }

        public abstract void CalcularCosto();
    }
}
