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

        private long VerificarID()
        {
            // Obtener el ID desde el TextBox
            string id = txt_id_legislatura.Text.Trim(); // Asegúrate de reemplazar 'txt_id_legislatura' con el nombre real de tu TextBox

            try
            {

                // Consulta SQL para verificar la existencia del ID
                string query = "SELECT COUNT(*) FROM TR_DATOS_GENERALES WHERE id_legislatura = @id"; // Reemplaza 'TR_DATOS_GENERALES' con el nombre de tu tabla

                using (SQLiteCommand command = new SQLiteCommand(query, _connection))
                {
                    // Agregar el parámetro para la consulta SQL
                    command.Parameters.AddWithValue("@id", id);

                    // Ejecutar la consulta y obtener el resultado
                    long count = (long)command.ExecuteScalar();

                    if (count > 0)
                    {
                        // Si el ID existe en la base de datos
                        MessageBox.Show("El ID ya existe en la base de datos. Validar información.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    return count; // Retorna el conteo para uso adicional
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                MessageBox.Show($"Error al conectar a la base de datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0; // Retorna 0 en caso de error para indicar fallo en la verificación
            }
        }

        private void BtnGuardarDG_Click_1(object sender, EventArgs e)
        {

            long long_reg = VerificarID(); // se verifica si el id existe en la base de datos

            bool cv = ValidacionCampos_DG();

            if (long_reg == 0)
            {
                if (cv == true)
                {
                    DialogResult respuesta = MessageBox.Show("¿Está seguro de Guardar los datos?", "Confirmacion",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (respuesta == DialogResult.Yes) // NO HAY PERIODOS EXTRAORDINARIOS
                    {
                        string cadena = "Data Source = DB_PLE.db;Version=3;";

                        using (SQLiteConnection connection = new SQLiteConnection(cadena))
                        {
                            connection.Open();

                            // el dgv de periodos extraordinarios esta vacio
                            if (dgvPE.RowCount == 0)
                            {
                                // Insertamos los datos en la base de datos
                                string query = "INSERT INTO TR_DATOS_GENERALES (id_legislatura," +
                                    "entidad_federativa," +
                                    "agee," +
                                    "numero_legislatura," +
                                    "nombre_legislatura," +
                                    "inicio_funciones_legislatura," +
                                    "termino_funciones_legislatura," +
                                    "distritos_uninominales," +
                                    "diputaciones_plurinominales," +
                                    //"periodo_extraordinario_reportado," +
                                    "ejercicio_constitucional_informacion_reportada," +
                                    "fecha_inicio_informacion_reportada," +
                                    "fecha_termino_informacion_reportada," +
                                    "periodo_reportado," +
                                    "fecha_inicio_p," +
                                    "fecha_termino_p," +
                                    "sesiones_celebradas_p," +
                                    //"cond_celebracion_periodos_extraordinarios," +
                                    //"periodos_extraordinarios_celebrados," +
                                    //"periodo_extraordinario_reportado," +
                                    //"fecha_inicio_pe," +
                                    //"fecha_termino_pe," +
                                    //"sesiones_celebradas_pe," +
                                    //"cond_reconocimiento_iniciativa_p," +
                                    //"cond_reconocimiento_iniciativa_urgente_obvia," +
                                    //"cond_existencia_juicio_politico," +
                                    //"cond_existencia_declaracion_procedencia," +
                                    //"cond_existencia_comparecencia," +
                                    "fecha_actualizacion," +
                                    "periodo_reportado_rec," +
                                    "fecha_inicio_p_rec," +
                                    "fecha_termino_p_rec," +
                                    "sesiones_celebradas_p_rec)" +
                             "VALUES" +
                                    " (@id_legislatura," +
                                    "@entidad_federativa," +
                                    "@agee," +
                                    "@numero_legislatura," +
                                    "@nombre_legislatura," +
                                    "@inicio_funciones_legislatura," +
                                    "@termino_funciones_legislatura," +
                                    "@distritos_uninominales," +
                                    "@diputaciones_plurinominales," +
                                    //"periodo_extraordinario_reportado," +
                                    "@ejercicio_constitucional_informacion_reportada," +
                                    "@fecha_inicio_informacion_reportada," +
                                    "@fecha_termino_informacion_reportada," +
                                    "@periodo_reportado," +
                                    "@fecha_inicio_p," +
                                    "@fecha_termino_p," +
                                    "@sesiones_celebradas_p," +
                                    //"cond_celebracion_periodos_extraordinarios," +
                                    //"periodos_extraordinarios_celebrados," +
                                    //"periodo_extraordinario_reportado," +
                                    //"fecha_inicio_pe," +
                                    //"fecha_termino_pe," +
                                    //"sesiones_celebradas_pe," +
                                    //"cond_reconocimiento_iniciativa_p," +
                                    //"cond_reconocimiento_iniciativa_urgente_obvia," +
                                    //"cond_existencia_juicio_politico," +
                                    //"cond_existencia_declaracion_procedencia," +
                                    //"cond_existencia_comparecencia," +
                                    "@fecha_actualizacion," +
                                    "@periodo_reportado_rec," +
                                    "@fecha_inicio_p_rec," +
                                    "@fecha_termino_p_rec," +
                                    "@sesiones_celebradas_p_rec)";

                                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                                {
                                    // Variables individuales
                                    command.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text);
                                    command.Parameters.AddWithValue("@entidad_federativa", cmb_entidad_federativa.Text);
                                    command.Parameters.AddWithValue("@agee", txt_agee.Text);
                                    command.Parameters.AddWithValue("@numero_legislatura", cmb_numero_legislatura.Text);
                                    command.Parameters.AddWithValue("@nombre_legislatura", txt_nombre_legislatura.Text);
                                    command.Parameters.AddWithValue("@inicio_funciones_legislatura", dtp_inicio_funciones_legislatura.Text);
                                    command.Parameters.AddWithValue("@termino_funciones_legislatura", dtp_termino_funciones_legislatura.Text);
                                    command.Parameters.AddWithValue("@distritos_uninominales", Txt_distritos_uninominales.Text);
                                    command.Parameters.AddWithValue("@diputaciones_plurinominales", Txt_diputaciones_plurinominales.Text);
                                    command.Parameters.AddWithValue("@ejercicio_constitucional_informacion_reportada", cmb_ejercicio_constitucional_informacion_reportada.Text);
                                    command.Parameters.AddWithValue("@fecha_inicio_informacion_reportada", dtp_fecha_inicio_informacion_reportada.Text);
                                    command.Parameters.AddWithValue("@fecha_termino_informacion_reportada", dtp_fecha_termino_informacion_reportada.Text);
                                    command.Parameters.AddWithValue("@periodo_reportado", cmb_periodo_reportado_po.Text);
                                    command.Parameters.AddWithValue("@fecha_inicio_p", dtp_fecha_inicio_po.Text);
                                    command.Parameters.AddWithValue("@fecha_termino_p", dtp_fecha_termino_po.Text);
                                    command.Parameters.AddWithValue("@sesiones_celebradas_p", Txt_sesiones_celebradas_po.Text);
                                    command.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                    command.Parameters.AddWithValue("@periodo_reportado_rec", txt_periodo_reportado_rec.Text);
                                    command.Parameters.AddWithValue("@fecha_inicio_p_rec", dtp_fecha_inicio_p_rec.Text);
                                    command.Parameters.AddWithValue("@fecha_termino_p_rec", dtp_fecha_termino_p_rec.Text);
                                    command.Parameters.AddWithValue("@sesiones_celebradas_p_rec", txt_sesiones_celebradas_p_rec.Text);



                                    command.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                // Recorremos las filas del DataGridView
                                foreach (DataGridViewRow row in dgvPE.Rows)
                                {
                                    // Ignoramos la fila vacía al final
                                    if (!row.IsNewRow)
                                    {
                                        // Insertamos los datos en la base de datos
                                        // Insertamos los datos en la base de datos
                                        string query = "INSERT INTO TR_DATOS_GENERALES (id_legislatura," +
                                            "entidad_federativa," +
                                            "agee," +
                                            "numero_legislatura," +
                                            "nombre_legislatura," +
                                            "inicio_funciones_legislatura," +
                                            "termino_funciones_legislatura," +
                                            "distritos_uninominales," +
                                            "diputaciones_plurinominales," +
                                            //"periodo_extraordinario_reportado," +
                                            "ejercicio_constitucional_informacion_reportada," +
                                            "fecha_inicio_informacion_reportada," +
                                            "fecha_termino_informacion_reportada," +
                                            "periodo_reportado," +
                                            "fecha_inicio_p," +
                                            "fecha_termino_p," +
                                            "sesiones_celebradas_p," +
                                            //"cond_celebracion_periodos_extraordinarios," +
                                            "periodos_extraordinarios_celebrados," +
                                            "periodo_extraordinario_reportado," +
                                            "fecha_inicio_pe," +
                                            "fecha_termino_pe," +
                                            "sesiones_celebradas_pe," +
                                            //"cond_reconocimiento_iniciativa_p," +
                                            //"cond_reconocimiento_iniciativa_urgente_obvia," +
                                            //"cond_existencia_juicio_politico," +
                                            //"cond_existencia_declaracion_procedencia," +
                                            //"cond_existencia_comparecencia," +
                                            "fecha_actualizacion," +
                                            "periodo_reportado_rec," +
                                            "fecha_inicio_p_rec," +
                                            "fecha_termino_p_rec," +
                                            "sesiones_celebradas_p_rec)" +
                                     "VALUES" +
                                            " (@id_legislatura," +
                                            "@entidad_federativa," +
                                            "@agee," +
                                            "@numero_legislatura," +
                                            "@nombre_legislatura," +
                                            "@inicio_funciones_legislatura," +
                                            "@termino_funciones_legislatura," +
                                            "@distritos_uninominales," +
                                            "@diputaciones_plurinominales," +
                                            //"periodo_extraordinario_reportado," +
                                            "@ejercicio_constitucional_informacion_reportada," +
                                            "@fecha_inicio_informacion_reportada," +
                                            "@fecha_termino_informacion_reportada," +
                                            "@periodo_reportado," +
                                            "@fecha_inicio_p," +
                                            "@fecha_termino_p," +
                                            "@sesiones_celebradas_p," +
                                            //"cond_celebracion_periodos_extraordinarios," +
                                            "@periodos_extraordinarios_celebrados," +
                                            "@periodo_extraordinario_reportado," +
                                            "@fecha_inicio_pe," +
                                            "@fecha_termino_pe," +
                                            "@sesiones_celebradas_pe," +
                                            //"cond_reconocimiento_iniciativa_p," +
                                            //"cond_reconocimiento_iniciativa_urgente_obvia," +
                                            //"cond_existencia_juicio_politico," +
                                            //"cond_existencia_declaracion_procedencia," +
                                            //"cond_existencia_comparecencia," +
                                            "@fecha_actualizacion," +
                                            "@periodo_reportado_rec," +
                                    "@fecha_inicio_p_rec," +
                                    "@fecha_termino_p_rec," +
                                    "@sesiones_celebradas_p_rec)";

                                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                                        {
                                            // Variables individuales
                                            command.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text);
                                            command.Parameters.AddWithValue("@entidad_federativa", cmb_entidad_federativa.Text);
                                            command.Parameters.AddWithValue("@agee", txt_agee.Text);
                                            command.Parameters.AddWithValue("@numero_legislatura", cmb_numero_legislatura.Text);
                                            command.Parameters.AddWithValue("@nombre_legislatura", txt_nombre_legislatura.Text);
                                            command.Parameters.AddWithValue("@inicio_funciones_legislatura", dtp_inicio_funciones_legislatura.Text);
                                            command.Parameters.AddWithValue("@termino_funciones_legislatura", dtp_termino_funciones_legislatura.Text);
                                            command.Parameters.AddWithValue("@distritos_uninominales", Txt_distritos_uninominales.Text);
                                            command.Parameters.AddWithValue("@diputaciones_plurinominales", Txt_diputaciones_plurinominales.Text);
                                            command.Parameters.AddWithValue("@ejercicio_constitucional_informacion_reportada", cmb_ejercicio_constitucional_informacion_reportada.Text);
                                            command.Parameters.AddWithValue("@fecha_inicio_informacion_reportada", dtp_fecha_inicio_informacion_reportada.Text);
                                            command.Parameters.AddWithValue("@fecha_termino_informacion_reportada", dtp_fecha_termino_informacion_reportada.Text);
                                            command.Parameters.AddWithValue("@periodo_reportado", cmb_periodo_reportado_po.Text);
                                            command.Parameters.AddWithValue("@fecha_inicio_p", dtp_fecha_inicio_po.Text);
                                            command.Parameters.AddWithValue("@fecha_termino_p", dtp_fecha_termino_po.Text);
                                            command.Parameters.AddWithValue("@sesiones_celebradas_p", Txt_sesiones_celebradas_po.Text);
                                            command.Parameters.AddWithValue("@periodos_extraordinarios_celebrados", txt_periodos_extraordinarios_celebrados.Text);
                                            command.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                            command.Parameters.AddWithValue("@periodo_reportado_rec", txt_periodo_reportado_rec.Text);
                                            command.Parameters.AddWithValue("@fecha_inicio_p_rec", dtp_fecha_inicio_p_rec.Text);
                                            command.Parameters.AddWithValue("@fecha_termino_p_rec", dtp_fecha_termino_p_rec.Text);
                                            command.Parameters.AddWithValue("@sesiones_celebradas_p_rec", txt_sesiones_celebradas_p_rec.Text);

                                            // Variables del dgv
                                            command.Parameters.AddWithValue("@periodo_extraordinario_reportado", row.Cells["periodo_reportado_pe"].Value);
                                            command.Parameters.AddWithValue("@fecha_inicio_pe", row.Cells["fecha_inicio_pe"].Value);
                                            command.Parameters.AddWithValue("@fecha_termino_pe", row.Cells["fecha_termino_pe"].Value);
                                            command.Parameters.AddWithValue("@sesiones_celebradas_pe", row.Cells["sesiones_celebradas_pe"].Value);


                                            command.ExecuteNonQuery();
                                        }
                                    }

                                }
                            }
                            connection.Close();
                        }

                        // Se desabilitan los campos pero se queda la información.
                        txt_nombre_legislatura.Enabled = false; txt_nombre_legislatura.BackColor = Color.LightGray;
                        cmb_entidad_federativa.Enabled = false; cmb_numero_legislatura.Enabled = false;
                        dtp_fecha_inicio_informacion_reportada.Enabled = false;
                        dtp_fecha_termino_informacion_reportada.Enabled = false;
                        cmb_ejercicio_constitucional_informacion_reportada.Enabled = false;
                        Txt_distritos_uninominales.Enabled = false; Txt_distritos_uninominales.BackColor = Color.LightGray;
                        Txt_diputaciones_plurinominales.Enabled = false; Txt_diputaciones_plurinominales.BackColor = Color.LightGray;
                        txt_periodo_reportado_rec.Enabled = false; BackColor = Color.LightGray;
                        Txt_sesiones_celebradas_pe.Enabled = false; Txt_sesiones_celebradas_pe.BackColor = Color.LightGray;
                        txt_sesiones_celebradas_p_rec.Enabled = false; txt_sesiones_celebradas_p_rec.BackColor = Color.LightGray;
                        dgvPE.Enabled = false; dgvPE.BackgroundColor = Color.LightGray;
                        btnAgregarPE.Enabled = false; BtnEliminarPE.Enabled = false;
                        cmb_periodo_reportado_po.Enabled = false; cmb_periodo_reportado_po.BackColor = Color.LightGray;
                        dtp_fecha_inicio_po.Enabled = false;
                        dtp_fecha_termino_po.Enabled = false;
                        dtp_fecha_inicio_pe.Enabled = false;
                        dtp_fecha_termino_pe.Enabled = false;
                        Txt_sesiones_celebradas_po.Enabled = false; Txt_sesiones_celebradas_po.BackColor = Color.LightGray;
                        txt_periodos_extraordinarios_celebrados.Enabled = false; txt_periodos_extraordinarios_celebrados.BackColor = Color.LightGray;
                        chbPE.Enabled = false;
                        cmb_periodo_extraordinario_reportado.Enabled = false; cmb_periodo_extraordinario_reportado.BackColor = Color.LightGray;
                        dtp_inicio_funciones_legislatura.Enabled = false;
                        dtp_termino_funciones_legislatura.Enabled = false;

                        // SE HABILITAN LOS CONTROLES DE LAS PESTAÑAS
                        EnableTab(tabPageCL);
                        txt_ID_comision_legislativa.Enabled = false; txt_ID_comision_legislativa.BackColor = Color.LightGray;
                        EnableTab(tabPagePL);
                        txt_ID_persona_legisladora.Enabled = false;

                        MessageBox.Show("Datos guardados correctamente");

                        // this.Close(); //CIERRA EL FORMULARIO ACTUAL
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
            else
            {

            }


        }
        private void BtnEliminarPE_Click_1(object sender, EventArgs e)
        {
            if (dgvPE.SelectedRows.Count > 0)
            {
                dgvPE.Rows.RemoveAt(dgvPE.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }
        private void construccion_id_legislatura()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            // se obtiene cve_ent
            string valorComboBox1 = cmb_entidad_federativa.Text.ToString();
            string CVE_ENT = "";
            using (SQLiteConnection con = new SQLiteConnection(cadena))
            {
                con.Open();
                string query = "SELECT distinct cve_ent FROM TC_AGEEM WHERE nom_ent = @valorComboBox1";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@valorComboBox1", valorComboBox1);
                CVE_ENT = cmd.ExecuteScalar()?.ToString();
                con.Close();
            }

            // se obtiene periodo reportado
            string valorComboBox3 = cmb_periodo_reportado_po.Text.ToString();
            string valortxt = txt_periodo_reportado_rec.Text;
            string PR = "";

            if (valorComboBox3 == "Primer periodo ordinario" & valortxt == "Primer periodo de receso")
            {
                PR = "1_1";
            }
            else if (valorComboBox3 == "Segundo periodo ordinario" & valortxt == "Segundo periodo de receso")
            {
                PR = "2_2";
            }
            else if (valorComboBox3 == "Tercer periodo ordinario" & valortxt == "Tercer periodo de receso")
            {
                PR = "3_3";
            }

            /*
            using (SQLiteConnection con = new SQLiteConnection(cadena))
            {
                con.Open();
                string query = "SELECT distinct abr_pr FROM TC_CALENDARIO_SESIONES WHERE periodos_reportar = @valorComboBox3";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@valorComboBox3", valorComboBox3);
                PR = cmd.ExecuteScalar()?.ToString();
                con.Close();
            }
            */

            // se obtiene numero de legislatura
            string NL = cmb_numero_legislatura.Text.ToString();

            // se obtiene el ejercicio constitucional
            string valorEC = cmb_ejercicio_constitucional_informacion_reportada.Text.ToString();
            string EC = "";
            using (SQLiteConnection con = new SQLiteConnection(cadena))
            {
                con.Open();
                string query = "SELECT distinct abr_ec FROM TC_CALENDARIO_SESIONES  WHERE ejercicio_constitucional = @valorEC";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@valorEC", valorEC);
                EC = cmd.ExecuteScalar()?.ToString();
                con.Close();
            }

            // Concatenar ID

            string resultadoConcatenado = CVE_ENT + "_" + NL + "_" + EC + "_" + PR;

            // Se muestra el ID y AGEE
            txt_id_legislatura.Text = resultadoConcatenado;
            txt_agee.Text = CVE_ENT;
        }

        // cmb_entidad_federativa
        private void cmb_entidad_federativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            // CONSTRUCCION ID----------------------------------------------------------------------------------------------    
            string cadena = "Data Source = DB_PLE.db;Version=3;";
            construccion_id_legislatura();

            // SE LLENA EL COMBOBOX QUE DEPENDE DE LA ENTIDAD PARA LLENAR COMBOBO LEGISLATURA-----------------------------------
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                // Verifica que haya una selección 
                if (cmb_entidad_federativa.SelectedItem != null)
                {
                    try
                    {
                        // se obtiene el objeto DataRowView seleccionado
                        DataRowView rowView = cmb_entidad_federativa.SelectedItem as DataRowView;

                        if (rowView != null)
                        {
                            // Se obtiene el valor de nom_ent de la tabla TC_AGEEM
                            string entidad_federativa = rowView["nom_ent"].ToString();

                            conexion.Open();

                            // Consulta SQL para obtener datos del cmb de entidad federativa y extraer la legislatura------------------------------
                            string query = "select distinct legislatura from TC_CALENDARIO_SESIONES WHERE entidad = @entidad_federativa";
                            using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                            {
                                cmd.Parameters.AddWithValue("@entidad_federativa", entidad_federativa);

                                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                                DataTable table = new DataTable();
                                adapter.Fill(table);

                                cmb_numero_legislatura.DisplayMember = "legislatura";
                                cmb_numero_legislatura.ValueMember = "legislatura";
                                cmb_numero_legislatura.DataSource = table;

                                cmb_numero_legislatura.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                                cmb_numero_legislatura.AutoCompleteSource = AutoCompleteSource.ListItems;

                                cmb_numero_legislatura.DropDownStyle = ComboBoxStyle.DropDown;
                            }

                            // Consulta SQL para obtener datos del cmb de entidad federativa y extraer inicio legislatura------------------------------
                            string query2 = "select distinct inicio_legislatura from TC_CALENDARIO_SESIONES WHERE entidad = @entidad_federativa";
                            using (SQLiteCommand cmd2 = new SQLiteCommand(query2, conexion))
                            {
                                cmd2.Parameters.AddWithValue("@entidad_federativa", entidad_federativa);

                                object resultado = cmd2.ExecuteScalar();

                                if (DateTime.TryParse(resultado.ToString(), out DateTime inicioLegislatura))
                                {
                                    dtp_inicio_funciones_legislatura.Value = inicioLegislatura;
                                }
                                else
                                {
                                    // Manejo de error si no se puede convertir el resultado a DateTime
                                    MessageBox.Show("No se pudo convertir el valor de inicio de legislatura a DateTime.");
                                }
                            }
                            // Consulta SQL para obtener datos del cmb de entidad federativa y extraer inicio legislatura------------------------------
                            string query3 = "select distinct fin_legislatura from TC_CALENDARIO_SESIONES WHERE entidad = @entidad_federativa";
                            using (SQLiteCommand cmd3 = new SQLiteCommand(query3, conexion))
                            {
                                cmd3.Parameters.AddWithValue("@entidad_federativa", entidad_federativa);

                                object resultado = cmd3.ExecuteScalar();

                                if (DateTime.TryParse(resultado.ToString(), out DateTime finLegislatura))
                                {
                                    dtp_termino_funciones_legislatura.Value = finLegislatura;
                                }
                                else
                                {
                                    // Manejo de error si no se puede convertir el resultado a DateTime
                                    MessageBox.Show("No se pudo convertir el valor de inicio de legislatura a DateTime.");
                                }
                            }
                            // Consulta SQL para obtener datos del cmb de entidad federativa y extraer la año legislativo------------------------------
                            string query4 = "select distinct ejercicio_constitucional from TC_CALENDARIO_SESIONES WHERE entidad = @entidad_federativa";
                            using (SQLiteCommand cmd4 = new SQLiteCommand(query4, conexion))
                            {
                                cmd4.Parameters.AddWithValue("@entidad_federativa", entidad_federativa);

                                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd4);
                                DataTable table = new DataTable();
                                adapter.Fill(table);

                                cmb_ejercicio_constitucional_informacion_reportada.DisplayMember = "ejercicio_constitucional";
                                cmb_ejercicio_constitucional_informacion_reportada.ValueMember = "ejercicio_constitucional";
                                cmb_ejercicio_constitucional_informacion_reportada.DataSource = table;

                                cmb_ejercicio_constitucional_informacion_reportada.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                                cmb_ejercicio_constitucional_informacion_reportada.AutoCompleteSource = AutoCompleteSource.ListItems;

                                cmb_ejercicio_constitucional_informacion_reportada.DropDownStyle = ComboBoxStyle.DropDown;
                            }


                            // fecha del periodo de receso inicio y fin
                            string ejerc_const = cmb_ejercicio_constitucional_informacion_reportada.Text;
                            string periodo_receso = txt_periodo_reportado_rec.Text;

                            string query5 = "select distinct inicio_pr from TC_CALENDARIO_SESIONES WHERE entidad = @entidad_federativa " +
                                "AND ejercicio_constitucional = @ejerc_const " +
                                "AND periodos_reportar = @periodo_receso;";

                            using (SQLiteCommand cmd5 = new SQLiteCommand(query5, conexion))
                            {
                                cmd5.Parameters.AddWithValue("@entidad_federativa", entidad_federativa);
                                cmd5.Parameters.AddWithValue("@ejerc_const", ejerc_const);
                                cmd5.Parameters.AddWithValue("@periodo_receso", periodo_receso);

                                object resultado = cmd5.ExecuteScalar();

                                if (resultado != null && DateTime.TryParse(resultado.ToString(), out DateTime inicioReceso))
                                {
                                    dtp_fecha_inicio_p_rec.Value = inicioReceso;
                                }
                                else
                                {

                                }
                            }

                            string query6 = "select distinct fin_pr from TC_CALENDARIO_SESIONES WHERE entidad = @entidad_federativa " +
                                "AND ejercicio_constitucional = @ejerc_const " +
                                "AND periodos_reportar = @periodo_receso;";

                            using (SQLiteCommand cmd6 = new SQLiteCommand(query6, conexion))
                            {
                                cmd6.Parameters.AddWithValue("@entidad_federativa", entidad_federativa);
                                cmd6.Parameters.AddWithValue("@ejerc_const", ejerc_const);
                                cmd6.Parameters.AddWithValue("@periodo_receso", periodo_receso);

                                object resultado = cmd6.ExecuteScalar();

                                if (resultado != null && DateTime.TryParse(resultado.ToString(), out DateTime inicioReceso))
                                {
                                    dtp_fecha_termino_p_rec.Value = inicioReceso;
                                }
                                else
                                {

                                }
                            }
                            conexion.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }

            }


        }
        private void cmb_entidad_federativa_Validating(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;

            if (comboBox != null)
            {
                // Quitar espacios en blanco del texto ingresado y convertir a minúsculas
                string cleanedText = comboBox.Text.Trim().Replace(" ", string.Empty).ToLower();

                // Permitir que el ComboBox se quede en blanco
                if (string.IsNullOrEmpty(cleanedText))
                {
                    e.Cancel = false;
                    return;
                }

                // Verificar si el texto del ComboBox coincide con alguna de las opciones
                bool isValid = false;

                foreach (DataRowView item in comboBox.Items)
                {
                    string cleanedItem = item["nom_ent"].ToString().Trim().Replace(" ", string.Empty).ToLower(); // nom_ent: nombre de la columna de la tabla
                    if (cleanedText == cleanedItem)
                    {
                        isValid = true;
                        break;
                    }
                    // Mostrar el valor actual de item (para depuración)
                    Console.WriteLine("Current item: " + item["nom_ent"]);
                    // O usar Debug.WriteLine si estás depurando
                    System.Diagnostics.Debug.WriteLine("Current item: " + item["nom_ent"]);
                }

                if (!isValid)
                {
                    // Mostrar mensaje de error
                    MessageBox.Show("Por favor, seleccione una opción válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Borrar el contenido del ComboBox
                    comboBox.Text = string.Empty;

                    // Evitar que el control pierda el foco
                    e.Cancel = true;
                }
            }
        }

        // cmb_numero_legislatura
        private void cmb_numero_legislatura_Validating(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;

            if (comboBox != null)
            {
                // Quitar espacios en blanco del texto ingresado y convertir a minúsculas
                string cleanedText = comboBox.Text.Trim().Replace(" ", string.Empty).ToLower();

                // Permitir que el ComboBox se quede en blanco
                if (string.IsNullOrEmpty(cleanedText))
                {
                    e.Cancel = false;
                    return;
                }

                // Verificar si el texto del ComboBox coincide con alguna de las opciones
                bool isValid = false;

                foreach (DataRowView item in comboBox.Items)
                {
                    string cleanedItem = item["legislatura"].ToString().Trim().Replace(" ", string.Empty).ToLower(); // nom_ent: nombre de la columna de la tabla
                    if (cleanedText == cleanedItem)
                    {
                        isValid = true;
                        break;
                    }
                    // Mostrar el valor actual de item (para depuración)
                    Console.WriteLine("Current item: " + item["legislatura"]);
                    // O usar Debug.WriteLine si estás depurando
                    System.Diagnostics.Debug.WriteLine("Current item: " + item["legislatura"]);
                }

                if (!isValid)
                {
                    // Mostrar mensaje de error
                    MessageBox.Show("Por favor, seleccione una opción válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Borrar el contenido del ComboBox
                    comboBox.Text = string.Empty;

                    // Evitar que el control pierda el foco
                    e.Cancel = true;
                }
            }
        }

        // cmb_ejercicio_constitucional_informacion_reportada
        private void cmb_ejercicio_constitucional_informacion_reportada_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            // construccion id
            construccion_id_legislatura();

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                // Verifica que haya una selección 
                if (cmb_ejercicio_constitucional_informacion_reportada.SelectedItem != null)
                {
                    try
                    {
                        // se obtiene el objeto DataRowView seleccionado
                        DataRowView rowView = cmb_ejercicio_constitucional_informacion_reportada.SelectedItem as DataRowView;
                        DataRowView rowView2 = cmb_entidad_federativa.SelectedItem as DataRowView;


                        if (rowView != null & rowView2 != null)
                        {
                            // Se obtiene el valor de ejercicio_constitucional de la tabla TC_CALENDARIO_SESIONES
                            string ec = rowView["ejercicio_constitucional"].ToString();
                            string ent = rowView2["nom_ent"].ToString();

                            conexion.Open();

                            // Consulta SQL para obtener datos del cmb de entidad federativa y extraer inicio legislatura------------------------------

                            string query2 = "select distinct inicio_ec from TC_CALENDARIO_SESIONES" +
                            " WHERE ejercicio_constitucional = @ec and entidad = @ent";
                            using (SQLiteCommand cmd2 = new SQLiteCommand(query2, conexion))
                            {
                                cmd2.Parameters.AddWithValue("@ec", ec);
                                cmd2.Parameters.AddWithValue("@ent", ent);

                                object resultado = cmd2.ExecuteScalar();

                                if (DateTime.TryParse(resultado.ToString(), out DateTime inicio_ec))
                                {
                                    dtp_fecha_inicio_informacion_reportada.Value = inicio_ec;
                                }
                                else
                                {
                                    // Manejo de error si no se puede convertir el resultado a DateTime
                                    MessageBox.Show("No se pudo convertir el valor de inicio de legislatura a DateTime.");
                                }
                            }

                            // Consulta SQL para obtener datos del cmb de entidad federativa y extraer fin legislatura------------------------------

                            string query3 = "select distinct fin_ec from TC_CALENDARIO_SESIONES WHERE ejercicio_constitucional = @ec and entidad = @ent";
                            using (SQLiteCommand cmd3 = new SQLiteCommand(query3, conexion))
                            {
                                cmd3.Parameters.AddWithValue("@ec", ec);
                                cmd3.Parameters.AddWithValue("@ent", ent);


                                object resultado = cmd3.ExecuteScalar();

                                if (DateTime.TryParse(resultado.ToString(), out DateTime fin_ec))
                                {
                                    dtp_fecha_termino_informacion_reportada.Value = fin_ec;
                                }
                                else
                                {
                                    // Manejo de error si no se puede convertir el resultado a DateTime
                                    MessageBox.Show("No se pudo convertir el valor de inicio de legislatura a DateTime.");
                                }
                            }

                            // Consulta SQL para obtener datos del cmb de entidad federativa y extraer la periodo reportado------------------------------


                            string query4 = "select  distinct periodos_reportar from TC_CALENDARIO_SESIONES WHERE ejercicio_constitucional = @ec " +
                                "AND entidad = @ent " +
                                "AND abr_pr in ('1O','2O','3O')";
                            using (SQLiteCommand cmd4 = new SQLiteCommand(query4, conexion))
                            {
                                cmd4.Parameters.AddWithValue("@ec", ec);
                                cmd4.Parameters.AddWithValue("@ent", ent);

                                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd4);
                                DataTable table = new DataTable();
                                adapter.Fill(table);

                                cmb_periodo_reportado_po.DisplayMember = "periodos_reportar";
                                cmb_periodo_reportado_po.ValueMember = "periodos_reportar";
                                cmb_periodo_reportado_po.DataSource = table;

                                cmb_periodo_reportado_po.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                                cmb_periodo_reportado_po.AutoCompleteSource = AutoCompleteSource.ListItems;
                                cmb_periodo_reportado_po.DropDownStyle = ComboBoxStyle.DropDown;

                            }

                            // fecha del periodo de receso inicio y fin
                            string ejerc_const = cmb_ejercicio_constitucional_informacion_reportada.Text;
                            string periodo_receso = txt_periodo_reportado_rec.Text;

                            string query5 = "select distinct inicio_pr from TC_CALENDARIO_SESIONES WHERE entidad = @ent " +
                                "AND ejercicio_constitucional = @ejerc_const " +
                                "AND periodos_reportar = @periodo_receso;";

                            using (SQLiteCommand cmd5 = new SQLiteCommand(query5, conexion))
                            {
                                cmd5.Parameters.AddWithValue("@ent", ent);
                                cmd5.Parameters.AddWithValue("@ejerc_const", ejerc_const);
                                cmd5.Parameters.AddWithValue("@periodo_receso", periodo_receso);

                                object resultado = cmd5.ExecuteScalar();

                                if (resultado != null && DateTime.TryParse(resultado.ToString(), out DateTime inicioReceso))
                                {
                                    dtp_fecha_inicio_p_rec.Value = inicioReceso;
                                }
                                else
                                {

                                }
                            }

                            string query6 = "select distinct fin_pr from TC_CALENDARIO_SESIONES WHERE entidad = @ent " +
                                "AND ejercicio_constitucional = @ejerc_const " +
                                "AND periodos_reportar = @periodo_receso;";

                            using (SQLiteCommand cmd6 = new SQLiteCommand(query6, conexion))
                            {
                                cmd6.Parameters.AddWithValue("@ent", ent);
                                cmd6.Parameters.AddWithValue("@ejerc_const", ejerc_const);
                                cmd6.Parameters.AddWithValue("@periodo_receso", periodo_receso);

                                object resultado = cmd6.ExecuteScalar();

                                if (resultado != null && DateTime.TryParse(resultado.ToString(), out DateTime inicioReceso))
                                {
                                    dtp_fecha_termino_p_rec.Value = inicioReceso;
                                }
                                else
                                {

                                }
                            }

                            conexion.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }

            }
        }
        private void cmb_ejercicio_constitucional_informacion_reportada_Validating(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;

            if (comboBox != null)
            {
                // Quitar espacios en blanco del texto ingresado y convertir a minúsculas
                string cleanedText = comboBox.Text.Trim().Replace(" ", string.Empty).ToLower();

                // Permitir que el ComboBox se quede en blanco
                if (string.IsNullOrEmpty(cleanedText))
                {
                    e.Cancel = false;
                    return;
                }

                // Verificar si el texto del ComboBox coincide con alguna de las opciones
                bool isValid = false;

                foreach (DataRowView item in comboBox.Items)
                {
                    string cleanedItem = item["ejercicio_constitucional"].ToString().Trim().Replace(" ", string.Empty).ToLower(); // nom_ent: nombre de la columna de la tabla
                    if (cleanedText == cleanedItem)
                    {
                        isValid = true;
                        break;
                    }
                    // Mostrar el valor actual de item (para depuración)
                    Console.WriteLine("Current item: " + item["ejercicio_constitucional"]);
                    // O usar Debug.WriteLine si estás depurando
                    System.Diagnostics.Debug.WriteLine("Current item: " + item["ejercicio_constitucional"]);
                }

                if (!isValid)
                {
                    // Mostrar mensaje de error
                    MessageBox.Show("Por favor, seleccione una opción válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Borrar el contenido del ComboBox
                    comboBox.Text = string.Empty;

                    // Evitar que el control pierda el foco
                    e.Cancel = true;
                }
            }
        }

        // cmb_periodo_reportado_po
        private void cmb_periodo_reportado_po_SelectedIndexChanged(object sender, EventArgs e)
        {

            string cadena = "Data Source = DB_PLE.db;Version=3;";

            // construccion id
            construccion_id_legislatura();

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                // Verifica que haya una selección 
                if (cmb_periodo_reportado_po.SelectedItem != null)
                {
                    try
                    {
                        // se obtiene el objeto DataRowView seleccionado
                        DataRowView rowView = cmb_periodo_reportado_po.SelectedItem as DataRowView;
                        DataRowView rowView2 = cmb_entidad_federativa.SelectedItem as DataRowView;
                        DataRowView rowView3 = cmb_ejercicio_constitucional_informacion_reportada.SelectedItem as DataRowView;


                        if (rowView != null & rowView2 != null & rowView3 != null)
                        {
                            // Se obtiene el valor de ejercicio_constitucional de la tabla TC_CALENDARIO_SESIONES
                            string pr = rowView["periodos_reportar"].ToString();
                            string ent = rowView2["nom_ent"].ToString();
                            string ec = rowView3["ejercicio_constitucional"].ToString();


                            conexion.Open();

                            // Consulta SQL para obtener datos del cmb de periodos reportar y extraer inicio pr------------------------------

                            string query1 = "select distinct inicio_pr from TC_CALENDARIO_SESIONES " +
                                "WHERE periodos_reportar = @pr and entidad = @ent and ejercicio_constitucional = @ec";
                            using (SQLiteCommand cmd1 = new SQLiteCommand(query1, conexion))
                            {
                                cmd1.Parameters.AddWithValue("@pr", pr);
                                cmd1.Parameters.AddWithValue("@ent", ent);
                                cmd1.Parameters.AddWithValue("@ec", ec);

                                object resultado = cmd1.ExecuteScalar();

                                if (DateTime.TryParse(resultado.ToString(), out DateTime inicio_pr))
                                {
                                    dtp_fecha_inicio_po.Value = inicio_pr;
                                }
                                else
                                {
                                    // Manejo de error si no se puede convertir el resultado a DateTime
                                    MessageBox.Show("No se pudo convertir el valor de inicio de legislatura a DateTime.");
                                }
                            }

                            // Consulta SQL para obtener datos del cmb de periodos reportar y extraer fin pr------------------------------

                            string query2 = "select distinct fin_pr from TC_CALENDARIO_SESIONES " +
                                "WHERE periodos_reportar = @pr and entidad = @ent and ejercicio_constitucional = @ec";
                            using (SQLiteCommand cmd2 = new SQLiteCommand(query2, conexion))
                            {
                                cmd2.Parameters.AddWithValue("@pr", pr);
                                cmd2.Parameters.AddWithValue("@ent", ent);
                                cmd2.Parameters.AddWithValue("@ec", ec);

                                object resultado = cmd2.ExecuteScalar();

                                if (DateTime.TryParse(resultado.ToString(), out DateTime inicio_pr))
                                {
                                    dtp_fecha_termino_po.Value = inicio_pr;
                                }
                                else
                                {
                                    // Manejo de error si no se puede convertir el resultado a DateTime
                                    MessageBox.Show("No se pudo convertir el valor de inicio de legislatura a DateTime.");
                                }
                            }

                            // Se asigna en el txt periodo de receso dependiendo del periodo reportado y se restringe dependiendo la entidad
                            string per_ord = cmb_periodo_reportado_po.Text;
                            string ent_rep = cmb_entidad_federativa.Text;

                            if (ent_rep == "Baja California" || ent_rep == "Jalisco")
                            {
                                txt_periodo_reportado_rec.Text = "";
                                dtp_fecha_inicio_p_rec.Enabled = false; dtp_fecha_inicio_p_rec.Value = new DateTime(1899, 9, 9);
                                dtp_fecha_termino_p_rec.Enabled = false; dtp_fecha_termino_p_rec.Value = new DateTime(1899, 9, 9);
                                txt_sesiones_celebradas_p_rec.Enabled = false; txt_sesiones_celebradas_p_rec.BackColor = Color.LightGray;
                                txt_sesiones_celebradas_p_rec.Text = "";
                                chbPE.Enabled = false;

                            }
                            else
                            {
                                dtp_fecha_inicio_p_rec.Enabled = true; dtp_fecha_inicio_p_rec.Value = new DateTime(1899, 9, 9);
                                dtp_fecha_termino_p_rec.Enabled = true; dtp_fecha_termino_p_rec.Value = new DateTime(1899, 9, 9);
                                txt_sesiones_celebradas_p_rec.Enabled = true; txt_sesiones_celebradas_p_rec.BackColor = Color.Honeydew;
                                txt_sesiones_celebradas_p_rec.Text = "";
                                chbPE.Enabled = true;

                                if (per_ord == "Primer periodo ordinario")
                                {
                                    txt_periodo_reportado_rec.Text = "Primer periodo de receso";
                                }
                                else if (per_ord == "Segundo periodo ordinario")
                                {
                                    txt_periodo_reportado_rec.Text = "Segundo periodo de receso";
                                }
                                else if (per_ord == "Tercer periodo ordinario")
                                {
                                    txt_periodo_reportado_rec.Text = "Tercer periodo de receso";
                                }
                            }




                            conexion.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }

            }

        }
        private void cmb_periodo_reportado_po_Validating(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;

            if (comboBox != null)
            {
                // Quitar espacios en blanco del texto ingresado y convertir a minúsculas
                string cleanedText = comboBox.Text.Trim().Replace(" ", string.Empty).ToLower();

                // Permitir que el ComboBox se quede en blanco
                if (string.IsNullOrEmpty(cleanedText))
                {
                    e.Cancel = false;
                    return;
                }

                // Verificar si el texto del ComboBox coincide con alguna de las opciones
                bool isValid = false;

                foreach (DataRowView item in comboBox.Items)
                {
                    string cleanedItem = item["periodos_reportar"].ToString().Trim().Replace(" ", string.Empty).ToLower(); // nom_ent: nombre de la columna de la tabla
                    if (cleanedText == cleanedItem)
                    {
                        isValid = true;
                        break;
                    }
                    // Mostrar el valor actual de item (para depuración)
                    Console.WriteLine("Current item: " + item["periodos_reportar"]);
                    // O usar Debug.WriteLine si estás depurando
                    System.Diagnostics.Debug.WriteLine("Current item: " + item["periodos_reportar"]);
                }

                if (!isValid)
                {
                    // Mostrar mensaje de error
                    MessageBox.Show("Por favor, seleccione una opción válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Borrar el contenido del ComboBox
                    comboBox.Text = string.Empty;

                    // Evitar que el control pierda el foco
                    e.Cancel = true;
                }
            }
        }

        private void txt_periodos_extraordinarios_celebrados_TextChanged(object sender, EventArgs e)
        {
            int valorTextBox;

            // Verificar si el valor del TextBox es un número válido
            if (int.TryParse(txt_periodos_extraordinarios_celebrados.Text, out valorTextBox))
            {
                // Delimitar el valor del ComboBox según el valor del TextBox
                if (valorTextBox >= 1 && valorTextBox <= 10)
                {
                    // Limpiar el ComboBox antes de agregar nuevos elementos
                    cmb_periodo_extraordinario_reportado.Items.Clear();

                    // Llenar el ComboBox con los elementos del 1 al valor del TextBox
                    for (int i = 1; i <= valorTextBox; i++)
                    {
                        string cadena = "Data Source = DB_PLE.db;Version=3;";

                        using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                        {
                            try
                            {
                                // abrir la conexion
                                conexion.Open();

                                // comando de sql con filtro
                                string query = "select descripcion from TC_PERIODO_EXT where id_periodo_ext = @id";
                                SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                                cmd.Parameters.AddWithValue("@id", i);

                                // Utilizar un DataReader para obtener los datos
                                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);

                                // Agregar los elementos del DataTable al ComboBox
                                foreach (DataRow row in dataTable.Rows)
                                {
                                    cmb_periodo_extraordinario_reportado.Items.Add(row["descripcion"].ToString());
                                }
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
                }
                else
                {
                    // Si el valor del TextBox está fuera del rango permitido, mostrar un mensaje de error
                    MessageBox.Show("El valor debe estar entre 1 y 10");
                }
            }
            else
            {
                // Si el valor del TextBox no es un número válido, mostrar un mensaje de error
                //MessageBox.Show("Ingrese un número válido");
            }
        }
        private bool IsDuplicateRecord(string periodo_reportado_pe)
        {
            foreach (DataGridViewRow row in dgvPE.Rows)
            {
                if (row.IsNewRow) continue; // Skip the new row placeholder

                string existingId = row.Cells["periodo_reportado_pe"].Value.ToString();

                if (existingId == periodo_reportado_pe)
                {
                    return true;
                }
            }
            return false;
        }
        private void txt_nombre_legislatura_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_nombre_legislatura.Text = txt_nombre_legislatura.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_nombre_legislatura.SelectionStart = txt_nombre_legislatura.Text.Length;
        }
        private void Txt_distritos_uninominales_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada es un número o una tecla de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Si no es un número ni una tecla de control, cancela la entrada
                e.Handled = true;

                // Muestra una ventana emergente informando al usuario que solo se permiten números
                MessageBox.Show("Solo se permiten valores numéricos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Si el carácter es un número, realizamos una validación adicional
                System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;

                // Obtén el texto actual del TextBox y añádele el carácter presionado
                string newText = textBox.Text.Insert(textBox.SelectionStart, e.KeyChar.ToString());

                // Intenta convertir el nuevo texto a un número
                if (int.TryParse(newText, out int result))
                {
                    // Verifica si el número es mayor que 1
                    if (result < 1)
                    {
                        // Si el número es menor o igual a 1, cancela la entrada
                        e.Handled = true;

                        // Muestra una ventana emergente informando al usuario que solo se permiten valores mayores a 1
                        MessageBox.Show("Solo se permiten valores mayores a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void Txt_diputaciones_plurinominales_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada es un número o una tecla de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Si no es un número ni una tecla de control, cancela la entrada
                e.Handled = true;

                // Muestra una ventana emergente informando al usuario que solo se permiten números
                MessageBox.Show("Solo se permiten valores numéricos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Txt_sesiones_celebradas_po_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada es un número o una tecla de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Si no es un número ni una tecla de control, cancela la entrada
                e.Handled = true;

                // Muestra una ventana emergente informando al usuario que solo se permiten números
                MessageBox.Show("Solo se permiten valores numéricos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Txt_sesiones_celebradas_pe_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada es un número o una tecla de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Si no es un número ni una tecla de control, cancela la entrada
                e.Handled = true;

                // Muestra una ventana emergente informando al usuario que solo se permiten números
                MessageBox.Show("Solo se permiten valores numéricos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txt_sesiones_celebradas_p_rec_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada es un número o una tecla de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Si no es un número ni una tecla de control, cancela la entrada
                e.Handled = true;

                // Muestra una ventana emergente informando al usuario que solo se permiten números
                MessageBox.Show("Solo se permiten valores numéricos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txt_periodos_extraordinarios_celebrados_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada es un número o una tecla de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Si no es un número ni una tecla de control, cancela la entrada
                e.Handled = true;

                // Muestra una ventana emergente informando al usuario que solo se permiten números
                MessageBox.Show("Solo se permiten valores numéricos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Dtp_fecha_inicio_informacion_reportada_ValueChanged_1(object sender, EventArgs e)
        {
            /*
            if (dtp_fecha_inicio_informacion_reportada.Value < dtp_inicio_funciones_legislatura.Value ||
                dtp_fecha_inicio_informacion_reportada.Value > dtp_termino_funciones_legislatura.Value)
            {
                MessageBox.Show("La fecha debe estar entre el inicio y término de funciones", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                dtp_fecha_inicio_informacion_reportada.Value = dtp_inicio_funciones_legislatura.Value.AddDays(1);
                dtp_fecha_inicio_informacion_reportada.Focus();
            }
            */
        }
        private void dtp_fecha_inicio_po_ValueChanged_1(object sender, EventArgs e)
        {



        }
        private bool ValidacionCampos_DG()
        {
            // Array de controles a validar
            Control[] controlesAValidar;

            string ent_rep = cmb_entidad_federativa.Text;


            if (chbPE.Checked)
            {
                if (ent_rep == "Baja California" || ent_rep == "Jalisco")
                {
                    controlesAValidar = new Control[]  {
                        txt_nombre_legislatura, Txt_sesiones_celebradas_po,
                        Txt_distritos_uninominales,Txt_diputaciones_plurinominales,
                        txt_periodos_extraordinarios_celebrados
                    };
                }
                else
                {
                    controlesAValidar = new Control[]  {
                        txt_nombre_legislatura, Txt_sesiones_celebradas_po, txt_sesiones_celebradas_p_rec,
                        Txt_distritos_uninominales,Txt_diputaciones_plurinominales,
                        txt_periodos_extraordinarios_celebrados
                    };
                }

            }
            else
            {
                if (ent_rep == "Baja California" || ent_rep == "Jalisco")
                {
                    controlesAValidar = new Control[]  {
                        txt_nombre_legislatura, Txt_sesiones_celebradas_po,
                        Txt_distritos_uninominales,Txt_diputaciones_plurinominales
                    };
                }
                else
                {
                    controlesAValidar = new Control[]  {
                        txt_nombre_legislatura, Txt_sesiones_celebradas_po, txt_sesiones_celebradas_p_rec,
                        Txt_distritos_uninominales,Txt_diputaciones_plurinominales
                    };
                }

            }

            bool camposValidos = true;

            foreach (Control c in controlesAValidar)
            {
                // Asigna el evento GotFocus fuera del bucle
                c.GotFocus += Control_GotFocus;

                // Verificar si el control está vacío
                if (c is System.Windows.Forms.TextBox && string.IsNullOrWhiteSpace(c.Text))
                {
                    MessageBox.Show($"El campo {c.Name} está vacío.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    c.Focus(); // Enfocar el control vacío
                    camposValidos = false; // Marcar que hay campos inválidos
                    break; // Salir del bucle después de encontrar el primer campo vacío
                }
                else if (c is System.Windows.Forms.ComboBox && ((System.Windows.Forms.ComboBox)c).SelectedIndex == -1)
                {
                    MessageBox.Show($"Debe seleccionar una opción en {c.Name}.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    c.Focus(); // Enfocar el control vacío
                    camposValidos = false; // Marcar que hay campos inválidos
                    break; // Salir del bucle después de encontrar el primer campo vacío
                }
                // Agregar más validaciones según sea necesario para otros tipos de controles
            }


            // Validar DataGridView solo si chbPE.Checked es verdadero
            if (chbPE.Checked && dgvPE != null)
            {
                if (dgvPE.Rows.Count == 0 || dgvPE.Rows.Cast<DataGridViewRow>().All(row => row.IsNewRow))
                {
                    MessageBox.Show("No hay periodos extraordinarios registrados.", "Sin registros", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    camposValidos = false; // Marcar que hay campos inválidos
                }
            }

            return camposValidos;
        }

        private void chbPE_CheckedChanged_1(object sender, EventArgs e)
        {
            // Cuando el estado del CheckBox cambia, se ejecutará este código
            CheckBox chbPE = (CheckBox)sender;
            if (chbPE.Checked)
            {

                // Si el CheckBox está marcado
                dgvPE.Enabled = true; dgvPE.BackgroundColor = Color.Honeydew;
                cmb_periodo_extraordinario_reportado.Enabled = true; cmb_periodo_extraordinario_reportado.BackColor = Color.Honeydew;
                txt_periodos_extraordinarios_celebrados.Enabled = true; txt_periodos_extraordinarios_celebrados.BackColor = Color.Honeydew;
                Txt_sesiones_celebradas_pe.Enabled = true; Txt_sesiones_celebradas_pe.BackColor = Color.Honeydew;
                dtp_fecha_inicio_pe.Enabled = true; dtp_fecha_termino_pe.Enabled = true;
                btnAgregarPE.Enabled = true; BtnEliminarPE.Enabled = true;

                // se ajustan las fechas
                dtp_fecha_inicio_pe.Value = dtp_fecha_inicio_p_rec.Value; dtp_fecha_termino_pe.Value = dtp_fecha_termino_p_rec.Value;


            }
            else
            {
                // Si el CheckBox está desmarcado
                dgvPE.Enabled = false; dgvPE.BackgroundColor = Color.LightGray;
                cmb_periodo_extraordinario_reportado.Enabled = false; cmb_periodo_extraordinario_reportado.BackColor = Color.LightGray;
                Txt_sesiones_celebradas_pe.Enabled = false; Txt_sesiones_celebradas_pe.BackColor = Color.LightGray;
                txt_periodos_extraordinarios_celebrados.Enabled = false; txt_periodos_extraordinarios_celebrados.BackColor = Color.LightGray;
                dtp_fecha_inicio_pe.Enabled = false; dtp_fecha_termino_pe.Enabled = false;
                btnAgregarPE.Enabled = false; BtnEliminarPE.Enabled = false;
                dgvPE.Rows.Clear(); cmb_periodo_extraordinario_reportado.Items.Clear();

                // se ajustan las fechas
                dtp_fecha_inicio_pe.Value = new DateTime(1899, 9, 9); dtp_fecha_termino_pe.Value = new DateTime(1899, 9, 9);

            }
        }

        // cmb_periodo_extraordinario_reportado
        private void cmb_periodo_extraordinario_reportado_SelectedIndexChanged(object sender, EventArgs e)
        {
            // se ajustan las fechas
            dtp_fecha_inicio_pe.Value = dtp_fecha_inicio_p_rec.Value; dtp_fecha_termino_pe.Value = dtp_fecha_termino_p_rec.Value;
        }
        private void cmb_periodo_extraordinario_reportado_Validating(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;

            if (comboBox != null)
            {
                // Quitar espacios en blanco del texto ingresado y convertir a minúsculas
                string cleanedText = comboBox.Text.Trim().Replace(" ", string.Empty).ToLower();

                // Permitir que el ComboBox se quede en blanco
                if (string.IsNullOrEmpty(cleanedText))
                {
                    e.Cancel = false;
                    return;
                }

                // Verificar si el texto del ComboBox coincide con alguna de las opciones
                bool isValid = false;
                foreach (var item in comboBox.Items)
                {
                    string cleanedItem = item.ToString().Trim().Replace(" ", string.Empty).ToLower();
                    if (cleanedText == cleanedItem)
                    {
                        isValid = true;
                        break;
                    }
                }

                if (!isValid)
                {
                    // Mostrar mensaje de error
                    MessageBox.Show("Por favor, seleccione una opción válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Borrar el contenido del ComboBox
                    comboBox.Text = string.Empty;

                    // Evitar que el control pierda el foco
                    e.Cancel = true;
                }
            }
        }

        // fecha inicio funciones legislatura
        private DateTime f1;
        private void dtp_inicio_funciones_legislatura_DropDown(object sender, EventArgs e)
        {
            f1 = dtp_inicio_funciones_legislatura.Value;
        }
        private void dtp_inicio_funciones_legislatura_CloseUp(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de MODIFICAR la fecha?", "Confirmacion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                dtp_inicio_funciones_legislatura.Value = f1;
            }

        }
        private void dtp_inicio_funciones_legislatura_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cancelar cualquier entrada manual
            e.Handled = true;
        }

        // fecha termino_funciones_legislatura
        private DateTime f2;
        private void dtp_termino_funciones_legislatura_DropDown(object sender, EventArgs e)
        {
            f2 = dtp_termino_funciones_legislatura.Value;

        }
        private void dtp_termino_funciones_legislatura_CloseUp(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de MODIFICAR la fecha?", "Confirmacion",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                dtp_termino_funciones_legislatura.Value = f2;
            }
        }
        private void dtp_termino_funciones_legislatura_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cancelar cualquier entrada manual
            e.Handled = true;
        }

        // fecha fecha_inicio_informacion_reportada
        private DateTime f3;
        private void dtp_fecha_inicio_informacion_reportada_DropDown(object sender, EventArgs e)
        {
            f3 = dtp_fecha_inicio_informacion_reportada.Value;
        }
        private void dtp_fecha_inicio_informacion_reportada_CloseUp(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de MODIFICAR la fecha?", "Confirmacion",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                dtp_fecha_inicio_informacion_reportada.Value = f3;
            }
        }
        private void dtp_fecha_inicio_informacion_reportada_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cancelar cualquier entrada manual
            e.Handled = true;
        }

        // fecha fecha_termino_informacion_reportada
        private DateTime f4;
        private void dtp_fecha_termino_informacion_reportada_DropDown(object sender, EventArgs e)
        {
            f4 = dtp_fecha_termino_informacion_reportada.Value;
        }
        private void dtp_fecha_termino_informacion_reportada_CloseUp(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de MODIFICAR la fecha?", "Confirmacion",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                dtp_fecha_termino_informacion_reportada.Value = f4;
            }
        }
        private void dtp_fecha_termino_informacion_reportada_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cancelar cualquier entrada manual
            e.Handled = true;
        }

        // fecha fecha_inicio_po
        private DateTime f5;
        private void dtp_fecha_inicio_po_DropDown(object sender, EventArgs e)
        {
            f5 = dtp_fecha_inicio_po.Value;
        }
        private void dtp_fecha_inicio_po_CloseUp(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de MODIFICAR la fecha?", "Confirmacion",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                dtp_fecha_inicio_po.Value = f5;
            }
        }
        private void dtp_fecha_inicio_po_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cancelar cualquier entrada manual
            e.Handled = true;
        }

        // fecha fecha_termino_po
        private DateTime f6;
        private void dtp_fecha_termino_po_DropDown(object sender, EventArgs e)
        {
            f6 = dtp_fecha_termino_po.Value;
        }
        private void dtp_fecha_termino_po_CloseUp(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de MODIFICAR la fecha?", "Confirmacion",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                dtp_fecha_termino_po.Value = f6;
            }
        }
        private void dtp_fecha_termino_po_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cancelar cualquier entrada manual
            e.Handled = true;
        }

        // fecha fecha_inicio_p_rec
        private DateTime f7;
        private void dtp_fecha_inicio_p_rec_DropDown(object sender, EventArgs e)
        {
            f7 = dtp_fecha_inicio_p_rec.Value;
        }
        private void dtp_fecha_inicio_p_rec_CloseUp(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de MODIFICAR la fecha?", "Confirmacion",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                dtp_fecha_inicio_p_rec.Value = f7;
            }
        }
        private void dtp_fecha_inicio_p_rec_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cancelar cualquier entrada manual
            e.Handled = true;
        }

        // fecha fecha_termino_p_rec
        private DateTime f8;
        private void dtp_fecha_termino_p_rec_DropDown(object sender, EventArgs e)
        {
            f8 = dtp_fecha_termino_p_rec.Value;
        }
        private void dtp_fecha_termino_p_rec_CloseUp(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de MODIFICAR la fecha?", "Confirmacion",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                dtp_fecha_termino_p_rec.Value = f8;
            }
        }
        private void dtp_fecha_termino_p_rec_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cancelar cualquier entrada manual
            e.Handled = true;
        }

        // fecha fecha_inicio_pe
        private DateTime f9;
        private void dtp_fecha_inicio_pe_DropDown(object sender, EventArgs e)
        {
            f9 = dtp_fecha_inicio_pe.Value;
        }
        private void dtp_fecha_inicio_pe_CloseUp(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de MODIFICAR la fecha?", "Confirmacion",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                dtp_fecha_inicio_pe.Value = f9;
            }
            else
            {
                if (dtp_fecha_inicio_pe.Value <= dtp_fecha_termino_p_rec.Value
                 && dtp_fecha_inicio_pe.Value >= dtp_fecha_inicio_p_rec.Value)
                {

                }
                else
                {
                    MessageBox.Show("La fecha debe estar contenida en el rango del periodo de receso reportado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtp_fecha_inicio_pe.Value = dtp_fecha_inicio_p_rec.Value;
                    dtp_fecha_inicio_pe.Focus();
                }
            }
        }
        private void dtp_fecha_inicio_pe_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cancelar cualquier entrada manual
            e.Handled = true;
        }

        // fecha fecha_termino_pe
        private DateTime f10;


        private void dtp_fecha_termino_pe_DropDown(object sender, EventArgs e)
        {
            f10 = dtp_fecha_termino_pe.Value;
        }
        private void dtp_fecha_termino_pe_CloseUp(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de MODIFICAR la fecha?", "Confirmacion",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                dtp_fecha_termino_pe.Value = f9;
            }
            else
            {
                if (dtp_fecha_termino_pe.Value <= dtp_fecha_termino_p_rec.Value
                 && dtp_fecha_termino_pe.Value >= dtp_fecha_inicio_p_rec.Value)
                {

                }
                else
                {
                    MessageBox.Show("La fecha debe estar contenida en el rango del periodo de receso reportado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtp_fecha_termino_pe.Value = dtp_fecha_termino_p_rec.Value;
                    dtp_fecha_termino_pe.Focus();
                }
            }
        }
        private void dtp_fecha_termino_pe_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cancelar cualquier entrada manual
            e.Handled = true;
        }
        private void txt_periodo_reportado_rec_TextChanged(object sender, EventArgs e)
        {
            // construccion id
            construccion_id_legislatura();

            string entidad_federativa = cmb_entidad_federativa.Text;
            string ejerc_const = cmb_ejercicio_constitucional_informacion_reportada.Text;
            string periodo_receso = txt_periodo_reportado_rec.Text;


            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                string query2 = "select distinct inicio_pr from TC_CALENDARIO_SESIONES WHERE entidad = @entidad_federativa " +
                    "AND ejercicio_constitucional = @ejerc_const " +
                    "AND periodos_reportar = @periodo_receso;";

                string query3 = "select distinct fin_pr from TC_CALENDARIO_SESIONES WHERE entidad = @entidad_federativa " +
                    "AND ejercicio_constitucional = @ejerc_const " +
                    "AND periodos_reportar = @periodo_receso;";

                conexion.Open();

                using (SQLiteCommand cmd2 = new SQLiteCommand(query2, conexion))
                {


                    cmd2.Parameters.AddWithValue("@entidad_federativa", entidad_federativa);
                    cmd2.Parameters.AddWithValue("@ejerc_const", ejerc_const);
                    cmd2.Parameters.AddWithValue("@periodo_receso", periodo_receso);

                    object resultado = cmd2.ExecuteScalar();

                    if (resultado != null && DateTime.TryParse(resultado.ToString(), out DateTime inicioReceso))
                    {
                        dtp_fecha_inicio_p_rec.Value = inicioReceso;
                    }
                    else
                    {

                    }
                }

                using (SQLiteCommand cmd3 = new SQLiteCommand(query3, conexion))
                {

                    cmd3.Parameters.AddWithValue("@entidad_federativa", entidad_federativa);
                    cmd3.Parameters.AddWithValue("@ejerc_const", ejerc_const);
                    cmd3.Parameters.AddWithValue("@periodo_receso", periodo_receso);

                    object resultado = cmd3.ExecuteScalar();

                    if (resultado != null && DateTime.TryParse(resultado.ToString(), out DateTime finReceso))
                    {
                        dtp_fecha_termino_p_rec.Value = finReceso;
                    }
                    else
                    {

                    }
                }

                conexion.Close();
            }



            /*
            if (cmb_periodo_reportado_po.Text.ToString() == "Primer periodo de receso" ||
                cmb_periodo_reportado_po.Text.ToString() == "Segundo periodo de receso" ||
                cmb_periodo_reportado_po.Text.ToString() == "Tercer periodo de receso")
            {
                chbPE.Enabled = true;
            }
            else
            {
                dgvPE.Enabled = false; cmb_periodo_extraordinario_reportado.Enabled = false;
                dtp_fecha_inicio_pe.Enabled = false; dtp_fecha_termino_pe.Enabled = false; Txt_sesiones_celebradas_pe.Enabled = false;
                btnAgregarPE.Enabled = false; BtnEliminarPE.Enabled = false; txt_periodos_extraordinarios_celebrados.Enabled = false;
                dgvPE.Rows.Clear(); cmb_periodo_extraordinario_reportado.Text = ""; txt_periodos_extraordinarios_celebrados.Clear();
                Txt_sesiones_celebradas_pe.Clear();
                chbPE.Checked = false; chbPE.Enabled = false;

            }
            */
        }
    }
}
