using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using Lab2Tp2.clases;

namespace Lab2Tp2.clases
    {
    [Serializable]
    internal class Empresa : IExportable
    {
        private List<Propiedad> propiedades = new List<Propiedad>();
        private List<Hotel> hoteles = new List<Hotel>();
        private List<Casa> casas = new List<Casa>();
        private List<Reserva> reservas = new List<Reserva>();
        private List<Cliente> clientes = new List<Cliente>();

        public enum TipoPropiedad { Casa, Hotel }

        public List<Reserva> Reservas
        {
            get { return reservas; }
            set { reservas = value; }
        }

        public List<Hotel> Hoteles
        {
            get { return hoteles; }
        }

        public List<Casa> Casas
        {
            get { return casas; }
        }

        public List<Propiedad> Propiedades
        {
            get { return propiedades; }

        }
        public List<Cliente> Clientes
        {
            get { return clientes; }
            set { clientes = value; }
        }
        public void AgregarCliente(Cliente unCliente)
        {
            clientes.Add(unCliente);
        }

        public void AgregarPropiedad(Propiedad unaPropiedad)
        {
            if (unaPropiedad is Casa)
            {
                casas.Add((Casa)unaPropiedad);
                propiedades.Add(unaPropiedad);

            }
            if (unaPropiedad is Hotel)
            {
                hoteles.Add((Hotel)unaPropiedad);
                propiedades.Add(unaPropiedad);

            }
        }
        public void ReservarHabitacion(List<Cliente> lista, DateTime entrada, int dias, Propiedad prop, int num, double precio)
        {
            Reserva unaReserva = new ReservaHabitacion(lista, entrada, dias, prop, precio, num);
            reservas.Add(unaReserva);

        }
        public void ReservarHabitacion(Cliente clien, DateTime entrada, int dias, Propiedad prop, int num, double precio)
        {
            Reserva unaReserva = new ReservaHabitacion(clien, entrada, dias, prop, precio, num);
            reservas.Add(unaReserva);

        }

        public void ReservarCasa(List<Cliente> lista, DateTime entrada, int dias, Propiedad prop, double precio)
        {
            Reserva unaReserva = new Reserva(lista, entrada, dias, prop, precio);
            reservas.Add(unaReserva);
        }

        public void ReservarCasa(Cliente clien, DateTime entrada, int dias, Propiedad prop, double precio)
        {
            Reserva unaReserva = new Reserva(clien, entrada, dias, prop, precio);
            reservas.Add(unaReserva);
        }

        public List<Propiedad> Filtrar(Comparador comp)
        {

            List<Propiedad> resultado = new List<Propiedad>();



            foreach (Hotel hotel in hoteles)
            {


                //int cont = 0;
                //for (int i = 0; i < comp.servicios.Count; i++)
                //{
                //    if (hotel.VerServicios.Contains(comp.servicios[i]))
                //    {
                //        cont++;
                //    }
                //}
                //if (cont >= comp.servicios.Count)
                //{
                //    resultado.Add((hotel));
                //}
                if (comp.plazas != 0)
                {
                    if (comp.plazas == 1 && hotel.Unaplaza != 0 || hotel.Loc.ToLower().Contains(comp.localidad.ToLower()))
                    {
                        resultado.Add(hotel);

                    }
                    else if (comp.plazas == 2 && hotel.Dosplazas != 0 || hotel.Loc.ToLower().Contains(comp.localidad.ToLower()))
                    {
                        resultado.Add(hotel);
                    }
                    else if (comp.plazas == 3 && hotel.Tresplazas != 0 || hotel.Loc.ToLower().Contains(comp.localidad.ToLower()))
                    {
                        resultado.Add(hotel);
                    }
                }
                else if (hotel.Loc.ToLower().Contains(comp.localidad.ToLower()))
                {
                    resultado.Add(hotel);
                }
                //if (comp.precioMinimo < hotel.ValorBase && hotel.ValorBase < comp.precioMaximo)
                //{
                //    resultado.Add(hotel);
                //}

            }
            foreach (Casa ca in casas)
            {

                //int cont = 0;
                //for (int i = 0; i < comp.servicios.Count; i++)
                //{
                //    if (ca.VerServicios.Contains(comp.servicios[i]))
                //    {
                //        cont++;
                //    }
                //}
                //if (cont >= comp.servicios.Count)
                //{

                //    {
                //        resultado.Add(ca);
                //    }

                //}
                if (comp.plazas != 0)
                {
                    if (ca.Plazas >= comp.plazas || ca.Loc.ToLower().Contains(comp.localidad.ToLower()))
                    {
                        resultado.Add(ca);
                    }
                }
                else if (ca.Loc.ToLower().Contains(comp.localidad.ToLower()))
                {
                    resultado.Add(ca);
                }
                //if (comp.precioMinimo < ca.ValorBase && ca.ValorBase < comp.precioMaximo)
                //{
                //    resultado.Add(ca);
                //}

            }
            return resultado;
        }

        public void ProcesarPropiedades(FileStream file)
        {
            int ultimo = propiedades.Count;

            StreamReader lectura = new StreamReader(file);

            lectura.ReadLine();
            string[] datos;

            string startupPath = System.IO.Directory.GetCurrentDirectory();

            while (!lectura.EndOfStream)
            {
                ultimo++;
                datos = lectura.ReadLine().Split(';');
                if (datos.Length <= 16)
                {
                    bool[] serv = new bool[6];
                    string nombre = datos[0];
                    int plazas = Convert.ToInt32(datos[1]);
                    string direc = datos[2];
                    string loca = datos[3];
                    if (datos[4] == "x")
                    {
                        serv[0] = true;
                    }
                    if (datos[5] == "x")
                    {
                        serv[1] = true;
                    }
                    if (datos[6] == "x")
                    {
                        serv[2] = true;
                    }
                    if (datos[7] == "x")
                    {
                        serv[3] = true;
                    }
                    if (datos[8] == "x")
                    {
                        serv[4] = true;
                    }
                    if (datos[9] == "x")
                    {
                        serv[5] = true;
                    }
                    int minimos = Convert.ToInt32(datos[10]);

                    double precio = Convert.ToDouble(datos[11]);

                    Image imagen1 = Image.FromFile(startupPath + datos[12]);
                    Image imagen2 = Image.FromFile(startupPath + datos[13]);
                    Image imagen3 = Image.FromFile(startupPath + datos[14]);
                    Image imagen4 = Image.FromFile(startupPath + datos[15]);
                    Propiedad casita = new Casa(ultimo, nombre, plazas, direc, loca, precio, minimos, serv, imagen1, imagen2, imagen3, imagen4);
                    AgregarPropiedad(casita);
                }
                else
                {
                    bool[] serv = new bool[6];
                    string nombre = datos[0];
                    double precio = Convert.ToDouble(datos[1]);
                    string direc = datos[2];
                    string loca = datos[3];
                    int pla1 = Convert.ToInt32(datos[4]);
                    int pla2 = Convert.ToInt32(datos[5]);
                    int pla3 = Convert.ToInt32(datos[6]);
                    if (datos[7] == "x")
                    {
                        serv[0] = true;
                    }
                    if (datos[8] == "x")
                    {
                        serv[1] = true;
                    }
                    if (datos[9] == "x")
                    {
                        serv[2] = true;
                    }
                    if (datos[10] == "x")
                    {
                        serv[3] = true;
                    }
                    if (datos[11] == "x")
                    {
                        serv[4] = true;
                    }
                    if (datos[12] == "x")
                    {
                        serv[5] = true;
                    }

                    Image imagen1 = Image.FromFile(startupPath + datos[13]);
                    Image imagen2 = Image.FromFile(startupPath + datos[14]);
                    Image imagen3 = Image.FromFile(startupPath + datos[15]);
                    Image imagen4 = Image.FromFile(startupPath + datos[16]);

                    Propiedad minihotel = new Hotel(ultimo, nombre, precio, direc, loca, pla1, pla2, pla3, serv, imagen1, imagen2, imagen3, imagen4);
                    AgregarPropiedad(minihotel);

                }
            }
            lectura.Close();
        }

        public Propiedad VerPropiedad(int numero)
        {
            Propiedad prop = null;
            foreach (Casa c in casas)
            {
                if (c.Codigo == numero)
                {
                    prop = c;
                }
            }
            foreach (Hotel h in hoteles)
            {
                if (h.Codigo == numero)
                {
                    prop = h;
                }
            }
            return prop;
        }


        public void Exportar(string ruta, int opc)
        {
            FileStream fs = null;
            StreamWriter sw = null;

            if (opc == 1)
            {
                try
                {
                    fs = new FileStream(ruta, FileMode.Create, FileAccess.Write);
                    sw = new StreamWriter(fs);
                    foreach (Cliente cl in clientes)
                    {
                        sw.WriteLine(cl.ToString());
                    }
                }
                catch
                {
                    Exception ex;
                }
                finally
                {
                    sw.Close();
                    fs.Close();

                }
            }
            else
            {
                try
                {
                    fs = new FileStream(ruta, FileMode.Create, FileAccess.Write);
                    sw = new StreamWriter(fs);
                    foreach (Reserva res in reservas)
                    {
                        sw.WriteLine(res.ToString());
                    }
                }
                catch
                {
                    Exception ex;
                }
                finally
                {
                    sw.Close();
                    fs.Close();

                }
            }
        }
        public void Importar(string ruta, int opc)
        {
            FileStream fs = null;
            StreamReader sr = null;
            if (opc == 1)
            {
                try
                {
                    fs = new FileStream(ruta, FileMode.Open, FileAccess.Read);
                    sr = new StreamReader(fs);
                    string[] linea;
                    string nombre;
                    int dni;
                    int telefono;
                    string direccion;
                    DateTime fecha;
                    Cliente clien;
                    while (!sr.EndOfStream)
                    {
                        linea = sr.ReadLine().Split(';');
                        dni = Convert.ToInt32(linea[0]);
                        nombre = linea[1];
                        telefono = Convert.ToInt32(linea[2]);
                        direccion = linea[3];
                        fecha = Convert.ToDateTime(linea[4]);
                        clien = new Cliente(dni, nombre, telefono, direccion, fecha);
                        clientes.Add(clien);
                    }
                }
                catch
                {
                    Exception ex;
                }
                finally
                {
                    sr.Close();
                    fs.Close();

                }
            }
            else
            {
                try
                {
                    fs = new FileStream(ruta, FileMode.Open, FileAccess.Read);
                    sr = new StreamReader(fs);
                    string[] linea;
                    int id;
                    //int dni;
                    DateTime entrada;
                    DateTime salida;
                    Reserva rese;
                    while (!sr.EndOfStream)
                    {
                        linea = sr.ReadLine().Split(';');
                        id = Convert.ToInt32(linea[0]);
                        entrada = Convert.ToDateTime(linea[1]);
                        salida = Convert.ToDateTime(linea[2]);
                        rese = new Reserva(id, entrada, salida);
                        reservas.Add(rese);
                    }
                }
                catch
                {
                    Exception ex;
                }
                finally
                {
                    sr.Close();
                    fs.Close();

                }
            }
        }

       


     


    }
}

