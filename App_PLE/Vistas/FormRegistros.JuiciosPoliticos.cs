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
            // Desbloquea fecha de resolución y sentido de resolución
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

                // COmbobox Sexo
                cmb_sexo_persona_servidora_publica_juicio_politico.Enabled = true;
                cmb_sexo_persona_servidora_publica_juicio_politico.BackColor = Color.Honeydew;
                cmb_sexo_persona_servidora_publica_juicio_politico.Text = "";

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

                // COmbobox Sexo
                cmb_sexo_persona_servidora_publica_juicio_politico.Enabled = false;
                cmb_sexo_persona_servidora_publica_juicio_politico.BackColor = Color.LightGray;
                cmb_sexo_persona_servidora_publica_juicio_politico.Text = "";

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

        }
        private void dtp_fecha_procedencia_denuncia_juicio_politico_CloseUp(object sender, EventArgs e)
        {

        }

        // ----------------------------- Pleno / Resolución ----------------------------

        private void dtp_fecha_resolucion_pleno_juicio_politico_CloseUp(object sender, EventArgs e)
        {

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





        //-------------------------------------









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
