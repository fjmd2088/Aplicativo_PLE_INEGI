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

        // METODOS GENERALES
        private void met_no_permite_acentos(KeyPressEventArgs e)
        {
            // Lista de caracteres permitidos sin acentos
            string allowedCharacters = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789 ";

            // Verificar si el carácter presionado no está en la lista permitida
            if (!allowedCharacters.Contains(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Cancelar la entrada del carácter
            }
        }


        // CARACTERISTICAS DEMOGRÁFICAS-------------------------------------------------------------------------------------------------------------

        private void txt_nombre_1_personal_apoyo_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_nombre_1_personal_apoyo_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_nombre_1_personal_apoyo.Text = txt_nombre_1_personal_apoyo.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_nombre_1_personal_apoyo.SelectionStart = txt_nombre_1_personal_apoyo.Text.Length;

            // Desbloquear txt_nombre_2_personal_apoyo, cambiar su color de fondo, o borrarlo y deshabilitarlo
            if (!string.IsNullOrEmpty(txt_nombre_1_personal_apoyo.Text))
            {
                txt_nombre_2_personal_apoyo.Enabled = true;
                txt_nombre_2_personal_apoyo.BackColor = Color.Honeydew;
            }
            else
            {
                // Si txt_nombre_1_personal_apoyo está vacío, borrar y deshabilitar txt_nombre_2_personal_apoyo
                txt_nombre_2_personal_apoyo.Text = string.Empty;
                txt_nombre_2_personal_apoyo.Enabled = false;
                txt_nombre_2_personal_apoyo.BackColor = SystemColors.Window; // Restaurar el color predeterminado
            }

            // Lógica para generar ID a partir de nombres, apellidos, fecha de nacimiento y sexo
            string primerosDigitos = "";

            // Logica para generar ID de nombres
            if (!string.IsNullOrEmpty(txt_nombre_1_personal_apoyo.Text))
            {
                // Si el segundo campo también tiene texto, extraer la primera letra de ambos
                if (!string.IsNullOrEmpty(txt_nombre_2_personal_apoyo.Text))
                {
                    string primeraLetraNombre1 = txt_nombre_1_personal_apoyo.Text.Substring(0, 1);
                    string primeraLetraNombre2 = txt_nombre_2_personal_apoyo.Text.Substring(0, 1);

                    // Combinar la primera letra de ambos campos
                    primerosDigitos += primeraLetraNombre1 + primeraLetraNombre2;
                }
                else
                {
                    // Si el segundo campo está vacío, extraer las primeras dos letras del primero si tiene al menos dos caracteres
                    if (txt_nombre_1_personal_apoyo.Text.Length >= 2)
                    {
                        primerosDigitos += txt_nombre_1_personal_apoyo.Text.Substring(0, 2);
                    }
                    else
                    {
                        // Si tiene menos de dos caracteres, usar el texto completo
                        primerosDigitos += txt_nombre_1_personal_apoyo.Text;
                    }
                }
            }

            // Lógica para generar ID de apellidos
            if (!string.IsNullOrEmpty(txt_apellido_1_personal_apoyo.Text))
            {
                // Si el segundo campo también tiene texto, extraer la primera letra de ambos
                if (!string.IsNullOrEmpty(txt_apellido_2_personal_apoyo.Text))
                {
                    string primeraLetraApellido1 = txt_apellido_1_personal_apoyo.Text.Substring(0, 1);
                    string primeraLetraApellido2 = txt_apellido_2_personal_apoyo.Text.Substring(0, 1);

                    // Combinar la primera letra de ambos campos
                    primerosDigitos += primeraLetraApellido1 + primeraLetraApellido2;
                }
                else
                {
                    // Si el segundo campo está vacío, extraer las primeras dos letras del primero si tiene al menos dos caracteres
                    if (txt_apellido_1_personal_apoyo.Text.Length >= 2)
                    {
                        primerosDigitos += txt_apellido_1_personal_apoyo.Text.Substring(0, 2);
                    }
                    else
                    {
                        // Si tiene menos de dos caracteres, usar el texto completo
                        primerosDigitos += txt_apellido_1_personal_apoyo.Text;
                    }
                }
            }

            // Extraer los últimos 4 dígitos del año de nacimiento
            if (dtp_fecha_nacimiento_personal_apoyo.Value != null)
            {
                int añoNacimiento = dtp_fecha_nacimiento_personal_apoyo.Value.Year; // Obtener el año completo
                primerosDigitos += añoNacimiento.ToString(); // Añadir el año completo (4 dígitos)
            }

            // Extraer la primera letra del sexo
            if (!string.IsNullOrEmpty(cmb_sexo_personal_apoyo.Text))
            {
                primerosDigitos += cmb_sexo_personal_apoyo.Text.Substring(0, 1); // Obtener la primera letra
            }

            // Asignar el resultado a txt_ID_personal_apoyo
            txt_ID_personal_apoyo.Text = primerosDigitos;

            // Limpiar el campo ID si no se ingresa ningún nombre o apellido
            if (string.IsNullOrEmpty(txt_nombre_1_personal_apoyo.Text) && string.IsNullOrEmpty(txt_apellido_1_personal_apoyo.Text))
            {
                txt_ID_personal_apoyo.Text = "";
            }
        }

        // txt_nombre_2_personal_apoyo
        private void txt_nombre_2_personal_apoyo_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_nombre_2_personal_apoyo_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_nombre_2_personal_apoyo.Text = txt_nombre_2_personal_apoyo.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_nombre_2_personal_apoyo.SelectionStart = txt_nombre_2_personal_apoyo.Text.Length;

            // Desbloquear txt_nombre_3_personal_apoyo, cambiar su color de fondo, o borrarlo y deshabilitarlo
            if (!string.IsNullOrEmpty(txt_nombre_2_personal_apoyo.Text))
            {
                txt_nombre_3_personal_apoyo.Enabled = true;
                txt_nombre_3_personal_apoyo.BackColor = Color.Honeydew;
            }
            else
            {
                // Si txt_nombre_2_personal_apoyo está vacío, borrar y deshabilitar txt_nombre_3_personal_apoyo
                txt_nombre_3_personal_apoyo.Text = string.Empty;
                txt_nombre_3_personal_apoyo.Enabled = false;
                txt_nombre_3_personal_apoyo.BackColor = SystemColors.Window; // Restaurar el color predeterminado
            }
            // Lógica para generar ID a partir de nombres, apellidos, fecha de nacimiento y sexo
            string primerosDigitos = "";

            // Logica para generar ID de nombres
            if (!string.IsNullOrEmpty(txt_nombre_1_personal_apoyo.Text))
            {
                // Si el segundo campo también tiene texto, extraer la primera letra de ambos
                if (!string.IsNullOrEmpty(txt_nombre_2_personal_apoyo.Text))
                {
                    string primeraLetraNombre1 = txt_nombre_1_personal_apoyo.Text.Substring(0, 1);
                    string primeraLetraNombre2 = txt_nombre_2_personal_apoyo.Text.Substring(0, 1);

                    // Combinar la primera letra de ambos campos
                    primerosDigitos += primeraLetraNombre1 + primeraLetraNombre2;
                }
                else
                {
                    // Si el segundo campo está vacío, extraer las primeras dos letras del primero si tiene al menos dos caracteres
                    if (txt_nombre_1_personal_apoyo.Text.Length >= 2)
                    {
                        primerosDigitos += txt_nombre_1_personal_apoyo.Text.Substring(0, 2);
                    }
                    else
                    {
                        // Si tiene menos de dos caracteres, usar el texto completo
                        primerosDigitos += txt_nombre_1_personal_apoyo.Text;
                    }
                }
            }

            // Lógica para generar ID de apellidos
            if (!string.IsNullOrEmpty(txt_apellido_1_personal_apoyo.Text))
            {
                // Si el segundo campo también tiene texto, extraer la primera letra de ambos
                if (!string.IsNullOrEmpty(txt_apellido_2_personal_apoyo.Text))
                {
                    string primeraLetraApellido1 = txt_apellido_1_personal_apoyo.Text.Substring(0, 1);
                    string primeraLetraApellido2 = txt_apellido_2_personal_apoyo.Text.Substring(0, 1);

                    // Combinar la primera letra de ambos campos
                    primerosDigitos += primeraLetraApellido1 + primeraLetraApellido2;
                }
                else
                {
                    // Si el segundo campo está vacío, extraer las primeras dos letras del primero si tiene al menos dos caracteres
                    if (txt_apellido_1_personal_apoyo.Text.Length >= 2)
                    {
                        primerosDigitos += txt_apellido_1_personal_apoyo.Text.Substring(0, 2);
                    }
                    else
                    {
                        // Si tiene menos de dos caracteres, usar el texto completo
                        primerosDigitos += txt_apellido_1_personal_apoyo.Text;
                    }
                }
            }

            // Extraer los últimos 4 dígitos del año de nacimiento
            if (dtp_fecha_nacimiento_personal_apoyo.Value != null)
            {
                int añoNacimiento = dtp_fecha_nacimiento_personal_apoyo.Value.Year; // Obtener el año completo
                primerosDigitos += añoNacimiento.ToString(); // Añadir el año completo (4 dígitos)
            }

            // Extraer la primera letra del sexo
            if (!string.IsNullOrEmpty(cmb_sexo_personal_apoyo.Text))
            {
                primerosDigitos += cmb_sexo_personal_apoyo.Text.Substring(0, 1); // Obtener la primera letra
            }

            // Asignar el resultado a txt_ID_personal_apoyo
            txt_ID_personal_apoyo.Text = primerosDigitos;

            // Limpiar el campo ID si no se ingresa ningún nombre o apellido
            if (string.IsNullOrEmpty(txt_nombre_1_personal_apoyo.Text) && string.IsNullOrEmpty(txt_apellido_1_personal_apoyo.Text))
            {
                txt_ID_personal_apoyo.Text = "";
            }

        }

        // txt_nombre_3_personal_apoyo
        private void txt_nombre_3_personal_apoyo_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_nombre_3_personal_apoyo_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_nombre_3_personal_apoyo.Text = txt_nombre_3_personal_apoyo.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_nombre_3_personal_apoyo.SelectionStart = txt_nombre_3_personal_apoyo.Text.Length;
        }

        // txt_apellido_1_personal_apoyo
        private void txt_apellido_1_personal_apoyo_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_apellido_1_personal_apoyo_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_apellido_1_personal_apoyo.Text = txt_apellido_1_personal_apoyo.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_apellido_1_personal_apoyo.SelectionStart = txt_apellido_1_personal_apoyo.Text.Length;

            // Desbloquear txt_apellido_2_personal_apoyo, cambiar su color de fondo, o borrarlo y deshabilitarlo
            if (!string.IsNullOrEmpty(txt_apellido_1_personal_apoyo.Text))
            {
                txt_apellido_2_personal_apoyo.Enabled = true;
                txt_apellido_2_personal_apoyo.BackColor = Color.Honeydew;
            }
            else
            {
                // Si txt_apellido_1_personal_apoyo está vacío, borrar y deshabilitar txt_apellido_2_personal_apoyo
                txt_apellido_2_personal_apoyo.Text = string.Empty;
                txt_apellido_2_personal_apoyo.Enabled = false;
                txt_apellido_2_personal_apoyo.BackColor = SystemColors.Window; // Restaurar el color predeterminado
            }

            // Lógica para generar ID a partir de nombres, apellidos, fecha de nacimiento y sexo
            string primerosDigitos = "";

            // Logica para generar ID de nombres
            if (!string.IsNullOrEmpty(txt_nombre_1_personal_apoyo.Text))
            {
                // Si el segundo campo también tiene texto, extraer la primera letra de ambos
                if (!string.IsNullOrEmpty(txt_nombre_2_personal_apoyo.Text))
                {
                    string primeraLetraNombre1 = txt_nombre_1_personal_apoyo.Text.Substring(0, 1);
                    string primeraLetraNombre2 = txt_nombre_2_personal_apoyo.Text.Substring(0, 1);

                    // Combinar la primera letra de ambos campos
                    primerosDigitos += primeraLetraNombre1 + primeraLetraNombre2;
                }
                else
                {
                    // Si el segundo campo está vacío, extraer las primeras dos letras del primero si tiene al menos dos caracteres
                    if (txt_nombre_1_personal_apoyo.Text.Length >= 2)
                    {
                        primerosDigitos += txt_nombre_1_personal_apoyo.Text.Substring(0, 2);
                    }
                    else
                    {
                        // Si tiene menos de dos caracteres, usar el texto completo
                        primerosDigitos += txt_nombre_1_personal_apoyo.Text;
                    }
                }
            }

            // Lógica para generar ID de apellidos
            if (!string.IsNullOrEmpty(txt_apellido_1_personal_apoyo.Text))
            {
                // Si el segundo campo también tiene texto, extraer la primera letra de ambos
                if (!string.IsNullOrEmpty(txt_apellido_2_personal_apoyo.Text))
                {
                    string primeraLetraApellido1 = txt_apellido_1_personal_apoyo.Text.Substring(0, 1);
                    string primeraLetraApellido2 = txt_apellido_2_personal_apoyo.Text.Substring(0, 1);

                    // Combinar la primera letra de ambos campos
                    primerosDigitos += primeraLetraApellido1 + primeraLetraApellido2;
                }
                else
                {
                    // Si el segundo campo está vacío, extraer las primeras dos letras del primero si tiene al menos dos caracteres
                    if (txt_apellido_1_personal_apoyo.Text.Length >= 2)
                    {
                        primerosDigitos += txt_apellido_1_personal_apoyo.Text.Substring(0, 2);
                    }
                    else
                    {
                        // Si tiene menos de dos caracteres, usar el texto completo
                        primerosDigitos += txt_apellido_1_personal_apoyo.Text;
                    }
                }
            }

            // Extraer los últimos 4 dígitos del año de nacimiento
            if (dtp_fecha_nacimiento_personal_apoyo.Value != null)
            {
                int añoNacimiento = dtp_fecha_nacimiento_personal_apoyo.Value.Year; // Obtener el año completo
                primerosDigitos += añoNacimiento.ToString(); // Añadir el año completo (4 dígitos)
            }

            // Extraer la primera letra del sexo
            if (!string.IsNullOrEmpty(cmb_sexo_personal_apoyo.Text))
            {
                primerosDigitos += cmb_sexo_personal_apoyo.Text.Substring(0, 1); // Obtener la primera letra
            }

            // Asignar el resultado a txt_ID_personal_apoyo
            txt_ID_personal_apoyo.Text = primerosDigitos;

            // Limpiar el campo ID si no se ingresa ningún nombre o apellido
            if (string.IsNullOrEmpty(txt_nombre_1_personal_apoyo.Text) && string.IsNullOrEmpty(txt_apellido_1_personal_apoyo.Text))
            {
                txt_ID_personal_apoyo.Text = "";
            }

        }

        // txt_apellido_2_personal_apoyo
        private void txt_apellido_2_personal_apoyo_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_apellido_2_personal_apoyo_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_apellido_2_personal_apoyo.Text = txt_apellido_2_personal_apoyo.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_apellido_2_personal_apoyo.SelectionStart = txt_apellido_2_personal_apoyo.Text.Length;

            // Desbloquear txt_apellido_3_personal_apoyo, cambiar su color de fondo, o borrarlo y deshabilitarlo
            if (!string.IsNullOrEmpty(txt_apellido_2_personal_apoyo.Text))
            {
                txt_apellido_3_personal_apoyo.Enabled = true;
                txt_apellido_3_personal_apoyo.BackColor = Color.Honeydew;
            }
            else
            {
                // Si txt_apellido_2_personal_apoyo está vacío, borrar y deshabilitar txt_apellido_3_personal_apoyo
                txt_apellido_3_personal_apoyo.Text = string.Empty;
                txt_apellido_3_personal_apoyo.Enabled = false;
                txt_apellido_3_personal_apoyo.BackColor = SystemColors.Window; // Restaurar el color predeterminado
            }
            // Lógica para generar ID a partir de nombres, apellidos, fecha de nacimiento y sexo
            string primerosDigitos = "";

            // Logica para generar ID de nombres
            if (!string.IsNullOrEmpty(txt_nombre_1_personal_apoyo.Text))
            {
                // Si el segundo campo también tiene texto, extraer la primera letra de ambos
                if (!string.IsNullOrEmpty(txt_nombre_2_personal_apoyo.Text))
                {
                    string primeraLetraNombre1 = txt_nombre_1_personal_apoyo.Text.Substring(0, 1);
                    string primeraLetraNombre2 = txt_nombre_2_personal_apoyo.Text.Substring(0, 1);

                    // Combinar la primera letra de ambos campos
                    primerosDigitos += primeraLetraNombre1 + primeraLetraNombre2;
                }
                else
                {
                    // Si el segundo campo está vacío, extraer las primeras dos letras del primero si tiene al menos dos caracteres
                    if (txt_nombre_1_personal_apoyo.Text.Length >= 2)
                    {
                        primerosDigitos += txt_nombre_1_personal_apoyo.Text.Substring(0, 2);
                    }
                    else
                    {
                        // Si tiene menos de dos caracteres, usar el texto completo
                        primerosDigitos += txt_nombre_1_personal_apoyo.Text;
                    }
                }
            }

            // Lógica para generar ID de apellidos
            if (!string.IsNullOrEmpty(txt_apellido_1_personal_apoyo.Text))
            {
                // Si el segundo campo también tiene texto, extraer la primera letra de ambos
                if (!string.IsNullOrEmpty(txt_apellido_2_personal_apoyo.Text))
                {
                    string primeraLetraApellido1 = txt_apellido_1_personal_apoyo.Text.Substring(0, 1);
                    string primeraLetraApellido2 = txt_apellido_2_personal_apoyo.Text.Substring(0, 1);

                    // Combinar la primera letra de ambos campos
                    primerosDigitos += primeraLetraApellido1 + primeraLetraApellido2;
                }
                else
                {
                    // Si el segundo campo está vacío, extraer las primeras dos letras del primero si tiene al menos dos caracteres
                    if (txt_apellido_1_personal_apoyo.Text.Length >= 2)
                    {
                        primerosDigitos += txt_apellido_1_personal_apoyo.Text.Substring(0, 2);
                    }
                    else
                    {
                        // Si tiene menos de dos caracteres, usar el texto completo
                        primerosDigitos += txt_apellido_1_personal_apoyo.Text;
                    }
                }
            }

            // Extraer los últimos 4 dígitos del año de nacimiento
            if (dtp_fecha_nacimiento_personal_apoyo.Value != null)
            {
                int añoNacimiento = dtp_fecha_nacimiento_personal_apoyo.Value.Year; // Obtener el año completo
                primerosDigitos += añoNacimiento.ToString(); // Añadir el año completo (4 dígitos)
            }

            // Extraer la primera letra del sexo
            if (!string.IsNullOrEmpty(cmb_sexo_personal_apoyo.Text))
            {
                primerosDigitos += cmb_sexo_personal_apoyo.Text.Substring(0, 1); // Obtener la primera letra
            }

            // Asignar el resultado a txt_ID_personal_apoyo
            txt_ID_personal_apoyo.Text = primerosDigitos;

            // Limpiar el campo ID si no se ingresa ningún nombre o apellido
            if (string.IsNullOrEmpty(txt_nombre_1_personal_apoyo.Text) && string.IsNullOrEmpty(txt_apellido_1_personal_apoyo.Text))
            {
                txt_ID_personal_apoyo.Text = "";
            }
        }

        // txt_apellido_3_personal_apoyo
        private void txt_apellido_3_personal_apoyo_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_apellido_3_personal_apoyo_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_apellido_3_personal_apoyo.Text = txt_apellido_3_personal_apoyo.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_apellido_3_personal_apoyo.SelectionStart = txt_apellido_3_personal_apoyo.Text.Length;
        }

        // cmb_sexo_personal_apoyo
        private void cmb_Sexo_personal_apoyo()
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

                    cmb_sexo_personal_apoyo.DataSource = dataTable;
                    cmb_sexo_personal_apoyo.DisplayMember = "descripcion";

                    cmb_sexo_personal_apoyo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_sexo_personal_apoyo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_sexo_personal_apoyo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_sexo_personal_apoyo.SelectedIndex = -1; // Aquí se establece como vacío
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al llenar el ComboBox cmb_Sexo_personal_apoyo: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }

            }
        }
        private void cmb_sexo_personal_apoyo_Validating(object sender, CancelEventArgs e)
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
        private void cmb_sexo_personal_apoyo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lógica para generar ID a partir de nombres, apellidos, fecha de nacimiento y sexo
            string primerosDigitos = "";

            // Logica para generar ID de nombres
            if (!string.IsNullOrEmpty(txt_nombre_1_personal_apoyo.Text))
            {
                // Si el segundo campo también tiene texto, extraer la primera letra de ambos
                if (!string.IsNullOrEmpty(txt_nombre_2_personal_apoyo.Text))
                {
                    string primeraLetraNombre1 = txt_nombre_1_personal_apoyo.Text.Substring(0, 1);
                    string primeraLetraNombre2 = txt_nombre_2_personal_apoyo.Text.Substring(0, 1);

                    // Combinar la primera letra de ambos campos
                    primerosDigitos += primeraLetraNombre1 + primeraLetraNombre2;
                }
                else
                {
                    // Si el segundo campo está vacío, extraer las primeras dos letras del primero si tiene al menos dos caracteres
                    if (txt_nombre_1_personal_apoyo.Text.Length >= 2)
                    {
                        primerosDigitos += txt_nombre_1_personal_apoyo.Text.Substring(0, 2);
                    }
                    else
                    {
                        // Si tiene menos de dos caracteres, usar el texto completo
                        primerosDigitos += txt_nombre_1_personal_apoyo.Text;
                    }
                }
            }

            // Lógica para generar ID de apellidos
            if (!string.IsNullOrEmpty(txt_apellido_1_personal_apoyo.Text))
            {
                // Si el segundo campo también tiene texto, extraer la primera letra de ambos
                if (!string.IsNullOrEmpty(txt_apellido_2_personal_apoyo.Text))
                {
                    string primeraLetraApellido1 = txt_apellido_1_personal_apoyo.Text.Substring(0, 1);
                    string primeraLetraApellido2 = txt_apellido_2_personal_apoyo.Text.Substring(0, 1);

                    // Combinar la primera letra de ambos campos
                    primerosDigitos += primeraLetraApellido1 + primeraLetraApellido2;
                }
                else
                {
                    // Si el segundo campo está vacío, extraer las primeras dos letras del primero si tiene al menos dos caracteres
                    if (txt_apellido_1_personal_apoyo.Text.Length >= 2)
                    {
                        primerosDigitos += txt_apellido_1_personal_apoyo.Text.Substring(0, 2);
                    }
                    else
                    {
                        // Si tiene menos de dos caracteres, usar el texto completo
                        primerosDigitos += txt_apellido_1_personal_apoyo.Text;
                    }
                }
            }

            // Extraer los últimos 4 dígitos del año de nacimiento
            if (dtp_fecha_nacimiento_personal_apoyo.Value != null)
            {
                int añoNacimiento = dtp_fecha_nacimiento_personal_apoyo.Value.Year; // Obtener el año completo
                primerosDigitos += añoNacimiento.ToString(); // Añadir el año completo (4 dígitos)
            }

            // Extraer la primera letra del sexo
            if (!string.IsNullOrEmpty(cmb_sexo_personal_apoyo.Text))
            {
                primerosDigitos += cmb_sexo_personal_apoyo.Text.Substring(0, 1); // Obtener la primera letra
            }

            // Asignar el resultado a txt_ID_personal_apoyo
            txt_ID_personal_apoyo.Text = primerosDigitos;

            // Limpiar el campo ID si no se ingresa ningún nombre o apellido
            if (string.IsNullOrEmpty(txt_nombre_1_personal_apoyo.Text) && string.IsNullOrEmpty(txt_apellido_1_personal_apoyo.Text))
            {
                txt_ID_personal_apoyo.Text = "";
            }
        }

        // LENGUA ------------------------------------------------------------------------------------------------------------------------------------

        private void cmb_Cond_lengua_ind_personal_apoyo()
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

                    cmb_cond_lengua_ind_personal_apoyo.DataSource = dataTable;
                    cmb_cond_lengua_ind_personal_apoyo.DisplayMember = "descripcion";

                    cmb_cond_lengua_ind_personal_apoyo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_lengua_ind_personal_apoyo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_lengua_ind_personal_apoyo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_lengua_ind_personal_apoyo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_lengua_ind_personal_apoyo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_lengua_ind_personal_apoyo.Text.ToString();

            if (valorComboBox1 == "Si")
            {
                cmb_lengua_ind_1_personal_apoyo.Enabled = true; cmb_lengua_ind_1_personal_apoyo.BackColor = Color.Honeydew;
                btn_agr_leng_ind.Enabled = true; btn_elim_leng_ind.Enabled = true;
                dgv_leng_ind.BackgroundColor = Color.Honeydew;
                cmb_lengua_ind_1_personal_apoyo.Focus();
            }
            else
            {
                cmb_lengua_ind_1_personal_apoyo.Enabled = false; cmb_lengua_ind_1_personal_apoyo.BackColor = Color.LightGray;
                dgv_leng_ind.Rows.Clear(); dgv_leng_ind.BackgroundColor = Color.LightGray;
                btn_agr_leng_ind.Enabled = false; btn_elim_leng_ind.Enabled = false;

                cmb_lengua_ind_1_personal_apoyo.Text = "";
            }
        }
        private void cmb_cond_lengua_ind_personal_apoyo_Validating(object sender, CancelEventArgs e)
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

        // cmb_lengua_ind_1_personal_apoyo

        private void CMB_LENGUA_IND_1_PERSONAL_APOYO()
        {

            try
            {
                // abrir la conexion
                //       conexion.Open();

                // comando de sql
                string query = "select descripcion from TC_LENGUA_INDIGENA";
                SQLiteCommand cmd = new SQLiteCommand(query, _connection);

                // Utilizar un DataReader para obtener los datos
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, _connection);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                cmb_lengua_ind_1_personal_apoyo.DataSource = dataTable;
                cmb_lengua_ind_1_personal_apoyo.DisplayMember = "descripcion";

                cmb_lengua_ind_1_personal_apoyo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmb_lengua_ind_1_personal_apoyo.AutoCompleteSource = AutoCompleteSource.ListItems;

                cmb_lengua_ind_1_personal_apoyo.DropDownStyle = ComboBoxStyle.DropDown;
                cmb_lengua_ind_1_personal_apoyo.SelectedIndex = -1; // Aquí se establece como vacío
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar el ComboBox: " + ex.Message);
            }

        }
        private void cmb_lengua_ind_1_personal_apoyo_Validating(object sender, CancelEventArgs e)
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

        // Botones de Tabla

        private void btn_agr_leng_ind_MouseClick(object sender, MouseEventArgs e)
        {
            // se obtienen los valores
            string lengua_Ap = cmb_lengua_ind_1_personal_apoyo.Text.Trim();


            if (string.IsNullOrWhiteSpace(cmb_lengua_ind_1_personal_apoyo.Text))
            {
                MessageBox.Show("Revisar datos vacios");
            }
            else
            {
                // Agregar una nueva fila al DataGridView
                bool respuesta = IsDuplicateRecord_Apoyo(cmb_lengua_ind_1_personal_apoyo.Text.ToString());

                if (respuesta == true)
                {
                    MessageBox.Show("Dato duplicado");
                    cmb_lengua_ind_1_personal_apoyo.Text = "";
                }
                else
                {
                    // Agregar una nueva fila al DataGridView
                    dgv_leng_ind.Rows.Add(lengua_Ap);
                    cmb_lengua_ind_1_personal_apoyo.Text = "";
                }
            }
        }
        private void btn_elim_leng_ind_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgv_leng_ind.SelectedRows.Count > 0)
            {
                dgv_leng_ind.Rows.RemoveAt(dgv_leng_ind.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }
        private bool IsDuplicateRecord_Apoyo(string variable_cmb)
        {
            foreach (DataGridViewRow row in dgv_leng_ind.Rows)
            {
                if (row.IsNewRow) continue; // Skip the new row placeholder

                string existingId = row.Cells["Lengua_indigena_PA"].Value.ToString();

                if (existingId == variable_cmb)
                {
                    return true;
                }
            }
            return false;
        }

        // DISCAPACIDAD -----------------------------------------------------------------------------------------------------------------------------

        private void cmb_Cond_discapacidad_personal_apoyo()
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

                    cmb_cond_discapacidad_personal_apoyo.DataSource = dataTable;
                    cmb_cond_discapacidad_personal_apoyo.DisplayMember = "descripcion";

                    cmb_cond_discapacidad_personal_apoyo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_discapacidad_personal_apoyo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_discapacidad_personal_apoyo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_discapacidad_personal_apoyo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_discapacidad_personal_apoyo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_discapacidad_personal_apoyo.Text.ToString();

            if (valorComboBox1 == "Si")
            {
                cmb_tipo_discapacidad_1_personal_apoyo.Enabled = true; cmb_tipo_discapacidad_1_personal_apoyo.BackColor = Color.Honeydew;
                btn_agreg_discap.Enabled = true; btn_borr_discap.Enabled = true;
                dgv_tip_discap.BackgroundColor = Color.Honeydew;
                cmb_tipo_discapacidad_1_personal_apoyo.Focus();
            }
            else
            {
                cmb_tipo_discapacidad_1_personal_apoyo.Enabled = false; cmb_tipo_discapacidad_1_personal_apoyo.BackColor = Color.LightGray;
                dgv_tip_discap.Rows.Clear(); dgv_tip_discap.BackgroundColor = Color.LightGray;
                btn_agreg_discap.Enabled = false; btn_borr_discap.Enabled = false;

                cmb_tipo_discapacidad_1_personal_apoyo.Text = "";
            }
        }
        private void cmb_cond_discapacidad_personal_apoyo_Validating(object sender, CancelEventArgs e)
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

        // cmb_tipo_discapacidad_1_personal_apoyo

        private void CMB_tipo_discapacidad_1_personal_apoyo()
        {
            try
            {
                // abrir la conexion
                //       conexion.Open();

                // comando de sql
                string query = "select descripcion from TC_TIPO_DISCAPACIDAD";
                SQLiteCommand cmd = new SQLiteCommand(query, _connection);

                // Utilizar un DataReader para obtener los datos
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, _connection);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                cmb_tipo_discapacidad_1_personal_apoyo.DataSource = dataTable;
                cmb_tipo_discapacidad_1_personal_apoyo.DisplayMember = "descripcion";

                cmb_tipo_discapacidad_1_personal_apoyo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmb_tipo_discapacidad_1_personal_apoyo.AutoCompleteSource = AutoCompleteSource.ListItems;

                cmb_tipo_discapacidad_1_personal_apoyo.DropDownStyle = ComboBoxStyle.DropDown;
                cmb_tipo_discapacidad_1_personal_apoyo.SelectedIndex = -1; // Aquí se establece como vacío
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar el ComboBox: " + ex.Message);
            }
        }
        private void cmb_tipo_discapacidad_1_personal_apoyo_Validating(object sender, CancelEventArgs e)
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

        // Botones de Tabla

        private void btn_agreg_discap_Click(object sender, EventArgs e)
        {
            // se obtienen los valores
            string Discap_Ap = cmb_tipo_discapacidad_1_personal_apoyo.Text.Trim();


            if (string.IsNullOrWhiteSpace(cmb_tipo_discapacidad_1_personal_apoyo.Text))
            {
                MessageBox.Show("Revisar datos vacios");
            }
            else
            {
                // Agregar una nueva fila al DataGridView
                bool respuesta = IsDuplicateRecord_ApoyoLeng(cmb_tipo_discapacidad_1_personal_apoyo.Text.ToString());

                if (respuesta == true)
                {
                    MessageBox.Show("Dato duplicado");
                    cmb_tipo_discapacidad_1_personal_apoyo.Text = "";
                }
                else
                {
                    // Agregar una nueva fila al DataGridView
                    dgv_tip_discap.Rows.Add(Discap_Ap);
                    cmb_tipo_discapacidad_1_personal_apoyo.Text = "";
                }
            }
        }
        private void btn_borr_discap_Click(object sender, EventArgs e)
        {
            if (dgv_tip_discap.SelectedRows.Count > 0)
            {
                dgv_tip_discap.Rows.RemoveAt(dgv_tip_discap.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }
        private bool IsDuplicateRecord_ApoyoLeng(string variable_cmb)
        {
            foreach (DataGridViewRow row in dgv_tip_discap.Rows)
            {
                if (row.IsNewRow) continue; // Skip the new row placeholder

                string existingId = row.Cells["Tabla_discapacidad"].Value.ToString();

                if (existingId == variable_cmb)
                {
                    return true;
                }
            }
            return false;
        }

        // PUEBLO INDIGENA ---------------------------------------------------------------------------------------------------------------------------

        private void cmb_Cond_pueblo_ind_personal_apoyo()
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

                    cmb_cond_pueblo_ind_personal_apoyo.DataSource = dataTable;
                    cmb_cond_pueblo_ind_personal_apoyo.DisplayMember = "descripcion";

                    cmb_cond_pueblo_ind_personal_apoyo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_pueblo_ind_personal_apoyo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_pueblo_ind_personal_apoyo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_pueblo_ind_personal_apoyo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_pueblo_ind_personal_apoyo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_pueblo_ind_personal_apoyo.Text.ToString();

            if (valorComboBox1 == "Si")
            {
                cmb_pueblo_ind_pertenencia_personal_apoyo.Enabled = true; cmb_pueblo_ind_pertenencia_personal_apoyo.BackColor = Color.Honeydew;

            }
            else
            {
                cmb_pueblo_ind_pertenencia_personal_apoyo.Enabled = false; cmb_pueblo_ind_pertenencia_personal_apoyo.BackColor = Color.LightGray;

                cmb_pueblo_ind_pertenencia_personal_apoyo.Text = "";
            }
        }
        private void cmb_cond_pueblo_ind_personal_apoyo_Validating(object sender, CancelEventArgs e)
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

        // cmb_pueblo_ind_pertenencia_personal_apoyo

        private void cmb_Pueblo_ind_pertenencia_personal_apoyo()
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

                    cmb_pueblo_ind_pertenencia_personal_apoyo.DataSource = dataTable;
                    cmb_pueblo_ind_pertenencia_personal_apoyo.DisplayMember = "descripcion";

                    cmb_pueblo_ind_pertenencia_personal_apoyo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_pueblo_ind_pertenencia_personal_apoyo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_pueblo_ind_pertenencia_personal_apoyo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_pueblo_ind_pertenencia_personal_apoyo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_pueblo_ind_pertenencia_personal_apoyo_Validating(object sender, CancelEventArgs e)
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

        // REGIMEN DE CONTRATACIÓN --------------------------------------------------------------------------------------------------------------------

        private void cmb_Regimen_ontratacion_personal_apoyo()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_REGIMEN_CONTRATACION";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_regimen_contratacion_personal_apoyo.DataSource = dataTable;
                    cmb_regimen_contratacion_personal_apoyo.DisplayMember = "descripcion";

                    cmb_regimen_contratacion_personal_apoyo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_regimen_contratacion_personal_apoyo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_regimen_contratacion_personal_apoyo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_regimen_contratacion_personal_apoyo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_regimen_contratacion_personal_apoyo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_regimen_contratacion_personal_apoyo.Text.ToString();

            if (valorComboBox1 == "Otro régimen de contratación (especifique)")
            {
                txt_otro_regimen_contratacion_personal_apoyo_especifique.Enabled = true; txt_otro_regimen_contratacion_personal_apoyo_especifique.BackColor = Color.Honeydew;
                txt_otro_regimen_contratacion_personal_apoyo_especifique.Focus();
            }
            else
            {
                txt_otro_regimen_contratacion_personal_apoyo_especifique.Enabled = false; txt_otro_regimen_contratacion_personal_apoyo_especifique.BackColor = Color.LightGray;
                txt_otro_regimen_contratacion_personal_apoyo_especifique.Text = "";
            }
        }
        private void cmb_regimen_contratacion_personal_apoyo_Validating(object sender, CancelEventArgs e)
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

        // txt_otro_regimen_contratacion_personal_apoyo_especifique
        private void txt_otro_regimen_contratacion_personal_apoyo_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_otro_regimen_contratacion_personal_apoyo_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_otro_regimen_contratacion_personal_apoyo_especifique.Text = txt_otro_regimen_contratacion_personal_apoyo_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_otro_regimen_contratacion_personal_apoyo_especifique.SelectionStart = txt_otro_regimen_contratacion_personal_apoyo_especifique.Text.Length;
        }

        // SEGURIDAD SOCIAL ---------------------------------------------------------------------------------------------------------------------------

        private void cmb_Institucion_seguridad_social_personal_apoyo()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_INST_SEG_SOCIAL";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_institucion_seguridad_social_personal_apoyo.DataSource = dataTable;
                    cmb_institucion_seguridad_social_personal_apoyo.DisplayMember = "descripcion";

                    cmb_institucion_seguridad_social_personal_apoyo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_institucion_seguridad_social_personal_apoyo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_institucion_seguridad_social_personal_apoyo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_institucion_seguridad_social_personal_apoyo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_institucion_seguridad_social_personal_apoyo_Validating(object sender, CancelEventArgs e)
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

        // txt_ingreso_mensual_personal_apoyo

        private void txt_ingreso_mensual_personal_apoyo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }

        // ADSCRIPCIÓN LABORAL ------------------------------------------------------------------------------------------------------------------------

        private void cmb_Tipo_adscripcion_personal_apoyo()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_TIPO_ADSCRIP_PA";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_tipo_adscripcion_personal_apoyo.DataSource = dataTable;
                    cmb_tipo_adscripcion_personal_apoyo.DisplayMember = "descripcion";

                    cmb_tipo_adscripcion_personal_apoyo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_tipo_adscripcion_personal_apoyo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_tipo_adscripcion_personal_apoyo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_tipo_adscripcion_personal_apoyo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_tipo_adscripcion_personal_apoyo_Validating(object sender, CancelEventArgs e)
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
        private void cmb_tipo_adscripcion_personal_apoyo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_tipo_adscripcion_personal_apoyo.Text.ToString();

            if (valorComboBox1 == "Otro tipo de adscripción (especifique)")
            {
                txt_otro_tipo_adscripcion_personal_apoyo_especifique.Enabled = true; 
                txt_otro_tipo_adscripcion_personal_apoyo_especifique.BackColor = Color.Honeydew;
                txt_otro_tipo_adscripcion_personal_apoyo_especifique.Focus();
            }
            else
            {
                txt_otro_tipo_adscripcion_personal_apoyo_especifique.Enabled = false; 
                txt_otro_tipo_adscripcion_personal_apoyo_especifique.BackColor = Color.LightGray;
                txt_otro_tipo_adscripcion_personal_apoyo_especifique.Text = "";
            }
            // Verificar si se selecciona "Persona legisladora"
            if (valorComboBox1 == "Persona legisladora")
            {
                cmb_nombre_persona_legisladora_personal_apoyo.Enabled = true;
                cmb_nombre_persona_legisladora_personal_apoyo.BackColor = Color.Honeydew;

                txt_ID_persona_legisladora_personal_apoyo.Enabled = false;
                txt_ID_persona_legisladora_personal_apoyo.BackColor = Color.Honeydew;

                cmb_nombre_persona_legisladora_personal_apoyo.Focus();
            }
            else
            {
                cmb_nombre_persona_legisladora_personal_apoyo.Enabled = false;
                cmb_nombre_persona_legisladora_personal_apoyo.BackColor = Color.LightGray;
                cmb_nombre_persona_legisladora_personal_apoyo.SelectedIndex = -1; // Limpiar selección

                txt_ID_persona_legisladora_personal_apoyo.Enabled = false;
                txt_ID_persona_legisladora_personal_apoyo.BackColor = Color.LightGray;
                txt_ID_persona_legisladora_personal_apoyo.Text = ""; // Limpiar texto
            }
            // Verificar si se selecciona "Grupo parlamentario"
            if (valorComboBox1 == "Grupo parlamentario")
            {
                cmb_grupo_parlamentario_personal_apoyo.Enabled = true;
                cmb_grupo_parlamentario_personal_apoyo.BackColor = Color.Honeydew;

                cmb_grupo_parlamentario_personal_apoyo.Focus();
            }
            else
            {
                cmb_grupo_parlamentario_personal_apoyo.Enabled = false;
                cmb_grupo_parlamentario_personal_apoyo.BackColor = Color.LightGray;
                cmb_grupo_parlamentario_personal_apoyo.SelectedIndex = -1; // Limpiar selección
            }
        }

        // cmb_nombre_persona_legisladora_personal_apoyo

        private void CMB_nombre_persona_legisladora_personal_apoyo()
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

                    cmb_nombre_persona_legisladora_personal_apoyo.DataSource = dataTable;
                    cmb_nombre_persona_legisladora_personal_apoyo.DisplayMember = "txt_nombre_1_persona_legisladora";

                    cmb_nombre_persona_legisladora_personal_apoyo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_nombre_persona_legisladora_personal_apoyo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_nombre_persona_legisladora_personal_apoyo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_nombre_persona_legisladora_personal_apoyo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_nombre_persona_legisladora_personal_apoyo_Validating(object sender, CancelEventArgs e)
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
        private void cmb_nombre_persona_legisladora_personal_apoyo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = cmb_nombre_persona_legisladora_personal_apoyo.Text;

            // Verificar si el nombre seleccionado es nulo o vacío
            if (string.IsNullOrEmpty(nombreSeleccionado))
            {
                txt_ID_persona_legisladora_personal_apoyo.Text = "";
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
                    string query = "select txt_ID_persona_legisladora from TR_PERSONAS_LEGISLADORAS";

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
                            txt_ID_persona_legisladora_personal_apoyo.Text = resultado.ToString();
                        }
                        else
                        {
                            txt_ID_persona_legisladora_personal_apoyo.Text = ""; // Limpiar el TextBox si no se encontró un ID
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

        // cmb_grupo_parlamentario_personal_apoyo

        private void CMB_grupo_parlamentario_personal_apoyo()
        {
            string cadena = "Data Source=DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // Comando SQL para obtener la entidad_federativa de TR_DATOS_GENERALES
                    string queryEntidad = "SELECT entidad_federativa FROM TR_DATOS_GENERALES LIMIT 1";
                    SQLiteCommand cmdEntidad = new SQLiteCommand(queryEntidad, conexion);

                    // Ejecutar la consulta y obtener la entidad_federativa
                    string entidadFederativa = cmdEntidad.ExecuteScalar()?.ToString();

                    if (!string.IsNullOrEmpty(entidadFederativa))
                    {
                        // Comando SQL para obtener las descripciones de TC_GRUPO_PARLAMENTARIO que coincidan con la entidad
                        string query = "SELECT descripcion FROM TC_GRUPO_PARLAMENTARIO WHERE entidad = @entidadFederativa";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@entidadFederativa", entidadFederativa);

                        // Utilizar un DataAdapter para llenar un DataTable con los resultados
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Asignar el DataTable como fuente de datos para el ComboBox
                        cmb_grupo_parlamentario_personal_apoyo.DataSource = dataTable;
                        cmb_grupo_parlamentario_personal_apoyo.DisplayMember = "descripcion";

                        cmb_grupo_parlamentario_personal_apoyo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_grupo_parlamentario_personal_apoyo.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_grupo_parlamentario_personal_apoyo.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_grupo_parlamentario_personal_apoyo.SelectedIndex = -1; // Aquí se establece como vacío
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la entidad federativa en TR_DATOS_GENERALES.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void cmb_grupo_parlamentario_personal_apoyo_Validating(object sender, CancelEventArgs e)
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

        // txt_otro_tipo_adscripcion_personal_apoyo_especifique
        private void txt_otro_tipo_adscripcion_personal_apoyo_especifique_KeyPress(object sender, KeyPressEventArgs e)
        {
            met_no_permite_acentos(e);
        }
        private void txt_otro_tipo_adscripcion_personal_apoyo_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox

            txt_otro_tipo_adscripcion_personal_apoyo_especifique.Text = txt_otro_tipo_adscripcion_personal_apoyo_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor

            txt_otro_tipo_adscripcion_personal_apoyo_especifique.SelectionStart = txt_otro_tipo_adscripcion_personal_apoyo_especifique.Text.Length;

        }
        


        // TITULARIDAD DE LA SEECRETARIA TÉCNICA DE DETERMINADA COMISIÓN LEGISLATIVA ------------------------------------------------------------------

        private void cmb_Cond_secretario_tecnico_comision_legislativa_personal_apoyo()
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

                    cmb_cond_secretario_tecnico_comision_legislativa_personal_apoyo.DataSource = dataTable;
                    cmb_cond_secretario_tecnico_comision_legislativa_personal_apoyo.DisplayMember = "descripcion";

                    cmb_cond_secretario_tecnico_comision_legislativa_personal_apoyo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_secretario_tecnico_comision_legislativa_personal_apoyo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_secretario_tecnico_comision_legislativa_personal_apoyo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_secretario_tecnico_comision_legislativa_personal_apoyo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_secretario_tecnico_comision_legislativa_personal_apoyo_Validating(object sender, CancelEventArgs e)
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
        private void cmb_cond_secretario_tecnico_comision_legislativa_personal_apoyo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_secretario_tecnico_comision_legislativa_personal_apoyo.Text.ToString();

            if (valorComboBox1 == "Si")
            {
                cmb_nombre_comision_legislativa_personal_apoyo.Enabled = true;
                cmb_nombre_comision_legislativa_personal_apoyo.BackColor = Color.Honeydew;

                txt_ID_comision_legislativa_personal_apoyo.Enabled = false;
                txt_ID_comision_legislativa_personal_apoyo.BackColor = Color.Honeydew;

                // Enfocar el ComboBox
                cmb_nombre_comision_legislativa_personal_apoyo.Focus();
            }
            else
            {
                // Si el valor es "No", deshabilitar el ComboBox y limpiar el TextBox
                cmb_nombre_comision_legislativa_personal_apoyo.Enabled = false;
                cmb_nombre_comision_legislativa_personal_apoyo.BackColor = Color.LightGray;
                cmb_nombre_comision_legislativa_personal_apoyo.SelectedIndex = -1; // Limpiar selección del ComboBox

                txt_ID_comision_legislativa_personal_apoyo.Enabled = false;
                txt_ID_comision_legislativa_personal_apoyo.BackColor = Color.LightGray;
                txt_ID_comision_legislativa_personal_apoyo.Text = ""; // Limpiar el contenido del TextBox
            }
        }

        // cmb_nombre_comision_legislativa_personal_apoyo

        private void cmb_Nombre_comision_legislativa_personal_apoyo()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // Abrir la conexión
                    conexion.Open();

                    // Comando de SQL para seleccionar el nombre de la comisión legislativa y el ID
                    string query = "SELECT ID_comision_legislativa, nombre_comision_legislativa FROM TR_COMISIONES_LEGISLATIVAS";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataAdapter para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Asignar el DataTable como fuente de datos para el ComboBox
                    cmb_nombre_comision_legislativa_personal_apoyo.DataSource = dataTable;
                    cmb_nombre_comision_legislativa_personal_apoyo.DisplayMember = "nombre_comision_legislativa";
                    cmb_nombre_comision_legislativa_personal_apoyo.ValueMember = "ID_comision_legislativa"; // Asociar el ID

                    // Configurar el ComboBox
                    cmb_nombre_comision_legislativa_personal_apoyo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_nombre_comision_legislativa_personal_apoyo.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cmb_nombre_comision_legislativa_personal_apoyo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_nombre_comision_legislativa_personal_apoyo.SelectedIndex = -1; // Dejar vacío al iniciar

                    // Asignar evento para actualizar el TextBox cuando se selecciona una opción
                    cmb_nombre_comision_legislativa_personal_apoyo.SelectedIndexChanged += cmb_nombre_comision_legislativa_personal_apoyo_SelectedIndexChanged;
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
        private void cmb_nombre_comision_legislativa_personal_apoyo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verificar si hay una selección válida
            if (cmb_nombre_comision_legislativa_personal_apoyo.SelectedIndex != -1 && !string.IsNullOrEmpty(cmb_nombre_comision_legislativa_personal_apoyo.Text))
            {
                // Obtener el ID asociado a la comisión seleccionada
                string idComisionLegislativa = cmb_nombre_comision_legislativa_personal_apoyo.SelectedValue.ToString();

                // Mostrar el ID en el TextBox txt_ID_comision_legislativa_personal_apoyo
                txt_ID_comision_legislativa_personal_apoyo.Text = idComisionLegislativa;
            }
            else
            {
                // Limpiar el TextBox si no hay selección válida o si se vacía el ComboBox
                txt_ID_comision_legislativa_personal_apoyo.Text = string.Empty;
            }
        }
        private void cmb_nombre_comision_legislativa_personal_apoyo_Validating(object sender, CancelEventArgs e)
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


        // PERFIL PROFECIONAL -------------------------------------------------------------------------------------------------------------------------

        private void cmb_Escolaridad_personal_apoyo()
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

                    cmb_escolaridad_personal_apoyo.DataSource = dataTable;
                    cmb_escolaridad_personal_apoyo.DisplayMember = "descripcion";

                    cmb_escolaridad_personal_apoyo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_escolaridad_personal_apoyo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_escolaridad_personal_apoyo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_escolaridad_personal_apoyo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_escolaridad_personal_apoyo_Validating(object sender, CancelEventArgs e)
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
        private void cmb_escolaridad_personal_apoyo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            if (cmb_escolaridad_personal_apoyo.SelectedItem != null)
            {
                // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
                string valorComboBox = cmb_escolaridad_personal_apoyo.Text.ToString();


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

                        cmb_estatus_escolaridad_personal_apoyo.DataSource = dataTable;
                        cmb_estatus_escolaridad_personal_apoyo.DisplayMember = "descripcion";

                        cmb_estatus_escolaridad_personal_apoyo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_estatus_escolaridad_personal_apoyo.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_estatus_escolaridad_personal_apoyo.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_estatus_escolaridad_personal_apoyo.SelectedIndex = -1; // Aquí se establece como vacío

                        if (valorComboBox == "Licenciatura")
                        {
                            cmb_carrera_licenciatura_personal_apoyo.Enabled = true; cmb_carrera_licenciatura_personal_apoyo.BackColor = Color.Honeydew;
                            cmb_carrera_maestria_personal_apoyo.Enabled = false; cmb_carrera_maestria_personal_apoyo.BackColor = Color.LightGray;
                            cmb_carrera_doctorado_personal_apoyo.Enabled = false; cmb_carrera_doctorado_personal_apoyo.BackColor = Color.LightGray;
                            dgv_nivel_escolaridad_PA.BackgroundColor = Color.Honeydew;

                            cmb_carrera_licenciatura_personal_apoyo.Focus();
                            btnAgregarNivelEscPA.Enabled = true; btnEliminarNivelEscPA.Enabled = true;

                            cmb_carrera_maestria_personal_apoyo.Text = ""; cmb_carrera_doctorado_personal_apoyo.Text = "";
                        }
                        else if (valorComboBox == "Maestría")
                        {

                            cmb_carrera_licenciatura_personal_apoyo.Enabled = true; cmb_carrera_licenciatura_personal_apoyo.BackColor = Color.Honeydew;
                            cmb_carrera_maestria_personal_apoyo.Enabled = true; cmb_carrera_maestria_personal_apoyo.BackColor = Color.Honeydew;
                            cmb_carrera_doctorado_personal_apoyo.Enabled = false; cmb_carrera_doctorado_personal_apoyo.BackColor = Color.LightGray;
                            dgv_nivel_escolaridad_PA.BackgroundColor = Color.Honeydew;
                            cmb_carrera_licenciatura_personal_apoyo.Focus();

                            btnAgregarNivelEscPA.Enabled = true; btnEliminarNivelEscPA.Enabled = true;

                            cmb_carrera_doctorado_personal_apoyo.Text = "";
                        }
                        else if (valorComboBox == "Doctorado")
                        {
                            cmb_carrera_licenciatura_personal_apoyo.Enabled = true; cmb_carrera_licenciatura_personal_apoyo.BackColor = Color.Honeydew;
                            cmb_carrera_maestria_personal_apoyo.Enabled = true; cmb_carrera_maestria_personal_apoyo.BackColor = Color.Honeydew;
                            cmb_carrera_doctorado_personal_apoyo.Enabled = true; cmb_carrera_doctorado_personal_apoyo.BackColor = Color.Honeydew;
                            dgv_nivel_escolaridad_PA.BackgroundColor = Color.Honeydew;

                            btnAgregarNivelEscPA.Enabled = true; btnEliminarNivelEscPA.Enabled = true;

                            cmb_carrera_licenciatura_personal_apoyo.Focus();
                        }
                        else
                        {
                            cmb_carrera_licenciatura_personal_apoyo.Enabled = false; cmb_carrera_licenciatura_personal_apoyo.BackColor = Color.LightGray;
                            cmb_carrera_maestria_personal_apoyo.Enabled = false; cmb_carrera_maestria_personal_apoyo.BackColor = Color.LightGray;
                            cmb_carrera_doctorado_personal_apoyo.Enabled = false; cmb_carrera_doctorado_personal_apoyo.BackColor = Color.LightGray;
                            dgv_nivel_escolaridad_PA.BackgroundColor = Color.LightGray;
                            dgv_nivel_escolaridad_PA.Rows.Clear();

                            cmb_carrera_licenciatura_personal_apoyo.Text = ""; cmb_carrera_maestria_personal_apoyo.Text = "";
                            cmb_carrera_doctorado_personal_apoyo.Text = "";

                            btnAgregarNivelEscPA.Enabled = false; btnEliminarNivelEscPA.Enabled = false;
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

        // btnAgregarNivelEscPA

        private void btnAgregarNivelEscPA_Click(object sender, EventArgs e)
        {
            // Se obtienen los valores de los ComboBox
            string lic_pa = cmb_carrera_licenciatura_personal_apoyo.Text.Trim();
            string mae_pa = cmb_carrera_maestria_personal_apoyo.Text.Trim();
            string doc_pa = cmb_carrera_doctorado_personal_apoyo.Text.Trim();

            // Variable para controlar si hay duplicados
            bool isDuplicate = false;

            // Revisar si el valor ya existe en el DataGridView SOLO si el campo tiene un valor
            foreach (DataGridViewRow row in dgv_nivel_escolaridad_PA.Rows)
            {
                // Comparar solo si el valor en licenciatura no es vacío
                if (!string.IsNullOrEmpty(lic_pa) && row.Cells[0].Value != null && row.Cells[0].Value.ToString().Trim() == lic_pa)
                {
                    isDuplicate = true;
                    break;
                }

                // Comparar solo si el valor en maestría no es vacío
                if (!string.IsNullOrEmpty(mae_pa) && row.Cells[1].Value != null && row.Cells[1].Value.ToString().Trim() == mae_pa)
                {
                    isDuplicate = true;
                    break;
                }

                // Comparar solo si el valor en doctorado no es vacío
                if (!string.IsNullOrEmpty(doc_pa) && row.Cells[2].Value != null && row.Cells[2].Value.ToString().Trim() == doc_pa)
                {
                    isDuplicate = true;
                    break;
                }
            }

            // Si es duplicado, mostrar mensaje y salir
            if (isDuplicate)
            {
                MessageBox.Show("Dato duplicado. No se puede agregar el mismo valor en licenciatura, maestría o doctorado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Si no es duplicado, agregar una nueva fila al DataGridView
                dgv_nivel_escolaridad_PA.Rows.Add(lic_pa, mae_pa, doc_pa);
            }

            // Limpiar los ComboBox después de agregar o intentar agregar
            cmb_carrera_licenciatura_personal_apoyo.Text = "";
            cmb_carrera_maestria_personal_apoyo.Text = "";
            cmb_carrera_doctorado_personal_apoyo.Text = "";
        }

        // btnEliminarNivelEscPA

        private void btnEliminarNivelEscPA_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgv_nivel_escolaridad_PA.SelectedRows.Count > 0)
            {
                dgv_nivel_escolaridad_PA.Rows.RemoveAt(dgv_nivel_escolaridad_PA.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }

        // filtros adicionales de perfil profecional

        private void cmb_Estatus_escolaridad_personal_apoyo()
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

                    cmb_estatus_escolaridad_personal_apoyo.DataSource = dataTable;
                    cmb_estatus_escolaridad_personal_apoyo.DisplayMember = "descripcion";

                    cmb_estatus_escolaridad_personal_apoyo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_estatus_escolaridad_personal_apoyo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_estatus_escolaridad_personal_apoyo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_estatus_escolaridad_personal_apoyo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_estatus_escolaridad_personal_apoyo_Validating(object sender, CancelEventArgs e)
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
        private void cmb_Carrera_licenciatura_personal_apoyo()
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

                    cmb_carrera_licenciatura_personal_apoyo.DataSource = dataTable;
                    cmb_carrera_licenciatura_personal_apoyo.DisplayMember = "descripcion";

                    cmb_carrera_licenciatura_personal_apoyo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_carrera_licenciatura_personal_apoyo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_carrera_licenciatura_personal_apoyo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_carrera_licenciatura_personal_apoyo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_carrera_licenciatura_personal_apoyo_Validating(object sender, CancelEventArgs e)
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
        private void cmb_Carrera_maestria_personal_apoyo()
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

                    cmb_carrera_maestria_personal_apoyo.DataSource = dataTable;
                    cmb_carrera_maestria_personal_apoyo.DisplayMember = "descripcion";

                    cmb_carrera_maestria_personal_apoyo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_carrera_maestria_personal_apoyo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_carrera_maestria_personal_apoyo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_carrera_maestria_personal_apoyo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_carrera_maestria_personal_apoyo_Validating(object sender, CancelEventArgs e)
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
        private void cmb_Carrera_doctorado_personal_apoyo()
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

                    cmb_carrera_doctorado_personal_apoyo.DataSource = dataTable;
                    cmb_carrera_doctorado_personal_apoyo.DisplayMember = "descripcion";

                    cmb_carrera_doctorado_personal_apoyo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_carrera_doctorado_personal_apoyo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_carrera_doctorado_personal_apoyo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_carrera_doctorado_personal_apoyo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_carrera_doctorado_personal_apoyo_Validating(object sender, CancelEventArgs e)
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







    }
}
