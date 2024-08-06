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
        }

        //-------------------------------------------------- CARGA INICIAL DE FORMULARIO ----------------------------------------------------

        private void FormRegistros_Load(object sender, EventArgs e)
        {
            // ajustar el tamaño del formulario
            this.Size = new System.Drawing.Size(1300, 720); // ancho, alto
            // ajustar posicion del formulario
            this.StartPosition = FormStartPosition.CenterScreen;

            // se desactivan las tabpages de manera inicial
            DisableTab(tabPageCL);
            DisableTab(tabPagePL);


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

            Txt_otro_tipo_comision_legislativa_especifique.Enabled = false; txt_ID_comision_legislativa.Enabled = false;
            txt_otro_tema_comision_legislativa_especifique.Enabled = false;

            txt_ID_comision_legislativa.Text = string.Empty;
            cmb_tema_comision_legislativa.Text = "";
            cmb_tipo_comision_legislativa.Text = "";

        }

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
        //-------------------------------------------------- DATOS GENERALES ----------------------------------------------------
        private void cmb_Entidad()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select nom_ent from TC_AGEEM group by nom_ent";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_entidad_federativa.DataSource = dataTable;
                    cmb_entidad_federativa.DisplayMember = "nom_ent";

                    cmb_entidad_federativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_entidad_federativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_entidad_federativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_entidad_federativa.Text = "";
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
        private void btnAgregarPE_Click_1(object sender, EventArgs e)
        {
            // se obtienen los valores
            string periodo_reportado_pe = cmb_periodo_extraordinario_reportado.Text.Trim();
            string fecha_inicio_pe = dtp_fecha_inicio_pe.Text.Trim();
            string fecha_termino_pe = dtp_fecha_termino_pe.Text.Trim();
            string sesiones_celebradas_pe = Txt_sesiones_celebradas_pe.Text.Trim();

            if (string.IsNullOrWhiteSpace(Txt_sesiones_celebradas_pe.Text) ||
                string.IsNullOrWhiteSpace(cmb_periodo_extraordinario_reportado.Text))
            {
                MessageBox.Show("Revisar datos vacios");
            }
            else
            {
                bool respuesta = IsDuplicateRecord(cmb_periodo_extraordinario_reportado.Text.ToString());

                if (respuesta == true)
                {
                    MessageBox.Show("Dato duplicado");
                }
                else
                {
                    // Agregar una nueva fila al DataGridView
                    dgvPE.Rows.Add(periodo_reportado_pe, fecha_inicio_pe, fecha_termino_pe, sesiones_celebradas_pe);

                    Txt_sesiones_celebradas_pe.Clear();
                    dtp_fecha_inicio_pe.Value = dtp_fecha_inicio_p_rec.Value; dtp_fecha_termino_pe.Value = dtp_fecha_termino_p_rec.Value;
                }


            }
        }
        
        private long VerificarID()
        {
            // Obtener el ID desde el TextBox
            string id = txt_id_legislatura.Text.Trim(); // Asegúrate de reemplazar 'txt_id_legislatura' con el nombre real de tu TextBox

            // Cadena de conexión a la base de datos SQLite
            string cadena = "Data Source=DB_PLE.db;Version=3;"; // Asegúrate de que esta cadena de conexión es correcta

            using (SQLiteConnection connection = new SQLiteConnection(cadena))
            {
                try
                {
                    connection.Open();

                    // Consulta SQL para verificar la existencia del ID
                    string query = "SELECT COUNT(*) FROM TR_DATOS_GENERALES WHERE id_legislatura = @id"; // Reemplaza 'TR_DATOS_GENERALES' con el nombre de tu tabla

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        // Agregar el parámetro para la consulta SQL
                        command.Parameters.AddWithValue("@id", id);

                        // Ejecutar la consulta y obtener el resultado
                        long count = (long)command.ExecuteScalar();

                        if (count > 0)
                        {
                            // Si el ID existe en la base de datos
                            MessageBox.Show("El ID ya existe en la base de datos. Validar información.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        return count; // Retorna el conteo para uso adicional
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    MessageBox.Show($"Error al conectar a la base de datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0; // Retorna 0 en caso de error para indicar fallo en la verificación
                }
            }
        }

        private void BtnGuardarDG_Click_1(object sender, EventArgs e)
        {

            long long_reg = VerificarID(); // se verifica si el id existe en la base de datos

            bool cv = ValidacionCampos_DG();

            if (long_reg == 0)
            {
                if (cv == true)
                {
                    DialogResult respuesta = MessageBox.Show("¿Está seguro de Guardar los datos?", "Confirmacion",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (respuesta == DialogResult.Yes) // NO HAY PERIODOS EXTRAORDINARIOS
                    {
                        string cadena = "Data Source = DB_PLE.db;Version=3;";

                        using (SQLiteConnection connection = new SQLiteConnection(cadena))
                        {
                            connection.Open();

                            // el dgv de periodos extraordinarios esta vacio
                            if (dgvPE.RowCount == 0)
                            {
                                // Insertamos los datos en la base de datos
                                string query = "INSERT INTO TR_DATOS_GENERALES (id_legislatura," +
                                    "entidad_federativa," +
                                    "agee," +
                                    "numero_legislatura," +
                                    "nombre_legislatura," +
                                    "inicio_funciones_legislatura," +
                                    "termino_funciones_legislatura," +
                                    "distritos_uninominales," +
                                    "diputaciones_plurinominales," +
                                    //"periodo_extraordinario_reportado," +
                                    "ejercicio_constitucional_informacion_reportada," +
                                    "fecha_inicio_informacion_reportada," +
                                    "fecha_termino_informacion_reportada," +
                                    "periodo_reportado," +
                                    "fecha_inicio_p," +
                                    "fecha_termino_p," +
                                    "sesiones_celebradas_p," +
                                    //"cond_celebracion_periodos_extraordinarios," +
                                    //"periodos_extraordinarios_celebrados," +
                                    //"periodo_extraordinario_reportado," +
                                    //"fecha_inicio_pe," +
                                    //"fecha_termino_pe," +
                                    //"sesiones_celebradas_pe," +
                                    //"cond_reconocimiento_iniciativa_p," +
                                    //"cond_reconocimiento_iniciativa_urgente_obvia," +
                                    //"cond_existencia_juicio_politico," +
                                    //"cond_existencia_declaracion_procedencia," +
                                    //"cond_existencia_comparecencia," +
                                    "fecha_actualizacion," +
                                    "periodo_reportado_rec," +
                                    "fecha_inicio_p_rec," +
                                    "fecha_termino_p_rec," +
                                    "sesiones_celebradas_p_rec)" +
                             "VALUES" +
                                    " (@id_legislatura," +
                                    "@entidad_federativa," +
                                    "@agee," +
                                    "@numero_legislatura," +
                                    "@nombre_legislatura," +
                                    "@inicio_funciones_legislatura," +
                                    "@termino_funciones_legislatura," +
                                    "@distritos_uninominales," +
                                    "@diputaciones_plurinominales," +
                                    //"periodo_extraordinario_reportado," +
                                    "@ejercicio_constitucional_informacion_reportada," +
                                    "@fecha_inicio_informacion_reportada," +
                                    "@fecha_termino_informacion_reportada," +
                                    "@periodo_reportado," +
                                    "@fecha_inicio_p," +
                                    "@fecha_termino_p," +
                                    "@sesiones_celebradas_p," +
                                    //"cond_celebracion_periodos_extraordinarios," +
                                    //"periodos_extraordinarios_celebrados," +
                                    //"periodo_extraordinario_reportado," +
                                    //"fecha_inicio_pe," +
                                    //"fecha_termino_pe," +
                                    //"sesiones_celebradas_pe," +
                                    //"cond_reconocimiento_iniciativa_p," +
                                    //"cond_reconocimiento_iniciativa_urgente_obvia," +
                                    //"cond_existencia_juicio_politico," +
                                    //"cond_existencia_declaracion_procedencia," +
                                    //"cond_existencia_comparecencia," +
                                    "@fecha_actualizacion," +
                                    "@periodo_reportado_rec," +
                                    "@fecha_inicio_p_rec," +
                                    "@fecha_termino_p_rec," +
                                    "@sesiones_celebradas_p_rec)";

                                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                                {
                                    // Variables individuales
                                    command.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text);
                                    command.Parameters.AddWithValue("@entidad_federativa", cmb_entidad_federativa.Text);
                                    command.Parameters.AddWithValue("@agee", txt_agee.Text);
                                    command.Parameters.AddWithValue("@numero_legislatura", cmb_numero_legislatura.Text);
                                    command.Parameters.AddWithValue("@nombre_legislatura", txt_nombre_legislatura.Text);
                                    command.Parameters.AddWithValue("@inicio_funciones_legislatura", dtp_inicio_funciones_legislatura.Text);
                                    command.Parameters.AddWithValue("@termino_funciones_legislatura", dtp_termino_funciones_legislatura.Text);
                                    command.Parameters.AddWithValue("@distritos_uninominales", Txt_distritos_uninominales.Text);
                                    command.Parameters.AddWithValue("@diputaciones_plurinominales", Txt_diputaciones_plurinominales.Text);
                                    command.Parameters.AddWithValue("@ejercicio_constitucional_informacion_reportada", cmb_ejercicio_constitucional_informacion_reportada.Text);
                                    command.Parameters.AddWithValue("@fecha_inicio_informacion_reportada", dtp_fecha_inicio_informacion_reportada.Text);
                                    command.Parameters.AddWithValue("@fecha_termino_informacion_reportada", dtp_fecha_termino_informacion_reportada.Text);
                                    command.Parameters.AddWithValue("@periodo_reportado", cmb_periodo_reportado_po.Text);
                                    command.Parameters.AddWithValue("@fecha_inicio_p", dtp_fecha_inicio_po.Text);
                                    command.Parameters.AddWithValue("@fecha_termino_p", dtp_fecha_termino_po.Text);
                                    command.Parameters.AddWithValue("@sesiones_celebradas_p", Txt_sesiones_celebradas_po.Text);
                                    command.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                    command.Parameters.AddWithValue("@periodo_reportado_rec", txt_periodo_reportado_rec.Text);
                                    command.Parameters.AddWithValue("@fecha_inicio_p_rec", dtp_fecha_inicio_p_rec.Text);
                                    command.Parameters.AddWithValue("@fecha_termino_p_rec", dtp_fecha_termino_p_rec.Text);
                                    command.Parameters.AddWithValue("@sesiones_celebradas_p_rec", txt_sesiones_celebradas_p_rec.Text);



                                    command.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                // Recorremos las filas del DataGridView
                                foreach (DataGridViewRow row in dgvPE.Rows)
                                {
                                    // Ignoramos la fila vacía al final
                                    if (!row.IsNewRow)
                                    {
                                        // Insertamos los datos en la base de datos
                                        // Insertamos los datos en la base de datos
                                        string query = "INSERT INTO TR_DATOS_GENERALES (id_legislatura," +
                                            "entidad_federativa," +
                                            "agee," +
                                            "numero_legislatura," +
                                            "nombre_legislatura," +
                                            "inicio_funciones_legislatura," +
                                            "termino_funciones_legislatura," +
                                            "distritos_uninominales," +
                                            "diputaciones_plurinominales," +
                                            //"periodo_extraordinario_reportado," +
                                            "ejercicio_constitucional_informacion_reportada," +
                                            "fecha_inicio_informacion_reportada," +
                                            "fecha_termino_informacion_reportada," +
                                            "periodo_reportado," +
                                            "fecha_inicio_p," +
                                            "fecha_termino_p," +
                                            "sesiones_celebradas_p," +
                                            //"cond_celebracion_periodos_extraordinarios," +
                                            "periodos_extraordinarios_celebrados," +
                                            "periodo_extraordinario_reportado," +
                                            "fecha_inicio_pe," +
                                            "fecha_termino_pe," +
                                            "sesiones_celebradas_pe," +
                                            //"cond_reconocimiento_iniciativa_p," +
                                            //"cond_reconocimiento_iniciativa_urgente_obvia," +
                                            //"cond_existencia_juicio_politico," +
                                            //"cond_existencia_declaracion_procedencia," +
                                            //"cond_existencia_comparecencia," +
                                            "fecha_actualizacion," +
                                            "periodo_reportado_rec," +
                                            "fecha_inicio_p_rec," +
                                            "fecha_termino_p_rec," +
                                            "sesiones_celebradas_p_rec)" +
                                     "VALUES" +
                                            " (@id_legislatura," +
                                            "@entidad_federativa," +
                                            "@agee," +
                                            "@numero_legislatura," +
                                            "@nombre_legislatura," +
                                            "@inicio_funciones_legislatura," +
                                            "@termino_funciones_legislatura," +
                                            "@distritos_uninominales," +
                                            "@diputaciones_plurinominales," +
                                            //"periodo_extraordinario_reportado," +
                                            "@ejercicio_constitucional_informacion_reportada," +
                                            "@fecha_inicio_informacion_reportada," +
                                            "@fecha_termino_informacion_reportada," +
                                            "@periodo_reportado," +
                                            "@fecha_inicio_p," +
                                            "@fecha_termino_p," +
                                            "@sesiones_celebradas_p," +
                                            //"cond_celebracion_periodos_extraordinarios," +
                                            "@periodos_extraordinarios_celebrados," +
                                            "@periodo_extraordinario_reportado," +
                                            "@fecha_inicio_pe," +
                                            "@fecha_termino_pe," +
                                            "@sesiones_celebradas_pe," +
                                            //"cond_reconocimiento_iniciativa_p," +
                                            //"cond_reconocimiento_iniciativa_urgente_obvia," +
                                            //"cond_existencia_juicio_politico," +
                                            //"cond_existencia_declaracion_procedencia," +
                                            //"cond_existencia_comparecencia," +
                                            "@fecha_actualizacion," +
                                            "@periodo_reportado_rec," +
                                    "@fecha_inicio_p_rec," +
                                    "@fecha_termino_p_rec," +
                                    "@sesiones_celebradas_p_rec)";

                                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                                        {
                                            // Variables individuales
                                            command.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text);
                                            command.Parameters.AddWithValue("@entidad_federativa", cmb_entidad_federativa.Text);
                                            command.Parameters.AddWithValue("@agee", txt_agee.Text);
                                            command.Parameters.AddWithValue("@numero_legislatura", cmb_numero_legislatura.Text);
                                            command.Parameters.AddWithValue("@nombre_legislatura", txt_nombre_legislatura.Text);
                                            command.Parameters.AddWithValue("@inicio_funciones_legislatura", dtp_inicio_funciones_legislatura.Text);
                                            command.Parameters.AddWithValue("@termino_funciones_legislatura", dtp_termino_funciones_legislatura.Text);
                                            command.Parameters.AddWithValue("@distritos_uninominales", Txt_distritos_uninominales.Text);
                                            command.Parameters.AddWithValue("@diputaciones_plurinominales", Txt_diputaciones_plurinominales.Text);
                                            command.Parameters.AddWithValue("@ejercicio_constitucional_informacion_reportada", cmb_ejercicio_constitucional_informacion_reportada.Text);
                                            command.Parameters.AddWithValue("@fecha_inicio_informacion_reportada", dtp_fecha_inicio_informacion_reportada.Text);
                                            command.Parameters.AddWithValue("@fecha_termino_informacion_reportada", dtp_fecha_termino_informacion_reportada.Text);
                                            command.Parameters.AddWithValue("@periodo_reportado", cmb_periodo_reportado_po.Text);
                                            command.Parameters.AddWithValue("@fecha_inicio_p", dtp_fecha_inicio_po.Text);
                                            command.Parameters.AddWithValue("@fecha_termino_p", dtp_fecha_termino_po.Text);
                                            command.Parameters.AddWithValue("@sesiones_celebradas_p", Txt_sesiones_celebradas_po.Text);
                                            command.Parameters.AddWithValue("@periodos_extraordinarios_celebrados", txt_periodos_extraordinarios_celebrados.Text);
                                            command.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                            command.Parameters.AddWithValue("@periodo_reportado_rec", txt_periodo_reportado_rec.Text);
                                            command.Parameters.AddWithValue("@fecha_inicio_p_rec", dtp_fecha_inicio_p_rec.Text);
                                            command.Parameters.AddWithValue("@fecha_termino_p_rec", dtp_fecha_termino_p_rec.Text);
                                            command.Parameters.AddWithValue("@sesiones_celebradas_p_rec", txt_sesiones_celebradas_p_rec.Text);

                                            // Variables del dgv
                                            command.Parameters.AddWithValue("@periodo_extraordinario_reportado", row.Cells["periodo_reportado_pe"].Value);
                                            command.Parameters.AddWithValue("@fecha_inicio_pe", row.Cells["fecha_inicio_pe"].Value);
                                            command.Parameters.AddWithValue("@fecha_termino_pe", row.Cells["fecha_termino_pe"].Value);
                                            command.Parameters.AddWithValue("@sesiones_celebradas_pe", row.Cells["sesiones_celebradas_pe"].Value);


                                            command.ExecuteNonQuery();
                                        }
                                    }

                                }
                            }
                            connection.Close();
                        }

                        // Se desabilitan los campos pero se queda la información.
                        txt_nombre_legislatura.Enabled = false; txt_nombre_legislatura.BackColor = Color.LightGray;
                        cmb_entidad_federativa.Enabled = false; cmb_numero_legislatura.Enabled = false;
                        dtp_fecha_inicio_informacion_reportada.Enabled = false;
                        dtp_fecha_termino_informacion_reportada.Enabled = false;
                        cmb_ejercicio_constitucional_informacion_reportada.Enabled = false;
                        Txt_distritos_uninominales.Enabled = false; Txt_distritos_uninominales.BackColor = Color.LightGray;
                        Txt_diputaciones_plurinominales.Enabled = false; Txt_diputaciones_plurinominales.BackColor = Color.LightGray;
                        txt_periodo_reportado_rec.Enabled = false; BackColor = Color.LightGray;
                        Txt_sesiones_celebradas_pe.Enabled = false; Txt_sesiones_celebradas_pe.BackColor = Color.LightGray;
                        txt_sesiones_celebradas_p_rec.Enabled = false; txt_sesiones_celebradas_p_rec.BackColor = Color.LightGray;
                        dgvPE.Enabled = false; dgvPE.BackgroundColor = Color.LightGray;
                        btnAgregarPE.Enabled = false; BtnEliminarPE.Enabled = false;
                        cmb_periodo_reportado_po.Enabled = false; cmb_periodo_reportado_po.BackColor = Color.LightGray;
                        dtp_fecha_inicio_po.Enabled = false;
                        dtp_fecha_termino_po.Enabled = false;
                        dtp_fecha_inicio_pe.Enabled = false;
                        dtp_fecha_termino_pe.Enabled = false;
                        Txt_sesiones_celebradas_po.Enabled = false; Txt_sesiones_celebradas_po.BackColor = Color.LightGray;
                        txt_periodos_extraordinarios_celebrados.Enabled = false; txt_periodos_extraordinarios_celebrados.BackColor = Color.LightGray;
                        chbPE.Enabled = false;
                        cmb_periodo_extraordinario_reportado.Enabled = false; cmb_periodo_extraordinario_reportado.BackColor = Color.LightGray;
                        dtp_inicio_funciones_legislatura.Enabled = false;
                        dtp_termino_funciones_legislatura.Enabled = false;

                        // SE HABILITAN LOS CONTROLES DE LAS PESTAÑAS
                        EnableTab(tabPageCL);
                        txt_ID_comision_legislativa.Enabled = false; txt_ID_comision_legislativa.BackColor = Color.LightGray;
                        EnableTab(tabPagePL);
                        txt_ID_persona_legisladora.Enabled = false;

                        MessageBox.Show("Datos guardados correctamente");

                        // this.Close(); //CIERRA EL FORMULARIO ACTUAL
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
            else
            {

            }

            
        }
        private void BtnEliminarPE_Click_1(object sender, EventArgs e)
        {
            if (dgvPE.SelectedRows.Count > 0)
            {
                dgvPE.Rows.RemoveAt(dgvPE.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }
        private void construccion_id_legislatura()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            // se obtiene cve_ent
            string valorComboBox1 = cmb_entidad_federativa.Text.ToString();
            string CVE_ENT = "";
            using (SQLiteConnection con = new SQLiteConnection(cadena))
            {
                con.Open();
                string query = "SELECT distinct cve_ent FROM TC_AGEEM WHERE nom_ent = @valorComboBox1";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@valorComboBox1", valorComboBox1);
                CVE_ENT = cmd.ExecuteScalar()?.ToString();
                con.Close();
            }

            // se obtiene periodo reportado
            string valorComboBox3 = cmb_periodo_reportado_po.Text.ToString();
            string valortxt = txt_periodo_reportado_rec.Text;
            string PR = "";

            if (valorComboBox3 == "Primer periodo ordinario" & valortxt == "Primer periodo de receso")
            {
                PR = "1_1";
            }
            else if (valorComboBox3 == "Segundo periodo ordinario" & valortxt == "Segundo periodo de receso") 
            {
                PR = "2_2";
            }
            else if(valorComboBox3 == "Tercer periodo ordinario" & valortxt == "Tercer periodo de receso")
            {
                PR = "3_3";
            }
            
            /*
            using (SQLiteConnection con = new SQLiteConnection(cadena))
            {
                con.Open();
                string query = "SELECT distinct abr_pr FROM TC_CALENDARIO_SESIONES WHERE periodos_reportar = @valorComboBox3";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@valorComboBox3", valorComboBox3);
                PR = cmd.ExecuteScalar()?.ToString();
                con.Close();
            }
            */

            // se obtiene numero de legislatura
            string NL = cmb_numero_legislatura.Text.ToString();

            // se obtiene el ejercicio constitucional
            string valorEC = cmb_ejercicio_constitucional_informacion_reportada.Text.ToString();
            string EC = "";
            using (SQLiteConnection con = new SQLiteConnection(cadena))
            {
                con.Open();
                string query = "SELECT distinct abr_ec FROM TC_CALENDARIO_SESIONES  WHERE ejercicio_constitucional = @valorEC";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@valorEC", valorEC);
                EC = cmd.ExecuteScalar()?.ToString();
                con.Close();
            }

            // Concatenar ID

            string resultadoConcatenado = CVE_ENT + "_" + NL + "_" + EC + "_" + PR;

            // Se muestra el ID y AGEE
            txt_id_legislatura.Text = resultadoConcatenado;
            txt_agee.Text = CVE_ENT;
        }
        
        // cmb_entidad_federativa
        private void cmb_entidad_federativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            // CONSTRUCCION ID----------------------------------------------------------------------------------------------    
            string cadena = "Data Source = DB_PLE.db;Version=3;";
            construccion_id_legislatura();

            // SE LLENA EL COMBOBOX QUE DEPENDE DE LA ENTIDAD PARA LLENAR COMBOBO LEGISLATURA-----------------------------------
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                // Verifica que haya una selección 
                if (cmb_entidad_federativa.SelectedItem != null)
                {
                    try
                    {
                        // se obtiene el objeto DataRowView seleccionado
                        DataRowView rowView = cmb_entidad_federativa.SelectedItem as DataRowView;

                        if (rowView != null)
                        {
                            // Se obtiene el valor de nom_ent de la tabla TC_AGEEM
                            string entidad_federativa = rowView["nom_ent"].ToString();

                            conexion.Open();

                            // Consulta SQL para obtener datos del cmb de entidad federativa y extraer la legislatura------------------------------
                            string query = "select distinct legislatura from TC_CALENDARIO_SESIONES WHERE entidad = @entidad_federativa";
                            using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                            {
                                cmd.Parameters.AddWithValue("@entidad_federativa", entidad_federativa);

                                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                                DataTable table = new DataTable();
                                adapter.Fill(table);

                                cmb_numero_legislatura.DisplayMember = "legislatura";
                                cmb_numero_legislatura.ValueMember = "legislatura";
                                cmb_numero_legislatura.DataSource = table;

                                cmb_numero_legislatura.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                                cmb_numero_legislatura.AutoCompleteSource = AutoCompleteSource.ListItems;

                                cmb_numero_legislatura.DropDownStyle = ComboBoxStyle.DropDown;
                            }

                            // Consulta SQL para obtener datos del cmb de entidad federativa y extraer inicio legislatura------------------------------
                            string query2 = "select distinct inicio_legislatura from TC_CALENDARIO_SESIONES WHERE entidad = @entidad_federativa";
                            using (SQLiteCommand cmd2 = new SQLiteCommand(query2, conexion))
                            {
                                cmd2.Parameters.AddWithValue("@entidad_federativa", entidad_federativa);

                                object resultado = cmd2.ExecuteScalar();

                                if (DateTime.TryParse(resultado.ToString(), out DateTime inicioLegislatura))
                                {
                                    dtp_inicio_funciones_legislatura.Value = inicioLegislatura;
                                }
                                else
                                {
                                    // Manejo de error si no se puede convertir el resultado a DateTime
                                    MessageBox.Show("No se pudo convertir el valor de inicio de legislatura a DateTime.");
                                }
                            }
                            // Consulta SQL para obtener datos del cmb de entidad federativa y extraer inicio legislatura------------------------------
                            string query3 = "select distinct fin_legislatura from TC_CALENDARIO_SESIONES WHERE entidad = @entidad_federativa";
                            using (SQLiteCommand cmd3 = new SQLiteCommand(query3, conexion))
                            {
                                cmd3.Parameters.AddWithValue("@entidad_federativa", entidad_federativa);

                                object resultado = cmd3.ExecuteScalar();

                                if (DateTime.TryParse(resultado.ToString(), out DateTime finLegislatura))
                                {
                                    dtp_termino_funciones_legislatura.Value = finLegislatura;
                                }
                                else
                                {
                                    // Manejo de error si no se puede convertir el resultado a DateTime
                                    MessageBox.Show("No se pudo convertir el valor de inicio de legislatura a DateTime.");
                                }
                            }
                            // Consulta SQL para obtener datos del cmb de entidad federativa y extraer la año legislativo------------------------------
                            string query4 = "select distinct ejercicio_constitucional from TC_CALENDARIO_SESIONES WHERE entidad = @entidad_federativa";
                            using (SQLiteCommand cmd4 = new SQLiteCommand(query4, conexion))
                            {
                                cmd4.Parameters.AddWithValue("@entidad_federativa", entidad_federativa);

                                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd4);
                                DataTable table = new DataTable();
                                adapter.Fill(table);

                                cmb_ejercicio_constitucional_informacion_reportada.DisplayMember = "ejercicio_constitucional";
                                cmb_ejercicio_constitucional_informacion_reportada.ValueMember = "ejercicio_constitucional";
                                cmb_ejercicio_constitucional_informacion_reportada.DataSource = table;

                                cmb_ejercicio_constitucional_informacion_reportada.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                                cmb_ejercicio_constitucional_informacion_reportada.AutoCompleteSource = AutoCompleteSource.ListItems;

                                cmb_ejercicio_constitucional_informacion_reportada.DropDownStyle = ComboBoxStyle.DropDown;
                            }

         
                            // fecha del periodo de receso inicio y fin
                            string ejerc_const = cmb_ejercicio_constitucional_informacion_reportada.Text;
                            string periodo_receso = txt_periodo_reportado_rec.Text;
                          
                                string query5 = "select distinct inicio_pr from TC_CALENDARIO_SESIONES WHERE entidad = @entidad_federativa " +
                                    "AND ejercicio_constitucional = @ejerc_const " +
                                    "AND periodos_reportar = @periodo_receso;";

                                using (SQLiteCommand cmd5 = new SQLiteCommand(query5, conexion))
                                {
                                    cmd5.Parameters.AddWithValue("@entidad_federativa", entidad_federativa);
                                    cmd5.Parameters.AddWithValue("@ejerc_const", ejerc_const);
                                    cmd5.Parameters.AddWithValue("@periodo_receso", periodo_receso);

                                    object resultado = cmd5.ExecuteScalar();

                                    if (resultado != null && DateTime.TryParse(resultado.ToString(), out DateTime inicioReceso))
                                    {
                                        dtp_fecha_inicio_p_rec.Value = inicioReceso;
                                    }
                                    else
                                    {

                                    }
                                }

                            string query6 = "select distinct fin_pr from TC_CALENDARIO_SESIONES WHERE entidad = @entidad_federativa " +
                                "AND ejercicio_constitucional = @ejerc_const " +
                                "AND periodos_reportar = @periodo_receso;";

                            using (SQLiteCommand cmd6 = new SQLiteCommand(query6, conexion))
                            {
                                cmd6.Parameters.AddWithValue("@entidad_federativa", entidad_federativa);
                                cmd6.Parameters.AddWithValue("@ejerc_const", ejerc_const);
                                cmd6.Parameters.AddWithValue("@periodo_receso", periodo_receso);

                                object resultado = cmd6.ExecuteScalar();

                                if (resultado != null && DateTime.TryParse(resultado.ToString(), out DateTime inicioReceso))
                                {
                                    dtp_fecha_termino_p_rec.Value = inicioReceso;
                                }
                                else
                                {

                                }
                            }
                            conexion.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }

            }


        }
        private void cmb_entidad_federativa_Validating(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;

            if (comboBox != null)
            {
                // Quitar espacios en blanco del texto ingresado y convertir a minúsculas
                string cleanedText = comboBox.Text.Trim().Replace(" ", string.Empty).ToLower();

                // Verificar si el texto del ComboBox coincide con alguna de las opciones
                bool isValid = false;

                foreach (DataRowView item in comboBox.Items)
                {
                    string cleanedItem = item["nom_ent"].ToString().Trim().Replace(" ", string.Empty).ToLower(); // nom_ent: nombre de la columna de la tabla
                    if (cleanedText == cleanedItem)
                    {
                        isValid = true;
                        break;
                    }
                    // Mostrar el valor actual de item (para depuración)
                    Console.WriteLine("Current item: " + item["nom_ent"]);
                    // O usar Debug.WriteLine si estás depurando
                    System.Diagnostics.Debug.WriteLine("Current item: " + item["nom_ent"]);
                }

                if (!isValid)
                {
                    // Mostrar mensaje de error
                    MessageBox.Show("Por favor, seleccione una opción válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Borrar el contenido del ComboBox
                    comboBox.Text = string.Empty;

                    // Evitar que el control pierda el foco
                    e.Cancel = true;
                }
            }
        }

        // cmb_numero_legislatura
        private void cmb_numero_legislatura_Validating(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;

            if (comboBox != null)
            {
                // Quitar espacios en blanco del texto ingresado y convertir a minúsculas
                string cleanedText = comboBox.Text.Trim().Replace(" ", string.Empty).ToLower();

                // Verificar si el texto del ComboBox coincide con alguna de las opciones
                bool isValid = false;

                foreach (DataRowView item in comboBox.Items)
                {
                    string cleanedItem = item["legislatura"].ToString().Trim().Replace(" ", string.Empty).ToLower(); // nom_ent: nombre de la columna de la tabla
                    if (cleanedText == cleanedItem)
                    {
                        isValid = true;
                        break;
                    }
                    // Mostrar el valor actual de item (para depuración)
                    Console.WriteLine("Current item: " + item["legislatura"]);
                    // O usar Debug.WriteLine si estás depurando
                    System.Diagnostics.Debug.WriteLine("Current item: " + item["legislatura"]);
                }

                if (!isValid)
                {
                    // Mostrar mensaje de error
                    MessageBox.Show("Por favor, seleccione una opción válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Borrar el contenido del ComboBox
                    comboBox.Text = string.Empty;

                    // Evitar que el control pierda el foco
                    e.Cancel = true;
                }
            }
        }

        // cmb_ejercicio_constitucional_informacion_reportada
        private void cmb_ejercicio_constitucional_informacion_reportada_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            // construccion id
            construccion_id_legislatura();

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                // Verifica que haya una selección 
                if (cmb_ejercicio_constitucional_informacion_reportada.SelectedItem != null)
                {
                    try
                    {
                        // se obtiene el objeto DataRowView seleccionado
                        DataRowView rowView = cmb_ejercicio_constitucional_informacion_reportada.SelectedItem as DataRowView;
                        DataRowView rowView2 = cmb_entidad_federativa.SelectedItem as DataRowView;


                        if (rowView != null & rowView2 != null)
                        {
                            // Se obtiene el valor de ejercicio_constitucional de la tabla TC_CALENDARIO_SESIONES
                            string ec = rowView["ejercicio_constitucional"].ToString();
                            string ent = rowView2["nom_ent"].ToString();

                            conexion.Open();

                            // Consulta SQL para obtener datos del cmb de entidad federativa y extraer inicio legislatura------------------------------

                            string query2 = "select distinct inicio_ec from TC_CALENDARIO_SESIONES" +
                            " WHERE ejercicio_constitucional = @ec and entidad = @ent";
                            using (SQLiteCommand cmd2 = new SQLiteCommand(query2, conexion))
                            {
                                cmd2.Parameters.AddWithValue("@ec", ec);
                                cmd2.Parameters.AddWithValue("@ent", ent);

                                object resultado = cmd2.ExecuteScalar();

                                if (DateTime.TryParse(resultado.ToString(), out DateTime inicio_ec))
                                {
                                    dtp_fecha_inicio_informacion_reportada.Value = inicio_ec;
                                }
                                else
                                {
                                    // Manejo de error si no se puede convertir el resultado a DateTime
                                    MessageBox.Show("No se pudo convertir el valor de inicio de legislatura a DateTime.");
                                }
                            }

                            // Consulta SQL para obtener datos del cmb de entidad federativa y extraer fin legislatura------------------------------

                            string query3 = "select distinct fin_ec from TC_CALENDARIO_SESIONES WHERE ejercicio_constitucional = @ec and entidad = @ent";
                            using (SQLiteCommand cmd3 = new SQLiteCommand(query3, conexion))
                            {
                                cmd3.Parameters.AddWithValue("@ec", ec);
                                cmd3.Parameters.AddWithValue("@ent", ent);


                                object resultado = cmd3.ExecuteScalar();

                                if (DateTime.TryParse(resultado.ToString(), out DateTime fin_ec))
                                {
                                    dtp_fecha_termino_informacion_reportada.Value = fin_ec;
                                }
                                else
                                {
                                    // Manejo de error si no se puede convertir el resultado a DateTime
                                    MessageBox.Show("No se pudo convertir el valor de inicio de legislatura a DateTime.");
                                }
                            }

                            // Consulta SQL para obtener datos del cmb de entidad federativa y extraer la periodo reportado------------------------------


                            string query4 = "select  distinct periodos_reportar from TC_CALENDARIO_SESIONES WHERE ejercicio_constitucional = @ec " +
                                "AND entidad = @ent " +
                                "AND abr_pr in ('1O','2O','3O')";
                            using (SQLiteCommand cmd4 = new SQLiteCommand(query4, conexion))
                            {
                                cmd4.Parameters.AddWithValue("@ec", ec);
                                cmd4.Parameters.AddWithValue("@ent", ent);

                                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd4);
                                DataTable table = new DataTable();
                                adapter.Fill(table);

                                cmb_periodo_reportado_po.DisplayMember = "periodos_reportar";
                                cmb_periodo_reportado_po.ValueMember = "periodos_reportar";
                                cmb_periodo_reportado_po.DataSource = table;

                                cmb_periodo_reportado_po.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                                cmb_periodo_reportado_po.AutoCompleteSource = AutoCompleteSource.ListItems;
                                cmb_periodo_reportado_po.DropDownStyle = ComboBoxStyle.DropDown;

                            }

                            // fecha del periodo de receso inicio y fin
                            string ejerc_const = cmb_ejercicio_constitucional_informacion_reportada.Text;
                            string periodo_receso = txt_periodo_reportado_rec.Text;

                            string query5 = "select distinct inicio_pr from TC_CALENDARIO_SESIONES WHERE entidad = @ent " +
                                "AND ejercicio_constitucional = @ejerc_const " +
                                "AND periodos_reportar = @periodo_receso;";

                            using (SQLiteCommand cmd5 = new SQLiteCommand(query5, conexion))
                            {
                                cmd5.Parameters.AddWithValue("@ent", ent);
                                cmd5.Parameters.AddWithValue("@ejerc_const", ejerc_const);
                                cmd5.Parameters.AddWithValue("@periodo_receso", periodo_receso);

                                object resultado = cmd5.ExecuteScalar();

                                if (resultado != null && DateTime.TryParse(resultado.ToString(), out DateTime inicioReceso))
                                {
                                    dtp_fecha_inicio_p_rec.Value = inicioReceso;
                                }
                                else
                                {

                                }
                            }

                            string query6 = "select distinct fin_pr from TC_CALENDARIO_SESIONES WHERE entidad = @ent " +
                                "AND ejercicio_constitucional = @ejerc_const " +
                                "AND periodos_reportar = @periodo_receso;";

                            using (SQLiteCommand cmd6 = new SQLiteCommand(query6, conexion))
                            {
                                cmd6.Parameters.AddWithValue("@ent", ent);
                                cmd6.Parameters.AddWithValue("@ejerc_const", ejerc_const);
                                cmd6.Parameters.AddWithValue("@periodo_receso", periodo_receso);

                                object resultado = cmd6.ExecuteScalar();

                                if (resultado != null && DateTime.TryParse(resultado.ToString(), out DateTime inicioReceso))
                                {
                                    dtp_fecha_termino_p_rec.Value = inicioReceso;
                                }
                                else
                                {

                                }
                            }

                            conexion.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }

            }
        }
        private void cmb_ejercicio_constitucional_informacion_reportada_Validating(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;

            if (comboBox != null)
            {
                // Quitar espacios en blanco del texto ingresado y convertir a minúsculas
                string cleanedText = comboBox.Text.Trim().Replace(" ", string.Empty).ToLower();

                // Verificar si el texto del ComboBox coincide con alguna de las opciones
                bool isValid = false;

                foreach (DataRowView item in comboBox.Items)
                {
                    string cleanedItem = item["ejercicio_constitucional"].ToString().Trim().Replace(" ", string.Empty).ToLower(); // nom_ent: nombre de la columna de la tabla
                    if (cleanedText == cleanedItem)
                    {
                        isValid = true;
                        break;
                    }
                    // Mostrar el valor actual de item (para depuración)
                    Console.WriteLine("Current item: " + item["ejercicio_constitucional"]);
                    // O usar Debug.WriteLine si estás depurando
                    System.Diagnostics.Debug.WriteLine("Current item: " + item["ejercicio_constitucional"]);
                }

                if (!isValid)
                {
                    // Mostrar mensaje de error
                    MessageBox.Show("Por favor, seleccione una opción válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Borrar el contenido del ComboBox
                    comboBox.Text = string.Empty;

                    // Evitar que el control pierda el foco
                    e.Cancel = true;
                }
            }
        }

        // cmb_periodo_reportado_po
        private void cmb_periodo_reportado_po_SelectedIndexChanged(object sender, EventArgs e)
        {

            string cadena = "Data Source = DB_PLE.db;Version=3;";

            // construccion id
            construccion_id_legislatura();

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                // Verifica que haya una selección 
                if (cmb_periodo_reportado_po.SelectedItem != null)
                {
                    try
                    {
                        // se obtiene el objeto DataRowView seleccionado
                        DataRowView rowView = cmb_periodo_reportado_po.SelectedItem as DataRowView;
                        DataRowView rowView2 = cmb_entidad_federativa.SelectedItem as DataRowView;
                        DataRowView rowView3 = cmb_ejercicio_constitucional_informacion_reportada.SelectedItem as DataRowView;


                        if (rowView != null & rowView2 != null & rowView3 != null)
                        {
                            // Se obtiene el valor de ejercicio_constitucional de la tabla TC_CALENDARIO_SESIONES
                            string pr = rowView["periodos_reportar"].ToString();
                            string ent = rowView2["nom_ent"].ToString();
                            string ec = rowView3["ejercicio_constitucional"].ToString();


                            conexion.Open();

                            // Consulta SQL para obtener datos del cmb de periodos reportar y extraer inicio pr------------------------------

                            string query1 = "select distinct inicio_pr from TC_CALENDARIO_SESIONES " +
                                "WHERE periodos_reportar = @pr and entidad = @ent and ejercicio_constitucional = @ec";
                            using (SQLiteCommand cmd1 = new SQLiteCommand(query1, conexion))
                            {
                                cmd1.Parameters.AddWithValue("@pr", pr);
                                cmd1.Parameters.AddWithValue("@ent", ent);
                                cmd1.Parameters.AddWithValue("@ec", ec);

                                object resultado = cmd1.ExecuteScalar();

                                if (DateTime.TryParse(resultado.ToString(), out DateTime inicio_pr))
                                {
                                    dtp_fecha_inicio_po.Value = inicio_pr;
                                }
                                else
                                {
                                    // Manejo de error si no se puede convertir el resultado a DateTime
                                    MessageBox.Show("No se pudo convertir el valor de inicio de legislatura a DateTime.");
                                }
                            }

                            // Consulta SQL para obtener datos del cmb de periodos reportar y extraer fin pr------------------------------

                            string query2 = "select distinct fin_pr from TC_CALENDARIO_SESIONES " +
                                "WHERE periodos_reportar = @pr and entidad = @ent and ejercicio_constitucional = @ec";
                            using (SQLiteCommand cmd2 = new SQLiteCommand(query2, conexion))
                            {
                                cmd2.Parameters.AddWithValue("@pr", pr);
                                cmd2.Parameters.AddWithValue("@ent", ent);
                                cmd2.Parameters.AddWithValue("@ec", ec);

                                object resultado = cmd2.ExecuteScalar();

                                if (DateTime.TryParse(resultado.ToString(), out DateTime inicio_pr))
                                {
                                    dtp_fecha_termino_po.Value = inicio_pr;
                                }
                                else
                                {
                                    // Manejo de error si no se puede convertir el resultado a DateTime
                                    MessageBox.Show("No se pudo convertir el valor de inicio de legislatura a DateTime.");
                                }
                            }

                            // Se asigna en el txt periodo de receso dependiendo del periodo reportado y se restringe dependiendo la entidad
                            string per_ord = cmb_periodo_reportado_po.Text;
                            string ent_rep = cmb_entidad_federativa.Text;

                            if (ent_rep == "Baja California" || ent_rep == "Jalisco")
                            {
                                txt_periodo_reportado_rec.Text = "";
                                dtp_fecha_inicio_p_rec.Enabled = false; dtp_fecha_inicio_p_rec.Value = new DateTime(1899, 9, 9);
                                dtp_fecha_termino_p_rec.Enabled = false; dtp_fecha_termino_p_rec.Value = new DateTime(1899, 9, 9);
                                txt_sesiones_celebradas_p_rec.Enabled = false; txt_sesiones_celebradas_p_rec.BackColor = Color.LightGray;
                                txt_sesiones_celebradas_p_rec.Text = "";
                                chbPE.Enabled = false;

                            }
                            else
                            {
                                dtp_fecha_inicio_p_rec.Enabled = true; dtp_fecha_inicio_p_rec.Value = new DateTime(1899, 9, 9);
                                dtp_fecha_termino_p_rec.Enabled = true; dtp_fecha_termino_p_rec.Value = new DateTime(1899, 9, 9);
                                txt_sesiones_celebradas_p_rec.Enabled = true; txt_sesiones_celebradas_p_rec.BackColor = Color.Honeydew;
                                txt_sesiones_celebradas_p_rec.Text = "";
                                chbPE.Enabled = true;

                                if (per_ord == "Primer periodo ordinario")
                                {
                                    txt_periodo_reportado_rec.Text = "Primer periodo de receso";
                                }
                                else if (per_ord == "Segundo periodo ordinario")
                                {
                                    txt_periodo_reportado_rec.Text = "Segundo periodo de receso";
                                }
                                else if (per_ord == "Tercer periodo ordinario")
                                {
                                    txt_periodo_reportado_rec.Text = "Tercer periodo de receso";
                                }
                            }


                            
                            
                            conexion.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }

            }

        }
        private void cmb_periodo_reportado_po_Validating(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;

            if (comboBox != null)
            {
                // Quitar espacios en blanco del texto ingresado y convertir a minúsculas
                string cleanedText = comboBox.Text.Trim().Replace(" ", string.Empty).ToLower();

                // Verificar si el texto del ComboBox coincide con alguna de las opciones
                bool isValid = false;

                foreach (DataRowView item in comboBox.Items)
                {
                    string cleanedItem = item["periodos_reportar"].ToString().Trim().Replace(" ", string.Empty).ToLower(); // nom_ent: nombre de la columna de la tabla
                    if (cleanedText == cleanedItem)
                    {
                        isValid = true;
                        break;
                    }
                    // Mostrar el valor actual de item (para depuración)
                    Console.WriteLine("Current item: " + item["periodos_reportar"]);
                    // O usar Debug.WriteLine si estás depurando
                    System.Diagnostics.Debug.WriteLine("Current item: " + item["periodos_reportar"]);
                }

                if (!isValid)
                {
                    // Mostrar mensaje de error
                    MessageBox.Show("Por favor, seleccione una opción válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Borrar el contenido del ComboBox
                    comboBox.Text = string.Empty;

                    // Evitar que el control pierda el foco
                    e.Cancel = true;
                }
            }
        }

        private void txt_periodos_extraordinarios_celebrados_TextChanged(object sender, EventArgs e)
        {
            int valorTextBox;

            // Verificar si el valor del TextBox es un número válido
            if (int.TryParse(txt_periodos_extraordinarios_celebrados.Text, out valorTextBox))
            {
                // Delimitar el valor del ComboBox según el valor del TextBox
                if (valorTextBox >= 1 && valorTextBox <= 10)
                {
                    // Limpiar el ComboBox antes de agregar nuevos elementos
                    cmb_periodo_extraordinario_reportado.Items.Clear();

                    // Llenar el ComboBox con los elementos del 1 al valor del TextBox
                    for (int i = 1; i <= valorTextBox; i++)
                    {
                        string cadena = "Data Source = DB_PLE.db;Version=3;";

                        using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                        {
                            try
                            {
                                // abrir la conexion
                                conexion.Open();

                                // comando de sql con filtro
                                string query = "select descripcion from TC_PERIODO_EXT where id_periodo_ext = @id";
                                SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                                cmd.Parameters.AddWithValue("@id", i);

                                // Utilizar un DataReader para obtener los datos
                                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);

                                // Agregar los elementos del DataTable al ComboBox
                                foreach (DataRow row in dataTable.Rows)
                                {
                                    cmb_periodo_extraordinario_reportado.Items.Add(row["descripcion"].ToString());
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
                }
                else
                {
                    // Si el valor del TextBox está fuera del rango permitido, mostrar un mensaje de error
                    MessageBox.Show("El valor debe estar entre 1 y 10");
                }
            }
            else
            {
                // Si el valor del TextBox no es un número válido, mostrar un mensaje de error
                //MessageBox.Show("Ingrese un número válido");
            }
        }
        private bool IsDuplicateRecord(string periodo_reportado_pe)
        {
            foreach (DataGridViewRow row in dgvPE.Rows)
            {
                if (row.IsNewRow) continue; // Skip the new row placeholder

                string existingId = row.Cells["periodo_reportado_pe"].Value.ToString();

                if (existingId == periodo_reportado_pe)
                {
                    return true;
                }
            }
            return false;
        }
        private void txt_nombre_legislatura_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_nombre_legislatura.Text = txt_nombre_legislatura.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_nombre_legislatura.SelectionStart = txt_nombre_legislatura.Text.Length;
        }
        private void Txt_distritos_uninominales_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada es un número o una tecla de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Si no es un número ni una tecla de control, cancela la entrada
                e.Handled = true;

                // Muestra una ventana emergente informando al usuario que solo se permiten números
                MessageBox.Show("Solo se permiten valores numéricos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Si el carácter es un número, realizamos una validación adicional
                System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;

                // Obtén el texto actual del TextBox y añádele el carácter presionado
                string newText = textBox.Text.Insert(textBox.SelectionStart, e.KeyChar.ToString());

                // Intenta convertir el nuevo texto a un número
                if (int.TryParse(newText, out int result))
                {
                    // Verifica si el número es mayor que 1
                    if (result < 1)
                    {
                        // Si el número es menor o igual a 1, cancela la entrada
                        e.Handled = true;

                        // Muestra una ventana emergente informando al usuario que solo se permiten valores mayores a 1
                        MessageBox.Show("Solo se permiten valores mayores a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void Txt_diputaciones_plurinominales_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada es un número o una tecla de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Si no es un número ni una tecla de control, cancela la entrada
                e.Handled = true;

                // Muestra una ventana emergente informando al usuario que solo se permiten números
                MessageBox.Show("Solo se permiten valores numéricos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Txt_sesiones_celebradas_po_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada es un número o una tecla de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Si no es un número ni una tecla de control, cancela la entrada
                e.Handled = true;

                // Muestra una ventana emergente informando al usuario que solo se permiten números
                MessageBox.Show("Solo se permiten valores numéricos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Txt_sesiones_celebradas_pe_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada es un número o una tecla de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Si no es un número ni una tecla de control, cancela la entrada
                e.Handled = true;

                // Muestra una ventana emergente informando al usuario que solo se permiten números
                MessageBox.Show("Solo se permiten valores numéricos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txt_sesiones_celebradas_p_rec_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada es un número o una tecla de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Si no es un número ni una tecla de control, cancela la entrada
                e.Handled = true;

                // Muestra una ventana emergente informando al usuario que solo se permiten números
                MessageBox.Show("Solo se permiten valores numéricos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txt_periodos_extraordinarios_celebrados_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada es un número o una tecla de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Si no es un número ni una tecla de control, cancela la entrada
                e.Handled = true;

                // Muestra una ventana emergente informando al usuario que solo se permiten números
                MessageBox.Show("Solo se permiten valores numéricos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Dtp_fecha_inicio_informacion_reportada_ValueChanged_1(object sender, EventArgs e)
        {
            /*
            if (dtp_fecha_inicio_informacion_reportada.Value < dtp_inicio_funciones_legislatura.Value ||
                dtp_fecha_inicio_informacion_reportada.Value > dtp_termino_funciones_legislatura.Value)
            {
                MessageBox.Show("La fecha debe estar entre el inicio y término de funciones", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                dtp_fecha_inicio_informacion_reportada.Value = dtp_inicio_funciones_legislatura.Value.AddDays(1);
                dtp_fecha_inicio_informacion_reportada.Focus();
            }
            */
        }
        private void dtp_fecha_inicio_po_ValueChanged_1(object sender, EventArgs e)
        {



        }
        private bool ValidacionCampos_DG()
        {
            // Array de controles a validar
            Control[] controlesAValidar;

            string ent_rep = cmb_entidad_federativa.Text;


            if (chbPE.Checked)
            {
                if (ent_rep == "Baja California" || ent_rep == "Jalisco")
                {
                    controlesAValidar = new Control[]  {
                        txt_nombre_legislatura, Txt_sesiones_celebradas_po,
                        Txt_distritos_uninominales,Txt_diputaciones_plurinominales,
                        txt_periodos_extraordinarios_celebrados
                    };
                }
                else
                {
                    controlesAValidar = new Control[]  {
                        txt_nombre_legislatura, Txt_sesiones_celebradas_po, txt_sesiones_celebradas_p_rec,
                        Txt_distritos_uninominales,Txt_diputaciones_plurinominales,
                        txt_periodos_extraordinarios_celebrados
                    };
                }
                
            }
            else
            {
                if (ent_rep == "Baja California" || ent_rep == "Jalisco")
                {
                    controlesAValidar = new Control[]  {
                        txt_nombre_legislatura, Txt_sesiones_celebradas_po,
                        Txt_distritos_uninominales,Txt_diputaciones_plurinominales
                    };
                }
                else
                {
                    controlesAValidar = new Control[]  {
                        txt_nombre_legislatura, Txt_sesiones_celebradas_po, txt_sesiones_celebradas_p_rec,
                        Txt_distritos_uninominales,Txt_diputaciones_plurinominales
                    };
                }
                
            }

            bool camposValidos = true;

            foreach (Control c in controlesAValidar)
            {
                // Asigna el evento GotFocus fuera del bucle
                c.GotFocus += Control_GotFocus;

                // Verificar si el control está vacío
                if (c is System.Windows.Forms.TextBox && string.IsNullOrWhiteSpace(c.Text))
                {
                    MessageBox.Show($"El campo {c.Name} está vacío.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    c.Focus(); // Enfocar el control vacío
                    camposValidos = false; // Marcar que hay campos inválidos
                    break; // Salir del bucle después de encontrar el primer campo vacío
                }
                else if (c is System.Windows.Forms.ComboBox && ((System.Windows.Forms.ComboBox)c).SelectedIndex == -1)
                {
                    MessageBox.Show($"Debe seleccionar una opción en {c.Name}.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    c.Focus(); // Enfocar el control vacío
                    camposValidos = false; // Marcar que hay campos inválidos
                    break; // Salir del bucle después de encontrar el primer campo vacío
                }
                // Agregar más validaciones según sea necesario para otros tipos de controles
            }
            

            // Validar DataGridView solo si chbPE.Checked es verdadero
            if (chbPE.Checked && dgvPE != null)
            {
                if (dgvPE.Rows.Count == 0 || dgvPE.Rows.Cast<DataGridViewRow>().All(row => row.IsNewRow))
                {
                    MessageBox.Show("No hay periodos extraordinarios registrados.", "Sin registros", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    camposValidos = false; // Marcar que hay campos inválidos
                }
            }

            return camposValidos;
        }

        private void chbPE_CheckedChanged_1(object sender, EventArgs e)
        {
            // Cuando el estado del CheckBox cambia, se ejecutará este código
            CheckBox chbPE = (CheckBox)sender;
            if (chbPE.Checked)
            {

                // Si el CheckBox está marcado
                dgvPE.Enabled = true; dgvPE.BackgroundColor = Color.Honeydew;
                cmb_periodo_extraordinario_reportado.Enabled = true; cmb_periodo_extraordinario_reportado.BackColor = Color.Honeydew;
                txt_periodos_extraordinarios_celebrados.Enabled = true; txt_periodos_extraordinarios_celebrados.BackColor = Color.Honeydew;
                Txt_sesiones_celebradas_pe.Enabled = true; Txt_sesiones_celebradas_pe.BackColor = Color.Honeydew;
                dtp_fecha_inicio_pe.Enabled = true; dtp_fecha_termino_pe.Enabled = true; 
                btnAgregarPE.Enabled = true; BtnEliminarPE.Enabled = true; 

                // se ajustan las fechas
                dtp_fecha_inicio_pe.Value = dtp_fecha_inicio_p_rec.Value; dtp_fecha_termino_pe.Value = dtp_fecha_termino_p_rec.Value;


            }
            else
            {
                // Si el CheckBox está desmarcado
                dgvPE.Enabled = false; dgvPE.BackgroundColor = Color.LightGray;
                cmb_periodo_extraordinario_reportado.Enabled = false; cmb_periodo_extraordinario_reportado.BackColor = Color.LightGray;
                Txt_sesiones_celebradas_pe.Enabled = false; Txt_sesiones_celebradas_pe.BackColor= Color.LightGray;
                txt_periodos_extraordinarios_celebrados.Enabled = false; txt_periodos_extraordinarios_celebrados.BackColor = Color.LightGray;
                dtp_fecha_inicio_pe.Enabled = false; dtp_fecha_termino_pe.Enabled = false; 
                btnAgregarPE.Enabled = false; BtnEliminarPE.Enabled = false; 
                dgvPE.Rows.Clear(); cmb_periodo_extraordinario_reportado.Items.Clear();

                // se ajustan las fechas
                dtp_fecha_inicio_pe.Value = new DateTime(1899, 9, 9); dtp_fecha_termino_pe.Value = new DateTime(1899, 9, 9);

            }
        }

        // cmb_periodo_extraordinario_reportado
        private void cmb_periodo_extraordinario_reportado_SelectedIndexChanged(object sender, EventArgs e)
        {
            // se ajustan las fechas
            dtp_fecha_inicio_pe.Value = dtp_fecha_inicio_p_rec.Value; dtp_fecha_termino_pe.Value = dtp_fecha_termino_p_rec.Value;
        }
        private void cmb_periodo_extraordinario_reportado_Validating(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;

            if (comboBox != null)
            {
                // Quitar espacios en blanco del texto ingresado y convertir a minúsculas
                string cleanedText = comboBox.Text.Trim().Replace(" ", string.Empty).ToLower();

                // Verificar si el texto del ComboBox coincide con alguna de las opciones
                bool isValid = false;
                foreach (var item in comboBox.Items)
                {
                    string cleanedItem = item.ToString().Trim().Replace(" ", string.Empty).ToLower();
                    if (cleanedText == cleanedItem)
                    {
                        isValid = true;
                        break;
                    }
                }

                if (!isValid)
                {
                    // Mostrar mensaje de error
                    MessageBox.Show("Por favor, seleccione una opción válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Borrar el contenido del ComboBox
                    comboBox.Text = string.Empty;

                    // Evitar que el control pierda el foco
                    e.Cancel = true;
                }
            }
        }

        // fecha inicio funciones legislatura
        private DateTime f1;
        private void dtp_inicio_funciones_legislatura_DropDown(object sender, EventArgs e)
        {
            f1 = dtp_inicio_funciones_legislatura.Value;
        }
        private void dtp_inicio_funciones_legislatura_CloseUp(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de MODIFICAR la fecha?", "Confirmacion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                dtp_inicio_funciones_legislatura.Value = f1;
            }
            
        }
        private void dtp_inicio_funciones_legislatura_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cancelar cualquier entrada manual
            e.Handled = true;
        }

        // fecha termino_funciones_legislatura
        private DateTime f2;
        private void dtp_termino_funciones_legislatura_DropDown(object sender, EventArgs e)
        {
            f2 = dtp_termino_funciones_legislatura.Value;

        }
        private void dtp_termino_funciones_legislatura_CloseUp(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de MODIFICAR la fecha?", "Confirmacion",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                dtp_termino_funciones_legislatura.Value = f2;
            }
        }
        private void dtp_termino_funciones_legislatura_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cancelar cualquier entrada manual
            e.Handled = true;
        }

        // fecha fecha_inicio_informacion_reportada
        private DateTime f3;
        private void dtp_fecha_inicio_informacion_reportada_DropDown(object sender, EventArgs e)
        {
            f3 = dtp_fecha_inicio_informacion_reportada.Value;
        }
        private void dtp_fecha_inicio_informacion_reportada_CloseUp(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de MODIFICAR la fecha?", "Confirmacion",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                dtp_fecha_inicio_informacion_reportada.Value = f3;
            }
        }
        private void dtp_fecha_inicio_informacion_reportada_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cancelar cualquier entrada manual
            e.Handled = true;
        }

        // fecha fecha_termino_informacion_reportada
        private DateTime f4;
        private void dtp_fecha_termino_informacion_reportada_DropDown(object sender, EventArgs e)
        {
            f4 = dtp_fecha_termino_informacion_reportada.Value;
        }
        private void dtp_fecha_termino_informacion_reportada_CloseUp(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de MODIFICAR la fecha?", "Confirmacion",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                dtp_fecha_termino_informacion_reportada.Value = f4;
            }
        }
        private void dtp_fecha_termino_informacion_reportada_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cancelar cualquier entrada manual
            e.Handled = true;
        }

        // fecha fecha_inicio_po
        private DateTime f5;
        private void dtp_fecha_inicio_po_DropDown(object sender, EventArgs e)
        {
            f5 = dtp_fecha_inicio_po.Value;
        }
        private void dtp_fecha_inicio_po_CloseUp(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de MODIFICAR la fecha?", "Confirmacion",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                dtp_fecha_inicio_po.Value = f5;
            }
        }
        private void dtp_fecha_inicio_po_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cancelar cualquier entrada manual
            e.Handled = true;
        }

        // fecha fecha_termino_po
        private DateTime f6;
        private void dtp_fecha_termino_po_DropDown(object sender, EventArgs e)
        {
            f6 = dtp_fecha_termino_po.Value;
        }
        private void dtp_fecha_termino_po_CloseUp(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de MODIFICAR la fecha?", "Confirmacion",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                dtp_fecha_termino_po.Value = f6;
            }
        }
        private void dtp_fecha_termino_po_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cancelar cualquier entrada manual
            e.Handled = true;
        }

        // fecha fecha_inicio_p_rec
        private DateTime f7;
        private void dtp_fecha_inicio_p_rec_DropDown(object sender, EventArgs e)
        {
            f7 = dtp_fecha_inicio_p_rec.Value;
        }
        private void dtp_fecha_inicio_p_rec_CloseUp(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de MODIFICAR la fecha?", "Confirmacion",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                dtp_fecha_inicio_p_rec.Value = f7;
            }
        }
        private void dtp_fecha_inicio_p_rec_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cancelar cualquier entrada manual
            e.Handled = true;
        }

        // fecha fecha_termino_p_rec
        private DateTime f8;
        private void dtp_fecha_termino_p_rec_DropDown(object sender, EventArgs e)
        {
            f8 = dtp_fecha_termino_p_rec.Value;
        }
        private void dtp_fecha_termino_p_rec_CloseUp(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de MODIFICAR la fecha?", "Confirmacion",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                dtp_fecha_termino_p_rec.Value = f8;
            }
        }
        private void dtp_fecha_termino_p_rec_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cancelar cualquier entrada manual
            e.Handled = true;
        }

        // fecha fecha_inicio_pe
        private DateTime f9;
        private void dtp_fecha_inicio_pe_DropDown(object sender, EventArgs e)
        {
            f9 = dtp_fecha_inicio_pe.Value;
        }
        private void dtp_fecha_inicio_pe_CloseUp(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de MODIFICAR la fecha?", "Confirmacion",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                dtp_fecha_inicio_pe.Value = f9;
            }
            else
            {
                if (dtp_fecha_inicio_pe.Value <= dtp_fecha_termino_p_rec.Value
                 && dtp_fecha_inicio_pe.Value >= dtp_fecha_inicio_p_rec.Value)
                {

                }
                else
                {
                    MessageBox.Show("La fecha debe estar contenida en el rango del periodo de receso reportado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtp_fecha_inicio_pe.Value = dtp_fecha_inicio_p_rec.Value;
                    dtp_fecha_inicio_pe.Focus();
                }
            }
        }
        private void dtp_fecha_inicio_pe_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cancelar cualquier entrada manual
            e.Handled = true;
        }

        // fecha fecha_termino_pe
        private DateTime f10;
        private void dtp_fecha_termino_pe_DropDown(object sender, EventArgs e)
        {
            f10 = dtp_fecha_termino_pe.Value;
        }
        private void dtp_fecha_termino_pe_CloseUp(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de MODIFICAR la fecha?", "Confirmacion",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                dtp_fecha_termino_pe.Value = f9;
            }
            else
            {
                if (dtp_fecha_termino_pe.Value <= dtp_fecha_termino_p_rec.Value
                 && dtp_fecha_termino_pe.Value >= dtp_fecha_inicio_p_rec.Value)
                {

                }
                else
                {
                    MessageBox.Show("La fecha debe estar contenida en el rango del periodo de receso reportado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtp_fecha_termino_pe.Value = dtp_fecha_termino_p_rec.Value;
                    dtp_fecha_termino_pe.Focus();
                }
            }
        }
        private void dtp_fecha_termino_pe_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cancelar cualquier entrada manual
            e.Handled = true;
        }

        private void txt_periodo_reportado_rec_TextChanged(object sender, EventArgs e)
        {
            // construccion id
            construccion_id_legislatura();

            string entidad_federativa = cmb_entidad_federativa.Text;
            string ejerc_const = cmb_ejercicio_constitucional_informacion_reportada.Text;
            string periodo_receso = txt_periodo_reportado_rec.Text;


            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                string query2 = "select distinct inicio_pr from TC_CALENDARIO_SESIONES WHERE entidad = @entidad_federativa " +
                    "AND ejercicio_constitucional = @ejerc_const " +
                    "AND periodos_reportar = @periodo_receso;";

                string query3 = "select distinct fin_pr from TC_CALENDARIO_SESIONES WHERE entidad = @entidad_federativa " +
                    "AND ejercicio_constitucional = @ejerc_const " +
                    "AND periodos_reportar = @periodo_receso;";

                conexion.Open();

                using (SQLiteCommand cmd2 = new SQLiteCommand(query2, conexion))
                {
                    

                    cmd2.Parameters.AddWithValue("@entidad_federativa", entidad_federativa);
                    cmd2.Parameters.AddWithValue("@ejerc_const", ejerc_const);
                    cmd2.Parameters.AddWithValue("@periodo_receso", periodo_receso);

                    object resultado = cmd2.ExecuteScalar();

                    if (resultado != null &&  DateTime.TryParse(resultado.ToString(), out DateTime inicioReceso))
                    {
                        dtp_fecha_inicio_p_rec.Value = inicioReceso;
                    }
                    else
                    {
                       
                    }
                }

                using (SQLiteCommand cmd3 = new SQLiteCommand(query3, conexion))
                {

                    cmd3.Parameters.AddWithValue("@entidad_federativa", entidad_federativa);
                    cmd3.Parameters.AddWithValue("@ejerc_const", ejerc_const);
                    cmd3.Parameters.AddWithValue("@periodo_receso", periodo_receso);

                    object resultado = cmd3.ExecuteScalar();

                    if (resultado != null && DateTime.TryParse(resultado.ToString(), out DateTime finReceso))
                    {
                        dtp_fecha_termino_p_rec.Value = finReceso;
                    }
                    else
                    {

                    }
                }

                conexion.Close();
            }

            

            /*
            if (cmb_periodo_reportado_po.Text.ToString() == "Primer periodo de receso" ||
                cmb_periodo_reportado_po.Text.ToString() == "Segundo periodo de receso" ||
                cmb_periodo_reportado_po.Text.ToString() == "Tercer periodo de receso")
            {
                chbPE.Enabled = true;
            }
            else
            {
                dgvPE.Enabled = false; cmb_periodo_extraordinario_reportado.Enabled = false;
                dtp_fecha_inicio_pe.Enabled = false; dtp_fecha_termino_pe.Enabled = false; Txt_sesiones_celebradas_pe.Enabled = false;
                btnAgregarPE.Enabled = false; BtnEliminarPE.Enabled = false; txt_periodos_extraordinarios_celebrados.Enabled = false;
                dgvPE.Rows.Clear(); cmb_periodo_extraordinario_reportado.Text = ""; txt_periodos_extraordinarios_celebrados.Clear();
                Txt_sesiones_celebradas_pe.Clear();
                chbPE.Checked = false; chbPE.Enabled = false;

            }
            */
        }
        //-------------------------------------------------- COMISIONES LEGISLATIVAS ----------------------------------------------------

        private void cmb_Tipo_CL()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_TIPO_COMISION";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_tipo_comision_legislativa.DataSource = dataTable;
                    cmb_tipo_comision_legislativa.DisplayMember = "descripcion";

                    cmb_tipo_comision_legislativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_tipo_comision_legislativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_tipo_comision_legislativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_tipo_comision_legislativa.SelectedIndex = -1; // Aquí se establece como vacío
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al llenar el ComboBox cmb_Tipo_CL: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }

            }
        }
        private void cmb_Tema_CL()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_TEMA_COMISION";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_tema_comision_legislativa.DataSource = dataTable;
                    cmb_tema_comision_legislativa.DisplayMember = "descripcion";

                    cmb_tema_comision_legislativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_tema_comision_legislativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_tema_comision_legislativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_tema_comision_legislativa.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_transmision_reuniones_celebradas_CL()
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

                    cmb_cond_transmision_reuniones_celebradas_comision_legislativa.DataSource = dataTable;
                    cmb_cond_transmision_reuniones_celebradas_comision_legislativa.DisplayMember = "descripcion";

                    cmb_cond_transmision_reuniones_celebradas_comision_legislativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_transmision_reuniones_celebradas_comision_legislativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_transmision_reuniones_celebradas_comision_legislativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_transmision_reuniones_celebradas_comision_legislativa.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_cond_celebracion_reuniones_CL()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_SI_NO WHERE id_si_no IN (1, 6, 3)";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cond_celebracion_reuniones_comision_legislativa.DataSource = dataTable;
                    cmb_cond_celebracion_reuniones_comision_legislativa.DisplayMember = "descripcion";

                    cmb_cond_celebracion_reuniones_comision_legislativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_celebracion_reuniones_comision_legislativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_celebracion_reuniones_comision_legislativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_celebracion_reuniones_comision_legislativa.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void pbo_cant_integrantes_comision_legislativa_Click_1(object sender, EventArgs e)
        {
            string mensaje = "1. En caso de que sus registros no le permitan desglosar la información de acuerdo" +
                " con los requerimientos solicitados capture - 1 (no se sabe “NS”).\n\n" +
               "2.En caso de que determinada categoría no se encuentre prevista en la normatividad aplicable, " +
               "capture - 2 (no aplica “NA”).\r\n";

            string titulo = "";

            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void pbo_cant_reuniones_celebradas_comision_legislativa_Click(object sender, EventArgs e)
        {
            string mensaje = "1. En caso de que sus registros no le permitan desglosar la información de acuerdo" +
                " con los requerimientos solicitados capture - 1 (no se sabe “NS”).\n\n" +
               "2.En caso de que determinada categoría no se encuentre prevista en la normatividad aplicable, " +
               "capture - 2 (no aplica “NA”).\r\n";

            string titulo = "";

            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void pbo_cant_reuniones_celebradas_transmitidas_comision_legislativa_Click(object sender, EventArgs e)
        {
            string mensaje = "1. En caso de que sus registros no le permitan desglosar la información de acuerdo" +
                " con los requerimientos solicitados capture - 1 (no se sabe “NS”).\n\n" +
               "2.En caso de que determinada categoría no se encuentre prevista en la normatividad aplicable, " +
               "capture - 2 (no aplica “NA”).\r\n";

            string titulo = "";

            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void pbo_cant_iniciativas_turnadas_a_comision_legislativa_Click(object sender, EventArgs e)
        {
            string mensaje = "1. En caso de que sus registros no le permitan desglosar la información de acuerdo" +
                 " con los requerimientos solicitados capture - 1 (no se sabe “NS”).\n\n" +
                "2.En caso de que determinada categoría no se encuentre prevista en la normatividad aplicable, " +
                "capture - 2 (no aplica “NA”).\r\n";

            string titulo = "";

            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void pbo_cant_dictamenes_emitidos_por_comision_legislativa_Click(object sender, EventArgs e)
        {
            string mensaje = "1. En caso de que sus registros no le permitan desglosar la información de acuerdo" +
                " con los requerimientos solicitados capture - 1 (no se sabe “NS”).\n\n" +
               "2.En caso de que determinada categoría no se encuentre prevista en la normatividad aplicable, " +
               "capture - 2 (no aplica “NA”).\r\n";

            string titulo = "";

            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void txt_consecutivo_comision_legislativa_TextChanged(object sender, EventArgs e)
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string tipo_cl = cmb_tipo_comision_legislativa.Text.ToString();

            string num_leg = "";
            using (SQLiteConnection con = new SQLiteConnection(cadena))
            {
                con.Open();
                string query = "SELECT abr FROM TC_TIPO_COMISION WHERE descripcion = @tipo_cl";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@tipo_cl", tipo_cl);
                num_leg = cmd.ExecuteScalar()?.ToString();
                con.Close();
            }

            string conse_cl = txt_consecutivo_comision_legislativa.Text.ToString();
            string cve_ent = txt_id_legislatura.Text.Substring(0, 2).ToString();
            string resultadoConcatenado = "COM_" + num_leg + "_" + cve_ent + "_" + conse_cl;

            // Mostrar el resultado en TextBox1
            txt_ID_comision_legislativa.Text = resultadoConcatenado;

        }
        private void cmb_tipo_comision_legislativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                string cadena = "Data Source = DB_PLE.db;Version=3;";

                // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
                string tipo_cl = cmb_tipo_comision_legislativa.Text.ToString();

                if (tipo_cl == "Otro tipo (especifique)")
                {
                    Txt_otro_tipo_comision_legislativa_especifique.Enabled = true; Txt_otro_tipo_comision_legislativa_especifique.BackColor = Color.Honeydew;
                    Txt_otro_tipo_comision_legislativa_especifique.Focus();
            }
                else
                {
                    Txt_otro_tipo_comision_legislativa_especifique.Enabled = false; Txt_otro_tipo_comision_legislativa_especifique.BackColor = Color.LightGray;
                Txt_otro_tipo_comision_legislativa_especifique.Text = "";
                }

            string num_leg = "";
            using (SQLiteConnection con = new SQLiteConnection(cadena))
            {
                con.Open();
                string query = "SELECT abr FROM TC_TIPO_COMISION WHERE descripcion = @tipo_cl";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@tipo_cl", tipo_cl);
                num_leg = cmd.ExecuteScalar()?.ToString();
                con.Close();
            }

            if (txt_id_legislatura.Text == "")
            {
                string cve_ent = "";
                string conse_cl = txt_consecutivo_comision_legislativa.Text.ToString();
                string resultadoConcatenado = "COM_" + num_leg + "_" + cve_ent + "_" + conse_cl;
                txt_ID_comision_legislativa.Text = resultadoConcatenado;
            }
            else
            {
                string cve_ent = txt_id_legislatura.Text.Substring(0, 2).ToString();
                string conse_cl = txt_consecutivo_comision_legislativa.Text.ToString();
                string resultadoConcatenado = "COM_" + num_leg + "_" + cve_ent + "_" + conse_cl;
                txt_ID_comision_legislativa.Text = resultadoConcatenado;
            }

        }
        private void btnAgregarCL_Click(object sender, EventArgs e)
        {
            // se obtienen los valores
            string tema_comision_legislativa = cmb_tema_comision_legislativa.Text.Trim();
            string otro_tema = txt_otro_tema_comision_legislativa_especifique.Text.Trim();

            if (string.IsNullOrWhiteSpace(cmb_tema_comision_legislativa.Text))
            {
                MessageBox.Show("Revisar datos vacios");
            }
            else
            {
                // Agregar una nueva fila al DataGridView
                bool respuesta = IsDuplicateRecord_CL(cmb_tema_comision_legislativa.Text.ToString());

                if (respuesta == true)
                {
                    MessageBox.Show("Dato duplicado");
                }
                else
                {
                    // Agregar una nueva fila al DataGridView
                    dgv_tema_comision_legislativa.Rows.Add(tema_comision_legislativa, otro_tema);

                    cmb_tema_comision_legislativa.Text = "";
                    txt_otro_tema_comision_legislativa_especifique.Clear(); txt_otro_tema_comision_legislativa_especifique.Enabled = false;
                    txt_otro_tema_comision_legislativa_especifique.BackColor = Color.LightGray;
                }
            }

        }
        private bool IsDuplicateRecord_CL(string variable_cmb)
        {
            foreach (DataGridViewRow row in dgv_tema_comision_legislativa.Rows)
            {
                if (row.IsNewRow) continue; // Skip the new row placeholder

                string existingId = row.Cells["tema_comision_legislativa"].Value.ToString();

                if (existingId == variable_cmb)
                {
                    return true;
                }
            }
            return false;
        }
        private void cmb_tema_comision_legislativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_tema_comision_legislativa.Text.ToString();

            if (valorComboBox1 == "Otro tema o asunto (especifique)")
            {
                txt_otro_tema_comision_legislativa_especifique.Enabled = true; txt_otro_tema_comision_legislativa_especifique.BackColor = Color.Honeydew;
                txt_otro_tema_comision_legislativa_especifique.Focus();
            }
            else
            {
                txt_otro_tema_comision_legislativa_especifique.Enabled = false; txt_otro_tema_comision_legislativa_especifique.BackColor = Color.LightGray;
                txt_otro_tema_comision_legislativa_especifique.Text = "";
            }

        }
        private void Txt_otro_tipo_comision_legislativa_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            Txt_otro_tipo_comision_legislativa_especifique.Text = Txt_otro_tipo_comision_legislativa_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            Txt_otro_tipo_comision_legislativa_especifique.SelectionStart = Txt_otro_tipo_comision_legislativa_especifique.Text.Length;

        }
        private void txt_otro_tema_comision_legislativa_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_otro_tema_comision_legislativa_especifique.Text = txt_otro_tema_comision_legislativa_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_otro_tema_comision_legislativa_especifique.SelectionStart = txt_otro_tema_comision_legislativa_especifique.Text.Length;
        }
        private void btnEliminarCL_Click(object sender, EventArgs e)
        {
            if (dgv_tema_comision_legislativa.SelectedRows.Count > 0)
            {
                dgv_tema_comision_legislativa.Rows.RemoveAt(dgv_tema_comision_legislativa.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }

        }
        private void txt_cant_integrantes_comision_legislativa_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '-') || (e.KeyChar == '-' && ((System.Windows.Forms.TextBox)sender).Text.Length != 0))
            {
                e.Handled = true; // Ignorar el carácter
            }

        }
        private void cmb_cond_celebracion_reuniones_comision_legislativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_cant_reuniones_celebradas_comision_legislativa.Clear();
            cmb_cond_transmision_reuniones_celebradas_comision_legislativa.SelectedIndex = -1;
            txt_cant_reuniones_celebradas_comision_legislativa.Clear();

            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_celebracion_reuniones_comision_legislativa.Text.ToString();

            if (valorComboBox1 == "No (especifique)")
            {
                txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Enabled = true;
                txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Focus();
                txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.BackColor = Color.Honeydew;
            }
            else
            {
                txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Enabled = false;
                txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Text = "";
                txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.BackColor = Color.LightGray;
            }

            if (valorComboBox1 == "Si")
            {
                txt_cant_reuniones_celebradas_comision_legislativa.Enabled = true; txt_cant_reuniones_celebradas_comision_legislativa.BackColor = Color.Honeydew;
                cmb_cond_transmision_reuniones_celebradas_comision_legislativa.Enabled = true; cmb_cond_transmision_reuniones_celebradas_comision_legislativa.BackColor = Color.Honeydew;
            }
            else
            {
                txt_cant_reuniones_celebradas_comision_legislativa.Enabled = false; txt_cant_reuniones_celebradas_comision_legislativa.BackColor = Color.LightGray;
                txt_cant_reuniones_celebradas_comision_legislativa.Text = "";

                cmb_cond_transmision_reuniones_celebradas_comision_legislativa.Enabled = false; cmb_cond_transmision_reuniones_celebradas_comision_legislativa.BackColor = Color.LightGray;
                cmb_cond_transmision_reuniones_celebradas_comision_legislativa.Text = "";
            }
        }
        private void cmb_cond_transmision_reuniones_celebradas_comision_legislativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_transmision_reuniones_celebradas_comision_legislativa.Text.ToString();

            if (valorComboBox1 == "Si")
            {
                txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.Enabled = true; txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.BackColor = Color.Honeydew;
            }
            else
            {
                txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.Enabled = false; txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.BackColor = Color.LightGray;
                txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.Text = "";
            }
        }
        private void txt_observaciones_cl_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_observaciones_cl.Text = txt_observaciones_cl.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_observaciones_cl.SelectionStart = txt_observaciones_cl.Text.Length;

        }
        private void Txt_otro_tipo_comision_legislativa_especifique_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Txt_otro_tipo_comision_legislativa_especifique.Text))
            {
                MessageBox.Show("Debe especificar el otro tipo de comisión legislativa.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Txt_otro_tipo_comision_legislativa_especifique.Focus();
            }
        }
        private void txt_otro_tema_comision_legislativa_especifique_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_otro_tema_comision_legislativa_especifique.Text))
            {
                MessageBox.Show("Debe especificar el otro tema o asunto atendido por la comisión legislativa.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_otro_tema_comision_legislativa_especifique.Focus();
            }
        }
        private void txt_no_cond_celebracion_reuniones_comision_legislativa_especifique_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Text))
            {
                MessageBox.Show("Debe especificar el motivo por el cual la comisión legislativa no se reunió durante el periodo reportado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Focus();
            }
        }
        private void txt_cant_reuniones_celebradas_transmitidas_comision_legislativa_TextChanged(object sender, EventArgs e)
        {
            int valor;
            int valor2;

            int.TryParse(txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.Text, out valor);
            int.TryParse(txt_cant_reuniones_celebradas_comision_legislativa.Text, out valor2);

            // Verificar si el valor está dentro del rango permitido
            if (valor > valor2)
            {
                MessageBox.Show("Debe ser igual o menor a la cantidad de reuniones celebradas por la comisión legislativa.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.Text = ""; // Limpiar el TextBox si está fuera del rango
                txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.Focus();
            }
            if (valor < -2)
            {
                MessageBox.Show("Registrar el valor correcto, ver cuadro de ayuda.");
                txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.Text = ""; // Limpiar el TextBox si está fuera del rango
            }

            if (valor == -1 || valor == -2 )
            {
                MessageBox.Show("Justificar la elección en el apartado de observaciones");
                txt_observaciones_cl.Focus();
            }
        }
        private void Txt_nombre_comision_legislativa_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_nombre_comision_legislativa.Text = txt_nombre_comision_legislativa.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_nombre_comision_legislativa.SelectionStart = txt_nombre_comision_legislativa.Text.Length;
        }
        private void txt_no_cond_celebracion_reuniones_comision_legislativa_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Text = txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.SelectionStart = txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Text.Length;
        }
        private void txt_cant_reuniones_celebradas_comision_legislativa_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '-') || (e.KeyChar == '-' && ((System.Windows.Forms.TextBox)sender).Text.Length != 0))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }
        private void txt_cant_reuniones_celebradas_transmitidas_comision_legislativa_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '-') || (e.KeyChar == '-' && ((System.Windows.Forms.TextBox)sender).Text.Length != 0))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }
        private void txt_cant_iniciativas_turnadas_a_comision_legislativa_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '-') || (e.KeyChar == '-' && ((System.Windows.Forms.TextBox)sender).Text.Length != 0))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }
        private void txt_cant_dictamenes_emitidos_por_comision_legislativa_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '-') || (e.KeyChar == '-' && ((System.Windows.Forms.TextBox)sender).Text.Length != 0))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }
        private void txt_cant_reuniones_celebradas_comision_legislativa_Leave(object sender, EventArgs e)
        {
            int valor;
            int.TryParse(txt_cant_reuniones_celebradas_comision_legislativa.Text, out valor);

            // Verificar si el valor está dentro del rango permitido
            if (valor < -2)
            {
                MessageBox.Show("Registrar el valor correcto, ver cuadro de ayuda.");
                txt_cant_reuniones_celebradas_comision_legislativa.Text = ""; // Limpiar el TextBox si está fuera del rango
            }

            if (valor == -1 || valor == -2)
            {
                MessageBox.Show("Justificar la elección en el apartado de observaciones");
                txt_observaciones_cl.Focus();
            }
        }
        private void Txt_consecutivo_comision_legislativa_MouseHover(object sender, EventArgs e)
        {
            // Mostrar mensaje al pasar el ratón sobre el TextBox
            System.Windows.Forms.ToolTip tooltip = new System.Windows.Forms.ToolTip();
            tooltip.SetToolTip(txt_consecutivo_comision_legislativa, "Número asignado a la comisión legislativa." +
                " Para el caso de las comisiones ordinarias, permanentes u homólogas, " +
                "se sugiere respetar el orden descendente de las fracciones establecidas en el correspondiente " +
                "artículo de la Ley o Reglamento del Congreso de la entidad federativa.");
        }
        private void txt_cant_iniciativas_turnadas_a_comision_legislativa_TextChanged(object sender, EventArgs e)
        {
            int valor;
            int.TryParse(txt_cant_iniciativas_turnadas_a_comision_legislativa.Text, out valor);

            // Verificar si el valor está dentro del rango permitido
            if (valor < -2)
            {
                MessageBox.Show("Registrar el valor correcto, ver cuadro de ayuda.");
                txt_cant_iniciativas_turnadas_a_comision_legislativa.Text = ""; // Limpiar el TextBox si está fuera del rango
            }

            if (valor == -1 || valor == -2)
            {
                MessageBox.Show("Justificar la elección en el apartado de observaciones");
                txt_observaciones_cl.Focus();
            }
        }
        private void txt_cant_dictamenes_emitidos_por_comision_legislativa_TextChanged(object sender, EventArgs e)
        {
            int valor;
            int.TryParse(txt_cant_dictamenes_emitidos_por_comision_legislativa.Text, out valor);

            // Verificar si el valor está dentro del rango permitido
            if (valor < -2)
            {
                MessageBox.Show("Registrar el valor correcto, ver cuadro de ayuda.");
                txt_cant_dictamenes_emitidos_por_comision_legislativa.Text = ""; // Limpiar el TextBox si está fuera del rango
            }

            if (valor == -1 || valor == -2)
            {
                MessageBox.Show("Justificar la elección en el apartado de observaciones");
                txt_observaciones_cl.Focus();
            }
        }
        private void btnGuardarDB_CL_Click(object sender, EventArgs e)
        {
            bool cv = ValidarCampos_CL();

            if (cv == true)
            {
                DialogResult respuesta = MessageBox.Show("¿Está seguro de Guardar los datos?", "Confirmacion",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    // Agregar una nueva fila al DataGridView
                    bool duplicado = IsDuplicateRecord_RegistrosCL(txt_ID_comision_legislativa.Text.ToString());

                    if (duplicado == true)
                    {
                        MessageBox.Show("El ID ya se encuentra registrado. Favor de verificar la información.", "Comisiones Legislativas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {


                        string cadena = "Data Source = DB_PLE.db;Version=3;";

                        using (SQLiteConnection connection = new SQLiteConnection(cadena))
                        {
                            connection.Open();

                            // Recorremos las filas del DataGridView
                            foreach (DataGridViewRow row in dgv_tema_comision_legislativa.Rows)
                            {
                                // Ignoramos la fila vacía al final
                                if (!row.IsNewRow)
                                {
                                    // Insertamos los datos en la base de datos
                                    string query = "INSERT INTO TR_COMISIONES_LEGISLATIVAS (" +
                                        "id_legislatura," +
                                        "ID_comision_legislativa," +
                                        "consecutivo_comision_legislativa," +
                                        "nombre_comision_legislativa," +
                                        "tipo_comision_legislativa," +
                                        "otro_tipo_comision_legislativa_especifique," +
                                        "tema_comision_legislativa," +
                                        "otro_tema_comision_legislativa_especifique," +
                                        "cant_integrantes_comision_legislativa," +
                                        "cond_celebracion_reuniones_comision_legislativa," +
                                        "no_cond_celebracion_reuniones_comision_legislativa_especifique," +
                                        "cant_reuniones_celebradas_comision_legislativa," +
                                        "cond_transmision_reuniones_celebradas_comision_legislativa," +
                                        "cant_reuniones_celebradas_transmitidas_comision_legislativa," +
                                        "cant_iniciativas_turnadas_a_comision_legislativa," +
                                        "cant_dictamenes_emitidos_por_comision_legislativa," +
                                        "observaciones_cl," +
                                        "fecha_actualizacion" +
                                        ")" +
                                 "VALUES" +
                                        " (" +
                                        "@id_legislatura," +
                                        "@ID_comision_legislativa," +
                                        "@consecutivo_comision_legislativa," +
                                        "@nombre_comision_legislativa," +
                                        "@tipo_comision_legislativa," +
                                        "@otro_tipo_comision_legislativa_especifique," +
                                        "@tema_comision_legislativa," +
                                        "@otro_tema_comision_legislativa_especifique," +
                                        "@cant_integrantes_comision_legislativa," +
                                        "@cond_celebracion_reuniones_comision_legislativa," +
                                        "@no_cond_celebracion_reuniones_comision_legislativa_especifique," +
                                        "@cant_reuniones_celebradas_comision_legislativa," +
                                        "@cond_transmision_reuniones_celebradas_comision_legislativa," +
                                        "@cant_reuniones_celebradas_transmitidas_comision_legislativa," +
                                        "@cant_iniciativas_turnadas_a_comision_legislativa," +
                                        "@cant_dictamenes_emitidos_por_comision_legislativa," +
                                        "@observaciones_cl," +
                                        "@fecha_actualizacion" +
                                        ")";

                                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                                    {
                                        // Variables individuales
                                        command.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text);
                                        command.Parameters.AddWithValue("@ID_comision_legislativa", txt_ID_comision_legislativa.Text);
                                        command.Parameters.AddWithValue("@consecutivo_comision_legislativa", txt_consecutivo_comision_legislativa.Text);
                                        command.Parameters.AddWithValue("@nombre_comision_legislativa", txt_nombre_comision_legislativa.Text);
                                        command.Parameters.AddWithValue("@tipo_comision_legislativa", cmb_tipo_comision_legislativa.Text);
                                        command.Parameters.AddWithValue("@otro_tipo_comision_legislativa_especifique", Txt_otro_tipo_comision_legislativa_especifique.Text);
                                        command.Parameters.AddWithValue("@cant_integrantes_comision_legislativa", txt_cant_integrantes_comision_legislativa.Text);
                                        command.Parameters.AddWithValue("@cond_celebracion_reuniones_comision_legislativa", cmb_cond_celebracion_reuniones_comision_legislativa.Text);
                                        command.Parameters.AddWithValue("@no_cond_celebracion_reuniones_comision_legislativa_especifique", txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Text);
                                        command.Parameters.AddWithValue("@cant_reuniones_celebradas_comision_legislativa", txt_cant_reuniones_celebradas_comision_legislativa.Text);
                                        command.Parameters.AddWithValue("@cond_transmision_reuniones_celebradas_comision_legislativa", cmb_cond_transmision_reuniones_celebradas_comision_legislativa.Text);
                                        command.Parameters.AddWithValue("@cant_reuniones_celebradas_transmitidas_comision_legislativa", txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.Text);
                                        command.Parameters.AddWithValue("@cant_iniciativas_turnadas_a_comision_legislativa", txt_cant_iniciativas_turnadas_a_comision_legislativa.Text);
                                        command.Parameters.AddWithValue("@cant_dictamenes_emitidos_por_comision_legislativa", txt_cant_dictamenes_emitidos_por_comision_legislativa.Text);
                                        command.Parameters.AddWithValue("@observaciones_cl", txt_observaciones_cl.Text);
                                        command.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

                                        // Variables del dgv
                                        command.Parameters.AddWithValue("@tema_comision_legislativa", row.Cells["tema_comision_legislativa"].Value);
                                        command.Parameters.AddWithValue("@otro_tema_comision_legislativa_especifique", row.Cells["otro_tema_comision_legislativa_especifique"].Value);

                                        command.ExecuteNonQuery();
                                    }
                                }

                            }
                            connection.Close();
                        }

                        // Se reinicion los botones
                        MessageBox.Show("Datos guardados correctamente");

                        txt_nombre_comision_legislativa.Clear();
                        cmb_tipo_comision_legislativa.Text = ""; Txt_otro_tipo_comision_legislativa_especifique.Clear();
                        cmb_tema_comision_legislativa.Text = ""; txt_otro_tema_comision_legislativa_especifique.Clear();
                        dgv_tema_comision_legislativa.Rows.Clear();
                        txt_cant_integrantes_comision_legislativa.Clear(); cmb_cond_celebracion_reuniones_comision_legislativa.Text = "";
                        txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Clear();
                        txt_cant_reuniones_celebradas_comision_legislativa.Clear();
                        cmb_cond_transmision_reuniones_celebradas_comision_legislativa.SelectedIndex = -1;
                        txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.Clear();
                        txt_cant_iniciativas_turnadas_a_comision_legislativa.Clear();
                        txt_cant_dictamenes_emitidos_por_comision_legislativa.Clear();
                        txt_observaciones_cl.Clear();
                        txt_consecutivo_comision_legislativa.Clear();
                        Txt_otro_tipo_comision_legislativa_especifique.Enabled = false; Txt_otro_tipo_comision_legislativa_especifique.BackColor = Color.LightGray;
                        txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Enabled = false; txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.BackColor = Color.LightGray;
                        DGV_REGISTROS_CL();
                        txt_ID_comision_legislativa.Text = "";
                    }



                }
                else
                {

                }
            }
            else
            {
                //MessageBox.Show("El ID ya se encuentra registrado. Favor de verificar la información.", "Comisiones Legislativas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
        private void DGV_REGISTROS_CL()
        {
            string cadena = "Data Source=DB_PLE.db;Version=3;";
            string id_legis = txt_id_legislatura.Text;

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // Abrir la conexión
                    conexion.Open();

                    // Comando de SQL
                    string query = "SELECT DISTINCT ID_comision_legislativa, nombre_comision_legislativa, " +
                                   "tipo_comision_legislativa, cant_integrantes_comision_legislativa " +
                                   "FROM TR_COMISIONES_LEGISLATIVAS WHERE id_legislatura = @id_legis";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        // Asignar el parámetro
                        cmd.Parameters.AddWithValue("@id_legis", id_legis);

                        // Utilizar un DataAdapter para obtener los datos
                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            dgv_registros_cl.DataSource = dataTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al llenar DGV comisiones legislativas: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }
            }
        }
        private bool IsDuplicateRecord_RegistrosCL(string variable_cmb)
        {
            foreach (DataGridViewRow row in dgv_registros_cl.Rows)
            {
                if (row.IsNewRow) continue; // Skip the new row placeholder

                string existingId = row.Cells["ID_comision_legislativa"].Value.ToString();

                if (existingId == variable_cmb)
                {
                    return true;
                }
            }
            return false;
        }
        private bool ValidarCampos_CL()
        {
            // Array de controles a validar
            Control[] controlesAValidar = { txt_consecutivo_comision_legislativa, txt_nombre_comision_legislativa, cmb_tipo_comision_legislativa,
            txt_cant_integrantes_comision_legislativa,cmb_cond_celebracion_reuniones_comision_legislativa,txt_cant_iniciativas_turnadas_a_comision_legislativa,
            txt_cant_dictamenes_emitidos_por_comision_legislativa};

            foreach (Control control in controlesAValidar)
            {
                // Verificar si el control está vacío
                if (string.IsNullOrWhiteSpace(control.Text))
                {
                    MessageBox.Show($"Existen campos vacíos.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    control.Focus(); // Enfocar el control vacío
                    return false; // Salir del método después de encontrar el primer campo vacío
                }
            }

            int ren_dg;
            ren_dg = dgv_tema_comision_legislativa.Rows.Count;

            if(ren_dg == 0)
            {
                MessageBox.Show($"Existen campos vacíos.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_tema_comision_legislativa.Focus();
                return false;
                
            }

            return true;
        }
        private void btnActualizarDGV_CL_Click(object sender, EventArgs e)
        {
            DGV_REGISTROS_CL();
        }
        //-------------------------------------------------- PERSONAS LEGISLADORAS ----------------------------------------------------
        private void cmb_Sexo_Persona_Legisladora()
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

                    cmb_sexo_persona_legisladora.DataSource = dataTable;
                    cmb_sexo_persona_legisladora.DisplayMember = "descripcion";

                    cmb_sexo_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_sexo_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_sexo_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_sexo_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al llenar el ComboBox cmb_Sexo_Persona_Legisladora: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }

            }
        }
        private void cmb_Estatus_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_ESTATUS";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_estatus_persona_legisladora.DataSource = dataTable;
                    cmb_estatus_persona_legisladora.DisplayMember = "descripcion";

                    cmb_estatus_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_estatus_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;
                   
                    cmb_estatus_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_estatus_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Tipo_licencia_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_TIPO_LICENICIA";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cbm_tipo_licencia_persona_legisladora.DataSource = dataTable;
                    cbm_tipo_licencia_persona_legisladora.DisplayMember = "descripcion";

                    cbm_tipo_licencia_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cbm_tipo_licencia_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cbm_tipo_licencia_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cbm_tipo_licencia_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Causa_fallecimiento_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_CAUSAS_FALLECIMIENTO";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    
                    cbm_causa_fallecimiento_persona_legisladora.DataSource = dataTable;
                    cbm_causa_fallecimiento_persona_legisladora.DisplayMember = "descripcion";

                    cbm_causa_fallecimiento_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cbm_causa_fallecimiento_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cbm_causa_fallecimiento_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cbm_causa_fallecimiento_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Caracter_cargo_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_CARACTER_CARGO";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_caracter_cargo_persona_legisladora.DataSource = dataTable;
                    cmb_caracter_cargo_persona_legisladora.DisplayMember = "descripcion";

                    cmb_caracter_cargo_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_caracter_cargo_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_caracter_cargo_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_caracter_cargo_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Escolaridad_persona_legisladora()
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

                    cmb_escolaridad_persona_legisladora_PL.DataSource = dataTable;
                    cmb_escolaridad_persona_legisladora_PL.DisplayMember = "descripcion";

                    cmb_escolaridad_persona_legisladora_PL.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_escolaridad_persona_legisladora_PL.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_escolaridad_persona_legisladora_PL.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_escolaridad_persona_legisladora_PL.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Persona_Legisladora_Propietaria()
        {
            string cadena = "Data Source=DB_PLE.db;Version=3;";
            string id_legis = txt_id_legislatura.Text;

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // Abrir la conexión
                    conexion.Open();

                    // Comando de SQL
                    string query = "SELECT " +
                                   "(" +
                                   "IFNULL(txt_nombre_1_persona_legisladora, '') || ' ' || " +
                                   "IFNULL(txt_nombre_2_persona_legisladora, '') || ' ' || " +
                                   "IFNULL(txt_nombre_3_persona_legisladora, '') || ' ' || " +
                                   "IFNULL(txt_apellido_1_persona_legisladora, '') || ' ' || " +
                                   "IFNULL(txt_apellido_2_persona_legisladora, '') || ' ' || " +
                                   "IFNULL(txt_apellido_3_persona_legisladora, '') || ' - ' || " +
                                   "txt_ID_persona_legisladora" +
                                   ") AS descripcion " +
                                   "FROM TR_PERSONAS_LEGISLADORAS " +
                                   "WHERE cmb_caracter_cargo_persona_legisladora = 'Propietario' AND id_legislatura = @id_legis";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        // Asignar el parámetro
                        cmd.Parameters.AddWithValue("@id_legis", id_legis);

                        // Utilizar un DataAdapter para obtener los datos
                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            cmb_nombre_persona_legisladora_propietaria.DataSource = dataTable;
                            cmb_nombre_persona_legisladora_propietaria.DisplayMember = "descripcion";
                        }
                    }

                    cmb_nombre_persona_legisladora_propietaria.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_nombre_persona_legisladora_propietaria.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cmb_nombre_persona_legisladora_propietaria.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_nombre_persona_legisladora_propietaria.SelectedIndex = -1; // Aquí se establece como vacío
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al llenar el ComboBox cmb_nombre_persona_legisladora_propietaria: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }
            }
        }
        private void cmb_nombre_persona_legisladora_propietaria_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string nombreCompleto = cmb_nombre_persona_legisladora_propietaria.Text;

            // Verificar si el nombre completo es nulo o vacío
            if (string.IsNullOrEmpty(nombreCompleto))
            {
                txt_ID_persona_legisladora_propietaria.Text = "";
                return;
            }

            // Eliminar espacios adicionales y separar el nombre completo en partes
            
            // Separar el texto utilizando el delimitador '-'
            string[] partes = nombreCompleto.Split('-');

            // Verificar si la separación resultó en al menos dos partes
            if (partes.Length < 2)
            {
                txt_ID_persona_legisladora_propietaria.Text = "";
                return;
            }

            // Extraer la parte que contiene el número
            string id = partes[1].Trim();

            txt_ID_persona_legisladora_propietaria.Text = id;

        }
        private void cmb_Estatus_escolaridad_persona_legisladora()
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

                    cmb_estatus_escolaridad_persona_legisladora.DataSource = dataTable;
                    cmb_estatus_escolaridad_persona_legisladora.DisplayMember = "descripcion";

                    cmb_estatus_escolaridad_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_estatus_escolaridad_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_estatus_escolaridad_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_estatus_escolaridad_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Carrera_licenciatura_persona_legisladora()
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

                    cmb_carrera_licenciatura_persona_legisladora_PL.DataSource = dataTable;
                    cmb_carrera_licenciatura_persona_legisladora_PL.DisplayMember = "descripcion";

                    cmb_carrera_licenciatura_persona_legisladora_PL.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_carrera_licenciatura_persona_legisladora_PL.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_carrera_licenciatura_persona_legisladora_PL.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_carrera_licenciatura_persona_legisladora_PL.SelectedIndex = -1; // Aquí se establece como vacío
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al llenar el ComboBox cmb_Carrera_licenciatura_persona_legisladora: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }

            }
        }
        private void cmb_Carrera_maestria_persona_legisladora()
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

                    cmb_carrera_maestria_persona_legisladora_PL.DataSource = dataTable;
                    cmb_carrera_maestria_persona_legisladora_PL.DisplayMember = "descripcion";

                    cmb_carrera_maestria_persona_legisladora_PL.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_carrera_maestria_persona_legisladora_PL.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_carrera_maestria_persona_legisladora_PL.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_carrera_maestria_persona_legisladora_PL.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Carrera_doctorado_persona_legisladora()
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

                    cmb_carrera_doctorado_persona_legisladora_PL.DataSource = dataTable;
                    cmb_carrera_doctorado_persona_legisladora_PL.DisplayMember = "descripcion";

                    cmb_carrera_doctorado_persona_legisladora_PL.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_carrera_doctorado_persona_legisladora_PL.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_carrera_doctorado_persona_legisladora_PL.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_carrera_doctorado_persona_legisladora_PL.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Cond_lengua_ind_persona_legisladora()
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

                    cmb_cond_lengua_ind_persona_legisladora_PL.DataSource = dataTable;
                    cmb_cond_lengua_ind_persona_legisladora_PL.DisplayMember = "descripcion";

                    cmb_cond_lengua_ind_persona_legisladora_PL.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_lengua_ind_persona_legisladora_PL.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_lengua_ind_persona_legisladora_PL.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_lengua_ind_persona_legisladora_PL.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Lengua_ind_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_LENGUA_INDIGENA";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_lengua_ind_persona_legisladora.DataSource = dataTable;
                    cmb_lengua_ind_persona_legisladora.DisplayMember = "descripcion";

                    cmb_lengua_ind_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_lengua_ind_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_lengua_ind_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_lengua_ind_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Cond_discapacidad_persona_legisladora()
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

                    cmb_cond_discapacidad_persona_legisladora.DataSource = dataTable;
                    cmb_cond_discapacidad_persona_legisladora.DisplayMember = "descripcion";

                    cmb_cond_discapacidad_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_discapacidad_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_discapacidad_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_discapacidad_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Tipo_discapacidad_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_TIPO_DISCAPACIDAD";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_tipo_discapacidad_persona_legisladora.DataSource = dataTable;
                    cmb_tipo_discapacidad_persona_legisladora.DisplayMember = "descripcion";

                    cmb_tipo_discapacidad_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_tipo_discapacidad_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_tipo_discapacidad_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_tipo_discapacidad_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Cond_pueblo_ind_persona_legisladora()
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

                    cmb_cond_pueblo_ind_persona_legisladora_PL.DataSource = dataTable;
                    cmb_cond_pueblo_ind_persona_legisladora_PL.DisplayMember = "descripcion";

                    cmb_cond_pueblo_ind_persona_legisladora_PL.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_pueblo_ind_persona_legisladora_PL.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_pueblo_ind_persona_legisladora_PL.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_pueblo_ind_persona_legisladora_PL.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Pueblo_ind_persona_legisladora()
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

                    cmb_pueblo_ind_persona_legisladora_PL.DataSource = dataTable;
                    cmb_pueblo_ind_persona_legisladora_PL.DisplayMember = "descripcion";

                    cmb_pueblo_ind_persona_legisladora_PL.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_pueblo_ind_persona_legisladora_PL.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_pueblo_ind_persona_legisladora_PL.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_pueblo_ind_persona_legisladora_PL.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Cond_pob_diversidad_sexual_persona_legisladora()
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

                    cmb_cond_pob_diversidad_sexual_persona_legisladora.DataSource = dataTable;
                    cmb_cond_pob_diversidad_sexual_persona_legisladora.DisplayMember = "descripcion";

                    cmb_cond_pob_diversidad_sexual_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_pob_diversidad_sexual_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_pob_diversidad_sexual_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_pob_diversidad_sexual_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Cond_pob_afromexicana_persona_legisladora()
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

                    cmb_cond_pob_afromexicana_persona_legisladora_PL.DataSource = dataTable;
                    cmb_cond_pob_afromexicana_persona_legisladora_PL.DisplayMember = "descripcion";

                    cmb_cond_pob_afromexicana_persona_legisladora_PL.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_pob_afromexicana_persona_legisladora_PL.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_pob_afromexicana_persona_legisladora_PL.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_pob_afromexicana_persona_legisladora_PL.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Empleo_anterior_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_EMPLEO_ANT";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_empleo_anterior_persona_legisladora.DataSource = dataTable;
                    cmb_empleo_anterior_persona_legisladora.DisplayMember = "descripcion";

                    cmb_empleo_anterior_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_empleo_anterior_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_empleo_anterior_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_empleo_anterior_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Antigüedad_servicio_publico_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_ANTIGUEDAD";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_antigüedad_servicio_publico_persona_legisladora.DataSource = dataTable;
                    cmb_antigüedad_servicio_publico_persona_legisladora.DisplayMember = "descripcion";

                    cmb_antigüedad_servicio_publico_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_antigüedad_servicio_publico_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_antigüedad_servicio_publico_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_antigüedad_servicio_publico_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Antigüedad_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_ANTIGUEDAD";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_antigüedad_persona_legisladora.DataSource = dataTable;
                    cmb_antigüedad_persona_legisladora.DisplayMember = "descripcion";

                    cmb_antigüedad_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_antigüedad_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_antigüedad_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_antigüedad_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Forma_eleccion_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_FORMA_ELECCION";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_forma_eleccion_persona_legisladora.DataSource = dataTable;
                    cmb_forma_eleccion_persona_legisladora.DisplayMember = "descripcion";

                    cmb_forma_eleccion_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_forma_eleccion_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_forma_eleccion_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_forma_eleccion_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Tipo_candidatura_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_TIPO_CANDIDATURA";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_tipo_candidatura_persona_legisladora.DataSource = dataTable;
                    cmb_tipo_candidatura_persona_legisladora.DisplayMember = "descripcion";

                    cmb_tipo_candidatura_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_tipo_candidatura_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_tipo_candidatura_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_tipo_candidatura_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Tipo_adscripcion_inicial_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_TIPO_ADSCRIPCION";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_tipo_adscripcion_final_persona_legisladora.DataSource = dataTable;
                    cmb_tipo_adscripcion_final_persona_legisladora.DisplayMember = "descripcion";

                    cmb_tipo_adscripcion_final_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_tipo_adscripcion_final_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_tipo_adscripcion_final_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_tipo_adscripcion_final_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Tipo_adscripcion_final_persona_legisladora()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_TIPO_ADSCRIPCION";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_tipo_adscripcion_inicial_persona_legisladora.DataSource = dataTable;
                    cmb_tipo_adscripcion_inicial_persona_legisladora.DisplayMember = "descripcion";

                    cmb_tipo_adscripcion_inicial_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_tipo_adscripcion_inicial_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_tipo_adscripcion_inicial_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_tipo_adscripcion_inicial_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Cond_presentacion_declaracion_situacion_patrimonial()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_SI_NO where id_si_no in (1,2,4,3)";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cond_presentacion_declaracion_situacion_patrimonial.DataSource = dataTable;
                    cmb_cond_presentacion_declaracion_situacion_patrimonial.DisplayMember = "descripcion";

                    cmb_cond_presentacion_declaracion_situacion_patrimonial.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_presentacion_declaracion_situacion_patrimonial.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_presentacion_declaracion_situacion_patrimonial.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_presentacion_declaracion_situacion_patrimonial.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Cond_presentacion_declaracion_intereses()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_SI_NO where id_si_no in (1,2,3,4)";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cond_presentacion_declaracion_intereses.DataSource = dataTable;
                    cmb_cond_presentacion_declaracion_intereses.DisplayMember = "descripcion";

                    cmb_cond_presentacion_declaracion_intereses.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_presentacion_declaracion_intereses.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_presentacion_declaracion_intereses.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_presentacion_declaracion_intereses.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Cond_presentacion_declaracion_fiscal()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_SI_NO where id_si_no in (1,2,3,4)";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cond_presentacion_declaracion_fiscal.DataSource = dataTable;
                    cmb_cond_presentacion_declaracion_fiscal.DisplayMember = "descripcion";

                    cmb_cond_presentacion_declaracion_fiscal.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_presentacion_declaracion_fiscal.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_presentacion_declaracion_fiscal.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_presentacion_declaracion_fiscal.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Cond_casa_atencion_ciudadana()
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

                    cmb_cond_casa_atencion_ciudadana.DataSource = dataTable;
                    cmb_cond_casa_atencion_ciudadana.DisplayMember = "descripcion";

                    cmb_cond_casa_atencion_ciudadana.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_casa_atencion_ciudadana.AutoCompleteSource = AutoCompleteSource.ListItems;
                    
                    cmb_cond_casa_atencion_ciudadana.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_casa_atencion_ciudadana.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Cond_casa_atencion_ciudadana_movil()
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

                    cmb_cond_casa_atencion_ciudadana_movil.DataSource = dataTable;
                    cmb_cond_casa_atencion_ciudadana_movil.DisplayMember = "descripcion";

                    cmb_cond_casa_atencion_ciudadana_movil.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_casa_atencion_ciudadana_movil.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_casa_atencion_ciudadana_movil.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_casa_atencion_ciudadana_movil.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Cond_integrante_comision_permanente()
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

                    cmb_cond_integrante_comision_permanente.DataSource = dataTable;
                    cmb_cond_integrante_comision_permanente.DisplayMember = "descripcion";

                    cmb_cond_integrante_comision_permanente.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_integrante_comision_permanente.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_integrante_comision_permanente.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_integrante_comision_permanente.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Cargo_comision_permanente()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_CARGO";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cargo_comision_permanente.DataSource = dataTable;
                    cmb_cargo_comision_permanente.DisplayMember = "descripcion";

                    cmb_cargo_comision_permanente.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cargo_comision_permanente.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cargo_comision_permanente.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cargo_comision_permanente.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Nombre_comision_legislativa()
        {
            string cadena = "Data Source=DB_PLE.db;Version=3;";
            string id_legis = txt_id_legislatura.Text;

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // Abrir la conexión
                    conexion.Open();

                    // Comando de SQL
                    string query = "SELECT nombre_comision_legislativa FROM TR_COMISIONES_LEGISLATIVAS WHERE id_legislatura = @id_legis";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        // Asignar el parámetro
                        cmd.Parameters.AddWithValue("@id_legis", id_legis);

                        // Utilizar un DataAdapter para obtener los datos
                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            cmb_nombre_comision_legislativa.DataSource = dataTable;
                            cmb_nombre_comision_legislativa.DisplayMember = "nombre_comision_legislativa";
                        }
                    }

                    cmb_nombre_comision_legislativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_nombre_comision_legislativa.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cmb_nombre_comision_legislativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_nombre_comision_legislativa.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Cargo_comision_legislativa()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_CARGO";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cargo_comision_legislativa.DataSource = dataTable;
                    cmb_cargo_comision_legislativa.DisplayMember = "descripcion";

                    cmb_cargo_comision_legislativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cargo_comision_legislativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cargo_comision_legislativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cargo_comision_legislativa.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Cond_integrante_jucopo()
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

                    cmb_cond_integrante_jucopo.DataSource = dataTable;
                    cmb_cond_integrante_jucopo.DisplayMember = "descripcion";

                    cmb_cond_integrante_jucopo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_integrante_jucopo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_integrante_jucopo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_integrante_jucopo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Cargo_jucopo()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_CARGO";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cargo_jucopo.DataSource = dataTable;
                    cmb_cargo_jucopo.DisplayMember = "descripcion";

                    cmb_cargo_jucopo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cargo_jucopo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cargo_jucopo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cargo_jucopo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Cond_integrante_mesa_directiva()
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

                    cmb_cond_integrante_mesa_directiva.DataSource = dataTable;
                    cmb_cond_integrante_mesa_directiva.DisplayMember = "descripcion";

                    cmb_cond_integrante_mesa_directiva.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_integrante_mesa_directiva.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_integrante_mesa_directiva.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_integrante_mesa_directiva.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Cargo_mesa_directiva_PL()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_CARGO";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cargo_mesa_directiva_PL.DataSource = dataTable;
                    cmb_cargo_mesa_directiva_PL.DisplayMember = "descripcion";

                    cmb_cargo_mesa_directiva_PL.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cargo_mesa_directiva_PL.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cargo_mesa_directiva_PL.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cargo_mesa_directiva_PL.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void txt_nombre_1_persona_legisladora_TextChanged(object sender, EventArgs e)
        {
            cmb_Nombre_comision_legislativa();

            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_nombre_1_persona_legisladora.Text = txt_nombre_1_persona_legisladora.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_nombre_1_persona_legisladora.SelectionStart = txt_nombre_1_persona_legisladora.Text.Length;

            if (string.IsNullOrWhiteSpace(txt_nombre_1_persona_legisladora.Text))
            {
                //MessageBox.Show("Debe especificar el nombre.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_nombre_2_persona_legisladora.BackColor = Color.LightGray; txt_nombre_2_persona_legisladora.Enabled = false;
                txt_nombre_2_persona_legisladora.Clear();
                txt_nombre_1_persona_legisladora.Focus();
            }
            else
            {
                //txt_nombre_2_persona_legisladora.Visible = false;
                txt_nombre_2_persona_legisladora.Enabled = true; txt_nombre_2_persona_legisladora.BackColor = Color.Honeydew;
            }

            // CONSTRUCCION ID
            string primerNombre = txt_nombre_1_persona_legisladora.Text;
            string segundoNombre = txt_nombre_2_persona_legisladora.Text;
            string tercerNombre = txt_nombre_3_persona_legisladora.Text;
            string primerApellido = txt_apellido_1_persona_legisladora.Text;
            string segundoApellido = txt_apellido_2_persona_legisladora.Text;
            string tercerApellido = txt_apellido_3_persona_legisladora.Text;
            string sexo1 = cmb_sexo_persona_legisladora.Text;
            DateTime fechaNacimiento = dtp_fecha_nacimiento_persona_legisladora.Value;

            string uniqueID = GenerateUniqueID(primerNombre, segundoNombre, tercerNombre,
                    primerApellido, segundoApellido, tercerApellido,
                    sexo1, fechaNacimiento);
            txt_ID_persona_legisladora.Text = uniqueID;
        }
        public static string GenerateUniqueID(string primerNombre, string segundoNombre, string tercerNombre,
            string primerApellido, string segundoApellido, string tercerApellido,
            string sexo, DateTime fechaNacimiento)
        {
            // Concatenar los datos en un string
            string dataToHash = $"{primerNombre}{segundoNombre}{tercerNombre}{primerApellido}{segundoApellido}{tercerApellido}{sexo}{fechaNacimiento.ToString("yyyyMMdd")}";

            // Generar el hash SHA-256
            string uniqueID = CalculateSHA256(dataToHash);

            return uniqueID.Substring(0, 12); // Tomamos solo los primeros 12 caracteres del hash
        }
        private static string CalculateSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("x2")); // Convierte cada byte a su representación hexadecimal
                }
                return sb.ToString();
            }
        }
        private void txt_nombre_2_persona_legisladora_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_nombre_2_persona_legisladora.Text = txt_nombre_2_persona_legisladora.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_nombre_2_persona_legisladora.SelectionStart = txt_nombre_2_persona_legisladora.Text.Length;

            if (string.IsNullOrWhiteSpace(txt_nombre_2_persona_legisladora.Text))
            {
                //MessageBox.Show("Debe especificar el nombre.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_nombre_3_persona_legisladora.BackColor = Color.LightGray; txt_nombre_3_persona_legisladora.Enabled = false;
                txt_nombre_3_persona_legisladora.Clear();
                txt_nombre_2_persona_legisladora.Focus();
            }
            else
            {
                txt_nombre_3_persona_legisladora.Enabled = true; txt_nombre_3_persona_legisladora.BackColor = Color.Honeydew;
            }

            // CONSTRUCCION ID
            string primerNombre = txt_nombre_1_persona_legisladora.Text;
            string segundoNombre = txt_nombre_2_persona_legisladora.Text;
            string tercerNombre = txt_nombre_3_persona_legisladora.Text;
            string primerApellido = txt_apellido_1_persona_legisladora.Text;
            string segundoApellido = txt_apellido_2_persona_legisladora.Text;
            string tercerApellido = txt_apellido_3_persona_legisladora.Text;
            string sexo1 = cmb_sexo_persona_legisladora.Text;
            DateTime fechaNacimiento = dtp_fecha_nacimiento_persona_legisladora.Value;

            string uniqueID = GenerateUniqueID(primerNombre, segundoNombre, tercerNombre,
                    primerApellido, segundoApellido, tercerApellido,
                    sexo1, fechaNacimiento);
            txt_ID_persona_legisladora.Text = uniqueID;
        }
        private void txt_nombre_3_persona_legisladora_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_nombre_3_persona_legisladora.Text = txt_nombre_3_persona_legisladora.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_nombre_3_persona_legisladora.SelectionStart = txt_nombre_3_persona_legisladora.Text.Length;

            // CONSTRUCCION ID
            string primerNombre = txt_nombre_1_persona_legisladora.Text;
            string segundoNombre = txt_nombre_2_persona_legisladora.Text;
            string tercerNombre = txt_nombre_3_persona_legisladora.Text;
            string primerApellido = txt_apellido_1_persona_legisladora.Text;
            string segundoApellido = txt_apellido_2_persona_legisladora.Text;
            string tercerApellido = txt_apellido_3_persona_legisladora.Text;
            string sexo1 = cmb_sexo_persona_legisladora.Text;
            DateTime fechaNacimiento = dtp_fecha_nacimiento_persona_legisladora.Value;

            string uniqueID = GenerateUniqueID(primerNombre, segundoNombre, tercerNombre,
                    primerApellido, segundoApellido, tercerApellido,
                    sexo1, fechaNacimiento);
            txt_ID_persona_legisladora.Text = uniqueID;
        }
        private void txt_apellido_1_persona_legisladora_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_apellido_1_persona_legisladora.Text = txt_apellido_1_persona_legisladora.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_apellido_1_persona_legisladora.SelectionStart = txt_apellido_1_persona_legisladora.Text.Length;

            if (string.IsNullOrWhiteSpace(txt_apellido_1_persona_legisladora.Text))
            {
                //MessageBox.Show("Debe especificar el apellido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_apellido_2_persona_legisladora.BackColor = Color.LightGray; txt_apellido_2_persona_legisladora.Enabled = false;
                txt_apellido_2_persona_legisladora.Clear();
                txt_apellido_1_persona_legisladora.Focus();
            }
            else
            {
                txt_apellido_2_persona_legisladora.Enabled = true; txt_apellido_2_persona_legisladora.BackColor = Color.Honeydew;

                // CONSTRUCCION ID
                string primerNombre = txt_nombre_1_persona_legisladora.Text;
                string segundoNombre = txt_nombre_2_persona_legisladora.Text;
                string tercerNombre = txt_nombre_3_persona_legisladora.Text;
                string primerApellido = txt_apellido_1_persona_legisladora.Text;
                string segundoApellido = txt_apellido_2_persona_legisladora.Text;
                string tercerApellido = txt_apellido_3_persona_legisladora.Text;
                string sexo1 = cmb_sexo_persona_legisladora.Text;
                DateTime fechaNacimiento = dtp_fecha_nacimiento_persona_legisladora.Value;

                string uniqueID = GenerateUniqueID(primerNombre, segundoNombre, tercerNombre,
                    primerApellido, segundoApellido, tercerApellido,
                    sexo1, fechaNacimiento);
                txt_ID_persona_legisladora.Text = uniqueID;
            }
        }
        private void txt_apellido_2_persona_legisladora_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_apellido_2_persona_legisladora.Text = txt_apellido_2_persona_legisladora.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_apellido_2_persona_legisladora.SelectionStart = txt_apellido_2_persona_legisladora.Text.Length;

            if (string.IsNullOrWhiteSpace(txt_apellido_2_persona_legisladora.Text))
            {
                //MessageBox.Show("Debe especificar el apellido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_apellido_3_persona_legisladora.BackColor = Color.LightGray; txt_apellido_3_persona_legisladora.Enabled = false;
                txt_apellido_3_persona_legisladora.Clear();
                txt_apellido_2_persona_legisladora.Focus();
            }
            else
            {
                txt_apellido_3_persona_legisladora.Enabled = true; txt_apellido_3_persona_legisladora.BackColor = Color.Honeydew;

                // CONSTRUCCION ID
                string primerNombre = txt_nombre_1_persona_legisladora.Text;
                string segundoNombre = txt_nombre_2_persona_legisladora.Text;
                string tercerNombre = txt_nombre_3_persona_legisladora.Text;
                string primerApellido = txt_apellido_1_persona_legisladora.Text;
                string segundoApellido = txt_apellido_2_persona_legisladora.Text;
                string tercerApellido = txt_apellido_3_persona_legisladora.Text;
                string sexo1 = cmb_sexo_persona_legisladora.Text;
                DateTime fechaNacimiento = dtp_fecha_nacimiento_persona_legisladora.Value;

                string uniqueID = GenerateUniqueID(primerNombre, segundoNombre, tercerNombre,
                    primerApellido, segundoApellido, tercerApellido,
                    sexo1, fechaNacimiento);
                txt_ID_persona_legisladora.Text = uniqueID;
            }
        }
        private void txt_apellido_3_persona_legisladora_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_apellido_3_persona_legisladora.Text = txt_apellido_3_persona_legisladora.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_apellido_3_persona_legisladora.SelectionStart = txt_apellido_3_persona_legisladora.Text.Length;

            // CONSTRUCCION ID
            string primerNombre = txt_nombre_1_persona_legisladora.Text;
            string segundoNombre = txt_nombre_2_persona_legisladora.Text;
            string tercerNombre = txt_nombre_3_persona_legisladora.Text;
            string primerApellido = txt_apellido_1_persona_legisladora.Text;
            string segundoApellido = txt_apellido_2_persona_legisladora.Text;
            string tercerApellido = txt_apellido_3_persona_legisladora.Text;
            string sexo1 = cmb_sexo_persona_legisladora.Text;
            DateTime fechaNacimiento = dtp_fecha_nacimiento_persona_legisladora.Value;

            string uniqueID = GenerateUniqueID(primerNombre, segundoNombre, tercerNombre,
                    primerApellido, segundoApellido, tercerApellido,
                    sexo1, fechaNacimiento);
            txt_ID_persona_legisladora.Text = uniqueID;
        }
        private void txt_otro_estatus_persona_legisladora_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_otro_estatus_persona_legisladora_especifique.Text = txt_otro_estatus_persona_legisladora_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_otro_estatus_persona_legisladora_especifique.SelectionStart = txt_otro_estatus_persona_legisladora_especifique.Text.Length;
        }
        private void cmb_estatus_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_estatus_persona_legisladora.Text.ToString();

            if (valorComboBox1 == "Otro estatus (especifique)")
            {
                txt_otro_estatus_persona_legisladora_especifique.Enabled = true; txt_otro_estatus_persona_legisladora_especifique.BackColor = Color.Honeydew;
                txt_otro_estatus_persona_legisladora_especifique.Focus();
            }
            else
            {
                txt_otro_estatus_persona_legisladora_especifique.Enabled = false; txt_otro_estatus_persona_legisladora_especifique.BackColor = Color.LightGray;
                txt_otro_estatus_persona_legisladora_especifique.Text = "";
            }

            if (valorComboBox1 == "Fallecimiento")
            {
                cbm_causa_fallecimiento_persona_legisladora.Enabled = true; cbm_causa_fallecimiento_persona_legisladora.BackColor = Color.Honeydew;
                cbm_causa_fallecimiento_persona_legisladora.Focus();
            }
            else
            {
                cbm_causa_fallecimiento_persona_legisladora.Enabled = false; cbm_causa_fallecimiento_persona_legisladora.BackColor = Color.LightGray;
                cbm_causa_fallecimiento_persona_legisladora.SelectedIndex = -1;
            }

            if (valorComboBox1 == "Con licencia")
            {
                cbm_tipo_licencia_persona_legisladora.Enabled = true; cbm_tipo_licencia_persona_legisladora.BackColor = Color.Honeydew;
                cbm_tipo_licencia_persona_legisladora.Focus();
            }
            else
            {
                cbm_tipo_licencia_persona_legisladora.Enabled = false; cbm_tipo_licencia_persona_legisladora.BackColor = Color.LightGray;
                cbm_tipo_licencia_persona_legisladora.SelectedIndex = -1;
            }
        }
        private void txt_otro_estatus_persona_legisladora_especifique_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_otro_estatus_persona_legisladora_especifique.Text))
            {
                MessageBox.Show("Debe especificar el otro tipo de estatus.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_otro_estatus_persona_legisladora_especifique.Focus();
            }
        }
        private void cmb_caracter_cargo_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_caracter_cargo_persona_legisladora.Text.ToString();

            if (valorComboBox1 == "Suplente")
            {
                cmb_Persona_Legisladora_Propietaria();
                cmb_nombre_persona_legisladora_propietaria.Enabled = true; cmb_nombre_persona_legisladora_propietaria.BackColor = Color.Honeydew;
                cmb_nombre_persona_legisladora_propietaria.Focus();
            }
            else
            {
                cmb_nombre_persona_legisladora_propietaria.Enabled = false; cmb_nombre_persona_legisladora_propietaria.BackColor = Color.LightGray;
                cmb_nombre_persona_legisladora_propietaria.SelectedIndex = -1; ;
            }
        }
        private void cmb_escolaridad_persona_legisladora_PL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            if (cmb_escolaridad_persona_legisladora_PL.SelectedItem != null)
            {
                // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
                string valorComboBox = cmb_escolaridad_persona_legisladora_PL.Text.ToString();

                
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

                        cmb_estatus_escolaridad_persona_legisladora.DataSource = dataTable;
                        cmb_estatus_escolaridad_persona_legisladora.DisplayMember = "descripcion";

                        cmb_estatus_escolaridad_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_estatus_escolaridad_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_estatus_escolaridad_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_estatus_escolaridad_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío

                        if (valorComboBox == "Licenciatura")
                        {
                            cmb_carrera_licenciatura_persona_legisladora_PL.Enabled = true; cmb_carrera_licenciatura_persona_legisladora_PL.BackColor = Color.Honeydew;
                            cmb_carrera_maestria_persona_legisladora_PL.Enabled = false; cmb_carrera_maestria_persona_legisladora_PL.BackColor = Color.LightGray;
                            cmb_carrera_doctorado_persona_legisladora_PL.Enabled = false; cmb_carrera_doctorado_persona_legisladora_PL.BackColor = Color.LightGray;
                            dgv_nivel_escolaridad_PL.BackgroundColor = Color.Honeydew;

                            cmb_carrera_licenciatura_persona_legisladora_PL.Focus();
                            btnAgregarNivelEscPL.Enabled = true; btnEliminarNivelEscPL.Enabled = true;

                            cmb_carrera_maestria_persona_legisladora_PL.Text = ""; cmb_carrera_doctorado_persona_legisladora_PL.Text = "";
                        }
                        else if (valorComboBox == "Maestría")
                        {

                            cmb_carrera_licenciatura_persona_legisladora_PL.Enabled = true; cmb_carrera_licenciatura_persona_legisladora_PL.BackColor = Color.Honeydew;
                            cmb_carrera_maestria_persona_legisladora_PL.Enabled = true; cmb_carrera_maestria_persona_legisladora_PL.BackColor = Color.Honeydew;
                            cmb_carrera_doctorado_persona_legisladora_PL.Enabled = false; cmb_carrera_doctorado_persona_legisladora_PL.BackColor = Color.LightGray;
                            dgv_nivel_escolaridad_PL.BackgroundColor = Color.Honeydew;
                            cmb_carrera_licenciatura_persona_legisladora_PL.Focus();

                            btnAgregarNivelEscPL.Enabled = true; btnEliminarNivelEscPL.Enabled = true;

                            cmb_carrera_doctorado_persona_legisladora_PL.Text = "";
                        }
                        else if (valorComboBox == "Doctorado")
                        {
                            cmb_carrera_licenciatura_persona_legisladora_PL.Enabled = true; cmb_carrera_licenciatura_persona_legisladora_PL.BackColor = Color.Honeydew;
                            cmb_carrera_maestria_persona_legisladora_PL.Enabled = true; cmb_carrera_maestria_persona_legisladora_PL.BackColor = Color.Honeydew;
                            cmb_carrera_doctorado_persona_legisladora_PL.Enabled = true; cmb_carrera_doctorado_persona_legisladora_PL.BackColor = Color.Honeydew;
                            dgv_nivel_escolaridad_PL.BackgroundColor = Color.Honeydew;

                            btnAgregarNivelEscPL.Enabled = true; btnEliminarNivelEscPL.Enabled = true;

                            cmb_carrera_licenciatura_persona_legisladora_PL.Focus();
                        }
                        else
                        {
                            cmb_carrera_licenciatura_persona_legisladora_PL.Enabled = false; cmb_carrera_licenciatura_persona_legisladora_PL.BackColor = Color.LightGray;
                            cmb_carrera_maestria_persona_legisladora_PL.Enabled = false; cmb_carrera_maestria_persona_legisladora_PL.BackColor = Color.LightGray;
                            cmb_carrera_doctorado_persona_legisladora_PL.Enabled = false; cmb_carrera_doctorado_persona_legisladora_PL.BackColor = Color.LightGray;
                            dgv_nivel_escolaridad_PL.BackgroundColor = Color.LightGray;
                            dgv_nivel_escolaridad_PL.Rows.Clear();

                            cmb_carrera_licenciatura_persona_legisladora_PL.Text = ""; cmb_carrera_maestria_persona_legisladora_PL.Text = "";
                            cmb_carrera_doctorado_persona_legisladora_PL.Text = "";

                            btnAgregarNivelEscPL.Enabled = false; btnEliminarNivelEscPL.Enabled = false;
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
            else { 
            }

            
        }
        private void btnAgregarNivelEscPL_Click(object sender, EventArgs e)
        {
            // se obtienen los valores
            string lic_pl = cmb_carrera_licenciatura_persona_legisladora_PL.Text.Trim();
            string mae_pl = cmb_carrera_maestria_persona_legisladora_PL.Text.Trim();
            string doc_pl = cmb_carrera_doctorado_persona_legisladora_PL.Text.Trim();

            //if (string.IsNullOrWhiteSpace(cmb_carrera_licenciatura_persona_legisladora_PL.Text))
            //{
            //    MessageBox.Show("Revisar datos vacios");
            //}
            //else
            //{
                
                    // Agregar una nueva fila al DataGridView
                    dgv_nivel_escolaridad_PL.Rows.Add(lic_pl, mae_pl, doc_pl);

                    cmb_carrera_licenciatura_persona_legisladora_PL.Text = "";
                    //cmb_carrera_licenciatura_persona_legisladora_PL.Enabled = false; cmb_carrera_licenciatura_persona_legisladora_PL.BackColor = Color.LightGray;

                cmb_carrera_maestria_persona_legisladora_PL.Text = "";
                //cmb_carrera_maestria_persona_legisladora_PL.Enabled = false; cmb_carrera_maestria_persona_legisladora_PL.BackColor = Color.LightGray;

                cmb_carrera_doctorado_persona_legisladora_PL.Text = "";
                //cmb_carrera_doctorado_persona_legisladora_PL.Enabled = false; cmb_carrera_doctorado_persona_legisladora_PL.BackColor = Color.LightGray;

            //}
        }
        private void btnEliminarNivelEscPL_Click(object sender, EventArgs e)
        {
            if (dgv_nivel_escolaridad_PL.SelectedRows.Count > 0)
            {
                dgv_nivel_escolaridad_PL.Rows.RemoveAt(dgv_nivel_escolaridad_PL.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }
        private void cmb_cond_lengua_ind_persona_legisladora_PL_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_lengua_ind_persona_legisladora_PL.Text.ToString();

            if (valorComboBox1 == "Si")
            {
                cmb_lengua_ind_persona_legisladora.Enabled = true; cmb_lengua_ind_persona_legisladora.BackColor = Color.Honeydew;
                btnAgregarLenguaPA.Enabled = true; btnEliminarLenguaPA.Enabled = true;
                dgv_lengua_PA.BackgroundColor = Color.Honeydew;
                cmb_lengua_ind_persona_legisladora.Focus();
            }
            else
            {
                cmb_lengua_ind_persona_legisladora.Enabled = false; cmb_lengua_ind_persona_legisladora.BackColor = Color.LightGray;
                dgv_lengua_PA.Rows.Clear(); dgv_lengua_PA.BackgroundColor = Color.LightGray;
                btnAgregarLenguaPA.Enabled = false; btnEliminarLenguaPA.Enabled = false;
                
                cmb_lengua_ind_persona_legisladora.Text = "";
            }
        }
        private void btnAgregarLenguaPL_Click(object sender, EventArgs e)
        {
            // se obtienen los valores
            string lengua_pl = cmb_lengua_ind_persona_legisladora.Text.Trim();
            

            if (string.IsNullOrWhiteSpace(cmb_lengua_ind_persona_legisladora.Text))
            {
                MessageBox.Show("Revisar datos vacios");
            }
            else
            {

            // Agregar una nueva fila al DataGridView
            dgv_lengua_PA.Rows.Add(lengua_pl);

            cmb_lengua_ind_persona_legisladora.Text = "";
            
            }
        }
        private void btnEliminarLenguaPL_Click(object sender, EventArgs e)
        {
            if (dgv_lengua_PA.SelectedRows.Count > 0)
            {
                dgv_lengua_PA.Rows.RemoveAt(dgv_lengua_PA.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }
        private void cmb_cond_pueblo_ind_persona_legisladora_PL_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_pueblo_ind_persona_legisladora_PL.Text.ToString();

            if (valorComboBox1 == "Si")
            {
                cmb_pueblo_ind_persona_legisladora_PL.Enabled = true; cmb_pueblo_ind_persona_legisladora_PL.BackColor = Color.Honeydew;
                cmb_pueblo_ind_persona_legisladora_PL.Focus();
            }
            else
            {
                cmb_pueblo_ind_persona_legisladora_PL.Enabled = false; cmb_pueblo_ind_persona_legisladora_PL.BackColor = Color.LightGray;
                cmb_pueblo_ind_persona_legisladora_PL.Text = "";
            }
        }
        private void cmb_cond_discapacidad_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_discapacidad_persona_legisladora.Text.ToString();

            if (valorComboBox1 == "Si")
            {
                cmb_tipo_discapacidad_persona_legisladora.Enabled = true; cmb_tipo_discapacidad_persona_legisladora.BackColor = Color.Honeydew;
                btnAgregarDiscapacidadPA.Enabled = true; btnEliminarDiscapacidadPA.Enabled = true;
                dgv_tipo_discapacidad_PA.BackgroundColor = Color.Honeydew;
                cmb_tipo_discapacidad_persona_legisladora.Focus();
            }
            else
            {
                cmb_tipo_discapacidad_persona_legisladora.Enabled = false; cmb_tipo_discapacidad_persona_legisladora.BackColor = Color.LightGray;
                dgv_tipo_discapacidad_PA.Rows.Clear(); dgv_tipo_discapacidad_PA.BackgroundColor = Color.LightGray;
                btnAgregarDiscapacidadPA.Enabled = false; btnEliminarDiscapacidadPA.Enabled = false; 
                cmb_tipo_discapacidad_persona_legisladora.Text = "";
            }
        }
        private void btnAgregarDiscapacidadPL_Click(object sender, EventArgs e)
        {
            // se obtienen los valores
            string tipo_discapacidad_pl = cmb_tipo_discapacidad_persona_legisladora.Text.Trim();


            if (string.IsNullOrWhiteSpace(cmb_tipo_discapacidad_persona_legisladora.Text))
            {
                MessageBox.Show("Revisar datos vacios");
            }
            else
            {

                // Agregar una nueva fila al DataGridView
                dgv_tipo_discapacidad_PA.Rows.Add(tipo_discapacidad_pl);

                cmb_tipo_discapacidad_persona_legisladora.Text = "";

            }
        }
        private void btnEliminarDiscapacidadPL_Click(object sender, EventArgs e)
        {
            if (dgv_tipo_discapacidad_PA.SelectedRows.Count > 0)
            {
                dgv_tipo_discapacidad_PA.Rows.RemoveAt(dgv_tipo_discapacidad_PA.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }
        private void cmb_sexo_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_sexo_persona_legisladora.Text.ToString();

            if (valorComboBox1 == "No binario")
            {
                cmb_cond_pob_diversidad_sexual_persona_legisladora.Enabled = true; cmb_cond_pob_diversidad_sexual_persona_legisladora.BackColor = Color.Honeydew;
                cmb_cond_pob_diversidad_sexual_persona_legisladora.Focus();
            }
            else
            {
                cmb_cond_pob_diversidad_sexual_persona_legisladora.Enabled = false; cmb_cond_pob_diversidad_sexual_persona_legisladora.BackColor = Color.LightGray;
                cmb_cond_pob_diversidad_sexual_persona_legisladora.Text = "";
            }

            // CONSTRUCCION ID
            string primerNombre = txt_nombre_1_persona_legisladora.Text;
            string segundoNombre = txt_nombre_2_persona_legisladora.Text;
            string tercerNombre = txt_nombre_3_persona_legisladora.Text;
            string primerApellido = txt_apellido_1_persona_legisladora.Text;
            string segundoApellido = txt_apellido_2_persona_legisladora.Text;
            string tercerApellido = txt_apellido_3_persona_legisladora.Text;
            string sexo1 = cmb_sexo_persona_legisladora.Text;
            DateTime fechaNacimiento = dtp_fecha_nacimiento_persona_legisladora.Value;

           
                string uniqueID = GenerateUniqueID(primerNombre, segundoNombre, tercerNombre,
                    primerApellido, segundoApellido, tercerApellido,
                    sexo1, fechaNacimiento);
                txt_ID_persona_legisladora.Text = uniqueID;
               
        }
        private void dtp_fecha_nacimiento_persona_legisladora_ValueChanged(object sender, EventArgs e)
        {
            // CONSTRUCCION ID
            string primerNombre = txt_nombre_1_persona_legisladora.Text;
            string segundoNombre = txt_nombre_2_persona_legisladora.Text;
            string tercerNombre = txt_nombre_3_persona_legisladora.Text;
            string primerApellido = txt_apellido_1_persona_legisladora.Text;
            string segundoApellido = txt_apellido_2_persona_legisladora.Text;
            string tercerApellido = txt_apellido_3_persona_legisladora.Text;
            string sexo1 = cmb_sexo_persona_legisladora.Text;
            DateTime fechaNacimiento = dtp_fecha_nacimiento_persona_legisladora.Value;

           
                string uniqueID = GenerateUniqueID(primerNombre, segundoNombre, tercerNombre,
                    primerApellido, segundoApellido, tercerApellido,
                    sexo1, fechaNacimiento);
                txt_ID_persona_legisladora.Text = uniqueID;
            
        }
        private void cmb_cond_pob_diversidad_sexual_persona_legisladora_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmb_cond_pob_diversidad_sexual_persona_legisladora.Text))
            {
                MessageBox.Show("Debe especificar la condición de la persona legisladora de formar parte de algún grupo de la diversidad sexual.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_cond_pob_diversidad_sexual_persona_legisladora.Focus();
            }
        }
        private void cmb_forma_eleccion_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_forma_eleccion_persona_legisladora.Text.ToString();

            if (valorComboBox1 == "Mayoría relativa")
            {
                cmb_distrito_electoral_mayoria_relativa.Enabled = true; cmb_distrito_electoral_mayoria_relativa.BackColor = Color.Honeydew;
                cmb_distrito_electoral_mayoria_relativa.Focus();

                cmb_tipo_candidatura_persona_legisladora.Enabled = true; cmb_tipo_candidatura_persona_legisladora.BackColor = Color.Honeydew;

                string cadena = "Data Source = DB_PLE.db;Version=3;";
                int distritos_uni;

                distritos_uni = int.Parse(Txt_distritos_uninominales.Text);

                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_NUM_LEGISLATURA WHERE id_numero_legislatura BETWEEN 1 AND @distritos_uni";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Agregar parámetro al comando
                        cmd.Parameters.AddWithValue("@distritos_uni", distritos_uni);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        cmb_distrito_electoral_mayoria_relativa.DataSource = dataTable;
                        cmb_distrito_electoral_mayoria_relativa.DisplayMember = "descripcion";

                        cmb_distrito_electoral_mayoria_relativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_distrito_electoral_mayoria_relativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_distrito_electoral_mayoria_relativa.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_distrito_electoral_mayoria_relativa.SelectedIndex = -1; // Aquí se establece como vacío
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al llenar el ComboBox cmb_forma_eleccion_persona_legisladora_SelectedIndexChanged: " + ex.Message);
                    }
                    finally
                    {
                        conexion.Close();
                    }

                }

            }
            else
            {
                cmb_distrito_electoral_mayoria_relativa.Enabled = false; cmb_distrito_electoral_mayoria_relativa.BackColor = Color.LightGray;
                cmb_tipo_candidatura_persona_legisladora.Enabled = false; cmb_tipo_candidatura_persona_legisladora.BackColor = Color.LightGray;

                cmb_tipo_candidatura_persona_legisladora.Text = "";
                cmb_distrito_electoral_mayoria_relativa.Text = "";
            }
        }
        private void cmb_tipo_candidatura_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_tipo_candidatura_persona_legisladora.Text.ToString();

            if (valorComboBox1 == "Candidatura por partido único")
            {
                cmb_partido_politico_candidatura_coalicion.Enabled = false; cmb_partido_politico_candidatura_coalicion.BackColor = Color.LightGray;
                cmb_partido_politico_candidatura_partido_unico.Enabled = true; cmb_partido_politico_candidatura_partido_unico.BackColor = Color.Honeydew;

                btnAgregarCandidaturaPL.Enabled = false; btnEliminarCandidaturaPL.Enabled = false;
                dgv_partido_coalicion.Rows.Clear(); dgv_partido_coalicion.BackgroundColor = Color.LightGray;

                cmb_partido_politico_candidatura_coalicion.Text = "";
                cmb_partido_politico_candidatura_partido_unico.Focus();

                

                string cadena = "Data Source = DB_PLE.db;Version=3;";
                
                // SE AGREGAN LOS PARTIDOS POLITICOS---------------------------------------------------
                string ent_dg;
                ent_dg = cmb_entidad_federativa.Text;

                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_PARTIDOS_POLITICOS WHERE entidad =  @ent_dg";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Agregar parámetro al comando
                        cmd.Parameters.AddWithValue("@ent_dg", ent_dg);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        cmb_partido_politico_candidatura_partido_unico.DataSource = dataTable;
                        cmb_partido_politico_candidatura_partido_unico.DisplayMember = "descripcion";

                        cmb_partido_politico_candidatura_partido_unico.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_partido_politico_candidatura_partido_unico.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_partido_politico_candidatura_partido_unico.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_partido_politico_candidatura_partido_unico.SelectedIndex = -1; // Aquí se establece como vacío
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al llenar el ComboBox cmb_tipo_candidatura_persona_legisladora_SelectedIndexChanged: " + ex.Message);
                    }
                    finally
                    {
                        conexion.Close();
                    }

                }

                // SE AGREGAN TIPO DE ADSCRIPCION---------------------------------------------------
                
                //inicial
                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_TIPO_ADSCRIPCION WHERE id_tip_adscripcion in  (1,3,9)";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Agregar parámetro al comando
                        //cmd.Parameters.AddWithValue("@ent_dg", ent_dg);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(query,conexion);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // inicial
                        cmb_tipo_adscripcion_inicial_persona_legisladora.DataSource = dataTable;
                        cmb_tipo_adscripcion_inicial_persona_legisladora.DisplayMember = "descripcion";

                        cmb_tipo_adscripcion_inicial_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_tipo_adscripcion_inicial_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_tipo_adscripcion_inicial_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_tipo_adscripcion_inicial_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_TIPO_ADSCRIPCION WHERE id_tip_adscripcion in  (1,3,9)";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Agregar parámetro al comando
                        //cmd.Parameters.AddWithValue("@ent_dg", ent_dg);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);


                        // final
                        cmb_tipo_adscripcion_final_persona_legisladora.DataSource = dataTable;
                        cmb_tipo_adscripcion_final_persona_legisladora.DisplayMember = "descripcion";

                        cmb_tipo_adscripcion_final_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_tipo_adscripcion_final_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_tipo_adscripcion_final_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_tipo_adscripcion_final_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
            else if (valorComboBox1 == "Candidatura por coalición")
            {
                cmb_partido_politico_candidatura_coalicion.Enabled = true; cmb_partido_politico_candidatura_coalicion.BackColor = Color.Honeydew;
                cmb_partido_politico_candidatura_partido_unico.Enabled = false; cmb_partido_politico_candidatura_partido_unico.BackColor = Color.LightGray;
                cmb_partido_politico_candidatura_partido_unico.Text = "";
                dgv_partido_coalicion.BackgroundColor = Color.Honeydew;
                btnAgregarCandidaturaPL.Enabled = true; btnEliminarCandidaturaPL.Enabled = true;
                cmb_partido_politico_candidatura_coalicion.Focus();

                //cmb_tipo_candidatura_persona_legisladora.Enabled = true; cmb_tipo_candidatura_persona_legisladora.BackColor = Color.Honeydew;

                string cadena = "Data Source = DB_PLE.db;Version=3;";

                // SE AGREGAN LOS PARTIDOS POLITICOS---------------------------------------------------
                string ent_dg;
                ent_dg = cmb_entidad_federativa.Text;

                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_PARTIDOS_POLITICOS WHERE entidad =  @ent_dg";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Agregar parámetro al comando
                        cmd.Parameters.AddWithValue("@ent_dg", ent_dg);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        
                        cmb_partido_politico_candidatura_coalicion.DataSource = dataTable;
                        cmb_partido_politico_candidatura_coalicion.DisplayMember = "descripcion";

                        cmb_partido_politico_candidatura_coalicion.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_partido_politico_candidatura_coalicion.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_partido_politico_candidatura_coalicion.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_partido_politico_candidatura_coalicion.SelectedIndex = -1; // Aquí se establece como vacío
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
                // SE AGREGAN TIPO DE ADSCRIPCION---------------------------------------------------

                //inicial
                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_TIPO_ADSCRIPCION WHERE id_tip_adscripcion in  (1,3,9)";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Agregar parámetro al comando
                        //cmd.Parameters.AddWithValue("@ent_dg", ent_dg);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // inicial
                        cmb_tipo_adscripcion_inicial_persona_legisladora.DataSource = dataTable;
                        cmb_tipo_adscripcion_inicial_persona_legisladora.DisplayMember = "descripcion";

                        cmb_tipo_adscripcion_inicial_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_tipo_adscripcion_inicial_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_tipo_adscripcion_inicial_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_tipo_adscripcion_inicial_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío

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

                //final
                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_TIPO_ADSCRIPCION WHERE id_tip_adscripcion in  (1,3,9)";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Agregar parámetro al comando
                        //cmd.Parameters.AddWithValue("@ent_dg", ent_dg);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // final
                        cmb_tipo_adscripcion_final_persona_legisladora.DataSource = dataTable;
                        cmb_tipo_adscripcion_final_persona_legisladora.DisplayMember = "descripcion";

                        cmb_tipo_adscripcion_final_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_tipo_adscripcion_final_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_tipo_adscripcion_final_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_tipo_adscripcion_final_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
                // SE AGREGAN TIPO DE ADSCRIPCION---------------------------------------------------
                string cadena = "Data Source = DB_PLE.db;Version=3;";

                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_TIPO_ADSCRIPCION ";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Agregar parámetro al comando
                        //cmd.Parameters.AddWithValue("@ent_dg", ent_dg);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        cmb_tipo_adscripcion_inicial_persona_legisladora.DataSource = dataTable;
                        cmb_tipo_adscripcion_inicial_persona_legisladora.DisplayMember = "descripcion";

                        cmb_tipo_adscripcion_inicial_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_tipo_adscripcion_inicial_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_tipo_adscripcion_inicial_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_tipo_adscripcion_inicial_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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

                cmb_partido_politico_candidatura_partido_unico.Enabled = false; cmb_partido_politico_candidatura_partido_unico.BackColor = Color.LightGray;
                cmb_partido_politico_candidatura_coalicion.Enabled = false; cmb_partido_politico_candidatura_coalicion.BackColor = Color.LightGray;
                btnAgregarCandidaturaPL.Enabled = false; btnEliminarCandidaturaPL.Enabled = false;
                dgv_partido_coalicion.Rows.Clear(); dgv_partido_coalicion.BackgroundColor = Color.LightGray;
                cmb_partido_politico_candidatura_partido_unico.Text = "";
                cmb_partido_politico_candidatura_coalicion.Text = "";

            }
        }
        private void btnAgregarCandidaturaPL_Click(object sender, EventArgs e)
        {
            // se obtienen los valores
            string partido_coalicion_pl = cmb_partido_politico_candidatura_coalicion.Text.Trim();


            if (string.IsNullOrWhiteSpace(cmb_partido_politico_candidatura_coalicion.Text))
            {
                MessageBox.Show("Revisar datos vacios");
            }
            else
            {

                // Agregar una nueva fila al DataGridView
                dgv_partido_coalicion.Rows.Add(partido_coalicion_pl);

                cmb_partido_politico_candidatura_coalicion.Text = "";

            }
        }
        private void btnEliminarCandidaturaPL_Click(object sender, EventArgs e)
        {
            if (dgv_partido_coalicion.SelectedRows.Count > 0)
            {
                dgv_partido_coalicion.Rows.RemoveAt(dgv_partido_coalicion.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }
        private void cmb_tipo_adscripcion_inicial_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_tipo_adscripcion_inicial_persona_legisladora.Text.ToString();

            if (valorComboBox1 == "Grupo parlamentario")
            {
                cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.Enabled = true; cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.BackColor = Color.Honeydew;
                //cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.Focus();


                string cadena = "Data Source = DB_PLE.db;Version=3;";

                // SE AGREGAN LOS PARTIDOS POLITICOS---------------------------------------------------
                string ent_dg;
                ent_dg = cmb_entidad_federativa.Text;

                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_GRUPO_PARLAMENTARIO WHERE entidad =  @ent_dg";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Agregar parámetro al comando
                        cmd.Parameters.AddWithValue("@ent_dg", ent_dg);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.DataSource = dataTable;
                        cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.DisplayMember = "descripcion";

                        cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
                txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.Enabled = false; txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.BackColor = Color.LightGray;
                cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.Enabled = true; cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.BackColor = Color.LightGray;
                cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.Text = "";
                txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.Text = "";

            }
        }
        private void txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.Text = txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.SelectionStart = txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.Text.Length;
        }
        private void cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_grupo_parlamentario_adscipcion_inicial_persona_legisladora.Text.ToString();

            if (valorComboBox1 == "Otro grupo parlamentario (especifique)")
            {
                txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.Enabled = true; txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.BackColor = Color.Honeydew;
                txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.Focus();

            }
            else
            {
                txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.Enabled = false; txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.BackColor = Color.LightGray;
                txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.Text = "";

            }
        }
        private void txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.Text))
            {
                MessageBox.Show("Debe especificar el otro grupo parlamentario de adscripción inicial de la persona legisladora.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_otro_grupo_parlamentario_adscipcion_inicial_persona_legisladora_especifique.Focus();
            }
        }
        private void cmb_tipo_adscripcion_final_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_tipo_adscripcion_final_persona_legisladora.Text.ToString();

            if (valorComboBox1 == "Grupo parlamentario")
            {
                cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.Enabled = true; cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.BackColor = Color.Honeydew;
                //cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.Focus();


                string cadena = "Data Source = DB_PLE.db;Version=3;";

                // SE AGREGAN LOS PARTIDOS POLITICOS---------------------------------------------------
                string ent_dg;
                ent_dg = cmb_entidad_federativa.Text;

                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_GRUPO_PARLAMENTARIO WHERE entidad =  @ent_dg";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Agregar parámetro al comando
                        cmd.Parameters.AddWithValue("@ent_dg", ent_dg);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.DataSource = dataTable;
                        cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.DisplayMember = "descripcion";

                        cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
                txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.Enabled = false; txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.BackColor = Color.LightGray;
                cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.Enabled = true; cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.BackColor = Color.LightGray;
                cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.Text = "";
                txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.Text = "";
            }
        }
        private void otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.Text = txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.SelectionStart = txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.Text.Length;
        }
        private void cmb_grupo_parlamentario_adscipcion_final_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_grupo_parlamentario_adscipcion_final_persona_legisladora.Text.ToString();

            if (valorComboBox1 == "Otro grupo parlamentario (especifique)")
            {
                txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.Enabled = true; txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.BackColor = Color.Honeydew;
                txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.Focus();

            }
            else
            {
                txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.Enabled = false; txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.BackColor = Color.LightGray;
                txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.Text = "";

            }
        }
        private void txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.Text))
            {
                MessageBox.Show("Debe especificar el otro grupo parlamentario de adscripción inicial de la persona legisladora.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_otro_grupo_parlamentario_adscipcion_final_persona_legisladora_especifique.Focus();
            }
        }
        private void cmb_empleo_anterior_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_empleo_anterior_persona_legisladora.Text.ToString();

            if (valorComboBox1 == "Legislador federal" || valorComboBox1 == "Legislador estatal (reelección)" || valorComboBox1 == "Legislador estatal (de otra entidad federativa)"
                ||  valorComboBox1 == "Gobierno federal" || valorComboBox1 == "Gobierno estatal" || valorComboBox1 == "Gobierno municipal" || valorComboBox1 == "Sindico(a)/ regidor(a)")
            {
                
                string cadena = "Data Source = DB_PLE.db;Version=3;";

                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_ANTIGUEDAD WHERE id_antiguedad between 2 and 101 ";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        cmb_antigüedad_servicio_publico_persona_legisladora.DataSource = dataTable;
                        cmb_antigüedad_servicio_publico_persona_legisladora.DisplayMember = "descripcion";

                        cmb_antigüedad_servicio_publico_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_antigüedad_servicio_publico_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_antigüedad_servicio_publico_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_antigüedad_servicio_publico_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al llenar el ComboBox cmb_empleo_anterior_persona_legisladora_SelectedIndexChanged: " + ex.Message);
                    }
                    finally
                    {
                        conexion.Close();
                    }

                }

            }
            else 
            {
                cmb_Antigüedad_servicio_publico_persona_legisladora(); 
            }

            if (valorComboBox1 == "Legislador federal" || valorComboBox1 == "Legislador estatal (reelección)" || valorComboBox1 == "Legislador estatal (de otra entidad federativa)")
            {
                string cadena = "Data Source = DB_PLE.db;Version=3;";

                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select descripcion from TC_ANTIGUEDAD WHERE id_antiguedad between 2 and 101 ";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Utilizar un DataReader para obtener los datos
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        cmb_antigüedad_persona_legisladora.DataSource = dataTable;
                        cmb_antigüedad_persona_legisladora.DisplayMember = "descripcion";

                        cmb_antigüedad_persona_legisladora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmb_antigüedad_persona_legisladora.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmb_antigüedad_persona_legisladora.DropDownStyle = ComboBoxStyle.DropDown;
                        cmb_antigüedad_persona_legisladora.SelectedIndex = -1; // Aquí se establece como vacío
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
                cmb_Antigüedad_persona_legisladora();
            }
        }
        private void cmb_antigüedad_persona_legisladora_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string ant_pl;
            ant_pl = cmb_antigüedad_persona_legisladora.Text;
            
            if (ant_pl != "No identificado " )
            {
                // se extrae antiguedad persona legisladora
                int ant_pl2;
                int.TryParse(ant_pl, out ant_pl2); // antiguedad como persona legisladora

                if (ant_pl2 != 0)
                {
                    // se extrae la antiguedad como servidor publico
                    string ant_sp_pl; int ant_sp_pl2;
                    ant_sp_pl = cmb_antigüedad_servicio_publico_persona_legisladora.Text;
                    int.TryParse(ant_sp_pl, out ant_sp_pl2); // antiguedad servidor publico

                    if (ant_sp_pl2 != 0)
                    {
                        if (ant_pl2 > ant_sp_pl2)
                        {

                            MessageBox.Show("Debe ser igual o menor a la cantidad reportada en el campo antigüedad en el servicio público.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cmb_antigüedad_persona_legisladora.SelectedIndex = -1; cmb_antigüedad_persona_legisladora.Focus();
                        }
                    }
                }
            }
            /*
            else
            {
                



                    
                    else
                    {

                    }
                
                

            }
            */
        }
        private void txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.Text = txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.SelectionStart = txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.Text.Length;
        }
        private void cmb_cond_presentacion_declaracion_situacion_patrimonial_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_presentacion_declaracion_situacion_patrimonial.Text.ToString();

            if (valorComboBox1 == "No aplica (especifique)")
            {
                txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.Enabled = true; txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.BackColor = Color.Honeydew;
                txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.Focus();

            }
            else
            {
                txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.Enabled = false; txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.BackColor = Color.LightGray;
                txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.Text = "";

            }
        }
        private void txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.Text))
            {
                MessageBox.Show("Debe especificar el motivo por el cual no le es aplicable a la persona legisladora la presentación de la declaración de situación patrimonial.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_no_aplica_presentacion_declaracion_situacion_patrimonial_especifique.Focus();
            }
        }
        private void cmb_cond_presentacion_declaracion_intereses_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_presentacion_declaracion_intereses.Text.ToString();

            if (valorComboBox1 == "No aplica (especifique)")
            {
                txt_no_aplica_presentacion_declaracion_intereses_especifique.Enabled = true; txt_no_aplica_presentacion_declaracion_intereses_especifique.BackColor = Color.Honeydew;
                txt_no_aplica_presentacion_declaracion_intereses_especifique.Focus();

            }
            else
            {
                txt_no_aplica_presentacion_declaracion_intereses_especifique.Enabled = false; txt_no_aplica_presentacion_declaracion_intereses_especifique.BackColor = Color.LightGray;
                txt_no_aplica_presentacion_declaracion_intereses_especifique.Text = "";

            }
        }
        private void txt_no_aplica_presentacion_declaracion_intereses_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_no_aplica_presentacion_declaracion_intereses_especifique.Text = txt_no_aplica_presentacion_declaracion_intereses_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_no_aplica_presentacion_declaracion_intereses_especifique.SelectionStart = txt_no_aplica_presentacion_declaracion_intereses_especifique.Text.Length;

        }
        private void txt_no_aplica_presentacion_declaracion_intereses_especifique_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_no_aplica_presentacion_declaracion_intereses_especifique.Text))
            {
                MessageBox.Show("Debe especificar el motivo por el cual no le es aplicable a la persona legisladora la presentación de la declaración de intereses.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_no_aplica_presentacion_declaracion_intereses_especifique.Focus();
            }
        }
        private void cmb_cond_presentacion_declaracion_fiscal_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_presentacion_declaracion_fiscal.Text.ToString();

            if (valorComboBox1 == "No aplica (especifique)")
            {
                txt_no_aplica_presentacion_declaracion_fiscal_especifique.Enabled = true; txt_no_aplica_presentacion_declaracion_fiscal_especifique.BackColor = Color.Honeydew;
                txt_no_aplica_presentacion_declaracion_fiscal_especifique.Focus();

            }
            else
            {
                txt_no_aplica_presentacion_declaracion_fiscal_especifique.Enabled = false; txt_no_aplica_presentacion_declaracion_fiscal_especifique.BackColor = Color.LightGray;
                txt_no_aplica_presentacion_declaracion_fiscal_especifique.Text = "";

            }
        }
        private void txt_no_aplica_presentacion_declaracion_fiscal_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_no_aplica_presentacion_declaracion_fiscal_especifique.Text = txt_no_aplica_presentacion_declaracion_fiscal_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_no_aplica_presentacion_declaracion_fiscal_especifique.SelectionStart = txt_no_aplica_presentacion_declaracion_fiscal_especifique.Text.Length;
        }
        private void txt_no_aplica_presentacion_declaracion_fiscal_especifique_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_no_aplica_presentacion_declaracion_fiscal_especifique.Text))
            {
                MessageBox.Show("Debe especificar el motivo por el cual no le es aplicable a la persona legisladora la presentación de la declaración fiscal.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_no_aplica_presentacion_declaracion_fiscal_especifique.Focus();
            }
        }
        private void txt_remuneracion_persona_legisladora_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }
        private void txt_asistencia_legislativa_persona_legisladora_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }
        private void txt_gestion_parlamentaria_persona_legisladora_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }
        private void txt_atencion_ciudadana_persona_legisladora_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }
        private void txt_otro_concepto_gasto_persona_legisladora_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter
            }
        }
        private void InitializeMap()
        {
            gMapControl.MapProvider = GMapProviders.GoogleMap;
            gMapControl.Position = new PointLatLng(19.42847, -99.12766); // Centrar el mapa en el ecuador por defecto
            gMapControl.MinZoom = 0;
            gMapControl.MaxZoom = 18;
            gMapControl.Zoom = 6;

            gMapControl.MouseClick += new MouseEventHandler(gMapControl_MouseClick);
            gMapControl.MouseWheel += new MouseEventHandler(gMapControl_MouseWheel); // Añadir manejador de evento MouseWheel

            gMapControl.DragButton = MouseButtons.Left; // Permitir arrastrar con el botón izquierdo del ratón
            gMapControl.CanDragMap = true; // Habilitar el arrastre del mapa
            gMapControl.ShowCenter = false; // Ocultar el cursor central por defecto
            gMapControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter; // Configurar el zoom con la rueda del ratón
            gMapControl.IgnoreMarkerOnMouseWheel = true; // Ignorar los marcadores al hacer zoom con la rueda del ratón


            markersOverlay = new GMapOverlay("markers");
            gMapControl.Overlays.Add(markersOverlay);
        }
        private void gMapControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (gMapControl.Bounds.Contains(PointToClient(Cursor.Position)))
            {
                // Maneja el evento de zoom en el mapa
                if (e.Delta > 0)
                {
                    if (gMapControl.Zoom < gMapControl.MaxZoom)
                    {
                        gMapControl.Zoom++;
                    }
                }
                else if (e.Delta < 0)
                {
                    if (gMapControl.Zoom > gMapControl.MinZoom)
                    {
                        gMapControl.Zoom--;
                    }
                }
        // Marca el evento como manejado para que no se propague
        ((HandledMouseEventArgs)e).Handled = true;
            }
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (gMapControl.Bounds.Contains(PointToClient(Cursor.Position)))
            {
                ((HandledMouseEventArgs)e).Handled = true;
            }
            else
            {
                base.OnMouseWheel(e);
            }
        }
        private void gMapControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PointLatLng point = gMapControl.FromLocalToLatLng(e.X, e.Y);
                txt_latitud_casa_atencion_ciudadana.Text = point.Lat.ToString();
                txt_longitud_casa_atencion_ciudadana.Text = point.Lng.ToString();

                markersOverlay.Markers.Clear();
                GMarkerGoogle marker = new GMarkerGoogle(point, GMarkerGoogleType.red_dot);
                markersOverlay.Markers.Add(marker);
            }
        }
        private void cmb_cond_casa_atencion_ciudadana_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_casa_atencion_ciudadana.Text.ToString();

            if (valorComboBox1 == "Si")
            {
                cmb_cond_casa_atencion_ciudadana_movil.Enabled = true; cmb_cond_casa_atencion_ciudadana_movil.BackColor = Color.Honeydew;
                cmb_cond_casa_atencion_ciudadana_movil.Focus();

            }
            else
            {
                cmb_cond_casa_atencion_ciudadana_movil.Enabled = false; cmb_cond_casa_atencion_ciudadana_movil.BackColor = Color.LightGray;
                txt_latitud_casa_atencion_ciudadana.Enabled = false; txt_latitud_casa_atencion_ciudadana.BackColor = Color.LightGray;
                txt_longitud_casa_atencion_ciudadana.Enabled = false; txt_longitud_casa_atencion_ciudadana.BackColor = Color.LightGray;
                txt_otro_cargo_comision_permanente_especifique.Enabled = false; txt_otro_cargo_comision_permanente_especifique.BackColor = Color.LightGray;
                gMapControl.Enabled = false;
                cmb_cond_casa_atencion_ciudadana_movil.Text = "";
                txt_latitud_casa_atencion_ciudadana.Text = "";
                txt_longitud_casa_atencion_ciudadana.Text = "";
                txt_otro_cargo_comision_permanente_especifique.Text = "";
            }
        }
        private void cmb_cond_integrante_comision_permanente_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_integrante_comision_permanente.Text.ToString();

            if (valorComboBox1 == "Si")
            {
                cmb_cargo_comision_permanente.Enabled = true; cmb_cargo_comision_permanente.BackColor = Color.Honeydew;
                txt_asist_sesiones_comision_permanente_persona_legisladora.Enabled = true; txt_asist_sesiones_comision_permanente_persona_legisladora.BackColor = Color.Honeydew;
                cmb_cargo_comision_permanente.Focus();

            }
            else
            {
                cmb_cargo_comision_permanente.Enabled = false; cmb_cargo_comision_permanente.BackColor = Color.LightGray;
                txt_asist_sesiones_comision_permanente_persona_legisladora.Enabled = false; txt_asist_sesiones_comision_permanente_persona_legisladora.BackColor = Color.LightGray;
                cmb_cargo_comision_permanente.Focus();
                cmb_cargo_comision_permanente.Text = "";
            }
        }
        private void cmb_cargo_comision_permanente_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cargo_comision_permanente.Text.ToString();

            if (valorComboBox1 == "Otro cargo (especifique)")
            {
                txt_otro_cargo_comision_permanente_especifique.Enabled = true; txt_otro_cargo_comision_permanente_especifique.BackColor = Color.Honeydew;
                txt_otro_cargo_comision_permanente_especifique.Focus();

            }
            else
            {
                txt_otro_cargo_comision_permanente_especifique.Enabled = false; txt_otro_cargo_comision_permanente_especifique.BackColor = Color.LightGray;
                txt_otro_cargo_comision_permanente_especifique.Text = "";
            }
           
        }
        private void txt_otro_cargo_comision_permanente_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_otro_cargo_comision_permanente_especifique.Text = txt_otro_cargo_comision_permanente_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_otro_cargo_comision_permanente_especifique.SelectionStart = txt_otro_cargo_comision_permanente_especifique.Text.Length;
        }
        private void txt_otro_cargo_comision_permanente_especifique_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_otro_cargo_comision_permanente_especifique.Text))
            {
                MessageBox.Show("Debe especificar el otro cargo desempeñado por la persona legisladora en la Comisión Permanente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_otro_cargo_comision_permanente_especifique.Focus();
            }
        }
        private void cmb_cond_integrante_jucopo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_integrante_jucopo.Text.ToString();

            if (valorComboBox1 == "Si")
            {
                cmb_cargo_jucopo.Enabled = true; cmb_cargo_jucopo.BackColor = Color.Honeydew;
                
                //txt_latitud_casa_atencion_ciudadana.Enabled = true; txt_latitud_casa_atencion_ciudadana.BackColor = Color.Honeydew;
                //txt_longitud_casa_atencion_ciudadana.Enabled = true; txt_longitud_casa_atencion_ciudadana.BackColor = Color.Honeydew;
                //gMapControl.Enabled = true;
                cmb_cargo_jucopo.Focus();

            }
            else
            {
                cmb_cargo_jucopo.Enabled = false; cmb_cargo_jucopo.BackColor = Color.LightGray;
                txt_otro_cargo_jucopo_especifique.Enabled = false; txt_otro_cargo_jucopo_especifique.BackColor = Color.LightGray;
                cmb_cargo_jucopo.Text = "";
                txt_otro_cargo_jucopo_especifique.Text = "";
            }
        }
        private void cmb_cargo_jucopo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cargo_jucopo.Text.ToString();

            if (valorComboBox1 == "Otro cargo (especifique)")
            {
                txt_otro_cargo_jucopo_especifique.Enabled = true; txt_otro_cargo_jucopo_especifique.BackColor = Color.Honeydew;
                txt_otro_cargo_jucopo_especifique.Focus();

            }
            else
            {
                txt_otro_cargo_jucopo_especifique.Enabled = false; txt_otro_cargo_jucopo_especifique.BackColor = Color.LightGray;
                txt_otro_cargo_jucopo_especifique.Text = "";
            }
        }
        private void txt_otro_cargo_jucopo_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_otro_cargo_jucopo_especifique.Text = txt_otro_cargo_jucopo_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_otro_cargo_jucopo_especifique.SelectionStart = txt_otro_cargo_jucopo_especifique.Text.Length;
        }
        private void txt_otro_cargo_jucopo_especifique_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_otro_cargo_jucopo_especifique.Text))
            {
                MessageBox.Show("Debe especificar el otro cargo desempeñado por la persona legisladora en la Junta de Coordinación Política.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_otro_cargo_jucopo_especifique.Focus();
            }
        }
        private void cmb_cond_integrante_mesa_directiva_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cond_integrante_mesa_directiva.Text.ToString();

            if (valorComboBox1 == "Si")
            {
                cmb_cargo_mesa_directiva_PL.Enabled = true; cmb_cargo_mesa_directiva_PL.BackColor = Color.Honeydew;
                cmb_cargo_mesa_directiva_PL.Focus();

            }
            else
            {
                cmb_cargo_mesa_directiva_PL.Enabled = false; cmb_cargo_mesa_directiva_PL.BackColor = Color.LightGray;
                txt_otro_cargo_mesa_directiva_especifique.Enabled = false; txt_otro_cargo_mesa_directiva_especifique.BackColor = Color.LightGray;
                cmb_cargo_mesa_directiva_PL.Text = "";
                txt_otro_cargo_mesa_directiva_especifique.Text = "";
            }
        }
        private void cmb_cargo_mesa_directiva_PL_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_cargo_mesa_directiva_PL.Text.ToString();

            if (valorComboBox1 == "Otro cargo (especifique)")
            {
                txt_otro_cargo_mesa_directiva_especifique.Enabled = true; txt_otro_cargo_mesa_directiva_especifique.BackColor = Color.Honeydew;
                txt_otro_cargo_mesa_directiva_especifique.Focus();

            }
            else
            {
                txt_otro_cargo_mesa_directiva_especifique.Enabled = false; txt_otro_cargo_mesa_directiva_especifique.BackColor = Color.LightGray;
                txt_otro_cargo_mesa_directiva_especifique.Text = "";
            }
        }
        private void txt_otro_cargo_mesa_directiva_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_otro_cargo_mesa_directiva_especifique.Text = txt_otro_cargo_mesa_directiva_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_otro_cargo_mesa_directiva_especifique.SelectionStart = txt_otro_cargo_mesa_directiva_especifique.Text.Length;
        }
        private void txt_otro_cargo_mesa_directiva_especifique_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_otro_cargo_mesa_directiva_especifique.Text))
            {
                MessageBox.Show("Debe especificar el otro cargo desempeñado por la persona legisladora en la Mesa Directiva.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_otro_cargo_mesa_directiva_especifique.Focus();
            }
        }
        private void cmb_nombre_comision_legislativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_nombre_comision_legislativa.SelectedItem != null)
            {
                string valor_cmb = cmb_nombre_comision_legislativa.Text;
                string cadena = "Data Source = DB_PLE.db;Version=3;";

                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    try
                    {
                        // abrir la conexion
                        conexion.Open();

                        // comando de sql
                        string query = "select ID_comision_legislativa from TR_COMISIONES_LEGISLATIVAS where nombre_comision_legislativa = @valor_cmb";
                        
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                        // Agregar parámetro al comando
                        cmd.Parameters.AddWithValue("@valor_cmb", valor_cmb);
                        txt_ID_comision_legislativa_pc.Text = cmd.ExecuteScalar()?.ToString();
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al llenar el ID: " + ex.Message);
                    }
                    finally
                    {
                        conexion.Close();
                    }

                }
            }
            else { 

            }
                
        }
        private void btnAgregarParticipacionCom_Click(object sender, EventArgs e)
        {
            // se obtienen los valores
            string nom_com = cmb_nombre_comision_legislativa.Text.Trim();
            string id_com = txt_ID_comision_legislativa_pc.Text.Trim();
            string cargo_com = cmb_cargo_comision_legislativa.Text.Trim();


            if (string.IsNullOrWhiteSpace(cmb_nombre_comision_legislativa.Text) || string.IsNullOrWhiteSpace(cmb_cargo_comision_legislativa.Text))
            {
                MessageBox.Show("Revisar datos vacios");
            }
            else
            {

                // Agregar una nueva fila al DataGridView
                dgv_participacion_comisiones.Rows.Add(nom_com,id_com,cargo_com);

                cmb_nombre_comision_legislativa.Text = "";
                txt_ID_comision_legislativa_pc.Text = "";
                cmb_cargo_comision_legislativa.Text = "";
            }
        }
        private void btnEliminarParticipacionCom_Click(object sender, EventArgs e)
        {
            if (dgv_participacion_comisiones.SelectedRows.Count > 0)
            {
                dgv_participacion_comisiones.Rows.RemoveAt(dgv_participacion_comisiones.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
            }
        }
        private void txt_asist_sesiones_plenarias_persona_legisladora_TextChanged(object sender, EventArgs e)
        {
            string valor_txt = txt_asist_sesiones_plenarias_persona_legisladora.Text;

            string sesiones_per_ord = Txt_sesiones_celebradas_po.Text;
            string sesiones_per_ext = Txt_sesiones_celebradas_pe.Text;

            int.TryParse(sesiones_per_ord, out int v1);
            int.TryParse(sesiones_per_ext, out int v2);
            int.TryParse(valor_txt, out int v3);

            if (!string.IsNullOrEmpty(valor_txt) && int.TryParse(valor_txt, out int valor) && valor > 0)
            {
                txt_cant_intervenciones_sesiones_plenarias_persona_legisladora.Enabled = true;
                txt_cant_intervenciones_sesiones_plenarias_persona_legisladora.BackColor = Color.Honeydew;
            }
            else
            {
                txt_cant_intervenciones_sesiones_plenarias_persona_legisladora.Enabled = false;
                txt_cant_intervenciones_sesiones_plenarias_persona_legisladora.BackColor = Color.LightGray;
            }

            int suma = v1 + v2;
            if (v3 > suma)
            {
                MessageBox.Show("Debe ser igual o menor a la suma de las sesiones registradas en periodo ordinario y extraordinarias.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_asist_sesiones_plenarias_persona_legisladora.Clear();
            }
            else
            {

            }
        }
        private void txt_asist_sesiones_comision_permanente_persona_legisladora_TextChanged(object sender, EventArgs e)
        {
            string valor_txt = txt_asist_sesiones_comision_permanente_persona_legisladora.Text;

            if (!string.IsNullOrEmpty(valor_txt) && int.TryParse(valor_txt, out int valor) && valor > 0)
            {
                txt_cant_interv_sesiones_dip_permanente_persona_legisladora.Enabled = true;
                txt_cant_interv_sesiones_dip_permanente_persona_legisladora.BackColor = Color.Honeydew;
            }
            else
            {
                txt_cant_interv_sesiones_dip_permanente_persona_legisladora.Enabled = false;
                txt_cant_interv_sesiones_dip_permanente_persona_legisladora.BackColor = Color.LightGray;
            }
        }
        private void cmb_cond_casa_atencion_ciudadana_movil_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valor_cmb = cmb_cond_casa_atencion_ciudadana_movil.Text;

            if (valor_cmb == "Si")
            {
                txt_latitud_casa_atencion_ciudadana.Enabled = false; txt_latitud_casa_atencion_ciudadana.BackColor = Color.LightGray;
                txt_longitud_casa_atencion_ciudadana.Enabled = false; txt_longitud_casa_atencion_ciudadana.BackColor = Color.LightGray;
                txt_latitud_casa_atencion_ciudadana.Text = ""; txt_longitud_casa_atencion_ciudadana.Text = "";
                gMapControl.Enabled = false;
            }
            else
            {
                txt_latitud_casa_atencion_ciudadana.Enabled = true; txt_latitud_casa_atencion_ciudadana.BackColor = Color.Honeydew;
                txt_longitud_casa_atencion_ciudadana.Enabled = true; txt_longitud_casa_atencion_ciudadana.BackColor = Color.Honeydew;
                gMapControl.Enabled = true;

                
            }
        }
        private void btnGuardarPL_Click(object sender, EventArgs e)
        {
            bool cv =  ValidarCampos_PL2();
            //bool cv = true;

            if (cv == true)
            {
                DialogResult respuesta = MessageBox.Show("¿Está seguro de Guardar los datos?", "Confirmacion",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    // Agregar una nueva fila al DataGridView
                    bool duplicado = IsDuplicateRecord_RegistrosPL(txt_ID_persona_legisladora.Text.ToString());

                    if (duplicado == true)
                    {
                        MessageBox.Show("El ID ya se encuentra registrado. Favor de verificar la información.", "Personas Legisladoras", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        GuardarDatos();

                        ClearControls(tabPagePL);

                        DGV_REGISTROS_PL();
                    }
                }
                else
                {

                }
            }
            else
            {
                
            }
        }
        private bool ValidarCampos_PL()
        {
            // Array de controles a validar
            Control[] controlesAValidar = { txt_nombre_1_persona_legisladora, txt_apellido_1_persona_legisladora, dtp_fecha_nacimiento_persona_legisladora,
            cmb_sexo_persona_legisladora,cmb_estatus_persona_legisladora,cmb_caracter_cargo_persona_legisladora,cmb_escolaridad_persona_legisladora_PL,
            cmb_estatus_escolaridad_persona_legisladora,dgv_lengua_PA,dgv_tipo_discapacidad_PA,cmb_cond_pueblo_ind_persona_legisladora_PL,
            cmb_cond_pob_afromexicana_persona_legisladora_PL,cmb_forma_eleccion_persona_legisladora,cmb_tipo_adscripcion_inicial_persona_legisladora,
            cmb_tipo_adscripcion_final_persona_legisladora,cmb_cond_presentacion_declaracion_situacion_patrimonial,cmb_cond_presentacion_declaracion_intereses,
            cmb_cond_presentacion_declaracion_fiscal,txt_asistencia_legislativa_persona_legisladora,txt_gestion_parlamentaria_persona_legisladora,
            txt_atencion_ciudadana_persona_legisladora,txt_otro_concepto_gasto_persona_legisladora,cmb_cond_casa_atencion_ciudadana,
            cmb_cond_integrante_comision_permanente,cmb_cond_integrante_jucopo,cmb_cond_integrante_mesa_directiva,txt_cant_iniciativas_presentadas_persona_legisladora,
            txt_asist_sesiones_plenarias_persona_legisladora };

            foreach (Control control in controlesAValidar)
            {

                // Verificar si el control está vacío
                if (string.IsNullOrWhiteSpace(control.Text))
                {

                    MessageBox.Show($"Existen campos vacíos.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    control.Focus(); // Enfocar el control vacío
                    return false; // Salir del método después de encontrar el primer campo vacío
                }
            }

            
            int ren_dg;
            ren_dg = dgv_tema_comision_legislativa.Rows.Count;

            if (ren_dg == 0)
            {
                MessageBox.Show($"Existen campos vacíos.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_tema_comision_legislativa.Focus();
                return false;

            }

            return true;
            
        }
        private bool ValidarCampos_PL2()
        {
            // Array de controles a validar
            Control[] controlesAValidar = {
        txt_nombre_1_persona_legisladora, txt_apellido_1_persona_legisladora, dtp_fecha_nacimiento_persona_legisladora,
        cmb_sexo_persona_legisladora, cmb_estatus_persona_legisladora, cmb_caracter_cargo_persona_legisladora,
        cmb_escolaridad_persona_legisladora_PL, cmb_estatus_escolaridad_persona_legisladora,
        dgv_lengua_PA, dgv_tipo_discapacidad_PA, cmb_cond_pueblo_ind_persona_legisladora_PL,
        cmb_cond_pob_afromexicana_persona_legisladora_PL, cmb_forma_eleccion_persona_legisladora,
        cmb_tipo_adscripcion_inicial_persona_legisladora, cmb_tipo_adscripcion_final_persona_legisladora,
        cmb_cond_presentacion_declaracion_situacion_patrimonial, cmb_cond_presentacion_declaracion_intereses,
        cmb_cond_presentacion_declaracion_fiscal, txt_asistencia_legislativa_persona_legisladora,
        txt_gestion_parlamentaria_persona_legisladora, txt_atencion_ciudadana_persona_legisladora,
        txt_otro_concepto_gasto_persona_legisladora, cmb_cond_casa_atencion_ciudadana,
        cmb_cond_integrante_comision_permanente, cmb_cond_integrante_jucopo, cmb_cond_integrante_mesa_directiva,
        txt_cant_iniciativas_presentadas_persona_legisladora, txt_asist_sesiones_plenarias_persona_legisladora
    };

            bool camposValidos = true;

            foreach (Control c in controlesAValidar)
            {
                // Asigna el evento GotFocus fuera del bucle
                c.GotFocus += Control_GotFocus;

                // Verificar si el control está vacío
                if (c is System.Windows.Forms.TextBox && string.IsNullOrWhiteSpace(c.Text))
                {
                    MessageBox.Show($"El campo {c.Name} está vacío.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    c.Focus(); // Enfocar el control vacío
                    camposValidos = false; // Marcar que hay campos inválidos
                    break; // Salir del bucle después de encontrar el primer campo vacío
                }
                else if (c is System.Windows.Forms.ComboBox && ((System.Windows.Forms.ComboBox)c).SelectedIndex == -1)
                {
                    MessageBox.Show($"Debe seleccionar una opción en {c.Name}.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    c.Focus(); // Enfocar el control vacío
                    camposValidos = false; // Marcar que hay campos inválidos
                    break; // Salir del bucle después de encontrar el primer campo vacío
                }
                // Agregar más validaciones según sea necesario para otros tipos de controles
            }

            return camposValidos;
        }

        private async void Control_GotFocus(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control != null)
            {
                Color originalColor = control.BackColor;
                control.BackColor = Color.Yellow; // Color de resaltado
                await Task.Delay(1500); // Espera 500 milisegundos
                control.BackColor = originalColor; // Restablece el color original
            }
        }


        private bool IsDuplicateRecord_RegistrosPL(string variable_cmb)
        {
            foreach (DataGridViewRow row in dgv_registros_pl.Rows)
            {
                if (row.IsNewRow) continue; // Skip the new row placeholder

                string existingId = row.Cells["txt_ID_persona_legisladora"].Value.ToString();

                if (existingId == variable_cmb)
                {
                    return true;
                }
            }
            return false;
        }
        private void DGV_REGISTROS_PL()
        {
            string cadena = "Data Source=DB_PLE.db;Version=3;";
            string id_legis = txt_id_legislatura.Text;

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // Abrir la conexión
                    conexion.Open();

                    // Comando de SQL
                    string query = "SELECT DISTINCT txt_ID_persona_legisladora, txt_nombre_1_persona_legisladora, " +
                                   "dtp_fecha_nacimiento_persona_legisladora, cmb_estatus_persona_legisladora, cmb_caracter_cargo_persona_legisladora " +
                                   "FROM TR_PERSONAS_LEGISLADORAS " +
                                   "WHERE id_legislatura = @id_legis";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        // Asignar el parámetro
                        cmd.Parameters.AddWithValue("@id_legis", id_legis);

                        // Utilizar un DataAdapter para obtener los datos
                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            dgv_registros_pl.DataSource = dataTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al llenar DGV personas legisladoras: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }
            }
        }
        private void GuardarDatos()
        {
            var data = new Dictionary<string, string>();

            // Recorrer todos los controles y guardar datos no vacíos en el diccionario
            RecorrerControles(tabPagePL, data);

            if (data.Count == 0)
            {
                MessageBox.Show("No hay datos para guardar.");
                return;
            }

            string cadena = "Data Source=DB_PLE.db;Version=3;";

            using (var connection = new SQLiteConnection(cadena))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Construir dinámicamente la consulta SQL
                        var columns = string.Join(", ", data.Keys);
                        var parameters = string.Join(", ", data.Keys.Select(key => "@" + key));
                        string query = $"INSERT INTO TR_PERSONAS_LEGISLADORAS ({columns}, fecha_actualizacion,id_legislatura) " +
                            $"VALUES " +
                            $"({parameters}, @fecha_actualizacion, @id_legislatura)";

                        using (var command = new SQLiteCommand(query, connection, transaction))
                        {
                            // Agregar los parámetros al comando
                            foreach (var kvp in data)
                            {
                                command.Parameters.AddWithValue($"@{kvp.Key}", kvp.Value);
                            }

                            // Registrar la consulta y los parámetros para depuración
                            Console.WriteLine("Query: " + query);
                            foreach (SQLiteParameter param in command.Parameters)
                            {
                                Console.WriteLine($"Parameter: {param.ParameterName} = {param.Value}");
                            }

                            command.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                            command.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text.ToString());

                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Datos guardados correctamente.");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Error al guardar los datos: {ex.Message}");
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
        }
        private void RecorrerControles(Control control, Dictionary<string, string> data)
        {
            // List of DataGridView names to exclude
            var excludedDataGridViews = new List<string> { "dgv_registros_pl" };

            foreach (Control c in control.Controls)
            {
                if (c is System.Windows.Forms.TextBox textBox && !string.IsNullOrWhiteSpace(textBox.Text))
                {
                    data.Add(textBox.Name, textBox.Text);
                }
                else if (c is System.Windows.Forms.ComboBox comboBox && !string.IsNullOrWhiteSpace(comboBox.Text))
                {
                    data.Add(comboBox.Name, comboBox.Text);
                }
                else if (c is System.Windows.Forms.DateTimePicker dateTimePicker)
                {
                    data.Add(dateTimePicker.Name, dateTimePicker.Text);
                }
                else if (c is DataGridView dataGridView && !excludedDataGridViews.Contains(dataGridView.Name))
                {
                    // Variable para almacenar las filas concatenadas
                    List<string> rowValuesList = new List<string>();

                    for (int i = 0; i < dataGridView.Rows.Count; i++)
                    {
                        string rowValues = string.Empty;
                        for (int j = 0; j < dataGridView.Columns.Count; j++)
                        {
                            if (dataGridView.Rows[i].Cells[j].Value != null)
                            {
                                rowValues = dataGridView.Rows[i].Cells[j].Value.ToString() ; // Agrega un separador, como un espacio

                                if (!string.IsNullOrEmpty(rowValues))
                                {
                                    // se guardan los datagridviews que contienen (i,j) columnas*******
                                    if (dataGridView.Name == "dgv_nivel_escolaridad_PL")
                                    {
                                        string idPL2 = txt_ID_persona_legisladora.Text;
                                        string cadena2 = "Data Source=DB_PLE.db;Version=3;";

                                        using (SQLiteConnection conn = new SQLiteConnection(cadena2))
                                        {
                                            conn.Open();

                                            if (j == 0)
                                            {
                                                string query = "INSERT INTO TR_PERSONAS_LEGISLADORAS (id_legislatura, " +
                                                    "txt_ID_persona_legisladora," +
                                                    "dgv_carrera_licenciatura_persona_legisladora_PL, " +
                                                    "fecha_actualizacion) " +
                                                 "VALUES " +
                                                 "(@id_legislatura," +
                                                 "@txt_ID_persona_legisladora," +
                                                 "@RowValue," +
                                                 "@fecha_actualizacion)";

                                                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                                                {
                                                    cmd.Parameters.AddWithValue("@RowValue", rowValues);
                                                    cmd.Parameters.AddWithValue("@txt_ID_persona_legisladora", idPL2);
                                                    cmd.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                                    cmd.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text.ToString());

                                                    cmd.ExecuteNonQuery();
                                                }
                                            }
                                            if (j == 1)
                                            {
                                                string query = "INSERT INTO TR_PERSONAS_LEGISLADORAS (id_legislatura, txt_ID_persona_legisladora, dgv_carrera_maestria_persona_legisladora_PL," +
                                                    "fecha_actualizacion) " +
                                                "VALUES " +
                                                "(@id_legislatura,@txt_ID_persona_legisladora, @RowValue, @fecha_actualizacion)";
                                                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                                                {
                                                    cmd.Parameters.AddWithValue("@RowValue", rowValues);
                                                    cmd.Parameters.AddWithValue("@txt_ID_persona_legisladora", idPL2);
                                                    cmd.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                                    cmd.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text.ToString());

                                                    cmd.ExecuteNonQuery();
                                                }
                                            }
                                            if (j == 2)
                                            {
                                                string query = "INSERT INTO TR_PERSONAS_LEGISLADORAS (id_legislatura,txt_ID_persona_legisladora, dgv_carrera_doctorado_persona_legisladora_PL," +
                                                    "fecha_actualizacion) " +
                                                "VALUES " +
                                                "(@id_legislatura,@txt_ID_persona_legisladora, @RowValue, @fecha_actualizacion)";
                                                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                                                {
                                                    cmd.Parameters.AddWithValue("@RowValue", rowValues);
                                                    cmd.Parameters.AddWithValue("@txt_ID_persona_legisladora", idPL2);
                                                    cmd.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                                    cmd.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text.ToString());

                                                    cmd.ExecuteNonQuery();
                                                }
                                            }
                                        }
                                    }

                                    if (dataGridView.Name == "dgv_participacion_comisiones")
                                    {
                                        string idPL2 = txt_ID_persona_legisladora.Text;
                                        string cadena2 = "Data Source=DB_PLE.db;Version=3;";

                                        using (SQLiteConnection conn = new SQLiteConnection(cadena2))
                                        {
                                            conn.Open();

                                            if (j == 0)
                                            {
                                                string query = "INSERT INTO TR_PERSONAS_LEGISLADORAS (id_legislatura,txt_ID_persona_legisladora, dgv_nombre_comision_legislativa," +
                                                    "fecha_actualizacion) " +
                                                 "VALUES " +
                                                 "(@id_legislatura,@txt_ID_persona_legisladora, @RowValue, @fecha_actualizacion)";

                                                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                                                {
                                                    cmd.Parameters.AddWithValue("@RowValue", rowValues);
                                                    cmd.Parameters.AddWithValue("@txt_ID_persona_legisladora", idPL2);
                                                    cmd.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                                    cmd.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text.ToString());

                                                    cmd.ExecuteNonQuery();
                                                }
                                            }
                                            if (j == 1)
                                            {
                                                string query = "INSERT INTO TR_PERSONAS_LEGISLADORAS (id_legislatura,txt_ID_persona_legisladora, dgv_ID_comision_legislativa_pc," +
                                                    "fecha_actualizacion) " +
                                                "VALUES " +
                                                "(@id_legislatura,@txt_ID_persona_legisladora, @RowValue, @fecha_actualizacion)";
                                                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                                                {
                                                    cmd.Parameters.AddWithValue("@RowValue", rowValues);
                                                    cmd.Parameters.AddWithValue("@txt_ID_persona_legisladora", idPL2);
                                                    cmd.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                                    cmd.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text.ToString());

                                                    cmd.ExecuteNonQuery();
                                                }
                                            }
                                            if (j == 2)
                                            {
                                                string query = "INSERT INTO TR_PERSONAS_LEGISLADORAS (id_legislatura,txt_ID_persona_legisladora, dgv_cargo_comision_legislativa, fecha_actualizacion) " +
                                                "VALUES " +
                                                "(@id_legislatura,@txt_ID_persona_legisladora, @RowValue, @fecha_actualizacion)";
                                                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                                                {
                                                    cmd.Parameters.AddWithValue("@RowValue", rowValues);
                                                    cmd.Parameters.AddWithValue("@txt_ID_persona_legisladora", idPL2);
                                                    cmd.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                                    cmd.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text.ToString());

                                                    cmd.ExecuteNonQuery();
                                                }
                                            }
                                        }
                                    }
                                }
                                
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(rowValues))
                        {
                            rowValues = rowValues.Trim(); // Elimina el espacio extra al final
                            rowValuesList.Add(rowValues);
                        }
                    }

                    // Se guardan los datagridview que solo contienen una columna******
                    foreach (var rowValue in rowValuesList)
                    {
                        // Aquí debes agregar tu lógica para guardar en la base de datos
                        string idPL = txt_ID_persona_legisladora.Text;
       
                        string cadena = "Data Source=DB_PLE.db;Version=3;";
                        using (SQLiteConnection conn = new SQLiteConnection(cadena))
                        {
                            conn.Open();
                            if (dataGridView.Name == "dgv_lengua_PL")
                            {
                                string query = "INSERT INTO TR_PERSONAS_LEGISLADORAS (id_legislatura, txt_ID_persona_legisladora, dgv_cond_lengua_ind_persona_legisladora_PL," +
                                    "fecha_actualizacion) " +
                                    "VALUES " +
                                    "(@id_legislatura,@txt_ID_persona_legisladora, @RowValue, @fecha_actualizacion)";

                                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@RowValue", rowValue);
                                    cmd.Parameters.AddWithValue("@txt_ID_persona_legisladora", idPL);
                                    cmd.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                    cmd.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text.ToString());

                                    cmd.ExecuteNonQuery();
                                }
                            }
                            if (dataGridView.Name == "dgv_tipo_discapacidad_PL")
                            {
                                string query = "INSERT INTO TR_PERSONAS_LEGISLADORAS (txt_ID_persona_legisladora, dgv_tipo_discapacidad_persona_legisladora," +
                                    "fecha_actualizacion,id_legislatura) " +
                                    "VALUES " +
                                    "(@txt_ID_persona_legisladora, @RowValue, @fecha_actualizacion, @id_legislatura)";

                                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@RowValue", rowValue);
                                    cmd.Parameters.AddWithValue("@txt_ID_persona_legisladora", idPL);
                                    cmd.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                    cmd.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text.ToString());

                                    cmd.ExecuteNonQuery();
                                }
                                
                            }
                            if (dataGridView.Name == "dgv_partido_coalicion")
                            {
                                string query = "INSERT INTO TR_PERSONAS_LEGISLADORAS (txt_ID_persona_legisladora, dgv_partido_politico_candidatura_coalicion," +
                                    "fecha_actualizacion, id_legislatura) " +
                                    "VALUES " +
                                    "(@txt_ID_persona_legisladora, @RowValue, @fecha_actualizacion, @id_legislatura)";

                                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@RowValue", rowValue);
                                    cmd.Parameters.AddWithValue("@txt_ID_persona_legisladora", idPL);
                                    cmd.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                    cmd.Parameters.AddWithValue("@id_legislatura", txt_id_legislatura.Text.ToString());

                                    cmd.ExecuteNonQuery();
                                }

                            }
                            
                        }
                    }

                    
                }

                if (c.Controls.Count > 0)
                {
                    RecorrerControles(c, data);
                }
            }

        }
        // Método para limpiar los controles de un TabPage
        private void ClearControls(Control control)
        {
            // Lista de nombres de DataGridView a excluir
            var excludedDataGridViews = new List<string> { "dgv_registros_pl" };

            foreach (Control c in control.Controls)
            {
                if (c is System.Windows.Forms.TextBox)
                {
                    ((System.Windows.Forms.TextBox)c).Clear();
                }
                else if (c is System.Windows.Forms.ComboBox)
                {
                    ((System.Windows.Forms.ComboBox)c).SelectedIndex = -1;
                }
                else if (c is DataGridView)
                {
                    if (!excludedDataGridViews.Contains(c.Name))
                    {
                        ((DataGridView)c).Rows.Clear();
                    }
                }
                else if (c.HasChildren)
                {
                    // Llamar recursivamente si el control tiene hijos
                    ClearControls(c);
                }
            }
        }
        private void btnActualizarDGV_PL_Click(object sender, EventArgs e)
        {
            DGV_REGISTROS_PL();
        }

        //-------------------------------------------------- PERSONAL DE APOYO ----------------------------------------------------

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
                    string query = "select descripcion from TC_SI_NO";
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
                    string query = "select descripcion from TC_SI_NO";
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
                    string query = "select descripcion from TC_SI_NO";
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
                    string query = "select descripcion from TC_SI_NO";
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

        
        //-------------------------------------------------- INICIATIVAS ----------------------------------------------------

        private void cmb_Cond_presentacion_iniciativa_legislatura_actual()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_SI_NO";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cond_presentacion_iniciativa_legislatura_actual.DataSource = dataTable;
                    cmb_cond_presentacion_iniciativa_legislatura_actual.DisplayMember = "descripcion";

                    cmb_cond_presentacion_iniciativa_legislatura_actual.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_presentacion_iniciativa_legislatura_actual.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_presentacion_iniciativa_legislatura_actual.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_presentacion_iniciativa_legislatura_actual.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Cond_presentacion_iniciativa_periodo()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_SI_NO";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cond_presentacion_iniciativa_periodo.DataSource = dataTable;
                    cmb_cond_presentacion_iniciativa_periodo.DisplayMember = "descripcion";

                    cmb_cond_presentacion_iniciativa_periodo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_presentacion_iniciativa_periodo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_presentacion_iniciativa_periodo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_presentacion_iniciativa_periodo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Numero_legislatura_presentacion_iniciativa()
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

                    cmb_numero_legislatura_presentacion_iniciativa.DataSource = dataTable;
                    cmb_numero_legislatura_presentacion_iniciativa.DisplayMember = "descripcion";

                    cmb_numero_legislatura_presentacion_iniciativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_numero_legislatura_presentacion_iniciativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_numero_legislatura_presentacion_iniciativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_numero_legislatura_presentacion_iniciativa.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Cond_actualizacion_estatus_iniciativa_periodo()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_SI_NO";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cond_actualizacion_estatus_iniciativa_periodo.DataSource = dataTable;
                    cmb_cond_actualizacion_estatus_iniciativa_periodo.DisplayMember = "descripcion";

                    cmb_cond_actualizacion_estatus_iniciativa_periodo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_actualizacion_estatus_iniciativa_periodo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_actualizacion_estatus_iniciativa_periodo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_actualizacion_estatus_iniciativa_periodo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Cond_modificacion_informacion_ingreso_periodo()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_SI_NO";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_cond_modificacion_informacion_ingreso_periodo.DataSource = dataTable;
                    cmb_cond_modificacion_informacion_ingreso_periodo.DisplayMember = "descripcion";

                    cmb_cond_modificacion_informacion_ingreso_periodo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_cond_modificacion_informacion_ingreso_periodo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_cond_modificacion_informacion_ingreso_periodo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_cond_modificacion_informacion_ingreso_periodo.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Estatus_iniciativa()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_ESTATUS_INI";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_estatus_iniciativa.DataSource = dataTable;
                    cmb_estatus_iniciativa.DisplayMember = "descripcion";

                    cmb_estatus_iniciativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_estatus_iniciativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_estatus_iniciativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_estatus_iniciativa.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Etapa_procesal_iniciativa()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_ETAPA_PROC";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_etapa_procesal_iniciativa.DataSource = dataTable;
                    cmb_etapa_procesal_iniciativa.DisplayMember = "descripcion";

                    cmb_etapa_procesal_iniciativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_etapa_procesal_iniciativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_etapa_procesal_iniciativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_etapa_procesal_iniciativa.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Tipo_iniciativa()
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

                    cmb_tipo_iniciativa.DataSource = dataTable;
                    cmb_tipo_iniciativa.DisplayMember = "descripcion";

                    cmb_tipo_iniciativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_tipo_iniciativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_tipo_iniciativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_tipo_iniciativa.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void cmb_Tipo_promovente_iniciativa()
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

                    cmb_tipo_promovente_iniciativa.DataSource = dataTable;
                    cmb_tipo_promovente_iniciativa.DisplayMember = "descripcion";

                    cmb_tipo_promovente_iniciativa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_tipo_promovente_iniciativa.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_tipo_promovente_iniciativa.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_tipo_promovente_iniciativa.SelectedIndex = -1; // Aquí se establece como vacío
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
        private void tipo_Organo_constitucional_autonomo()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_ORG_CONST_AUT_PROMOVENTE";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_tipo_organo_constitucional_autonomo.DataSource = dataTable;
                    cmb_tipo_organo_constitucional_autonomo.DisplayMember = "descripcion";

                    cmb_tipo_organo_constitucional_autonomo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_tipo_organo_constitucional_autonomo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_tipo_organo_constitucional_autonomo.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_tipo_organo_constitucional_autonomo.SelectedIndex = -1; // Aquí se establece como vacío
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

        private void txt_ID_persona_legisladora_propietaria_TextChanged(object sender, EventArgs e)
        {

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
