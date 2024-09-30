using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2010.Word;
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
                cmb_estatus_iniciativa.Text = "";
                txt_otro_estatus_iniciativa_especifique.Text = "";
            }
            else
            {
                cmb_estatus_iniciativa.Enabled = true;
                cmb_estatus_iniciativa.BackColor = Color.Honeydew;
                
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
            if (valorComboBox1.Equals("Si", StringComparison.OrdinalIgnoreCase))
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
            // Desbloquear Primer estudio
            if (valorComboBox1.Equals("Estudio", StringComparison.OrdinalIgnoreCase) ||
                valorComboBox1.Equals("Dictamen", StringComparison.OrdinalIgnoreCase) ||
                valorComboBox1.Equals("Desechada o improcedente", StringComparison.OrdinalIgnoreCase) ||
                valorComboBox1.Equals("Aprobada o procedente", StringComparison.OrdinalIgnoreCase))
            {
                // CMB
                cmb_nombre_comision_legislativa_1_primer_estudio.Enabled = true;
                cmb_nombre_comision_legislativa_1_primer_estudio.BackColor = Color.Honeydew;
                // TXT 
                txt_ID_comision_legislativa_1_primer_estudio.Enabled = false;
                txt_ID_comision_legislativa_1_primer_estudio.BackColor = Color.Honeydew;
                // TABLA
                dgv_prim_est_CL.BackgroundColor = Color.Honeydew;
                // BOTONES
                btn_agreg_prim_est.Enabled = true;
                btn_agreg_prim_est.BackColor = Color.Honeydew;
                btn_elim_prim_est.Enabled = true;
                btn_elim_prim_est.BackColor = Color.Honeydew;
            }
            else
            {
                // CMB
                cmb_nombre_comision_legislativa_1_primer_estudio.Enabled = false;
                cmb_nombre_comision_legislativa_1_primer_estudio.BackColor = Color.LightGray;
                cmb_nombre_comision_legislativa_1_primer_estudio.Text = "";
                // TXT
                txt_ID_comision_legislativa_1_primer_estudio.Enabled = false;
                txt_ID_comision_legislativa_1_primer_estudio.BackColor = Color.LightGray;
                txt_ID_comision_legislativa_1_primer_estudio.Text = "";
                // TABLA
                dgv_prim_est_CL.BackgroundColor = Color.LightGray;
                dgv_prim_est_CL.Rows.Clear();
                // BOTONES
                btn_agreg_prim_est.Enabled = false;
                btn_agreg_prim_est.BackColor = Color.LightGray;
                btn_elim_prim_est.Enabled = false;
                btn_elim_prim_est.BackColor = Color.LightGray;
            }
            // Desbloquear Primer dictamen
            if (valorComboBox1.Equals("Dictamen", StringComparison.OrdinalIgnoreCase) ||
                valorComboBox1.Equals("Desechada o improcedente", StringComparison.OrdinalIgnoreCase) ||
                valorComboBox1.Equals("Aprobada o procedente", StringComparison.OrdinalIgnoreCase))
            {
                // CMB
                cmb_tipo_primer_dictamen1.Enabled = true;
                cmb_tipo_primer_dictamen1.BackColor = Color.Honeydew;
                
            }
            else
            {
                // CMB
                cmb_tipo_primer_dictamen1.Enabled = false;
                cmb_tipo_primer_dictamen1.BackColor = Color.LightGray;
                cmb_tipo_primer_dictamen1.Text = "";
               
            }
            // Desbloquear Sentido de resolución
            if (valorComboBox1.Equals("Dictamen", StringComparison.OrdinalIgnoreCase) ||
                valorComboBox1.Equals("Desechada o improcedente", StringComparison.OrdinalIgnoreCase) ||
                valorComboBox1.Equals("Aprobada o procedente", StringComparison.OrdinalIgnoreCase))
            {
                // CMB
                cmb_sentido_resolucion_primer_dictamen.Enabled = true;
                cmb_sentido_resolucion_primer_dictamen.BackColor = Color.Honeydew;

            }
            else
            {
                // CMB
                cmb_sentido_resolucion_primer_dictamen.Enabled = false;
                cmb_sentido_resolucion_primer_dictamen.BackColor = Color.LightGray;
                cmb_sentido_resolucion_primer_dictamen.Text = "";

            }
            // Desbloquear Fecha de resolucion Publicación
            if (valorComboBox1.Equals("Desechada o improcedente", StringComparison.OrdinalIgnoreCase) ||
                valorComboBox1.Equals("Aprobada o procedente", StringComparison.OrdinalIgnoreCase))
            {
                // DTP
                dtp_fecha_resolucion_pleno_iniciativa.Enabled = true;
                dtp_fecha_resolucion_pleno_iniciativa.BackColor = Color.Honeydew;

            }
            else
            {
                // DTP
                dtp_fecha_resolucion_pleno_iniciativa.Enabled = false;
                dtp_fecha_resolucion_pleno_iniciativa.BackColor = Color.LightGray;
                dtp_fecha_resolucion_pleno_iniciativa.Text = "";

            }
            // Desbloquear Sentido de resolución pleno
            if (valorComboBox1.Equals("Desechada o improcedente", StringComparison.OrdinalIgnoreCase) ||
                valorComboBox1.Equals("Aprobada o procedente", StringComparison.OrdinalIgnoreCase))
            {
                // DTP
                cmb_sentido_resolucion_pleno_iniciativa.Enabled = true;
                cmb_sentido_resolucion_pleno_iniciativa.BackColor = Color.Honeydew;

            }
            else
            {
                // DTP
                cmb_sentido_resolucion_pleno_iniciativa.Enabled = false;
                cmb_sentido_resolucion_pleno_iniciativa.BackColor = Color.LightGray;
                cmb_sentido_resolucion_pleno_iniciativa.Text = "";

            }
            // Desbloquear Votaciones plenarias
            if (valorComboBox1.Equals("Desechada o improcedente", StringComparison.OrdinalIgnoreCase) ||
                valorComboBox1.Equals("Aprobada o procedente", StringComparison.OrdinalIgnoreCase))
            {
                // TXT Votaciones plenarias a favor
                txt_votaciones_pleno_a_favor_iniciativa.Enabled = true;
                txt_votaciones_pleno_a_favor_iniciativa.BackColor = Color.Honeydew;
                // TXT Votaciones plenarias a en contra
                txt_votaciones_pleno_en_contra_iniciativa_vp.Enabled = true;
                txt_votaciones_pleno_en_contra_iniciativa_vp.BackColor = Color.Honeydew;
                // TXT Votaciones plenarias a en abstención
                txt_votaciones_pleno_abstencion_iniciativa.Enabled = true;
                txt_votaciones_pleno_abstencion_iniciativa.BackColor = Color.Honeydew;
                // TXT Votaciones plenarias a totales
                txt_total_votaciones_pleno_iniciativa.BackColor = Color.Honeydew;
            }
            else
            {
                // TXT Votaciones plenarias a favor
                txt_votaciones_pleno_a_favor_iniciativa.Enabled = false;
                txt_votaciones_pleno_a_favor_iniciativa.BackColor = Color.LightGray;
                txt_votaciones_pleno_a_favor_iniciativa.Text = "";
                // TXT Votaciones plenarias a contra
                txt_votaciones_pleno_en_contra_iniciativa_vp.Enabled = false;
                txt_votaciones_pleno_en_contra_iniciativa_vp.BackColor = Color.LightGray;
                txt_votaciones_pleno_en_contra_iniciativa_vp.Text = "";
                // TXT Votaciones plenarias a abstención
                txt_votaciones_pleno_abstencion_iniciativa.Enabled = false;
                txt_votaciones_pleno_abstencion_iniciativa.BackColor = Color.LightGray;
                txt_votaciones_pleno_abstencion_iniciativa.Text = "";
                // TXT Votaciones plenarias a totales
                txt_total_votaciones_pleno_iniciativa.BackColor = Color.LightGray;
            }
            // Desbloquear Publicacion del poder ejecutivo
            if (valorComboBox1.Equals("Aprobada o procedente", StringComparison.OrdinalIgnoreCase))
            {
                // Desbloquea dtp fecha poder ejecutivo
                dtp_fecha_remision_ejecutivo_iniciativa.Enabled = true;
                dtp_fecha_remision_ejecutivo_iniciativa.BackColor = Color.Honeydew;
                // Desbloquea cmb de sentido resolución
                cmb_sentido_resolucion_ejecutivo_iniciativa.Enabled = true;
                cmb_sentido_resolucion_ejecutivo_iniciativa.BackColor = Color.Honeydew;
            }
            else
            {
                // Desbloquea dtp fecha poder ejecutivo
                dtp_fecha_remision_ejecutivo_iniciativa.Enabled = false;
                dtp_fecha_remision_ejecutivo_iniciativa.BackColor = Color.LightGray;
                dtp_fecha_remision_ejecutivo_iniciativa.Text = "";
                // Desbloquea cmb de sentido resolución
                cmb_sentido_resolucion_ejecutivo_iniciativa.Enabled = false;
                cmb_sentido_resolucion_ejecutivo_iniciativa.BackColor = Color.LightGray;
                cmb_sentido_resolucion_ejecutivo_iniciativa.Text = "";
            }
            
            // Combo box de Resolución Pleno
            string cadena = "Data Source=DB_PLE.db;Version=3;";

            if (cmb_estatus_iniciativa.SelectedItem != null)
            {
                try
                {
                    string valorComboBox = cmb_estatus_iniciativa.Text.ToString();

                    using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                    {
                        conexion.Open();

                        string query;

                        switch (valorComboBox)
                        {
                            case "Desechada o improcedente":
                                query = "SELECT descripcion FROM TC_SENTIDO_RESOLUCION WHERE id_sentido_resolucion = 1";
                                break;
                            case "Aprobada o procedente":
                                query = "SELECT descripcion FROM TC_SENTIDO_RESOLUCION WHERE id_sentido_resolucion = 2";
                                break;
                            default:
                                query = "SELECT descripcion FROM TC_SENTIDO_RESOLUCION WHERE id_sentido_resolucion = 3";
                                break;
                        }

                        using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                        {
                            using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);

                                cmb_sentido_resolucion_pleno_iniciativa.DataSource = dataTable;
                                cmb_sentido_resolucion_pleno_iniciativa.DisplayMember = "descripcion";

                                cmb_sentido_resolucion_pleno_iniciativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                                cmb_sentido_resolucion_pleno_iniciativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                                cmb_sentido_resolucion_pleno_iniciativa.DropDownStyle = ComboBoxStyle.DropDown;
                                cmb_sentido_resolucion_pleno_iniciativa.SelectedIndex = -1; // Establecer como vacío
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al llenar el ComboBox: " + ex.Message);
                }
            }
            else
            {
                // No hay elemento seleccionado en cmb_estatus_iniciativa
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
            string valorComboBox1 = cmb_etapa_procesal_iniciativa.Text.Trim();

            
            // Desbloquear Primer estudio
            if (valorComboBox1.Equals("Segundo estudio", StringComparison.OrdinalIgnoreCase) ||
                valorComboBox1.Equals("Segundo dictamen", StringComparison.OrdinalIgnoreCase)) 
                
            {
                // CMB
                cmb_nombre_comision_legislativa_1_segundo_estudio.Enabled = true;
                cmb_nombre_comision_legislativa_1_segundo_estudio.BackColor = Color.Honeydew;
                // TXT 
                txt_ID_comision_legislativa_1_segundo_estudio.Enabled = false;
                txt_ID_comision_legislativa_1_segundo_estudio.BackColor = Color.Honeydew;
                // TABLA
                dgv_segundo_est_CL.BackgroundColor = Color.Honeydew;
                // BOTONES
                btn_agreg_seg_est.Enabled = true;
                btn_agreg_seg_est.BackColor = Color.Honeydew;
                btn_elim_seg_est.Enabled = true;
                btn_elim_seg_est.BackColor = Color.Honeydew;
            }
            else
            {
                // CMB
                cmb_nombre_comision_legislativa_1_segundo_estudio.Enabled = false;
                cmb_nombre_comision_legislativa_1_segundo_estudio.BackColor = Color.LightGray;
                cmb_nombre_comision_legislativa_1_segundo_estudio.Text = "";
                // TXT
                txt_ID_comision_legislativa_1_segundo_estudio.Enabled = false;
                txt_ID_comision_legislativa_1_segundo_estudio.BackColor = Color.LightGray;
                txt_ID_comision_legislativa_1_segundo_estudio.Text = "";
                // TABLA
                dgv_segundo_est_CL.BackgroundColor = Color.LightGray;
                dgv_segundo_est_CL.Rows.Clear();
                // BOTONES
                btn_agreg_seg_est.Enabled = false;
                btn_agreg_seg_est.BackColor = Color.LightGray;
                btn_elim_seg_est.Enabled = false;
                btn_elim_seg_est.BackColor = Color.LightGray;
            }
            // Desbloquear Segundo estudio
            if (valorComboBox1.Equals("Segundo dictamen", StringComparison.OrdinalIgnoreCase))

            {
                // CMB
                cmb_tipo_segundo_dictamen.Enabled = true;
                cmb_tipo_segundo_dictamen.BackColor = Color.Honeydew;
                
            }
            else
            {
                // CMB
                cmb_tipo_segundo_dictamen.Enabled = false;
                cmb_tipo_segundo_dictamen.BackColor = Color.LightGray;
                cmb_tipo_segundo_dictamen.Text = "";
               
            }
            // Desbloquear Segundo estudio de resolución
            if (valorComboBox1.Equals("Segundo dictamen", StringComparison.OrdinalIgnoreCase))

            {
                // CMB
                cmb_sentido_resolucion_segundo_dictamen.Enabled = true;
                cmb_sentido_resolucion_segundo_dictamen.BackColor = Color.Honeydew;

            }
            else
            {
                // CMB
                cmb_sentido_resolucion_segundo_dictamen.Enabled = false;
                cmb_sentido_resolucion_segundo_dictamen.BackColor = Color.LightGray;
                cmb_sentido_resolucion_segundo_dictamen.Text = "";

            }
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
        private void dtp_fecha_ingreso_iniciativa_oficialia_partes_CloseUp(object sender, EventArgs e)
        {
            // Obtener las fechas seleccionadas
            DateTime fechaIngresoIniciativa = dtp_fecha_ingreso_iniciativa_oficialia_partes.Value;
            DateTime fechaTerminoReportada = dtp_fecha_termino_informacion_reportada.Value;

            // Validar si la fecha de ingreso es mayor a la fecha de término
            if (fechaIngresoIniciativa > fechaTerminoReportada)
            {
                // Mostrar mensaje de error
                MessageBox.Show("La fecha de ingreso a la iniciativa debe ser igual o menor a la fecha de término de la información reportada en datos generales", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Vaciar el campo de fecha
                dtp_fecha_ingreso_iniciativa_oficialia_partes.CustomFormat = " ";  // Deja en blanco el campo
                dtp_fecha_ingreso_iniciativa_oficialia_partes.Format = DateTimePickerFormat.Custom;  // Establece formato personalizado vacío
            }
        }
        private void dtp_fecha_sesion_presentacion_iniciativa_CloseUp(object sender, EventArgs e)
        {
            DateTime fechaSeesión = dtp_fecha_sesion_presentacion_iniciativa.Value;
            DateTime fechaOficialiaP = dtp_fecha_ingreso_iniciativa_oficialia_partes.Value;

            if (fechaSeesión < fechaOficialiaP)
            {
                MessageBox.Show("La fecha de sesión debe ser igual o mayor a la Fecha de ingreso de la iniciativa a oficialía de partes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Opcional: Resetear la fecha de publicación
                dtp_fecha_publicacion_gaceta_oficial_iniciativa.Value = DateTime.Now; // o una fecha predeterminada

                // Vaciar el campo de fecha
                dtp_fecha_sesion_presentacion_iniciativa.CustomFormat = " ";  // Deja en blanco el campo
                dtp_fecha_sesion_presentacion_iniciativa.Format = DateTimePickerFormat.Custom;  // Establece formato personalizado vacío
            }
        }

        // Tipo 
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

        // Promovente
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
                dgv_per_legis.Rows.Clear();
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
                //btn_agregar_grupo_parla.Enabled = true; btn_eliminar_grupo_parla.Enabled = true;
                //dgv_grupos_parla.BackgroundColor = Color.Honeydew;
            }
            else
            {
                cmb_grupo_parlamentario.Enabled = false;
                cmb_grupo_parlamentario.BackColor = Color.LightGray;
                cmb_grupo_parlamentario.Text = "";
                //btn_agregar_grupo_parla.Enabled = false; btn_eliminar_grupo_parla.Enabled = false;
                //dgv_grupos_parla.BackgroundColor = Color.LightGray;
                dgv_grupos_parla.Rows.Clear();
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
                dgv_com_legis.Rows.Clear();
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
                txt_ageem_ini.Text = "";
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
                txt_otro_tipo_organo_constitucional_autonomo_especifique.Text = "";
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

        // Tabla prsonas legisladoras
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

        // Botones agregar y eliminar de la tabla personas legisladoras
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

                string existingId = row.Cells["id_per_leg_agr"].Value.ToString();

                if (existingId == variable_cmb)
                {
                    return true;
                }
            }
            return false;
        }

        // Tabla grupo parrlamentario
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
        private void cmb_grupo_parlamentario_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_grupo_parlamentario.Text.Trim();

            // Desbloquea Grupo parlamentario tabla y botones varios
            if (valorComboBox1.Equals("Varios", StringComparison.OrdinalIgnoreCase))
            {
                cmb_varios_grupos_parlamentarios_especifique_1.Enabled = true;
                cmb_varios_grupos_parlamentarios_especifique_1.BackColor = Color.Honeydew;
                cmb_varios_grupos_parlamentarios_especifique_1.Text = "";
                btn_agregar_grupo_parla.Enabled = true; btn_eliminar_grupo_parla.Enabled = true;
                dgv_grupos_parla.BackgroundColor = Color.Honeydew;
            }
            else
            {
                cmb_varios_grupos_parlamentarios_especifique_1.Enabled = false;
                cmb_varios_grupos_parlamentarios_especifique_1.BackColor = Color.LightGray;
                cmb_varios_grupos_parlamentarios_especifique_1.Text = "";
                btn_agregar_grupo_parla.Enabled = false; btn_eliminar_grupo_parla.Enabled = false;
                dgv_grupos_parla.BackgroundColor = Color.LightGray;
                dgv_grupos_parla.Rows.Clear();
            }

           
        }

        // Varios grupos parlamentarios
        private void cmb_varios_grupos_parlamentarios_especifique_1_Validating(object sender, CancelEventArgs e)
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

        // Botones agregar y eliminar de la tabla grupo parlamentario
        private void btn_agregar_grupo_parla_MouseClick(object sender, MouseEventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = cmb_varios_grupos_parlamentarios_especifique_1.Text.Trim();

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
                    cmb_varios_grupos_parlamentarios_especifique_1.Text = "";
                }
                else
                {
                    // Agregar una nueva fila al DataGridView
                    dgv_grupos_parla.Rows.Add(nombreSeleccionado);
                    cmb_varios_grupos_parlamentarios_especifique_1.Text = "";

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

        // Tabla comisiones legislativas

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

        // Botones agregar y eliminar de la tabla d comisiones legislativas

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

                string existingId = row.Cells["ID_COM_LEG"].Value.ToString();

                if (existingId == variable_cmb)
                {
                    return true;
                }
            }
            return false;
        }

        // Ayuntamiento

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

        // Tipo de organo

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

        // Iniciativa preferente 

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

        // Adheción a la iniciativa 

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

        // ESTUDIO ---------------------------------------------------------------------------------------------------------------------------------

        // Primer estudio

        private void Cmb_nombre_comision_legislativa_1_primer_estudio()
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

                    cmb_nombre_comision_legislativa_1_primer_estudio.DataSource = dataTable;
                    cmb_nombre_comision_legislativa_1_primer_estudio.DisplayMember = "nombre_comision_legislativa";

                    cmb_nombre_comision_legislativa_1_primer_estudio.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_nombre_comision_legislativa_1_primer_estudio.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_nombre_comision_legislativa_1_primer_estudio.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_nombre_comision_legislativa_1_primer_estudio.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_nombre_comision_legislativa_1_primer_estudio_Validating(object sender, CancelEventArgs e)
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
        private void cmb_nombre_comision_legislativa_1_primer_estudio_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = cmb_nombre_comision_legislativa_1_primer_estudio.Text;

            // Verificar si el nombre seleccionado es nulo o vacío
            if (string.IsNullOrEmpty(nombreSeleccionado))
            {
                txt_ID_comision_legislativa_1_primer_estudio.Text = "";
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
                            txt_ID_comision_legislativa_1_primer_estudio.Text = resultado.ToString();
                        }
                        else
                        {
                            txt_ID_comision_legislativa_1_primer_estudio.Text = ""; // Limpiar el TextBox si no se encontró un ID
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
        private void btn_agreg_prim_est_Click(object sender, EventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = cmb_nombre_comision_legislativa_1_primer_estudio.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreSeleccionado))
            {
                MessageBox.Show("Revisar datos vacíos");
            }
            else
            {
                // Verificar si el nombre ya existe en la tabla
                bool respuesta = IsDuplicateRecord_PEST(nombreSeleccionado);

                if (respuesta)
                {
                    MessageBox.Show("Dato duplicado");
                    cmb_nombre_comision_legislativa_1_primer_estudio.Text = "";
                }
                else
                {
                    // Agregar una nueva fila al DataGridView
                    dgv_prim_est_CL.Rows.Add(nombreSeleccionado, txt_ID_comision_legislativa_1_primer_estudio.Text);

                    // Limpiar los campos
                    cmb_nombre_comision_legislativa_1_primer_estudio.Text = "";
                    txt_ID_comision_legislativa_1_primer_estudio.Text = "";  // Limpiar el campo txt_ID_comision_legislativa_1
                }
            }
        }
        private void btn_elim_prim_est_Click(object sender, EventArgs e)
        {
            if (dgv_prim_est_CL.SelectedRows.Count > 0)
            {
                dgv_prim_est_CL.Rows.RemoveAt(dgv_prim_est_CL.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }
        private bool IsDuplicateRecord_PEST(string variable_cmb)

        {
            foreach (DataGridViewRow row in dgv_prim_est_CL.Rows)
            {
                if (row.IsNewRow) continue; // Skip the new row placeholder

                string existingId = row.Cells["Tabla_prim_est"].Value.ToString();

                if (existingId == variable_cmb)
                {
                    return true;
                }
            }
            return false;
        }

        // Segundo estudio 

        private void Cmb_nombre_comision_legislativa_1_segundo_estudio()
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

                    cmb_nombre_comision_legislativa_1_segundo_estudio.DataSource = dataTable;
                    cmb_nombre_comision_legislativa_1_segundo_estudio.DisplayMember = "nombre_comision_legislativa";

                    cmb_nombre_comision_legislativa_1_segundo_estudio.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_nombre_comision_legislativa_1_segundo_estudio.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_nombre_comision_legislativa_1_segundo_estudio.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_nombre_comision_legislativa_1_segundo_estudio.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_nombre_comision_legislativa_1_segundo_estudio_Validating(object sender, CancelEventArgs e)
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
        private void cmb_nombre_comision_legislativa_1_segundo_estudio_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = cmb_nombre_comision_legislativa_1_segundo_estudio.Text;

            // Verificar si el nombre seleccionado es nulo o vacío
            if (string.IsNullOrEmpty(nombreSeleccionado))
            {
                txt_ID_comision_legislativa_1_segundo_estudio.Text = "";
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
                            txt_ID_comision_legislativa_1_segundo_estudio.Text = resultado.ToString();
                        }
                        else
                        {
                            txt_ID_comision_legislativa_1_segundo_estudio.Text = ""; // Limpiar el TextBox si no se encontró un ID
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
        private void btn_agreg_seg_est_Click(object sender, EventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = cmb_nombre_comision_legislativa_1_segundo_estudio.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreSeleccionado))
            {
                MessageBox.Show("Revisar datos vacíos");
            }
            else
            {
                // Verificar si el nombre ya existe en la tabla
                bool respuesta = IsDuplicateRecord_SEST(nombreSeleccionado);

                if (respuesta)
                {
                    MessageBox.Show("Dato duplicado");
                    cmb_nombre_comision_legislativa_1_segundo_estudio.Text = "";
                }
                else
                {
                    // Agregar una nueva fila al DataGridView
                    dgv_segundo_est_CL.Rows.Add(nombreSeleccionado, txt_ID_comision_legislativa_1_segundo_estudio.Text);

                    // Limpiar los campos
                    cmb_nombre_comision_legislativa_1_segundo_estudio.Text = "";
                    txt_ID_comision_legislativa_1_segundo_estudio.Text = "";  // Limpiar el campo txt_ID_comision_legislativa_1
                }
            }
        }
        private void btn_elim_seg_est_Click(object sender, EventArgs e)
        {
            if (dgv_segundo_est_CL.SelectedRows.Count > 0)
            {
                dgv_segundo_est_CL.Rows.RemoveAt(dgv_segundo_est_CL.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }
        private bool IsDuplicateRecord_SEST(string variable_cmb)

        {
            foreach (DataGridViewRow row in dgv_segundo_est_CL.Rows)
            {
                if (row.IsNewRow) continue; // Skip the new row placeholder

                string existingId = row.Cells["Tabla_seg_est"].Value.ToString();

                if (existingId == variable_cmb)
                {
                    return true;
                }
            }
            return false;
        }

        // DICTAMEN ---------------------------------------------------------------------------------------------------------------------------------

        // Primer dictamen

        private void Cmb_tipo_primer_dictamen1()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_DICTAMEN";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_tipo_primer_dictamen1.DataSource = dataTable;
                    cmb_tipo_primer_dictamen1.DisplayMember = "descripcion";

                    cmb_tipo_primer_dictamen1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_tipo_primer_dictamen1.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_tipo_primer_dictamen1.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_tipo_primer_dictamen1.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_tipo_primer_dictamen1_Validating(object sender, CancelEventArgs e)
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
        private void cmb_tipo_primer_dictamen1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_tipo_primer_dictamen1.Text.Trim();

            // Desbloquear Otro estatus de la iniciativa.
            if (valorComboBox1.Equals("Otro tipo (especifique)", StringComparison.OrdinalIgnoreCase))
            {
                txt_otro_tipo_primer_dictamen_especifique.Enabled = true;
                txt_otro_tipo_primer_dictamen_especifique.BackColor = Color.Honeydew;

            }
            else
            {
                txt_otro_tipo_primer_dictamen_especifique.Enabled = false;
                txt_otro_tipo_primer_dictamen_especifique.BackColor = Color.LightGray;
                txt_otro_tipo_primer_dictamen_especifique.Text = "";
            }
           
        }
        private void Cmb_sentido_resolucion_primer_dictamen()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_DICTAMEN";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_sentido_resolucion_primer_dictamen.DataSource = dataTable;
                    cmb_sentido_resolucion_primer_dictamen.DisplayMember = "descripcion";

                    cmb_sentido_resolucion_primer_dictamen.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_sentido_resolucion_primer_dictamen.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_sentido_resolucion_primer_dictamen.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_sentido_resolucion_primer_dictamen.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_sentido_resolucion_primer_dictamen_Validating(object sender, CancelEventArgs e)
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
        private void cmb_sentido_resolucion_primer_dictamen_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_sentido_resolucion_primer_dictamen.Text.Trim();

            // Desbloquear Otro estatus de la iniciativa.
            if (valorComboBox1.Equals("Otro tipo (especifique)", StringComparison.OrdinalIgnoreCase))
            {
                txt_otro_sentido_resolucion_primer_dictamen_especifique.Enabled = true;
                txt_otro_sentido_resolucion_primer_dictamen_especifique.BackColor = Color.Honeydew;

            }
            else
            {
                txt_otro_sentido_resolucion_primer_dictamen_especifique.Enabled = false;
                txt_otro_sentido_resolucion_primer_dictamen_especifique.BackColor = Color.LightGray;
                txt_otro_sentido_resolucion_primer_dictamen_especifique.Text = "";
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


        // Segundo dictamen

        private void Cmb_tipo_segundo_dictamen()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_DICTAMEN";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_tipo_segundo_dictamen.DataSource = dataTable;
                    cmb_tipo_segundo_dictamen.DisplayMember = "descripcion";

                    cmb_tipo_segundo_dictamen.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_tipo_segundo_dictamen.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_tipo_segundo_dictamen.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_tipo_segundo_dictamen.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_tipo_segundo_dictamen_Validating(object sender, CancelEventArgs e)
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
        private void cmb_tipo_segundo_dictamen_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_tipo_segundo_dictamen.Text.Trim();

            // Desbloquear Otro estatus de la iniciativa.
            if (valorComboBox1.Equals("Otro tipo (especifique)", StringComparison.OrdinalIgnoreCase))
            {
                txt_otro_tipo_segundo_dictamen_especifique.Enabled = true;
                txt_otro_tipo_segundo_dictamen_especifique.BackColor = Color.Honeydew;

            }
            else
            {
                txt_otro_tipo_segundo_dictamen_especifique.Enabled = false;
                txt_otro_tipo_segundo_dictamen_especifique.BackColor = Color.LightGray;
                txt_otro_tipo_segundo_dictamen_especifique.Text = "";
            }

        }
        private void Cmb_sentido_resolucion_segundo_dictamen()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_DICTAMEN";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_sentido_resolucion_segundo_dictamen.DataSource = dataTable;
                    cmb_sentido_resolucion_segundo_dictamen.DisplayMember = "descripcion";

                    cmb_sentido_resolucion_segundo_dictamen.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_sentido_resolucion_segundo_dictamen.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_sentido_resolucion_segundo_dictamen.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_sentido_resolucion_segundo_dictamen.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_sentido_resolucion_segundo_dictamen_Validating(object sender, CancelEventArgs e)
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
        private void cmb_sentido_resolucion_segundo_dictamen_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_sentido_resolucion_segundo_dictamen.Text.Trim();

            // Desbloquear Otro estatus de la iniciativa.
            if (valorComboBox1.Equals("Otro tipo (especifique)", StringComparison.OrdinalIgnoreCase))
            {
                txt_otro_sentido_resolucion_segundo_dictamen_especifique.Enabled = true;
                txt_otro_sentido_resolucion_segundo_dictamen_especifique.BackColor = Color.Honeydew;

            }
            else
            {
                txt_otro_sentido_resolucion_segundo_dictamen_especifique.Enabled = false;
                txt_otro_sentido_resolucion_segundo_dictamen_especifique.BackColor = Color.LightGray;
                txt_otro_sentido_resolucion_segundo_dictamen_especifique.Text = "";
            }
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

        // PLENO --------------------------------------------------------------------------------------------------------------------------
               
        private void cmb_sentido_resolucion_pleno_iniciativa_Validating(object sender, CancelEventArgs e)
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
        private void dtp_fecha_resolucion_pleno_iniciativa_CloseUp(object sender, EventArgs e)
        {
            DateTime fechaResolución = dtp_fecha_resolucion_pleno_iniciativa.Value;
            DateTime fechaPresentaciónini = dtp_fecha_sesion_presentacion_iniciativa.Value;

            if (fechaResolución < fechaPresentaciónini)
            {
                MessageBox.Show("La fecha de resolución debe ser igual o mayor a la fecha de sesión que se presento la iniciativa.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Opcional: Resetear la fecha de publicación
                dtp_fecha_publicacion_gaceta_oficial_iniciativa.Value = DateTime.Now; // o una fecha predeterminada

                // Vaciar el campo de fecha
                dtp_fecha_resolucion_pleno_iniciativa.CustomFormat = " ";  // Deja en blanco el campo
                dtp_fecha_resolucion_pleno_iniciativa.Format = DateTimePickerFormat.Custom;  // Establece formato personalizado vacío
            }
        }

        // VOTACIONES PLENARIAS------------------------------------------------------------------------------------------------------------

        private void txt_votaciones_pleno_a_favor_iniciativa_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalVotaciones();

        }
        private void txt_votaciones_pleno_en_contra_iniciativa_vp_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalVotaciones();

        }
        private void txt_votaciones_pleno_abstencion_iniciativa_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalVotaciones();

        }
        private void txt_total_votaciones_pleno_iniciativa_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalVotaciones();

        }
        private void CalcularTotalVotaciones()
        {
            // Inicializar las variables
            int aFavor = 0, enContra = 0, abstencion = 0;

            // Verificar que los textos no estén vacíos y convertir a número
            if (!string.IsNullOrEmpty(txt_votaciones_pleno_a_favor_iniciativa.Text))
                int.TryParse(txt_votaciones_pleno_a_favor_iniciativa.Text, out aFavor);

            if (!string.IsNullOrEmpty(txt_votaciones_pleno_en_contra_iniciativa_vp.Text))
                int.TryParse(txt_votaciones_pleno_en_contra_iniciativa_vp.Text, out enContra);

            if (!string.IsNullOrEmpty(txt_votaciones_pleno_abstencion_iniciativa.Text))
                int.TryParse(txt_votaciones_pleno_abstencion_iniciativa.Text, out abstencion);

            // Calcular el total
            int total = aFavor + enContra + abstencion;

            // Mostrar el resultado en el TextBox total
            txt_total_votaciones_pleno_iniciativa.Text = total.ToString();

            // Obtener las cantidades de distritos y diputaciones
            int distritos = 0, plurinominales = 0;

            // Solo intentar convertir si los campos no están vacíos
            int.TryParse(Txt_distritos_uninominales.Text, out distritos);
            int.TryParse(Txt_diputaciones_plurinominales.Text, out plurinominales);

            // Verificar que el total no supere la suma de distritos y plurinominales
            if (total > (distritos + plurinominales))
            {
                // Mostrar el mensaje de error
                MessageBox.Show("El total debe ser igual o menor a la suma de los distritos uninominales y diputaciones plurinominales.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Limpiar los campos de votaciones
                txt_votaciones_pleno_a_favor_iniciativa.Clear();
                txt_votaciones_pleno_en_contra_iniciativa_vp.Clear();
                txt_votaciones_pleno_abstencion_iniciativa.Clear();

                // Opcional: Restablecer el total a 0
                txt_total_votaciones_pleno_iniciativa.Text = "0";
            }
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

        // PODER EJECUTIVO --------------------------------------------------------------------------------------------------------------

        private void dtp_fecha_remision_ejecutivo_iniciativa_CloseUp(object sender, EventArgs e)
        {
            DateTime fechaRemision = dtp_fecha_remision_ejecutivo_iniciativa.Value;
            DateTime fechaResolucionPleno = dtp_fecha_resolucion_pleno_iniciativa.Value;

            if (fechaRemision < fechaResolucionPleno)
            {
                MessageBox.Show("La fecha de remisión debe ser igual o mayor a la fecha de resolución pleno.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Opcional: Resetear la fecha de publicación
                dtp_fecha_publicacion_gaceta_oficial_iniciativa.Value = DateTime.Now; // o una fecha predeterminada

                // Vaciar el campo de fecha
                dtp_fecha_remision_ejecutivo_iniciativa.CustomFormat = " ";  // Deja en blanco el campo
                dtp_fecha_remision_ejecutivo_iniciativa.Format = DateTimePickerFormat.Custom;  // Establece formato personalizado vacío
            }
        }
        private void Cmb_sentido_resolucion_ejecutivo_iniciativa()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_SENT_RESOLUCION";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_sentido_resolucion_ejecutivo_iniciativa.DataSource = dataTable;
                    cmb_sentido_resolucion_ejecutivo_iniciativa.DisplayMember = "descripcion";

                    cmb_sentido_resolucion_ejecutivo_iniciativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_sentido_resolucion_ejecutivo_iniciativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_sentido_resolucion_ejecutivo_iniciativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_sentido_resolucion_ejecutivo_iniciativa.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_sentido_resolucion_ejecutivo_iniciativa_Validating(object sender, CancelEventArgs e)
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
        private void cmb_sentido_resolucion_ejecutivo_iniciativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_sentido_resolucion_ejecutivo_iniciativa.Text.Trim();

            // Desbloquear Gaceta o periodico oficial
            if (valorComboBox1.Equals("Aprobado", StringComparison.OrdinalIgnoreCase))
            {
                // Desbloquea dtp fecha de publicación
                dtp_fecha_publicacion_gaceta_oficial_iniciativa.Enabled = true;
                dtp_fecha_publicacion_gaceta_oficial_iniciativa.BackColor = Color.Honeydew;

            }
            else
            {
                // Desbloquea dtp fecha de publicación
                dtp_fecha_publicacion_gaceta_oficial_iniciativa.Enabled = false;
                dtp_fecha_publicacion_gaceta_oficial_iniciativa.BackColor = Color.LightGray;
                dtp_fecha_publicacion_gaceta_oficial_iniciativa.Text = "";

            }
        }

        // GACETA O PERIODICO OFICIAL ---------------------------------------------------------------------------------------------------
                
        // Método para comparar fechas        
        private void dtp_fecha_publicacion_gaceta_oficial_iniciativa_CloseUp(object sender, EventArgs e)
        {
            DateTime fechaPublicacion = dtp_fecha_publicacion_gaceta_oficial_iniciativa.Value;
            DateTime fechaRemision = dtp_fecha_remision_ejecutivo_iniciativa.Value;

            if (fechaPublicacion < fechaRemision)
            {
                MessageBox.Show("La fecha de publicación debe ser igual o mayor a la fecha de remisión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Opcional: Resetear la fecha de publicación
                dtp_fecha_publicacion_gaceta_oficial_iniciativa.Value = DateTime.Now; // o una fecha predeterminada

                // Vaciar el campo de fecha
                dtp_fecha_publicacion_gaceta_oficial_iniciativa.CustomFormat = " ";  // Deja en blanco el campo
                dtp_fecha_publicacion_gaceta_oficial_iniciativa.Format = DateTimePickerFormat.Custom;  // Establece formato personalizado vacío
            }
        }

    }

}
