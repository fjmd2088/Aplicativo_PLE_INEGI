using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_PLE.Vistas
{
    public partial class FormRegistros: Form
    {

     // Configuración de TXT para mayuculas y Numeros 

        // txt_consecutivo_comparecencia
        private void txt_consecutivo_comparecencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
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
