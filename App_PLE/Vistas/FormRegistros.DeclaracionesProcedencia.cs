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
