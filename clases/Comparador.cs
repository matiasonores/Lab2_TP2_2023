using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Tp2.clases
{
    [Serializable]
    internal class Comparador
    {

        public List<Propiedad.TipoServicios> servicios;
        public int precioMinimo;
        public int precioMaximo;
        public int plazas;
        public string localidad;

        public Comparador(List<Propiedad.TipoServicios> servicios, int precioMinimo, int precioMaximo, int plazas, string localidad)
        {

            this.servicios = servicios;
            this.precioMinimo = precioMinimo;
            this.precioMaximo = precioMaximo;
            this.plazas = plazas;
            this.localidad = localidad;

        }

    }
}
