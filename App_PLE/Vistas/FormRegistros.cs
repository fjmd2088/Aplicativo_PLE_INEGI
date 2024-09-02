using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Resources.ResXFileRef;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using GMap.NET.WindowsForms.Markers;
using System.Data.SqlClient;
using DocumentFormat.OpenXml.Office.Word;
using System.Threading;

namespace App_PLE.Vistas
{
    public partial class FormRegistros : Form
    {
        private GMapOverlay markersOverlay;


        public FormRegistros()
        {
            InitializeComponent();
            InitializeMap();

            ConexionBasedatosSQLite(); // se hace la conexion a la base de datos de sqlite
        }

        private void FormRegistros_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Cierra la conexión cuando se cierra el formulario
            if (_connection != null)
            {
                try
                {
                    _connection.Close();
                    //MessageBox.Show("Conexión cerrada exitosamente.");
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Error al cerrar la conexión: " + ex.Message);
                }
            }
        }
        //-------------------------------------------------- CARGA INICIAL DE FORMULARIO ----------------------------------------------------

        private void FormRegistros_Load(object sender, EventArgs e)
        {
            // ajustar el tamaño del formulario
            this.Size = new System.Drawing.Size(1300, 720); // ancho, alto
            // ajustar posicion del formulario
            this.StartPosition = FormStartPosition.CenterScreen;

            // se desactivan las tabpages de manera inicial
            //DisableTab(tabPageCL);
            //DisableTab(tabPagePL);


            // ---------------------------------------------- DATOS GENERALES ---------------------------------------------------------------
            cmb_Entidad();

            // CAMPOS DESHABILITADOS INICIALMENTE
            txt_agee.Enabled = false; txt_agee.BackColor = Color.LightGray;

            txt_id_legislatura.Enabled = false; dgvPE.Enabled = false; cmb_periodo_extraordinario_reportado.Enabled = false;
            Txt_sesiones_celebradas_pe.Enabled = false;
            btnAgregarPE.Enabled = false; BtnEliminarPE.Enabled = false; txt_periodos_extraordinarios_celebrados.Enabled = false;

            txt_periodo_reportado_rec.Enabled = false; txt_periodo_reportado_rec.BackColor = Color.LightGray;
            //tabPageCL.Enabled = false; tabPagePL.Enabled = false; tabPagePA.Enabled = false; tabPageIni.Enabled = false;
            //tabPageIniUO.Enabled = false; tabPageJP.Enabled = false; tabPageDP.Enabled = false; tabPageCom.Enabled = false;

            dgvPE.Enabled = false;
            txt_periodos_extraordinarios_celebrados.Enabled = false; txt_periodos_extraordinarios_celebrados.BackColor = Color.LightGray;
            Txt_sesiones_celebradas_pe.Enabled = false; Txt_sesiones_celebradas_pe.BackColor = Color.LightGray;
            cmb_periodo_extraordinario_reportado.Enabled = false; cmb_periodo_extraordinario_reportado.BackColor = Color.LightGray;
            dgvPE.Enabled = false; dgvPE.BackgroundColor = Color.LightGray;
            dtp_fecha_inicio_pe.Enabled = false;
            dtp_fecha_termino_pe.Enabled = false;

            btnAgregarPE.Enabled = false; BtnEliminarPE.Enabled = false;

            chbPE.Checked = false;


            // CAMPOS VACIOS O CON VALOR PREDETERMINADO
            txt_id_legislatura.Text = string.Empty; txt_agee.Text = string.Empty; cmb_numero_legislatura.Text = "";
            dtp_inicio_funciones_legislatura.Value = new DateTime(1899, 9, 9); dtp_termino_funciones_legislatura.Value = new DateTime(1899, 9, 9);
            cmb_ejercicio_constitucional_informacion_reportada.Text = "";
            dtp_fecha_inicio_informacion_reportada.Value = new DateTime(1899, 9, 9); dtp_fecha_termino_informacion_reportada.Value = new DateTime(1899, 9, 9);
            cmb_periodo_reportado_po.Text = ""; txt_periodo_reportado_rec.Text = "";
            dtp_fecha_inicio_po.Value = new DateTime(1899, 9, 9); dtp_fecha_termino_po.Value = new DateTime(1899, 9, 9);
            dtp_fecha_inicio_pe.Value = new DateTime(1899, 9, 9); dtp_fecha_termino_pe.Value = new DateTime(1899, 9, 9);
            dtp_fecha_inicio_p_rec.Value = new DateTime(1899, 9, 9); dtp_fecha_termino_p_rec.Value = new DateTime(1899, 9, 9);

            // ---------------------------------------------- COMISIONES LEGISLATIVAS ---------------------------------------------------------------
            cmb_Tipo_CL();
            cmb_Tema_CL();
            cmb_cond_transmision_reuniones_celebradas_CL();
            cmb_cond_celebracion_reuniones_CL();
            DGV_REGISTROS_CL();

            // CAMPOS DESHABILITADOS INICIALMENTE
            Txt_otro_tipo_comision_legislativa_especifique.Enabled = false; Txt_otro_tipo_comision_legislativa_especifique.BackColor = Color.LightGray;
            txt_otro_tema_comision_legislativa_especifique.Enabled = false; txt_otro_tema_comision_legislativa_especifique.BackColor = Color.LightGray;
            txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Enabled = false; txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.BackColor = Color.LightGray;
            txt_cant_reuniones_celebradas_comision_legislativa.Enabled = false; txt_cant_reuniones_celebradas_comision_legislativa.BackColor = Color.LightGray;
            cmb_cond_transmision_reuniones_celebradas_comision_legislativa.Enabled = false; cmb_cond_transmision_reuniones_celebradas_comision_legislativa.BackColor = Color.LightGray;
            txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.Enabled = false; txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.BackColor = Color.LightGray;


            // ---------------------------------------------- PERSONAS LEGISLADORAS ---------------------------------------------------------------
            //DGV_REGISTROS_PL();

            cmb_Sexo_Persona_Legisladora();
            cmb_Estatus_persona_legisladora();
            cmb_Tipo_licencia_persona_legisladora();
            cmb_Causa_fallecimiento_persona_legisladora();
            cmb_Caracter_cargo_persona_legisladora();
            cmb_Escolaridad_persona_legisladora();
            //cmb_Estatus_escolaridad_persona_legisladora();
            cmb_Carrera_licenciatura_persona_legisladora();
            cmb_Carrera_maestria_persona_legisladora();
            cmb_Carrera_doctorado_persona_legisladora();
            cmb_Cond_lengua_ind_persona_legisladora();
            cmb_Lengua_ind_persona_legisladora();


            cmb_Cond_discapacidad_persona_legisladora();


            cmb_Tipo_discapacidad_persona_legisladora();
            cmb_Cond_pueblo_ind_persona_legisladora();
            cmb_Pueblo_ind_persona_legisladora();
            cmb_Cond_pob_diversidad_sexual_persona_legisladora();
            cmb_Cond_pob_afromexicana_persona_legisladora();
            cmb_Empleo_anterior_persona_legisladora();
            cmb_Antigüedad_servicio_publico_persona_legisladora();
            cmb_Antigüedad_persona_legisladora();
            cmb_Forma_eleccion_persona_legisladora();
            cmb_Tipo_candidatura_persona_legisladora();
            cmb_Tipo_adscripcion_inicial_persona_legisladora();
            cmb_Tipo_adscripcion_final_persona_legisladora();
            cmb_Cond_presentacion_declaracion_situacion_patrimonial();
            cmb_Cond_presentacion_declaracion_intereses();
            cmb_Cond_presentacion_declaracion_fiscal();
            cmb_Cond_casa_atencion_ciudadana();
            cmb_Cond_casa_atencion_ciudadana_movil();
            cmb_Cond_integrante_comision_permanente();
            cmb_Cargo_comision_permanente();
            cmb_Cargo_comision_legislativa();
            cmb_Cond_integrante_jucopo();
            cmb_Cond_integrante_mesa_directiva();
            cmb_Cargo_mesa_directiva_PL();
            cmb_Cargo_jucopo();
            cmb_Nombre_comision_legislativa();

            // CAMPOS DESHABILITADOS INICIALMENTE
            txt_nombre_2_persona_legisladora.Enabled = false; txt_nombre_2_persona_legisladora.BackColor = Color.LightGray;
            txt_nombre_3_persona_legisladora.Enabled = false; txt_nombre_3_persona_legisladora.BackColor = Color.LightGray;
            txt_apellido_2_persona_legisladora.Enabled = false; txt_apellido_2_persona_legisladora.BackColor = Color.LightGray;
            txt_apellido_3_persona_legisladora.Enabled = false; txt_apellido_3_persona_legisladora.BackColor = Color.LightGray;
            txt_otro_estatus_persona_legisladora_especifique.Enabled = false; txt_otro_estatus_persona_legisladora_especifique.BackColor = Color.LightGray;
            cbm_causa_fallecimiento_persona_legisladora.Enabled = false; cbm_causa_fallecimiento_persona_legisladora.BackColor = Color.LightGray;
            cbm_tipo_licencia_persona_legisladora.Enabled = false; cbm_tipo_licencia_persona_legisladora.BackColor = Color.LightGray;
            cmb_nombre_persona_legisladora_propietaria.Enabled = false; cmb_nombre_persona_legisladora_propietaria.BackColor = Color.LightGray;
            txt_ID_persona_legisladora_propietaria.Enabled = false; txt_ID_persona_legisladora_propietaria.BackColor = Color.LightGray;
            cmb_carrera_licenciatura_persona_legisladora_PL.Enabled = false; cmb_carrera_licenciatura_persona_legisladora_PL.BackColor = Color.LightGray;
            cmb_carrera_maestria_persona_legisladora_PL.Enabled = false; cmb_carrera_maestria_persona_legisladora_PL.BackColor = Color.LightGray;
            cmb_carrera_doctorado_persona_legisladora_PL.Enabled = false; cmb_carrera_doctorado_persona_legisladora_PL.BackColor = Color.LightGray;
            cmb_lengua_ind_persona_legisladora.Enabled = false; cmb_lengua_ind_persona_legisladora.BackColor = Color.LightGray;
            cmb_pueblo_ind_persona_legisladora_PL.Enabled = false; cmb_pueblo_ind_persona_legisladora_PL.BackColor = Color.LightGray;
            cmb_tipo_discapacidad_persona_legisladora.Enabled = false; cmb_tipo_discapacidad_persona_legisladora.BackColor = Color.LightGray;
            cmb_cond_pob_diversidad_sexual_persona_legisladora.Enabled = false; cmb_cond_pob_diversidad_sexual_persona_legisladora.BackColor = Color.LightGray;
            cmb_distrito_electoral_mayoria_relativa.Enabled = false; cmb_distrito_electoral_mayoria_relativa.BackColor = Color.LightGray;
            cmb_tipo_candidatura_persona_legisladora.Enabled = false; cmb_tipo_candidatura_persona_legisladora.BackColor = Color.LightGray;
            cmb_partido_politico_candidatura_partido_unico.Enabled = false; cmb_partido_politico_candidatura_partido_unico.BackColor = Color.LightGray;
            cmb_partido_politico_candidatura_coalicion.Enabled = false; cmb_partido_politico_candidatura_coalicion.BackColor = Color.LightGray;
            txt_ID_persona_legisladora.Enabled = false; txt_ID_persona_legisladora.BackColor = Color.LightGray;
            dgv_partido_coalicion.BackgroundColor = Color.LightGray;
            dgv_nivel_escolaridad_PL.BackgroundColor = Color.LightGray;
            dgv_lengua_PA.BackgroundColor = Color.LightGray;
            dgv_tipo_discapacidad_PA.BackgroundColor = Color.LightGray;
            cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.Enabled = false; cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.BackColor = Color.LightGray;
            cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.Enabled = false; cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.BackColor = Color.LightGray;
            txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.Enabled = false; txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.BackColor = Color.LightGray;
            txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.Enabled = false; txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.BackColor = Color.LightGray;
            txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.Enabled = false; txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.BackColor = Color.LightGray;
            txt_no_aplica_presentacion_declaracion_intereses_especifique.Enabled = false; txt_no_aplica_presentacion_declaracion_intereses_especifique.BackColor = Color.LightGray;
            txt_no_aplica_presentacion_declaracion_fiscal_especifique.Enabled = false; txt_no_aplica_presentacion_declaracion_fiscal_especifique.BackColor = Color.LightGray;
            cmb_cond_casa_atencion_ciudadana_movil.Enabled = false; cmb_cond_casa_atencion_ciudadana_movil.BackColor = Color.LightGray;
            txt_latitud_casa_atencion_ciudadana.Enabled = false; txt_latitud_casa_atencion_ciudadana.BackColor = Color.LightGray;
            txt_longitud_casa_atencion_ciudadana.Enabled = false; txt_longitud_casa_atencion_ciudadana.BackColor = Color.LightGray;
            cmb_cargo_comision_permanente.Enabled = false; cmb_cargo_comision_permanente.BackColor = Color.LightGray;
            txt_otro_cargo_comision_permanente_especifique.Enabled = false; txt_otro_cargo_comision_permanente_especifique.BackColor = Color.LightGray;
            cmb_cargo_jucopo.Enabled = false; cmb_cargo_jucopo.BackColor = Color.LightGray;
            txt_otro_cargo_jucopo_especifique.Enabled = false; txt_otro_cargo_jucopo_especifique.BackColor = Color.LightGray;
            cmb_cargo_mesa_directiva_PL.Enabled = false; cmb_cargo_mesa_directiva_PL.BackColor = Color.LightGray;
            txt_otro_cargo_mesa_directiva_especifique.Enabled = false; txt_otro_cargo_mesa_directiva_especifique.BackColor = Color.LightGray;
            txt_ID_comision_legislativa_pc.Enabled = false; txt_ID_comision_legislativa_pc.BackColor = Color.LightGray;
            txt_cant_intervenciones_sesiones_plenarias_persona_legisladora.Enabled = false; txt_cant_intervenciones_sesiones_plenarias_persona_legisladora.BackColor = Color.LightGray;
            txt_asist_sesiones_comision_permanente_persona_legisladora.Enabled = false; txt_asist_sesiones_comision_permanente_persona_legisladora.BackColor = Color.LightGray;
            txt_cant_interv_sesiones_dip_permanente_persona_legisladora.Enabled = false; txt_cant_interv_sesiones_dip_permanente_persona_legisladora.BackColor = Color.LightGray;
            gMapControl.Enabled = false;

            btnAgregarNivelEscPL.Enabled = false; btnEliminarNivelEscPL.Enabled = false;
            btnAgregarLenguaPA.Enabled = false; btnEliminarLenguaPA.Enabled = false;
            btnAgregarDiscapacidadPA.Enabled = false; btnEliminarDiscapacidadPA.Enabled = false;
            btnAgregarCandidaturaPL.Enabled = false; btnEliminarCandidaturaPL.Enabled = false;

            // CAMPOS VACIOS O CON VALOR PREDETERMINADO
            txt_id_legislatura.Text = "";
            dtp_fecha_nacimiento_persona_legisladora.Value = new DateTime(1980, 9, 9);

            // ---------------------------------------------- PERSONAL DE APOYO ---------------------------------------------------------------
            cmb_Sexo_personal_apoyo();
            cmb_Institucion_seguridad_social_personal_apoyo();
            cmb_Regimen_ontratacion_personal_apoyo();
            cmb_Escolaridad_personal_apoyo();
            cmb_Estatus_escolaridad_personal_apoyo();
            cmb_Carrera_licenciatura_personal_apoyo();
            cmb_Carrera_maestria_personal_apoyo();
            cmb_Carrera_doctorado_personal_apoyo();
            cmb_Cond_discapacidad_personal_apoyo();
            cmb_Cond_lengua_ind_personal_apoyo();
            cmb_Cond_pueblo_ind_personal_apoyo();
            cmb_Tipo_adscripcion_personal_apoyo();
            cmb_Cond_secretario_tecnico_comision_legislativa_personal_apoyo();
            cmb_Pueblo_ind_pertenencia_personal_apoyo();

            // ---------------------------------------------- INICIATIVAS ---------------------------------------------------------------
            cmb_Cond_presentacion_iniciativa_legislatura_actual();

            cmb_Cond_presentacion_iniciativa_periodo();
            cmb_Numero_legislatura_presentacion_iniciativa();
            cmb_Cond_actualizacion_estatus_iniciativa_periodo();
            cmb_Cond_modificacion_informacion_ingreso_periodo();
            cmb_Estatus_iniciativa();
            cmb_Etapa_procesal_iniciativa();
            cmb_Tipo_iniciativa();
            cmb_Tipo_promovente_iniciativa();
            tipo_Organo_constitucional_autonomo();

            // CAMPOS DESHABILITADOS INICIALMENTE
            txt_ID_iniciativa.Enabled = false; txt_ID_iniciativa.BackColor = Color.LightGray;
            cmb_cond_presentacion_iniciativa_periodo.Enabled = true; cmb_cond_presentacion_iniciativa_periodo.BackColor = Color.Honeydew;


            Txt_otro_tipo_comision_legislativa_especifique.Enabled = false; txt_ID_comision_legislativa.Enabled = false;
            txt_otro_tema_comision_legislativa_especifique.Enabled = false;

            txt_ID_comision_legislativa.Text = string.Empty;
            cmb_tema_comision_legislativa.Text = "";
            cmb_tipo_comision_legislativa.Text = "";

        }

        //-------------------------------------------------- METODOS GENERALES ----------------------------------------------------

        // metodos para activar y desactivar tabpages
        private void DisableTab(TabPage page)
        {
            foreach (Control control in page.Controls)
            {
                control.Enabled = false;
            }
        }
        private void EnableTab(TabPage page)
        {
            foreach (Control control in page.Controls)
            {
                control.Enabled = true;
            }
        }

        // OTROS-----------------------------------------------------------------------------------------------------------------
        public void CargarDatos(string id_registro)
        {
            // Usa los datos recibidos para cargar los controles en el formulario nuevo
            txt_id_legislatura.Text = id_registro;

        }
        private void BtnSalirDG_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de Salir?", "Confirmacion",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {

            }

        }

       











































































































































































































































































        /*
        // Clase para mostrar una ventana emergente de entrada de texto
        public static class Prompt
        {
            public static string ShowDialog(string text, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 300,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };
                Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
                System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox() { Left = 50, Top = 50, Width = 200 };
                System.Windows.Forms.Button confirmation = new System.Windows.Forms.Button() { Text = "Ok", Left = 150, Width = 100, Top = 70, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
        }
        
        private void btnBuscarDG_Click(object sender, EventArgs e)
        {
            // Abrir la ventana emergente para solicitar el dato a buscar
            string datoABuscar = Prompt.ShowDialog("Ingrese el ID:", "Buscar ID Registro");

            if (!string.IsNullOrEmpty(datoABuscar))
            {
                // Realizar la búsqueda en la base de datos
                BuscarEnBaseDeDatos(datoABuscar);
            }
        }

        private void BuscarEnBaseDeDatos(string datoABuscar)
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(cadena))
            {
                try
                {
                    connection.Open();

                    // Realizar la consulta SQL para buscar registros que coincidan con el dato proporcionado
                    string consulta = "SELECT entidad_federativa FROM TR_DATOS_GENERALES WHERE id_datos_generales = @DatoABuscar";
                    SQLiteCommand comando = new SQLiteCommand(consulta, connection);
                    comando.Parameters.AddWithValue("@DatoABuscar", datoABuscar);

                    SQLiteDataReader reader = comando.ExecuteReader();



                    // Agregar resultados al ComboBox
                    while (reader.Read())
                    {
                        cmb_entidad_federativa.Items.Add(reader["entidad_federativa"]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar en la base de datos: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

        }

       */
    }
}
