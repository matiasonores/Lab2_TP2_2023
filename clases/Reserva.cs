using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Tp2.clases
{
    [Serializable]
    internal class Reserva
    {
        private Propiedad unaPropiedad;
        private List<Cliente> inquilinos = new List<Cliente>();
        private Cliente unCliente;
        private DateTime entrada;
        private int dias;


        public int ID
        { get; set; }
        public double Precio { get; set; }
        public DateTime Salida { get; set; }

        public DateTime Entrada
        {
            get { return entrada; }
        }
        public int Dias
        {
            get { return dias; }
        }
        public Propiedad UnaPropiedad
        {
            get { return unaPropiedad; }
        }

        public List<Cliente> Inquilinos
        {
            get { return inquilinos; }

        }
        public Cliente VerCliente
        {
            get { return unCliente; }
        }

        public Reserva(Cliente clien, DateTime entrada, int dias, Propiedad prop, double precio)
        {
            unaPropiedad = prop;
            inquilinos.Add(clien);
            this.entrada = entrada;
            this.dias = dias;
            Salida = entrada.AddDays(dias);
            Precio = precio;
            unCliente = clien;
            ID++;
        }

        public Reserva(List<Cliente> lista, DateTime entrada, int dias, Propiedad prop, double precio)
        {
            unaPropiedad = prop;
            inquilinos = lista;
            this.entrada = entrada;
            this.dias = dias;
            Salida = entrada.AddDays(dias);
            Precio = precio;
            ID++;
        }
        public Reserva(int ide, DateTime entra, DateTime sale)
        {
            ID = ide;
            entrada = entra;
            Salida = sale;
        }

        public override string ToString()
        {
            string linea = String.Format("Id: {0} , Checkin: {1} ,  Checkout: {2}", ID, entrada, Salida);
            return linea;
        }

    }
}