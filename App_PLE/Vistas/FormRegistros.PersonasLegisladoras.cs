using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_PLE.Vistas
{
    public partial class FormRegistros: Form
    {

        private SQLiteConnection _connection; // variable para la conexion de datos
        private void ConexionBasedatosSQLite()
        {
            // Crea la cadena de conexión
            string connectionString = "Data Source=DB_PLE.db;Version=3;";

            // Inicializa la conexión
            _connection = new SQLiteConnection(connectionString);

            try
            {
                // Abre la conexión
                _connection.Open();
                MessageBox.Show("Conexión abierta exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir la conexión: " + ex.Message);
            }
        }

        // sexo_persona_legisladora
        private void cmb_Sexo_Persona_Legisladora()
        {
            try
            {

                // comando de sql
                string query = "select descripcion from TC_SEXO";

                // Utilizar un DataReader para obtener los datos
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, _connection);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                cmb_sexo_persona_legisladora.DataSource = dataTable;
                cmb_sexo_persona_legisladora.DisplayMember = "descripcion";

                cmb_sexo_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmb_sexo_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                cmb_sexo_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                cmb_sexo_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar el ComboBox cmb_Sexo_Persona_Legisladora: " + ex.Message);
            }

        }
        private void cmb_sexo_persona_legisladora_Validating(object sender, CancelEventArgs e)
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

        private void cmb_Estatus_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            //using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            //{
            try
            {

                // comando de sql
                string query = "select descripcion from TC_ESTATUS";

                // Utilizar un DataReader para obtener los datos
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, _connection);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                cmb_estatus_persona_legisladora.DataSource = dataTable;
                cmb_estatus_persona_legisladora.DisplayMember = "descripcion";

                cmb_estatus_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmb_estatus_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                cmb_estatus_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                cmb_estatus_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar el ComboBox: " + ex.Message);
            }
        }

        // tipo_licencia_persona_legisladora
        private void cmb_Tipo_licencia_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_TIPO_LICENICIA";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cbm_tipo_licencia_persona_legisladora.DataSource = dataTable;
                    cbm_tipo_licencia_persona_legisladora.DisplayMember = "descripcion";

                    cbm_tipo_licencia_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cbm_tipo_licencia_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cbm_tipo_licencia_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cbm_tipo_licencia_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cbm_tipo_licencia_persona_legisladora_Validating(object sender, CancelEventArgs e)
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

        // causa_fallecimiento_persona_legisladora
        private void cmb_Causa_fallecimiento_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_CAUSAS_FALLECIMIENTO";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cbm_causa_fallecimiento_persona_legisladora.DataSource = dataTable;
                    cbm_causa_fallecimiento_persona_legisladora.DisplayMember = "descripcion";

                    cbm_causa_fallecimiento_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cbm_causa_fallecimiento_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cbm_causa_fallecimiento_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cbm_causa_fallecimiento_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cbm_causa_fallecimiento_persona_legisladora_Validating(object sender, CancelEventArgs e)
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

        // caracter_cargo_persona_legisladora
        private void cmb_Caracter_cargo_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_CARACTER_CARGO";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_caracter_cargo_persona_legisladora.DataSource = dataTable;
                    cmb_caracter_cargo_persona_legisladora.DisplayMember = "descripcion";

                    cmb_caracter_cargo_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_caracter_cargo_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_caracter_cargo_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_caracter_cargo_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_caracter_cargo_persona_legisladora_Validating(object sender, CancelEventArgs e)
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

        private void cmb_Escolaridad_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_ESCOLARIDAD";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_escolaridad_persona_legisladora_PL.DataSource = dataTable;
                    cmb_escolaridad_persona_legisladora_PL.DisplayMember = "descripcion";

                    cmb_escolaridad_persona_legisladora_PL.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_escolaridad_persona_legisladora_PL.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_escolaridad_persona_legisladora_PL.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_escolaridad_persona_legisladora_PL.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Persona_Legisladora_Propietaria()
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
                    string query = "SELECT " +
                                   "(" +
                                   "IFNULL(txt_nombre_1_persona_legisladora, '') || ' ' || " +
                                   "IFNULL(txt_nombre_2_persona_legisladora, '') || ' ' || " +
                                   "IFNULL(txt_nombre_3_persona_legisladora, '') || ' ' || " +
                                   "IFNULL(txt_apellido_1_persona_legisladora, '') || ' ' || " +
                                   "IFNULL(txt_apellido_2_persona_legisladora, '') || ' ' || " +
                                   "IFNULL(txt_apellido_3_persona_legisladora, '') || ' - ' || " +
                                   "txt_ID_persona_legisladora" +
                                   ") AS descripcion " +
                                   "FROM TR_PERSONAS_LEGISLADORAS " +
                                   "WHERE cmb_caracter_cargo_persona_legisladora = 'Propietario' AND id_legislatura = @id_legis";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        // Asignar el parámetro
                        cmd.Parameters.AddWithValue("@id_legis", id_legis);

                        // Utilizar un DataAdapter para obtener los datos
                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            cmb_nombre_persona_legisladora_propietaria.DataSource = dataTable;
                            cmb_nombre_persona_legisladora_propietaria.DisplayMember = "descripcion";
                        }
                    }

                    cmb_nombre_persona_legisladora_propietaria.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_nombre_persona_legisladora_propietaria.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cmb_nombre_persona_legisladora_propietaria.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_nombre_persona_legisladora_propietaria.SelectedIndex = -1; // Aquí se establece como vacío
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al llenar el ComboBox cmb_nombre_persona_legisladora_propietaria: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }
            }
        }

        // nombre_persona_legisladora_propietaria
        private void cmb_nombre_persona_legisladora_propietaria_SelectedIndexChanged(object sender, EventArgs e)
        {

            string nombreCompleto = cmb_nombre_persona_legisladora_propietaria.Text;

            // Verificar si el nombre completo es nulo o vacío
            if (string.IsNullOrEmpty(nombreCompleto))
            {
                txt_ID_persona_legisladora_propietaria.Text = "";
                return;
            }

            // Eliminar espacios adicionales y separar el nombre completo en partes

            // Separar el texto utilizando el delimitador '-'
            string[] partes = nombreCompleto.Split('-');

            // Verificar si la separación resultó en al menos dos partes
            if (partes.Length < 2)
            {
                txt_ID_persona_legisladora_propietaria.Text = "";
                return;
            }

            // Extraer la parte que contiene el número
            string id = partes[1].Trim();

            txt_ID_persona_legisladora_propietaria.Text = id;

        }
        private void cmb_nombre_persona_legisladora_propietaria_Validating(object sender, CancelEventArgs e)
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

                // Permitir campo vacío
                if (string.IsNullOrEmpty(cleanedText))
                {
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

        // estatus_escolaridad_persona_legisladora
        private void cmb_Estatus_escolaridad_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_ESTATUS_ESCOLARIDAD";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_estatus_escolaridad_persona_legisladora.DataSource = dataTable;
                    cmb_estatus_escolaridad_persona_legisladora.DisplayMember = "descripcion";

                    cmb_estatus_escolaridad_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_estatus_escolaridad_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_estatus_escolaridad_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_estatus_escolaridad_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_estatus_escolaridad_persona_legisladora_Validating(object sender, CancelEventArgs e)
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

        // carrera_licenciatura_persona_legisladora
        private void cmb_Carrera_licenciatura_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_CARRERAS";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_carrera_licenciatura_persona_legisladora_PL.DataSource = dataTable;
                    cmb_carrera_licenciatura_persona_legisladora_PL.DisplayMember = "descripcion";

                    cmb_carrera_licenciatura_persona_legisladora_PL.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_carrera_licenciatura_persona_legisladora_PL.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_carrera_licenciatura_persona_legisladora_PL.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_carrera_licenciatura_persona_legisladora_PL.SelectedIndex = -1; // Aquí se establece como vacío
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al llenar el ComboBox cmb_Carrera_licenciatura_persona_legisladora: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }

            }
        }
        private void cmb_carrera_licenciatura_persona_legisladora_PL_Validating(object sender, CancelEventArgs e)
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

        // carrera_maestria_persona_legisladora_PL
        private void cmb_Carrera_maestria_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_CARRERAS";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_carrera_maestria_persona_legisladora_PL.DataSource = dataTable;
                    cmb_carrera_maestria_persona_legisladora_PL.DisplayMember = "descripcion";

                    cmb_carrera_maestria_persona_legisladora_PL.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_carrera_maestria_persona_legisladora_PL.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_carrera_maestria_persona_legisladora_PL.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_carrera_maestria_persona_legisladora_PL.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_carrera_maestria_persona_legisladora_PL_Validating(object sender, CancelEventArgs e)
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

        // carrera_doctorado_persona_legisladora_PL
        private void cmb_Carrera_doctorado_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_CARRERAS";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_carrera_doctorado_persona_legisladora_PL.DataSource = dataTable;
                    cmb_carrera_doctorado_persona_legisladora_PL.DisplayMember = "descripcion";

                    cmb_carrera_doctorado_persona_legisladora_PL.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_carrera_doctorado_persona_legisladora_PL.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_carrera_doctorado_persona_legisladora_PL.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_carrera_doctorado_persona_legisladora_PL.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_carrera_doctorado_persona_legisladora_PL_Validating(object sender, CancelEventArgs e)
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

        // lengua_ind_persona_legisladora
        private void cmb_Cond_lengua_ind_persona_legisladora()
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

                    cmb_cond_lengua_ind_persona_legisladora_PL.DataSource = dataTable;
                    cmb_cond_lengua_ind_persona_legisladora_PL.DisplayMember = "descripcion";

                    cmb_cond_lengua_ind_persona_legisladora_PL.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_lengua_ind_persona_legisladora_PL.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_lengua_ind_persona_legisladora_PL.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_lengua_ind_persona_legisladora_PL.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Lengua_ind_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_LENGUA_INDIGENA";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_lengua_ind_persona_legisladora.DataSource = dataTable;
                    cmb_lengua_ind_persona_legisladora.DisplayMember = "descripcion";

                    cmb_lengua_ind_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_lengua_ind_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_lengua_ind_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_lengua_ind_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_lengua_ind_persona_legisladora_Validating(object sender, CancelEventArgs e)
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

                // Permitir campo vacío
                if (string.IsNullOrEmpty(cleanedText))
                {
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

        // cond_discapacidad_persona_legisladora
        private void cmb_Cond_discapacidad_persona_legisladora()
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

                    cmb_cond_discapacidad_persona_legisladora.DataSource = dataTable;
                    cmb_cond_discapacidad_persona_legisladora.DisplayMember = "descripcion";

                    cmb_cond_discapacidad_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_discapacidad_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_discapacidad_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_discapacidad_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_discapacidad_persona_legisladora_Validating(object sender, CancelEventArgs e)
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

        // tipo_discapacidad_persona_legisladora
        private void cmb_Tipo_discapacidad_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_TIPO_DISCAPACIDAD";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_tipo_discapacidad_persona_legisladora.DataSource = dataTable;
                    cmb_tipo_discapacidad_persona_legisladora.DisplayMember = "descripcion";

                    cmb_tipo_discapacidad_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_tipo_discapacidad_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_tipo_discapacidad_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_tipo_discapacidad_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_tipo_discapacidad_persona_legisladora_Validating(object sender, CancelEventArgs e)
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

                // Permitir campo vacío
                if (string.IsNullOrEmpty(cleanedText))
                {
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

        // cond_pueblo_ind_persona_legisladora_PL
        private void cmb_Cond_pueblo_ind_persona_legisladora()
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

                    cmb_cond_pueblo_ind_persona_legisladora_PL.DataSource = dataTable;
                    cmb_cond_pueblo_ind_persona_legisladora_PL.DisplayMember = "descripcion";

                    cmb_cond_pueblo_ind_persona_legisladora_PL.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_pueblo_ind_persona_legisladora_PL.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_pueblo_ind_persona_legisladora_PL.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_pueblo_ind_persona_legisladora_PL.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_pueblo_ind_persona_legisladora_PL_Validating(object sender, CancelEventArgs e)
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

        // pueblo_ind_persona_legisladora_PL
        private void cmb_Pueblo_ind_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_PUEBLO_INDIGENA";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_pueblo_ind_persona_legisladora_PL.DataSource = dataTable;
                    cmb_pueblo_ind_persona_legisladora_PL.DisplayMember = "descripcion";

                    cmb_pueblo_ind_persona_legisladora_PL.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_pueblo_ind_persona_legisladora_PL.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_pueblo_ind_persona_legisladora_PL.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_pueblo_ind_persona_legisladora_PL.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_pueblo_ind_persona_legisladora_PL_Validating(object sender, CancelEventArgs e)
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

        // Cond_pob_diversidad_sexual_persona_legisladora
        private void cmb_Cond_pob_diversidad_sexual_persona_legisladora()
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

                    cmb_cond_pob_diversidad_sexual_persona_legisladora.DataSource = dataTable;
                    cmb_cond_pob_diversidad_sexual_persona_legisladora.DisplayMember = "descripcion";

                    cmb_cond_pob_diversidad_sexual_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_pob_diversidad_sexual_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_pob_diversidad_sexual_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_pob_diversidad_sexual_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_pob_diversidad_sexual_persona_legisladora_Validating(object sender, CancelEventArgs e)
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

        // Cond_pob_afromexicana_persona_legisladora
        private void cmb_Cond_pob_afromexicana_persona_legisladora()
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

                    cmb_cond_pob_afromexicana_persona_legisladora_PL.DataSource = dataTable;
                    cmb_cond_pob_afromexicana_persona_legisladora_PL.DisplayMember = "descripcion";

                    cmb_cond_pob_afromexicana_persona_legisladora_PL.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_pob_afromexicana_persona_legisladora_PL.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_pob_afromexicana_persona_legisladora_PL.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_pob_afromexicana_persona_legisladora_PL.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_pob_afromexicana_persona_legisladora_PL_Validating(object sender, CancelEventArgs e)
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

        // empleo_anterior_persona_legisladora
        private void cmb_Empleo_anterior_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_EMPLEO_ANT";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_empleo_anterior_persona_legisladora.DataSource = dataTable;
                    cmb_empleo_anterior_persona_legisladora.DisplayMember = "descripcion";

                    cmb_empleo_anterior_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_empleo_anterior_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_empleo_anterior_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_empleo_anterior_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_empleo_anterior_persona_legisladora_Validating(object sender, CancelEventArgs e)
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

        // antigüedad_servicio_publico_persona_legisladora
        private void cmb_Antigüedad_servicio_publico_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_ANTIGUEDAD";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_antigüedad_servicio_publico_persona_legisladora.DataSource = dataTable;
                    cmb_antigüedad_servicio_publico_persona_legisladora.DisplayMember = "descripcion";

                    cmb_antigüedad_servicio_publico_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_antigüedad_servicio_publico_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_antigüedad_servicio_publico_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_antigüedad_servicio_publico_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_antigüedad_servicio_publico_persona_legisladora_Validating(object sender, CancelEventArgs e)
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

        // antigüedad_persona_legisladora
        private void cmb_Antigüedad_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_ANTIGUEDAD";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_antigüedad_persona_legisladora.DataSource = dataTable;
                    cmb_antigüedad_persona_legisladora.DisplayMember = "descripcion";

                    cmb_antigüedad_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_antigüedad_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_antigüedad_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_antigüedad_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_antigüedad_persona_legisladora_Validating(object sender, CancelEventArgs e)
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

        // forma_eleccion_persona_legisladora
        private void cmb_Forma_eleccion_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_FORMA_ELECCION";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_forma_eleccion_persona_legisladora.DataSource = dataTable;
                    cmb_forma_eleccion_persona_legisladora.DisplayMember = "descripcion";

                    cmb_forma_eleccion_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_forma_eleccion_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_forma_eleccion_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_forma_eleccion_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_forma_eleccion_persona_legisladora_Validating(object sender, CancelEventArgs e)
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

        // tipo_candidatura_persona_legisladora
        private void cmb_Tipo_candidatura_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_TIPO_CANDIDATURA";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_tipo_candidatura_persona_legisladora.DataSource = dataTable;
                    cmb_tipo_candidatura_persona_legisladora.DisplayMember = "descripcion";

                    cmb_tipo_candidatura_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_tipo_candidatura_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_tipo_candidatura_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_tipo_candidatura_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_tipo_candidatura_persona_legisladora_Validating(object sender, CancelEventArgs e)
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

        // tipo_adscripcion_inicial_persona_legisladora
        private void cmb_Tipo_adscripcion_inicial_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_TIPO_ADSCRIPCION";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_tipo_adscripcion_final_persona_legisladora.DataSource = dataTable;
                    cmb_tipo_adscripcion_final_persona_legisladora.DisplayMember = "descripcion";

                    cmb_tipo_adscripcion_final_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_tipo_adscripcion_final_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_tipo_adscripcion_final_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_tipo_adscripcion_final_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_tipo_adscripcion_inicial_persona_legisladora_Validating(object sender, CancelEventArgs e)
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

        // tipo_adscripcion_final_persona_legisladora
        private void cmb_Tipo_adscripcion_final_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_TIPO_ADSCRIPCION";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_tipo_adscripcion_inicial_persona_legisladora.DataSource = dataTable;
                    cmb_tipo_adscripcion_inicial_persona_legisladora.DisplayMember = "descripcion";

                    cmb_tipo_adscripcion_inicial_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_tipo_adscripcion_inicial_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_tipo_adscripcion_inicial_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_tipo_adscripcion_inicial_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_tipo_adscripcion_final_persona_legisladora_Validating(object sender, CancelEventArgs e)
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

        // cond_presentacion_declaracion_situacion_patrimonial
        private void cmb_Cond_presentacion_declaracion_situacion_patrimonial()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_SI_NO where id_si_no in (1,2,4,3)";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cond_presentacion_declaracion_situacion_patrimonial.DataSource = dataTable;
                    cmb_cond_presentacion_declaracion_situacion_patrimonial.DisplayMember = "descripcion";

                    cmb_cond_presentacion_declaracion_situacion_patrimonial.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_presentacion_declaracion_situacion_patrimonial.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_presentacion_declaracion_situacion_patrimonial.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_presentacion_declaracion_situacion_patrimonial.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_presentacion_declaracion_situacion_patrimonial_Validating(object sender, CancelEventArgs e)
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


        // cond_presentacion_declaracion_intereses
        private void cmb_Cond_presentacion_declaracion_intereses()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_SI_NO where id_si_no in (1,2,3,4)";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cond_presentacion_declaracion_intereses.DataSource = dataTable;
                    cmb_cond_presentacion_declaracion_intereses.DisplayMember = "descripcion";

                    cmb_cond_presentacion_declaracion_intereses.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_presentacion_declaracion_intereses.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_presentacion_declaracion_intereses.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_presentacion_declaracion_intereses.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_presentacion_declaracion_intereses_Validating(object sender, CancelEventArgs e)
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


        // Cond_presentacion_declaracion_fiscal
        private void cmb_Cond_presentacion_declaracion_fiscal()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_SI_NO where id_si_no in (1,2,3,4)";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cond_presentacion_declaracion_fiscal.DataSource = dataTable;
                    cmb_cond_presentacion_declaracion_fiscal.DisplayMember = "descripcion";

                    cmb_cond_presentacion_declaracion_fiscal.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_presentacion_declaracion_fiscal.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_presentacion_declaracion_fiscal.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_presentacion_declaracion_fiscal.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_presentacion_declaracion_fiscal_Validating(object sender, CancelEventArgs e)
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

        // distrito_electoral_mayoria_relativa
        private void cmb_distrito_electoral_mayoria_relativa_Validating(object sender, CancelEventArgs e)
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

        // cond_casa_atencion_ciudadana
        private void cmb_Cond_casa_atencion_ciudadana()
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

                    cmb_cond_casa_atencion_ciudadana.DataSource = dataTable;
                    cmb_cond_casa_atencion_ciudadana.DisplayMember = "descripcion";

                    cmb_cond_casa_atencion_ciudadana.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_casa_atencion_ciudadana.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_casa_atencion_ciudadana.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_casa_atencion_ciudadana.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_casa_atencion_ciudadana_Validating(object sender, CancelEventArgs e)
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

        // cond_casa_atencion_ciudadana_movil
        private void cmb_Cond_casa_atencion_ciudadana_movil()
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

                    cmb_cond_casa_atencion_ciudadana_movil.DataSource = dataTable;
                    cmb_cond_casa_atencion_ciudadana_movil.DisplayMember = "descripcion";

                    cmb_cond_casa_atencion_ciudadana_movil.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_casa_atencion_ciudadana_movil.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_casa_atencion_ciudadana_movil.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_casa_atencion_ciudadana_movil.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_casa_atencion_ciudadana_movil_Validating(object sender, CancelEventArgs e)
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

        // cond_integrante_comision_permanente
        private void cmb_Cond_integrante_comision_permanente()
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

                    cmb_cond_integrante_comision_permanente.DataSource = dataTable;
                    cmb_cond_integrante_comision_permanente.DisplayMember = "descripcion";

                    cmb_cond_integrante_comision_permanente.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_integrante_comision_permanente.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_integrante_comision_permanente.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_integrante_comision_permanente.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_integrante_comision_permanente_Validating(object sender, CancelEventArgs e)
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

        // cargo_comision_permanente
        private void cmb_Cargo_comision_permanente()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_CARGO";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cargo_comision_permanente.DataSource = dataTable;
                    cmb_cargo_comision_permanente.DisplayMember = "descripcion";

                    cmb_cargo_comision_permanente.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cargo_comision_permanente.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cargo_comision_permanente.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cargo_comision_permanente.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cargo_comision_permanente_Validating(object sender, CancelEventArgs e)
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

        // nombre_comision_legislativa
        private void cmb_Nombre_comision_legislativa()
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
                    string query = "SELECT nombre_comision_legislativa FROM TR_COMISIONES_LEGISLATIVAS WHERE id_legislatura = @id_legis";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        // Asignar el parámetro
                        cmd.Parameters.AddWithValue("@id_legis", id_legis);

                        // Utilizar un DataAdapter para obtener los datos
                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            cmb_nombre_comision_legislativa.DataSource = dataTable;
                            cmb_nombre_comision_legislativa.DisplayMember = "nombre_comision_legislativa";
                        }
                    }

                    cmb_nombre_comision_legislativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_nombre_comision_legislativa.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cmb_nombre_comision_legislativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_nombre_comision_legislativa.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_nombre_comision_legislativa_Validating(object sender, CancelEventArgs e)
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

        // cargo_comision_legislativa
        private void cmb_Cargo_comision_legislativa()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_CARGO";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cargo_comision_legislativa.DataSource = dataTable;
                    cmb_cargo_comision_legislativa.DisplayMember = "descripcion";

                    cmb_cargo_comision_legislativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cargo_comision_legislativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cargo_comision_legislativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cargo_comision_legislativa.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cargo_comision_legislativa_Validating(object sender, CancelEventArgs e)
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

        // cond_integrante_jucopo
        private void cmb_Cond_integrante_jucopo()
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

                    cmb_cond_integrante_jucopo.DataSource = dataTable;
                    cmb_cond_integrante_jucopo.DisplayMember = "descripcion";

                    cmb_cond_integrante_jucopo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_integrante_jucopo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_integrante_jucopo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_integrante_jucopo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_integrante_jucopo_Validating(object sender, CancelEventArgs e)
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

        // Cargo_jucopo
        private void cmb_Cargo_jucopo()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_CARGO";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cargo_jucopo.DataSource = dataTable;
                    cmb_cargo_jucopo.DisplayMember = "descripcion";

                    cmb_cargo_jucopo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cargo_jucopo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cargo_jucopo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cargo_jucopo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cargo_jucopo_Validating(object sender, CancelEventArgs e)
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

        // cond_integrante_mesa_directiva
        private void cmb_Cond_integrante_mesa_directiva()
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

                    cmb_cond_integrante_mesa_directiva.DataSource = dataTable;
                    cmb_cond_integrante_mesa_directiva.DisplayMember = "descripcion";

                    cmb_cond_integrante_mesa_directiva.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_integrante_mesa_directiva.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_integrante_mesa_directiva.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_integrante_mesa_directiva.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_integrante_mesa_directiva_Validating(object sender, CancelEventArgs e)
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

        // Cargo_mesa_directiva
        private void cmb_Cargo_mesa_directiva_PL()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_CARGO";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cargo_mesa_directiva_PL.DataSource = dataTable;
                    cmb_cargo_mesa_directiva_PL.DisplayMember = "descripcion";

                    cmb_cargo_mesa_directiva_PL.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cargo_mesa_directiva_PL.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cargo_mesa_directiva_PL.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cargo_mesa_directiva_PL.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cargo_mesa_directiva_PL_Validating(object sender, CancelEventArgs e)
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

        private void txt_nombre_1_persona_legisladora_TextChanged(object sender, EventArgs e)
        {
            cmb_Nombre_comision_legislativa();

            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_nombre_1_persona_legisladora.Text = txt_nombre_1_persona_legisladora.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_nombre_1_persona_legisladora.SelectionStart = txt_nombre_1_persona_legisladora.Text.Length;

            if (string.IsNullOrWhiteSpace(txt_nombre_1_persona_legisladora.Text))
            {
                //MessageBox.Show("Debe especificar el nombre.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_nombre_2_persona_legisladora.BackColor = Color.LightGray; txt_nombre_2_persona_legisladora.Enabled = false;
                txt_nombre_2_persona_legisladora.Clear();
                txt_nombre_1_persona_legisladora.Focus();
            }
            else
            {
                //txt_nombre_2_persona_legisladora.Visible = false;
                txt_nombre_2_persona_legisladora.Enabled = true; txt_nombre_2_persona_legisladora.BackColor = Color.Honeydew;
            }

            // CONSTRUCCION ID
            string primerNombre = txt_nombre_1_persona_legisladora.Text;
            string segundoNombre = txt_nombre_2_persona_legisladora.Text;
            string tercerNombre = txt_nombre_3_persona_legisladora.Text;
            string primerApellido = txt_apellido_1_persona_legisladora.Text;
            string segundoApellido = txt_apellido_2_persona_legisladora.Text;
            string tercerApellido = txt_apellido_3_persona_legisladora.Text;
            string sexo1 = cmb_sexo_persona_legisladora.Text;
            DateTime fechaNacimiento = dtp_fecha_nacimiento_persona_legisladora.Value;

            string uniqueID = GenerateUniqueID(primerNombre, segundoNombre, tercerNombre,
                    primerApellido, segundoApellido, tercerApellido,
                    sexo1, fechaNacimiento);
            txt_ID_persona_legisladora.Text = uniqueID;
        }
        public static string GenerateUniqueID(string primerNombre, string segundoNombre, string tercerNombre,
            string primerApellido, string segundoApellido, string tercerApellido,
            string sexo, DateTime fechaNacimiento)
        {
            // Concatenar los datos en un string
            string dataToHash = $"{primerNombre}{segundoNombre}{tercerNombre}{primerApellido}{segundoApellido}{tercerApellido}{sexo}{fechaNacimiento.ToString("yyyyMMdd")}";

            // Generar el hash SHA-256
            string uniqueID = CalculateSHA256(dataToHash);

            return uniqueID.Substring(0, 12); // Tomamos solo los primeros 12 caracteres del hash
        }
        private static string CalculateSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("x2")); // Convierte cada byte a su representación hexadecimal
                }
                return sb.ToString();
            }
        }
        private void txt_nombre_2_persona_legisladora_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_nombre_2_persona_legisladora.Text = txt_nombre_2_persona_legisladora.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_nombre_2_persona_legisladora.SelectionStart = txt_nombre_2_persona_legisladora.Text.Length;

            if (string.IsNullOrWhiteSpace(txt_nombre_2_persona_legisladora.Text))
            {
                //MessageBox.Show("Debe especificar el nombre.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_nombre_3_persona_legisladora.BackColor = Color.LightGray; txt_nombre_3_persona_legisladora.Enabled = false;
                txt_nombre_3_persona_legisladora.Clear();
                txt_nombre_2_persona_legisladora.Focus();
            }
            else
            {
                txt_nombre_3_persona_legisladora.Enabled = true; txt_nombre_3_persona_legisladora.BackColor = Color.Honeydew;
            }

            // CONSTRUCCION ID
            string primerNombre = txt_nombre_1_persona_legisladora.Text;
            string segundoNombre = txt_nombre_2_persona_legisladora.Text;
            string tercerNombre = txt_nombre_3_persona_legisladora.Text;
            string primerApellido = txt_apellido_1_persona_legisladora.Text;
            string segundoApellido = txt_apellido_2_persona_legisladora.Text;
            string tercerApellido = txt_apellido_3_persona_legisladora.Text;
            string sexo1 = cmb_sexo_persona_legisladora.Text;
            DateTime fechaNacimiento = dtp_fecha_nacimiento_persona_legisladora.Value;

            string uniqueID = GenerateUniqueID(primerNombre, segundoNombre, tercerNombre,
                    primerApellido, segundoApellido, tercerApellido,
                    sexo1, fechaNacimiento);
            txt_ID_persona_legisladora.Text = uniqueID;
        }
        private void txt_nombre_3_persona_legisladora_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_nombre_3_persona_legisladora.Text = txt_nombre_3_persona_legisladora.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_nombre_3_persona_legisladora.SelectionStart = txt_nombre_3_persona_legisladora.Text.Length;

            // CONSTRUCCION ID
            string primerNombre = txt_nombre_1_persona_legisladora.Text;
            string segundoNombre = txt_nombre_2_persona_legisladora.Text;
            string tercerNombre = txt_nombre_3_persona_legisladora.Text;
            string primerApellido = txt_apellido_1_persona_legisladora.Text;
            string segundoApellido = txt_apellido_2_persona_legisladora.Text;
            string tercerApellido = txt_apellido_3_persona_legisladora.Text;
            string sexo1 = cmb_sexo_persona_legisladora.Text;
            DateTime fechaNacimiento = dtp_fecha_nacimiento_persona_legisladora.Value;

            string uniqueID = GenerateUniqueID(primerNombre, segundoNombre, tercerNombre,
                    primerApellido, segundoApellido, tercerApellido,
                    sexo1, fechaNacimiento);
            txt_ID_persona_legisladora.Text = uniqueID;
        }
        private void txt_apellido_1_persona_legisladora_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_apellido_1_persona_legisladora.Text = txt_apellido_1_persona_legisladora.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_apellido_1_persona_legisladora.SelectionStart = txt_apellido_1_persona_legisladora.Text.Length;

            if (string.IsNullOrWhiteSpace(txt_apellido_1_persona_legisladora.Text))
            {
                //MessageBox.Show("Debe especificar el apellido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_apellido_2_persona_legisladora.BackColor = Color.LightGray; txt_apellido_2_persona_legisladora.Enabled = false;
                txt_apellido_2_persona_legisladora.Clear();
                txt_apellido_1_persona_legisladora.Focus();
            }
            else
            {
                txt_apellido_2_persona_legisladora.Enabled = true; txt_apellido_2_persona_legisladora.BackColor = Color.Honeydew;

                // CONSTRUCCION ID
                string primerNombre = txt_nombre_1_persona_legisladora.Text;
                string segundoNombre = txt_nombre_2_persona_legisladora.Text;
                string tercerNombre = txt_nombre_3_persona_legisladora.Text;
                string primerApellido = txt_apellido_1_persona_legisladora.Text;
                string segundoApellido = txt_apellido_2_persona_legisladora.Text;
                string tercerApellido = txt_apellido_3_persona_legisladora.Text;
                string sexo1 = cmb_sexo_persona_legisladora.Text;
                DateTime fechaNacimiento = dtp_fecha_nacimiento_persona_legisladora.Value;

                string uniqueID = GenerateUniqueID(primerNombre, segundoNombre, tercerNombre,
                    primerApellido, segundoApellido, tercerApellido,
                    sexo1, fechaNacimiento);
                txt_ID_persona_legisladora.Text = uniqueID;
            }
        }
        private void txt_apellido_2_persona_legisladora_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_apellido_2_persona_legisladora.Text = txt_apellido_2_persona_legisladora.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_apellido_2_persona_legisladora.SelectionStart = txt_apellido_2_persona_legisladora.Text.Length;

            if (string.IsNullOrWhiteSpace(txt_apellido_2_persona_legisladora.Text))
            {
                //MessageBox.Show("Debe especificar el apellido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_apellido_3_persona_legisladora.BackColor = Color.LightGray; txt_apellido_3_persona_legisladora.Enabled = false;
                txt_apellido_3_persona_legisladora.Clear();
                txt_apellido_2_persona_legisladora.Focus();
            }
            else
            {
                txt_apellido_3_persona_legisladora.Enabled = true; txt_apellido_3_persona_legisladora.BackColor = Color.Honeydew;

                // CONSTRUCCION ID
                string primerNombre = txt_nombre_1_persona_legisladora.Text;
                string segundoNombre = txt_nombre_2_persona_legisladora.Text;
                string tercerNombre = txt_nombre_3_persona_legisladora.Text;
                string primerApellido = txt_apellido_1_persona_legisladora.Text;
                string segundoApellido = txt_apellido_2_persona_legisladora.Text;
                string tercerApellido = txt_apellido_3_persona_legisladora.Text;
                string sexo1 = cmb_sexo_persona_legisladora.Text;
                DateTime fechaNacimiento = dtp_fecha_nacimiento_persona_legisladora.Value;

                string uniqueID = GenerateUniqueID(primerNombre, segundoNombre, tercerNombre,
                    primerApellido, segundoApellido, tercerApellido,
                    sexo1, fechaNacimiento);
                txt_ID_persona_legisladora.Text = uniqueID;
            }
        }
        private void txt_apellido_3_persona_legisladora_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_apellido_3_persona_legisladora.Text = txt_apellido_3_persona_legisladora.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_apellido_3_persona_legisladora.SelectionStart = txt_apellido_3_persona_legisladora.Text.Length;

            // CONSTRUCCION ID
            string primerNombre = txt_nombre_1_persona_legisladora.Text;
            string segundoNombre = txt_nombre_2_persona_legisladora.Text;
            string tercerNombre = txt_nombre_3_persona_legisladora.Text;
            string primerApellido = txt_apellido_1_persona_legisladora.Text;
            string segundoApellido = txt_apellido_2_persona_legisladora.Text;
            string tercerApellido = txt_apellido_3_persona_legisladora.Text;
            string sexo1 = cmb_sexo_persona_legisladora.Text;
            DateTime fechaNacimiento = dtp_fecha_nacimiento_persona_legisladora.Value;

            string uniqueID = GenerateUniqueID(primerNombre, segundoNombre, tercerNombre,
                    primerApellido, segundoApellido, tercerApellido,
                    sexo1, fechaNacimiento);
            txt_ID_persona_legisladora.Text = uniqueID;
        }
        private void txt_otro_estatus_persona_legisladora_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_otro_estatus_persona_legisladora_especifique.Text = txt_otro_estatus_persona_legisladora_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_otro_estatus_persona_legisladora_especifique.SelectionStart = txt_otro_estatus_persona_legisladora_especifique.Text.Length;
        }

        // estatus_persona_legisladora
        private void cmb_estatus_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_estatus_persona_legisladora.Text.ToString();

            if (valorComboBox1 == "Otro estatus (especifique)")
            {
                txt_otro_estatus_persona_legisladora_especifique.Enabled = true; txt_otro_estatus_persona_legisladora_especifique.BackColor = Color.Honeydew;
                txt_otro_estatus_persona_legisladora_especifique.Focus();
            }
            else
            {
                txt_otro_estatus_persona_legisladora_especifique.Enabled = false; txt_otro_estatus_persona_legisladora_especifique.BackColor = Color.LightGray;
                txt_otro_estatus_persona_legisladora_especifique.Text = "";
            }

            if (valorComboBox1 == "Fallecimiento")
            {
                cbm_causa_fallecimiento_persona_legisladora.Enabled = true; cbm_causa_fallecimiento_persona_legisladora.BackColor = Color.Honeydew;
                cbm_causa_fallecimiento_persona_legisladora.Focus();
            }
            else
            {
                cbm_causa_fallecimiento_persona_legisladora.Enabled = false; cbm_causa_fallecimiento_persona_legisladora.BackColor = Color.LightGray;
                cbm_causa_fallecimiento_persona_legisladora.SelectedIndex = -1;
            }

            if (valorComboBox1 == "Con licencia")
            {
                cbm_tipo_licencia_persona_legisladora.Enabled = true; cbm_tipo_licencia_persona_legisladora.BackColor = Color.Honeydew;
                cbm_tipo_licencia_persona_legisladora.Focus();
            }
            else
            {
                cbm_tipo_licencia_persona_legisladora.Enabled = false; cbm_tipo_licencia_persona_legisladora.BackColor = Color.LightGray;
                cbm_tipo_licencia_persona_legisladora.SelectedIndex = -1;
            }
        }
        private void cmb_estatus_persona_legisladora_Validating(object sender, CancelEventArgs e)
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

        private void txt_otro_estatus_persona_legisladora_especifique_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_otro_estatus_persona_legisladora_especifique.Text))
            {
                MessageBox.Show("Debe especificar el otro tipo de estatus.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_otro_estatus_persona_legisladora_especifique.Focus();
            }
        }
        private void cmb_caracter_cargo_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_caracter_cargo_persona_legisladora.Text.ToString();

            if (valorComboBox1 == "Suplente")
            {
                cmb_Persona_Legisladora_Propietaria();
                cmb_nombre_persona_legisladora_propietaria.Enabled = true; cmb_nombre_persona_legisladora_propietaria.BackColor = Color.Honeydew;
                cmb_nombre_persona_legisladora_propietaria.Focus();
            }
            else
            {
                cmb_nombre_persona_legisladora_propietaria.Enabled = false; cmb_nombre_persona_legisladora_propietaria.BackColor = Color.LightGray;
                cmb_nombre_persona_legisladora_propietaria.SelectedIndex = -1; ;
            }
        }

        // escolaridad_persona_legisladora_PL
        private void cmb_escolaridad_persona_legisladora_PL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            if (cmb_escolaridad_persona_legisladora_PL.SelectedItem != null)
            {
                // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
                string valorComboBox = cmb_escolaridad_persona_legisladora_PL.Text.ToString();


                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        string query;

                        switch (valorComboBox)
                        {
                            case "Ninguno":
                                query = "select descripcion from TC_ESTATUS_ESCOLARIDAD where id_estatus_escolaridad in (8)";
                                break;
                            case "Preescolar o primaria":
                                query = "select descripcion from TC_ESTATUS_ESCOLARIDAD where id_estatus_escolaridad in (1,2,3,9)";
                                break;
                            case "Secundaria":
                                query = "select descripcion from TC_ESTATUS_ESCOLARIDAD where id_estatus_escolaridad in (1,2,3,9)";
                                break;
                            case "Preparatoria":
                                query = "select descripcion from TC_ESTATUS_ESCOLARIDAD where id_estatus_escolaridad in (1,2,3,9)";
                                break;
                            case "Carrera técnica o carrera comercial":
                                query = "select descripcion from TC_ESTATUS_ESCOLARIDAD where id_estatus_escolaridad in (1,2,3,4,9)";
                                break;
                            case "Licenciatura":
                                query = "select descripcion from TC_ESTATUS_ESCOLARIDAD where id_estatus_escolaridad in (1,2,3,4,9)";
                                break;
                            case "Maestría":
                                query = "select descripcion from TC_ESTATUS_ESCOLARIDAD where id_estatus_escolaridad in (1,2,3,4,9)";
                                break;
                            case "Doctorado":
                                query = "select descripcion from TC_ESTATUS_ESCOLARIDAD where id_estatus_escolaridad in (1,2,3,4,9)";
                                break;
                            case "No identificado":
                                query = "select descripcion from TC_ESTATUS_ESCOLARIDAD where id_estatus_escolaridad in (9)";
                                break;

                            default:
                                query = "select descripcion from TC_ESTATUS_ESCOLARIDAD where id_estatus_escolaridad in (10)";
                                break;
                        }

                        // comando de sql
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        cmb_estatus_escolaridad_persona_legisladora.DataSource = dataTable;
                        cmb_estatus_escolaridad_persona_legisladora.DisplayMember = "descripcion";

                        cmb_estatus_escolaridad_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_estatus_escolaridad_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_estatus_escolaridad_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_estatus_escolaridad_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío

                        if (valorComboBox == "Licenciatura")
                        {
                            cmb_carrera_licenciatura_persona_legisladora_PL.Enabled = true; cmb_carrera_licenciatura_persona_legisladora_PL.BackColor = Color.Honeydew;
                            cmb_carrera_maestria_persona_legisladora_PL.Enabled = false; cmb_carrera_maestria_persona_legisladora_PL.BackColor = Color.LightGray;
                            cmb_carrera_doctorado_persona_legisladora_PL.Enabled = false; cmb_carrera_doctorado_persona_legisladora_PL.BackColor = Color.LightGray;
                            dgv_nivel_escolaridad_PL.BackgroundColor = Color.Honeydew;

                            cmb_carrera_licenciatura_persona_legisladora_PL.Focus();
                            btnAgregarNivelEscPL.Enabled = true; btnEliminarNivelEscPL.Enabled = true;

                            cmb_carrera_maestria_persona_legisladora_PL.Text = ""; cmb_carrera_doctorado_persona_legisladora_PL.Text = "";
                        }
                        else if (valorComboBox == "Maestría")
                        {

                            cmb_carrera_licenciatura_persona_legisladora_PL.Enabled = true; cmb_carrera_licenciatura_persona_legisladora_PL.BackColor = Color.Honeydew;
                            cmb_carrera_maestria_persona_legisladora_PL.Enabled = true; cmb_carrera_maestria_persona_legisladora_PL.BackColor = Color.Honeydew;
                            cmb_carrera_doctorado_persona_legisladora_PL.Enabled = false; cmb_carrera_doctorado_persona_legisladora_PL.BackColor = Color.LightGray;
                            dgv_nivel_escolaridad_PL.BackgroundColor = Color.Honeydew;
                            cmb_carrera_licenciatura_persona_legisladora_PL.Focus();

                            btnAgregarNivelEscPL.Enabled = true; btnEliminarNivelEscPL.Enabled = true;

                            cmb_carrera_doctorado_persona_legisladora_PL.Text = "";
                        }
                        else if (valorComboBox == "Doctorado")
                        {
                            cmb_carrera_licenciatura_persona_legisladora_PL.Enabled = true; cmb_carrera_licenciatura_persona_legisladora_PL.BackColor = Color.Honeydew;
                            cmb_carrera_maestria_persona_legisladora_PL.Enabled = true; cmb_carrera_maestria_persona_legisladora_PL.BackColor = Color.Honeydew;
                            cmb_carrera_doctorado_persona_legisladora_PL.Enabled = true; cmb_carrera_doctorado_persona_legisladora_PL.BackColor = Color.Honeydew;
                            dgv_nivel_escolaridad_PL.BackgroundColor = Color.Honeydew;

                            btnAgregarNivelEscPL.Enabled = true; btnEliminarNivelEscPL.Enabled = true;

                            cmb_carrera_licenciatura_persona_legisladora_PL.Focus();
                        }
                        else
                        {
                            cmb_carrera_licenciatura_persona_legisladora_PL.Enabled = false; cmb_carrera_licenciatura_persona_legisladora_PL.BackColor = Color.LightGray;
                            cmb_carrera_maestria_persona_legisladora_PL.Enabled = false; cmb_carrera_maestria_persona_legisladora_PL.BackColor = Color.LightGray;
                            cmb_carrera_doctorado_persona_legisladora_PL.Enabled = false; cmb_carrera_doctorado_persona_legisladora_PL.BackColor = Color.LightGray;
                            dgv_nivel_escolaridad_PL.BackgroundColor = Color.LightGray;
                            dgv_nivel_escolaridad_PL.Rows.Clear();

                            cmb_carrera_licenciatura_persona_legisladora_PL.Text = ""; cmb_carrera_maestria_persona_legisladora_PL.Text = "";
                            cmb_carrera_doctorado_persona_legisladora_PL.Text = "";

                            btnAgregarNivelEscPL.Enabled = false; btnEliminarNivelEscPL.Enabled = false;
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
            else
            {
            }


        }
        private void cmb_escolaridad_persona_legisladora_PL_Validating(object sender, CancelEventArgs e)
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

        private void btnAgregarNivelEscPL_Click(object sender, EventArgs e)
        {
            // se obtienen los valores
            string lic_pl = cmb_carrera_licenciatura_persona_legisladora_PL.Text.Trim();
            string mae_pl = cmb_carrera_maestria_persona_legisladora_PL.Text.Trim();
            string doc_pl = cmb_carrera_doctorado_persona_legisladora_PL.Text.Trim();

            // Agregar una nueva fila al DataGridView
            dgv_nivel_escolaridad_PL.Rows.Add(lic_pl, mae_pl, doc_pl);

            cmb_carrera_licenciatura_persona_legisladora_PL.Text = "";
            //cmb_carrera_licenciatura_persona_legisladora_PL.Enabled = false; cmb_carrera_licenciatura_persona_legisladora_PL.BackColor = Color.LightGray;

            cmb_carrera_maestria_persona_legisladora_PL.Text = "";
            //cmb_carrera_maestria_persona_legisladora_PL.Enabled = false; cmb_carrera_maestria_persona_legisladora_PL.BackColor = Color.LightGray;

            cmb_carrera_doctorado_persona_legisladora_PL.Text = "";
            //cmb_carrera_doctorado_persona_legisladora_PL.Enabled = false; cmb_carrera_doctorado_persona_legisladora_PL.BackColor = Color.LightGray;
        }
        private void btnEliminarNivelEscPL_Click(object sender, EventArgs e)
        {
            if (dgv_nivel_escolaridad_PL.SelectedRows.Count > 0)
            {
                dgv_nivel_escolaridad_PL.Rows.RemoveAt(dgv_nivel_escolaridad_PL.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }

        // cond_lengua_ind_persona_legisladora_PL
        private void cmb_cond_lengua_ind_persona_legisladora_PL_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_lengua_ind_persona_legisladora_PL.Text.ToString();

            if (valorComboBox1 == "Si")
            {
                cmb_lengua_ind_persona_legisladora.Enabled = true; cmb_lengua_ind_persona_legisladora.BackColor = Color.Honeydew;
                btnAgregarLenguaPA.Enabled = true; btnEliminarLenguaPA.Enabled = true;
                dgv_lengua_PA.BackgroundColor = Color.Honeydew;
                cmb_lengua_ind_persona_legisladora.Focus();
            }
            else
            {
                cmb_lengua_ind_persona_legisladora.Enabled = false; cmb_lengua_ind_persona_legisladora.BackColor = Color.LightGray;
                dgv_lengua_PA.Rows.Clear(); dgv_lengua_PA.BackgroundColor = Color.LightGray;
                btnAgregarLenguaPA.Enabled = false; btnEliminarLenguaPA.Enabled = false;

                cmb_lengua_ind_persona_legisladora.Text = "";
            }
        }
        private void cmb_cond_lengua_ind_persona_legisladora_PL_Validating(object sender, CancelEventArgs e)
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


        private void btnAgregarLenguaPL_Click(object sender, EventArgs e)
        {
            // se obtienen los valores
            string lengua_pl = cmb_lengua_ind_persona_legisladora.Text.Trim();


            if (string.IsNullOrWhiteSpace(cmb_lengua_ind_persona_legisladora.Text))
            {
                MessageBox.Show("Revisar datos vacios");
            }
            else
            {

                // Agregar una nueva fila al DataGridView
                dgv_lengua_PA.Rows.Add(lengua_pl);

                cmb_lengua_ind_persona_legisladora.Text = "";

            }
        }
        private void btnEliminarLenguaPL_Click(object sender, EventArgs e)
        {
            if (dgv_lengua_PA.SelectedRows.Count > 0)
            {
                dgv_lengua_PA.Rows.RemoveAt(dgv_lengua_PA.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }
        private void cmb_cond_pueblo_ind_persona_legisladora_PL_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_pueblo_ind_persona_legisladora_PL.Text.ToString();

            if (valorComboBox1 == "Si")
            {
                cmb_pueblo_ind_persona_legisladora_PL.Enabled = true; cmb_pueblo_ind_persona_legisladora_PL.BackColor = Color.Honeydew;
                cmb_pueblo_ind_persona_legisladora_PL.Focus();
            }
            else
            {
                cmb_pueblo_ind_persona_legisladora_PL.Enabled = false; cmb_pueblo_ind_persona_legisladora_PL.BackColor = Color.LightGray;
                cmb_pueblo_ind_persona_legisladora_PL.Text = "";
            }
        }
        private void cmb_cond_discapacidad_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_discapacidad_persona_legisladora.Text.ToString();

            if (valorComboBox1 == "Si")
            {
                cmb_tipo_discapacidad_persona_legisladora.Enabled = true; cmb_tipo_discapacidad_persona_legisladora.BackColor = Color.Honeydew;
                btnAgregarDiscapacidadPA.Enabled = true; btnEliminarDiscapacidadPA.Enabled = true;
                dgv_tipo_discapacidad_PA.BackgroundColor = Color.Honeydew;
                cmb_tipo_discapacidad_persona_legisladora.Focus();
            }
            else
            {
                cmb_tipo_discapacidad_persona_legisladora.Enabled = false; cmb_tipo_discapacidad_persona_legisladora.BackColor = Color.LightGray;
                dgv_tipo_discapacidad_PA.Rows.Clear(); dgv_tipo_discapacidad_PA.BackgroundColor = Color.LightGray;
                btnAgregarDiscapacidadPA.Enabled = false; btnEliminarDiscapacidadPA.Enabled = false;
                cmb_tipo_discapacidad_persona_legisladora.Text = "";
            }
        }
        private void btnAgregarDiscapacidadPL_Click(object sender, EventArgs e)
        {
            // se obtienen los valores
            string tipo_discapacidad_pl = cmb_tipo_discapacidad_persona_legisladora.Text.Trim();


            if (string.IsNullOrWhiteSpace(cmb_tipo_discapacidad_persona_legisladora.Text))
            {
                MessageBox.Show("Revisar datos vacios");
            }
            else
            {

                // Agregar una nueva fila al DataGridView
                dgv_tipo_discapacidad_PA.Rows.Add(tipo_discapacidad_pl);

                cmb_tipo_discapacidad_persona_legisladora.Text = "";

            }
        }
        private void btnEliminarDiscapacidadPL_Click(object sender, EventArgs e)
        {
            if (dgv_tipo_discapacidad_PA.SelectedRows.Count > 0)
            {
                dgv_tipo_discapacidad_PA.Rows.RemoveAt(dgv_tipo_discapacidad_PA.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }
        private void cmb_sexo_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_sexo_persona_legisladora.Text.ToString();

            if (valorComboBox1 == "No binario")
            {
                cmb_cond_pob_diversidad_sexual_persona_legisladora.Enabled = true; cmb_cond_pob_diversidad_sexual_persona_legisladora.BackColor = Color.Honeydew;
                cmb_cond_pob_diversidad_sexual_persona_legisladora.Focus();
            }
            else
            {
                cmb_cond_pob_diversidad_sexual_persona_legisladora.Enabled = false; cmb_cond_pob_diversidad_sexual_persona_legisladora.BackColor = Color.LightGray;
                cmb_cond_pob_diversidad_sexual_persona_legisladora.Text = "";
            }

            // CONSTRUCCION ID
            string primerNombre = txt_nombre_1_persona_legisladora.Text;
            string segundoNombre = txt_nombre_2_persona_legisladora.Text;
            string tercerNombre = txt_nombre_3_persona_legisladora.Text;
            string primerApellido = txt_apellido_1_persona_legisladora.Text;
            string segundoApellido = txt_apellido_2_persona_legisladora.Text;
            string tercerApellido = txt_apellido_3_persona_legisladora.Text;
            string sexo1 = cmb_sexo_persona_legisladora.Text;
            DateTime fechaNacimiento = dtp_fecha_nacimiento_persona_legisladora.Value;


            string uniqueID = GenerateUniqueID(primerNombre, segundoNombre, tercerNombre,
                primerApellido, segundoApellido, tercerApellido,
                sexo1, fechaNacimiento);
            txt_ID_persona_legisladora.Text = uniqueID;

        }

        // dtp_fecha_nacimiento_persona_legisladora
        private void dtp_fecha_nacimiento_persona_legisladora_ValueChanged(object sender, EventArgs e)
        {
            // CONSTRUCCION ID
            string primerNombre = txt_nombre_1_persona_legisladora.Text;
            string segundoNombre = txt_nombre_2_persona_legisladora.Text;
            string tercerNombre = txt_nombre_3_persona_legisladora.Text;
            string primerApellido = txt_apellido_1_persona_legisladora.Text;
            string segundoApellido = txt_apellido_2_persona_legisladora.Text;
            string tercerApellido = txt_apellido_3_persona_legisladora.Text;
            string sexo1 = cmb_sexo_persona_legisladora.Text;
            DateTime fechaNacimiento = dtp_fecha_nacimiento_persona_legisladora.Value;


            string uniqueID = GenerateUniqueID(primerNombre, segundoNombre, tercerNombre,
                primerApellido, segundoApellido, tercerApellido,
                sexo1, fechaNacimiento);
            txt_ID_persona_legisladora.Text = uniqueID;

        }
        private void dtp_fecha_nacimiento_persona_legisladora_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cancelar cualquier entrada manual
            e.Handled = true;
        }


        private void cmb_cond_pob_diversidad_sexual_persona_legisladora_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmb_cond_pob_diversidad_sexual_persona_legisladora.Text))
            {
                MessageBox.Show("Debe especificar la condición de la persona legisladora de formar parte de algún grupo de la diversidad sexual.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_cond_pob_diversidad_sexual_persona_legisladora.Focus();
            }
        }
        private void cmb_forma_eleccion_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_forma_eleccion_persona_legisladora.Text.ToString();

            if (valorComboBox1 == "Mayoría relativa")
            {
                cmb_distrito_electoral_mayoria_relativa.Enabled = true; cmb_distrito_electoral_mayoria_relativa.BackColor = Color.Honeydew;
                cmb_distrito_electoral_mayoria_relativa.Focus();

                cmb_tipo_candidatura_persona_legisladora.Enabled = true; cmb_tipo_candidatura_persona_legisladora.BackColor = Color.Honeydew;

                string cadena = "Data Source = DB_PLE.db;Version=3;";
                int distritos_uni;

                distritos_uni = int.Parse(Txt_distritos_uninominales.Text);

                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_NUM_LEGISLATURA WHERE id_numero_legislatura BETWEEN 1 AND @distritos_uni";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Agregar parámetro al comando
                        cmd.Parameters.AddWithValue("@distritos_uni", distritos_uni);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        cmb_distrito_electoral_mayoria_relativa.DataSource = dataTable;
                        cmb_distrito_electoral_mayoria_relativa.DisplayMember = "descripcion";

                        cmb_distrito_electoral_mayoria_relativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_distrito_electoral_mayoria_relativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_distrito_electoral_mayoria_relativa.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_distrito_electoral_mayoria_relativa.SelectedIndex = -1; // Aquí se establece como vacío
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al llenar el ComboBox cmb_forma_eleccion_persona_legisladora_SelectedIndexChanged: " + ex.Message);
                    }
                    finally
                    {
                        conexion.Close();
                    }

                }

            }
            else
            {
                cmb_distrito_electoral_mayoria_relativa.Enabled = false; cmb_distrito_electoral_mayoria_relativa.BackColor = Color.LightGray;
                cmb_tipo_candidatura_persona_legisladora.Enabled = false; cmb_tipo_candidatura_persona_legisladora.BackColor = Color.LightGray;

                cmb_tipo_candidatura_persona_legisladora.Text = "";
                cmb_distrito_electoral_mayoria_relativa.Text = "";
            }
        }
        private void cmb_tipo_candidatura_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_tipo_candidatura_persona_legisladora.Text.ToString();

            if (valorComboBox1 == "Candidatura por partido único")
            {
                cmb_partido_politico_candidatura_coalicion.Enabled = false; cmb_partido_politico_candidatura_coalicion.BackColor = Color.LightGray;
                cmb_partido_politico_candidatura_partido_unico.Enabled = true; cmb_partido_politico_candidatura_partido_unico.BackColor = Color.Honeydew;

                btnAgregarCandidaturaPL.Enabled = false; btnEliminarCandidaturaPL.Enabled = false;
                dgv_partido_coalicion.Rows.Clear(); dgv_partido_coalicion.BackgroundColor = Color.LightGray;

                cmb_partido_politico_candidatura_coalicion.Text = "";
                cmb_partido_politico_candidatura_partido_unico.Focus();



                string cadena = "Data Source = DB_PLE.db;Version=3;";

                // SE AGREGAN LOS PARTIDOS POLITICOS---------------------------------------------------
                string ent_dg;
                ent_dg = cmb_entidad_federativa.Text;

                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_PARTIDOS_POLITICOS WHERE entidad =  @ent_dg";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Agregar parámetro al comando
                        cmd.Parameters.AddWithValue("@ent_dg", ent_dg);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        cmb_partido_politico_candidatura_partido_unico.DataSource = dataTable;
                        cmb_partido_politico_candidatura_partido_unico.DisplayMember = "descripcion";

                        cmb_partido_politico_candidatura_partido_unico.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_partido_politico_candidatura_partido_unico.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_partido_politico_candidatura_partido_unico.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_partido_politico_candidatura_partido_unico.SelectedIndex = -1; // Aquí se establece como vacío
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al llenar el ComboBox cmb_tipo_candidatura_persona_legisladora_SelectedIndexChanged: " + ex.Message);
                    }
                    finally
                    {
                        conexion.Close();
                    }

                }

                // SE AGREGAN TIPO DE ADSCRIPCION---------------------------------------------------

                //inicial
                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_TIPO_ADSCRIPCION WHERE id_tip_adscripcion in  (1,3,9)";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Agregar parámetro al comando
                        //cmd.Parameters.AddWithValue("@ent_dg", ent_dg);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // inicial
                        cmb_tipo_adscripcion_inicial_persona_legisladora.DataSource = dataTable;
                        cmb_tipo_adscripcion_inicial_persona_legisladora.DisplayMember = "descripcion";

                        cmb_tipo_adscripcion_inicial_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_tipo_adscripcion_inicial_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_tipo_adscripcion_inicial_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_tipo_adscripcion_inicial_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_TIPO_ADSCRIPCION WHERE id_tip_adscripcion in  (1,3,9)";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Agregar parámetro al comando
                        //cmd.Parameters.AddWithValue("@ent_dg", ent_dg);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);


                        // final
                        cmb_tipo_adscripcion_final_persona_legisladora.DataSource = dataTable;
                        cmb_tipo_adscripcion_final_persona_legisladora.DisplayMember = "descripcion";

                        cmb_tipo_adscripcion_final_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_tipo_adscripcion_final_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_tipo_adscripcion_final_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_tipo_adscripcion_final_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
            else if (valorComboBox1 == "Candidatura por coalición")
            {
                cmb_partido_politico_candidatura_coalicion.Enabled = true; cmb_partido_politico_candidatura_coalicion.BackColor = Color.Honeydew;
                cmb_partido_politico_candidatura_partido_unico.Enabled = false; cmb_partido_politico_candidatura_partido_unico.BackColor = Color.LightGray;
                cmb_partido_politico_candidatura_partido_unico.Text = "";
                dgv_partido_coalicion.BackgroundColor = Color.Honeydew;
                btnAgregarCandidaturaPL.Enabled = true; btnEliminarCandidaturaPL.Enabled = true;
                cmb_partido_politico_candidatura_coalicion.Focus();

                //cmb_tipo_candidatura_persona_legisladora.Enabled = true; cmb_tipo_candidatura_persona_legisladora.BackColor = Color.Honeydew;

                string cadena = "Data Source = DB_PLE.db;Version=3;";

                // SE AGREGAN LOS PARTIDOS POLITICOS---------------------------------------------------
                string ent_dg;
                ent_dg = cmb_entidad_federativa.Text;

                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_PARTIDOS_POLITICOS WHERE entidad =  @ent_dg";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Agregar parámetro al comando
                        cmd.Parameters.AddWithValue("@ent_dg", ent_dg);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);


                        cmb_partido_politico_candidatura_coalicion.DataSource = dataTable;
                        cmb_partido_politico_candidatura_coalicion.DisplayMember = "descripcion";

                        cmb_partido_politico_candidatura_coalicion.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_partido_politico_candidatura_coalicion.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_partido_politico_candidatura_coalicion.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_partido_politico_candidatura_coalicion.SelectedIndex = -1; // Aquí se establece como vacío
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
                // SE AGREGAN TIPO DE ADSCRIPCION---------------------------------------------------

                //inicial
                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_TIPO_ADSCRIPCION WHERE id_tip_adscripcion in  (1,3,9)";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Agregar parámetro al comando
                        //cmd.Parameters.AddWithValue("@ent_dg", ent_dg);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // inicial
                        cmb_tipo_adscripcion_inicial_persona_legisladora.DataSource = dataTable;
                        cmb_tipo_adscripcion_inicial_persona_legisladora.DisplayMember = "descripcion";

                        cmb_tipo_adscripcion_inicial_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_tipo_adscripcion_inicial_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_tipo_adscripcion_inicial_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_tipo_adscripcion_inicial_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío

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

                //final
                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_TIPO_ADSCRIPCION WHERE id_tip_adscripcion in  (1,3,9)";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Agregar parámetro al comando
                        //cmd.Parameters.AddWithValue("@ent_dg", ent_dg);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // final
                        cmb_tipo_adscripcion_final_persona_legisladora.DataSource = dataTable;
                        cmb_tipo_adscripcion_final_persona_legisladora.DisplayMember = "descripcion";

                        cmb_tipo_adscripcion_final_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_tipo_adscripcion_final_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_tipo_adscripcion_final_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_tipo_adscripcion_final_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
                // SE AGREGAN TIPO DE ADSCRIPCION---------------------------------------------------
                string cadena = "Data Source = DB_PLE.db;Version=3;";

                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_TIPO_ADSCRIPCION ";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Agregar parámetro al comando
                        //cmd.Parameters.AddWithValue("@ent_dg", ent_dg);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        cmb_tipo_adscripcion_inicial_persona_legisladora.DataSource = dataTable;
                        cmb_tipo_adscripcion_inicial_persona_legisladora.DisplayMember = "descripcion";

                        cmb_tipo_adscripcion_inicial_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_tipo_adscripcion_inicial_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_tipo_adscripcion_inicial_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_tipo_adscripcion_inicial_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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

                cmb_partido_politico_candidatura_partido_unico.Enabled = false; cmb_partido_politico_candidatura_partido_unico.BackColor = Color.LightGray;
                cmb_partido_politico_candidatura_coalicion.Enabled = false; cmb_partido_politico_candidatura_coalicion.BackColor = Color.LightGray;
                btnAgregarCandidaturaPL.Enabled = false; btnEliminarCandidaturaPL.Enabled = false;
                dgv_partido_coalicion.Rows.Clear(); dgv_partido_coalicion.BackgroundColor = Color.LightGray;
                cmb_partido_politico_candidatura_partido_unico.Text = "";
                cmb_partido_politico_candidatura_coalicion.Text = "";

            }
        }
        private void btnAgregarCandidaturaPL_Click(object sender, EventArgs e)
        {
            // se obtienen los valores
            string partido_coalicion_pl = cmb_partido_politico_candidatura_coalicion.Text.Trim();


            if (string.IsNullOrWhiteSpace(cmb_partido_politico_candidatura_coalicion.Text))
            {
                MessageBox.Show("Revisar datos vacios");
            }
            else
            {

                // Agregar una nueva fila al DataGridView
                dgv_partido_coalicion.Rows.Add(partido_coalicion_pl);

                cmb_partido_politico_candidatura_coalicion.Text = "";

            }
        }
        private void btnEliminarCandidaturaPL_Click(object sender, EventArgs e)
        {
            if (dgv_partido_coalicion.SelectedRows.Count > 0)
            {
                dgv_partido_coalicion.Rows.RemoveAt(dgv_partido_coalicion.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }
        private void cmb_tipo_adscripcion_inicial_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_tipo_adscripcion_inicial_persona_legisladora.Text.ToString();

            if (valorComboBox1 == "Grupo parlamentario")
            {
                cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.Enabled = true; cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.BackColor = Color.Honeydew;
                //cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.Focus();


                string cadena = "Data Source = DB_PLE.db;Version=3;";

                // SE AGREGAN LOS PARTIDOS POLITICOS---------------------------------------------------
                string ent_dg;
                ent_dg = cmb_entidad_federativa.Text;

                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_GRUPO_PARLAMENTARIO WHERE entidad =  @ent_dg";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Agregar parámetro al comando
                        cmd.Parameters.AddWithValue("@ent_dg", ent_dg);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.DataSource = dataTable;
                        cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.DisplayMember = "descripcion";

                        cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
                txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.Enabled = false; txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.BackColor = Color.LightGray;
                cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.Enabled = true; cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.BackColor = Color.LightGray;
                cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.Text = "";
                txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.Text = "";

            }
        }
        private void txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.Text = txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.SelectionStart = txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.Text.Length;
        }

        // partido_politico_candidatura_coalicion
        private void cmb_partido_politico_candidatura_coalicion_Validating(object sender, CancelEventArgs e)
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

        // partido_politico_candidatura_partido_unico
        private void cmb_partido_politico_candidatura_partido_unico_Validating(object sender, CancelEventArgs e)
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

        // grupo_parlamentario_adscipcion_inicial_persona_legisladora
        private void cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.Text.ToString();

            if (valorComboBox1 == "Otro grupo parlamentario (especifique)")
            {
                txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.Enabled = true; txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.BackColor = Color.Honeydew;
                txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.Focus();

            }
            else
            {
                txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.Enabled = false; txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.BackColor = Color.LightGray;
                txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.Text = "";

            }
        }
        private void cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora_Validating(object sender, CancelEventArgs e)
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

        private void txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.Text))
            {
                MessageBox.Show("Debe especificar el otro grupo parlamentario de adscripción inicial de la persona legisladora.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.Focus();
            }
        }
        private void cmb_tipo_adscripcion_final_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_tipo_adscripcion_final_persona_legisladora.Text.ToString();

            if (valorComboBox1 == "Grupo parlamentario")
            {
                cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.Enabled = true; cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.BackColor = Color.Honeydew;
                //cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.Focus();


                string cadena = "Data Source = DB_PLE.db;Version=3;";

                // SE AGREGAN LOS PARTIDOS POLITICOS---------------------------------------------------
                string ent_dg;
                ent_dg = cmb_entidad_federativa.Text;

                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_GRUPO_PARLAMENTARIO WHERE entidad =  @ent_dg";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Agregar parámetro al comando
                        cmd.Parameters.AddWithValue("@ent_dg", ent_dg);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.DataSource = dataTable;
                        cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.DisplayMember = "descripcion";

                        cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
                txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.Enabled = false; txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.BackColor = Color.LightGray;
                cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.Enabled = true; cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.BackColor = Color.LightGray;
                cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.Text = "";
                txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.Text = "";
            }
        }
        private void otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.Text = txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.SelectionStart = txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.Text.Length;
        }

        // grupo_parlamentario_adscipcion_final_persona_legisladora
        private void cmb_grupo_parlamentario_adscipcion_final_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.Text.ToString();

            if (valorComboBox1 == "Otro grupo parlamentario (especifique)")
            {
                txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.Enabled = true; txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.BackColor = Color.Honeydew;
                txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.Focus();

            }
            else
            {
                txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.Enabled = false; txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.BackColor = Color.LightGray;
                txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.Text = "";

            }
        }
        private void cmb_grupo_parlamentario_adscipcion_final_persona_legisladora_Validating(object sender, CancelEventArgs e)
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

        private void txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.Text))
            {
                MessageBox.Show("Debe especificar el otro grupo parlamentario de adscripción inicial de la persona legisladora.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.Focus();
            }
        }
        private void cmb_empleo_anterior_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_empleo_anterior_persona_legisladora.Text.ToString();

            if (valorComboBox1 == "Legislador federal" || valorComboBox1 == "Legislador estatal (reelección)" || valorComboBox1 == "Legislador estatal (de otra entidad federativa)"
                || valorComboBox1 == "Gobierno federal" || valorComboBox1 == "Gobierno estatal" || valorComboBox1 == "Gobierno municipal" || valorComboBox1 == "Sindico(a)/ regidor(a)")
            {

                string cadena = "Data Source = DB_PLE.db;Version=3;";

                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_ANTIGUEDAD WHERE id_antiguedad between 2 and 101 ";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        cmb_antigüedad_servicio_publico_persona_legisladora.DataSource = dataTable;
                        cmb_antigüedad_servicio_publico_persona_legisladora.DisplayMember = "descripcion";

                        cmb_antigüedad_servicio_publico_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_antigüedad_servicio_publico_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_antigüedad_servicio_publico_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_antigüedad_servicio_publico_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al llenar el ComboBox cmb_empleo_anterior_persona_legisladora_SelectedIndexChanged: " + ex.Message);
                    }
                    finally
                    {
                        conexion.Close();
                    }

                }

            }
            else
            {
                cmb_Antigüedad_servicio_publico_persona_legisladora();
            }

            if (valorComboBox1 == "Legislador federal" || valorComboBox1 == "Legislador estatal (reelección)" || valorComboBox1 == "Legislador estatal (de otra entidad federativa)")
            {
                string cadena = "Data Source = DB_PLE.db;Version=3;";

                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_ANTIGUEDAD WHERE id_antiguedad between 2 and 101 ";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        cmb_antigüedad_persona_legisladora.DataSource = dataTable;
                        cmb_antigüedad_persona_legisladora.DisplayMember = "descripcion";

                        cmb_antigüedad_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_antigüedad_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_antigüedad_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_antigüedad_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
                cmb_Antigüedad_persona_legisladora();
            }
        }
        private void cmb_antigüedad_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {

            string ant_pl;
            ant_pl = cmb_antigüedad_persona_legisladora.Text;

            if (ant_pl != "No identificado ")
            {
                // se extrae antiguedad persona legisladora
                int ant_pl2;
                int.TryParse(ant_pl, out ant_pl2); // antiguedad como persona legisladora

                if (ant_pl2 != 0)
                {
                    // se extrae la antiguedad como servidor publico
                    string ant_sp_pl; int ant_sp_pl2;
                    ant_sp_pl = cmb_antigüedad_servicio_publico_persona_legisladora.Text;
                    int.TryParse(ant_sp_pl, out ant_sp_pl2); // antiguedad servidor publico

                    if (ant_sp_pl2 != 0)
                    {
                        if (ant_pl2 > ant_sp_pl2)
                        {

                            MessageBox.Show("Debe ser igual o menor a la cantidad reportada en el campo antigüedad en el servicio público.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cmb_antigüedad_persona_legisladora.SelectedIndex = -1; cmb_antigüedad_persona_legisladora.Focus();
                        }
                    }
                }
            }
            /*
            else
            {
                



                    
                    else
                    {

                    }
                
                

            }
            */
        }
        private void txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.Text = txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.SelectionStart = txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.Text.Length;
        }
        private void cmb_cond_presentacion_declaracion_situacion_patrimonial_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_presentacion_declaracion_situacion_patrimonial.Text.ToString();

            if (valorComboBox1 == "No aplica (especifique)")
            {
                txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.Enabled = true; txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.BackColor = Color.Honeydew;
                txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.Focus();

            }
            else
            {
                txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.Enabled = false; txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.BackColor = Color.LightGray;
                txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.Text = "";

            }
        }
        private void txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.Text))
            {
                MessageBox.Show("Debe especificar el motivo por el cual no le es aplicable a la persona legisladora la presentación de la declaración de situación patrimonial.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.Focus();
            }
        }
        private void cmb_cond_presentacion_declaracion_intereses_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_presentacion_declaracion_intereses.Text.ToString();

            if (valorComboBox1 == "No aplica (especifique)")
            {
                txt_no_aplica_presentacion_declaracion_intereses_especifique.Enabled = true; txt_no_aplica_presentacion_declaracion_intereses_especifique.BackColor = Color.Honeydew;
                txt_no_aplica_presentacion_declaracion_intereses_especifique.Focus();

            }
            else
            {
                txt_no_aplica_presentacion_declaracion_intereses_especifique.Enabled = false; txt_no_aplica_presentacion_declaracion_intereses_especifique.BackColor = Color.LightGray;
                txt_no_aplica_presentacion_declaracion_intereses_especifique.Text = "";

            }
        }
        private void txt_no_aplica_presentacion_declaracion_intereses_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_no_aplica_presentacion_declaracion_intereses_especifique.Text = txt_no_aplica_presentacion_declaracion_intereses_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_no_aplica_presentacion_declaracion_intereses_especifique.SelectionStart = txt_no_aplica_presentacion_declaracion_intereses_especifique.Text.Length;

        }
        private void txt_no_aplica_presentacion_declaracion_intereses_especifique_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_no_aplica_presentacion_declaracion_intereses_especifique.Text))
            {
                MessageBox.Show("Debe especificar el motivo por el cual no le es aplicable a la persona legisladora la presentación de la declaración de intereses.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_no_aplica_presentacion_declaracion_intereses_especifique.Focus();
            }
        }
        private void cmb_cond_presentacion_declaracion_fiscal_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_presentacion_declaracion_fiscal.Text.ToString();

            if (valorComboBox1 == "No aplica (especifique)")
            {
                txt_no_aplica_presentacion_declaracion_fiscal_especifique.Enabled = true; txt_no_aplica_presentacion_declaracion_fiscal_especifique.BackColor = Color.Honeydew;
                txt_no_aplica_presentacion_declaracion_fiscal_especifique.Focus();

            }
            else
            {
                txt_no_aplica_presentacion_declaracion_fiscal_especifique.Enabled = false; txt_no_aplica_presentacion_declaracion_fiscal_especifique.BackColor = Color.LightGray;
                txt_no_aplica_presentacion_declaracion_fiscal_especifique.Text = "";

            }
        }
        private void txt_no_aplica_presentacion_declaracion_fiscal_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_no_aplica_presentacion_declaracion_fiscal_especifique.Text = txt_no_aplica_presentacion_declaracion_fiscal_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_no_aplica_presentacion_declaracion_fiscal_especifique.SelectionStart = txt_no_aplica_presentacion_declaracion_fiscal_especifique.Text.Length;
        }
        private void txt_no_aplica_presentacion_declaracion_fiscal_especifique_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_no_aplica_presentacion_declaracion_fiscal_especifique.Text))
            {
                MessageBox.Show("Debe especificar el motivo por el cual no le es aplicable a la persona legisladora la presentación de la declaración fiscal.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_no_aplica_presentacion_declaracion_fiscal_especifique.Focus();
            }
        }
        private void txt_remuneracion_persona_legisladora_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }
        private void txt_asistencia_legislativa_persona_legisladora_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }
        private void txt_gestion_parlamentaria_persona_legisladora_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }
        private void txt_atencion_ciudadana_persona_legisladora_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }
        private void txt_otro_concepto_gasto_persona_legisladora_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }
        private void InitializeMap()
        {
            gMapControl.MapProvider = GMapProviders.GoogleMap;
            gMapControl.Position = new PointLatLng(19.42847, -99.12766); // Centrar el mapa en el ecuador por defecto
            gMapControl.MinZoom = 0;
            gMapControl.MaxZoom = 18;
            gMapControl.Zoom = 6;

            gMapControl.MouseClick += new MouseEventHandler(gMapControl_MouseClick);
            gMapControl.MouseWheel += new MouseEventHandler(gMapControl_MouseWheel); // Añadir manejador de evento MouseWheel

            gMapControl.DragButton = MouseButtons.Left; // Permitir arrastrar con el botón izquierdo del ratón
            gMapControl.CanDragMap = true; // Habilitar el arrastre del mapa
            gMapControl.ShowCenter = false; // Ocultar el cursor central por defecto
            gMapControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter; // Configurar el zoom con la rueda del ratón
            gMapControl.IgnoreMarkerOnMouseWheel = true; // Ignorar los marcadores al hacer zoom con la rueda del ratón


            markersOverlay = new GMapOverlay("markers");
            gMapControl.Overlays.Add(markersOverlay);
        }
        private void gMapControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (gMapControl.Bounds.Contains(PointToClient(Cursor.Position)))
            {
                // Maneja el evento de zoom en el mapa
                if (e.Delta > 0)
                {
                    if (gMapControl.Zoom < gMapControl.MaxZoom)
                    {
                        gMapControl.Zoom++;
                    }
                }
                else if (e.Delta < 0)
                {
                    if (gMapControl.Zoom > gMapControl.MinZoom)
                    {
                        gMapControl.Zoom--;
                    }
                }
        // Marca el evento como manejado para que no se propague
        ((HandledMouseEventArgs)e).Handled = true;
            }
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (gMapControl.Bounds.Contains(PointToClient(Cursor.Position)))
            {
                ((HandledMouseEventArgs)e).Handled = true;
            }
            else
            {
                base.OnMouseWheel(e);
            }
        }
        private void gMapControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PointLatLng point = gMapControl.FromLocalToLatLng(e.X, e.Y);
                txt_latitud_casa_atencion_ciudadana.Text = point.Lat.ToString();
                txt_longitud_casa_atencion_ciudadana.Text = point.Lng.ToString();

                markersOverlay.Markers.Clear();
                GMarkerGoogle marker = new GMarkerGoogle(point, GMarkerGoogleType.red_dot);
                markersOverlay.Markers.Add(marker);
            }
        }
        private void cmb_cond_casa_atencion_ciudadana_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_casa_atencion_ciudadana.Text.ToString();

            if (valorComboBox1 == "Si")
            {
                cmb_cond_casa_atencion_ciudadana_movil.Enabled = true; cmb_cond_casa_atencion_ciudadana_movil.BackColor = Color.Honeydew;
                cmb_cond_casa_atencion_ciudadana_movil.Focus();

            }
            else
            {
                cmb_cond_casa_atencion_ciudadana_movil.Enabled = false; cmb_cond_casa_atencion_ciudadana_movil.BackColor = Color.LightGray;
                txt_latitud_casa_atencion_ciudadana.Enabled = false; txt_latitud_casa_atencion_ciudadana.BackColor = Color.LightGray;
                txt_longitud_casa_atencion_ciudadana.Enabled = false; txt_longitud_casa_atencion_ciudadana.BackColor = Color.LightGray;
                txt_otro_cargo_comision_permanente_especifique.Enabled = false; txt_otro_cargo_comision_permanente_especifique.BackColor = Color.LightGray;
                gMapControl.Enabled = false;
                cmb_cond_casa_atencion_ciudadana_movil.Text = "";
                txt_latitud_casa_atencion_ciudadana.Text = "";
                txt_longitud_casa_atencion_ciudadana.Text = "";
                txt_otro_cargo_comision_permanente_especifique.Text = "";
            }
        }
        private void cmb_cond_integrante_comision_permanente_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_integrante_comision_permanente.Text.ToString();

            if (valorComboBox1 == "Si")
            {
                cmb_cargo_comision_permanente.Enabled = true; cmb_cargo_comision_permanente.BackColor = Color.Honeydew;
                txt_asist_sesiones_comision_permanente_persona_legisladora.Enabled = true; txt_asist_sesiones_comision_permanente_persona_legisladora.BackColor = Color.Honeydew;
                cmb_cargo_comision_permanente.Focus();

            }
            else
            {
                cmb_cargo_comision_permanente.Enabled = false; cmb_cargo_comision_permanente.BackColor = Color.LightGray;
                txt_asist_sesiones_comision_permanente_persona_legisladora.Enabled = false; txt_asist_sesiones_comision_permanente_persona_legisladora.BackColor = Color.LightGray;
                cmb_cargo_comision_permanente.Focus();
                cmb_cargo_comision_permanente.Text = "";
            }
        }
        private void cmb_cargo_comision_permanente_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cargo_comision_permanente.Text.ToString();

            if (valorComboBox1 == "Otro cargo (especifique)")
            {
                txt_otro_cargo_comision_permanente_especifique.Enabled = true; txt_otro_cargo_comision_permanente_especifique.BackColor = Color.Honeydew;
                txt_otro_cargo_comision_permanente_especifique.Focus();

            }
            else
            {
                txt_otro_cargo_comision_permanente_especifique.Enabled = false; txt_otro_cargo_comision_permanente_especifique.BackColor = Color.LightGray;
                txt_otro_cargo_comision_permanente_especifique.Text = "";
            }

        }
        private void txt_otro_cargo_comision_permanente_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_otro_cargo_comision_permanente_especifique.Text = txt_otro_cargo_comision_permanente_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_otro_cargo_comision_permanente_especifique.SelectionStart = txt_otro_cargo_comision_permanente_especifique.Text.Length;
        }
        private void txt_otro_cargo_comision_permanente_especifique_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_otro_cargo_comision_permanente_especifique.Text))
            {
                MessageBox.Show("Debe especificar el otro cargo desempeñado por la persona legisladora en la Comisión Permanente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_otro_cargo_comision_permanente_especifique.Focus();
            }
        }
        private void cmb_cond_integrante_jucopo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_integrante_jucopo.Text.ToString();

            if (valorComboBox1 == "Si")
            {
                cmb_cargo_jucopo.Enabled = true; cmb_cargo_jucopo.BackColor = Color.Honeydew;

                //txt_latitud_casa_atencion_ciudadana.Enabled = true; txt_latitud_casa_atencion_ciudadana.BackColor = Color.Honeydew;
                //txt_longitud_casa_atencion_ciudadana.Enabled = true; txt_longitud_casa_atencion_ciudadana.BackColor = Color.Honeydew;
                //gMapControl.Enabled = true;
                cmb_cargo_jucopo.Focus();

            }
            else
            {
                cmb_cargo_jucopo.Enabled = false; cmb_cargo_jucopo.BackColor = Color.LightGray;
                txt_otro_cargo_jucopo_especifique.Enabled = false; txt_otro_cargo_jucopo_especifique.BackColor = Color.LightGray;
                cmb_cargo_jucopo.Text = "";
                txt_otro_cargo_jucopo_especifique.Text = "";
            }
        }
        private void cmb_cargo_jucopo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cargo_jucopo.Text.ToString();

            if (valorComboBox1 == "Otro cargo (especifique)")
            {
                txt_otro_cargo_jucopo_especifique.Enabled = true; txt_otro_cargo_jucopo_especifique.BackColor = Color.Honeydew;
                txt_otro_cargo_jucopo_especifique.Focus();

            }
            else
            {
                txt_otro_cargo_jucopo_especifique.Enabled = false; txt_otro_cargo_jucopo_especifique.BackColor = Color.LightGray;
                txt_otro_cargo_jucopo_especifique.Text = "";
            }
        }
        private void txt_otro_cargo_jucopo_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_otro_cargo_jucopo_especifique.Text = txt_otro_cargo_jucopo_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_otro_cargo_jucopo_especifique.SelectionStart = txt_otro_cargo_jucopo_especifique.Text.Length;
        }
        private void txt_otro_cargo_jucopo_especifique_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_otro_cargo_jucopo_especifique.Text))
            {
                MessageBox.Show("Debe especificar el otro cargo desempeñado por la persona legisladora en la Junta de Coordinación Política.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_otro_cargo_jucopo_especifique.Focus();
            }
        }
        private void cmb_cond_integrante_mesa_directiva_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_integrante_mesa_directiva.Text.ToString();

            if (valorComboBox1 == "Si")
            {
                cmb_cargo_mesa_directiva_PL.Enabled = true; cmb_cargo_mesa_directiva_PL.BackColor = Color.Honeydew;
                cmb_cargo_mesa_directiva_PL.Focus();

            }
            else
            {
                cmb_cargo_mesa_directiva_PL.Enabled = false; cmb_cargo_mesa_directiva_PL.BackColor = Color.LightGray;
                txt_otro_cargo_mesa_directiva_especifique.Enabled = false; txt_otro_cargo_mesa_directiva_especifique.BackColor = Color.LightGray;
                cmb_cargo_mesa_directiva_PL.Text = "";
                txt_otro_cargo_mesa_directiva_especifique.Text = "";
            }
        }
        private void cmb_cargo_mesa_directiva_PL_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cargo_mesa_directiva_PL.Text.ToString();

            if (valorComboBox1 == "Otro cargo (especifique)")
            {
                txt_otro_cargo_mesa_directiva_especifique.Enabled = true; txt_otro_cargo_mesa_directiva_especifique.BackColor = Color.Honeydew;
                txt_otro_cargo_mesa_directiva_especifique.Focus();

            }
            else
            {
                txt_otro_cargo_mesa_directiva_especifique.Enabled = false; txt_otro_cargo_mesa_directiva_especifique.BackColor = Color.LightGray;
                txt_otro_cargo_mesa_directiva_especifique.Text = "";
            }
        }
        private void txt_otro_cargo_mesa_directiva_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_otro_cargo_mesa_directiva_especifique.Text = txt_otro_cargo_mesa_directiva_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_otro_cargo_mesa_directiva_especifique.SelectionStart = txt_otro_cargo_mesa_directiva_especifique.Text.Length;
        }
        private void txt_otro_cargo_mesa_directiva_especifique_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_otro_cargo_mesa_directiva_especifique.Text))
            {
                MessageBox.Show("Debe especificar el otro cargo desempeñado por la persona legisladora en la Mesa Directiva.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_otro_cargo_mesa_directiva_especifique.Focus();
            }
        }
        private void cmb_nombre_comision_legislativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_nombre_comision_legislativa.SelectedItem != null)
            {
                string valor_cmb = cmb_nombre_comision_legislativa.Text;
                string cadena = "Data Source = DB_PLE.db;Version=3;";

                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select ID_comision_legislativa from TR_COMISIONES_LEGISLATIVAS where nombre_comision_legislativa = @valor_cmb";

                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Agregar parámetro al comando
                        cmd.Parameters.AddWithValue("@valor_cmb", valor_cmb);
                        txt_ID_comision_legislativa_pc.Text = cmd.ExecuteScalar()?.ToString();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al llenar el ID: " + ex.Message);
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
        private void btnAgregarParticipacionCom_Click(object sender, EventArgs e)
        {
            // se obtienen los valores
            string nom_com = cmb_nombre_comision_legislativa.Text.Trim();
            string id_com = txt_ID_comision_legislativa_pc.Text.Trim();
            string cargo_com = cmb_cargo_comision_legislativa.Text.Trim();


            if (string.IsNullOrWhiteSpace(cmb_nombre_comision_legislativa.Text) || string.IsNullOrWhiteSpace(cmb_cargo_comision_legislativa.Text))
            {
                MessageBox.Show("Revisar datos vacios");
            }
            else
            {

                // Agregar una nueva fila al DataGridView
                dgv_participacion_comisiones.Rows.Add(nom_com, id_com, cargo_com);

                cmb_nombre_comision_legislativa.Text = "";
                txt_ID_comision_legislativa_pc.Text = "";
                cmb_cargo_comision_legislativa.Text = "";
            }
        }
        private void btnEliminarParticipacionCom_Click(object sender, EventArgs e)
        {
            if (dgv_participacion_comisiones.SelectedRows.Count > 0)
            {
                dgv_participacion_comisiones.Rows.RemoveAt(dgv_participacion_comisiones.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }
        private void txt_asist_sesiones_plenarias_persona_legisladora_TextChanged(object sender, EventArgs e)
        {
            string valor_txt = txt_asist_sesiones_plenarias_persona_legisladora.Text;

            string sesiones_per_ord = Txt_sesiones_celebradas_po.Text;
            string sesiones_per_ext = Txt_sesiones_celebradas_pe.Text;

            int.TryParse(sesiones_per_ord, out int v1);
            int.TryParse(sesiones_per_ext, out int v2);
            int.TryParse(valor_txt, out int v3);

            if (!string.IsNullOrEmpty(valor_txt) && int.TryParse(valor_txt, out int valor) && valor > 0)
            {
                txt_cant_intervenciones_sesiones_plenarias_persona_legisladora.Enabled = true;
                txt_cant_intervenciones_sesiones_plenarias_persona_legisladora.BackColor = Color.Honeydew;
            }
            else
            {
                txt_cant_intervenciones_sesiones_plenarias_persona_legisladora.Enabled = false;
                txt_cant_intervenciones_sesiones_plenarias_persona_legisladora.BackColor = Color.LightGray;
            }

            int suma = v1 + v2;
            if (v3 > suma)
            {
                MessageBox.Show("Debe ser igual o menor a la suma de las sesiones registradas en periodo ordinario y extraordinarias.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_asist_sesiones_plenarias_persona_legisladora.Clear();
            }
            else
            {

            }
        }
        private void txt_asist_sesiones_comision_permanente_persona_legisladora_TextChanged(object sender, EventArgs e)
        {
            string valor_txt = txt_asist_sesiones_comision_permanente_persona_legisladora.Text;

            if (!string.IsNullOrEmpty(valor_txt) && int.TryParse(valor_txt, out int valor) && valor > 0)
            {
                txt_cant_interv_sesiones_dip_permanente_persona_legisladora.Enabled = true;
                txt_cant_interv_sesiones_dip_permanente_persona_legisladora.BackColor = Color.Honeydew;
            }
            else
            {
                txt_cant_interv_sesiones_dip_permanente_persona_legisladora.Enabled = false;
                txt_cant_interv_sesiones_dip_permanente_persona_legisladora.BackColor = Color.LightGray;
            }
        }
        private void cmb_cond_casa_atencion_ciudadana_movil_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valor_cmb = cmb_cond_casa_atencion_ciudadana_movil.Text;

            if (valor_cmb == "Si")
            {
                txt_latitud_casa_atencion_ciudadana.Enabled = false; txt_latitud_casa_atencion_ciudadana.BackColor = Color.LightGray;
                txt_longitud_casa_atencion_ciudadana.Enabled = false; txt_longitud_casa_atencion_ciudadana.BackColor = Color.LightGray;
                txt_latitud_casa_atencion_ciudadana.Text = ""; txt_longitud_casa_atencion_ciudadana.Text = "";
                gMapControl.Enabled = false;
            }
            else
            {
                txt_latitud_casa_atencion_ciudadana.Enabled = true; txt_latitud_casa_atencion_ciudadana.BackColor = Color.Honeydew;
                txt_longitud_casa_atencion_ciudadana.Enabled = true; txt_longitud_casa_atencion_ciudadana.BackColor = Color.Honeydew;
                gMapControl.Enabled = true;


            }
        }
        private void btnGuardarPL_Click(object sender, EventArgs e)
        {
            bool cv = ValidarCampos_PL2();
            //bool cv = true;

            if (cv == true)
            {
                DialogResult respuesta = MessageBox.Show("¿Está seguro de Guardar los datos?", "Confirmacion",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    // Agregar una nueva fila al DataGridView
                    bool duplicado = IsDuplicateRecord_RegistrosPL(txt_ID_persona_legisladora.Text.ToString());

                    if (duplicado == true)
                    {
                        MessageBox.Show("El ID ya se encuentra registrado. Favor de verificar la información.", "Personas Legisladoras", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        GuardarDatos();

                        ClearControls(tabPagePL);

                        DGV_REGISTROS_PL();
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
        private bool ValidarCampos_PL()
        {
            // Array de controles a validar
            Control[] controlesAValidar = { txt_nombre_1_persona_legisladora, txt_apellido_1_persona_legisladora, dtp_fecha_nacimiento_persona_legisladora,
            cmb_sexo_persona_legisladora,cmb_estatus_persona_legisladora,cmb_caracter_cargo_persona_legisladora,cmb_escolaridad_persona_legisladora_PL,
            cmb_estatus_escolaridad_persona_legisladora,dgv_lengua_PA,dgv_tipo_discapacidad_PA,cmb_cond_pueblo_ind_persona_legisladora_PL,
            cmb_cond_pob_afromexicana_persona_legisladora_PL,cmb_forma_eleccion_persona_legisladora,cmb_tipo_adscripcion_inicial_persona_legisladora,
            cmb_tipo_adscripcion_final_persona_legisladora,cmb_cond_presentacion_declaracion_situacion_patrimonial,cmb_cond_presentacion_declaracion_intereses,
            cmb_cond_presentacion_declaracion_fiscal,txt_asistencia_legislativa_persona_legisladora,txt_gestion_parlamentaria_persona_legisladora,
            txt_atencion_ciudadana_persona_legisladora,txt_otro_concepto_gasto_persona_legisladora,cmb_cond_casa_atencion_ciudadana,
            cmb_cond_integrante_comision_permanente,cmb_cond_integrante_jucopo,cmb_cond_integrante_mesa_directiva,txt_cant_iniciativas_presentadas_persona_legisladora,
            txt_asist_sesiones_plenarias_persona_legisladora };

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
        private bool ValidarCampos_PL2()
        {
            // Array de controles a validar
            Control[] controlesAValidar = {
        txt_nombre_1_persona_legisladora, txt_apellido_1_persona_legisladora, dtp_fecha_nacimiento_persona_legisladora,
        cmb_sexo_persona_legisladora, cmb_estatus_persona_legisladora, cmb_caracter_cargo_persona_legisladora,
        cmb_escolaridad_persona_legisladora_PL, cmb_estatus_escolaridad_persona_legisladora,
        dgv_lengua_PA, dgv_tipo_discapacidad_PA, cmb_cond_pueblo_ind_persona_legisladora_PL,
        cmb_cond_pob_afromexicana_persona_legisladora_PL, cmb_forma_eleccion_persona_legisladora,
        cmb_tipo_adscripcion_inicial_persona_legisladora, cmb_tipo_adscripcion_final_persona_legisladora,
        cmb_cond_presentacion_declaracion_situacion_patrimonial, cmb_cond_presentacion_declaracion_intereses,
        cmb_cond_presentacion_declaracion_fiscal, txt_asistencia_legislativa_persona_legisladora,
        txt_gestion_parlamentaria_persona_legisladora, txt_atencion_ciudadana_persona_legisladora,
        txt_otro_concepto_gasto_persona_legisladora, cmb_cond_casa_atencion_ciudadana,
        cmb_cond_integrante_comision_permanente, cmb_cond_integrante_jucopo, cmb_cond_integrante_mesa_directiva,
        txt_cant_iniciativas_presentadas_persona_legisladora, txt_asist_sesiones_plenarias_persona_legisladora
    };

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

            return camposValidos;
        }

        private async void Control_GotFocus(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control != null)
            {
                Color originalColor = control.BackColor;
                control.BackColor = Color.Yellow; // Color de resaltado
                await Task.Delay(1500); // Espera 500 milisegundos
                control.BackColor = originalColor; // Restablece el color original
            }
        }


        private bool IsDuplicateRecord_RegistrosPL(string variable_cmb)
        {
            foreach (DataGridViewRow row in dgv_registros_pl.Rows)
            {
                if (row.IsNewRow) continue; // Skip the new row placeholder

                string existingId = row.Cells["txt_ID_persona_legisladora"].Value.ToString();

                if (existingId == variable_cmb)
                {
                    return true;
                }
            }
            return false;
        }
        private void DGV_REGISTROS_PL()
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
                    string query = "SELECT DISTINCT txt_ID_persona_legisladora, txt_nombre_1_persona_legisladora, " +
                                   "dtp_fecha_nacimiento_persona_legisladora, cmb_estatus_persona_legisladora, cmb_caracter_cargo_persona_legisladora " +
                                   "FROM TR_PERSONAS_LEGISLADORAS " +
                                   "WHERE id_legislatura = @id_legis";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        // Asignar el parámetro
                        cmd.Parameters.AddWithValue("@id_legis", id_legis);

                        // Utilizar un DataAdapter para obtener los datos
                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            dgv_registros_pl.DataSource = dataTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al llenar DGV personas legisladoras: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }
            }
        }
        private void GuardarDatos()
        {
            var data = new Dictionary<string, string>();

            // Recorrer todos los controles y guardar datos no vacíos en el diccionario
            RecorrerControles(tabPagePL, data);

            if (data.Count == 0)
            {
                MessageBox.Show("No hay datos para guardar.");
                return;
            }

            string cadena = "Data Source=DB_PLE.db;Version=3;";

            using (var connection = new SQLiteConnection(cadena))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Construir dinámicamente la consulta SQL
                        var columns = string.Join(", ", data.Keys);
                        var parameters = string.Join(", ", data.Keys.Select(key => "@" + key));
                        string query = $"INSERT INTO TR_PERSONAS_LEGISLADORAS ({columns}, fecha_actualizacion,id_legislatura) " +
                            $"VALUES " +
                            $"({parameters}, @fecha_actualizacion, @id_legislatura)";

                        using (var command = new SQLiteCommand(query, connection, transaction))
                        {
                            // Agregar los parámetros al comando
                            foreach (var kvp in data)
                            {
                                command.Parameters.AddWithValue($"@{kvp.Key}", kvp.Value);
                            }

                            // Registrar la consulta y los parámetros para depuración
                            Console.WriteLine("Query: " + query);
                            foreach (SQLiteParameter param in command.Parameters)
                            {
                                Console.WriteLine($"Parameter: {param.ParameterName} = {param.Value}");
                            }

                            command.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                            command.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text.ToString());

                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Datos guardados correctamente.");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Error al guardar los datos: {ex.Message}");
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
        }
        private void RecorrerControles(Control control, Dictionary<string, string> data)
        {
            // List of DataGridView names to exclude
            var excludedDataGridViews = new List<string> { "dgv_registros_pl" };

            foreach (Control c in control.Controls)
            {
                if (c is System.Windows.Forms.TextBox textBox && !string.IsNullOrWhiteSpace(textBox.Text))
                {
                    data.Add(textBox.Name, textBox.Text);
                }
                else if (c is System.Windows.Forms.ComboBox comboBox && !string.IsNullOrWhiteSpace(comboBox.Text))
                {
                    data.Add(comboBox.Name, comboBox.Text);
                }
                else if (c is System.Windows.Forms.DateTimePicker dateTimePicker)
                {
                    data.Add(dateTimePicker.Name, dateTimePicker.Text);
                }
                else if (c is DataGridView dataGridView && !excludedDataGridViews.Contains(dataGridView.Name))
                {
                    // Variable para almacenar las filas concatenadas
                    List<string> rowValuesList = new List<string>();

                    for (int i = 0; i < dataGridView.Rows.Count; i++)
                    {
                        string rowValues = string.Empty;
                        for (int j = 0; j < dataGridView.Columns.Count; j++)
                        {
                            if (dataGridView.Rows[i].Cells[j].Value != null)
                            {
                                rowValues = dataGridView.Rows[i].Cells[j].Value.ToString(); // Agrega un separador, como un espacio

                                if (!string.IsNullOrEmpty(rowValues))
                                {
                                    // se guardan los datagridviews que contienen (i,j) columnas*******
                                    if (dataGridView.Name == "dgv_nivel_escolaridad_PL")
                                    {
                                        string idPL2 = txt_ID_persona_legisladora.Text;
                                        string cadena2 = "Data Source=DB_PLE.db;Version=3;";

                                        using (SQLiteConnection conn = new SQLiteConnection(cadena2))
                                        {
                                            conn.Open();

                                            if (j == 0)
                                            {
                                                string query = "INSERT INTO TR_PERSONAS_LEGISLADORAS (id_legislatura, " +
                                                    "txt_ID_persona_legisladora," +
                                                    "dgv_carrera_licenciatura_persona_legisladora_PL, " +
                                                    "fecha_actualizacion) " +
                                                 "VALUES " +
                                                 "(@id_legislatura," +
                                                 "@txt_ID_persona_legisladora," +
                                                 "@RowValue," +
                                                 "@fecha_actualizacion)";

                                                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                                                {
                                                    cmd.Parameters.AddWithValue("@RowValue", rowValues);
                                                    cmd.Parameters.AddWithValue("@txt_ID_persona_legisladora", idPL2);
                                                    cmd.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                                    cmd.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text.ToString());

                                                    cmd.ExecuteNonQuery();
                                                }
                                            }
                                            if (j == 1)
                                            {
                                                string query = "INSERT INTO TR_PERSONAS_LEGISLADORAS (id_legislatura, txt_ID_persona_legisladora, dgv_carrera_maestria_persona_legisladora_PL," +
                                                    "fecha_actualizacion) " +
                                                "VALUES " +
                                                "(@id_legislatura,@txt_ID_persona_legisladora, @RowValue, @fecha_actualizacion)";
                                                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                                                {
                                                    cmd.Parameters.AddWithValue("@RowValue", rowValues);
                                                    cmd.Parameters.AddWithValue("@txt_ID_persona_legisladora", idPL2);
                                                    cmd.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                                    cmd.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text.ToString());

                                                    cmd.ExecuteNonQuery();
                                                }
                                            }
                                            if (j == 2)
                                            {
                                                string query = "INSERT INTO TR_PERSONAS_LEGISLADORAS (id_legislatura,txt_ID_persona_legisladora, dgv_carrera_doctorado_persona_legisladora_PL," +
                                                    "fecha_actualizacion) " +
                                                "VALUES " +
                                                "(@id_legislatura,@txt_ID_persona_legisladora, @RowValue, @fecha_actualizacion)";
                                                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                                                {
                                                    cmd.Parameters.AddWithValue("@RowValue", rowValues);
                                                    cmd.Parameters.AddWithValue("@txt_ID_persona_legisladora", idPL2);
                                                    cmd.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                                    cmd.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text.ToString());

                                                    cmd.ExecuteNonQuery();
                                                }
                                            }
                                        }
                                    }

                                    if (dataGridView.Name == "dgv_participacion_comisiones")
                                    {
                                        string idPL2 = txt_ID_persona_legisladora.Text;
                                        string cadena2 = "Data Source=DB_PLE.db;Version=3;";

                                        using (SQLiteConnection conn = new SQLiteConnection(cadena2))
                                        {
                                            conn.Open();

                                            if (j == 0)
                                            {
                                                string query = "INSERT INTO TR_PERSONAS_LEGISLADORAS (id_legislatura,txt_ID_persona_legisladora, dgv_nombre_comision_legislativa," +
                                                    "fecha_actualizacion) " +
                                                 "VALUES " +
                                                 "(@id_legislatura,@txt_ID_persona_legisladora, @RowValue, @fecha_actualizacion)";

                                                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                                                {
                                                    cmd.Parameters.AddWithValue("@RowValue", rowValues);
                                                    cmd.Parameters.AddWithValue("@txt_ID_persona_legisladora", idPL2);
                                                    cmd.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                                    cmd.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text.ToString());

                                                    cmd.ExecuteNonQuery();
                                                }
                                            }
                                            if (j == 1)
                                            {
                                                string query = "INSERT INTO TR_PERSONAS_LEGISLADORAS (id_legislatura,txt_ID_persona_legisladora, dgv_ID_comision_legislativa_pc," +
                                                    "fecha_actualizacion) " +
                                                "VALUES " +
                                                "(@id_legislatura,@txt_ID_persona_legisladora, @RowValue, @fecha_actualizacion)";
                                                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                                                {
                                                    cmd.Parameters.AddWithValue("@RowValue", rowValues);
                                                    cmd.Parameters.AddWithValue("@txt_ID_persona_legisladora", idPL2);
                                                    cmd.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                                    cmd.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text.ToString());

                                                    cmd.ExecuteNonQuery();
                                                }
                                            }
                                            if (j == 2)
                                            {
                                                string query = "INSERT INTO TR_PERSONAS_LEGISLADORAS (id_legislatura,txt_ID_persona_legisladora, dgv_cargo_comision_legislativa, fecha_actualizacion) " +
                                                "VALUES " +
                                                "(@id_legislatura,@txt_ID_persona_legisladora, @RowValue, @fecha_actualizacion)";
                                                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                                                {
                                                    cmd.Parameters.AddWithValue("@RowValue", rowValues);
                                                    cmd.Parameters.AddWithValue("@txt_ID_persona_legisladora", idPL2);
                                                    cmd.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                                    cmd.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text.ToString());

                                                    cmd.ExecuteNonQuery();
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                        }
                        if (!string.IsNullOrWhiteSpace(rowValues))
                        {
                            rowValues = rowValues.Trim(); // Elimina el espacio extra al final
                            rowValuesList.Add(rowValues);
                        }
                    }

                    // Se guardan los datagridview que solo contienen una columna******
                    foreach (var rowValue in rowValuesList)
                    {
                        // Aquí debes agregar tu lógica para guardar en la base de datos
                        string idPL = txt_ID_persona_legisladora.Text;

                        string cadena = "Data Source=DB_PLE.db;Version=3;";
                        using (SQLiteConnection conn = new SQLiteConnection(cadena))
                        {
                            conn.Open();
                            if (dataGridView.Name == "dgv_lengua_PL")
                            {
                                string query = "INSERT INTO TR_PERSONAS_LEGISLADORAS (id_legislatura, txt_ID_persona_legisladora, dgv_cond_lengua_ind_persona_legisladora_PL," +
                                    "fecha_actualizacion) " +
                                    "VALUES " +
                                    "(@id_legislatura,@txt_ID_persona_legisladora, @RowValue, @fecha_actualizacion)";

                                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@RowValue", rowValue);
                                    cmd.Parameters.AddWithValue("@txt_ID_persona_legisladora", idPL);
                                    cmd.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                    cmd.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text.ToString());

                                    cmd.ExecuteNonQuery();
                                }
                            }
                            if (dataGridView.Name == "dgv_tipo_discapacidad_PL")
                            {
                                string query = "INSERT INTO TR_PERSONAS_LEGISLADORAS (txt_ID_persona_legisladora, dgv_tipo_discapacidad_persona_legisladora," +
                                    "fecha_actualizacion,id_legislatura) " +
                                    "VALUES " +
                                    "(@txt_ID_persona_legisladora, @RowValue, @fecha_actualizacion, @id_legislatura)";

                                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@RowValue", rowValue);
                                    cmd.Parameters.AddWithValue("@txt_ID_persona_legisladora", idPL);
                                    cmd.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                    cmd.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text.ToString());

                                    cmd.ExecuteNonQuery();
                                }

                            }
                            if (dataGridView.Name == "dgv_partido_coalicion")
                            {
                                string query = "INSERT INTO TR_PERSONAS_LEGISLADORAS (txt_ID_persona_legisladora, dgv_partido_politico_candidatura_coalicion," +
                                    "fecha_actualizacion, id_legislatura) " +
                                    "VALUES " +
                                    "(@txt_ID_persona_legisladora, @RowValue, @fecha_actualizacion, @id_legislatura)";

                                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@RowValue", rowValue);
                                    cmd.Parameters.AddWithValue("@txt_ID_persona_legisladora", idPL);
                                    cmd.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                    cmd.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text.ToString());

                                    cmd.ExecuteNonQuery();
                                }

                            }

                        }
                    }


                }

                if (c.Controls.Count > 0)
                {
                    RecorrerControles(c, data);
                }
            }

        }
        // Método para limpiar los controles de un TabPage
        private void ClearControls(Control control)
        {
            // Lista de nombres de DataGridView a excluir
            var excludedDataGridViews = new List<string> { "dgv_registros_pl" };

            foreach (Control c in control.Controls)
            {
                if (c is System.Windows.Forms.TextBox)
                {
                    ((System.Windows.Forms.TextBox)c).Clear();
                }
                else if (c is System.Windows.Forms.ComboBox)
                {
                    ((System.Windows.Forms.ComboBox)c).SelectedIndex = -1;
                }
                else if (c is DataGridView)
                {
                    if (!excludedDataGridViews.Contains(c.Name))
                    {
                        ((DataGridView)c).Rows.Clear();
                    }
                }
                else if (c.HasChildren)
                {
                    // Llamar recursivamente si el control tiene hijos
                    ClearControls(c);
                }
            }
        }
        private void btnActualizarDGV_PL_Click(object sender, EventArgs e)
        {
            DGV_REGISTROS_PL();
        }


    }
}
