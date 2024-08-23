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
        // txt_turno_iniciativa_urgente_obvia
        private void txt_turno_iniciativa_urgente_obvia_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }

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
