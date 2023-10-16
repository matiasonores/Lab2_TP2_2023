using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_Laboratorio2_2023.Modelos
{
    public class Casa : Propiedad
    {

        private ArrayList _servicios;
        public int Servicios
        {
            get
            {
                return _servicios.Count;
            }
        }

        public void AgregarServicio(string servicio)
        {
            _servicios.Add(servicio);
        }

        public override void CalcularCosto()
        {
            Costo = Servicios * PrecioBase;
        }
    }
}
