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
        // PRESENTACION --------------------------------------------------------------------------------------------------------------------

        // txt_turno_iniciativa_urgente_obvia
        private void txt_turno_iniciativa_urgente_obvia_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }

        // ESTATUS -------------------------------------------------------------------------------------------------------------------------

        private void Cmb_estatus_iniciativa_urgente_obvia()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_ESTATUS_INI_UO";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_estatus_iniciativa_urgente_obvia.DataSource = dataTable;
                    cmb_estatus_iniciativa_urgente_obvia.DisplayMember = "descripcion";

                    cmb_estatus_iniciativa_urgente_obvia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_estatus_iniciativa_urgente_obvia.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_estatus_iniciativa_urgente_obvia.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_estatus_iniciativa_urgente_obvia.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_estatus_iniciativa_urgente_obvia_Validating(object sender, CancelEventArgs e)
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

        // INGRESO -------------------------------------------------------------------------------------------------------------------------

        // txt_nombre_iniciativa_urgente_obvia
        private void txt_nombre_iniciativa_urgente_obvia_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_nombre_iniciativa_urgente_obvia_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_nombre_iniciativa_urgente_obvia.Text = txt_nombre_iniciativa_urgente_obvia.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_nombre_iniciativa_urgente_obvia.SelectionStart = txt_nombre_iniciativa_urgente_obvia.Text.Length;
        }

        // Fecha de Ingreso y Sesión en que se presento la iniciativa UO

        private void dtp_fecha_ingreso_iniciativa_urgente_obvia_oficialia_partes_CloseUp(object sender, EventArgs e)
        {
            // Obtener las fechas seleccionadas
            DateTime fechaIngreso = dtp_fecha_ingreso_iniciativa_urgente_obvia_oficialia_partes.Value.Date; // Solo la fecha, sin hora
            DateTime fechaTermInf = dtp_fecha_termino_informacion_reportada.Value; 
            // Validar si la fecha de ingreso es mayor que la fecha de término.
            if (fechaIngreso > fechaTermInf) // Solo si es mayor que fechaTermInf.
            {
                // Mostrar mensaje de error
                MessageBox.Show("La fecha de ingreso de la iniciativa de urgente y obvia resolución a oficialía de partes debe ser igual o menor a la fecha de término de la información reportada en datos generales.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Vaciar el campo de fecha
                dtp_fecha_ingreso_iniciativa_urgente_obvia_oficialia_partes.CustomFormat = " ";  // Vaciar el campo
                dtp_fecha_ingreso_iniciativa_urgente_obvia_oficialia_partes.Format = DateTimePickerFormat.Custom;  // Establecer formato personalizado vacío
            }
            else
            {
                // Si la fecha es válida (igual o menor que la fecha de término + 1), restaurar el formato de fecha corta
                dtp_fecha_ingreso_iniciativa_urgente_obvia_oficialia_partes.Format = DateTimePickerFormat.Short;
            }
        }
        private void dtp_fecha_sesion_presentacion_iniciativa_urgente_obvia_CloseUp(object sender, EventArgs e)
        {
            // Obtener las fechas seleccionadas
            DateTime fechaIngres = dtp_fecha_sesion_presentacion_iniciativa_urgente_obvia.Value.Date; // Solo la fecha, sin hora
            DateTime fechaSesion = dtp_fecha_ingreso_iniciativa_urgente_obvia_oficialia_partes.Value.Date; // Solo la fecha, sin hora

            // Validar si la fecha de la sesión es menor que la fecha de ingreso (queremos igual o mayor)
            if (fechaIngres < fechaSesion)
            {
                // Mostrar mensaje de error si la fecha de sesión es menor que la de ingreso
                MessageBox.Show("La fecha de la sesión en que se presentó la iniciativa de urgente y obvia resolución debe ser igual o mayor a la fecha de ingreso de la iniciativa de urgente y obvia resolución a oficialía de partes.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Vaciar el campo de fecha
                dtp_fecha_sesion_presentacion_iniciativa_urgente_obvia.CustomFormat = " ";  // Dejar el campo vacío
                dtp_fecha_sesion_presentacion_iniciativa_urgente_obvia.Format = DateTimePickerFormat.Custom;  // Establecer formato personalizado vacío
            }
            else
            {
                // Si la fecha es válida (igual o mayor), restaurar el formato de fecha corta
                dtp_fecha_sesion_presentacion_iniciativa_urgente_obvia.Format = DateTimePickerFormat.Short;
            }
        }

        // TIPO ---------------------------------------------------------------------------------------------------------------------------

        private void Cmb_tipo_iniciativa_urgente_obvia() 
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

                    cmb_tipo_iniciativa_urgente_obvia.DataSource = dataTable;
                    cmb_tipo_iniciativa_urgente_obvia.DisplayMember = "descripcion";

                    cmb_tipo_iniciativa_urgente_obvia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_tipo_iniciativa_urgente_obvia.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_tipo_iniciativa_urgente_obvia.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_tipo_iniciativa_urgente_obvia.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_tipo_iniciativa_urgente_obvia_Validating(object sender, CancelEventArgs e)
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
        private void cmb_tipo_iniciativa_urgente_obvia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_tipo_iniciativa_urgente_obvia.Text.Trim();

            // Desbloquear Otro estatus de la iniciativa.
            if (valorComboBox1.Equals("Otro tipo (especifique)", StringComparison.OrdinalIgnoreCase))
            {
                txt_otro_tipo_iniciativa_urgente_obvia_especifique.Enabled = true;
                txt_otro_tipo_iniciativa_urgente_obvia_especifique.BackColor = Color.Honeydew;

            }
            else
            {
                txt_otro_tipo_iniciativa_urgente_obvia_especifique.Enabled = false;
                txt_otro_tipo_iniciativa_urgente_obvia_especifique.BackColor = Color.LightGray;
                txt_otro_tipo_iniciativa_urgente_obvia_especifique.Text = "";
            }

        }

        // txt_otro_tipo_iniciativa_urgente_obvia_especifique
        private void txt_otro_tipo_iniciativa_urgente_obvia_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_otro_tipo_iniciativa_urgente_obvia_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_otro_tipo_iniciativa_urgente_obvia_especifique.Text = txt_otro_tipo_iniciativa_urgente_obvia_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_otro_tipo_iniciativa_urgente_obvia_especifique.SelectionStart = txt_otro_tipo_iniciativa_urgente_obvia_especifique.Text.Length;
        }

        // PROMOVENTE ----------------------------------------------------------------------------------------------------------------------

        private void Cmb_tipo_promovente_iniciativa_urgente_obvia() 
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

                    cmb_tipo_promovente_iniciativa_urgente_obvia.DataSource = dataTable;
                    cmb_tipo_promovente_iniciativa_urgente_obvia.DisplayMember = "descripcion";

                    cmb_tipo_promovente_iniciativa_urgente_obvia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_tipo_promovente_iniciativa_urgente_obvia.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_tipo_promovente_iniciativa_urgente_obvia.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_tipo_promovente_iniciativa_urgente_obvia.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_tipo_promovente_iniciativa_urgente_obvia_Validating(object sender, CancelEventArgs e)
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
        private void cmb_tipo_promovente_iniciativa_urgente_obvia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_tipo_promovente_iniciativa_urgente_obvia.Text.Trim();

            // Desbloquea ComboBox de nombre de la persona legisladora
            if (valorComboBox1.Equals("Personas legisladoras", StringComparison.OrdinalIgnoreCase))
            {
                cmb_nombre_persona_legisladora_1_UO.Enabled = true;
                cmb_nombre_persona_legisladora_1_UO.BackColor = Color.Honeydew;
                btn_agregar_per_leg_UO.Enabled = true; btn_eliminar_pers_legis_UO.Enabled = true;
                dgv_per_legis_UO.BackgroundColor = Color.Honeydew;
            }
            else
            {
                cmb_nombre_persona_legisladora_1_UO.Enabled = false;
                cmb_nombre_persona_legisladora_1_UO.BackColor = Color.LightGray;
                cmb_nombre_persona_legisladora_1_UO.Text = "";
                btn_agregar_per_leg_UO.Enabled = false; btn_eliminar_pers_legis_UO.Enabled = false;
                dgv_per_legis_UO.BackgroundColor = Color.LightGray;
                dgv_per_legis_UO.Rows.Clear();
            }

            // Desbloquea el ID de personas legisladorasn 
            if (valorComboBox1.Equals("Personas legisladoras", StringComparison.OrdinalIgnoreCase))
            {
                txt_ID_persona_legisladora_1_UO.Enabled = false;
                txt_ID_persona_legisladora_1_UO.BackColor = Color.Honeydew;
            }
            else
            {
                txt_ID_persona_legisladora_1_UO.Enabled = false;
                txt_ID_persona_legisladora_1_UO.BackColor = Color.LightGray;
                txt_ID_persona_legisladora_1_UO.Text = "";
            }

            // Desbloquea Grupo parlamentario tabla y botones
            if (valorComboBox1.Equals("Grupo parlamentario", StringComparison.OrdinalIgnoreCase))
            {
                cmb_grupo_parlamentario_UO.Enabled = true;
                cmb_grupo_parlamentario_UO.BackColor = Color.Honeydew;
                //btn_agregar_grupo_parla.Enabled = true; btn_eliminar_grupo_parla.Enabled = true;
                //dgv_grupos_parla.BackgroundColor = Color.Honeydew;

            }
            else
            {
                cmb_grupo_parlamentario_UO.Enabled = false;
                cmb_grupo_parlamentario_UO.BackColor = Color.LightGray;
                cmb_grupo_parlamentario_UO.Text = "";
                //btn_agregar_grupo_parla.Enabled = false; btn_eliminar_grupo_parla.Enabled = false;
                //dgv_grupos_parla.BackgroundColor = Color.LightGray;
                dgv_grupos_parla_UO.Rows.Clear();
            }

            // Desbloquea Comisiones legislativas
            if (valorComboBox1.Equals("Comisión legislativa", StringComparison.OrdinalIgnoreCase))
            {
                cmb_nombre_comision_legislativa_1_UO.Enabled = true;
                cmb_nombre_comision_legislativa_1_UO.BackColor = Color.Honeydew;
                btn_agregar_nom_com_leg_UO.Enabled = true; btn_elimina_con_legisl_UO.Enabled = true;
                dgv_com_legis_UO.BackgroundColor = Color.Honeydew;
                txt_ID_comision_legislativa_1_UO.Enabled = false;
                txt_ID_comision_legislativa_1_UO.BackColor = Color.Honeydew;
            }
            else
            {
                cmb_nombre_comision_legislativa_1_UO.Enabled = false;
                cmb_nombre_comision_legislativa_1_UO.BackColor = Color.LightGray;
                cmb_nombre_comision_legislativa_1_UO.Text = "";
                btn_agregar_nom_com_leg_UO.Enabled = false; btn_elimina_con_legisl_UO.Enabled = false;
                dgv_com_legis_UO.BackgroundColor = Color.LightGray;
                dgv_com_legis_UO.Rows.Clear();
                txt_ID_comision_legislativa_1_UO.Enabled = false;
                txt_ID_comision_legislativa_1_UO.BackColor = Color.LightGray;
                txt_ID_comision_legislativa_1_UO.Text = "";
            }
            // Desbloquea Otro tipo de promovente
            if (valorComboBox1.Equals("Otro tipo de promovente (especifique)", StringComparison.OrdinalIgnoreCase))
            {
                txt_otro_tipo_promovente_iniciativa_urgente_obvia_especifique.Enabled = true;
                txt_otro_tipo_promovente_iniciativa_urgente_obvia_especifique.BackColor = Color.Honeydew;
            }
            else
            {
                txt_otro_tipo_promovente_iniciativa_urgente_obvia_especifique.Enabled = false;
                txt_otro_tipo_promovente_iniciativa_urgente_obvia_especifique.BackColor = Color.LightGray;
                txt_otro_tipo_promovente_iniciativa_urgente_obvia_especifique.Text = "";
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

        private void Cmb_nombre_persona_legisladora_1_UO() 
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

                    cmb_nombre_persona_legisladora_1_UO.DataSource = dataTable;
                    cmb_nombre_persona_legisladora_1_UO.DisplayMember = "txt_nombre_1_persona_legisladora";

                    cmb_nombre_persona_legisladora_1_UO.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_nombre_persona_legisladora_1_UO.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_nombre_persona_legisladora_1_UO.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_nombre_persona_legisladora_1_UO.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_nombre_persona_legisladora_1_UO_Validating(object sender, CancelEventArgs e)
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
        private void cmb_nombre_persona_legisladora_1_UO_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = cmb_nombre_persona_legisladora_1_UO.Text;

            // Verificar si el nombre seleccionado es nulo o vacío
            if (string.IsNullOrEmpty(nombreSeleccionado))
            {
                txt_ID_persona_legisladora_1_UO.Text = "";
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
                            txt_ID_persona_legisladora_1_UO.Text = resultado.ToString();
                        }
                        else
                        {
                            txt_ID_persona_legisladora_1_UO.Text = ""; // Limpiar el TextBox si no se encontró un ID
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
        private void btn_agregar_per_leg_UO_Click(object sender, EventArgs e)
        {
            // Obtener el nombre y el ID seleccionados
            string nombreSeleccionado = cmb_nombre_persona_legisladora_1_UO.Text.Trim();
            string idSeleccionado = txt_ID_persona_legisladora_1_UO.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreSeleccionado) || string.IsNullOrWhiteSpace(idSeleccionado))
            {
                MessageBox.Show("Revisar datos vacíos");
            }
            else
            {
                // Verificar si el nombre ya existe en la tabla
                bool respuesta = IsDuplicateRecord_PL_UO(nombreSeleccionado);

                if (respuesta)
                {
                    MessageBox.Show("Dato duplicado");
                    cmb_nombre_persona_legisladora_1_UO.Text = "";
                }
                else
                {
                    // Agregar una nueva fila al DataGridView con el formato: ID primero, luego el nombre
                    dgv_per_legis_UO.Rows.Add(idSeleccionado, nombreSeleccionado);

                    // Limpiar los campos después de agregar los datos
                    cmb_nombre_persona_legisladora_1_UO.Text = "";
                    txt_ID_persona_legisladora_1_UO.Text = "";
                }
            }
        }
        private void btn_eliminar_pers_legis_UO_Click(object sender, EventArgs e)
        {
            if (dgv_per_legis_UO.SelectedRows.Count > 0)
            {
                dgv_per_legis_UO.Rows.RemoveAt(dgv_per_legis_UO.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }
        private bool IsDuplicateRecord_PL_UO(string variable_cmb)
        {
            foreach (DataGridViewRow row in dgv_per_legis_UO.Rows)
            {
                if (row.IsNewRow) continue; // Skip the new row placeholder

                string existingId = row.Cells["Per_leg_ou"].Value.ToString();

                if (existingId == variable_cmb)
                {
                    return true;
                }
            }
            return false;
        }

        // Tabla grupo parlamentario ------------- La funcion de filtro esta en datos generales en la entidad seleccionada

        private void cmb_grupo_parlamentario_UO_Validating(object sender, CancelEventArgs e)
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
        private void cmb_grupo_parlamentario_UO_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_grupo_parlamentario_UO.Text.Trim();

            // Desbloquea Grupo parlamentario tabla y botones varios
            if (valorComboBox1.Equals("Varios", StringComparison.OrdinalIgnoreCase))
            {
                cmb_varios_grupos_parlamentarios_especifique_UO.Enabled = true;
                cmb_varios_grupos_parlamentarios_especifique_UO.BackColor = Color.Honeydew;
                cmb_varios_grupos_parlamentarios_especifique_UO.Text = "";
                btn_agregar_grupo_parla_UO.Enabled = true; btn_eliminar_grupo_parla_UO.Enabled = true;
                dgv_grupos_parla_UO.BackgroundColor = Color.Honeydew;
            }
            else
            {
                cmb_varios_grupos_parlamentarios_especifique_UO.Enabled = false;
                cmb_varios_grupos_parlamentarios_especifique_UO.BackColor = Color.LightGray;
                cmb_varios_grupos_parlamentarios_especifique_UO.Text = "";
                btn_agregar_grupo_parla_UO.Enabled = false; btn_eliminar_grupo_parla_UO.Enabled = false;
                dgv_grupos_parla_UO.BackgroundColor = Color.LightGray;
                dgv_grupos_parla_UO.Rows.Clear();
            }
        }

        // Varios grupos parlamentarios

        private void cmb_varios_grupos_parlamentarios_especifique_UO_Validating(object sender, CancelEventArgs e)
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

        private void btn_agregar_grupo_parla_UO_Click(object sender, EventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = cmb_varios_grupos_parlamentarios_especifique_UO.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreSeleccionado))
            {
                MessageBox.Show("Revisar datos vacíos");
            }
            else
            {
                // Verificar si el nombre ya existe en la tabla
                bool respuesta = IsDuplicateRecord_ParUO(nombreSeleccionado);

                if (respuesta)
                {
                    MessageBox.Show("Dato duplicado");
                    cmb_varios_grupos_parlamentarios_especifique_UO.Text = "";
                }
                else
                {
                    // Agregar una nueva fila al DataGridView
                    dgv_grupos_parla_UO.Rows.Add(nombreSeleccionado);
                    cmb_varios_grupos_parlamentarios_especifique_UO.Text = "";

                }
            }
        }
        private void btn_eliminar_grupo_parla_UO_Click(object sender, EventArgs e)
        {
            if (dgv_grupos_parla_UO.SelectedRows.Count > 0)
            {
                dgv_grupos_parla_UO.Rows.RemoveAt(dgv_grupos_parla_UO.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }
        private bool IsDuplicateRecord_ParUO(string variable_cmb)
        {
            foreach (DataGridViewRow row in dgv_grupos_parla_UO.Rows)
            {
                if (row.IsNewRow) continue; // Skip the new row placeholder

                string existingId = row.Cells["Grupos_parlamentarios_ini_uo"].Value.ToString();

                if (existingId == variable_cmb)
                {
                    return true;
                }
            }
            return false;
        }

        // Tabla comisiones legislativas_UO

        private void Cmb_nombre_comision_legislativa_1_UO()
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

                    cmb_nombre_comision_legislativa_1_UO.DataSource = dataTable;
                    cmb_nombre_comision_legislativa_1_UO.DisplayMember = "nombre_comision_legislativa";

                    cmb_nombre_comision_legislativa_1_UO.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_nombre_comision_legislativa_1_UO.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_nombre_comision_legislativa_1_UO.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_nombre_comision_legislativa_1_UO.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_nombre_comision_legislativa_1_UO_Validating(object sender, CancelEventArgs e)
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
        private void cmb_nombre_comision_legislativa_1_UO_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = cmb_nombre_comision_legislativa_1_UO.Text;

            // Verificar si el nombre seleccionado es nulo o vacío
            if (string.IsNullOrEmpty(nombreSeleccionado))
            {
                txt_ID_comision_legislativa_1_UO.Text = "";
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
                            txt_ID_comision_legislativa_1_UO.Text = resultado.ToString();
                        }
                        else
                        {
                            txt_ID_comision_legislativa_1_UO.Text = ""; // Limpiar el TextBox si no se encontró un ID
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

        private void btn_agregar_nom_com_leg_UO_Click(object sender, EventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = cmb_nombre_comision_legislativa_1_UO.Text.Trim();
            string idSeleccionado = txt_ID_comision_legislativa_1_UO.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreSeleccionado) || string.IsNullOrWhiteSpace(idSeleccionado))
            {
                MessageBox.Show("Revisar datos vacíos");
            }
            else
            {
                // Verificar si el nombre ya existe en la tabla
                bool respuesta = IsDuplicateRecord_COMLUO(nombreSeleccionado);

                if (respuesta)
                {
                    MessageBox.Show("Dato duplicado");
                    cmb_nombre_comision_legislativa_1_UO.Text = "";
                }
                else
                {
                    // Agregar una nueva fila al DataGridView
                    dgv_com_legis_UO.Rows.Add(idSeleccionado, nombreSeleccionado);

                    // Limpiar los campos
                    cmb_nombre_comision_legislativa_1_UO.Text = "";
                    txt_ID_comision_legislativa_1_UO.Text = "";  // Limpiar el campo txt_ID_comision_legislativa_1_UO
                }
            }
        }
        private void btn_elimina_con_legisl_UO_Click(object sender, EventArgs e)
        {
            if (dgv_com_legis_UO.SelectedRows.Count > 0)
            {
                dgv_com_legis_UO.Rows.RemoveAt(dgv_com_legis_UO.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }
        private bool IsDuplicateRecord_COMLUO(string variable_cmb)

        {
            foreach (DataGridViewRow row in dgv_com_legis_UO.Rows)
            {
                if (row.IsNewRow) continue; // Skip the new row placeholder

                string existingId = row.Cells["Nom_com_l_i_uo"].Value.ToString();

                if (existingId == variable_cmb)
                {
                    return true;
                }
            }
            return false;
        }

        // txt_otro_tipo_promovente_iniciativa_urgente_obvia_especifique
        private void txt_otro_tipo_promovente_iniciativa_urgente_obvia_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_otro_tipo_promovente_iniciativa_urgente_obvia_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_otro_tipo_promovente_iniciativa_urgente_obvia_especifique.Text = txt_otro_tipo_promovente_iniciativa_urgente_obvia_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_otro_tipo_promovente_iniciativa_urgente_obvia_especifique.SelectionStart = txt_otro_tipo_promovente_iniciativa_urgente_obvia_especifique.Text.Length;
        }




        //-----------------------------









        // txt_otro_tipo_organo_constitucional_autonomo_especifique_uo
        private void txt_otro_tipo_organo_constitucional_autonomo_especifique_uo_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_otro_tipo_organo_constitucional_autonomo_especifique_uo_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_otro_tipo_organo_constitucional_autonomo_especifique_uo.Text = txt_otro_tipo_organo_constitucional_autonomo_especifique_uo.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_otro_tipo_organo_constitucional_autonomo_especifique_uo.SelectionStart = txt_otro_tipo_organo_constitucional_autonomo_especifique_uo.Text.Length;
        }

       
        // txt_votaciones_pleno_a_favor_iniciativa_urgente_obvia
        private void txt_votaciones_pleno_a_favor_iniciativa_urgente_obvia_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }

        // txt_votaciones_pleno_en_contra_iniciativa_urgente_obvia
        private void txt_votaciones_pleno_en_contra_iniciativa_urgente_obvia_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }

        // txt_votaciones_pleno_abstencion_iniciativa_urgente_obvia
        private void txt_votaciones_pleno_abstencion_iniciativa_urgente_obvia_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }

    }
}
