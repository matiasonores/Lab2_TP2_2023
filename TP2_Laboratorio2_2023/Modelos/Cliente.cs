using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_Laboratorio2_2023.Modelos
{
    public class Cliente
    {
        public string  Nombre { get; set; }
        public int DNI { get; set; }


        public Cliente(string nombre,int dni)
        {
          Nombre= nombre;
            DNI= dni;
        }
    }
}
