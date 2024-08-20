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
        private void cmb_Tema_CL()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_TEMA_COMISION";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_tema_comision_legislativa.DataSource = dataTable;
                    cmb_tema_comision_legislativa.DisplayMember = "descripcion";

                    cmb_tema_comision_legislativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_tema_comision_legislativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_tema_comision_legislativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_tema_comision_legislativa.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_transmision_reuniones_celebradas_CL()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_SI_NO where id_si_no in (1,2,3)";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cond_transmision_reuniones_celebradas_comision_legislativa.DataSource = dataTable;
                    cmb_cond_transmision_reuniones_celebradas_comision_legislativa.DisplayMember = "descripcion";

                    cmb_cond_transmision_reuniones_celebradas_comision_legislativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_transmision_reuniones_celebradas_comision_legislativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_transmision_reuniones_celebradas_comision_legislativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_transmision_reuniones_celebradas_comision_legislativa.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_celebracion_reuniones_CL()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_SI_NO WHERE id_si_no IN (1, 6, 3)";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cond_celebracion_reuniones_comision_legislativa.DataSource = dataTable;
                    cmb_cond_celebracion_reuniones_comision_legislativa.DisplayMember = "descripcion";

                    cmb_cond_celebracion_reuniones_comision_legislativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_celebracion_reuniones_comision_legislativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_celebracion_reuniones_comision_legislativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_celebracion_reuniones_comision_legislativa.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void pbo_cant_integrantes_comision_legislativa_Click_1(object sender, EventArgs e)
        {
            string mensaje = "1. En caso de que sus registros no le permitan desglosar la información de acuerdo" +
                " con los requerimientos solicitados capture - 1 (no se sabe “NS”).\n\n" +
               "2.En caso de que determinada categoría no se encuentre prevista en la normatividad aplicable, " +
               "capture - 2 (no aplica “NA”).\r\n";

            string titulo = "";

            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void pbo_cant_reuniones_celebradas_comision_legislativa_Click(object sender, EventArgs e)
        {
            string mensaje = "1. En caso de que sus registros no le permitan desglosar la información de acuerdo" +
                " con los requerimientos solicitados capture - 1 (no se sabe “NS”).\n\n" +
               "2.En caso de que determinada categoría no se encuentre prevista en la normatividad aplicable, " +
               "capture - 2 (no aplica “NA”).\r\n";

            string titulo = "";

            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void pbo_cant_reuniones_celebradas_transmitidas_comision_legislativa_Click(object sender, EventArgs e)
        {
            string mensaje = "1. En caso de que sus registros no le permitan desglosar la información de acuerdo" +
                " con los requerimientos solicitados capture - 1 (no se sabe “NS”).\n\n" +
               "2.En caso de que determinada categoría no se encuentre prevista en la normatividad aplicable, " +
               "capture - 2 (no aplica “NA”).\r\n";

            string titulo = "";

            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void pbo_cant_iniciativas_turnadas_a_comision_legislativa_Click(object sender, EventArgs e)
        {
            string mensaje = "1. En caso de que sus registros no le permitan desglosar la información de acuerdo" +
                 " con los requerimientos solicitados capture - 1 (no se sabe “NS”).\n\n" +
                "2.En caso de que determinada categoría no se encuentre prevista en la normatividad aplicable, " +
                "capture - 2 (no aplica “NA”).\r\n";

            string titulo = "";

            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void pbo_cant_dictamenes_emitidos_por_comision_legislativa_Click(object sender, EventArgs e)
        {
            string mensaje = "1. En caso de que sus registros no le permitan desglosar la información de acuerdo" +
                " con los requerimientos solicitados capture - 1 (no se sabe “NS”).\n\n" +
               "2.En caso de que determinada categoría no se encuentre prevista en la normatividad aplicable, " +
               "capture - 2 (no aplica “NA”).\r\n";

            string titulo = "";

            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void txt_consecutivo_comision_legislativa_TextChanged(object sender, EventArgs e)
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string tipo_cl = cmb_tipo_comision_legislativa.Text.ToString();

            string num_leg = "";
            using (SQLiteConnection con = new SQLiteConnection(cadena))
            {
                con.Open();
                string query = "SELECT abr FROM TC_TIPO_COMISION WHERE descripcion = @tipo_cl";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@tipo_cl", tipo_cl);
                num_leg = cmd.ExecuteScalar()?.ToString();
                con.Close();
            }

            string conse_cl = txt_consecutivo_comision_legislativa.Text.ToString();
            string cve_ent = txt_id_legislatura.Text.Substring(0, 2).ToString();
            string resultadoConcatenado = "COM_" + num_leg + "_" + cve_ent + "_" + conse_cl;

            // Mostrar el resultado en TextBox1
            txt_ID_comision_legislativa.Text = resultadoConcatenado;

        }

        // cmb_tipo_comision_legislativa
        private void cmb_tipo_comision_legislativa_SelectedIndexChanged(object sender, EventArgs e)
        {

            string cadena = "Data Source = DB_PLE.db;Version=3;";

            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string tipo_cl = cmb_tipo_comision_legislativa.Text.ToString();

            if (tipo_cl == "Otro tipo (especifique)")
            {
                Txt_otro_tipo_comision_legislativa_especifique.Enabled = true; Txt_otro_tipo_comision_legislativa_especifique.BackColor = Color.Honeydew;
                Txt_otro_tipo_comision_legislativa_especifique.Focus();
            }
            else
            {
                Txt_otro_tipo_comision_legislativa_especifique.Enabled = false; Txt_otro_tipo_comision_legislativa_especifique.BackColor = Color.LightGray;
                Txt_otro_tipo_comision_legislativa_especifique.Text = "";
            }

            string num_leg = "";
            using (SQLiteConnection con = new SQLiteConnection(cadena))
            {
                con.Open();
                string query = "SELECT abr FROM TC_TIPO_COMISION WHERE descripcion = @tipo_cl";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@tipo_cl", tipo_cl);
                num_leg = cmd.ExecuteScalar()?.ToString();
                con.Close();
            }

            if (txt_id_legislatura.Text == "")
            {
                string cve_ent = "";
                string conse_cl = txt_consecutivo_comision_legislativa.Text.ToString();
                string resultadoConcatenado = "COM_" + num_leg + "_" + cve_ent + "_" + conse_cl;
                txt_ID_comision_legislativa.Text = resultadoConcatenado;
            }
            else
            {
                string cve_ent = txt_id_legislatura.Text.Substring(0, 2).ToString();
                string conse_cl = txt_consecutivo_comision_legislativa.Text.ToString();
                string resultadoConcatenado = "COM_" + num_leg + "_" + cve_ent + "_" + conse_cl;
                txt_ID_comision_legislativa.Text = resultadoConcatenado;
            }

        }
        private void cmb_tipo_comision_legislativa_Validating(object sender, CancelEventArgs e)
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
                    // ajustar el nombre a la columna dependiendo el combobox
                    string cleanedItem = item["descripcion"].ToString().Trim().Replace(" ", string.Empty).ToLower();
                    if (cleanedText == cleanedItem)
                    {
                        isValid = true;
                        break;
                    }
                    // Mostrar el valor actual de item (para depuración)
                    Console.WriteLine(" Current item : " + item["descripcion"]);
                    // O usar Debug.WriteLine si estás depurando
                    System.Diagnostics.Debug.WriteLine(" Current item : " + item["descripcion"]);
                }
                if (!isValid)
                {
                    // Mostrar mensaje de error
                    MessageBox.Show(" Por favor, seleccione una opción válida.", " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Borrar el contenido del ComboBox
                    comboBox.Text = string.Empty;
                    // Evitar que el control pierda el foco
                    e.Cancel = true;
                }
            }
        }

        private void btnAgregarCL_Click(object sender, EventArgs e)
        {
            // se obtienen los valores
            string tema_comision_legislativa = cmb_tema_comision_legislativa.Text.Trim();
            string otro_tema = txt_otro_tema_comision_legislativa_especifique.Text.Trim();

            if (string.IsNullOrWhiteSpace(cmb_tema_comision_legislativa.Text))
            {
                MessageBox.Show("Revisar datos vacios");
            }
            else
            {
                // Agregar una nueva fila al DataGridView
                bool respuesta = IsDuplicateRecord_CL(cmb_tema_comision_legislativa.Text.ToString());

                if (respuesta == true)
                {
                    MessageBox.Show("Dato duplicado");
                }
                else
                {
                    // Agregar una nueva fila al DataGridView
                    dgv_tema_comision_legislativa.Rows.Add(tema_comision_legislativa, otro_tema);

                    cmb_tema_comision_legislativa.Text = "";
                    txt_otro_tema_comision_legislativa_especifique.Clear(); txt_otro_tema_comision_legislativa_especifique.Enabled = false;
                    txt_otro_tema_comision_legislativa_especifique.BackColor = Color.LightGray;
                }
            }

        }
        private bool IsDuplicateRecord_CL(string variable_cmb)
        {
            foreach (DataGridViewRow row in dgv_tema_comision_legislativa.Rows)
            {
                if (row.IsNewRow) continue; // Skip the new row placeholder

                string existingId = row.Cells["tema_comision_legislativa"].Value.ToString();

                if (existingId == variable_cmb)
                {
                    return true;
                }
            }
            return false;
        }

        // cmb_tema_comision_legislativa
        private void cmb_tema_comision_legislativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_tema_comision_legislativa.Text.ToString();

            if (valorComboBox1 == "Otro tema o asunto (especifique)")
            {
                txt_otro_tema_comision_legislativa_especifique.Enabled = true; txt_otro_tema_comision_legislativa_especifique.BackColor = Color.Honeydew;
                txt_otro_tema_comision_legislativa_especifique.Focus();
            }
            else
            {
                txt_otro_tema_comision_legislativa_especifique.Enabled = false; txt_otro_tema_comision_legislativa_especifique.BackColor = Color.LightGray;
                txt_otro_tema_comision_legislativa_especifique.Text = "";
            }

        }
        private void cmb_tema_comision_legislativa_Validating(object sender, CancelEventArgs e)
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
                    // ajustar el nombre a la columna dependiendo el combobox
                    string cleanedItem = item["descripcion"].ToString().Trim().Replace(" ", string.Empty).ToLower();
                    if (cleanedText == cleanedItem)
                    {
                        isValid = true;
                        break;
                    }
                    // Mostrar el valor actual de item (para depuración)
                    Console.WriteLine(" Current item : " + item["descripcion"]);
                    // O usar Debug.WriteLine si estás depurando
                    System.Diagnostics.Debug.WriteLine(" Current item : " + item["descripcion"]);
                }
                if (!isValid)
                {
                    // Mostrar mensaje de error
                    MessageBox.Show(" Por favor, seleccione una opción válida.", " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Borrar el contenido del ComboBox
                    comboBox.Text = string.Empty;
                    // Evitar que el control pierda el foco
                    e.Cancel = true;
                }
            }
        }

        private void Txt_otro_tipo_comision_legislativa_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            Txt_otro_tipo_comision_legislativa_especifique.Text = Txt_otro_tipo_comision_legislativa_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            Txt_otro_tipo_comision_legislativa_especifique.SelectionStart = Txt_otro_tipo_comision_legislativa_especifique.Text.Length;

        }
        private void txt_otro_tema_comision_legislativa_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_otro_tema_comision_legislativa_especifique.Text = txt_otro_tema_comision_legislativa_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_otro_tema_comision_legislativa_especifique.SelectionStart = txt_otro_tema_comision_legislativa_especifique.Text.Length;
        }
        private void btnEliminarCL_Click(object sender, EventArgs e)
        {
            if (dgv_tema_comision_legislativa.SelectedRows.Count > 0)
            {
                dgv_tema_comision_legislativa.Rows.RemoveAt(dgv_tema_comision_legislativa.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }

        }

        // txt_cant_integrantes_comision_legislativa
        private void txt_cant_integrantes_comision_legislativa_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '-') || (e.KeyChar == '-' && ((System.Windows.Forms.TextBox)sender).Text.Length != 0))
            {
                e.Handled = true; // Ignorar el carácter
            }

        }
        private void txt_cant_integrantes_comision_legislativa_Leave(object sender, EventArgs e)
        {
            int valor;
            int.TryParse(txt_cant_integrantes_comision_legislativa.Text, out valor);
            // Verificar si el valor está dentro del rango permitido
            if (valor < -2)
            {
                MessageBox.Show("Registrar el valor correcto, ver cuadro de ayuda.");
                txt_cant_integrantes_comision_legislativa.Text = ""; // Limpiar el TextBox si está fuera del rango
            }
            if (valor == -1 || valor == -2)
            {
                MessageBox.Show("Justificar la elección en el apartado de observaciones");
            }
        }

        // cmb_cond_celebracion_reuniones_comision_legislativa
        private void cmb_cond_celebracion_reuniones_comision_legislativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_cant_reuniones_celebradas_comision_legislativa.Clear();
            cmb_cond_transmision_reuniones_celebradas_comision_legislativa.SelectedIndex = -1;
            txt_cant_reuniones_celebradas_comision_legislativa.Clear();

            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_celebracion_reuniones_comision_legislativa.Text.ToString();

            if (valorComboBox1 == "No (especifique)")
            {
                txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Enabled = true;
                txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Focus();
                txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.BackColor = Color.Honeydew;
            }
            else
            {
                txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Enabled = false;
                txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Text = "";
                txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.BackColor = Color.LightGray;
            }

            if (valorComboBox1 == "Si")
            {
                txt_cant_reuniones_celebradas_comision_legislativa.Enabled = true; txt_cant_reuniones_celebradas_comision_legislativa.BackColor = Color.Honeydew;
                cmb_cond_transmision_reuniones_celebradas_comision_legislativa.Enabled = true; cmb_cond_transmision_reuniones_celebradas_comision_legislativa.BackColor = Color.Honeydew;
            }
            else
            {
                txt_cant_reuniones_celebradas_comision_legislativa.Enabled = false; txt_cant_reuniones_celebradas_comision_legislativa.BackColor = Color.LightGray;
                txt_cant_reuniones_celebradas_comision_legislativa.Text = "";

                cmb_cond_transmision_reuniones_celebradas_comision_legislativa.Enabled = false; cmb_cond_transmision_reuniones_celebradas_comision_legislativa.BackColor = Color.LightGray;
                cmb_cond_transmision_reuniones_celebradas_comision_legislativa.Text = "";
            }
        }
        private void cmb_cond_celebracion_reuniones_comision_legislativa_Validating(object sender, CancelEventArgs e)
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
                    // ajustar el nombre a la columna dependiendo el combobox
                    string cleanedItem = item["descripcion"].ToString().Trim().Replace(" ", string.Empty).ToLower();
                    if (cleanedText == cleanedItem)
                    {
                        isValid = true;
                        break;
                    }
                    // Mostrar el valor actual de item (para depuración)
                    Console.WriteLine(" Current item : " + item["descripcion"]);
                    // O usar Debug.WriteLine si estás depurando
                    System.Diagnostics.Debug.WriteLine(" Current item : " + item["descripcion"]);
                }
                if (!isValid)
                {
                    // Mostrar mensaje de error
                    MessageBox.Show(" Por favor, seleccione una opción válida.", " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Borrar el contenido del ComboBox
                    comboBox.Text = string.Empty;
                    // Evitar que el control pierda el foco
                    e.Cancel = true;
                }
            }
        }

        // cmb_cond_transmision_reuniones_celebradas_comision_legislativa
        private void cmb_cond_transmision_reuniones_celebradas_comision_legislativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_transmision_reuniones_celebradas_comision_legislativa.Text.ToString();

            if (valorComboBox1 == "Si")
            {
                txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.Enabled = true; txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.BackColor = Color.Honeydew;
            }
            else
            {
                txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.Enabled = false; txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.BackColor = Color.LightGray;
                txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.Text = "";
            }
        }
        private void cmb_cond_transmision_reuniones_celebradas_comision_legislativa_Validating(object sender, CancelEventArgs e)
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
                    // ajustar el nombre a la columna dependiendo el combobox
                    string cleanedItem = item["descripcion"].ToString().Trim().Replace(" ", string.Empty).ToLower();
                    if (cleanedText == cleanedItem)
                    {
                        isValid = true;
                        break;
                    }
                    // Mostrar el valor actual de item (para depuración)
                    Console.WriteLine(" Current item : " + item["descripcion"]);
                    // O usar Debug.WriteLine si estás depurando
                    System.Diagnostics.Debug.WriteLine(" Current item : " + item["descripcion"]);
                }
                if (!isValid)
                {
                    // Mostrar mensaje de error
                    MessageBox.Show(" Por favor, seleccione una opción válida.", " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Borrar el contenido del ComboBox
                    comboBox.Text = string.Empty;
                    // Evitar que el control pierda el foco
                    e.Cancel = true;
                }
            }
        }

        private void txt_observaciones_cl_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_observaciones_cl.Text = txt_observaciones_cl.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_observaciones_cl.SelectionStart = txt_observaciones_cl.Text.Length;

        }
        private void Txt_otro_tipo_comision_legislativa_especifique_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Txt_otro_tipo_comision_legislativa_especifique.Text))
            {
                MessageBox.Show("Debe especificar el otro tipo de comisión legislativa.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Txt_otro_tipo_comision_legislativa_especifique.Focus();
            }
        }
        private void txt_otro_tema_comision_legislativa_especifique_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_otro_tema_comision_legislativa_especifique.Text))
            {
                MessageBox.Show("Debe especificar el otro tema o asunto atendido por la comisión legislativa.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_otro_tema_comision_legislativa_especifique.Focus();
            }
        }

        private void txt_cant_reuniones_celebradas_transmitidas_comision_legislativa_TextChanged(object sender, EventArgs e)
        {
            int valor;
            int valor2;

            int.TryParse(txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.Text, out valor);
            int.TryParse(txt_cant_reuniones_celebradas_comision_legislativa.Text, out valor2);

            // Verificar si el valor está dentro del rango permitido
            if (valor > valor2)
            {
                MessageBox.Show("Debe ser igual o menor a la cantidad de reuniones celebradas por la comisión legislativa.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.Text = ""; // Limpiar el TextBox si está fuera del rango
                txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.Focus();
            }
            if (valor < -2)
            {
                MessageBox.Show("Registrar el valor correcto, ver cuadro de ayuda.");
                txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.Text = ""; // Limpiar el TextBox si está fuera del rango
            }

            if (valor == -1 || valor == -2)
            {
                MessageBox.Show("Justificar la elección en el apartado de observaciones");
                txt_observaciones_cl.Focus();
            }
        }
        private void Txt_nombre_comision_legislativa_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_nombre_comision_legislativa.Text = txt_nombre_comision_legislativa.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_nombre_comision_legislativa.SelectionStart = txt_nombre_comision_legislativa.Text.Length;
        }

        // txt_no_cond_celebracion_reuniones_comision_legislativa_especifique
        private void txt_no_cond_celebracion_reuniones_comision_legislativa_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Text = txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.SelectionStart = txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Text.Length;
        }
        private void txt_no_cond_celebracion_reuniones_comision_legislativa_especifique_Leave(object sender, EventArgs e)
        {
            /*
            if (string.IsNullOrWhiteSpace(txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Text))
            {
                MessageBox.Show("Debe especificar el motivo por el cual la comisión legislativa no se reunió durante el periodo reportado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Focus();
            }
            */
        }

        // txt_cant_reuniones_celebradas_comision_legislativa
        private void txt_cant_reuniones_celebradas_comision_legislativa_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '-') || (e.KeyChar == '-' && ((System.Windows.Forms.TextBox)sender).Text.Length != 0))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }
        private void txt_cant_reuniones_celebradas_comision_legislativa_Leave(object sender, EventArgs e)
        {
            int valor;
            int.TryParse(txt_cant_reuniones_celebradas_comision_legislativa.Text, out valor);

            // Verificar si el valor está dentro del rango permitido
            if (valor < -2)
            {
                MessageBox.Show("Registrar el valor correcto, ver cuadro de ayuda.");
                txt_cant_reuniones_celebradas_comision_legislativa.Text = ""; // Limpiar el TextBox si está fuera del rango
            }

            if (valor == -1 || valor == -2)
            {
                MessageBox.Show("Justificar la elección en el apartado de observaciones");
            }
        }

        // txt_cant_reuniones_celebradas_transmitidas_comision_legislativa
        private void txt_cant_reuniones_celebradas_transmitidas_comision_legislativa_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '-') || (e.KeyChar == '-' && ((System.Windows.Forms.TextBox)sender).Text.Length != 0))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }
        private void txt_cant_iniciativas_turnadas_a_comision_legislativa_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '-') || (e.KeyChar == '-' && ((System.Windows.Forms.TextBox)sender).Text.Length != 0))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }

        private void txt_cant_dictamenes_emitidos_por_comision_legislativa_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '-') || (e.KeyChar == '-' && ((System.Windows.Forms.TextBox)sender).Text.Length != 0))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }

        private void Txt_consecutivo_comision_legislativa_MouseHover(object sender, EventArgs e)
        {
            // Mostrar mensaje al pasar el ratón sobre el TextBox
            System.Windows.Forms.ToolTip tooltip = new System.Windows.Forms.ToolTip();
            tooltip.SetToolTip(txt_consecutivo_comision_legislativa, "Número asignado a la comisión legislativa." +
                " Para el caso de las comisiones ordinarias, permanentes u homólogas, " +
                "se sugiere respetar el orden descendente de las fracciones establecidas en el correspondiente " +
                "artículo de la Ley o Reglamento del Congreso de la entidad federativa.");
        }
        private void txt_cant_iniciativas_turnadas_a_comision_legislativa_TextChanged(object sender, EventArgs e)
        {
            int valor;
            int.TryParse(txt_cant_iniciativas_turnadas_a_comision_legislativa.Text, out valor);

            // Verificar si el valor está dentro del rango permitido
            if (valor < -2)
            {
                MessageBox.Show("Registrar el valor correcto, ver cuadro de ayuda.");
                txt_cant_iniciativas_turnadas_a_comision_legislativa.Text = ""; // Limpiar el TextBox si está fuera del rango
            }

            if (valor == -1 || valor == -2)
            {
                MessageBox.Show("Justificar la elección en el apartado de observaciones");
                txt_observaciones_cl.Focus();
            }
        }
        private void txt_cant_dictamenes_emitidos_por_comision_legislativa_TextChanged(object sender, EventArgs e)
        {
            int valor;
            int.TryParse(txt_cant_dictamenes_emitidos_por_comision_legislativa.Text, out valor);

            // Verificar si el valor está dentro del rango permitido
            if (valor < -2)
            {
                MessageBox.Show("Registrar el valor correcto, ver cuadro de ayuda.");
                txt_cant_dictamenes_emitidos_por_comision_legislativa.Text = ""; // Limpiar el TextBox si está fuera del rango
            }

            if (valor == -1 || valor == -2)
            {
                MessageBox.Show("Justificar la elección en el apartado de observaciones");
                txt_observaciones_cl.Focus();
            }
        }
        private void btnGuardarDB_CL_Click(object sender, EventArgs e)
        {
            bool cv = ValidarCampos_CL();

            if (cv == true)
            {
                DialogResult respuesta = MessageBox.Show("¿Está seguro de Guardar los datos?", "Confirmacion",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    // Agregar una nueva fila al DataGridView
                    bool duplicado = IsDuplicateRecord_RegistrosCL(txt_ID_comision_legislativa.Text.ToString());

                    if (duplicado == true)
                    {
                        MessageBox.Show("El ID ya se encuentra registrado. Favor de verificar la información.", "Comisiones Legislativas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {


                        string cadena = "Data Source = DB_PLE.db;Version=3;";

                        using (SQLiteConnection connection = new SQLiteConnection(cadena))
                        {
                            connection.Open();

                            // Recorremos las filas del DataGridView
                            foreach (DataGridViewRow row in dgv_tema_comision_legislativa.Rows)
                            {
                                // Ignoramos la fila vacía al final
                                if (!row.IsNewRow)
                                {
                                    // Insertamos los datos en la base de datos
                                    string query = "INSERT INTO TR_COMISIONES_LEGISLATIVAS (" +
                                        "id_legislatura," +
                                        "ID_comision_legislativa," +
                                        "consecutivo_comision_legislativa," +
                                        "nombre_comision_legislativa," +
                                        "tipo_comision_legislativa," +
                                        "otro_tipo_comision_legislativa_especifique," +
                                        "tema_comision_legislativa," +
                                        "otro_tema_comision_legislativa_especifique," +
                                        "cant_integrantes_comision_legislativa," +
                                        "cond_celebracion_reuniones_comision_legislativa," +
                                        "no_cond_celebracion_reuniones_comision_legislativa_especifique," +
                                        "cant_reuniones_celebradas_comision_legislativa," +
                                        "cond_transmision_reuniones_celebradas_comision_legislativa," +
                                        "cant_reuniones_celebradas_transmitidas_comision_legislativa," +
                                        "cant_iniciativas_turnadas_a_comision_legislativa," +
                                        "cant_dictamenes_emitidos_por_comision_legislativa," +
                                        "observaciones_cl," +
                                        "fecha_actualizacion" +
                                        ")" +
                                 "VALUES" +
                                        " (" +
                                        "@id_legislatura," +
                                        "@ID_comision_legislativa," +
                                        "@consecutivo_comision_legislativa," +
                                        "@nombre_comision_legislativa," +
                                        "@tipo_comision_legislativa," +
                                        "@otro_tipo_comision_legislativa_especifique," +
                                        "@tema_comision_legislativa," +
                                        "@otro_tema_comision_legislativa_especifique," +
                                        "@cant_integrantes_comision_legislativa," +
                                        "@cond_celebracion_reuniones_comision_legislativa," +
                                        "@no_cond_celebracion_reuniones_comision_legislativa_especifique," +
                                        "@cant_reuniones_celebradas_comision_legislativa," +
                                        "@cond_transmision_reuniones_celebradas_comision_legislativa," +
                                        "@cant_reuniones_celebradas_transmitidas_comision_legislativa," +
                                        "@cant_iniciativas_turnadas_a_comision_legislativa," +
                                        "@cant_dictamenes_emitidos_por_comision_legislativa," +
                                        "@observaciones_cl," +
                                        "@fecha_actualizacion" +
                                        ")";

                                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                                    {
                                        // Variables individuales
                                        command.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text);
                                        command.Parameters.AddWithValue("@ID_comision_legislativa", txt_ID_comision_legislativa.Text);
                                        command.Parameters.AddWithValue("@consecutivo_comision_legislativa", txt_consecutivo_comision_legislativa.Text);
                                        command.Parameters.AddWithValue("@nombre_comision_legislativa", txt_nombre_comision_legislativa.Text);
                                        command.Parameters.AddWithValue("@tipo_comision_legislativa", cmb_tipo_comision_legislativa.Text);
                                        command.Parameters.AddWithValue("@otro_tipo_comision_legislativa_especifique", Txt_otro_tipo_comision_legislativa_especifique.Text);
                                        command.Parameters.AddWithValue("@cant_integrantes_comision_legislativa", txt_cant_integrantes_comision_legislativa.Text);
                                        command.Parameters.AddWithValue("@cond_celebracion_reuniones_comision_legislativa", cmb_cond_celebracion_reuniones_comision_legislativa.Text);
                                        command.Parameters.AddWithValue("@no_cond_celebracion_reuniones_comision_legislativa_especifique", txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Text);
                                        command.Parameters.AddWithValue("@cant_reuniones_celebradas_comision_legislativa", txt_cant_reuniones_celebradas_comision_legislativa.Text);
                                        command.Parameters.AddWithValue("@cond_transmision_reuniones_celebradas_comision_legislativa", cmb_cond_transmision_reuniones_celebradas_comision_legislativa.Text);
                                        command.Parameters.AddWithValue("@cant_reuniones_celebradas_transmitidas_comision_legislativa", txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.Text);
                                        command.Parameters.AddWithValue("@cant_iniciativas_turnadas_a_comision_legislativa", txt_cant_iniciativas_turnadas_a_comision_legislativa.Text);
                                        command.Parameters.AddWithValue("@cant_dictamenes_emitidos_por_comision_legislativa", txt_cant_dictamenes_emitidos_por_comision_legislativa.Text);
                                        command.Parameters.AddWithValue("@observaciones_cl", txt_observaciones_cl.Text);
                                        command.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

                                        // Variables del dgv
                                        command.Parameters.AddWithValue("@tema_comision_legislativa", row.Cells["tema_comision_legislativa"].Value);
                                        command.Parameters.AddWithValue("@otro_tema_comision_legislativa_especifique", row.Cells["otro_tema_comision_legislativa_especifique"].Value);

                                        command.ExecuteNonQuery();
                                    }
                                }

                            }
                            connection.Close();
                        }

                        // Se reinicion los botones
                        MessageBox.Show("Datos guardados correctamente");

                        txt_nombre_comision_legislativa.Clear();
                        cmb_tipo_comision_legislativa.Text = ""; Txt_otro_tipo_comision_legislativa_especifique.Clear();
                        cmb_tema_comision_legislativa.Text = ""; txt_otro_tema_comision_legislativa_especifique.Clear();
                        dgv_tema_comision_legislativa.Rows.Clear();
                        txt_cant_integrantes_comision_legislativa.Clear(); cmb_cond_celebracion_reuniones_comision_legislativa.Text = "";
                        txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Clear();
                        txt_cant_reuniones_celebradas_comision_legislativa.Clear();
                        cmb_cond_transmision_reuniones_celebradas_comision_legislativa.SelectedIndex = -1;
                        txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.Clear();
                        txt_cant_iniciativas_turnadas_a_comision_legislativa.Clear();
                        txt_cant_dictamenes_emitidos_por_comision_legislativa.Clear();
                        txt_observaciones_cl.Clear();
                        txt_consecutivo_comision_legislativa.Clear();
                        Txt_otro_tipo_comision_legislativa_especifique.Enabled = false; Txt_otro_tipo_comision_legislativa_especifique.BackColor = Color.LightGray;
                        txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Enabled = false; txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.BackColor = Color.LightGray;
                        DGV_REGISTROS_CL();
                        txt_ID_comision_legislativa.Text = "";
                    }



                }
                else
                {

                }
            }
            else
            {
                //MessageBox.Show("El ID ya se encuentra registrado. Favor de verificar la información.", "Comisiones Legislativas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void DGV_REGISTROS_CL()
        {
            string cadena = "Data Source=DB_PLE.db;Version=3;";
            string id_legis = txt_id_legislatura.Text;

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // Abrir la conexión
                    conexion.Open();

                    // Comando de SQL
                    string query = "SELECT DISTINCT ID_comision_legislativa, nombre_comision_legislativa, " +
                                   "tipo_comision_legislativa, cant_integrantes_comision_legislativa " +
                                   "FROM TR_COMISIONES_LEGISLATIVAS WHERE id_legislatura = @id_legis";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        // Asignar el parámetro
                        cmd.Parameters.AddWithValue("@id_legis", id_legis);

                        // Utilizar un DataAdapter para obtener los datos
                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            dgv_registros_cl.DataSource = dataTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al llenar DGV comisiones legislativas: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }
            }
        }
        private bool IsDuplicateRecord_RegistrosCL(string variable_cmb)
        {
            foreach (DataGridViewRow row in dgv_registros_cl.Rows)
            {
                if (row.IsNewRow) continue; // Skip the new row placeholder

                string existingId = row.Cells["ID_comision_legislativa"].Value.ToString();

                if (existingId == variable_cmb)
                {
                    return true;
                }
            }
            return false;
        }
        private bool ValidarCampos_CL()
        {
            // Array de controles a validar
            Control[] controlesAValidar = { txt_consecutivo_comision_legislativa, txt_nombre_comision_legislativa, cmb_tipo_comision_legislativa,
            txt_cant_integrantes_comision_legislativa,cmb_cond_celebracion_reuniones_comision_legislativa,txt_cant_iniciativas_turnadas_a_comision_legislativa,
            txt_cant_dictamenes_emitidos_por_comision_legislativa};

            foreach (Control control in controlesAValidar)
            {
                // Verificar si el control está vacío
                if (string.IsNullOrWhiteSpace(control.Text))
                {
                    MessageBox.Show($"Existen campos vacíos.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    control.Focus(); // Enfocar el control vacío
                    return false; // Salir del método después de encontrar el primer campo vacío
                }
            }

            int ren_dg;
            ren_dg = dgv_tema_comision_legislativa.Rows.Count;

            if (ren_dg == 0)
            {
                MessageBox.Show($"Existen campos vacíos.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_tema_comision_legislativa.Focus();
                return false;

            }

            return true;
        }
        private void btnActualizarDGV_CL_Click(object sender, EventArgs e)
        {
            DGV_REGISTROS_CL();
        }


    }
}
