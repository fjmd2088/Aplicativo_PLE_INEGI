using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace App_PLE.Vistas
{
    public partial class Resumen : Form
    {
        public Resumen()
        {
            InitializeComponent();
        }

        private void Resumen_Load(object sender, EventArgs e)
        {
            resumen();
        }

        private void resumen()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select distinct id_datos_generales, entidad_federativa, nombre_legislatura," +
                        "inicio_funciones_legislatura,termino_funciones_legislatura" +
                        " from" +
                        " TR_DATOS_GENERALES";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgvResumen.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al llenar el ComboBox: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }
            }
        }



        private void btnAgregarNuevoRegistro_Click(object sender, EventArgs e)
        {
            // FormDatosGenerales frmDatosGenerales  = new FormDatosGenerales();

            // Form1 frm = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            // frm.AbrirFormInPanel(frmDatosGenerales);

            //**********************

            FormRegistros frmRegistros = new FormRegistros();

            // Crear instancias de los formularios secundarios
            frmRegistros.ShowDialog();


        }

        private void btnEditarRegistro_Click(object sender, EventArgs e)
        {
            /*
            if (dgvResumen.SelectedRows.Count > 0)
            {
                // Se obtiene el ID del registro
                DataGridViewRow filaSeleccionada = dgvResumen.SelectedRows[0];

                string id_registro = filaSeleccionada.Cells["id_datos_generales"].Value.ToString();

                // crear instancia al formulario
                FormRegistrosPrecargado frmRegistros = new FormRegistrosPrecargado();

                frmRegistros.CargarDatos(id_registro);

                frmRegistros.Show();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un registro.");
            }
            */
        }
    }
}
