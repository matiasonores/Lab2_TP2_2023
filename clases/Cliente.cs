using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Tp2.clases
{
    [Serializable]
    internal class Cliente : IComparable
    {
        public int Dni { get; set; }
        public string Nombre { get; set; }
        public int Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }



        public Cliente(int dni, string nombre, int telefono, string direccion, DateTime fecha)
        {
            Dni = dni;
            Nombre = nombre;
            Telefono = telefono;
            Direccion = direccion;
            FechaNacimiento = fecha;
        }

        public int CompareTo(Object obj)
        {
            Cliente c = (Cliente)obj;
            return Nombre.CompareTo(c.Nombre);

        }
        public override string ToString()
        {
            string line = String.Format("{0};{1};{2};{3};{4}", Dni, Nombre, Telefono, Direccion, FechaNacimiento.ToShortDateString());
            return line;
        }

        //public string Datos()
        //{
        //    string linea = string.Format("DNI: {0} , {1}", Dni, Nombre);
        //    return linea;
        //}


    }
}
