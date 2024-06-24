using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices; // para generar la funcionalidad de arrastar la ventana
using App_PLE.Vistas;

namespace App_PLE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            

        }
        /*
        -------------------------------------------------- CARGA INICIAL DEL FORMULARIO PRINCIPAL ----------------------------------------------------
         */
        private void Form1_Load(object sender, EventArgs e)
        {
            AbrirFormInPanel(new Resumen());

            // ajustar el tamaño del formulario
            this.Size = new System.Drawing.Size(1300, 720); // ancho, alto
            // ajustar posicion del formulario
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // arrastrar ventana
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wpaaram, int lparam);


        private void btnSlide_Click(object sender, EventArgs e)
        {
            if (menuVertical.Width == 195)
            {
                menuVertical.Width = 67;
            }
            else
            {
                menuVertical.Width = 195;
            }
        }

        /*
        -------------------------------------------------- ICONOS ----------------------------------------------------
         */
        private void iconoCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconoMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            iconoRestaurar.Visible = true;
            iconoMaximizar.Visible = false;
        }

        private void iconoRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            iconoRestaurar.Visible = false;
            iconoMaximizar.Visible = true;
        }

        private void iconoMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        /*
         -------------------------------------------------- BARRA TITULO ----------------------------------------------------
         */
        private void barraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle,0x112,0xf012,0);
        }

        public void AbrirFormInPanel(object Formhijo)
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);

            Form fh = Formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(fh);
            this.panelContenedor.Tag = fh;
            fh.Show();
        }
        /*
        -------------------------------------------------- BOTONES ----------------------------------------------------
         */
        private void btnDG_Click(object sender, EventArgs e)
        {
            //AbrirFormInPanel(new FormDatosGenerales());
        }

        private void btnResumen_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new Resumen());
        }

        // Se carga el formulario de descarga de información
        private void btnDecargarInformacion_Click(object sender, EventArgs e)
        {
            DescargarInformacion frmRegistros = new DescargarInformacion();

            // Crear instancias de los formularios secundarios
            frmRegistros.ShowDialog();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            FormDashboard frmRegistros = new FormDashboard();

            // Crear instancias de los formularios secundarios
            frmRegistros.ShowDialog();
        }
    }
}
