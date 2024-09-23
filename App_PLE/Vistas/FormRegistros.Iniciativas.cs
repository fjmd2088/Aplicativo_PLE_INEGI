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
            // Desbloquea Condición Condición de iniciativa preferente.
            if (valorComboBox1.Equals("Si", StringComparison.OrdinalIgnoreCase))
            {
                cmb_cond_iniciativa_preferente.Enabled = true;
                cmb_cond_iniciativa_preferente.BackColor = Color.Honeydew;
            }
            else
            {
                cmb_cond_iniciativa_preferente.Enabled = false;
                cmb_cond_iniciativa_preferente.BackColor = Color.LightGray;
                cmb_cond_iniciativa_preferente.Text = "";
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

            // Bloquea Tipo de promovente de iniciativa
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                cmb_tipo_promovente_iniciativa.Enabled = false;
                cmb_tipo_promovente_iniciativa.BackColor = Color.LightGray;
            }
            else
            {
                cmb_tipo_promovente_iniciativa.Enabled = true;
                cmb_tipo_promovente_iniciativa.BackColor = Color.Honeydew;
                cmb_tipo_promovente_iniciativa.Text = "";
            }
            // Desbloquea Condición Condición de iniciativa preferente.
            if (valorComboBox1.Equals("Si", StringComparison.OrdinalIgnoreCase))
            {
                cmb_cond_iniciativa_preferente.Enabled = true;
                cmb_cond_iniciativa_preferente.BackColor = Color.Honeydew;
            }
            else
            {
                cmb_cond_iniciativa_preferente.Enabled = false;
                cmb_cond_iniciativa_preferente.BackColor = Color.LightGray;
                cmb_cond_iniciativa_preferente.Text = "";
            }
            // Desbloquea Condición Condición de iniciativa preferente.
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                cmb_cond_adhesion_iniciativa.Enabled = true;
                cmb_cond_adhesion_iniciativa.BackColor = Color.Honeydew;
            }
            else
            {
                cmb_cond_adhesion_iniciativa.Enabled = false;
                cmb_cond_adhesion_iniciativa.BackColor = Color.LightGray;
                cmb_cond_adhesion_iniciativa.Text = "";
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


        // INGRRESO -----------------------------------------------------------------------------------------------------------------------

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

        // TIPO ------------------ cmb_tipo_iniciativa
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
        private void cmb_tipo_iniciativa_Validating(object sender, CancelEventArgs e)
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


        // PROMOVENTE ------------------------------ cmb_tipo_promovente_iniciativa
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
        private void cmb_tipo_promovente_iniciativa_Validating(object sender, CancelEventArgs e)
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
        private void cmb_tipo_promovente_iniciativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_tipo_promovente_iniciativa.Text.Trim();

            // Desbloquea ComboBox de nombre de la persona legisladora
            if (valorComboBox1.Equals("Personas legisladoras", StringComparison.OrdinalIgnoreCase))
            {
                cmb_nombre_persona_legisladora_1.Enabled = true;
                cmb_nombre_persona_legisladora_1.BackColor = Color.Honeydew;
                btn_agregar_per_leg.Enabled = true; btn_eliminar_pers_legis.Enabled = true;
                dgv_per_legis.BackgroundColor = Color.Honeydew;
            }
            else
            {
                cmb_nombre_persona_legisladora_1.Enabled = false;
                cmb_nombre_persona_legisladora_1.BackColor = Color.LightGray;
                cmb_nombre_persona_legisladora_1.Text = "";
                btn_agregar_per_leg.Enabled = false; btn_eliminar_pers_legis.Enabled = false;
                dgv_per_legis.BackgroundColor = Color.LightGray;
            }

            // Desbloquea el ID de personas legisladorasn 
            if (valorComboBox1.Equals("Personas legisladoras", StringComparison.OrdinalIgnoreCase))
            {
                txt_ID_persona_legisladora_1.Enabled = false;
                txt_ID_persona_legisladora_1.BackColor = Color.Honeydew;
            }
            else
            {
                txt_ID_persona_legisladora_1.Enabled = false;
                txt_ID_persona_legisladora_1.BackColor = Color.LightGray;
                txt_ID_persona_legisladora_1.Text = "";
            }

            // Desbloquea Grupo parlamentario tabla y botones
            if (valorComboBox1.Equals("Grupo parlamentario", StringComparison.OrdinalIgnoreCase))
            {
                cmb_grupo_parlamentario.Enabled = true;
                cmb_grupo_parlamentario.BackColor = Color.Honeydew;
                btn_agregar_grupo_parla.Enabled = true; btn_eliminar_grupo_parla.Enabled = true;
                dgv_grupos_parla.BackgroundColor = Color.Honeydew;
            }
            else
            {
                cmb_grupo_parlamentario.Enabled = false;
                cmb_grupo_parlamentario.BackColor = Color.LightGray;
                cmb_grupo_parlamentario.Text = "";
                btn_agregar_grupo_parla.Enabled = false; btn_eliminar_grupo_parla.Enabled = false;
                dgv_grupos_parla.BackgroundColor = Color.LightGray;
            }

            // Desbloquea Comisiones legislativas
            if (valorComboBox1.Equals("Comisión legislativa", StringComparison.OrdinalIgnoreCase))
            {
                cmb_nombre_comision_legislativa_1.Enabled = true;
                cmb_nombre_comision_legislativa_1.BackColor = Color.Honeydew;
                btn_agregar_nom_com_leg.Enabled = true; btn_elimina_con_legisl.Enabled = true;
                dgv_com_legis.BackgroundColor = Color.Honeydew;
                txt_ID_comision_legislativa_1.Enabled = false;
                txt_ID_comision_legislativa_1.BackColor = Color.Honeydew;
            }
            else
            {
                cmb_nombre_comision_legislativa_1.Enabled = false;
                cmb_nombre_comision_legislativa_1.BackColor = Color.LightGray;
                cmb_nombre_comision_legislativa_1.Text = "";
                btn_agregar_nom_com_leg.Enabled = false; btn_elimina_con_legisl.Enabled = false;
                dgv_com_legis.BackgroundColor = Color.LightGray;
                txt_ID_comision_legislativa_1.Enabled = false;
                txt_ID_comision_legislativa_1.BackColor = Color.LightGray;
                txt_ID_comision_legislativa_1.Text = "";
            }
            // Desbloquea Otro tipo de promovente
            if (valorComboBox1.Equals("Otro tipo de promovente (especifique)", StringComparison.OrdinalIgnoreCase))
            {
                txt_otro_tipo_promovente_iniciativa_especifique.Enabled = true;
                txt_otro_tipo_promovente_iniciativa_especifique.BackColor = Color.Honeydew;
            }
            else
            {
                txt_otro_tipo_promovente_iniciativa_especifique.Enabled = false;
                txt_otro_tipo_promovente_iniciativa_especifique.BackColor = Color.LightGray;
                txt_otro_tipo_promovente_iniciativa_especifique.Text = "";
            }
            // Desbloquea Ayuntamiento
            if (valorComboBox1.Equals("Ayuntamientos", StringComparison.OrdinalIgnoreCase))
            {
                cmb_ayuntamiento.Enabled = true; txt_ageem_ini.Enabled = false;
                cmb_ayuntamiento.BackColor = Color.Honeydew;
                txt_ageem_ini.BackColor = Color.Honeydew;
            }
            else
            {
                cmb_ayuntamiento.Enabled = false;
                cmb_ayuntamiento.BackColor = Color.LightGray;
                cmb_ayuntamiento.Text = "";
                txt_ageem_ini.Enabled = false;
                txt_ageem_ini.BackColor = Color.LightGray;
            }
            // Desbloquea el Tipo de organo constitucional aytónomo promovente de la iniciativa
            if (valorComboBox1.Equals("Órgano constitucional autónomo", StringComparison.OrdinalIgnoreCase))
            {
                cmb_tipo_organo_constitucional_autonomo.Enabled = true;
                cmb_tipo_organo_constitucional_autonomo.BackColor = Color.Honeydew;
                
            }
            else
            {
                cmb_tipo_organo_constitucional_autonomo.Enabled = false;
                cmb_tipo_organo_constitucional_autonomo.BackColor = Color.LightGray;
                cmb_tipo_organo_constitucional_autonomo.Text = "";
                
            }
            // Desbloquea Condición Condición de iniciativa preferente.
            if (valorComboBox1.Equals("Si", StringComparison.OrdinalIgnoreCase))
            {
                cmb_cond_iniciativa_preferente.Enabled = true;
                cmb_cond_iniciativa_preferente.BackColor = Color.Honeydew;
            }
            else
            {
                cmb_cond_iniciativa_preferente.Enabled = false;
                cmb_cond_iniciativa_preferente.BackColor = Color.LightGray;
                cmb_cond_iniciativa_preferente.Text = "";
            }
            // Desbloquea Condición Condición de iniciativa preferente.
            if (valorComboBox1.Equals("Persona titular del Poder Ejecutivo", StringComparison.OrdinalIgnoreCase))
            {
                cmb_cond_iniciativa_preferente.Enabled = true;
                cmb_cond_iniciativa_preferente.BackColor = Color.Honeydew;
            }
            else
            {
                cmb_cond_iniciativa_preferente.Enabled = false;
                cmb_cond_iniciativa_preferente.BackColor = Color.LightGray;
                cmb_cond_iniciativa_preferente.Text = "";
            }
        }

        // PERSONAS LEGISLADORAS ------- cmb_nombre_persona_legisladora_1
        private void Cmb_nombre_persona_legisladora_1()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select txt_nombre_1_persona_legisladora from TR_PERSONAS_LEGISLADORAS";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_nombre_persona_legisladora_1.DataSource = dataTable;
                    cmb_nombre_persona_legisladora_1.DisplayMember = "txt_nombre_1_persona_legisladora";

                    cmb_nombre_persona_legisladora_1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_nombre_persona_legisladora_1.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_nombre_persona_legisladora_1.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_nombre_persona_legisladora_1.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_nombre_persona_legisladora_1_Validating(object sender, CancelEventArgs e)
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
                    string cleanedItem = item["txt_nombre_1_persona_legisladora"].ToString().Trim().Replace(" ", string.Empty).ToLower();
                    if (cleanedText == cleanedItem)
                    {
                        isValid = true;
                        break;
                    }
                    // Mostrar el valor actual de item (para depuración)
                    Console.WriteLine(" Current item : " + item["txt_nombre_1_persona_legisladora"]);
                    // O usar Debug.WriteLine si estás depurando
                    System.Diagnostics.Debug.WriteLine(" Current item : " + item["txt_nombre_1_persona_legisladora"]);
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
        private void cmb_nombre_persona_legisladora_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = cmb_nombre_persona_legisladora_1.Text;

            // Verificar si el nombre seleccionado es nulo o vacío
            if (string.IsNullOrEmpty(nombreSeleccionado))
            {
                txt_ID_persona_legisladora_1.Text = "";
                return;
            }

            // Crear la cadena de conexión
            string cadena = "Data Source=DB_PLE.db;Version=3;";

            // Usar la conexión a la base de datos
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // Abrir la conexión
                    conexion.Open();

                    // Crear la consulta SQL para obtener el ID de la persona seleccionada
                    string query = "SELECT txt_ID_persona_legisladora FROM TR_PERSONAS_LEGISLADORAS WHERE txt_nombre_1_persona_legisladora = @nombreSeleccionado";

                    // Crear el comando SQL
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        // Asignar el valor del parámetro @nombreSeleccionado
                        cmd.Parameters.AddWithValue("@nombreSeleccionado", nombreSeleccionado);

                        // Ejecutar la consulta y obtener el resultado
                        object resultado = cmd.ExecuteScalar();

                        // Verificar si se obtuvo un resultado
                        if (resultado != null)
                        {
                            txt_ID_persona_legisladora_1.Text = resultado.ToString();
                        }
                        else
                        {
                            txt_ID_persona_legisladora_1.Text = ""; // Limpiar el TextBox si no se encontró un ID
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener el ID de la persona legisladora: " + ex.Message);
                }
                finally
                {
                    // Cerrar la conexión
                    conexion.Close();
                }
            }
        }

        // Botones agregar y eliminar
        private void btn_agregar_per_leg_MouseClick(object sender, MouseEventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = cmb_nombre_persona_legisladora_1.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreSeleccionado))
            {
                MessageBox.Show("Revisar datos vacíos");
            }
            else
            {
                // Verificar si el nombre ya existe en la tabla
                bool respuesta = IsDuplicateRecord_PL(nombreSeleccionado);

                if (respuesta)
                {
                    MessageBox.Show("Dato duplicado");
                    cmb_nombre_persona_legisladora_1.Text = "";
                }
                else
                {
                    // Agregar una nueva fila al DataGridView
                    dgv_per_legis.Rows.Add(nombreSeleccionado, txt_ID_persona_legisladora_1.Text);
                    cmb_nombre_persona_legisladora_1.Text = "";

                    // Limpiar los campos
                    cmb_nombre_persona_legisladora_1.Text = "";
                    txt_ID_persona_legisladora_1.Text = "";  // Limpiar el campo txt_ID_comision_legislativa_1
                }
            }
        }
        private void btn_eliminar_pers_legis_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgv_per_legis.SelectedRows.Count > 0)
            {
                dgv_per_legis.Rows.RemoveAt(dgv_per_legis.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }
        private bool IsDuplicateRecord_PL(string variable_cmb)
        {
            foreach (DataGridViewRow row in dgv_per_legis.Rows)
            {
                if (row.IsNewRow) continue; // Skip the new row placeholder

                string existingId = row.Cells["Pesonas_legisladoras_ini"].Value.ToString();

                if (existingId == variable_cmb)
                {
                    return true;
                }
            }
            return false;
        }

        // GRUPO PARLAMENTARIO -------- cmb_grupo_parlamentario
        private void Cmb_grupo_parlamentario()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select entidad_federativa from TR_DATOS_GENERALES";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_grupo_parlamentario.DataSource = dataTable;
                    cmb_grupo_parlamentario.DisplayMember = "entidad_federativa";

                    cmb_grupo_parlamentario.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_grupo_parlamentario.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_grupo_parlamentario.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_grupo_parlamentario.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_grupo_parlamentario_Validating(object sender, CancelEventArgs e)
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
                    string cleanedItem = item["entidad_federativa"].ToString().Trim().Replace(" ", string.Empty).ToLower();
                    if (cleanedText == cleanedItem)
                    {
                        isValid = true;
                        break;
                    }
                    // Mostrar el valor actual de item (para depuración)
                    Console.WriteLine(" Current item : " + item["entidad_federativa"]);
                    // O usar Debug.WriteLine si estás depurando
                    System.Diagnostics.Debug.WriteLine(" Current item : " + item["entidad_federativa"]);
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

        // Botones agregar y eliminar
        private void btn_agregar_grupo_parla_MouseClick(object sender, MouseEventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = cmb_grupo_parlamentario.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreSeleccionado))
            {
                MessageBox.Show("Revisar datos vacíos");
            }
            else
            {
                // Verificar si el nombre ya existe en la tabla
                bool respuesta = IsDuplicateRecord_Par(nombreSeleccionado);

                if (respuesta)
                {
                    MessageBox.Show("Dato duplicado");
                    cmb_grupo_parlamentario.Text = "";
                }
                else
                {
                    // Agregar una nueva fila al DataGridView
                    dgv_grupos_parla.Rows.Add(nombreSeleccionado);
                    cmb_grupo_parlamentario.Text = "";

                }
            }

        }
        private void btn_eliminar_grupo_parla_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgv_grupos_parla.SelectedRows.Count > 0)
            {
                dgv_grupos_parla.Rows.RemoveAt(dgv_grupos_parla.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }
        private bool IsDuplicateRecord_Par(string variable_cmb)
        {
            foreach (DataGridViewRow row in dgv_grupos_parla.Rows)
            {
                if (row.IsNewRow) continue; // Skip the new row placeholder

                string existingId = row.Cells["Grupos_palamentarios_ini"].Value.ToString();

                if (existingId == variable_cmb)
                {
                    return true;
                }
            }
            return false;
        }

        // COMISIONES LEGISLATIVAAS ------------- cmb_nombre_comision_legislativa_1

        private void Cmb_nombre_comision_legislativa_1()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select nombre_comision_legislativa from TR_COMISIONES_LEGISLATIVAS";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_nombre_comision_legislativa_1.DataSource = dataTable;
                    cmb_nombre_comision_legislativa_1.DisplayMember = "nombre_comision_legislativa";

                    cmb_nombre_comision_legislativa_1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_nombre_comision_legislativa_1.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_nombre_comision_legislativa_1.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_nombre_comision_legislativa_1.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_nombre_comision_legislativa_1_Validating(object sender, CancelEventArgs e)
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
                    string cleanedItem = item["nombre_comision_legislativa"].ToString().Trim().Replace(" ", string.Empty).ToLower();
                    if (cleanedText == cleanedItem)
                    {
                        isValid = true;
                        break;
                    }
                    // Mostrar el valor actual de item (para depuración)
                    Console.WriteLine(" Current item : " + item["nombre_comision_legislativa"]);
                    // O usar Debug.WriteLine si estás depurando
                    System.Diagnostics.Debug.WriteLine(" Current item : " + item["nombre_comision_legislativa"]);
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
        private void cmb_nombre_comision_legislativa_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = cmb_nombre_comision_legislativa_1.Text;

            // Verificar si el nombre seleccionado es nulo o vacío
            if (string.IsNullOrEmpty(nombreSeleccionado))
            {
                txt_ID_comision_legislativa_1.Text = "";
                return;
            }

            // Crear la cadena de conexión
            string cadena = "Data Source=DB_PLE.db;Version=3;";

            // Usar la conexión a la base de datos
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // Abrir la conexión
                    conexion.Open();

                    // Crear la consulta SQL para obtener el ID de la persona seleccionada
                    string query = "SELECT ID_comision_legislativa FROM TR_COMISIONES_LEGISLATIVAS WHERE nombre_comision_legislativa = @nombreSeleccionado";

                    // Crear el comando SQL
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        // Asignar el valor del parámetro @nombreSeleccionado
                        cmd.Parameters.AddWithValue("@nombreSeleccionado", nombreSeleccionado);

                        // Ejecutar la consulta y obtener el resultado
                        object resultado = cmd.ExecuteScalar();

                        // Verificar si se obtuvo un resultado
                        if (resultado != null)
                        {
                            txt_ID_comision_legislativa_1.Text = resultado.ToString();
                        }
                        else
                        {
                            txt_ID_comision_legislativa_1.Text = ""; // Limpiar el TextBox si no se encontró un ID
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener el ID de la persona legisladora: " + ex.Message);
                }
                finally
                {
                    // Cerrar la conexión
                    conexion.Close();
                }
            }
        }

        // Botones agregar y eliminar

        private void btn_agregar_nom_com_leg_Click(object sender, EventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = cmb_nombre_comision_legislativa_1.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreSeleccionado))
            {
                MessageBox.Show("Revisar datos vacíos");
            }
            else
            {
                // Verificar si el nombre ya existe en la tabla
                bool respuesta = IsDuplicateRecord_COML(nombreSeleccionado);

                if (respuesta)
                {
                    MessageBox.Show("Dato duplicado");
                    cmb_nombre_comision_legislativa_1.Text = "";
                }
                else
                {
                    // Agregar una nueva fila al DataGridView
                    dgv_com_legis.Rows.Add(nombreSeleccionado, txt_ID_comision_legislativa_1.Text);

                    // Limpiar los campos
                    cmb_nombre_comision_legislativa_1.Text = "";
                    txt_ID_comision_legislativa_1.Text = "";  // Limpiar el campo txt_ID_comision_legislativa_1
                }
            }
        }
        private void btn_elimina_con_legisl_Click(object sender, EventArgs e)
        {
            if (dgv_com_legis.SelectedRows.Count > 0)
            {
                dgv_com_legis.Rows.RemoveAt(dgv_com_legis.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }
        private bool IsDuplicateRecord_COML(string variable_cmb)

        {
            foreach (DataGridViewRow row in dgv_com_legis.Rows)
            {
                if (row.IsNewRow) continue; // Skip the new row placeholder

                string existingId = row.Cells["Com_leg_ini"].Value.ToString();

                if (existingId == variable_cmb)
                {
                    return true;
                }
            }
            return false;
        }

        // AYUNTAMIENTO -------------------------- cmb_ayuntamiento

        private void Cmb_ayuntamiento()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select nom_mun from TC_AGEEM";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_ayuntamiento.DataSource = dataTable;
                    cmb_ayuntamiento.DisplayMember = "nom_mun";

                    cmb_ayuntamiento.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_ayuntamiento.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_ayuntamiento.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_ayuntamiento.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_ayuntamiento_Validating(object sender, CancelEventArgs e)
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
                    string cleanedItem = item["nom_mun"].ToString().Trim().Replace(" ", string.Empty).ToLower();
                    if (cleanedText == cleanedItem)
                    {
                        isValid = true;
                        break;
                    }
                    // Mostrar el valor actual de item (para depuración)
                    Console.WriteLine(" Current item : " + item["nom_mun"]);
                    // O usar Debug.WriteLine si estás depurando
                    System.Diagnostics.Debug.WriteLine(" Current item : " + item["nom_mun"]);
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
        private void cmb_ayuntamiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = cmb_ayuntamiento.Text;

            // Verificar si el nombre seleccionado es nulo o vacío
            if (string.IsNullOrEmpty(nombreSeleccionado))
            {
                txt_ageem_ini.Text = "";
                return;
            }

            // Crear la cadena de conexión
            string cadena = "Data Source=DB_PLE.db;Version=3;";

            // Usar la conexión a la base de datos
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // Abrir la conexión
                    conexion.Open();

                    // Crear la consulta SQL para obtener el ID de la persona seleccionada
                    string query = "SELECT cve_mun FROM TC_AGEEM WHERE nom_mun = @nombreSeleccionado";

                    // Crear el comando SQL
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        // Asignar el valor del parámetro @nombreSeleccionado
                        cmd.Parameters.AddWithValue("@nombreSeleccionado", nombreSeleccionado);

                        // Ejecutar la consulta y obtener el resultado
                        object resultado = cmd.ExecuteScalar();

                        // Verificar si se obtuvo un resultado
                        if (resultado != null)
                        {
                            txt_ageem_ini.Text = resultado.ToString();
                        }
                        else
                        {
                            txt_ageem_ini.Text = ""; // Limpiar el TextBox si no se encontró un ID
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener el ID de la persona legisladora: " + ex.Message);
                }
                finally
                {
                    // Cerrar la conexión
                    conexion.Close();
                }
            }
        }

        // TIPO DE ORGANO ------------------------ cmb_tipo_organo_constitucional_autonomo

        private void Cmb_tipo_organo_constitucional_autonomo()
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
        private void cmb_tipo_organo_constitucional_autonomo_Validating(object sender, CancelEventArgs e)
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
        private void cmb_tipo_organo_constitucional_autonomo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_tipo_organo_constitucional_autonomo.Text.Trim();

            // Desbloquea Otrro tipo de órgano constitucional pormoveente de la iniciativa
            if (valorComboBox1.Equals("Otro órgano constitucional autónomo (específique)", StringComparison.OrdinalIgnoreCase))
            {
                txt_otro_tipo_organo_constitucional_autonomo_especifique.Enabled = true;
                txt_otro_tipo_organo_constitucional_autonomo_especifique.BackColor = Color.Honeydew;
            }
            else
            {
                txt_otro_tipo_organo_constitucional_autonomo_especifique.Enabled = false;
                txt_otro_tipo_organo_constitucional_autonomo_especifique.BackColor = Color.LightGray;
                txt_otro_tipo_organo_constitucional_autonomo_especifique.Text = "";
                
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

        // INICIATIVA PREFERENTE ---------------- cmb_cond_iniciativa_preferente

        private void Cmb_cond_iniciativa_preferente()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_SI_NO where id_si_no in (1,2,3) ";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cond_iniciativa_preferente.DataSource = dataTable;
                    cmb_cond_iniciativa_preferente.DisplayMember = "descripcion";

                    cmb_cond_iniciativa_preferente.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_iniciativa_preferente.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_iniciativa_preferente.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_iniciativa_preferente.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_iniciativa_preferente_Validating(object sender, CancelEventArgs e)
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

        // ADHESIÓN A LA INICIATIVA ------------- cmb_cond_adhesion_iniciativa

        private void Cmb_cond_adhesion_iniciativa()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_SI_NO where id_si_no in (1,2,3) ";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cond_adhesion_iniciativa.DataSource = dataTable;
                    cmb_cond_adhesion_iniciativa.DisplayMember = "descripcion";

                    cmb_cond_adhesion_iniciativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_adhesion_iniciativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_adhesion_iniciativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_adhesion_iniciativa.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_adhesion_iniciativa_Validating(object sender, CancelEventArgs e)
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

        //ESTUDIOS ---------------------------------------------------------------------------------------------------------------------------------






        //--------------------------------------


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
