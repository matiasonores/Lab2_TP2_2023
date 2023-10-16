using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace TP2_Laboratorio2_2023.Modelos
{
    public class Sistema
    {
    ArrayList propiedades = new ArrayList();
    ArrayList reservas = new ArrayList();
        public Sistema()
        {

        }

        public bool AgregarPropiedades(string tipo,DateTime inicio,DateTime fin)
        {
            if (tipo == "hotel")
            {

            propiedades.Add(new Hotel(inicio, fin));
            } else
            {
                propiedades.Add(new Casa(inicio, fin));
            }
        }
    }
}
