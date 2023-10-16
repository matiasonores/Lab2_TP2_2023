using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_Laboratorio2_2023.Modelos
{
    public class Reserva
    {
        

        // ArrayList de Tipo DateTime
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        int idReserva;
        // ArrayList de Tipo int

        public int nroHabitacion { get; set; }
        // ArrayList de Tipo Cliente

        public Cliente Cliente { get; set; }

        public Reserva(DateTime fechaInicio, DateTime fechaFin, int nroHabitacion,string nombre,int dni,int idReserva)
        {
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            this.nroHabitacion = nroHabitacion;
            Cliente = new Cliente(nombre,dni);
            this.idReserva = idReserva;
        }   
    }
}
