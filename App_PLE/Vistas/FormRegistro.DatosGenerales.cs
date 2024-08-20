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


        private void btnAgregarPE_Click_1(object sender, EventArgs e)
        {
            // se obtienen los valores
            string periodo_reportado_pe = cmb_periodo_extraordinario_reportado.Text.Trim();
            string fecha_inicio_pe = dtp_fecha_inicio_pe.Text.Trim();
            string fecha_termino_pe = dtp_fecha_termino_pe.Text.Trim();
            string sesiones_celebradas_pe = Txt_sesiones_celebradas_pe.Text.Trim();

            if (string.IsNullOrWhiteSpace(Txt_sesiones_celebradas_pe.Text) ||
                string.IsNullOrWhiteSpace(cmb_periodo_extraordinario_reportado.Text))
            {
                MessageBox.Show("Revisar datos vacios");
            }
            else
            {
                bool respuesta = IsDuplicateRecord(cmb_periodo_extraordinario_reportado.Text.ToString());

                if (respuesta == true)
                {
                    MessageBox.Show("Dato duplicado");
                }
                else
                {
                    // Agregar una nueva fila al DataGridView
                    dgvPE.Rows.Add(periodo_reportado_pe, fecha_inicio_pe, fecha_termino_pe, sesiones_celebradas_pe);

                    Txt_sesiones_celebradas_pe.Clear();
                    dtp_fecha_inicio_pe.Value = dtp_fecha_inicio_p_rec.Value; dtp_fecha_termino_pe.Value = dtp_fecha_termino_p_rec.Value;
                }


            }
        }
    }
}
