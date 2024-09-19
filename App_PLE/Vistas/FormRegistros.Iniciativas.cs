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

        // PRESENTACION --------------------------------------------------------------------------------------------------------------------

        private void cmb_Cond_presentacion_iniciativa_legislatura_actual()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_SI_NO where id_si_no in (1,2)";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cond_presentacion_iniciativa_legislatura_actual.DataSource = dataTable;
                    cmb_cond_presentacion_iniciativa_legislatura_actual.DisplayMember = "descripcion";

                    cmb_cond_presentacion_iniciativa_legislatura_actual.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_presentacion_iniciativa_legislatura_actual.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_presentacion_iniciativa_legislatura_actual.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_presentacion_iniciativa_legislatura_actual.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_presentacion_iniciativa_legislatura_actual_Validating(object sender, CancelEventArgs e)
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
        private void cmb_cond_presentacion_iniciativa_legislatura_actual_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el valor seleccionado y eliminar espacios adicionales
            string valorComboBox1 = cmb_cond_presentacion_iniciativa_legislatura_actual.Text.Trim();

            // Desbloquear condición de iniciativa
            if (valorComboBox1.Equals("Si", StringComparison.OrdinalIgnoreCase))
            {
                cmb_cond_presentacion_iniciativa_periodo.Enabled = true;
                cmb_cond_presentacion_iniciativa_periodo.BackColor = Color.Honeydew;
            }
            else
            {
                cmb_cond_presentacion_iniciativa_periodo.Enabled = false;
                cmb_cond_presentacion_iniciativa_periodo.BackColor = Color.LightGray;
                cmb_cond_presentacion_iniciativa_periodo.Text = "";
            }

            // Desbloquear Número de legislatura
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                cmb_numero_legislatura_presentacion_iniciativa.Enabled = true;
                cmb_numero_legislatura_presentacion_iniciativa.BackColor = Color.Honeydew;
            }
            else
            {
                cmb_numero_legislatura_presentacion_iniciativa.Enabled = false;
                cmb_numero_legislatura_presentacion_iniciativa.BackColor = Color.LightGray;
                cmb_numero_legislatura_presentacion_iniciativa.Text = "";
            }
            // Desbloquear Condición de actualización del estatus de la iniciativa en el periodo reportado.
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                cmb_cond_actualizacion_estatus_iniciativa_periodo.Enabled = true;
                cmb_cond_actualizacion_estatus_iniciativa_periodo.BackColor = Color.Honeydew;
            }
            else
            {
                cmb_cond_actualizacion_estatus_iniciativa_periodo.Enabled = false;
                cmb_cond_actualizacion_estatus_iniciativa_periodo.BackColor = Color.LightGray;
                cmb_cond_actualizacion_estatus_iniciativa_periodo.Text = "";
            }
            // Descloquea Condición de modificación de la información de ingreso de la iniciativa en el periodo reportado.
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                cmb_cond_modificacion_informacion_ingreso_periodo.Enabled = true;
                cmb_cond_modificacion_informacion_ingreso_periodo.BackColor = Color.Honeydew;
            }
            else
            {
                cmb_cond_modificacion_informacion_ingreso_periodo.Enabled = false;
                cmb_cond_modificacion_informacion_ingreso_periodo.BackColor = Color.LightGray;
                cmb_cond_modificacion_informacion_ingreso_periodo.Text = "";
            }
        }

        // txt_turno_iniciativa
        private void txt_turno_iniciativa_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }

        // cmb_numero_legislatura_presentacion_iniciativa
        private void cmb_numero_legislatura_presentacion_iniciativa_Validating(object sender, CancelEventArgs e)
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

        // cmb_Cond_presentacion_iniciativa_periodo
        private void cmb_Cond_presentacion_iniciativa_periodo()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_SI_NO where id_si_no in (1,2)";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cond_presentacion_iniciativa_periodo.DataSource = dataTable;
                    cmb_cond_presentacion_iniciativa_periodo.DisplayMember = "descripcion";

                    cmb_cond_presentacion_iniciativa_periodo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_presentacion_iniciativa_periodo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_presentacion_iniciativa_periodo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_presentacion_iniciativa_periodo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_presentacion_iniciativa_periodo_Validating(object sender, CancelEventArgs e)
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
        private void cmb_cond_presentacion_iniciativa_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el valor seleccionado y eliminar espacios adicionales
            string valorComboBox1 = cmb_cond_presentacion_iniciativa_periodo.Text.Trim();

            // Desbloquear Condición de actualización del estatus de la iniciativa en el periodo reportado.
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                cmb_cond_actualizacion_estatus_iniciativa_periodo.Enabled = true;
                cmb_cond_actualizacion_estatus_iniciativa_periodo.BackColor = Color.Honeydew;
            }
            else
            {
                cmb_cond_actualizacion_estatus_iniciativa_periodo.Enabled = false;
                cmb_cond_actualizacion_estatus_iniciativa_periodo.BackColor = Color.LightGray;
                cmb_cond_actualizacion_estatus_iniciativa_periodo.Text = "";
            }
            // Descloquea Condición de modificación de la información de ingreso de la iniciativa en el periodo reportado.
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                cmb_cond_modificacion_informacion_ingreso_periodo.Enabled = true;
                cmb_cond_modificacion_informacion_ingreso_periodo.BackColor = Color.Honeydew;
            }
            else
            {
                cmb_cond_modificacion_informacion_ingreso_periodo.Enabled = false;
                cmb_cond_modificacion_informacion_ingreso_periodo.BackColor = Color.LightGray;
                cmb_cond_modificacion_informacion_ingreso_periodo.Text = "";
            }
        }

        // cmb_numero_legislatura_presentacion_iniciativa
        private void cmb_Numero_legislatura_presentacion_iniciativa()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_NUM_LEGISLATURA";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_numero_legislatura_presentacion_iniciativa.DataSource = dataTable;
                    cmb_numero_legislatura_presentacion_iniciativa.DisplayMember = "descripcion";

                    cmb_numero_legislatura_presentacion_iniciativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_numero_legislatura_presentacion_iniciativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_numero_legislatura_presentacion_iniciativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_numero_legislatura_presentacion_iniciativa.SelectedIndex = -1; // Aquí se establece como vacío
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

        // ESTATUS --------------------------------------------------------------------------------------------------------------------

        private void cmb_Cond_actualizacion_estatus_iniciativa_periodo()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_SI_NO where id_si_no in (1,2) ";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cond_actualizacion_estatus_iniciativa_periodo.DataSource = dataTable;
                    cmb_cond_actualizacion_estatus_iniciativa_periodo.DisplayMember = "descripcion";

                    cmb_cond_actualizacion_estatus_iniciativa_periodo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_actualizacion_estatus_iniciativa_periodo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_actualizacion_estatus_iniciativa_periodo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_actualizacion_estatus_iniciativa_periodo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_actualizacion_estatus_iniciativa_periodo_Validating(object sender, CancelEventArgs e)
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
        private void cmb_cond_actualizacion_estatus_iniciativa_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el valor seleccionado y eliminar espacios adicionales
            string valorComboBox1 = cmb_cond_actualizacion_estatus_iniciativa_periodo.Text.Trim();

            // Desbloquear Condición de actualización del estatus de la iniciativa en el periodo reportado.
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                cmb_estatus_iniciativa.Enabled = false;
                cmb_estatus_iniciativa.BackColor = Color.LightGray;
            }
            else
            {
                cmb_estatus_iniciativa.Enabled = true;
                cmb_estatus_iniciativa.BackColor = Color.Honeydew;
                cmb_estatus_iniciativa.Text = "";
            }
        }

        // cmb_cond_modificacion_informacion_ingreso_periodo
        private void cmb_Cond_modificacion_informacion_ingreso_periodo()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_SI_NO where id_si_no in (1,2)";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cond_modificacion_informacion_ingreso_periodo.DataSource = dataTable;
                    cmb_cond_modificacion_informacion_ingreso_periodo.DisplayMember = "descripcion";

                    cmb_cond_modificacion_informacion_ingreso_periodo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_modificacion_informacion_ingreso_periodo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_modificacion_informacion_ingreso_periodo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_modificacion_informacion_ingreso_periodo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_modificacion_informacion_ingreso_periodo_Validating(object sender, CancelEventArgs e)
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
        private void cmb_cond_modificacion_informacion_ingreso_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el valor seleccionado y eliminar espacios adicionales
            string valorComboBox1 = cmb_cond_modificacion_informacion_ingreso_periodo.Text.Trim();

            // Bloquea fecha de ingreso de la iniciativa a oficilia partes.
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                dtp_fecha_ingreso_iniciativa_oficialia_partes.Enabled = false;
                dtp_fecha_ingreso_iniciativa_oficialia_partes.BackColor = Color.LightGray;
            }
            else
            {
                dtp_fecha_ingreso_iniciativa_oficialia_partes.Enabled = true;
                dtp_fecha_ingreso_iniciativa_oficialia_partes.BackColor = Color.Honeydew;
                dtp_fecha_ingreso_iniciativa_oficialia_partes.Text = "";
            }

            // Bloquea nombre de inicioativa por seleccionar No.
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                txt_nombre_iniciativa.Enabled = false;
                txt_nombre_iniciativa.BackColor = Color.LightGray;
            }
            else
            {
                txt_nombre_iniciativa.Enabled = true;
                txt_nombre_iniciativa.BackColor = Color.Honeydew;
                txt_nombre_iniciativa.Text = "";
            }


            // Bloquea la fecha de sesión en que se presenta la iniciativa.
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                dtp_fecha_sesion_presentacion_iniciativa.Enabled = false;
                dtp_fecha_sesion_presentacion_iniciativa.BackColor = Color.LightGray;
            }
            else
            {
                dtp_fecha_sesion_presentacion_iniciativa.Enabled = true;
                dtp_fecha_sesion_presentacion_iniciativa.BackColor = Color.Honeydew;
                dtp_fecha_sesion_presentacion_iniciativa.Text = "";
            }

            // Bloquea el tipo de iniciativa si se selecciona NO
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                cmb_tipo_iniciativa.Enabled = false;
                cmb_tipo_iniciativa.BackColor = Color.LightGray;
            }
            else
            {
                cmb_tipo_iniciativa.Enabled = true;
                cmb_tipo_iniciativa.BackColor = Color.Honeydew;
                cmb_tipo_iniciativa.Text = "";
            }
        }

        // cmb_Estatus_iniciativa
        private void cmb_Estatus_iniciativa()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_ESTATUS_INI";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_estatus_iniciativa.DataSource = dataTable;
                    cmb_estatus_iniciativa.DisplayMember = "descripcion";

                    cmb_estatus_iniciativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_estatus_iniciativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_estatus_iniciativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_estatus_iniciativa.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_estatus_iniciativa_Validating(object sender, CancelEventArgs e)
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
        private void cmb_estatus_iniciativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_estatus_iniciativa.Text.Trim();

            // Desbloquear Otro estatus de la iniciativa.
            if (valorComboBox1.Equals("Otro estatus (especifique)", StringComparison.OrdinalIgnoreCase))
            {
                txt_otro_estatus_iniciativa_especifique.Enabled = true;
                txt_otro_estatus_iniciativa_especifique.BackColor = Color.Honeydew;
            }
            else
            {
                txt_otro_estatus_iniciativa_especifique.Enabled = false;
                txt_otro_estatus_iniciativa_especifique.BackColor = Color.LightGray;
                txt_otro_estatus_iniciativa_especifique.Text = "";
            }

            // Desbloquear Etapa procesal de iniciativa para "Estudio" o "Dictamen"
            if (valorComboBox1.Equals("Estudio", StringComparison.OrdinalIgnoreCase) ||
                valorComboBox1.Equals("Dictamen", StringComparison.OrdinalIgnoreCase))
            {
                cmb_etapa_procesal_iniciativa.Enabled = true;
                cmb_etapa_procesal_iniciativa.BackColor = Color.Honeydew;

                // Aquí agregamos la lógica específica para "Estudio" y "Dictamen"
                if (valorComboBox1.Equals("Estudio", StringComparison.OrdinalIgnoreCase))
                {
                    CargarEtapaProcesal(1, 2); // Cargar etapas 1 y 2
                }
                else if (valorComboBox1.Equals("Dictamen", StringComparison.OrdinalIgnoreCase))
                {
                    CargarEtapaProcesal(3, 4); // Cargar etapas 3 y 4
                }
            }
            else
            {
                cmb_etapa_procesal_iniciativa.Enabled = false;
                cmb_etapa_procesal_iniciativa.BackColor = Color.LightGray;
                cmb_etapa_procesal_iniciativa.Text = "";
            }
        }
        private void CargarEtapaProcesal(int desde, int hasta)
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    conexion.Open();

                    string query = $"SELECT descripcion FROM TC_ETAPA_PROC WHERE id_etapa_proc BETWEEN {desde} AND {hasta}";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_etapa_procesal_iniciativa.DataSource = dataTable;
                    cmb_etapa_procesal_iniciativa.DisplayMember = "descripcion";

                    cmb_etapa_procesal_iniciativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_etapa_procesal_iniciativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_etapa_procesal_iniciativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_etapa_procesal_iniciativa.SelectedIndex = -1;
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

        // txt_otro_tipo_iniciativa_especifique
        private void txt_otro_tipo_iniciativa_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_otro_tipo_iniciativa_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_otro_tipo_iniciativa_especifique.Text = txt_otro_tipo_iniciativa_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_otro_tipo_iniciativa_especifique.SelectionStart = txt_otro_tipo_iniciativa_especifique.Text.Length;

        }

        // cmb_etapa_procesal_iniciativa
        private void cmb_Etapa_procesal_iniciativa()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_ETAPA_PROC";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_etapa_procesal_iniciativa.DataSource = dataTable;
                    cmb_etapa_procesal_iniciativa.DisplayMember = "descripcion";

                    cmb_etapa_procesal_iniciativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_etapa_procesal_iniciativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_etapa_procesal_iniciativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_etapa_procesal_iniciativa.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_etapa_procesal_iniciativa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        // Ingreso -----------------------------------------------------------------------------------------------------------------------

        private void txt_nombre_iniciativa_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_nombre_iniciativa_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_nombre_iniciativa.Text = txt_nombre_iniciativa.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_nombre_iniciativa.SelectionStart = txt_nombre_iniciativa.Text.Length;

        }

        // cmb_tipo_iniciativa
        private void cmb_Tipo_iniciativa()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_TIPO_INI";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_tipo_iniciativa.DataSource = dataTable;
                    cmb_tipo_iniciativa.DisplayMember = "descripcion";

                    cmb_tipo_iniciativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_tipo_iniciativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_tipo_iniciativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_tipo_iniciativa.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_tipo_iniciativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el valor seleccionado y eliminar espacios adicionales
            string valorComboBox1 = cmb_tipo_iniciativa.Text.Trim();

            // Bloquea fecha de ingreso de la iniciativa a oficilia partes.
            if (valorComboBox1.Equals("Otro tipo (especifique)", StringComparison.OrdinalIgnoreCase))
            {
                txt_otro_tipo_iniciativa_especifique.Enabled = true;
                txt_otro_tipo_iniciativa_especifique.BackColor = Color.Honeydew;
            }
            else
            {
                txt_otro_tipo_iniciativa_especifique.Enabled = false;
                txt_otro_tipo_iniciativa_especifique.BackColor = Color.LightGray;
                txt_otro_tipo_iniciativa_especifique.Text = "";
            }
        }

        // txt_otro_estatus_iniciativa_especifique
        private void txt_otro_estatus_iniciativa_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_otro_estatus_iniciativa_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_otro_estatus_iniciativa_especifique.Text = txt_otro_estatus_iniciativa_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_otro_estatus_iniciativa_especifique.SelectionStart = txt_otro_estatus_iniciativa_especifique.Text.Length;

        }

        
        
        
        
        
        
        
        
        
        //--------------------------------------






        private void cmb_Tipo_promovente_iniciativa()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_TIPO_PROMOVENTE";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_tipo_promovente_iniciativa.DataSource = dataTable;
                    cmb_tipo_promovente_iniciativa.DisplayMember = "descripcion";

                    cmb_tipo_promovente_iniciativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_tipo_promovente_iniciativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_tipo_promovente_iniciativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_tipo_promovente_iniciativa.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void tipo_Organo_constitucional_autonomo()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_ORG_CONST_AUT_PROMOVENTE";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_tipo_organo_constitucional_autonomo.DataSource = dataTable;
                    cmb_tipo_organo_constitucional_autonomo.DisplayMember = "descripcion";

                    cmb_tipo_organo_constitucional_autonomo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_tipo_organo_constitucional_autonomo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_tipo_organo_constitucional_autonomo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_tipo_organo_constitucional_autonomo.SelectedIndex = -1; // Aquí se establece como vacío
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

        

        

        

        // txt_otro_tipo_promovente_iniciativa_especifique
        private void txt_otro_tipo_promovente_iniciativa_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_otro_tipo_promovente_iniciativa_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_otro_tipo_promovente_iniciativa_especifique.Text = txt_otro_tipo_promovente_iniciativa_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_otro_tipo_promovente_iniciativa_especifique.SelectionStart = txt_otro_tipo_promovente_iniciativa_especifique.Text.Length;

        }

        // txt_otro_tipo_primer_dictamen_especifique
        private void txt_otro_tipo_primer_dictamen_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_otro_tipo_primer_dictamen_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_otro_tipo_primer_dictamen_especifique.Text = txt_otro_tipo_primer_dictamen_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_otro_tipo_primer_dictamen_especifique.SelectionStart = txt_otro_tipo_primer_dictamen_especifique.Text.Length;

        }

        // txt_otro_tipo_organo_constitucional_autonomo_especifique
        private void txt_otro_tipo_organo_constitucional_autonomo_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_otro_tipo_organo_constitucional_autonomo_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_otro_tipo_organo_constitucional_autonomo_especifique.Text = txt_otro_tipo_organo_constitucional_autonomo_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_otro_tipo_organo_constitucional_autonomo_especifique.SelectionStart = txt_otro_tipo_organo_constitucional_autonomo_especifique.Text.Length;

        }

        // txt_otro_sentido_resolucion_primer_dictamen_especifique
        private void txt_otro_sentido_resolucion_primer_dictamen_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_otro_sentido_resolucion_primer_dictamen_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_otro_sentido_resolucion_primer_dictamen_especifique.Text = txt_otro_sentido_resolucion_primer_dictamen_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_otro_sentido_resolucion_primer_dictamen_especifique.SelectionStart = txt_otro_sentido_resolucion_primer_dictamen_especifique.Text.Length;

        }

        // txt_otro_tipo_segundo_dictamen_especifique
        private void txt_otro_tipo_segundo_dictamen_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_otro_tipo_segundo_dictamen_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_otro_tipo_segundo_dictamen_especifique.Text = txt_otro_tipo_segundo_dictamen_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_otro_tipo_segundo_dictamen_especifique.SelectionStart = txt_otro_tipo_segundo_dictamen_especifique.Text.Length;

        }

        // txt_otro_sentido_resolucion_segundo_dictamen_especifique
        private void txt_otro_sentido_resolucion_segundo_dictamen_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_otro_sentido_resolucion_segundo_dictamen_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_otro_sentido_resolucion_segundo_dictamen_especifique.Text = txt_otro_sentido_resolucion_segundo_dictamen_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_otro_sentido_resolucion_segundo_dictamen_especifique.SelectionStart = txt_otro_sentido_resolucion_segundo_dictamen_especifique.Text.Length;

        }

        // txt_votaciones_pleno_a_favor_iniciativa
        private void txt_votaciones_pleno_a_favor_iniciativa_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }

        // txt_votaciones_pleno_en_contra_iniciativa_vp
        private void txt_votaciones_pleno_en_contra_iniciativa_vp_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }

        // txt_votaciones_pleno_abstencion_iniciativa
        private void txt_votaciones_pleno_abstencion_iniciativa_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }

    }

}
