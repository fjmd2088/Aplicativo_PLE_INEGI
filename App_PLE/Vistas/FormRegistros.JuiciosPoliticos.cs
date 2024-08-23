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
        // txt_turno_denuncia_juicio_politico
        private void txt_turno_denuncia_juicio_politico_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
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
