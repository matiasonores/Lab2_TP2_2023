using System;
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

        public int PersonasAdmitidas { get; set; }
        public override void CalcularCosto()
        {
            Costo = PrecioBase * Estrellas * NroCamas;
        }

        //public void Calcular
    }
}
