﻿using DocumentFormat.OpenXml.Drawing;
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






        // Configuración de TXT para mayuculas y Numeros 

        // txt_turno_denuncia_declaracion_procedencia
        private void txt_turno_denuncia_declaracion_procedencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
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
