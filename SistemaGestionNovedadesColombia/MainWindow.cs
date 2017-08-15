﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaGestionNovedadesColombia.Administracion.Parametros;
using SistemaGestionNovedadesColombia.Administracion.Usuario;
using SistemaGestionNovedadesColombia.Facturacion;
using SistemaGestionNovedadesColombia.Inventario;
using SistemaGestionNovedadesColombia.Personal;
using SistemaGestionNovedadesColombia.Proveedor;

namespace SistemaGestionNovedadesColombia
{
    public partial class MainWindow : Form
    {
        private ConexionSQL conexionSql;
        private string user;
        private bool[] permisos = new bool[12];

        public MainWindow()
        {
            InitializeComponent();
            conexionSql = new ConexionSQL();
            WindowState = FormWindowState.Maximized;
            adminMenuItem.Visible = false;
            usuarioMenuItem.Visible = false;
        }

        public void obtenerPermisos()
        {
            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("select * from USUARIO where USUARIO = '" + user + "'",
                conexionSql.getConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < permisos.Length; i++)
                {
                    permisos[i] = reader.GetBoolean(i + 3);
                }
            }
        }

        public void initComponents()
        {
            if (permisos[0])
            {
                if (!permisos[1])
                {
                    registrarCliente.Enabled = false;
                    modificarProveedor.Enabled = false;
                    eliminarCliente.Enabled = false;
                }
            }
            else
            {
                clienteMenuItem.Visible = false;
            }

            if (permisos[2])
            {
                if (!permisos[3])
                {
                    registrarProveedor.Enabled = false;
                    modificarProveedor.Enabled = false;
                }
            }
            else
            {
                proveedorMenuItem.Enabled = false;
            }

            if (permisos[4])
            {
                if (!permisos[5])
                {
                    registrarArticulo.Enabled = false;
                    modificarArticulo.Enabled = false;
                }
            }
            else
            {
                inventarioMenuItem.Visible = false;
            }

            if (permisos[6])
            {
                if (!permisos[7])
                {
                    registrarFactura.Enabled = false;
                    modificarFactura.Enabled = false;
                }
            }
            else
            {
                facturaMenuItem.Visible = false;
            }

            if (permisos[8])
            {
                if (!permisos[9])
                {
                    registrarVendedor.Enabled = false;
                    modificarVendedor.Enabled = false;
                }
            }
            else
            {
                vendedorMenuItem.Visible = false;
            }

            if (permisos[10])
            {
                if (permisos[11])
                {
                    adminMenuItem.Visible = true;
                }
            }
            else
            {
                usuarioMenuItem.Visible = true;
            }
        }

        private void registrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cliente form = new Cliente("Registrar", "");
            form.Text = "Registrar Cliente";
            form.Show();
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BusquedaCliente bc = new BusquedaCliente();
            bc.setTipo("Consultar");
            bc.Show();
        }

        private void informaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 a = new AboutBox1();
            a.Show();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BusquedaCliente bc = new BusquedaCliente();
            bc.setTipo("Eliminar");
            bc.Show();
        }

        public void setStatusBar(string user)
        {
            this.user = user;
            dateStatusLabel.Text = "Fecha: " + DateTime.Now.ToShortDateString();
            activeUserStarusLabel.Text = "Usuario: " + user;
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BusquedaCliente bc = new BusquedaCliente();
            bc.setTipo("Modificar");
            bc.Show();
        }

        private void manualUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.Combine(Directory.GetParent(Directory.GetParent(path).FullName).FullName, @"Resources\userManual.chm");
            Help.ShowHelp(this, "file://" + path);
        }

        private void registrarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Proveedor.Proveedor form = new Proveedor.Proveedor("Registrar");
            form.Text = "Registrar Proveedor";
            form.Show();
        }

        private void consultarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BusquedaProveedor bc = new BusquedaProveedor();
            bc.setTipo("Consultar");
            bc.Show();
        }

        private void modificarToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            BusquedaProveedor bc = new BusquedaProveedor();
            bc.setTipo("Modificar");
            bc.Show();
        }

        private void agregarArticuloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Producto p = new Producto("Registrar");
            p.Text = "Agregar Articulo Inventario";
            p.Show();
        }

        private void consultarToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            BusquedaArticulo ba = new BusquedaArticulo();
            ba.setTipo("Consultar");
            ba.Show();
        }

        private void modificarToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            BusquedaArticulo ba = new BusquedaArticulo();
            ba.setTipo("Modificar");
            ba.Show();
        }

        private void registrarToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Factura f = new Factura("Registrar");
            f.Text = "Registrar Factura";
            f.Show();
        }

        private void consultarPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BusquedaFactura bf = new BusquedaFactura();
            bf.setTipo("Consultar");
            bf.Show();
        }

        private void actualizarPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BusquedaFactura bf = new BusquedaFactura();
            bf.setTipo("Modificar");
            bf.Show();
        }

        private void registrarToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Personal.Personal p = new Personal.Personal("Registrar", "");
            p.Text = "Registrar Vendedor";
            p.Show();
        }

        private void modificarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BusquedaPersona bp = new BusquedaPersona();
            bp.setTipo("Modificar");
            bp.Show();
        }

        private void consultarToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            BusquedaPersona bp = new BusquedaPersona();
            bp.setTipo("Consultar");
            bp.Show();
        }

        private void registrarToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Usuario u = new Usuario("Registrar", "");
            u.Text = "Registrar Usuario";
            u.Show();
        }

        private void modificarToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            BusquedaUsuario bu = new BusquedaUsuario(user);
            bu.Show();
        }

        private void parametrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Parametros p = new Parametros();
            p.Show();
        }

        private void registrarGrupoDeTallaYColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GrupoTallaColor gtc = new GrupoTallaColor("");
            gtc.Text = "Registrar Grupo de Talla y Color";
            gtc.Show();
        }

        private void modificarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MUsuario mu = new MUsuario(user);
            mu.Show();
        }
    }
}
