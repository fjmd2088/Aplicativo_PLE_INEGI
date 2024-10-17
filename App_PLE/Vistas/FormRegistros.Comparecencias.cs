using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2010.Word;
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
        // ---------------------------  ESTATUS --------------------------

        // txt_consecutivo_comparecencia
        private void txt_consecutivo_comparecencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }

        }

        private void dtp_fecha_comparecencia_CloseUp(object sender, EventArgs e)
        {
            // Obtener las fechas seleccionadas
            DateTime fechaComparecencia = dtp_fecha_comparecencia.Value.Date; // Fecha de comparecencia
            DateTime fechaInicio = dtp_fecha_inicio_informacion_reportada.Value.Date; // Fecha de inicio
            DateTime fechaTermino = dtp_fecha_termino_informacion_reportada.Value.Date; // Fecha de término

            // Validar si la fecha de comparecencia está entre las fechas de inicio y término
            if (fechaComparecencia < fechaInicio || fechaComparecencia > fechaTermino)
            {
                // Mostrar mensaje de error
                MessageBox.Show("La fecha de comparecencia debe encontrarse entre las fecha de inicio de la información reportada y fecha de término de la información reportada en datos generales.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Vaciar el campo de fecha
                dtp_fecha_comparecencia.CustomFormat = " ";  // Vaciar el campo
                dtp_fecha_comparecencia.Format = DateTimePickerFormat.Custom;  // Establecer formato personalizado vacío
            }
            else
            {
                // Si la fecha es válida, restaurar el formato de fecha corta
                dtp_fecha_comparecencia.Format = DateTimePickerFormat.Short;
            }
        }


        // ----------- Motivo ---------------------

        private void Cmb_motivo_comparecencia()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_MOTIVO_COMPAR";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_motivo_comparecencia.DataSource = dataTable;
                    cmb_motivo_comparecencia.DisplayMember = "descripcion";

                    cmb_motivo_comparecencia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_motivo_comparecencia.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_motivo_comparecencia.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_motivo_comparecencia.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_motivo_comparecencia_Validating(object sender, CancelEventArgs e)
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
        private void cmb_motivo_comparecencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_motivo_comparecencia.Text.Trim();

            // Bloquea la condición de la debnucia
            if (valorComboBox1.Equals("Otro motivo (especifique)", StringComparison.OrdinalIgnoreCase))
            {
                txt_otro_motivo_comparecencia_especifique.Enabled = true;
                txt_otro_motivo_comparecencia_especifique.BackColor = Color.Honeydew;
                txt_otro_motivo_comparecencia_especifique.Text = "";

            }
            else
            {
                txt_otro_motivo_comparecencia_especifique.Enabled = true;
                txt_otro_motivo_comparecencia_especifique.BackColor = Color.LightGray;
                txt_otro_motivo_comparecencia_especifique.Text = "";

            }
        }
        
        // txt_otro_motivo_comparecencia_especifique
        private void txt_otro_motivo_comparecencia_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_otro_motivo_comparecencia_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_otro_motivo_comparecencia_especifique.Text = txt_otro_motivo_comparecencia_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_otro_motivo_comparecencia_especifique.SelectionStart = txt_otro_motivo_comparecencia_especifique.Text.Length;
        }

        // ----------- Modalidad ---------------------

        private void Cmb_modalidad_comparecencia()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_MODALIDAD_COMPAR";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_modalidad_comparecencia.DataSource = dataTable;
                    cmb_modalidad_comparecencia.DisplayMember = "descripcion";

                    cmb_modalidad_comparecencia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_modalidad_comparecencia.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_modalidad_comparecencia.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_modalidad_comparecencia.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_modalidad_comparecencia_Validating(object sender, CancelEventArgs e)
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
        private void cmb_modalidad_comparecencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_modalidad_comparecencia.Text.Trim();

            // Bloquea la condición de la debnucia
            if (valorComboBox1.Equals("Otra modalidad (especifique)", StringComparison.OrdinalIgnoreCase))
            {
                cmb_otra_modalidad_comparecencia_especifique.Enabled = true;
                cmb_otra_modalidad_comparecencia_especifique.BackColor = Color.Honeydew;
                cmb_otra_modalidad_comparecencia_especifique.Text = "";

            }
            else
            {
                cmb_otra_modalidad_comparecencia_especifique.Enabled = true;
                cmb_otra_modalidad_comparecencia_especifique.BackColor = Color.LightGray;
                cmb_otra_modalidad_comparecencia_especifique.Text = "";

            }

            // Desbloquea tabla de comisiones legislativas
            if (valorComboBox1.Equals("Ante Comisiones Legislativas", StringComparison.OrdinalIgnoreCase) | (valorComboBox1.Equals("Ante Pleno y Comisiones Legislativas", StringComparison.OrdinalIgnoreCase)))
            {
                cmb_nombre_comision_legislativa_1_comparecencia.Enabled = true;
                cmb_nombre_comision_legislativa_1_comparecencia.BackColor = Color.Honeydew;
                cmb_nombre_comision_legislativa_1_comparecencia.Text = "";

                txt_ID_comision_legislativa_1_comparecencia.Enabled = false;
                txt_ID_comision_legislativa_1_comparecencia.BackColor = Color.Honeydew;
                txt_ID_comision_legislativa_1_comparecencia.Text = "";

                btn_agregar_compare.Enabled = true;
                btn_agregar_compare.BackColor = Color.Honeydew;

                btn_eliminar_compare.Enabled = true;
                btn_eliminar_compare.BackColor = Color.Honeydew;

                dgv_comisiones_leg_comparecencias.BackgroundColor = Color.Honeydew;


            }
            else
            {
                cmb_nombre_comision_legislativa_1_comparecencia.Enabled = false;
                cmb_nombre_comision_legislativa_1_comparecencia.BackColor = Color.LightGray;
                cmb_nombre_comision_legislativa_1_comparecencia.Text = "";

                txt_ID_comision_legislativa_1_comparecencia.Enabled = false;
                txt_ID_comision_legislativa_1_comparecencia.BackColor = Color.LightGray;
                txt_ID_comision_legislativa_1_comparecencia.Text = "";

                btn_agregar_compare.Enabled = false;
                btn_agregar_compare.BackColor = Color.LightGray;

                btn_eliminar_compare.Enabled = false;
                btn_eliminar_compare.BackColor = Color.LightGray;

                dgv_comisiones_leg_comparecencias.Rows.Clear();
                dgv_comisiones_leg_comparecencias.BackgroundColor = Color.LightGray;
            }
        }

        // cmb_otra_modalidad_comparecencia_especifique
        private void cmb_otra_modalidad_comparecencia_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void cmb_otra_modalidad_comparecencia_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            cmb_otra_modalidad_comparecencia_especifique.Text = cmb_otra_modalidad_comparecencia_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            cmb_otra_modalidad_comparecencia_especifique.SelectionStart = cmb_otra_modalidad_comparecencia_especifique.Text.Length;
        }
 

        // ----------- TABLA Comisión ante la cual comparecio ---------------------

        private void Cmb_nombre_comision_legislativa_1_comparecencia()
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

                    cmb_nombre_comision_legislativa_1_comparecencia.DataSource = dataTable;
                    cmb_nombre_comision_legislativa_1_comparecencia.DisplayMember = "nombre_comision_legislativa";

                    cmb_nombre_comision_legislativa_1_comparecencia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_nombre_comision_legislativa_1_comparecencia.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_nombre_comision_legislativa_1_comparecencia.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_nombre_comision_legislativa_1_comparecencia.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_nombre_comision_legislativa_1_comparecencia_Validating(object sender, CancelEventArgs e)
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
        private void cmb_nombre_comision_legislativa_1_comparecencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = cmb_nombre_comision_legislativa_1_comparecencia.Text;

            // Verificar si el nombre seleccionado es nulo o vacío
            if (string.IsNullOrEmpty(nombreSeleccionado))
            {
                txt_ID_comision_legislativa_1_comparecencia.Text = "";
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
                            txt_ID_comision_legislativa_1_comparecencia.Text = resultado.ToString();
                        }
                        else
                        {
                            txt_ID_comision_legislativa_1_comparecencia.Text = ""; // Limpiar el TextBox si no se encontró un ID
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

        private void btn_agregar_compare_Click(object sender, EventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = cmb_nombre_comision_legislativa_1_comparecencia.Text.Trim();
            string idSeleccionado = txt_ID_comision_legislativa_1_comparecencia.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreSeleccionado) || string.IsNullOrWhiteSpace(idSeleccionado))
            {
                MessageBox.Show("Revisar datos vacíos");
            }
            else
            {
                // Verificar si el nombre ya existe en la tabla
                bool respuesta = IsDuplicateRecord_compare(nombreSeleccionado);

                if (respuesta)
                {
                    MessageBox.Show("Dato duplicado");
                    cmb_nombre_comision_legislativa_1_comparecencia.Text = "";
                }
                else
                {
                    // Agregar una nueva fila al DataGridView
                    dgv_comisiones_leg_comparecencias.Rows.Add(idSeleccionado, nombreSeleccionado);

                    // Limpiar los campos
                    cmb_nombre_comision_legislativa_1_comparecencia.Text = "";
                    txt_ID_comision_legislativa_1_comparecencia.Text = "";  // Limpiar el campo txt_ID_comision_legislativa_1_UO
                }
            }
        }
        private void btn_eliminar_compare_Click(object sender, EventArgs e)
        {
            if (dgv_comisiones_leg_comparecencias.SelectedRows.Count > 0)
            {
                dgv_comisiones_leg_comparecencias.Rows.RemoveAt(dgv_comisiones_leg_comparecencias.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }
        private bool IsDuplicateRecord_compare(string variable_cmb)

        {
            foreach (DataGridViewRow row in dgv_comisiones_leg_comparecencias.Rows)
            {
                if (row.IsNewRow) continue; // Skip the new row placeholder

                string existingId = row.Cells["com_leg_comp"].Value.ToString();

                if (existingId == variable_cmb)
                {
                    return true;
                }
            }
            return false;
        }

        // ----------- Características sociodemográficas de la persona servidora pública compareciente ---------------------

        private void Cmb_sexo_persona_servidora_publica_comparecencia()
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

                    cmb_sexo_persona_servidora_publica_comparecencia.DataSource = dataTable;
                    cmb_sexo_persona_servidora_publica_comparecencia.DisplayMember = "descripcion";

                    cmb_sexo_persona_servidora_publica_comparecencia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_sexo_persona_servidora_publica_comparecencia.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_sexo_persona_servidora_publica_comparecencia.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_sexo_persona_servidora_publica_comparecencia.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_sexo_persona_servidora_publica_comparecencia_Validating(object sender, CancelEventArgs e)
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



        // txt_nombre_1_persona_servidora_publica_comparecencia
        private void txt_nombre_1_persona_servidora_publica_comparecencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_nombre_1_persona_servidora_publica_comparecencia_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_nombre_1_persona_servidora_publica_comparecencia.Text = txt_nombre_1_persona_servidora_publica_comparecencia.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_nombre_1_persona_servidora_publica_comparecencia.SelectionStart = txt_nombre_1_persona_servidora_publica_comparecencia.Text.Length;

            // Desbloquear txt_nombre_2_persona_servidora_publica_comparecencia, cambiar su color de fondo, o borrarlo y deshabilitarlo
            if (!string.IsNullOrEmpty(txt_nombre_1_persona_servidora_publica_comparecencia.Text))
            {
                txt_nombre_2_persona_servidora_publica_comparecencia.Enabled = true;
                txt_nombre_2_persona_servidora_publica_comparecencia.BackColor = Color.Honeydew;
            }
            else
            {
                // Si txt_nombre_2_persona_servidora_publica_juicio_politico está vacío, borrar y deshabilitar txt_nombre_2_personal_apoyo
                txt_nombre_2_persona_servidora_publica_comparecencia.Text = string.Empty;
                txt_nombre_2_persona_servidora_publica_comparecencia.Enabled = false;
                txt_nombre_2_persona_servidora_publica_comparecencia.BackColor = SystemColors.Window; // Restaurar el color predeterminado
            }
        }

        // txt_nombre_2_persona_servidora_publica_comparecencia
        private void txt_nombre_2_persona_servidora_publica_comparecencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_nombre_2_persona_servidora_publica_comparecencia_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_nombre_2_persona_servidora_publica_comparecencia.Text = txt_nombre_2_persona_servidora_publica_comparecencia.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_nombre_2_persona_servidora_publica_comparecencia.SelectionStart = txt_nombre_2_persona_servidora_publica_comparecencia.Text.Length;

            // Desbloquear txt_nombre_2_persona_servidora_publica_comparecencia, cambiar su color de fondo, o borrarlo y deshabilitarlo
            if (!string.IsNullOrEmpty(txt_nombre_2_persona_servidora_publica_comparecencia.Text))
            {
                txt_nombre_3_persona_servidora_publica_comparecencia.Enabled = true;
                txt_nombre_3_persona_servidora_publica_comparecencia.BackColor = Color.Honeydew;
            }
            else
            {
                // Si txt_nombre_2_persona_servidora_publica_juicio_politico está vacío, borrar y deshabilitar txt_nombre_2_personal_apoyo
                txt_nombre_3_persona_servidora_publica_comparecencia.Text = string.Empty;
                txt_nombre_3_persona_servidora_publica_comparecencia.Enabled = false;
                txt_nombre_3_persona_servidora_publica_comparecencia.BackColor = SystemColors.Window; // Restaurar el color predeterminado
            }

        }

        // txt_nombre_3_persona_servidora_publica_comparecencia
        private void txt_nombre_3_persona_servidora_publica_comparecencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_nombre_3_persona_servidora_publica_comparecencia_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_nombre_3_persona_servidora_publica_comparecencia.Text = txt_nombre_3_persona_servidora_publica_comparecencia.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_nombre_3_persona_servidora_publica_comparecencia.SelectionStart = txt_nombre_3_persona_servidora_publica_comparecencia.Text.Length;
        }

        // txt_apellido_1_persona_servidora_publica_comparecencia
        private void txt_apellido_1_persona_servidora_publica_comparecencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_apellido_1_persona_servidora_publica_comparecencia_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_apellido_1_persona_servidora_publica_comparecencia.Text = txt_apellido_1_persona_servidora_publica_comparecencia.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_apellido_1_persona_servidora_publica_comparecencia.SelectionStart = txt_apellido_1_persona_servidora_publica_comparecencia.Text.Length;

            // Desbloquear txt_nombre_2_persona_servidora_publica_comparecencia, cambiar su color de fondo, o borrarlo y deshabilitarlo
            if (!string.IsNullOrEmpty(txt_apellido_1_persona_servidora_publica_comparecencia.Text))
            {
                txt_apellido_2_persona_servidora_publica_comparecencia.Enabled = true;
                txt_apellido_2_persona_servidora_publica_comparecencia.BackColor = Color.Honeydew;
            }
            else
            {
                // Si txt_nombre_2_persona_servidora_publica_juicio_politico está vacío, borrar y deshabilitar txt_nombre_2_personal_apoyo
                txt_apellido_2_persona_servidora_publica_comparecencia.Text = string.Empty;
                txt_apellido_2_persona_servidora_publica_comparecencia.Enabled = false;
                txt_apellido_2_persona_servidora_publica_comparecencia.BackColor = SystemColors.Window; // Restaurar el color predeterminado
            }
        }

        // txt_apellido_2_persona_servidora_publica_comparecencia
        private void txt_apellido_2_persona_servidora_publica_comparecencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_apellido_2_persona_servidora_publica_comparecencia_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_apellido_2_persona_servidora_publica_comparecencia.Text = txt_apellido_2_persona_servidora_publica_comparecencia.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_apellido_2_persona_servidora_publica_comparecencia.SelectionStart = txt_apellido_2_persona_servidora_publica_comparecencia.Text.Length;

            // Desbloquear txt_nombre_2_persona_servidora_publica_comparecencia, cambiar su color de fondo, o borrarlo y deshabilitarlo
            if (!string.IsNullOrEmpty(txt_apellido_2_persona_servidora_publica_comparecencia.Text))
            {
                txt_apellido_3_persona_servidora_publica_comparecencia.Enabled = true;
                txt_apellido_3_persona_servidora_publica_comparecencia.BackColor = Color.Honeydew;
            }
            else
            {
                // Si txt_nombre_2_persona_servidora_publica_juicio_politico está vacío, borrar y deshabilitar txt_nombre_2_personal_apoyo
                txt_apellido_3_persona_servidora_publica_comparecencia.Text = string.Empty;
                txt_apellido_3_persona_servidora_publica_comparecencia.Enabled = false;
                txt_apellido_3_persona_servidora_publica_comparecencia.BackColor = SystemColors.Window; // Restaurar el color predeterminado
            }
        }

        // txt_apellido_3_persona_servidora_publica_comparecencia
        private void txt_apellido_3_persona_servidora_publica_comparecencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_apellido_3_persona_servidora_publica_comparecencia_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_apellido_3_persona_servidora_publica_comparecencia.Text = txt_apellido_3_persona_servidora_publica_comparecencia.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_apellido_3_persona_servidora_publica_comparecencia.SelectionStart = txt_apellido_3_persona_servidora_publica_comparecencia.Text.Length;
        }

        // ----------- Características del cargo de la  persona servidora pública compareciente ---------------------

        private void Cmb_cargo_persona_servidora_publica_comparecencia()
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

                    cmb_cargo_persona_servidora_publica_comparecencia.DataSource = dataTable;
                    cmb_cargo_persona_servidora_publica_comparecencia.DisplayMember = "descripcion";

                    cmb_cargo_persona_servidora_publica_comparecencia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cargo_persona_servidora_publica_comparecencia.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cargo_persona_servidora_publica_comparecencia.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cargo_persona_servidora_publica_comparecencia.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cargo_persona_servidora_publica_comparecencia_Validating(object sender, CancelEventArgs e)
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
        private void cmb_cargo_persona_servidora_publica_comparecencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorComboBox1 = cmb_cargo_persona_servidora_publica_comparecencia.Text.Trim();

            // Desbloquea nombre de la institución
            if (valorComboBox1.Equals("Titular de alguna institución  o unidad administrativa de la Administración Pública Estatal (excluyendo, de ser el caso, a la Procuraduría General de Justicia)", StringComparison.OrdinalIgnoreCase) | (valorComboBox1.Equals("Titular de algún otro órgano constitucional autónomo de la entidad federativa (excluyendo al organismo público local electoral, al organismo garante de acceso a la información y protección de datos personales, al organismo público de derechos humanos de la entidad federativa, y, de ser el caso, a la Fiscalía General de la entidad federativa)", StringComparison.OrdinalIgnoreCase) | (valorComboBox1.Equals("Titular de alguna institución o unidad administrativa de la Administración Pública del municipio o demarcación territorial", StringComparison.OrdinalIgnoreCase))))
            {
                txt_nombre_institucion_persona_servidora_publica_comparecencia.Enabled = true;
                txt_nombre_institucion_persona_servidora_publica_comparecencia.BackColor = Color.Honeydew;
                txt_nombre_institucion_persona_servidora_publica_comparecencia.Text = "";

            }
            else
            {
                txt_nombre_institucion_persona_servidora_publica_comparecencia.Enabled = false;
                txt_nombre_institucion_persona_servidora_publica_comparecencia.BackColor = Color.LightGray;
                txt_nombre_institucion_persona_servidora_publica_comparecencia.Text = "";

            }
            // Desbloquea AGGEM
            if (valorComboBox1.Equals("Presidente(a) municipal", StringComparison.OrdinalIgnoreCase) | (valorComboBox1.Equals("Regidor(a)", StringComparison.OrdinalIgnoreCase) | (valorComboBox1.Equals("Síndico(a)", StringComparison.OrdinalIgnoreCase) | (valorComboBox1.Equals("Titular de alguna institución o unidad administrativa de la Administración Pública del municipio o demarcación territorial", StringComparison.OrdinalIgnoreCase) | (valorComboBox1.Equals("Otro cargo del ámbito municipal (especifique)", StringComparison.OrdinalIgnoreCase))))))
            {
                cmb_municipio_persona_servidora_publica_comparecencia.Enabled = true;
                cmb_municipio_persona_servidora_publica_comparecencia.BackColor = Color.Honeydew;
                cmb_municipio_persona_servidora_publica_comparecencia.Text = "";

                txt_AGEM_persona_servidora_publica_comparecencia.Enabled = false;
                txt_AGEM_persona_servidora_publica_comparecencia.BackColor = Color.Honeydew;
                txt_AGEM_persona_servidora_publica_comparecencia.Text = "";

            }
            else
            {
                cmb_municipio_persona_servidora_publica_comparecencia.Enabled = false;
                cmb_municipio_persona_servidora_publica_comparecencia.BackColor = Color.LightGray;
                cmb_municipio_persona_servidora_publica_comparecencia.Text = "";

                txt_AGEM_persona_servidora_publica_comparecencia.Enabled = false;
                txt_AGEM_persona_servidora_publica_comparecencia.BackColor = Color.LightGray;
                txt_AGEM_persona_servidora_publica_comparecencia.Text = "";

            }
            // Desbloquea Otro cargo del ambito
            if (valorComboBox1.Equals("Otro cargo del ámbito estatal (especifique)", StringComparison.OrdinalIgnoreCase))
            {
                txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_estatal_especifique.Enabled = true;
                txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_estatal_especifique.BackColor = Color.Honeydew;
                txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_estatal_especifique.Text = "";


            }
            else
            {
                txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_estatal_especifique.Enabled = false;
                txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_estatal_especifique.BackColor = Color.LightGray;
                txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_estatal_especifique.Text = "";


            }

            // Desbloquea "Otro cargo del ámbito municipal (especifique)"
            if (valorComboBox1.Equals("Otro cargo del ámbito municipal (especifique)", StringComparison.OrdinalIgnoreCase))
            {
                txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_municipal_especifique.Enabled = true;
                txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_municipal_especifique.BackColor = Color.Honeydew;
                txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_municipal_especifique.Text = "";


            }
            else
            {
                txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_municipal_especifique.Enabled = false;
                txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_municipal_especifique.BackColor = Color.LightGray;
                txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_municipal_especifique.Text = "";


            }
        }

        private void cmb_municipio_persona_servidora_publica_comparecencia_Validating(object sender, CancelEventArgs e)
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
        private void cmb_municipio_persona_servidora_publica_comparecencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = cmb_municipio_persona_servidora_publica_comparecencia.Text;

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
                            txt_AGEM_persona_servidora_publica_comparecencia.Text = resultado.ToString();
                        }
                        else
                        {
                            txt_AGEM_persona_servidora_publica_comparecencia.Text = ""; // Limpiar el TextBox si no se encontró un ID
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



        // -------------------------- Configuración de TXT para mayuculas y Numeros 

        // txt_nombre_institucion_persona_servidora_publica_comparecencia
        private void txt_nombre_institucion_persona_servidora_publica_comparecencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_nombre_institucion_persona_servidora_publica_comparecencia_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_nombre_institucion_persona_servidora_publica_comparecencia.Text = txt_nombre_institucion_persona_servidora_publica_comparecencia.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_nombre_institucion_persona_servidora_publica_comparecencia.SelectionStart = txt_nombre_institucion_persona_servidora_publica_comparecencia.Text.Length;
        }

        // txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_estatal_especifique
        private void txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_estatal_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_estatal_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_estatal_especifique.Text = txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_estatal_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_estatal_especifique.SelectionStart = txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_estatal_especifique.Text.Length;
        }

        // txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_municipal_especifique
        private void txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_municipal_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_municipal_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_municipal_especifique.Text = txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_municipal_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_municipal_especifique.SelectionStart = txt_otro_cargo_persona_servidora_publica_comparecencia_ambito_municipal_especifique.Text.Length;
        }

    }
}
