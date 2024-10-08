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

        private void Cmb_cond_presentacion_denuncia_juicio_politico_legislatura_actual()
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

                    cmb_cond_presentacion_denuncia_juicio_politico_legislatura_actual.DataSource = dataTable;
                    cmb_cond_presentacion_denuncia_juicio_politico_legislatura_actual.DisplayMember = "descripcion";

                    cmb_cond_presentacion_denuncia_juicio_politico_legislatura_actual.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_presentacion_denuncia_juicio_politico_legislatura_actual.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_presentacion_denuncia_juicio_politico_legislatura_actual.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_presentacion_denuncia_juicio_politico_legislatura_actual.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_presentacion_denuncia_juicio_politico_legislatura_actual_Validating(object sender, CancelEventArgs e)
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
        private void cmb_cond_presentacion_denuncia_juicio_politico_legislatura_actual_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_cond_presentacion_denuncia_juicio_politico_legislatura_actual.Text.Trim();

            // Bloquea la condicion de la denucnia
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                cmb_cond_presentacion_denuncia_juicio_politico_periodo.Enabled = false;
                cmb_cond_presentacion_denuncia_juicio_politico_periodo.BackColor = Color.LightGray;
                cmb_cond_presentacion_denuncia_juicio_politico_periodo.Text = "";
               
            }
            else
            {
                cmb_cond_presentacion_denuncia_juicio_politico_periodo.Enabled = true;
                cmb_cond_presentacion_denuncia_juicio_politico_periodo.BackColor = Color.Honeydew;
                cmb_cond_presentacion_denuncia_juicio_politico_periodo.Text = "";
                
            }
            // Desbloquea numero de la legislatura
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                cmb_numero_legislatura_presentacion_denuncia_juicio_politico.Enabled = true;
                cmb_numero_legislatura_presentacion_denuncia_juicio_politico.BackColor = Color.Honeydew;
                cmb_numero_legislatura_presentacion_denuncia_juicio_politico.Text = "";

            }
            else
            {
                cmb_numero_legislatura_presentacion_denuncia_juicio_politico.Enabled = false;
                cmb_numero_legislatura_presentacion_denuncia_juicio_politico.BackColor = Color.LightGray;
                cmb_numero_legislatura_presentacion_denuncia_juicio_politico.Text = "";

            }
            // Desbloquea Condición de actualización del estatus
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                cmb_cond_actualizacion_estatus_denuncia_juicio_politico_periodo.Enabled = true;
                cmb_cond_actualizacion_estatus_denuncia_juicio_politico_periodo.BackColor = Color.Honeydew;
                cmb_cond_actualizacion_estatus_denuncia_juicio_politico_periodo.Text = "";

            }
            else
            {
                cmb_cond_actualizacion_estatus_denuncia_juicio_politico_periodo.Enabled = false;
                cmb_cond_actualizacion_estatus_denuncia_juicio_politico_periodo.BackColor = Color.LightGray;
                cmb_cond_actualizacion_estatus_denuncia_juicio_politico_periodo.Text = "";

            }
            // Se bloquea la fecha de ingreso de la denuncia de juicio politico
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                dtp_fecha_ingreso_denuncia_juicio_politico_oficialia_partes.Enabled = false;
                dtp_fecha_ingreso_denuncia_juicio_politico_oficialia_partes.BackColor = Color.LightGray;
                dtp_fecha_ingreso_denuncia_juicio_politico_oficialia_partes.Text = "";

            }
            else
            {
                dtp_fecha_ingreso_denuncia_juicio_politico_oficialia_partes.Enabled = true;
                dtp_fecha_ingreso_denuncia_juicio_politico_oficialia_partes.BackColor = Color.Honeydew;
                dtp_fecha_ingreso_denuncia_juicio_politico_oficialia_partes.Text = "";

            }
            // Se desbloquea condición de ser una persona legisladora de la legislatura ctual
            if (valorComboBox1.Equals("Si", StringComparison.OrdinalIgnoreCase))
            {
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_juicio_politico.Enabled = true;
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_juicio_politico.BackColor = Color.Honeydew;
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_juicio_politico.Text = "";

            }
            else
            {
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_juicio_politico.Enabled = false;
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_juicio_politico.BackColor = Color.LightGray;
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_juicio_politico.Text = "";

            }
        }

        //Condición de la denucnia
        private void Cmb_cond_presentacion_denuncia_juicio_politico_periodo()
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

                    cmb_cond_presentacion_denuncia_juicio_politico_periodo.DataSource = dataTable;
                    cmb_cond_presentacion_denuncia_juicio_politico_periodo.DisplayMember = "descripcion";

                    cmb_cond_presentacion_denuncia_juicio_politico_periodo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_presentacion_denuncia_juicio_politico_periodo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_presentacion_denuncia_juicio_politico_periodo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_presentacion_denuncia_juicio_politico_periodo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_presentacion_denuncia_juicio_politico_periodo_Validating(object sender, CancelEventArgs e)
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
        private void cmb_cond_presentacion_denuncia_juicio_politico_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_cond_presentacion_denuncia_juicio_politico_periodo.Text.Trim();
            // Desbloquea Condición de actualización del estatus
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                cmb_cond_actualizacion_estatus_denuncia_juicio_politico_periodo.Enabled = true;
                cmb_cond_actualizacion_estatus_denuncia_juicio_politico_periodo.BackColor = Color.Honeydew;
                cmb_cond_actualizacion_estatus_denuncia_juicio_politico_periodo.Text = "";

            }
            else
            {
                cmb_cond_actualizacion_estatus_denuncia_juicio_politico_periodo.Enabled = false;
                cmb_cond_actualizacion_estatus_denuncia_juicio_politico_periodo.BackColor = Color.LightGray;
                cmb_cond_actualizacion_estatus_denuncia_juicio_politico_periodo.Text = "";

            }
            // Bloquea el estatus de la denuncia del Juicio politico
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                cmb_estatus_denuncia_juicio_politico.Enabled = false;
                cmb_estatus_denuncia_juicio_politico.BackColor = Color.LightGray;
                cmb_estatus_denuncia_juicio_politico.Text = "";

            }
            else
            {
                cmb_estatus_denuncia_juicio_politico.Enabled = true;
                cmb_estatus_denuncia_juicio_politico.BackColor = Color.Honeydew;
                cmb_estatus_denuncia_juicio_politico.Text = "";

            }
            // Se bloquea la fecha de ingreso de la denuncia de juicio politico
            if (valorComboBox1.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                dtp_fecha_ingreso_denuncia_juicio_politico_oficialia_partes.Enabled = false;
                dtp_fecha_ingreso_denuncia_juicio_politico_oficialia_partes.BackColor = Color.LightGray;
                dtp_fecha_ingreso_denuncia_juicio_politico_oficialia_partes.Text = "";

            }
            else
            {
                dtp_fecha_ingreso_denuncia_juicio_politico_oficialia_partes.Enabled = true;
                dtp_fecha_ingreso_denuncia_juicio_politico_oficialia_partes.BackColor = Color.Honeydew;
                dtp_fecha_ingreso_denuncia_juicio_politico_oficialia_partes.Text = "";

            }
            // Se habilita la fecha Procedencia
            if (valorComboBox1.Equals("Si", StringComparison.OrdinalIgnoreCase))
            {
                dtp_fecha_procedencia_denuncia_juicio_politico.Enabled = true;
                dtp_fecha_procedencia_denuncia_juicio_politico.BackColor = Color.Honeydew;
                dtp_fecha_procedencia_denuncia_juicio_politico.Text = "";

            }
            else
            {
                dtp_fecha_procedencia_denuncia_juicio_politico.Enabled = false;
                dtp_fecha_procedencia_denuncia_juicio_politico.BackColor = Color.LightGray;
                dtp_fecha_procedencia_denuncia_juicio_politico.Text = "";

            }
            // Se desbloquea condición de ser una persona legisladora de la legislatura ctual
            if (valorComboBox1.Equals("Si", StringComparison.OrdinalIgnoreCase))
            {
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_juicio_politico.Enabled = true;
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_juicio_politico.BackColor = Color.Honeydew;
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_juicio_politico.Text = "";

            }
            else
            {
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_juicio_politico.Enabled = false;
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_juicio_politico.BackColor = Color.LightGray;
                cmb_cond_pertenencia_legislatura_actual_persona_legisladora_juicio_politico.Text = "";

            }
        }

        // Numero de la legislatura
        private void Cmb_numero_legislatura_presentacion_denuncia_juicio_politico()
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

                    cmb_numero_legislatura_presentacion_denuncia_juicio_politico.DataSource = dataTable;
                    cmb_numero_legislatura_presentacion_denuncia_juicio_politico.DisplayMember = "descripcion";

                    cmb_numero_legislatura_presentacion_denuncia_juicio_politico.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_numero_legislatura_presentacion_denuncia_juicio_politico.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_numero_legislatura_presentacion_denuncia_juicio_politico.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_numero_legislatura_presentacion_denuncia_juicio_politico.SelectedIndex = -1; // Aquí se establece como vacío
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

        // txt_turno_denuncia_juicio_politico
        private void txt_turno_denuncia_juicio_politico_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }
               

        private void Cmb_cond_actualizacion_estatus_denuncia_juicio_politico_periodo()
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

                    cmb_cond_actualizacion_estatus_denuncia_juicio_politico_periodo.DataSource = dataTable;
                    cmb_cond_actualizacion_estatus_denuncia_juicio_politico_periodo.DisplayMember = "descripcion";

                    cmb_cond_actualizacion_estatus_denuncia_juicio_politico_periodo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_actualizacion_estatus_denuncia_juicio_politico_periodo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_actualizacion_estatus_denuncia_juicio_politico_periodo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_actualizacion_estatus_denuncia_juicio_politico_periodo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_actualizacion_estatus_denuncia_juicio_politico_periodo_Validating(object sender, CancelEventArgs e)
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
        private void cmb_cond_actualizacion_estatus_denuncia_juicio_politico_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            string cadena = "Data Source = DB_PLE.db;Version=3;";

            if (cmb_cond_actualizacion_estatus_denuncia_juicio_politico_periodo.SelectedItem != null)
            {
                // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
                string valorComboBox = cmb_cond_actualizacion_estatus_denuncia_juicio_politico_periodo.Text.ToString();


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

                            case "No  ":
                                query = "select descripcion from TC_ESTATUS_DENUNCIA where id_estatus_denuncia in (1,2,3,4,5,6,7)";
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

                        cmb_estatus_denuncia_juicio_politico.DataSource = dataTable;
                        cmb_estatus_denuncia_juicio_politico.DisplayMember = "descripcion";

                        cmb_estatus_denuncia_juicio_politico.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_estatus_denuncia_juicio_politico.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_estatus_denuncia_juicio_politico.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_estatus_denuncia_juicio_politico.SelectedIndex = -1; // Aquí se establece como vacío

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

        private void Cmb_estatus_denuncia_juicio_politico()
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

                    cmb_estatus_denuncia_juicio_politico.DataSource = dataTable;
                    cmb_estatus_denuncia_juicio_politico.DisplayMember = "descripcion";

                    cmb_estatus_denuncia_juicio_politico.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_estatus_denuncia_juicio_politico.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_estatus_denuncia_juicio_politico.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_estatus_denuncia_juicio_politico.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_estatus_denuncia_juicio_politico_Validating(object sender, CancelEventArgs e)
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
        private void cmb_estatus_denuncia_juicio_politico_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_estatus_denuncia_juicio_politico.Text.Trim();

            // Desbloquea Causa de improcedenia de la denuncia juicio politico
            if (valorComboBox1.Equals("Improcedente (especifique)", StringComparison.OrdinalIgnoreCase))
            {
                txt_improcedente_estatus_denuncia_juicio_politico_especifique.Enabled = true;
                txt_improcedente_estatus_denuncia_juicio_politico_especifique.BackColor = Color.Honeydew;
                txt_improcedente_estatus_denuncia_juicio_politico_especifique.Text = "";

            }
            else
            {
                txt_improcedente_estatus_denuncia_juicio_politico_especifique.Enabled = false;
                txt_improcedente_estatus_denuncia_juicio_politico_especifique.BackColor = Color.LightGray;
                txt_improcedente_estatus_denuncia_juicio_politico_especifique.Text = "";

            }

            // Desbloquea Otro estatus de la denucnia de juicio
            if (valorComboBox1.Equals("Otro estatus (especifique)", StringComparison.OrdinalIgnoreCase))
            {
                txt_otro_estatus_denuncia_juicio_politico_especifique.Enabled = true;
                txt_otro_estatus_denuncia_juicio_politico_especifique.BackColor = Color.Honeydew;
                txt_otro_estatus_denuncia_juicio_politico_especifique.Text = "";

            }
            else
            {
                txt_otro_estatus_denuncia_juicio_politico_especifique.Enabled = false;
                txt_otro_estatus_denuncia_juicio_politico_especifique.BackColor = Color.LightGray;
                txt_otro_estatus_denuncia_juicio_politico_especifique.Text = "";

            }
            // Desbloquea fecha de procedencia si el valor es "En trámite en instancia substanciadora" o "Concluida"
            if (valorComboBox1.Equals("En trámite en instancia substanciadora", StringComparison.OrdinalIgnoreCase) ||
                valorComboBox1.Equals("Concluida", StringComparison.OrdinalIgnoreCase))
            {
                dtp_fecha_procedencia_denuncia_juicio_politico.Enabled = true;
                dtp_fecha_procedencia_denuncia_juicio_politico.BackColor = Color.Honeydew;
                dtp_fecha_procedencia_denuncia_juicio_politico.Text = "";
            }
            else
            {
                dtp_fecha_procedencia_denuncia_juicio_politico.Enabled = false;
                dtp_fecha_procedencia_denuncia_juicio_politico.BackColor = Color.LightGray;
                dtp_fecha_procedencia_denuncia_juicio_politico.Text = "";
            }
            // Desbloquea funciones de JP
            if (valorComboBox1.Equals("Concluida", StringComparison.OrdinalIgnoreCase))
            {
                dtp_fecha_resolucion_pleno_juicio_politico.Enabled = true;
                dtp_fecha_resolucion_pleno_juicio_politico.BackColor = Color.Honeydew;
                dtp_fecha_resolucion_pleno_juicio_politico.Text = "";

                cmb_sentido_resolucion_pleno_juicio_politico.Enabled = true;
                cmb_sentido_resolucion_pleno_juicio_politico.BackColor = Color.Honeydew;
                cmb_sentido_resolucion_pleno_juicio_politico.Text = "";

                // Txt sumatorias
                txt_votaciones_pleno_a_favor_juicio_politico.Enabled = true;
                txt_votaciones_pleno_a_favor_juicio_politico.BackColor = Color.Honeydew;
                txt_votaciones_pleno_a_favor_juicio_politico.Text = "";

                txt_votaciones_pleno_en_contra_juicio_politico.Enabled = true;
                txt_votaciones_pleno_en_contra_juicio_politico.BackColor = Color.Honeydew;
                txt_votaciones_pleno_en_contra_juicio_politico.Text = "";

                txt_votaciones_pleno_abstencion_juicio_politico.Enabled = true;
                txt_votaciones_pleno_abstencion_juicio_politico.BackColor = Color.Honeydew;
                txt_votaciones_pleno_abstencion_juicio_politico.Text = "";

                // Nombre y apellido de servidoras publicas
                txt_nombre_1_persona_servidora_publica_juicio_politico.Enabled = true;
                txt_nombre_1_persona_servidora_publica_juicio_politico.BackColor = Color.Honeydew;
                txt_nombre_1_persona_servidora_publica_juicio_politico.Text = "";

                txt_apellido_1_persona_servidora_publica_juicio_politico.Enabled = true;
                txt_apellido_1_persona_servidora_publica_juicio_politico.BackColor = Color.Honeydew;
                txt_apellido_1_persona_servidora_publica_juicio_politico.Text = "";

                // Cmbobox Sexo
                cmb_sexo_persona_servidora_publica_juicio_politico.Enabled = true;
                cmb_sexo_persona_servidora_publica_juicio_politico.BackColor = Color.Honeydew;
                cmb_sexo_persona_servidora_publica_juicio_politico.Text = "";

                // Cmbobox Cargo desempeño
                cmb_cargo_persona_servidora_publica_juicio_politico.Enabled = true;
                cmb_cargo_persona_servidora_publica_juicio_politico.BackColor = Color.Honeydew;
                cmb_cargo_persona_servidora_publica_juicio_politico.Text = "";

                // Perjuicios a los intereses públicos         
                cmb_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.Enabled = true;
                cmb_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.BackColor = Color.Honeydew;
                btn_agregar_juic_pol.Enabled = true; btn_eliminar_juic_pol.Enabled = true;
                dgv_perjuicios_pub.BackgroundColor = Color.Honeydew;
           
            }
            else
            {
                dtp_fecha_resolucion_pleno_juicio_politico.Enabled = false;
                dtp_fecha_resolucion_pleno_juicio_politico.BackColor = Color.LightGray;
                dtp_fecha_resolucion_pleno_juicio_politico.Text = "";

                cmb_sentido_resolucion_pleno_juicio_politico.Enabled = false;
                cmb_sentido_resolucion_pleno_juicio_politico.BackColor = Color.LightGray;
                cmb_sentido_resolucion_pleno_juicio_politico.Text = "";

                // Txt sumatorias
                txt_votaciones_pleno_a_favor_juicio_politico.Enabled = false;
                txt_votaciones_pleno_a_favor_juicio_politico.BackColor = Color.LightGray;
                txt_votaciones_pleno_a_favor_juicio_politico.Text = "";

                txt_votaciones_pleno_en_contra_juicio_politico.Enabled = false;
                txt_votaciones_pleno_en_contra_juicio_politico.BackColor = Color.LightGray;
                txt_votaciones_pleno_en_contra_juicio_politico.Text = "";

                txt_votaciones_pleno_abstencion_juicio_politico.Enabled = false;
                txt_votaciones_pleno_abstencion_juicio_politico.BackColor = Color.LightGray;
                txt_votaciones_pleno_abstencion_juicio_politico.Text = "";

                // Nombre y apellido de servidoras publicas
                txt_nombre_1_persona_servidora_publica_juicio_politico.Enabled = false;
                txt_nombre_1_persona_servidora_publica_juicio_politico.BackColor = Color.LightGray;
                txt_nombre_1_persona_servidora_publica_juicio_politico.Text = "";

                txt_apellido_1_persona_servidora_publica_juicio_politico.Enabled = false;
                txt_apellido_1_persona_servidora_publica_juicio_politico.BackColor = Color.LightGray;
                txt_apellido_1_persona_servidora_publica_juicio_politico.Text = "";

                // Combobox Sexo
                cmb_sexo_persona_servidora_publica_juicio_politico.Enabled = false;
                cmb_sexo_persona_servidora_publica_juicio_politico.BackColor = Color.LightGray;
                cmb_sexo_persona_servidora_publica_juicio_politico.Text = "";

                // Cmbobox Cargo desempeño
                cmb_cargo_persona_servidora_publica_juicio_politico.Enabled = false;
                cmb_cargo_persona_servidora_publica_juicio_politico.BackColor = Color.LightGray;
                cmb_cargo_persona_servidora_publica_juicio_politico.Text = "";

                // Perjuicios a los intereses públicos
                cmb_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.Enabled = false;
                cmb_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.BackColor = Color.LightGray;
                cmb_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.Text = "";
                btn_agregar_juic_pol.Enabled = false; btn_eliminar_juic_pol.Enabled = false;
                dgv_perjuicios_pub.BackgroundColor = Color.LightGray;
                dgv_perjuicios_pub.Rows.Clear();
                txt_otro_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_especifique.Enabled = false;
                txt_otro_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_especifique.BackColor = Color.LightGray;
            }

        }

        // txt_improcedente_estatus_denuncia_juicio_politico_especifique
        private void txt_improcedente_estatus_denuncia_juicio_politico_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_improcedente_estatus_denuncia_juicio_politico_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_improcedente_estatus_denuncia_juicio_politico_especifique.Text = txt_improcedente_estatus_denuncia_juicio_politico_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_improcedente_estatus_denuncia_juicio_politico_especifique.SelectionStart = txt_improcedente_estatus_denuncia_juicio_politico_especifique.Text.Length;

        }

        // txt_otro_estatus_denuncia_juicio_politico_especifique
        private void txt_otro_estatus_denuncia_juicio_politico_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_otro_estatus_denuncia_juicio_politico_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_otro_estatus_denuncia_juicio_politico_especifique.Text = txt_otro_estatus_denuncia_juicio_politico_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_otro_estatus_denuncia_juicio_politico_especifique.SelectionStart = txt_otro_estatus_denuncia_juicio_politico_especifique.Text.Length;

        }

        // ----------------------------- Ingreso ----------------------------

        private void dtp_fecha_ingreso_denuncia_juicio_politico_oficialia_partes_CloseUp(object sender, EventArgs e)
        {
            // Obtener las fechas seleccionadas
            DateTime fechaGaceta = dtp_fecha_ingreso_denuncia_juicio_politico_oficialia_partes.Value.Date; // Solo la fecha, sin hora
            DateTime fechaRemPE = dtp_fecha_termino_informacion_reportada.Value.Date; // Solo la fecha, sin hora

            // Validar si la fecha de publicación es menor que la fecha de remisión.
            if (fechaGaceta > fechaRemPE)
            {
                // Mostrar mensaje de error
                MessageBox.Show("La fecha de ingreso de la denuncia de juicio político a oficialía de partes debe ser igual o menor a la fecha de información reportada en datos generales.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Vaciar el campo de fecha
                dtp_fecha_ingreso_denuncia_juicio_politico_oficialia_partes.CustomFormat = " ";  // Vaciar el campo
                dtp_fecha_ingreso_denuncia_juicio_politico_oficialia_partes.Format = DateTimePickerFormat.Custom;  // Establecer formato personalizado vacío
            }
            else
            {
                // Si la fecha es válida (igual o mayor que la fecha de remisión), restaurar el formato de fecha corta
                dtp_fecha_ingreso_denuncia_juicio_politico_oficialia_partes.Format = DateTimePickerFormat.Short;
            }
        }
        private void dtp_fecha_procedencia_denuncia_juicio_politico_CloseUp(object sender, EventArgs e)
        {
            // Obtener las fechas seleccionadas
            DateTime fechaGaceta = dtp_fecha_procedencia_denuncia_juicio_politico.Value.Date; // Solo la fecha, sin hora
            DateTime fechaRemPE = dtp_fecha_ingreso_denuncia_juicio_politico_oficialia_partes.Value.Date; // Solo la fecha, sin hora

            // Validar si la fecha de publicación es menor que la fecha de remisión.
            if (fechaGaceta < fechaRemPE)
            {
                // Mostrar mensaje de error
                MessageBox.Show("La fecha de procedencia debe ser igual o mayor a la fecha de ingreso de la denuncia de juicio político a oficialía de partes.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Vaciar el campo de fecha
                dtp_fecha_procedencia_denuncia_juicio_politico.CustomFormat = " ";  // Vaciar el campo
                dtp_fecha_procedencia_denuncia_juicio_politico.Format = DateTimePickerFormat.Custom;  // Establecer formato personalizado vacío
            }
            else
            {
                // Si la fecha es válida (igual o mayor que la fecha de remisión), restaurar el formato de fecha corta
                dtp_fecha_procedencia_denuncia_juicio_politico.Format = DateTimePickerFormat.Short;
            }
        }

        // ----------------------------- Pleno / Resolución ----------------------------

        private void dtp_fecha_resolucion_pleno_juicio_politico_CloseUp(object sender, EventArgs e)
        {
            // Obtener las fechas seleccionadas
            DateTime fechaGaceta = dtp_fecha_resolucion_pleno_juicio_politico.Value.Date; // Solo la fecha, sin hora
            DateTime fechaRemPE = dtp_fecha_procedencia_denuncia_juicio_politico.Value.Date; // Solo la fecha, sin hora

            // Validar si la fecha de publicación es menor que la fecha de remisión.
            if (fechaGaceta < fechaRemPE)
            {
                // Mostrar mensaje de error
                MessageBox.Show("La fecha de resolución del porcedimiento de juicio politico debe ser igual o mayor a la fecha en la que se determinó la procedencia de la denucncia de juicio politico.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Vaciar el campo de fecha
                dtp_fecha_resolucion_pleno_juicio_politico.CustomFormat = " ";  // Vaciar el campo
                dtp_fecha_resolucion_pleno_juicio_politico.Format = DateTimePickerFormat.Custom;  // Establecer formato personalizado vacío
            }
            else
            {
                // Si la fecha es válida (igual o mayor que la fecha de remisión), restaurar el formato de fecha corta
                dtp_fecha_resolucion_pleno_juicio_politico.Format = DateTimePickerFormat.Short;
            }
        }
        private void Cmb_sentido_resolucion_pleno_juicio_politico()
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

                    cmb_sentido_resolucion_pleno_juicio_politico.DataSource = dataTable;
                    cmb_sentido_resolucion_pleno_juicio_politico.DisplayMember = "descripcion";

                    cmb_sentido_resolucion_pleno_juicio_politico.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_sentido_resolucion_pleno_juicio_politico.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_sentido_resolucion_pleno_juicio_politico.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_sentido_resolucion_pleno_juicio_politico.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_sentido_resolucion_pleno_juicio_politico_Validating(object sender, CancelEventArgs e)
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

        // ----------------------------- Votaciones plenarias ----------------------------

        // txt_votaciones_pleno_a_favor_juicio_politico
        private void txt_votaciones_pleno_a_favor_juicio_politico_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }

        // txt_votaciones_pleno_en_contra_juicio_politico
        private void txt_votaciones_pleno_en_contra_juicio_politico_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }

        // txt_votaciones_pleno_abstencion_juicio_politico
        private void txt_votaciones_pleno_abstencion_juicio_politico_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }

        // Sumatorias
        private void txt_votaciones_pleno_a_favor_juicio_politico_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalVotaciones_jp();
        }
        private void txt_votaciones_pleno_en_contra_juicio_politico_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalVotaciones_jp();
        }
        private void txt_votaciones_pleno_abstencion_juicio_politico_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalVotaciones_jp();
        }
        private void CalcularTotalVotaciones_jp()
        {
            // Inicializar las variables
            int aFavor = 0, enContra = 0, abstencion = 0;

            // Verificar que los textos no estén vacíos y convertir a número
            if (!string.IsNullOrEmpty(txt_votaciones_pleno_a_favor_juicio_politico.Text))
                int.TryParse(txt_votaciones_pleno_a_favor_juicio_politico.Text, out aFavor);

            if (!string.IsNullOrEmpty(txt_votaciones_pleno_en_contra_juicio_politico.Text))
                int.TryParse(txt_votaciones_pleno_en_contra_juicio_politico.Text, out enContra);

            if (!string.IsNullOrEmpty(txt_votaciones_pleno_abstencion_juicio_politico.Text))
                int.TryParse(txt_votaciones_pleno_abstencion_juicio_politico.Text, out abstencion);

            // Calcular el total
            int total = aFavor + enContra + abstencion;

            // Mostrar el resultado en el TextBox total
            txt_total_votaciones_pleno_juicio_politico.Text = total.ToString();

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
                txt_votaciones_pleno_a_favor_juicio_politico.Clear();
                txt_votaciones_pleno_en_contra_juicio_politico.Clear();
                txt_votaciones_pleno_abstencion_juicio_politico.Clear();

                // Restablecer el total a 0
                txt_total_votaciones_pleno_juicio_politico.Text = "0";
            }
        }

        // Caracteristicas demografias de la persona servidora públicca

        private void Cmb_sexo_persona_servidora_publica_juicio_politico() 
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

                    cmb_sexo_persona_servidora_publica_juicio_politico.DataSource = dataTable;
                    cmb_sexo_persona_servidora_publica_juicio_politico.DisplayMember = "descripcion";

                    cmb_sexo_persona_servidora_publica_juicio_politico.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_sexo_persona_servidora_publica_juicio_politico.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_sexo_persona_servidora_publica_juicio_politico.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_sexo_persona_servidora_publica_juicio_politico.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_sexo_persona_servidora_publica_juicio_politico_Validating(object sender, CancelEventArgs e)
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

        // txt_nombre_1_persona_servidora_publica_juicio_politico
        private void txt_nombre_1_persona_servidora_publica_juicio_politico_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_nombre_1_persona_servidora_publica_juicio_politico_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_nombre_1_persona_servidora_publica_juicio_politico.Text = txt_nombre_1_persona_servidora_publica_juicio_politico.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_nombre_1_persona_servidora_publica_juicio_politico.SelectionStart = txt_nombre_1_persona_servidora_publica_juicio_politico.Text.Length;

            // Desbloquear txt_nombre_2_personal_apoyo, cambiar su color de fondo, o borrarlo y deshabilitarlo
            if (!string.IsNullOrEmpty(txt_nombre_1_persona_servidora_publica_juicio_politico.Text))
            {
                txt_nombre_2_persona_servidora_publica_juicio_politico.Enabled = true;
                txt_nombre_2_persona_servidora_publica_juicio_politico.BackColor = Color.Honeydew;
            }
            else
            {
                // Si txt_nombre_2_persona_servidora_publica_juicio_politico está vacío, borrar y deshabilitar txt_nombre_2_personal_apoyo
                txt_nombre_2_persona_servidora_publica_juicio_politico.Text = string.Empty;
                txt_nombre_2_persona_servidora_publica_juicio_politico.Enabled = false;
                txt_nombre_2_persona_servidora_publica_juicio_politico.BackColor = SystemColors.Window; // Restaurar el color predeterminado
            }
        }

        // txt_nombre_2_persona_servidora_publica_juicio_politico
        private void txt_nombre_2_persona_servidora_publica_juicio_politico_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_nombre_2_persona_servidora_publica_juicio_politico_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_nombre_2_persona_servidora_publica_juicio_politico.Text = txt_nombre_2_persona_servidora_publica_juicio_politico.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_nombre_2_persona_servidora_publica_juicio_politico.SelectionStart = txt_nombre_2_persona_servidora_publica_juicio_politico.Text.Length;

            // Desbloquear txt_nombre_3_personal_apoyo, cambiar su color de fondo, o borrarlo y deshabilitarlo
            if (!string.IsNullOrEmpty(txt_nombre_2_persona_servidora_publica_juicio_politico.Text))
            {
                txt_nombre_3_persona_servidora_publica_juicio_politico.Enabled = true;
                txt_nombre_3_persona_servidora_publica_juicio_politico.BackColor = Color.Honeydew;
            }
            else
            {
                // Si txt_nombre_2_persona_servidora_publica_juicio_politico está vacío, borrar y deshabilitar txt_nombre_2_personal_apoyo
                txt_nombre_3_persona_servidora_publica_juicio_politico.Text = string.Empty;
                txt_nombre_3_persona_servidora_publica_juicio_politico.Enabled = false;
                txt_nombre_3_persona_servidora_publica_juicio_politico.BackColor = SystemColors.Window; // Restaurar el color predeterminado
            }
        }

        // txt_nombre_3_persona_servidora_publica_juicio_politico
        private void txt_nombre_3_persona_servidora_publica_juicio_politico_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_nombre_3_persona_servidora_publica_juicio_politico_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_nombre_3_persona_servidora_publica_juicio_politico.Text = txt_nombre_3_persona_servidora_publica_juicio_politico.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_nombre_3_persona_servidora_publica_juicio_politico.SelectionStart = txt_nombre_3_persona_servidora_publica_juicio_politico.Text.Length;

        }

        // txt_apellido_1_persona_servidora_publica_juicio_politico
        private void txt_apellido_1_persona_servidora_publica_juicio_politico_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_apellido_1_persona_servidora_publica_juicio_politico_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_apellido_1_persona_servidora_publica_juicio_politico.Text = txt_apellido_1_persona_servidora_publica_juicio_politico.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_apellido_1_persona_servidora_publica_juicio_politico.SelectionStart = txt_apellido_1_persona_servidora_publica_juicio_politico.Text.Length;

            // Desbloquear txt_apellido_1_persona_servidora_publica_juicio_politico, cambiar su color de fondo, o borrarlo y deshabilitarlo
            if (!string.IsNullOrEmpty(txt_apellido_1_persona_servidora_publica_juicio_politico.Text))
            {
                txt_apellido_2_persona_servidora_publica_juicio_politico.Enabled = true;
                txt_apellido_2_persona_servidora_publica_juicio_politico.BackColor = Color.Honeydew;
            }
            else
            {
                // Si txt_nombre_2_persona_servidora_publica_juicio_politico está vacío, borrar y deshabilitar txt_nombre_2_personal_apoyo
                txt_apellido_2_persona_servidora_publica_juicio_politico.Text = string.Empty;
                txt_apellido_2_persona_servidora_publica_juicio_politico.Enabled = false;
                txt_apellido_2_persona_servidora_publica_juicio_politico.BackColor = SystemColors.Window; // Restaurar el color predeterminado
            }
        }

        // txt_apellido_2_persona_servidora_publica_juicio_politico
        private void txt_apellido_2_persona_servidora_publica_juicio_politico_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_apellido_2_persona_servidora_publica_juicio_politico_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_apellido_2_persona_servidora_publica_juicio_politico.Text = txt_apellido_2_persona_servidora_publica_juicio_politico.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_apellido_2_persona_servidora_publica_juicio_politico.SelectionStart = txt_apellido_2_persona_servidora_publica_juicio_politico.Text.Length;

            // Desbloquear txt_apellido_2_persona_servidora_publica_juicio_politico, cambiar su color de fondo, o borrarlo y deshabilitarlo
            if (!string.IsNullOrEmpty(txt_apellido_2_persona_servidora_publica_juicio_politico.Text))
            {
                txt_apellido_3_persona_servidora_publica_juicio_politico.Enabled = true;
                txt_apellido_3_persona_servidora_publica_juicio_politico.BackColor = Color.Honeydew;
            }
            else
            {
                // Si txt_nombre_2_persona_servidora_publica_juicio_politico está vacío, borrar y deshabilitar txt_nombre_2_personal_apoyo
                txt_apellido_3_persona_servidora_publica_juicio_politico.Text = string.Empty;
                txt_apellido_3_persona_servidora_publica_juicio_politico.Enabled = false;
                txt_apellido_3_persona_servidora_publica_juicio_politico.BackColor = SystemColors.Window; // Restaurar el color predeterminado
            }
        }

        // txt_apellido_3_persona_servidora_publica_juicio_politico
        private void txt_apellido_3_persona_servidora_publica_juicio_politico_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_apellido_3_persona_servidora_publica_juicio_politico_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_apellido_3_persona_servidora_publica_juicio_politico.Text = txt_apellido_3_persona_servidora_publica_juicio_politico.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_apellido_3_persona_servidora_publica_juicio_politico.SelectionStart = txt_apellido_3_persona_servidora_publica_juicio_politico.Text.Length;

        }

        // ----------------------------- Carateristicas del cargo ----------------------------

        private void Cmb_cargo_persona_servidora_publica_juicio_politico()
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

                    cmb_cargo_persona_servidora_publica_juicio_politico.DataSource = dataTable;
                    cmb_cargo_persona_servidora_publica_juicio_politico.DisplayMember = "descripcion";

                    cmb_cargo_persona_servidora_publica_juicio_politico.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cargo_persona_servidora_publica_juicio_politico.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cargo_persona_servidora_publica_juicio_politico.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cargo_persona_servidora_publica_juicio_politico.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cargo_persona_servidora_publica_juicio_politico_Validating(object sender, CancelEventArgs e)
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
        private void cmb_cargo_persona_servidora_publica_juicio_politico_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_cargo_persona_servidora_publica_juicio_politico.Text.Trim();

            // Desbloquea Nombre de la institución pública a la que pertenece la persona servidora pública sujeta a procedimiento de juicio político
            if (valorComboBox1.Equals("Titular de alguna institución  o unidad administrativa de la Administración Pública Estatal (excluyendo, de ser el caso, a la Procuraduría General de Justicia)", StringComparison.OrdinalIgnoreCase) ||
                valorComboBox1.Equals("Titular de algún otro órgano constitucional autónomo de la entidad federativa (excluyendo al organismo público local electoral, al organismo garante de acceso a la información y protección de datos personales, al organismo público de derechos humanos de la entidad federativa, y, de ser el caso, a la Fiscalía General de la entidad federativa)", StringComparison.OrdinalIgnoreCase) ||
                valorComboBox1.Equals("Titular de alguna institución o unidad administrativa de la Administración Pública del municipio o demarcación territorial", StringComparison.OrdinalIgnoreCase))
            {
                txt_nombre_institucion_persona_servidora_publica_juicio_politico.Enabled = true;
                txt_nombre_institucion_persona_servidora_publica_juicio_politico.BackColor = Color.Honeydew;
                txt_nombre_institucion_persona_servidora_publica_juicio_politico.Text = "";
            }
            else
            {
                txt_nombre_institucion_persona_servidora_publica_juicio_politico.Enabled = false;
                txt_nombre_institucion_persona_servidora_publica_juicio_politico.BackColor = Color.LightGray;
                txt_nombre_institucion_persona_servidora_publica_juicio_politico.Text = "";
            }
            // Desbloquea Nombre de la institución pública a la que pertenece la persona servidora pública sujeta a procedimiento de juicio político
            if (valorComboBox1.Equals("Presidente(a) municipal", StringComparison.OrdinalIgnoreCase) ||
                valorComboBox1.Equals("Regidor(a)", StringComparison.OrdinalIgnoreCase) ||
                 valorComboBox1.Equals("Síndico(a)", StringComparison.OrdinalIgnoreCase) ||
                  valorComboBox1.Equals("Titular de alguna institución o unidad administrativa de la Administración Pública del municipio o demarcación territorial", StringComparison.OrdinalIgnoreCase) ||
                valorComboBox1.Equals("Otro cargo del ámbito municipal (especifique)", StringComparison.OrdinalIgnoreCase))
            {
                cmb_municipio_persona_servidora_publica_juicio_politico.Enabled = true;
                cmb_municipio_persona_servidora_publica_juicio_politico.BackColor = Color.Honeydew;
                cmb_municipio_persona_servidora_publica_juicio_politico.Text = "";

                txt_AGEM_persona_servidora_publica_juicio_politico.Enabled = false;
                txt_AGEM_persona_servidora_publica_juicio_politico.BackColor = Color.Honeydew;
                txt_AGEM_persona_servidora_publica_juicio_politico.Text = "";
            }
            else
            {
                cmb_municipio_persona_servidora_publica_juicio_politico.Enabled = false;
                cmb_municipio_persona_servidora_publica_juicio_politico.BackColor = Color.LightGray;
                cmb_municipio_persona_servidora_publica_juicio_politico.Text = "";

                txt_AGEM_persona_servidora_publica_juicio_politico.Enabled = false;
                txt_AGEM_persona_servidora_publica_juicio_politico.BackColor = Color.LightGray;
                txt_AGEM_persona_servidora_publica_juicio_politico.Text = "";
            }
            // Desbloquea Otros cargos del ambito juicio politico y servidor publico 
            if (valorComboBox1.Equals("Otro cargo del ámbito estatal (especifique)", StringComparison.OrdinalIgnoreCase))
            {
                txt_otro_cargo_persona_servidora_publica_juicio_politico_ambito_estatal_especifique.Enabled = true;
                txt_otro_cargo_persona_servidora_publica_juicio_politico_ambito_estatal_especifique.BackColor = Color.Honeydew;
                txt_otro_cargo_persona_servidora_publica_juicio_politico_ambito_estatal_especifique.Text = "";
                                
            }
            else
            {
                txt_otro_cargo_persona_servidora_publica_juicio_politico_ambito_estatal_especifique.Enabled = false;
                txt_otro_cargo_persona_servidora_publica_juicio_politico_ambito_estatal_especifique.BackColor = Color.LightGray;
                txt_otro_cargo_persona_servidora_publica_juicio_politico_ambito_estatal_especifique.Text = "";
                               
            }
            // Desbloquea Otros cargos del ambito juicio politico y servidor publico 
            if (valorComboBox1.Equals("Otro cargo del ámbito municipal (especifique)", StringComparison.OrdinalIgnoreCase))
            {
               
                txt_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.Enabled = true;
                txt_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.BackColor = Color.Honeydew;
                txt_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.Text = "";
            }
            else
            {
                
                txt_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.Enabled = false;
                txt_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.BackColor = Color.LightGray;
                txt_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.Text = "";
            }
        }

        private void Cmb_cond_pertenencia_legislatura_actual_persona_legisladora_juicio_politico()
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

                    cmb_cond_pertenencia_legislatura_actual_persona_legisladora_juicio_politico.DataSource = dataTable;
                    cmb_cond_pertenencia_legislatura_actual_persona_legisladora_juicio_politico.DisplayMember = "descripcion";

                    cmb_cond_pertenencia_legislatura_actual_persona_legisladora_juicio_politico.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_pertenencia_legislatura_actual_persona_legisladora_juicio_politico.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_pertenencia_legislatura_actual_persona_legisladora_juicio_politico.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_pertenencia_legislatura_actual_persona_legisladora_juicio_politico.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_pertenencia_legislatura_actual_persona_legisladora_juicio_politico_Validating(object sender, CancelEventArgs e)
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
        private void cmb_cond_pertenencia_legislatura_actual_persona_legisladora_juicio_politico_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_cond_pertenencia_legislatura_actual_persona_legisladora_juicio_politico.Text.Trim();

            // Desbloquea nombre de la persona legisladora y su ID
            if (valorComboBox1.Equals("Si", StringComparison.OrdinalIgnoreCase))
            {
                cmb_nombre_persona_legisladora_juicio_politico.Enabled = true;
                cmb_nombre_persona_legisladora_juicio_politico.BackColor = Color.Honeydew;
                cmb_nombre_persona_legisladora_juicio_politico.Text = "";

                txt_ID_persona_legisladora_juicio_politico.Enabled = false;
                txt_ID_persona_legisladora_juicio_politico.BackColor = Color.Honeydew;
                txt_ID_persona_legisladora_juicio_politico.Text = "";

            }
            else
            {
                cmb_nombre_persona_legisladora_juicio_politico.Enabled = false;
                cmb_nombre_persona_legisladora_juicio_politico.BackColor = Color.LightGray;
                cmb_nombre_persona_legisladora_juicio_politico.Text = "";

                txt_ID_persona_legisladora_juicio_politico.Enabled = false;
                txt_ID_persona_legisladora_juicio_politico.BackColor = Color.LightGray;
                txt_ID_persona_legisladora_juicio_politico.Text = "";

            }
        }

        private void Cmb_nombre_persona_legisladora_juicio_politico()
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

                    cmb_nombre_persona_legisladora_juicio_politico.DataSource = dataTable;
                    cmb_nombre_persona_legisladora_juicio_politico.DisplayMember = "txt_nombre_1_persona_legisladora";

                    cmb_nombre_persona_legisladora_juicio_politico.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_nombre_persona_legisladora_juicio_politico.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_nombre_persona_legisladora_juicio_politico.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_nombre_persona_legisladora_juicio_politico.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_nombre_persona_legisladora_juicio_politico_Validating(object sender, CancelEventArgs e)
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
        private void cmb_nombre_persona_legisladora_juicio_politico_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = cmb_nombre_persona_legisladora_juicio_politico.Text;

            // Verificar si el nombre seleccionado es nulo o vacío
            if (string.IsNullOrEmpty(nombreSeleccionado))
            {
                txt_ID_persona_legisladora_juicio_politico.Text = "";
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
                            txt_ID_persona_legisladora_juicio_politico.Text = resultado.ToString();
                        }
                        else
                        {
                            txt_ID_persona_legisladora_juicio_politico.Text = ""; // Limpiar el TextBox si no se encontró un ID
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

        // txt_nombre_institucion_persona_servidora_publica_juicio_politico
        private void txt_nombre_institucion_persona_servidora_publica_juicio_politico_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_nombre_institucion_persona_servidora_publica_juicio_politico_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_nombre_institucion_persona_servidora_publica_juicio_politico.Text = txt_nombre_institucion_persona_servidora_publica_juicio_politico.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_nombre_institucion_persona_servidora_publica_juicio_politico.SelectionStart = txt_nombre_institucion_persona_servidora_publica_juicio_politico.Text.Length;

        }

        // txt_otro_cargo_persona_servidora_publica_juicio_politico_ambito_estatal_especifique
        private void txt_otro_cargo_persona_servidora_publica_juicio_politico_ambito_estatal_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_otro_cargo_persona_servidora_publica_juicio_politico_ambito_estatal_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_otro_cargo_persona_servidora_publica_juicio_politico_ambito_estatal_especifique.Text = txt_otro_cargo_persona_servidora_publica_juicio_politico_ambito_estatal_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_otro_cargo_persona_servidora_publica_juicio_politico_ambito_estatal_especifique.SelectionStart = txt_otro_cargo_persona_servidora_publica_juicio_politico_ambito_estatal_especifique.Text.Length;

        }

        // txt_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1
        private void txt_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.Text = txt_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.SelectionStart = txt_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.Text.Length;

        }

        private void cmb_municipio_persona_servidora_publica_juicio_politico_Validating(object sender, CancelEventArgs e)
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
        private void cmb_municipio_persona_servidora_publica_juicio_politico_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = cmb_municipio_persona_servidora_publica_juicio_politico.Text;

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
                            txt_AGEM_persona_servidora_publica_juicio_politico.Text = resultado.ToString();
                        }
                        else
                        {
                            txt_AGEM_persona_servidora_publica_juicio_politico.Text = ""; // Limpiar el TextBox si no se encontró un ID
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

        // ----------------------------- Perjuicio a los intereses públicos fundamentales y de su buen despacho ----------------------------

        private void Cmb_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_PERJ_PUBLICOS";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.DataSource = dataTable;
                    cmb_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.DisplayMember = "descripcion";

                    cmb_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1_Validating(object sender, CancelEventArgs e)
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
        private void cmb_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.Text.Trim();

            // Desbloquea Causa de improcedenia de la denuncia juicio politico
            if (valorComboBox1.Equals("Otro prejuicio a los intereses públicos fundamentales y de su buen despacho (especifique)", StringComparison.OrdinalIgnoreCase))
            {
                txt_otro_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_especifique.Enabled = true;
                txt_otro_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_especifique.BackColor = Color.Honeydew;
                txt_otro_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_especifique.Text = "";
                dgv_perjuicios_pub.Enabled = false;
                dgv_perjuicios_pub.BackgroundColor = Color.LightGray;
                dgv_perjuicios_pub.Rows.Clear();
                btn_agregar_juic_pol.Enabled = false;
                btn_eliminar_juic_pol.Enabled = false;
            }
            else
            {
                txt_otro_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_especifique.Enabled = false;
                txt_otro_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_especifique.BackColor = Color.LightGray;
                txt_otro_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_especifique.Text = "";
                btn_agregar_juic_pol.Enabled = true;
                btn_eliminar_juic_pol.Enabled = true;
                dgv_perjuicios_pub.BackgroundColor = Color.Honeydew;

            }
        }

        // Botones agregar y eliminar de la tabla personas legisladoras

        private void btn_agregar_juic_pol_Click(object sender, EventArgs e)
        {
            // Obtener el nombre y el ID seleccionados
            string nombreSeleccionado = cmb_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.Text.Trim();
            

            if (string.IsNullOrWhiteSpace(nombreSeleccionado) )
            {
                MessageBox.Show("Revisar datos vacíos");
            }
            else
            {
                // Verificar si el nombre ya existe en la tabla
                bool respuesta = IsDuplicateRecord_PL_JP(nombreSeleccionado);

                if (respuesta)
                {
                    MessageBox.Show("Dato duplicado");
                    cmb_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.Text = "";
                }
                else
                {
                    // Agregar una nueva fila al DataGridView con el formato: ID primero, luego el nombre
                    dgv_perjuicios_pub.Rows.Add(nombreSeleccionado);

                    // Limpiar los campos después de agregar los datos
                    cmb_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_1.Text = "";
                    
                }
            }
        }
        private void btn_eliminar_juic_pol_Click(object sender, EventArgs e)
        {
            if (dgv_perjuicios_pub.SelectedRows.Count > 0)
            {
                dgv_perjuicios_pub.Rows.RemoveAt(dgv_perjuicios_pub.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }
        private bool IsDuplicateRecord_PL_JP(string variable_cmb)
        {
            foreach (DataGridViewRow row in dgv_perjuicios_pub.Rows)
            {
                if (row.IsNewRow) continue; // Skip the new row placeholder

                string existingId = row.Cells["Perjuicios_JP"].Value.ToString();

                if (existingId == variable_cmb)
                {
                    return true;
                }
            }
            return false;
        }


        //-------------------------------------













        // txt_otro_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_especifique
        private void txt_otro_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_otro_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_otro_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_especifique.Text = txt_otro_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_otro_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_especifique.SelectionStart = txt_otro_perjuicio_a_los_intereses_publicos_fundamentales_y_de_su_buen_despacho_especifique.Text.Length;

        }

    }
}
