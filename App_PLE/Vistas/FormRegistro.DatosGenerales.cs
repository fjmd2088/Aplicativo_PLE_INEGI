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
    public partial class FormRegistros : Form
    {
        private void cmb_Entidad()
        {
            try
            {
                // comando de sql
                string query = "select nom_ent from TC_AGEEM group by nom_ent";

                // Utilizar un DataReader para obtener los datos
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, _connection);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                cmb_entidad_federativa.DataSource = dataTable;
                cmb_entidad_federativa.DisplayMember = "nom_ent";

                cmb_entidad_federativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmb_entidad_federativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                cmb_entidad_federativa.DropDownStyle = ComboBoxStyle.DropDown;
                cmb_entidad_federativa.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar el ComboBox: " + ex.Message);
            }
        }
    }
}
