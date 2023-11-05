using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Runtime.ConstrainedExecution;
using System.Drawing.Printing;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using System.Threading;
using Lab2Tp2.clases;
using EjercicioHotel;

namespace EjercicioHotel
{
    public partial class Form1 : Form
    {
        private Empresa miEmpresa;

        VerReservas editar;
        VentanadeReservas ven;

        public Form1()
        {

            InitializeComponent();

            miEmpresa = new Empresa();


        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            lbhora.Text = DateTime.Now.ToString("hh:mm:ss");
            lbfecha.Text = DateTime.Now.ToLongDateString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            FVerReservas inicio = new FVerReservas();
            //inicio.ShowDialog();


            string ruta = Application.StartupPath;
            string archivo = Path.Combine(ruta, "datosempresa.dat");
            FileStream fs = null;
            try
            {
                if (File.Exists(archivo))
                {
                    fs = new FileStream(archivo, FileMode.Open, FileAccess.Read);
                    BinaryFormatter bf = new BinaryFormatter();
                    miEmpresa = (Empresa)bf.Deserialize(fs);
                    //miEmpresa.usuarios = new List<Usuario>(); //PARA AGREGAR DE INICIO EL USUARIO, DESPUES YA NO SE NECESITA
                }
                else
                {
                    miEmpresa = new Empresa();
                }
            }
            catch
            {
                MessageBox.Show("Error De Deserializacion");
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    RefrescarGrilla();
                }
            }


        }

    


        private void agregarPropiedadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ultimo = miEmpresa.Propiedades.Count;
            int n = ultimo + 1;
            VagrePropiedad vAgProp = new VagrePropiedad();
            vAgProp.Text = "Propiedad";
            vAgProp.label9.Visible = false;
            vAgProp.groupBox3.Visible = true;
            vAgProp.groupBox6.Visible = true;
            vAgProp.label10.Visible = false;

            if (vAgProp.ShowDialog() == DialogResult.OK)
            {
                Image imagen1 = vAgProp.pictureBox1.Image;
                Image imagen2 = vAgProp.pictureBox2.Image;
                Image imagen3 = vAgProp.pictureBox3.Image;
                Image imagen4 = vAgProp.pictureBox4.Image;
                int plazas = Convert.ToInt32(vAgProp.nUDPlazas.Value);
                string direccion = vAgProp.tbDirecion.Text;
                string localidad = vAgProp.tbLocalidad.Text;
                double valorBase = Convert.ToDouble(vAgProp.tbPrecioBase.Text);
                int cant1plaza = Convert.ToInt32(vAgProp.nUD1plaza.Value);
                int cant2plazas = Convert.ToInt32(vAgProp.nUD2Plazas.Value);
                int cant3plazas = Convert.ToInt32(vAgProp.nUD3Plazas.Value);
                string nombre = vAgProp.tbnombre.Text;
                bool[] servicios = new bool[6];
                if (vAgProp.cbCochera.Checked)
                {
                    servicios[0] = true;
                }
                if (vAgProp.cbPileta.Checked)
                {
                    servicios[1] = true;
                }
                if (vAgProp.cbWifi.Checked)
                {
                    servicios[2] = true;
                }
                if (vAgProp.cbLimpieza.Checked)
                {
                    servicios[3] = true;
                }
                if (vAgProp.cbDesayuno.Checked)
                {
                    servicios[4] = true;
                }
                if (vAgProp.cbMascotas.Checked)
                {
                    servicios[5] = true;
                }
                if (vAgProp.rBCasa.Checked)
                {
                    int diasminimos = Convert.ToInt32(vAgProp.tbdiasminimos.Text);
                    Propiedad unaPropiedad = new Casa(n, nombre, plazas, direccion, localidad, valorBase, diasminimos, servicios, imagen1, imagen2, imagen3, imagen4);
                    miEmpresa.AgregarPropiedad(unaPropiedad);

                }
                else if (vAgProp.rBHotel.Checked)
                {
                    Propiedad unaPropiedad = new Hotel(n, nombre, valorBase, direccion, localidad, cant1plaza, cant2plazas, cant3plazas, servicios, imagen1, imagen2, imagen3, imagen4);
                    miEmpresa.AgregarPropiedad(unaPropiedad);
                }

            }
            miEmpresa.Propiedades.Sort();
            RefrescarGrilla();

        }


        private void bBuscar_Click(object sender, EventArgs e)
        {

            Comparador comp;
            miEmpresa.Casas.Sort();
            miEmpresa.Hoteles.Sort();


            List<Propiedad.TipoServicios> servicios = new List<Propiedad.TipoServicios>();
            string localidad = tbLocalidad.Text;
            int plazas = Convert.ToInt32(numPlazas.Value);
            int precioMin = Convert.ToInt32(nudMinValue.Value);
            int precioMax = Convert.ToInt32(nudMaxValue.Value);


            if (cbCochera.Checked)
            {
                servicios.Add((Propiedad.TipoServicios)0);
            }
            if (cbPileta.Checked)
            {
                servicios.Add((Propiedad.TipoServicios)1);
            }
            if (cbWifi.Checked)
            {
                servicios.Add((Propiedad.TipoServicios)2);
            }
            if (cbLimpieza.Checked)
            {
                servicios.Add((Propiedad.TipoServicios)3);
            }
            if (cbDesayuno.Checked)
            {
                servicios.Add((Propiedad.TipoServicios)4);
            }
            if (cbMascotas.Checked)
            {
                servicios.Add((Propiedad.TipoServicios)5);
            }
            comp = new Comparador(servicios, precioMin, precioMax, plazas, localidad);
            List<Propiedad> parcial = new List<Propiedad>();
            parcial = miEmpresa.Filtrar(comp);
            dataGridView1.Rows.Clear();
            foreach (Propiedad prop in parcial)
            {

                if (prop is Casa)
                {

                    bool cochera = prop.VerServicios.Contains(Propiedad.TipoServicios.Cochera);
                    bool pileta = prop.VerServicios.Contains(Propiedad.TipoServicios.Pileta);
                    bool wifi = prop.VerServicios.Contains(Propiedad.TipoServicios.WiFi);
                    bool limpieza = prop.VerServicios.Contains(Propiedad.TipoServicios.Limpieza);
                    bool desayuno = prop.VerServicios.Contains(Propiedad.TipoServicios.Desayuno);
                    bool mascotas = prop.VerServicios.Contains(Propiedad.TipoServicios.PetFriendly);
                    if (((Casa)prop).Plazas == 1)
                    { dataGridView1.Rows.Add("Casa", prop.Nombre, prop.Codigo, "✔", "", "", prop.Direccion, prop.Loc, cochera, pileta, wifi, limpieza, desayuno, mascotas, prop.ValorBase); }

                    if (((Casa)prop).Plazas == 2)
                    { dataGridView1.Rows.Add("Casa", prop.Nombre, prop.Codigo, "", "✔", "", prop.Direccion, prop.Loc, cochera, pileta, wifi, limpieza, desayuno, mascotas, prop.ValorBase); }
                    if (((Casa)prop).Plazas == 3)
                    { dataGridView1.Rows.Add("Casa", prop.Nombre, prop.Codigo, "", "", "✔", prop.Direccion, prop.Loc, cochera, pileta, wifi, limpieza, desayuno, mascotas, prop.ValorBase); }


                }
                else if (prop is Hotel)
                {
                    bool cochera = prop.VerServicios.Contains(Propiedad.TipoServicios.Cochera);
                    bool pileta = prop.VerServicios.Contains(Propiedad.TipoServicios.Pileta);
                    bool wifi = prop.VerServicios.Contains(Propiedad.TipoServicios.WiFi);
                    bool limpieza = prop.VerServicios.Contains(Propiedad.TipoServicios.Limpieza);
                    bool desayuno = prop.VerServicios.Contains(Propiedad.TipoServicios.Desayuno);
                    bool mascotas = prop.VerServicios.Contains(Propiedad.TipoServicios.PetFriendly);
                    dataGridView1.Rows.Add("Hotel", prop.Nombre, prop.Codigo, ((Hotel)prop).Unaplaza, ((Hotel)prop).Dosplazas, ((Hotel)prop).Tresplazas, ((Hotel)prop).Direccion, ((Hotel)prop).Loc, cochera, pileta, wifi, limpieza, desayuno, mascotas, ((Hotel)prop).ValorBase);
                }
            }
        }

        private void cargarPropiedadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream fs;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                miEmpresa.ProcesarPropiedades(fs);

            }
            RefrescarGrilla();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            List<Cliente> listaauxiliar = new List<Cliente>();
            Cliente auxiliar = null;
            int cantPlazas = 0;

            int numero = (Int32)dataGridView1[2, e.RowIndex].Value;
            Propiedad prop = null;
            prop = miEmpresa.VerPropiedad(numero);

            ven = new VentanadeReservas(this.miEmpresa.Reservas, prop);

            ven.Text = "Reservar";
            ven.pictureBox1.Image = prop.Imagen1;
            ven.pictureBox2.Image = prop.Imagen2;
            ven.pictureBox3.Image = prop.Imagen3;
            ven.pictureBox4.Image = prop.Imagen4;

            if (prop is Casa)
            {
                string nom = ((Casa)prop).Nombre;

                ven.ltipo.Text = "Casa";
                ven.lnombre.Text = nom;
                ven.llocalidad.Text = prop.Loc;
                ven.ldireccion.Text = ((Casa)prop).Direccion;
                ven.lnumerohab.Visible = false;
                ven.nCantPlazas.Visible = false;

            }
            if (prop is Hotel)
            {
                string nom = ((Hotel)prop).Nombre;

                ven.ltipo.Text = "Hotel";
                ven.lnombre.Text = nom;
                ven.llocalidad.Text = prop.Loc;
                ven.ldireccion.Text = ((Hotel)prop).Direccion;
                ven.lnumerohab.Visible = true;
                ven.nCantPlazas.Visible = true;
                cantPlazas = (Int32)ven.nCantPlazas.Value;
            }

            string nombre;
            int dni;
            int tel;
            string dir;
            DateTime fecha;
            int cantInqui = 0;
            DialogResult var = ven.ShowDialog();
            if (var == DialogResult.Ignore)
            {
                cantInqui = Convert.ToInt32(ven.cbCantidadInquilinos.SelectedIndex) + 1;
                for (int i = 0; i < cantInqui; i++)
                {
                    Vinicio ventClient = new Vinicio();
                    ventClient.ShowDialog();
                    nombre = ventClient.tbNombre.Text;
                    dni = Convert.ToInt32(ventClient.tbDni.Text);
                    tel = Convert.ToInt32(ventClient.tbTelefono.Text);
                    dir = ventClient.tbDireccion.Text;
                    fecha = Convert.ToDateTime(ventClient.dateTimePicker1.Value);
                    auxiliar = new Cliente(dni, nombre, tel, dir, fecha);
                    listaauxiliar.Add(auxiliar);
                }
            }
            var = ven.ShowDialog();

            if (var == DialogResult.OK)
            {
                int dias = 0;
                DateTime entrada = new DateTime();
                double precio = 0;
                try
                {
                    dias = Convert.ToInt32(ven.numericUpDown1.Value);
                    entrada = ven.monthCalendar1.SelectionStart;
                    precio = ven.Costo;
                    cantPlazas = Convert.ToInt32(ven.nCantPlazas.Text);

                    if (cantInqui == 1)
                    {

                        //nombre = ven.tbNombre.Text;
                        //dni = Convert.ToInt32(ven.tbDni.Text);
                        //tel = Convert.ToInt32(ven.tbTelefono.Text);
                        //dir = ven.tbDireccion.Text;
                        //fecha = ven.dateTimePicker1.Value;


                        //Cliente unCliente = new Cliente(dni, nom, tel, dir, fecha);
                        miEmpresa.AgregarCliente(auxiliar);

                        if (prop is Casa)
                        {
                            miEmpresa.ReservarCasa(auxiliar, entrada, dias, prop, precio);
                        }
                        else if (prop is Hotel)
                        {
                            miEmpresa.ReservarHabitacion(auxiliar, entrada, dias, prop, cantPlazas, precio);
                        }
                    }
                    if (cantInqui > 1)
                    {
                        foreach (Cliente c in listaauxiliar)
                        {
                            miEmpresa.AgregarCliente(c);
                        }
                        if (prop is Casa)
                        {
                            miEmpresa.ReservarCasa(listaauxiliar, entrada, dias, prop, precio);
                        }
                        else if (prop is Hotel)
                        {
                            miEmpresa.ReservarHabitacion(listaauxiliar, entrada, dias, prop, cantPlazas, precio);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No fue posible reservar el alojamiento. Intente nuevamente y corrobore que haya completado todos los campos correctamente.");
                }

            }
        }
        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ruta = Application.StartupPath;
            string n = Path.Combine(ruta, "datosempresa.dat");
            FileStream fs = null;


            try
            {
                fs = new FileStream(n, FileMode.Create, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, miEmpresa);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al serializar");
            }
            finally
            {

                fs.Close();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Realmente desea salir de la Aplicacion?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string ruta = Application.StartupPath;
                string n = Path.Combine(ruta, "datosempresa.dat");
                FileStream fs = null;


                try
                {
                    fs = new FileStream(n, FileMode.Create, FileAccess.Write);
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, miEmpresa);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al serializar");
                }
                finally
                {

                    fs.Close();
                }
            }
            else
            {
                e.Cancel = true;
            }

        }


        private void editarPropiedadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            miEmpresa.Propiedades.Sort();
            VerReservas editar = new VerReservas();
            editar.Text = ("Propiedades");
            editar.btImprimir.Visible = false;
            editar.dataGridView1.ColumnCount = 6;
            editar.dataGridView1.Columns[0].HeaderCell.Value = "Tipo Propiedad";
            editar.dataGridView1.Columns[1].HeaderCell.Value = "Nombre Prop.";
            editar.dataGridView1.Columns[2].HeaderCell.Value = "Numero";
            editar.dataGridView1.Columns[3].HeaderCell.Value = "Precio Base";
            editar.dataGridView1.Columns[4].HeaderCell.Value = "Direccion";
            editar.dataGridView1.Columns[5].HeaderCell.Value = "Localidad";
            editar.dataGridView1.Rows.Clear();
            foreach (Propiedad p in miEmpresa.Propiedades)
            {
                if (p is Casa)
                {
                    editar.dataGridView1.Rows.Add("Casa", p.Nombre, p.Codigo, p.ValorBase, p.Direccion, p.Loc);

                }
                if (p is Hotel)
                {
                    editar.dataGridView1.Rows.Add("Hotel", p.Nombre, p.Codigo, p.ValorBase, p.Direccion, p.Loc);
                }
            }
            DialogResult var = editar.ShowDialog();
            if (editar.IndiceDG != -1)
            {
                Propiedad p = miEmpresa.Propiedades[editar.IndiceDG];


                if (var == DialogResult.OK)
                {
                    for (int i = 0; i < miEmpresa.Reservas.Count; i++)
                    {
                        if (miEmpresa.Reservas[i].UnaPropiedad == p)
                        {
                            miEmpresa.Reservas.RemoveAt(i);
                        }
                    }
                    miEmpresa.Propiedades.Remove(p);
                    if (p is Casa/*miEmpresa.Propiedades[editar.VerIndice] is Casa*/)
                    {
                        miEmpresa.Casas.Remove((Casa)p);

                    }
                    if (p is Hotel/*miEmpresa.Propiedades[editar.VerIndice] is Hotel*/)
                    {
                        miEmpresa.Hoteles.Remove((Hotel)p);

                    }
                    editar.dataGridView1.Rows.RemoveAt(editar.IndiceDG);
                }
                else if (var == DialogResult.Yes)
                {
                    VagrePropiedad vN = new VagrePropiedad();
                    vN.tbDirecion.ReadOnly = true;
                    vN.tbLocalidad.ReadOnly = true;
                    vN.nUDPlazas.ReadOnly = true;
                    vN.tbnombre.Location = new Point(286, 154);//solo de modificar
                    vN.groupBox5.Controls.Add(vN.tbnombre);
                    vN.tbnombre.Text = miEmpresa.Propiedades[editar.IndiceDG].Nombre;
                    vN.label10.Visible = true;//solo de modificar
                    vN.groupBox3.Visible = false;//solo de modificar
                    vN.label9.Visible = true;//solo de modificar                    
                    vN.label9.Text = miEmpresa.Propiedades[editar.IndiceDG].Nombre;//solo de modificar                 
                    if (miEmpresa.Propiedades[editar.IndiceDG] is Casa)
                    {
                        vN.groupBox2.Visible = false;
                        vN.nUDPlazas.Value = ((Casa)miEmpresa.Propiedades[editar.IndiceDG]).Plazas;
                        vN.tbdiasminimos.Text = ((Casa)miEmpresa.Propiedades[editar.IndiceDG]).DiasMinimos.ToString();
                    }
                    else if (miEmpresa.Propiedades[editar.IndiceDG] is Hotel)
                    {
                        vN.nUDPlazas.Visible = false;
                        vN.label2.Visible = false;
                        vN.nUD1plaza.Value = ((Hotel)miEmpresa.Propiedades[editar.IndiceDG]).Unaplaza;
                        vN.nUD2Plazas.Value = ((Hotel)miEmpresa.Propiedades[editar.IndiceDG]).Dosplazas;
                        vN.nUD3Plazas.Value = ((Hotel)miEmpresa.Propiedades[editar.IndiceDG]).Tresplazas;
                        vN.tbdiasminimos.Visible = false;
                        vN.label8.Visible = false;

                    }
                    vN.tbLocalidad.Text = miEmpresa.Propiedades[editar.IndiceDG].Loc;
                    vN.tbDirecion.Text = miEmpresa.Propiedades[editar.IndiceDG].Direccion;
                    vN.tbPrecioBase.Text = miEmpresa.Propiedades[editar.IndiceDG].ValorBase.ToString();
                    vN.groupBox6.Visible = false;
                    vN.Size = new Size(551, 703);//solo de modificar, cambiar en agregar;
                    vN.Text = "Editar propiedad";
                    vN.btAgregarProp.Text = "Modificar";
                    bool cochera = miEmpresa.Propiedades[editar.IndiceDG].VerServicios.Contains(Propiedad.TipoServicios.Cochera);
                    bool pileta = miEmpresa.Propiedades[editar.IndiceDG].VerServicios.Contains(Propiedad.TipoServicios.Pileta);
                    bool wifi = miEmpresa.Propiedades[editar.IndiceDG].VerServicios.Contains(Propiedad.TipoServicios.WiFi);
                    bool limpieza = miEmpresa.Propiedades[editar.IndiceDG].VerServicios.Contains(Propiedad.TipoServicios.Limpieza);
                    bool desayuno = miEmpresa.Propiedades[editar.IndiceDG].VerServicios.Contains(Propiedad.TipoServicios.Desayuno);
                    bool mascotas = miEmpresa.Propiedades[editar.IndiceDG].VerServicios.Contains(Propiedad.TipoServicios.PetFriendly);
                    if (cochera) vN.cbCochera.Checked = true;
                    if (pileta) vN.cbPileta.Checked = true;
                    if (wifi) vN.cbWifi.Checked = true;
                    if (limpieza) vN.cbLimpieza.Checked = true;
                    if (desayuno) vN.cbDesayuno.Checked = true;
                    if (mascotas) vN.cbMascotas.Checked = true;
                    DialogResult venMod = vN.ShowDialog();
                    if (venMod == DialogResult.OK)
                    {

                        if (miEmpresa.Propiedades[editar.IndiceDG] is Casa)
                        {
                            ((Casa)miEmpresa.Propiedades[editar.IndiceDG]).DiasMinimos = Convert.ToInt32(vN.tbdiasminimos.Text);
                        }
                        miEmpresa.Propiedades[editar.IndiceDG].VerServicios.Clear();
                        bool[] servicios = new bool[6];
                        if (vN.cbCochera.Checked == true) servicios[0] = true;
                        if (vN.cbPileta.Checked == true) servicios[1] = true;
                        if (vN.cbWifi.Checked == true) servicios[2] = true;
                        if (vN.cbLimpieza.Checked == true) servicios[3] = true;
                        if (vN.cbDesayuno.Checked == true) servicios[4] = true;
                        if (vN.cbMascotas.Checked == true) servicios[5] = true;
                        miEmpresa.Propiedades[editar.IndiceDG].AgregarServicios(servicios);
                        miEmpresa.Propiedades[editar.IndiceDG].Nombre = vN.tbnombre.Text;
                        miEmpresa.Propiedades[editar.IndiceDG].ValorBase = Convert.ToDouble(vN.tbPrecioBase.Text);
                    }

                }
            }
            else if (var != DialogResult.Cancel)
            {
                MessageBox.Show("Debe seleccionar una propiedad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                editar.Close();
            }
            RefrescarGrilla();
        }

        public void RefrescarGrilla()
        {

            dataGridView1.Rows.Clear();
            miEmpresa.Propiedades.Sort();
            cbCochera.Checked = false;
            cbPileta.Checked = false;
            cbWifi.Checked = false;
            cbLimpieza.Checked = false;
            cbMascotas.Checked = false;
            cbDesayuno.Checked = false;
            tbLocalidad.Text = "";
            numPlazas.Text = "";
            nudMinValue.Text = "";
            nudMaxValue.Text = "";
            foreach (Propiedad p in miEmpresa.Propiedades)
            {
                bool cochera = p.VerServicios.Contains(Propiedad.TipoServicios.Cochera);
                bool pileta = p.VerServicios.Contains(Propiedad.TipoServicios.Pileta);
                bool wifi = p.VerServicios.Contains(Propiedad.TipoServicios.WiFi);
                bool limpieza = p.VerServicios.Contains(Propiedad.TipoServicios.Limpieza);
                bool desayuno = p.VerServicios.Contains(Propiedad.TipoServicios.Desayuno);
                bool mascotas = p.VerServicios.Contains(Propiedad.TipoServicios.PetFriendly);
                if (p is Hotel)
                {
                    dataGridView1.Rows.Add("Hotel", p.Nombre, p.Codigo, ((Hotel)p).Unaplaza, ((Hotel)p).Dosplazas, ((Hotel)p).Tresplazas, p.Direccion, p.Loc, cochera, pileta, wifi, limpieza, desayuno, mascotas, p.ValorBase);
                }
                if (p is Casa)
                {
                    if (((Casa)p).Plazas == 1)
                    {
                        dataGridView1.Rows.Add("Casa", p.Nombre, p.Codigo, "✔", "", "", p.Direccion, p.Loc, cochera, pileta, wifi, limpieza, desayuno, mascotas, p.ValorBase);
                    }
                    if (((Casa)p).Plazas == 2)
                    {
                        dataGridView1.Rows.Add("Casa", p.Nombre, p.Codigo, "", "✔", "", p.Direccion, p.Loc, cochera, pileta, wifi, limpieza, desayuno, mascotas, p.ValorBase);
                    }
                    if (((Casa)p).Plazas == 3)
                    {
                        dataGridView1.Rows.Add("Casa", p.Nombre, p.Codigo, "", "", "✔", p.Direccion, p.Loc, cochera, pileta, wifi, limpieza, desayuno, mascotas, p.ValorBase);
                    }
                }
            }
        }

        private void bReiniciar_Click(object sender, EventArgs e)
        {

            RefrescarGrilla();

        }

        private void eliminarClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vinicio vclien = new Vinicio();
            if (vclien.ShowDialog() == DialogResult.OK)
            {
                string nom = vclien.tbNombre.Text;
                int dni = Convert.ToInt32(vclien.tbDni.Text);
                int tel = Convert.ToInt32(vclien.tbTelefono.Text);
                string dir = vclien.tbDireccion.Text;
                DateTime fecha = DateTimePicker.MinimumDateTime;
                Cliente uncli = new Cliente(dni, nom, tel, dir, fecha);
                miEmpresa.AgregarCliente(uncli);

            }
        }

        private void verClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            miEmpresa.Clientes.Sort();
            VerReservas clients = new VerReservas();
            clients.Text = ("Clientes");
            clients.btImprimir.Visible = false;
            clients.btImportar.Visible = true;
            clients.btExportar.Visible = true;
            clients.dataGridView1.ColumnCount = 5;
            clients.dataGridView1.Columns[0].HeaderCell.Value = "DNI";
            clients.dataGridView1.Columns[1].HeaderCell.Value = "Nombre";
            clients.dataGridView1.Columns[2].HeaderCell.Value = "Telefono";
            clients.dataGridView1.Columns[3].HeaderCell.Value = "Direccion";
            clients.dataGridView1.Columns[4].HeaderCell.Value = "Fecha Nacimiento";
            clients.dataGridView1.Rows.Clear();

            foreach (Cliente c in miEmpresa.Clientes)
            {
                clients.dataGridView1.Rows.Add(c.Dni, c.Nombre, c.Telefono, c.Direccion, c.FechaNacimiento.ToShortDateString());
            }

            DialogResult resul = clients.ShowDialog();


            if (resul == DialogResult.OK)
            {
                int indice = clients.IndiceDG;
                miEmpresa.Clientes.RemoveAt(indice);
            }
            else if (resul == DialogResult.Yes)
            {
                int indice = clients.IndiceDG;
                Cliente c = miEmpresa.Clientes[indice];
                int dni = c.Dni;
                string nombre = c.Nombre;
                int tel = c.Telefono;
                string dir = c.Direccion;
                DateTime fecha = c.FechaNacimiento;
                Vinicio cliente = new Vinicio();
                cliente.tbDni.Text = dni.ToString();
                cliente.tbNombre.Text = nombre;
                cliente.tbTelefono.Text = tel.ToString();
                cliente.tbDireccion.Text = dir;
                cliente.dateTimePicker1.Value = fecha;
                cliente.bnAgregar.Text = "Modificar";

                if (cliente.ShowDialog() == DialogResult.OK)
                {
                    c.Dni = Convert.ToInt32(cliente.tbDni.Text);
                    c.Nombre = cliente.tbNombre.Text;
                    c.Telefono = Convert.ToInt32(cliente.tbTelefono.Text);
                    c.Direccion = cliente.tbDireccion.Text;
                    c.FechaNacimiento = cliente.dateTimePicker1.Value;
                    miEmpresa.Clientes[indice] = c;
                }
            }
            else if (resul == DialogResult.Retry)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveFileDialog1.FileName;
                    miEmpresa.Exportar(ruta, 1);
                }
            }
            else if (resul == DialogResult.Ignore)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string ruta = openFileDialog1.FileName;
                    miEmpresa.Importar(ruta, 1);
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int ultimo = miEmpresa.Propiedades.Count;
            int n = ultimo + 1;
            VagrePropiedad vAgProp = new VagrePropiedad();
            vAgProp.Text = "Propiedad";
            vAgProp.label9.Visible = false;
            vAgProp.groupBox3.Visible = true;
            vAgProp.groupBox6.Visible = true;
            vAgProp.label10.Visible = false;

            if (vAgProp.ShowDialog() == DialogResult.OK)
            {
                Image imagen1 = vAgProp.pictureBox1.Image;
                Image imagen2 = vAgProp.pictureBox2.Image;
                Image imagen3 = vAgProp.pictureBox3.Image;
                Image imagen4 = vAgProp.pictureBox4.Image;
                int plazas = Convert.ToInt32(vAgProp.nUDPlazas.Value);
                string direccion = vAgProp.tbDirecion.Text;
                string localidad = vAgProp.tbLocalidad.Text;
                double valorBase = Convert.ToDouble(vAgProp.tbPrecioBase.Text);
                int cant1plaza = Convert.ToInt32(vAgProp.nUD1plaza.Value);
                int cant2plazas = Convert.ToInt32(vAgProp.nUD2Plazas.Value);
                int cant3plazas = Convert.ToInt32(vAgProp.nUD3Plazas.Value);
                string nombre = vAgProp.tbnombre.Text;
                bool[] servicios = new bool[6];
                if (vAgProp.cbCochera.Checked)
                {
                    servicios[0] = true;
                }
                if (vAgProp.cbPileta.Checked)
                {
                    servicios[1] = true;
                }
                if (vAgProp.cbWifi.Checked)
                {
                    servicios[2] = true;
                }
                if (vAgProp.cbLimpieza.Checked)
                {
                    servicios[3] = true;
                }
                if (vAgProp.cbDesayuno.Checked)
                {
                    servicios[4] = true;
                }
                if (vAgProp.cbMascotas.Checked)
                {
                    servicios[5] = true;
                }
                if (vAgProp.rBCasa.Checked)
                {
                    int diasminimos = Convert.ToInt32(vAgProp.tbdiasminimos.Text);
                    Propiedad unaPropiedad = new Casa(n, nombre, plazas, direccion, localidad, valorBase, diasminimos, servicios, imagen1, imagen2, imagen3, imagen4);
                    miEmpresa.AgregarPropiedad(unaPropiedad);

                }
                else if (vAgProp.rBHotel.Checked)
                {
                    Propiedad unaPropiedad = new Hotel(n, nombre, valorBase, direccion, localidad, cant1plaza, cant2plazas, cant3plazas, servicios, imagen1, imagen2, imagen3, imagen4);
                    miEmpresa.AgregarPropiedad(unaPropiedad);
                }

            }
            miEmpresa.Propiedades.Sort();
            RefrescarGrilla();
        }
    }
}
