using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_PLE.Vistas
{
    public partial class FormRegistros: Form
    {

        private void cmb_Tipo_CL()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_TIPO_COMISION";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_tipo_comision_legislativa.DataSource = dataTable;
                    cmb_tipo_comision_legislativa.DisplayMember = "descripcion";

                    cmb_tipo_comision_legislativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_tipo_comision_legislativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_tipo_comision_legislativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_tipo_comision_legislativa.SelectedIndex = -1; // Aquí se establece como vacío
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al llenar el ComboBox cmb_Tipo_CL: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }

            }
        }

    }
}
