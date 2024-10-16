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
        }




        // -------------------------- Configuración de TXT para mayuculas y Numeros 




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
