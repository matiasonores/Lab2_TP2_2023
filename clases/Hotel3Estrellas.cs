using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Tp2.clases
{
    [Serializable]
    class Hotel3Estrellas : Hotel
    {
        public Hotel3Estrellas(int num, int unaplaza, int dosplazas, int tresplazas, double valorBase, string direccion, string loc, string nombre, bool[] servicios, Image img1, Image img2, Image img3, Image img4)
            : base(num, nombre, valorBase, direccion, loc, unaplaza, dosplazas, tresplazas, servicios, img1, img2, img3, img4)
        {
            base.valorBase = valorBase * 1.2;
        }
    }
}