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

        // cmb_Estatus_iniciat
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


        //------------------------------------------------------------------
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

        // txt_nombre_iniciativa
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
