using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Tp2.clases
{
    [Serializable]
    abstract class Propiedad : IComparable
    {
        public string Nombre { get; set; }
        public double ValorBase { get; set; }
        public string Direccion { get; private set; }
        public string Loc { get; private set; }
        public Image Imagen1 { get; private set; }
        public Image Imagen2 { get; private set; }
        public Image Imagen3 { get; private set; }
        public Image Imagen4 { get; private set; }
        public int Codigo { get; set; }

        protected List<TipoServicios> servicios = new List<TipoServicios>();
        public enum TipoServicios { Cochera, Pileta, WiFi, Limpieza, Desayuno, PetFriendly }

        public List<TipoServicios> VerServicios
        {
            get { return servicios; }
            set { servicios = value; }
        }

        public Propiedad(int num, string nombre, double valorBase, string direccion, string loc, Image img1, Image img2, Image img3, Image img4)
        {
            Imagen1 = img1;
            Imagen2 = img2;
            Imagen3 = img3;
            Imagen4 = img4;
            Nombre = nombre;
            ValorBase = valorBase;
            Direccion = direccion;
            Loc = loc;
            Codigo = num;
        }

        public abstract double CalcularPrecio(int dias, int plazas);

        public List<TipoServicios> AgregarServicios(bool[] servicioselegidos) // 0-Cochera, 1-Pileta, 2-WiFi, 3-Limpieza, 4-Desayuno, 5-PetFriendly
        {
            // Recibo el vector de los checkbox del form, checkeado = true, nocheckeado = false;
            for (int i = 0; i < 6; i++)
            {
                if (servicioselegidos[i] == true)
                {
                    servicios.Add((TipoServicios)i); // uso el enumerador de Hotel, y agrego al arrayList los servicios.
                }
            }
            return servicios;
        }

        public int CompareTo(Object obj)
        {
            Propiedad p = (Propiedad)obj;

            return Codigo.CompareTo(p.Codigo);
        }

        public override string ToString()
        {
            string linea = string.Format("{0}, {1}, {2}, {3}, {4}", Nombre, Codigo, ValorBase, Direccion, Loc);
            return linea;
        }
    }
}
