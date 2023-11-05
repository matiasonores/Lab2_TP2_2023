using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Tp2.clases
{
    [Serializable]
    internal class Casa : Propiedad
    {

        private int diasMinimos;

        public int Plazas { get; set; }

        public int DiasMinimos
        {
            get { return diasMinimos; }
            set { diasMinimos = value; }
        }


        public Casa(int num, string nombre, int plazas, string dir, string loc, double valorBase, int minimos, bool[] servicios, Image img1, Image img2, Image img3, Image img4)
            : base(num, nombre, valorBase, dir, loc, img1, img2, img3, img4)
        {
            this.servicios = AgregarServicios(servicios);
            Plazas = plazas;
            diasMinimos = minimos;
        }

        public override double CalcularPrecio(int dias, int plazas)
        {
            int cont = servicios.Count;

            double extras = cont * 0.5;
            double result = base.ValorBase * extras * dias;

            return result;
        }

        //hola

    }
}

