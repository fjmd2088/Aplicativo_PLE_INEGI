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
    public partial class FormRegistros: Form
    {
        //CARACTERIZACIÓN INICIAL ---------------------------------------------------------------------------------------------------

        // ---------------------------  Presentación ----------------------

        private void Cmb_cond_presentacion_denuncia_declaracion_procedencia_legislatura_actual()
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

                    cmb_cond_presentacion_denuncia_declaracion_procedencia_legislatura_actual.DataSource = dataTable;
                    cmb_cond_presentacion_denuncia_declaracion_procedencia_legislatura_actual.DisplayMember = "descripcion";

                    cmb_cond_presentacion_denuncia_declaracion_procedencia_legislatura_actual.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_presentacion_denuncia_declaracion_procedencia_legislatura_actual.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_presentacion_denuncia_declaracion_procedencia_legislatura_actual.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_presentacion_denuncia_declaracion_procedencia_legislatura_actual.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_presentacion_denuncia_declaracion_procedencia_legislatura_actual_Validating(object sender, CancelEventArgs e)
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
        private void cmb_cond_presentacion_denuncia_declaracion_procedencia_legislatura_actual_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_cond_presentacion_denuncia_declaracion_procedencia_legislatura_actual.Text.Trim();

            // Bloquea la condición de la debnucia
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                cmb_cond_presentacion_denuncia_declaracion_procedencia_periodo.Enabled = false;
                cmb_cond_presentacion_denuncia_declaracion_procedencia_periodo.BackColor = Color.LightGray;
                cmb_cond_presentacion_denuncia_declaracion_procedencia_periodo.Text = "";

            }
            else
            {
                cmb_cond_presentacion_denuncia_declaracion_procedencia_periodo.Enabled = true;
                cmb_cond_presentacion_denuncia_declaracion_procedencia_periodo.BackColor = Color.Honeydew;
                cmb_cond_presentacion_denuncia_declaracion_procedencia_periodo.Text = "";

            }
           
            // Bloquea la condicion de la denucnia
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                cmb_numero_legislatura_presentacion_denuncia_declaracion_procedencia.Enabled = true;
                cmb_numero_legislatura_presentacion_denuncia_declaracion_procedencia.BackColor = Color.Honeydew;
                cmb_numero_legislatura_presentacion_denuncia_declaracion_procedencia.Text = "";

            }
            else
            {
                cmb_numero_legislatura_presentacion_denuncia_declaracion_procedencia.Enabled = false;
                cmb_numero_legislatura_presentacion_denuncia_declaracion_procedencia.BackColor = Color.LightGray;
                cmb_numero_legislatura_presentacion_denuncia_declaracion_procedencia.Text = "";

            }
            // Desbloquea la condición actualización del estatus de la denuncia de declaración
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo.Enabled = true;
                cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo.BackColor = Color.Honeydew;
                cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo.Text = "";

            }
            else
            {
                cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo.Enabled = false;
                cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo.BackColor = Color.LightGray;
                cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo.Text = "";

            }
            // Bloquea la fcha de ingreso de la denuncia dee declaración de procedencia
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                dtp_fecha_ingreso_denuncia_declaracion_procedencia_oficialia_partes.Enabled = false;
                dtp_fecha_ingreso_denuncia_declaracion_procedencia_oficialia_partes.BackColor = Color.LightGray;
                dtp_fecha_ingreso_denuncia_declaracion_procedencia_oficialia_partes.Text = "";

            }
            else
            {
                dtp_fecha_ingreso_denuncia_declaracion_procedencia_oficialia_partes.Enabled = true;
                dtp_fecha_ingreso_denuncia_declaracion_procedencia_oficialia_partes.BackColor = Color.Honeydew;
                dtp_fecha_ingreso_denuncia_declaracion_procedencia_oficialia_partes.Text = "";

            }
            // Desbloquea Condición de ser una persona legisladora de la legislatura actual:
            if (valorComboBox1.Equals("Si", StringComparison.OrdinalIgnoreCase))
            {
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_declaracion_procedencia.Enabled = true;
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_declaracion_procedencia.BackColor = Color.Honeydew;
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_declaracion_procedencia.Text = "";

            }
            else
            {
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_declaracion_procedencia.Enabled = false;
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_declaracion_procedencia.BackColor = Color.LightGray;
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_declaracion_procedencia.Text = "";

            }
        }

        private void Cmb_cond_presentacion_denuncia_declaracion_procedencia_periodo()
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

                    cmb_cond_presentacion_denuncia_declaracion_procedencia_periodo.DataSource = dataTable;
                    cmb_cond_presentacion_denuncia_declaracion_procedencia_periodo.DisplayMember = "descripcion";

                    cmb_cond_presentacion_denuncia_declaracion_procedencia_periodo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_presentacion_denuncia_declaracion_procedencia_periodo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_presentacion_denuncia_declaracion_procedencia_periodo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_presentacion_denuncia_declaracion_procedencia_periodo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_presentacion_denuncia_declaracion_procedencia_periodo_Validating(object sender, CancelEventArgs e)
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
        private void cmb_cond_presentacion_denuncia_declaracion_procedencia_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_cond_presentacion_denuncia_declaracion_procedencia_periodo.Text.Trim();

            // Desbloquea la condición actualización del estatus de la denuncia de declaración
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo.Enabled = true;
                cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo.BackColor = Color.Honeydew;
                cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo.Text = "";

            }
            else
            {
                cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo.Enabled = false;
                cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo.BackColor = Color.LightGray;
                cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo.Text = "";

            }
            // Bloquea la fcha de ingreso de la denuncia dee declaración de procedencia
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                dtp_fecha_ingreso_denuncia_declaracion_procedencia_oficialia_partes.Enabled = false;
                dtp_fecha_ingreso_denuncia_declaracion_procedencia_oficialia_partes.BackColor = Color.LightGray;
                dtp_fecha_ingreso_denuncia_declaracion_procedencia_oficialia_partes.Text = "";

            }
            else
            {
                dtp_fecha_ingreso_denuncia_declaracion_procedencia_oficialia_partes.Enabled = true;
                dtp_fecha_ingreso_denuncia_declaracion_procedencia_oficialia_partes.BackColor = Color.Honeydew;
                dtp_fecha_ingreso_denuncia_declaracion_procedencia_oficialia_partes.Text = "";

            }
            // Se desbloquea la fecha de procedncia en que se determino
            if (valorComboBox1.Equals("Si", StringComparison.OrdinalIgnoreCase))
            {
                dtp_fecha_procedencia_denuncia_declaracion_procedencia.Enabled = true;
                dtp_fecha_procedencia_denuncia_declaracion_procedencia.BackColor = Color.Honeydew;
                dtp_fecha_procedencia_denuncia_declaracion_procedencia.Text = "";

            }
            else
            {
                dtp_fecha_procedencia_denuncia_declaracion_procedencia.Enabled = false;
                dtp_fecha_procedencia_denuncia_declaracion_procedencia.BackColor = Color.LightGray;
                dtp_fecha_procedencia_denuncia_declaracion_procedencia.Text = "";

            }
        }

        private void Cmb_numero_legislatura_presentacion_denuncia_declaracion_procedencia()
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

                    cmb_numero_legislatura_presentacion_denuncia_declaracion_procedencia.DataSource = dataTable;
                    cmb_numero_legislatura_presentacion_denuncia_declaracion_procedencia.DisplayMember = "descripcion";

                    cmb_numero_legislatura_presentacion_denuncia_declaracion_procedencia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_numero_legislatura_presentacion_denuncia_declaracion_procedencia.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_numero_legislatura_presentacion_denuncia_declaracion_procedencia.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_numero_legislatura_presentacion_denuncia_declaracion_procedencia.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_numero_legislatura_presentacion_denuncia_declaracion_procedencia_Validating(object sender, CancelEventArgs e)
        {

        }

        // txt_turno_denuncia_declaracion_procedencia
        private void txt_turno_denuncia_declaracion_procedencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }

        }



        // ---------------------------  Estatus --------------------------

        private void Cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo()
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

                    cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo.DataSource = dataTable;
                    cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo.DisplayMember = "descripcion";

                    cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo_Validating(object sender, CancelEventArgs e)
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
        private void cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo.Text.Trim();

            // Desbloquea la condición actualización del estatus de la denuncia de declaración
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                cmb_estatus_denuncia_declaracion_procedencia.Enabled = false;
                cmb_estatus_denuncia_declaracion_procedencia.BackColor = Color.LightGray;
                cmb_estatus_denuncia_declaracion_procedencia.Text = "";

            }
            else
            {
                cmb_estatus_denuncia_declaracion_procedencia.Enabled = true;
                cmb_estatus_denuncia_declaracion_procedencia.BackColor = Color.Honeydew;
                cmb_estatus_denuncia_declaracion_procedencia.Text = "";

            }

            string cadena = "Data Source = DB_PLE.db;Version=3;";

            if (cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo.SelectedItem != null)
            {
                // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
                string valorComboBox = cmb_cond_actualizacion_estatus_denuncia_declaracion_procedencia_periodo.Text.ToString();


                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        string query;

                        switch (valorComboBox)
                        {
                            case "Si":
                                query = "select descripcion from TC_ESTATUS_DENUNCIA where id_estatus_denuncia in (5,6)";
                                break;
                                                           
                            default:
                                query = "select descripcion from TC_ESTATUS_DENUNCIA where id_estatus_denuncia in (10)";
                                break;
                        }

                        // comando de sql
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        cmb_estatus_denuncia_declaracion_procedencia.DataSource = dataTable;
                        cmb_estatus_denuncia_declaracion_procedencia.DisplayMember = "descripcion";

                        cmb_estatus_denuncia_declaracion_procedencia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_estatus_denuncia_declaracion_procedencia.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_estatus_denuncia_declaracion_procedencia.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_estatus_denuncia_declaracion_procedencia.SelectedIndex = -1; // Aquí se establece como vacío

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
            else
            {
            }
        }


        private void Cmb_estatus_denuncia_declaracion_procedencia()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_ESTATUS_DENUNCIA";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_estatus_denuncia_declaracion_procedencia.DataSource = dataTable;
                    cmb_estatus_denuncia_declaracion_procedencia.DisplayMember = "descripcion";

                    cmb_estatus_denuncia_declaracion_procedencia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_estatus_denuncia_declaracion_procedencia.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_estatus_denuncia_declaracion_procedencia.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_estatus_denuncia_declaracion_procedencia.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_estatus_denuncia_declaracion_procedencia_Validating(object sender, CancelEventArgs e)
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
        private void cmb_estatus_denuncia_declaracion_procedencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_estatus_denuncia_declaracion_procedencia.Text.Trim();

            // Desbloquea la causa improcedencia
            if (valorComboBox1.Equals("Improcedente (especifique)", StringComparison.OrdinalIgnoreCase))
            {
                txt_improcedente_estatus_denuncia_declaracion_procedencia_especifique.Enabled = true;
                txt_improcedente_estatus_denuncia_declaracion_procedencia_especifique.BackColor = Color.Honeydew;
                txt_improcedente_estatus_denuncia_declaracion_procedencia_especifique.Text = "";

            }
            else
            {
                txt_improcedente_estatus_denuncia_declaracion_procedencia_especifique.Enabled = false;
                txt_improcedente_estatus_denuncia_declaracion_procedencia_especifique.BackColor = Color.LightGray;
                txt_improcedente_estatus_denuncia_declaracion_procedencia_especifique.Text = "";

            }

            // Desbloquea otro estatus
            if (valorComboBox1.Equals("Otro estatus (especifique)", StringComparison.OrdinalIgnoreCase))
            {
                txt_otro_estatus_denuncia_declaracion_procedencia_especifique.Enabled = true;
                txt_otro_estatus_denuncia_declaracion_procedencia_especifique.BackColor = Color.Honeydew;
                txt_otro_estatus_denuncia_declaracion_procedencia_especifique.Text = "";

            }
            else
            {
                txt_otro_estatus_denuncia_declaracion_procedencia_especifique.Enabled = false;
                txt_otro_estatus_denuncia_declaracion_procedencia_especifique.BackColor = Color.LightGray;
                txt_otro_estatus_denuncia_declaracion_procedencia_especifique.Text = "";

            }
            // Se desbloquea la fecha de procedncia en que se determino
            if (valorComboBox1.Equals("En trámite en instancia substanciadora", StringComparison.OrdinalIgnoreCase) || (valorComboBox1.Equals("Concluida", StringComparison.OrdinalIgnoreCase)))
            {
                dtp_fecha_procedencia_denuncia_declaracion_procedencia.Enabled = true;
                dtp_fecha_procedencia_denuncia_declaracion_procedencia.BackColor = Color.Honeydew;
                dtp_fecha_procedencia_denuncia_declaracion_procedencia.Text = "";

            }
            else
            {
                dtp_fecha_procedencia_denuncia_declaracion_procedencia.Enabled = false;
                dtp_fecha_procedencia_denuncia_declaracion_procedencia.BackColor = Color.LightGray;
                dtp_fecha_procedencia_denuncia_declaracion_procedencia.Text = "";

            }
            // Se desbloquea la fecha de resolucion del procedimiento 
            if (valorComboBox1.Equals("Concluida", StringComparison.OrdinalIgnoreCase))
            {
                dtp_fecha_resolucion_pleno_declaracion_procedencia.Enabled = true;
                dtp_fecha_resolucion_pleno_declaracion_procedencia.BackColor = Color.Honeydew;
                dtp_fecha_resolucion_pleno_declaracion_procedencia.Text = "";

            }
            else
            {
                dtp_fecha_resolucion_pleno_declaracion_procedencia.Enabled = false;
                dtp_fecha_resolucion_pleno_declaracion_procedencia.BackColor = Color.LightGray;
                dtp_fecha_resolucion_pleno_declaracion_procedencia.Text = "";

            }
            // Se desbloquea CMB sentido de reesolución del procedimiento de declaración de procedencia. 
            if (valorComboBox1.Equals("Concluida", StringComparison.OrdinalIgnoreCase))
            {
                cmb_sentido_resolucion_pleno_declaracion_procedencia.Enabled = true;
                cmb_sentido_resolucion_pleno_declaracion_procedencia.BackColor = Color.Honeydew;
                cmb_sentido_resolucion_pleno_declaracion_procedencia.Text = "";

            }
            else
            {
                cmb_sentido_resolucion_pleno_declaracion_procedencia.Enabled = false;
                cmb_sentido_resolucion_pleno_declaracion_procedencia.BackColor = Color.LightGray;
                cmb_sentido_resolucion_pleno_declaracion_procedencia.Text = "";

            }
            // Se desbloquea SUMATORIAS. 
            if (valorComboBox1.Equals("Concluida", StringComparison.OrdinalIgnoreCase))
            {
                txt_votaciones_pleno_a_favor_declaracion_procedencia.Enabled = true;
                txt_votaciones_pleno_a_favor_declaracion_procedencia.BackColor = Color.Honeydew;
                txt_votaciones_pleno_a_favor_declaracion_procedencia.Text = "";

                txt_votaciones_pleno_en_contra_declaracion_procedencia.Enabled = true;
                txt_votaciones_pleno_en_contra_declaracion_procedencia.BackColor = Color.Honeydew;
                txt_votaciones_pleno_en_contra_declaracion_procedencia.Text = "";

                txt_votaciones_pleno_abstencion_declaracion_procedencia.Enabled = true;
                txt_votaciones_pleno_abstencion_declaracion_procedencia.BackColor = Color.Honeydew;
                txt_votaciones_pleno_abstencion_declaracion_procedencia.Text = "";

                txt_total_votaciones_pleno_declaracion_procedencia.Enabled = true;
                txt_total_votaciones_pleno_declaracion_procedencia.BackColor = Color.Honeydew;
                txt_total_votaciones_pleno_declaracion_procedencia.Text = "";

            }
            else
            {
                txt_votaciones_pleno_a_favor_declaracion_procedencia.Enabled = false;
                txt_votaciones_pleno_a_favor_declaracion_procedencia.BackColor = Color.LightGray;
                txt_votaciones_pleno_a_favor_declaracion_procedencia.Text = "";

                txt_votaciones_pleno_en_contra_declaracion_procedencia.Enabled = false;
                txt_votaciones_pleno_en_contra_declaracion_procedencia.BackColor = Color.LightGray;
                txt_votaciones_pleno_en_contra_declaracion_procedencia.Text = "";

                txt_votaciones_pleno_abstencion_declaracion_procedencia.Enabled = false;
                txt_votaciones_pleno_abstencion_declaracion_procedencia.BackColor = Color.LightGray;
                txt_votaciones_pleno_abstencion_declaracion_procedencia.Text = "";

                txt_total_votaciones_pleno_declaracion_procedencia.Enabled = false;
                txt_total_votaciones_pleno_declaracion_procedencia.BackColor = Color.LightGray;
                txt_total_votaciones_pleno_declaracion_procedencia.Text = "";

            }
            // Se desbloquea priemr nombre de las caracteristicas sociodemograficaas. 
            if (valorComboBox1.Equals("Concluida", StringComparison.OrdinalIgnoreCase))
            {
                // Nombre
                txt_nombre_1_persona_servidora_publica_declaracion_procedencia.Enabled = true;
                txt_nombre_1_persona_servidora_publica_declaracion_procedencia.BackColor = Color.Honeydew;
                txt_nombre_1_persona_servidora_publica_declaracion_procedencia.Text = "";
                // Apellido
                txt_apellido_1_persona_servidora_publica_declaracion_procedencia.Enabled = true;
                txt_apellido_1_persona_servidora_publica_declaracion_procedencia.BackColor = Color.Honeydew;
                txt_apellido_1_persona_servidora_publica_declaracion_procedencia.Text = "";
                // Sexo
                cmb_sexo_persona_servidora_publica_declaracion_procedencia.Enabled = true;
                cmb_sexo_persona_servidora_publica_declaracion_procedencia.BackColor = Color.Honeydew;
                cmb_sexo_persona_servidora_publica_declaracion_procedencia.Text = "";

            }
            else
            {
                // Nombre
                txt_nombre_1_persona_servidora_publica_declaracion_procedencia.Enabled = false;
                txt_nombre_1_persona_servidora_publica_declaracion_procedencia.BackColor = Color.LightGray;
                txt_nombre_1_persona_servidora_publica_declaracion_procedencia.Text = "";
                // Apellido
                txt_apellido_1_persona_servidora_publica_declaracion_procedencia.Enabled = false;
                txt_apellido_1_persona_servidora_publica_declaracion_procedencia.BackColor = Color.LightGray;
                txt_apellido_1_persona_servidora_publica_declaracion_procedencia.Text = "";
                // Sexo
                cmb_sexo_persona_servidora_publica_declaracion_procedencia.Enabled = false;
                cmb_sexo_persona_servidora_publica_declaracion_procedencia.BackColor = Color.LightGray;
                cmb_sexo_persona_servidora_publica_declaracion_procedencia.Text = "";
            }
            // Se desbloquea cargo desempeñado 
            if (valorComboBox1.Equals("Concluida", StringComparison.OrdinalIgnoreCase))
            {
                cmb_cargo_persona_servidora_publica_declaracion_procedencia.Enabled = true;
                cmb_cargo_persona_servidora_publica_declaracion_procedencia.BackColor = Color.Honeydew;
                cmb_cargo_persona_servidora_publica_declaracion_procedencia.Text = "";

            }
            else
            {
                cmb_cargo_persona_servidora_publica_declaracion_procedencia.Enabled = false;
                cmb_cargo_persona_servidora_publica_declaracion_procedencia.BackColor = Color.LightGray;
                cmb_cargo_persona_servidora_publica_declaracion_procedencia.Text = "";

            }
        }
        
        // txt_improcedente_estatus_denuncia_declaracion_procedencia_especifique
        private void txt_improcedente_estatus_denuncia_declaracion_procedencia_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_improcedente_estatus_denuncia_declaracion_procedencia_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_improcedente_estatus_denuncia_declaracion_procedencia_especifique.Text = txt_improcedente_estatus_denuncia_declaracion_procedencia_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_improcedente_estatus_denuncia_declaracion_procedencia_especifique.SelectionStart = txt_improcedente_estatus_denuncia_declaracion_procedencia_especifique.Text.Length;
        }
        
        // txt_otro_estatus_denuncia_declaracion_procedencia_especifique
        private void txt_otro_estatus_denuncia_declaracion_procedencia_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_otro_estatus_denuncia_declaracion_procedencia_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_otro_estatus_denuncia_declaracion_procedencia_especifique.Text = txt_otro_estatus_denuncia_declaracion_procedencia_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_otro_estatus_denuncia_declaracion_procedencia_especifique.SelectionStart = txt_otro_estatus_denuncia_declaracion_procedencia_especifique.Text.Length;
        }

        // ---------------------------  Fecha Ingreso ------------------------------

        private void dtp_fecha_ingreso_denuncia_declaracion_procedencia_oficialia_partes_CloseUp(object sender, EventArgs e)
        {
            // Obtener las fechas seleccionadas
            DateTime fechaGaceta = dtp_fecha_ingreso_denuncia_declaracion_procedencia_oficialia_partes.Value.Date; // Solo la fecha, sin hora
            DateTime fechaRemPE = dtp_fecha_termino_informacion_reportada.Value.Date; // Solo la fecha, sin hora

            // Validar si la fecha de publicación es menor que la fecha de remisión.
            if (fechaGaceta > fechaRemPE)
            {
                // Mostrar mensaje de error
                MessageBox.Show("Fecha de resolución del procedimiento de declaración de procedencia debe ser igual o menor a la fecha de información reportada en datos generales.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Vaciar el campo de fecha
                dtp_fecha_ingreso_denuncia_declaracion_procedencia_oficialia_partes.CustomFormat = " ";  // Vaciar el campo
                dtp_fecha_ingreso_denuncia_declaracion_procedencia_oficialia_partes.Format = DateTimePickerFormat.Custom;  // Establecer formato personalizado vacío
            }
            else
            {
                // Si la fecha es válida (igual o mayor que la fecha de remisión), restaurar el formato de fecha corta
                dtp_fecha_ingreso_denuncia_declaracion_procedencia_oficialia_partes.Format = DateTimePickerFormat.Short;
            }
        }

        // ---------------------------  Fecha procedencia --------------------------

        private void dtp_fecha_procedencia_denuncia_declaracion_procedencia_CloseUp(object sender, EventArgs e)
        {
            // Obtener las fechas seleccionadas
            DateTime fechaGaceta = dtp_fecha_procedencia_denuncia_declaracion_procedencia.Value.Date; // Solo la fecha, sin hora
            DateTime fechaRemPE = dtp_fecha_ingreso_denuncia_declaracion_procedencia_oficialia_partes.Value.Date; // Solo la fecha, sin hora

            // Validar si la fecha de publicación es menor que la fecha de remisión.
            if (fechaGaceta < fechaRemPE)
            {
                // Mostrar mensaje de error
                MessageBox.Show("Fecha en la que se determinó la procedencia de la denuncia de declaración de procedencia debe ser igual o mayor a la fecha de ingreso de la denuncia de declaración de procedencia a oficialía de partes.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Vaciar el campo de fecha
                dtp_fecha_procedencia_denuncia_declaracion_procedencia.CustomFormat = " ";  // Vaciar el campo
                dtp_fecha_procedencia_denuncia_declaracion_procedencia.Format = DateTimePickerFormat.Custom;  // Establecer formato personalizado vacío
            }
            else
            {
                // Si la fecha es válida (igual o mayor que la fecha de remisión), restaurar el formato de fecha corta
                dtp_fecha_procedencia_denuncia_declaracion_procedencia.Format = DateTimePickerFormat.Short;
            }
        }

        // PLENO ----------------------------------------------------------------------------------------------------------

        // ---------------------------  Resolución ---------------------------------

        private void dtp_fecha_resolucion_pleno_declaracion_procedencia_CloseUp(object sender, EventArgs e)
        {
            // Obtener las fechas seleccionadas
            DateTime fechaGaceta = dtp_fecha_resolucion_pleno_declaracion_procedencia.Value.Date; // Solo la fecha, sin hora
            DateTime fechaRemPE = dtp_fecha_procedencia_denuncia_declaracion_procedencia.Value.Date; // Solo la fecha, sin hora

            // Validar si la fecha de publicación es menor que la fecha de remisión.
            if (fechaGaceta < fechaRemPE)
            {
                // Mostrar mensaje de error
                MessageBox.Show("Fecha de resolución del procedimiento de declaración de procedencia debe ser igual o mayor a la fecha en la que se determinó la procedencia de la denuncia de declaración de procedencia.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Vaciar el campo de fecha
                dtp_fecha_resolucion_pleno_declaracion_procedencia.CustomFormat = " ";  // Vaciar el campo
                dtp_fecha_resolucion_pleno_declaracion_procedencia.Format = DateTimePickerFormat.Custom;  // Establecer formato personalizado vacío
            }
            else
            {
                // Si la fecha es válida (igual o mayor que la fecha de remisión), restaurar el formato de fecha corta
                dtp_fecha_resolucion_pleno_declaracion_procedencia.Format = DateTimePickerFormat.Short;
            }
        }

        private void Cmb_sentido_resolucion_pleno_declaracion_procedencia()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_SENT_RES_PLENO";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_sentido_resolucion_pleno_declaracion_procedencia.DataSource = dataTable;
                    cmb_sentido_resolucion_pleno_declaracion_procedencia.DisplayMember = "descripcion";

                    cmb_sentido_resolucion_pleno_declaracion_procedencia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_sentido_resolucion_pleno_declaracion_procedencia.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_sentido_resolucion_pleno_declaracion_procedencia.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_sentido_resolucion_pleno_declaracion_procedencia.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_sentido_resolucion_pleno_declaracion_procedencia_Validating(object sender, CancelEventArgs e)
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
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // ---------------------------  Votaciones plenarias ----------------------

        // txt_votaciones_pleno_a_favor_declaracion_procedencia
        private void txt_votaciones_pleno_a_favor_declaracion_procedencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }

        // txt_votaciones_pleno_en_contra_declaracion_procedencia
        private void txt_votaciones_pleno_en_contra_declaracion_procedencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }

        // txt_votaciones_pleno_abstencion_declaracion_procedencia
        private void txt_votaciones_pleno_abstencion_declaracion_procedencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }

        // SUMATORIAS

        private void txt_votaciones_pleno_a_favor_declaracion_procedencia_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalVotaciones_Proc();
        }
        private void txt_votaciones_pleno_en_contra_declaracion_procedencia_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalVotaciones_Proc();
        }
        private void txt_votaciones_pleno_abstencion_declaracion_procedencia_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalVotaciones_Proc();
        }
        private void CalcularTotalVotaciones_Proc()
        {
            // Inicializar las variables
            int aFavor = 0, enContra = 0, abstencion = 0;

            // Verificar que los textos no estén vacíos y convertir a número
            if (!string.IsNullOrEmpty(txt_votaciones_pleno_a_favor_declaracion_procedencia.Text))
                int.TryParse(txt_votaciones_pleno_a_favor_declaracion_procedencia.Text, out aFavor);

            if (!string.IsNullOrEmpty(txt_votaciones_pleno_en_contra_declaracion_procedencia.Text))
                int.TryParse(txt_votaciones_pleno_en_contra_declaracion_procedencia.Text, out enContra);

            if (!string.IsNullOrEmpty(txt_votaciones_pleno_abstencion_declaracion_procedencia.Text))
                int.TryParse(txt_votaciones_pleno_abstencion_declaracion_procedencia.Text, out abstencion);

            // Calcular el total
            int total = aFavor + enContra + abstencion;

            // Mostrar el resultado en el TextBox total
            txt_total_votaciones_pleno_declaracion_procedencia.Text = total.ToString();

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
                txt_votaciones_pleno_a_favor_declaracion_procedencia.Clear();
                txt_votaciones_pleno_en_contra_declaracion_procedencia.Clear();
                txt_votaciones_pleno_abstencion_declaracion_procedencia.Clear();

                // Restablecer el total a 0
                txt_total_votaciones_pleno_declaracion_procedencia.Text = "0";
            }
        }

        // ------------------------------ Características sociodemográficas de la persona servidora pública sujeta a procedimiento de declaración de procedencia ------------

        private void Cmb_sexo_persona_servidora_publica_declaracion_procedencia()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_SEXO";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_sexo_persona_servidora_publica_declaracion_procedencia.DataSource = dataTable;
                    cmb_sexo_persona_servidora_publica_declaracion_procedencia.DisplayMember = "descripcion";

                    cmb_sexo_persona_servidora_publica_declaracion_procedencia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_sexo_persona_servidora_publica_declaracion_procedencia.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_sexo_persona_servidora_publica_declaracion_procedencia.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_sexo_persona_servidora_publica_declaracion_procedencia.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_sexo_persona_servidora_publica_declaracion_procedencia_Validating(object sender, CancelEventArgs e)
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

        // txt_nombre_1_persona_servidora_publica_declaracion_procedencia
        private void txt_nombre_1_persona_servidora_publica_declaracion_procedencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_nombre_1_persona_servidora_publica_declaracion_procedencia_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_nombre_1_persona_servidora_publica_declaracion_procedencia.Text = txt_nombre_1_persona_servidora_publica_declaracion_procedencia.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_nombre_1_persona_servidora_publica_declaracion_procedencia.SelectionStart = txt_nombre_1_persona_servidora_publica_declaracion_procedencia.Text.Length;
            
            // Desbloquear txt_nombre_2_personal_apoyo, cambiar su color de fondo, o borrarlo y deshabilitarlo
            if (!string.IsNullOrEmpty(txt_nombre_1_persona_servidora_publica_declaracion_procedencia.Text))
            {
                txt_nombre_2_persona_servidora_publica_declaracion_procedencia.Enabled = true;
                txt_nombre_2_persona_servidora_publica_declaracion_procedencia.BackColor = Color.Honeydew;
            }
            else
            {
                // Si txt_nombre_2_persona_servidora_publica_juicio_politico está vacío, borrar y deshabilitar txt_nombre_2_personal_apoyo
                txt_nombre_2_persona_servidora_publica_declaracion_procedencia.Text = string.Empty;
                txt_nombre_2_persona_servidora_publica_declaracion_procedencia.Enabled = false;
                txt_nombre_2_persona_servidora_publica_declaracion_procedencia.BackColor = SystemColors.Window; // Restaurar el color predeterminado
            }
        }

        // txt_nombre_2_persona_servidora_publica_declaracion_procedencia
        private void txt_nombre_2_persona_servidora_publica_declaracion_procedencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_nombre_2_persona_servidora_publica_declaracion_procedencia_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_nombre_2_persona_servidora_publica_declaracion_procedencia.Text = txt_nombre_2_persona_servidora_publica_declaracion_procedencia.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_nombre_2_persona_servidora_publica_declaracion_procedencia.SelectionStart = txt_nombre_2_persona_servidora_publica_declaracion_procedencia.Text.Length;
            
            // Desbloquear txt_nombre_3_personal_apoyo, cambiar su color de fondo, o borrarlo y deshabilitarlo
            if (!string.IsNullOrEmpty(txt_nombre_2_persona_servidora_publica_declaracion_procedencia.Text))
            {
                txt_nombre_3_persona_servidora_publica_declaracion_procedencia.Enabled = true;
                txt_nombre_3_persona_servidora_publica_declaracion_procedencia.BackColor = Color.Honeydew;
            }
            else
            {
                // Si txt_nombre_2_persona_servidora_publica_declaracion_procedencia está vacío, borrar y deshabilitar txt_nombre_2_personal_apoyo
                txt_nombre_3_persona_servidora_publica_declaracion_procedencia.Text = string.Empty;
                txt_nombre_3_persona_servidora_publica_declaracion_procedencia.Enabled = false;
                txt_nombre_3_persona_servidora_publica_declaracion_procedencia.BackColor = SystemColors.Window; // Restaurar el color predeterminado
            }
        }

        // txt_nombre_3_persona_servidora_publica_declaracion_procedencia
        private void txt_nombre_3_persona_servidora_publica_declaracion_procedencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_nombre_3_persona_servidora_publica_declaracion_procedencia_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_nombre_3_persona_servidora_publica_declaracion_procedencia.Text = txt_nombre_3_persona_servidora_publica_declaracion_procedencia.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_nombre_3_persona_servidora_publica_declaracion_procedencia.SelectionStart = txt_nombre_3_persona_servidora_publica_declaracion_procedencia.Text.Length;
        }

        // txt_apellido_1_persona_servidora_publica_declaracion_procedencia
        private void txt_apellido_1_persona_servidora_publica_declaracion_procedencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_apellido_1_persona_servidora_publica_declaracion_procedencia_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_apellido_1_persona_servidora_publica_declaracion_procedencia.Text = txt_apellido_1_persona_servidora_publica_declaracion_procedencia.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_apellido_1_persona_servidora_publica_declaracion_procedencia.SelectionStart = txt_apellido_1_persona_servidora_publica_declaracion_procedencia.Text.Length;

            // Desbloquear txt_apellido_1_persona_servidora_publica_juicio_politico, cambiar su color de fondo, o borrarlo y deshabilitarlo
            if (!string.IsNullOrEmpty(txt_apellido_1_persona_servidora_publica_declaracion_procedencia.Text))
            {
                txt_apellido_2_persona_servidora_publica_declaracion_procedencia.Enabled = true;
                txt_apellido_2_persona_servidora_publica_declaracion_procedencia.BackColor = Color.Honeydew;
            }
            else
            {
                // Si txt_apellido_2_persona_servidora_publica_declaracion_procedencia está vacío, borrar y deshabilitar txt_nombre_2_personal_apoyo
                txt_apellido_2_persona_servidora_publica_declaracion_procedencia.Text = string.Empty;
                txt_apellido_2_persona_servidora_publica_declaracion_procedencia.Enabled = false;
                txt_apellido_2_persona_servidora_publica_declaracion_procedencia.BackColor = SystemColors.Window; // Restaurar el color predeterminado
            }
        }

        // txt_apellido_2_persona_servidora_publica_declaracion_procedencia
        private void txt_apellido_2_persona_servidora_publica_declaracion_procedencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_apellido_2_persona_servidora_publica_declaracion_procedencia_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_apellido_2_persona_servidora_publica_declaracion_procedencia.Text = txt_apellido_2_persona_servidora_publica_declaracion_procedencia.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_apellido_2_persona_servidora_publica_declaracion_procedencia.SelectionStart = txt_apellido_2_persona_servidora_publica_declaracion_procedencia.Text.Length;

            // Desbloquear txt_apellido_2_persona_servidora_publica_juicio_politico, cambiar su color de fondo, o borrarlo y deshabilitarlo
            if (!string.IsNullOrEmpty(txt_apellido_2_persona_servidora_publica_declaracion_procedencia.Text))
            {
                txt_apellido_3_persona_servidora_publica_declaracion_procedencia.Enabled = true;
                txt_apellido_3_persona_servidora_publica_declaracion_procedencia.BackColor = Color.Honeydew;
            }
            else
            {
                // Si txt_apellido_3_persona_servidora_publica_declaracion_procedencia está vacío, borrar y deshabilitar txt_nombre_2_personal_apoyo
                txt_apellido_3_persona_servidora_publica_declaracion_procedencia.Text = string.Empty;
                txt_apellido_3_persona_servidora_publica_declaracion_procedencia.Enabled = false;
                txt_apellido_3_persona_servidora_publica_declaracion_procedencia.BackColor = SystemColors.Window; // Restaurar el color predeterminado
            }
        }

        // txt_apellido_3_persona_servidora_publica_declaracion_procedencia
        private void txt_apellido_3_persona_servidora_publica_declaracion_procedencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_apellido_3_persona_servidora_publica_declaracion_procedencia_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_apellido_3_persona_servidora_publica_declaracion_procedencia.Text = txt_apellido_3_persona_servidora_publica_declaracion_procedencia.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_apellido_3_persona_servidora_publica_declaracion_procedencia.SelectionStart = txt_apellido_3_persona_servidora_publica_declaracion_procedencia.Text.Length;
        }

        // ----------- Características sociodemográficas de la persona servidora pública sujeta a procedimiento de declaración de procedencia ------------

        private void Cmb_cargo_persona_servidora_publica_declaracion_procedencia()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_CARGO_SERV_PUBLICO";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cargo_persona_servidora_publica_declaracion_procedencia.DataSource = dataTable;
                    cmb_cargo_persona_servidora_publica_declaracion_procedencia.DisplayMember = "descripcion";

                    cmb_cargo_persona_servidora_publica_declaracion_procedencia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cargo_persona_servidora_publica_declaracion_procedencia.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cargo_persona_servidora_publica_declaracion_procedencia.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cargo_persona_servidora_publica_declaracion_procedencia.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cargo_persona_servidora_publica_declaracion_procedencia_Validating(object sender, CancelEventArgs e)
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
        private void cmb_cargo_persona_servidora_publica_declaracion_procedencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_cargo_persona_servidora_publica_declaracion_procedencia.Text.Trim();

            // Desbloquea nombre e ID de la persona legisladora
            if (valorComboBox1.Equals("Legislador(a) del Congreso de la entidad federativa", StringComparison.OrdinalIgnoreCase))
            {
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_declaracion_procedencia.Enabled = true;
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_declaracion_procedencia.BackColor = Color.Honeydew;
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_declaracion_procedencia.Text = "";

                txt_ID_persona_legisladora_declaracion_procedencia.Enabled = false;
                txt_ID_persona_legisladora_declaracion_procedencia.BackColor = Color.Honeydew;
                txt_ID_persona_legisladora_declaracion_procedencia.Text = "";

            }
            else
            {
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_declaracion_procedencia.Enabled = false;
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_declaracion_procedencia.BackColor = Color.LightGray;
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_declaracion_procedencia.Text = "";

                txt_ID_persona_legisladora_declaracion_procedencia.Enabled = false;
                txt_ID_persona_legisladora_declaracion_procedencia.BackColor = Color.LightGray;
                txt_ID_persona_legisladora_declaracion_procedencia.Text = "";

            }
            // Desbloquea nombre de la institución
            if (valorComboBox1.Equals("Titular de alguna institución  o unidad administrativa de la Administración Pública Estatal (excluyendo, de ser el caso, a la Procuraduría General de Justicia)", StringComparison.OrdinalIgnoreCase) | (valorComboBox1.Equals("Titular de algún otro órgano constitucional autónomo de la entidad federativa (excluyendo al organismo público local electoral, al organismo garante de acceso a la información y protección de datos personales, al organismo público de derechos humanos de la entidad federativa, y, de ser el caso, a la Fiscalía General de la entidad federativa)", StringComparison.OrdinalIgnoreCase) | (valorComboBox1.Equals("Titular de alguna institución o unidad administrativa de la Administración Pública del municipio o demarcación territorial", StringComparison.OrdinalIgnoreCase))))
            {
                txt_nombre_institucion_persona_servidora_publica_declaracion_procedencia.Enabled = true;
                txt_nombre_institucion_persona_servidora_publica_declaracion_procedencia.BackColor = Color.Honeydew;
                txt_nombre_institucion_persona_servidora_publica_declaracion_procedencia.Text = "";

            }
            else
            {
                txt_nombre_institucion_persona_servidora_publica_declaracion_procedencia.Enabled = false;
                txt_nombre_institucion_persona_servidora_publica_declaracion_procedencia.BackColor = Color.LightGray;
                txt_nombre_institucion_persona_servidora_publica_declaracion_procedencia.Text = "";

            }
            // Desbloquea AGGEM
            if (valorComboBox1.Equals("Presidente(a) municipal", StringComparison.OrdinalIgnoreCase) | (valorComboBox1.Equals("Regidor(a)", StringComparison.OrdinalIgnoreCase) |(valorComboBox1.Equals("Síndico(a)", StringComparison.OrdinalIgnoreCase) | (valorComboBox1.Equals("Titular de alguna institución o unidad administrativa de la Administración Pública del municipio o demarcación territorial", StringComparison.OrdinalIgnoreCase) | (valorComboBox1.Equals("Otro cargo del ámbito municipal (especifique)", StringComparison.OrdinalIgnoreCase))))))
            {
                cmb_municipio_persona_servidora_publica_declaracion_procedencia.Enabled = true;
                cmb_municipio_persona_servidora_publica_declaracion_procedencia.BackColor = Color.Honeydew;
                cmb_municipio_persona_servidora_publica_declaracion_procedencia.Text = "";

                txt_AGEM_persona_servidora_publica_declaracion_procedencia.Enabled = false;
                txt_AGEM_persona_servidora_publica_declaracion_procedencia.BackColor = Color.Honeydew;
                txt_AGEM_persona_servidora_publica_declaracion_procedencia.Text = "";

            }
            else
            {
                cmb_municipio_persona_servidora_publica_declaracion_procedencia.Enabled = false;
                cmb_municipio_persona_servidora_publica_declaracion_procedencia.BackColor = Color.LightGray;
                cmb_municipio_persona_servidora_publica_declaracion_procedencia.Text = "";

                txt_AGEM_persona_servidora_publica_declaracion_procedencia.Enabled = false;
                txt_AGEM_persona_servidora_publica_declaracion_procedencia.BackColor = Color.LightGray;
                txt_AGEM_persona_servidora_publica_declaracion_procedencia.Text = "";

            }
            // Desbloquea Otro cargo del ambito
            if (valorComboBox1.Equals("Otro cargo del ámbito estatal (especifique)", StringComparison.OrdinalIgnoreCase))
            {
                txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_estatal_especifique.Enabled = true;
                txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_estatal_especifique.BackColor = Color.Honeydew;
                txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_estatal_especifique.Text = "";


            }
            else
            {
                txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_estatal_especifique.Enabled = false;
                txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_estatal_especifique.BackColor = Color.LightGray;
                txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_estatal_especifique.Text = "";


            }

            // Desbloquea "Otro cargo del ámbito municipal (especifique)"
            if (valorComboBox1.Equals("Otro cargo del ámbito municipal (especifique)", StringComparison.OrdinalIgnoreCase))
            {
                txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_municipal_especifique.Enabled = true;
                txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_municipal_especifique.BackColor = Color.Honeydew;
                txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_municipal_especifique.Text = "";


            }
            else
            {
                txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_municipal_especifique.Enabled = false;
                txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_municipal_especifique.BackColor = Color.LightGray;
                txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_municipal_especifique.Text = "";


            }
        }

        private void Cmb_cond_pertenencia_legislatura_actual_persona_legisladora_declaracion_procedencia()
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

                    cmb_cond_pertenencia_legislatura_actual_persona_legisladora_declaracion_procedencia.DataSource = dataTable;
                    cmb_cond_pertenencia_legislatura_actual_persona_legisladora_declaracion_procedencia.DisplayMember = "descripcion";

                    cmb_cond_pertenencia_legislatura_actual_persona_legisladora_declaracion_procedencia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_pertenencia_legislatura_actual_persona_legisladora_declaracion_procedencia.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_pertenencia_legislatura_actual_persona_legisladora_declaracion_procedencia.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_pertenencia_legislatura_actual_persona_legisladora_declaracion_procedencia.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_pertenencia_legislatura_actual_persona_legisladora_declaracion_procedencia_Validating(object sender, CancelEventArgs e)
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
        private void cmb_cond_pertenencia_legislatura_actual_persona_legisladora_declaracion_procedencia_SelectedIndexChanged(object sender, EventArgs e) 
        {
            string valorComboBox1 = cmb_cond_pertenencia_legislatura_actual_persona_legisladora_declaracion_procedencia.Text.Trim();

            // Desbloquea la causa improcedencia
            if (valorComboBox1.Equals("Si", StringComparison.OrdinalIgnoreCase))
            {
                cmb_nombre_persona_legisladora_declaracion_procedencia.Enabled = true;
                cmb_nombre_persona_legisladora_declaracion_procedencia.BackColor = Color.Honeydew;
                cmb_nombre_persona_legisladora_declaracion_procedencia.Text = "";
                txt_ID_persona_legisladora_declaracion_procedencia.Text = "";

            }
            else
            {
                cmb_nombre_persona_legisladora_declaracion_procedencia.Enabled = false;
                cmb_nombre_persona_legisladora_declaracion_procedencia.BackColor = Color.LightGray;
                cmb_nombre_persona_legisladora_declaracion_procedencia.Text = "";
                txt_ID_persona_legisladora_declaracion_procedencia.Text = "";
                txt_ID_persona_legisladora_declaracion_procedencia.BackColor = Color.LightGray;

            }
        }

        private void Cmb_nombre_persona_legisladora_declaracion_procedencia()
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

                    cmb_nombre_persona_legisladora_declaracion_procedencia.DataSource = dataTable;
                    cmb_nombre_persona_legisladora_declaracion_procedencia.DisplayMember = "txt_nombre_1_persona_legisladora";

                    cmb_nombre_persona_legisladora_declaracion_procedencia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_nombre_persona_legisladora_declaracion_procedencia.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_nombre_persona_legisladora_declaracion_procedencia.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_nombre_persona_legisladora_declaracion_procedencia.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_nombre_persona_legisladora_declaracion_procedencia_Validating(object sender, CancelEventArgs e)
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
        private void cmb_nombre_persona_legisladora_declaracion_procedencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = cmb_nombre_persona_legisladora_declaracion_procedencia.Text;

            // Verificar si el nombre seleccionado es nulo o vacío
            if (string.IsNullOrEmpty(nombreSeleccionado))
            {
                txt_ID_persona_legisladora_declaracion_procedencia.Text = "";
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
                            txt_ID_persona_legisladora_declaracion_procedencia.Text = resultado.ToString();
                        }
                        else
                        {
                            txt_ID_persona_legisladora_declaracion_procedencia.Text = ""; // Limpiar el TextBox si no se encontró un ID
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

        private void cmb_municipio_persona_servidora_publica_declaracion_procedencia_Validating(object sender, CancelEventArgs e)
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
        private void cmb_municipio_persona_servidora_publica_declaracion_procedencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = cmb_municipio_persona_servidora_publica_declaracion_procedencia.Text;

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
                            txt_AGEM_persona_servidora_publica_declaracion_procedencia.Text = resultado.ToString();
                        }
                        else
                        {
                            txt_AGEM_persona_servidora_publica_declaracion_procedencia.Text = ""; // Limpiar el TextBox si no se encontró un ID
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


        //---------------------------

        // txt_nombre_institucion_persona_servidora_publica_declaracion_procedencia
        private void txt_nombre_institucion_persona_servidora_publica_declaracion_procedencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_nombre_institucion_persona_servidora_publica_declaracion_procedencia_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_nombre_institucion_persona_servidora_publica_declaracion_procedencia.Text = txt_nombre_institucion_persona_servidora_publica_declaracion_procedencia.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_nombre_institucion_persona_servidora_publica_declaracion_procedencia.SelectionStart = txt_nombre_institucion_persona_servidora_publica_declaracion_procedencia.Text.Length;
        }

        // txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_estatal_especifique
        private void txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_estatal_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_estatal_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_estatal_especifique.Text = txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_estatal_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_estatal_especifique.SelectionStart = txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_estatal_especifique.Text.Length;
        }

        // txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_municipal_especifique
        private void txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_municipal_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_municipal_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_municipal_especifique.Text = txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_municipal_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_municipal_especifique.SelectionStart = txt_otro_cargo_persona_servidora_publica_declaracion_procedencia_ambito_municipal_especifique.Text.Length;
        }

    }
}
