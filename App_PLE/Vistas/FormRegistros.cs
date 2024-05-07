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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace App_PLE.Vistas
{
    public partial class FormRegistros : Form
    {
        public FormRegistros()
        {
            InitializeComponent();
        }
        /*
        -------------------------------------------------- CARGA INICIAL DE FORMULARIO ----------------------------------------------------
         */
        private void FormRegistros_Load(object sender, EventArgs e)
        {
            // datos generales
            cmb_NumeroLegislatura();
            cmb_Entidad();
            cmb_ejercicio_const();
            cmb_PeriodoReportado_PO();
            cmb_PeriodoReportado_PE();

            // comisiones legislativas
            cmb_Tipo_CL();
            cmb_Tema_CL();
            cmb_cond_transmision_reuniones_celebradas_CL();
            cmb_cond_celebracion_reuniones_CL();

            //  PERSONAS LEGISLADORAS
            cmb_Sexo_Persona_Legisladora();
            cmb_Estatus_persona_legisladora();
            cmb_Tipo_licencia_persona_legisladora();
            cmb_Causa_fallecimiento_persona_legisladora();
            cmb_Caracter_cargo_persona_legisladora();
            cmb_Escolaridad_persona_legisladora();
            cmb_Estatus_escolaridad_persona_legisladora();
            cmb_Carrera_licenciatura_persona_legisladora();
            cmb_Carrera_maestria_persona_legisladora();
            cmb_Carrera_doctorado_persona_legisladora();
            cmb_Cond_lengua_ind_persona_legisladora();
            cmb_Cond_discapacidad_persona_legisladora();
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
            cmb_Cond_integrante_comision_permanente();
            cmb_Cargo_comision_permanente();
            cmb_Cond_integrante_jucopo();
            cmb_Cond_integrante_mesa_directiva();
            cmb_Cargo_mesa_directiva_PL();
            cmb_Cargo_jucopo();

            //  PERSONAL DE APOYO
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

            //  INICIATIVAS
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



            // campos desahabilitados incialmente
            txtID.Enabled = false;  dgvPE.Enabled = false; cmb_periodo_extraordinario_reportado.Enabled = false;
            dtp_fecha_inicio_pe.Enabled = false; dtp_fecha_termino_pe.Enabled = false; Txt_sesiones_celebradas_pe.Enabled = false;
            btnAgregarPE.Enabled = false; BtnEliminarPE.Enabled = false;
            Txt_otro_tipo_comision_legislativa_especifique.Enabled = false; Txt_ID_comision_legislativa.Enabled = false;
            txt_otro_tema_comision_legislativa_especifique.Enabled = false;

            // configuracion de fechas
            dtp_termino_funciones_legislatura.Value = DateTime.Today;
            Dtp_fecha_termino_informacion_reportada.Value = DateTime.Today;
            dtp_fecha_termino_po.Value = DateTime.Today;
            dtp_fecha_termino_pe.Value = DateTime.Today;

            // Campos vacios inicialmente
            txtID.Text = string.Empty;
            Txt_ID_comision_legislativa.Text =  string.Empty;
            cmb_tema_comision_legislativa.Text = "";
            cmb_tipo_comision_legislativa.Text = "";

           
        }
        /*
        -------------------------------------------------- LISTAS DESPLEGABLES ----------------------------------------------------
         */
        // DATOS GENERALES
        private void cmb_NumeroLegislatura()
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

                    cmb_numero_legislatura.DataSource = dataTable;
                    cmb_numero_legislatura.DisplayMember = "descripcion";

                    cmb_numero_legislatura.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_numero_legislatura.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_numero_legislatura.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_numero_legislatura.Text = "";
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
        private void cmb_ejercicio_const()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_EJERCICIO_CONST";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_ejercicio_constitucional_informacion_reportada.DataSource = dataTable;
                    cmb_ejercicio_constitucional_informacion_reportada.DisplayMember = "descripcion";

                    cmb_ejercicio_constitucional_informacion_reportada.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_ejercicio_constitucional_informacion_reportada.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_ejercicio_constitucional_informacion_reportada.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_ejercicio_constitucional_informacion_reportada.Text = "";
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
        private void cmb_PeriodoReportado_PO()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_PERIODO_REPORTADO";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_periodo_reportado_po.DataSource = dataTable;
                    cmb_periodo_reportado_po.DisplayMember = "descripcion";

                    cmb_periodo_reportado_po.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_periodo_reportado_po.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_periodo_reportado_po.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_periodo_reportado_po.Text = "";
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
        private void cmb_PeriodoReportado_PE()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    // comando de sql
                    string query = "select descripcion from TC_PERIODO_EXT";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                    // Utilizar un DataReader para obtener los datos
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmb_periodo_extraordinario_reportado.DataSource = dataTable;
                    cmb_periodo_extraordinario_reportado.DisplayMember = "descripcion";

                    cmb_periodo_extraordinario_reportado.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_periodo_extraordinario_reportado.AutoCompleteSource = AutoCompleteSource.ListItems;

                    cmb_periodo_extraordinario_reportado.DropDownStyle = ComboBoxStyle.DropDown;
                    cmb_periodo_extraordinario_reportado.Text = "";
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

        // COMISIONES LEGISLATIVAS
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
                    MessageBox.Show("Error al llenar el ComboBox: " + ex.Message);
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
                    string query = "select descripcion from TC_SI_NO";
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
                    string query = "select descripcion from TC_SI_NO";
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

        // PERSONAS LEGISLADORAS
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
                    MessageBox.Show("Error al llenar el ComboBox: " + ex.Message);
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
                    MessageBox.Show("Error al llenar el ComboBox: " + ex.Message);
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
                    string query = "select descripcion from TC_SI_NO";
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
                    string query = "select descripcion from TC_SI_NO";
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
                    string query = "select descripcion from TC_SI_NO";
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
                    string query = "select descripcion from TC_SI_NO";
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
                    string query = "select descripcion from TC_SI_NO";
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
                    string query = "select descripcion from TC_SI_NO";
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
                    string query = "select descripcion from TC_SI_NO";
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
                    string query = "select descripcion from TC_SI_NO";
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
                    string query = "select descripcion from TC_SI_NO";
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
                    string query = "select descripcion from TC_SI_NO";
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
                    string query = "select descripcion from TC_SI_NO";
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
                    string query = "select descripcion from TC_SI_NO";
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

        // PERSONAL DE PAOYO
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
                    MessageBox.Show("Error al llenar el ComboBox: " + ex.Message);
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

        // INICIATIVAS

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
        /*
        -------------------------------------------------- CARGAR DATOS ----------------------------------------------------
         */

        public void CargarDatos(string id_registro)
        {
            // Usa los datos recibidos para cargar los controles en el formulario nuevo
            txtID.Text = id_registro;
            
        }

        /*
        -------------------------------------------------- BOTONES ----------------------------------------------------
         */
        // DATOS GENERALES
        private void btnAgregarPeriodoOrdinario_Click_1(object sender, EventArgs e)
        {
            // se obtienen los valores
            string periodo_reportado_po = cmb_periodo_reportado_po.Text.Trim();
            string fecha_inicio_po = dtp_fecha_inicio_po.Text.Trim();
            string fecha_termino_po = dtp_fecha_termino_po.Text.Trim();
            string sesiones_celebradas_po = Txt_sesiones_celebradas_po.Text.Trim();

            if (string.IsNullOrWhiteSpace(Txt_sesiones_celebradas_po.Text) ||
                string.IsNullOrWhiteSpace(cmb_periodo_reportado_po.Text))
            {
                MessageBox.Show("Revisar datos vacios");
            }
            else
            {
                // Agregar una nueva fila al DataGridView
                dgvPO.Rows.Add(periodo_reportado_po, fecha_inicio_po, fecha_termino_po, sesiones_celebradas_po);

                cmb_periodo_reportado_po.Text = ""; Txt_sesiones_celebradas_po.Clear();
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
                // Agregar una nueva fila al DataGridView
                dgvPE.Rows.Add(periodo_reportado_pe, fecha_inicio_pe, fecha_termino_pe, sesiones_celebradas_pe);

                cmb_periodo_extraordinario_reportado.Text = ""; Txt_sesiones_celebradas_pe.Clear();
            }
        }

        private void BtnGuardarDG_Click_1(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de Guardar los datos?", "Confirmacion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                string cadena = "Data Source = DB_PLE.db;Version=3;";

                using (SQLiteConnection connection = new SQLiteConnection(cadena))
                {
                    connection.Open();

                    // Recorremos las filas del DataGridView
                    foreach (DataGridViewRow row in dgvPO.Rows)
                    {
                        // Ignoramos la fila vacía al final
                        if (!row.IsNewRow)
                        {
                            // Insertamos los datos en la base de datos
                            string query = "INSERT INTO TR_DATOS_GENERALES (entidad_federativa, periodo_reportado, fecha_inicio_p," +
                                "numero_legislatura, nombre_legislatura, inicio_funciones_legislatura," +
                                "termino_funciones_legislatura, distritos_uninominales, diputaciones_plurinominales," +
                                "ejercicio_constitucional_informacion_reportada, fecha_inicio_informacion_reportada," +
                                "fecha_termino_informacion_reportada, fecha_termino_p,sesiones_celebradas_p,id_datos_generales," +
                                "fecha_actualizacion) " +
                                "VALUES" +
                                " (@entidad_federativa, @periodo_reportado, @fecha_inicio_p," +
                                "@numero_legislatura, @nombre_legislatura, @inicio_funciones_legislatura," +
                                "@termino_funciones_legislatura, @distritos_uninominales, @diputaciones_plurinominales," +
                                "@ejercicio_constitucional_informacion_reportada, @fecha_inicio_informacion_reportada, " +
                                "@fecha_termino_informacion_reportada, @fecha_termino_p, @sesiones_celebradas_p,@id_datos_generales," +
                                "@fecha_actualizacion)";

                            using (SQLiteCommand command = new SQLiteCommand(query, connection))
                            {
                                // Variables restantes
                                command.Parameters.AddWithValue("@entidad_federativa", cmb_entidad_federativa.Text);
                                command.Parameters.AddWithValue("@numero_legislatura", cmb_numero_legislatura.Text);
                                command.Parameters.AddWithValue("@nombre_legislatura", txt_nombre_legislatura.Text);
                                command.Parameters.AddWithValue("@inicio_funciones_legislatura", Dtp_inicio_funciones_legislatura.Text);
                                command.Parameters.AddWithValue("@termino_funciones_legislatura", dtp_termino_funciones_legislatura.Text);
                                command.Parameters.AddWithValue("@distritos_uninominales", Txt_distritos_uninominales.Text);
                                command.Parameters.AddWithValue("@diputaciones_plurinominales", Txt_diputaciones_plurinominales.Text);
                                command.Parameters.AddWithValue("@ejercicio_constitucional_informacion_reportada", cmb_ejercicio_constitucional_informacion_reportada.Text);
                                command.Parameters.AddWithValue("@fecha_inicio_informacion_reportada", Dtp_fecha_inicio_informacion_reportada.Text);
                                command.Parameters.AddWithValue("@fecha_termino_informacion_reportada", Dtp_fecha_termino_informacion_reportada.Text);
                                command.Parameters.AddWithValue("@id_datos_generales", txtID.Text);
                                command.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now);
                                
                                // Variables del dgv
                                command.Parameters.AddWithValue("@periodo_reportado", row.Cells["periodo_reportado_po"].Value);
                                command.Parameters.AddWithValue("@fecha_inicio_p", row.Cells["fecha_inicio_po"].Value);
                                command.Parameters.AddWithValue("@fecha_termino_p", row.Cells["fecha_termino_po"].Value);
                                command.Parameters.AddWithValue("@sesiones_celebradas_p", row.Cells["sesiones_celebradas_po"].Value);

                                command.ExecuteNonQuery();
                            }
                        }

                    }

                    if (dgvPE.RowCount == 0)
                    {
                        // El DataGridView está vacío
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
                                string query = "INSERT INTO TR_DATOS_GENERALES (entidad_federativa, periodo_extraordinario_reportado," +
                                "numero_legislatura, nombre_legislatura, inicio_funciones_legislatura," +
                                "termino_funciones_legislatura, distritos_uninominales, diputaciones_plurinominales," +
                                "ejercicio_constitucional_informacion_reportada, fecha_inicio_informacion_reportada," +
                                "fecha_termino_informacion_reportada,id_datos_generales,fecha_inicio_pe,fecha_termino_pe," +
                                "sesiones_celebradas_pe,fecha_actualizacion) " +
                                "VALUES" +
                                " (@entidad_federativa, @periodo_extraordinario_reportado, " +
                                "@numero_legislatura, @nombre_legislatura, @inicio_funciones_legislatura," +
                                "@termino_funciones_legislatura, @distritos_uninominales, @diputaciones_plurinominales," +
                                "@ejercicio_constitucional_informacion_reportada, @fecha_inicio_informacion_reportada, " +
                                "@fecha_termino_informacion_reportada,@id_datos_generales,@fecha_inicio_pe,@fecha_termino_pe," +
                                "@sesiones_celebradas_pe,@fecha_actualizacion)";

                                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                                {
                                    // Variables restantes
                                    command.Parameters.AddWithValue("@entidad_federativa", cmb_entidad_federativa.Text);
                                    command.Parameters.AddWithValue("@numero_legislatura", cmb_numero_legislatura.Text);
                                    command.Parameters.AddWithValue("@nombre_legislatura", txt_nombre_legislatura.Text);
                                    command.Parameters.AddWithValue("@inicio_funciones_legislatura", Dtp_inicio_funciones_legislatura.Text);
                                    command.Parameters.AddWithValue("@termino_funciones_legislatura", dtp_termino_funciones_legislatura.Text);
                                    command.Parameters.AddWithValue("@distritos_uninominales", Txt_distritos_uninominales.Text);
                                    command.Parameters.AddWithValue("@diputaciones_plurinominales", Txt_diputaciones_plurinominales.Text);
                                    command.Parameters.AddWithValue("@ejercicio_constitucional_informacion_reportada", cmb_ejercicio_constitucional_informacion_reportada.Text);
                                    command.Parameters.AddWithValue("@fecha_inicio_informacion_reportada", Dtp_fecha_inicio_informacion_reportada.Text);
                                    command.Parameters.AddWithValue("@fecha_termino_informacion_reportada", Dtp_fecha_termino_informacion_reportada.Text);
                                    command.Parameters.AddWithValue("@id_datos_generales", txtID.Text);
                                    command.Parameters.AddWithValue("@fecha_actualizacion", DateTime.Now);

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

                // Se reinicion los botones
                cmb_entidad_federativa.Enabled = false; cmb_numero_legislatura.Enabled = false;
                txt_nombre_legislatura.Clear(); Txt_distritos_uninominales.Text = ""; Txt_diputaciones_plurinominales.Text = "";
                cmb_ejercicio_constitucional_informacion_reportada.Text = "";
                Txt_sesiones_celebradas_pe.Text = "";
                dgvPO.Rows.Clear(); dgvPE.Rows.Clear();

                MessageBox.Show("Datos guardados correctamente");

                this.Close();
            }
            else
            {

            }
        }

        private void BtnEliminarPO_Click_1(object sender, EventArgs e)
        {
            if (dgvPO.SelectedRows.Count > 0)
            {
                dgvPO.Rows.RemoveAt(dgvPO.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Seleccionar registro a eliminar");
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
        
        // COMISIONES LEGISLATIVAS
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

        private void BtnAgregarCL_Click(object sender, EventArgs e)
        {
            // se obtienen los valores
            string tema_comision_legislativa = cmb_tema_comision_legislativa.Text.Trim();
            

            if (string.IsNullOrWhiteSpace(cmb_tema_comision_legislativa.Text) ||
                string.IsNullOrWhiteSpace(cmb_tema_comision_legislativa.Text))
            {
                MessageBox.Show("Revisar datos vacios");
            }
            else
            {
                // Agregar una nueva fila al DataGridView
                dgv_tema_comision_legislativa.Rows.Add(tema_comision_legislativa);

                cmb_tema_comision_legislativa.Text = ""; 
            }
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

        /*
        -------------------------------------------------- CREACION ID ----------------------------------------------------
         */
        // ID LEGISLATURA
        private void cmb_entidad_federativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_entidad_federativa.Text.ToString();

            // Realizar la consulta para obtener el valor de c2
            string c2Value = "";
            using (SQLiteConnection con = new SQLiteConnection(cadena))
            {
                con.Open();
                string query = "SELECT cve_ent FROM TC_AGEEM WHERE nom_ent = @valorComboBox1";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@valorComboBox1", valorComboBox1);
                c2Value = cmd.ExecuteScalar()?.ToString();
                con.Close();
            }

            // se encuentra la abreviatura periodo reportado
            string valorComboBox3 = cmb_periodo_reportado_po.Text.ToString();

            string c3Value = "";
            using (SQLiteConnection con = new SQLiteConnection(cadena))
            {
                con.Open();
                string query = "SELECT abr FROM TC_PERIODO_REPORTADO WHERE descripcion = @valorComboBox3";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@valorComboBox3", valorComboBox3);
                c3Value = cmd.ExecuteScalar()?.ToString();
                con.Close();
            }

            // Concatenar ID
            string valorComboBox2 = cmb_numero_legislatura.Text.ToString();
            string resultadoConcatenado = c2Value + "_" + valorComboBox2 + "_" + c3Value;

            // Mostrar el resultado en TextBox1
            txtID.Text = resultadoConcatenado;
        }

        private void cmb_numero_legislatura_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_entidad_federativa.Text.ToString();

            // Realizar la consulta para obtener el valor de c2
            string c2Value = "";
            using (SQLiteConnection con = new SQLiteConnection(cadena))
            {
                con.Open();
                string query = "SELECT cve_ent FROM TC_AGEEM WHERE nom_ent = @valorComboBox1";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@valorComboBox1", valorComboBox1);
                c2Value = cmd.ExecuteScalar()?.ToString();
                con.Close();
            }

            // se encuentra la abreviatura periodo reportado
            string valorComboBox3 = cmb_periodo_reportado_po.Text.ToString();

            string c3Value = "";
            using (SQLiteConnection con = new SQLiteConnection(cadena))
            {
                con.Open();
                string query = "SELECT abr FROM TC_PERIODO_REPORTADO WHERE descripcion = @valorComboBox3";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@valorComboBox3", valorComboBox3);
                c3Value = cmd.ExecuteScalar()?.ToString();
                con.Close();
            }

            // Concatenar c2 con el valor de ComboBox2
            string valorComboBox2 = cmb_numero_legislatura.Text.ToString();
            string resultadoConcatenado = c2Value + "_" + valorComboBox2 + "_" + c3Value;

            // Mostrar el resultado en TextBox1
            txtID.Text = resultadoConcatenado;
        }

        private void cmb_periodo_reportado_po_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_entidad_federativa.Text.ToString();

            // Realizar la consulta para obtener el valor de c2
            string c2Value = "";
            using (SQLiteConnection con = new SQLiteConnection(cadena))
            {
                con.Open();
                string query = "SELECT cve_ent FROM TC_AGEEM WHERE nom_ent = @valorComboBox1";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@valorComboBox1", valorComboBox1);
                c2Value = cmd.ExecuteScalar()?.ToString();
                con.Close();
            }

            // se encuentra la abreviatura periodo reportado
            string valorComboBox3 = cmb_periodo_reportado_po.Text.ToString();

            string c3Value = "";
            using (SQLiteConnection con = new SQLiteConnection(cadena))
            {
                con.Open();
                string query = "SELECT abr FROM TC_PERIODO_REPORTADO WHERE descripcion = @valorComboBox3";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@valorComboBox3", valorComboBox3);
                c3Value = cmd.ExecuteScalar()?.ToString();
                con.Close();
            }

            // Concatenar c2 con el valor de ComboBox2
            string valorComboBox2 = cmb_numero_legislatura.Text.ToString();
            string resultadoConcatenado = c2Value + "_" + valorComboBox2 + "_" + c3Value;

            // Mostrar el resultado en TextBox1
            txtID.Text = resultadoConcatenado;
        }
        
        // ID COMISION LEGISLATIVA
        private void cmb_tipo_comision_legislativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_tipo_comision_legislativa.Text.ToString();

            if (valorComboBox1 == "Otro tipo (especifique)")
            {
                Txt_otro_tipo_comision_legislativa_especifique.Enabled = true;
            }
            else
            {
                Txt_otro_tipo_comision_legislativa_especifique.Enabled = false;
                Txt_otro_tipo_comision_legislativa_especifique.Text = "";
            }

            // Realizar la consulta para obtener el valor de c2
            string c2Value = "";
            using (SQLiteConnection con = new SQLiteConnection(cadena))
            {
                con.Open();
                string query = "SELECT abr FROM TC_TIPO_COMISION WHERE descripcion = @valorComboBox1";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@valorComboBox1", valorComboBox1);
                c2Value = cmd.ExecuteScalar()?.ToString();
                con.Close();
            }

            // Concatenar c2 con el valor de ComboBox2
            string valorComboBox2 = Txt_consecutivo_comision_legislativa.Text.ToString();
            string valorComboBox3 = txtID.Text.Substring(0,2).ToString();
            string resultadoConcatenado = "COM_" + c2Value + "_" + valorComboBox3 + "_" + valorComboBox2;

            // Mostrar el resultado en TextBox1
            Txt_ID_comision_legislativa.Text = resultadoConcatenado;

           

        }
        private void Txt_consecutivo_comision_legislativa_TextChanged(object sender, EventArgs e)
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_tipo_comision_legislativa.Text.ToString();

            // Realizar la consulta para obtener el valor de c2
            string c2Value = "";
            using (SQLiteConnection con = new SQLiteConnection(cadena))
            {
                con.Open();
                string query = "SELECT abr FROM TC_TIPO_COMISION WHERE descripcion = @valorComboBox1";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@valorComboBox1", valorComboBox1);
                c2Value = cmd.ExecuteScalar()?.ToString();
                con.Close();
            }

            // Concatenar c2 con el valor de ComboBox2
            string valorComboBox2 = Txt_consecutivo_comision_legislativa.Text.ToString();
            string valorComboBox3 = txtID.Text.Substring(0,2).ToString();
            string resultadoConcatenado = "COM_" + c2Value + "_" + valorComboBox3 + "_" + valorComboBox2;

            // Mostrar el resultado en TextBox1
            Txt_ID_comision_legislativa.Text = resultadoConcatenado;
        }
        /*
        -------------------------------------------------- MAYUSCULAS ----------------------------------------------------
         */
        // datos generales
        private void txt_nombre_legislatura_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_nombre_legislatura.Text = txt_nombre_legislatura.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_nombre_legislatura.SelectionStart = txt_nombre_legislatura.Text.Length;
        }

        // COMISIONES LEGISLATIVAS
        private void Txt_nombre_comision_legislativa_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            Txt_nombre_comision_legislativa.Text = Txt_nombre_comision_legislativa.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            Txt_nombre_comision_legislativa.SelectionStart = Txt_nombre_comision_legislativa.Text.Length;
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
        private void txt_no_cond_celebracion_reuniones_comision_legislativa_especifique_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Text = txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.SelectionStart = txt_no_cond_celebracion_reuniones_comision_legislativa_especifique.Text.Length;
        }
        private void txt_observaciones_cl_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto del TextBox a mayúsculas y establecerlo de nuevo en el TextBox
            txt_observaciones_cl.Text = txt_observaciones_cl.Text.ToUpper();

            // Colocar el cursor al final del texto para mantener la posición del cursor
            txt_observaciones_cl.SelectionStart = txt_observaciones_cl.Text.Length;
        }
        /*
        -------------------------------------------------- VALOR NUMERICO ----------------------------------------------------
         */
        // datos generales
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

        // COMISIONES LEGISLATIVAS
        private void Txt_consecutivo_comision_legislativa_KeyPress(object sender, KeyPressEventArgs e)
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
        
        private void txt_cant_integrantes_comision_legislativa_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, backspace, y el signo menos si está al principio
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '-') || (e.KeyChar == '-' && ((System.Windows.Forms.TextBox)sender).Text.Length != 0))
            {
                e.Handled = true; // Ignorar el carácter
            }
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
        /*
        -------------------------------------------------- VALIDACIONES ----------------------------------------------------
         */
        // datos generales
        private void Dtp_inicio_funciones_legislatura_ValueChanged_1(object sender, EventArgs e)
        {
            if (Dtp_inicio_funciones_legislatura.Value >= dtp_termino_funciones_legislatura.Value)
            {
                MessageBox.Show("La fecha de inicio debe ser menor que la fecha de término.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dtp_inicio_funciones_legislatura.Value = dtp_termino_funciones_legislatura.Value.AddDays(-1);
                Dtp_inicio_funciones_legislatura.Focus();
            }
        }

        private void Dtp_fecha_inicio_informacion_reportada_ValueChanged_1(object sender, EventArgs e)
        {
            if (Dtp_fecha_inicio_informacion_reportada.Value < Dtp_inicio_funciones_legislatura.Value ||
                Dtp_fecha_inicio_informacion_reportada.Value > dtp_termino_funciones_legislatura.Value)
            {
                MessageBox.Show("La fecha debe estar entre el inicio y término de funciones", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Dtp_fecha_inicio_informacion_reportada.Value = Dtp_inicio_funciones_legislatura.Value.AddDays(1);
                Dtp_fecha_inicio_informacion_reportada.Focus();
            }
        }

        private void Dtp_fecha_termino_informacion_reportada_ValueChanged_1(object sender, EventArgs e)
        {
            if (Dtp_fecha_termino_informacion_reportada.Value > dtp_termino_funciones_legislatura.Value ||
               Dtp_fecha_termino_informacion_reportada.Value < Dtp_fecha_inicio_informacion_reportada.Value)
            {
                MessageBox.Show("La fecha debe estar entre el inicio y término de funciones", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Dtp_fecha_termino_informacion_reportada.Value = dtp_termino_funciones_legislatura.Value.AddDays(-1);
                Dtp_fecha_termino_informacion_reportada.Focus();
            }
        }

        private void dtp_fecha_inicio_po_ValueChanged_1(object sender, EventArgs e)
        {
            if (dtp_fecha_inicio_po.Value < Dtp_fecha_inicio_informacion_reportada.Value
                || dtp_fecha_inicio_po.Value > Dtp_fecha_termino_informacion_reportada.Value)
            {
                MessageBox.Show("La fecha debe estar entre el periodo reportado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtp_fecha_inicio_po.Value = Dtp_fecha_inicio_informacion_reportada.Value.AddDays(1);
                dtp_fecha_inicio_po.Focus();
            }
        }

        private void dtp_fecha_termino_po_ValueChanged_1(object sender, EventArgs e)
        {
            if (dtp_fecha_termino_po.Value < Dtp_fecha_inicio_informacion_reportada.Value
               || dtp_fecha_termino_po.Value > Dtp_fecha_termino_informacion_reportada.Value)
            {
                MessageBox.Show("La fecha debe estar entre el periodo reportado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtp_fecha_termino_po.Value = Dtp_fecha_termino_informacion_reportada.Value.AddDays(-1);
                dtp_fecha_termino_po.Focus();
            }
        }

        // COMISIONES LEGISLATIVAS
        private void cmb_tema_comision_legislativa_SelectedIndexChanged(object sender, EventArgs e)
        {

            // Cuando se selecciona un elemento en ComboBox1, realizar la búsqueda y la concatenación
            string valorComboBox1 = cmb_tema_comision_legislativa.Text.ToString();

            if (valorComboBox1 == "Otro tema o asunto (especifique)")
            {
                txt_otro_tema_comision_legislativa_especifique.Enabled = true;
            }
            else
            {
                txt_otro_tema_comision_legislativa_especifique.Enabled = false;
                txt_otro_tema_comision_legislativa_especifique.Text = "";
            }
        }
      
        private void txt_cant_integrantes_comision_legislativa_Leave(object sender, EventArgs e)
        {
            int valor;
            int.TryParse(txt_cant_integrantes_comision_legislativa.Text, out valor);
            
            // Verificar si el valor está dentro del rango permitido
            if (valor < -3)
            {
                MessageBox.Show("Registrar el valor correcto, ver cuadro de ayuda.");
                txt_cant_integrantes_comision_legislativa.Text = ""; // Limpiar el TextBox si está fuera del rango
            }

            if (valor == -1 || valor == -2 || valor == -3)
            {
                MessageBox.Show("Justificar la elección en el apartado de observaciones");
                txt_observaciones_cl.Focus();
            }

        }
        private void txt_cant_reuniones_celebradas_comision_legislativa_Leave(object sender, EventArgs e)
        {
            int valor;
            int.TryParse(txt_cant_reuniones_celebradas_comision_legislativa.Text, out valor);

            // Verificar si el valor está dentro del rango permitido
            if (valor < -3)
            {
                MessageBox.Show("Registrar el valor correcto, ver cuadro de ayuda.");
                txt_cant_reuniones_celebradas_comision_legislativa.Text = ""; // Limpiar el TextBox si está fuera del rango
            }

            if (valor == -1 || valor == -2 || valor == -3)
            {
                MessageBox.Show("Justificar la elección en el apartado de observaciones");
                txt_observaciones_cl.Focus();
            }
        }

        private void txt_cant_reuniones_celebradas_transmitidas_comision_legislativa_TextChanged(object sender, EventArgs e)
        {
            int valor;
            int.TryParse(txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.Text, out valor);

            // Verificar si el valor está dentro del rango permitido
            if (valor < -3)
            {
                MessageBox.Show("Registrar el valor correcto, ver cuadro de ayuda.");
                txt_cant_reuniones_celebradas_transmitidas_comision_legislativa.Text = ""; // Limpiar el TextBox si está fuera del rango
            }

            if (valor == -1 || valor == -2 || valor == -3)
            {
                MessageBox.Show("Justificar la elección en el apartado de observaciones");
                txt_observaciones_cl.Focus();
            }
        }

        private void txt_cant_iniciativas_turnadas_a_comision_legislativa_TextChanged(object sender, EventArgs e)
        {
            int valor;
            int.TryParse(txt_cant_iniciativas_turnadas_a_comision_legislativa.Text, out valor);

            // Verificar si el valor está dentro del rango permitido
            if (valor < -3)
            {
                MessageBox.Show("Registrar el valor correcto, ver cuadro de ayuda.");
                txt_cant_iniciativas_turnadas_a_comision_legislativa.Text = ""; // Limpiar el TextBox si está fuera del rango
            }

            if (valor == -1 || valor == -2 || valor == -3)
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
            if (valor < -3)
            {
                MessageBox.Show("Registrar el valor correcto, ver cuadro de ayuda.");
                txt_cant_dictamenes_emitidos_por_comision_legislativa.Text = ""; // Limpiar el TextBox si está fuera del rango
            }

            if (valor == -1 || valor == -2 || valor == -3)
            {
                MessageBox.Show("Justificar la elección en el apartado de observaciones");
                txt_observaciones_cl.Focus();
            }
        }
        /*
         -------------------------------------------------- CHECK BOX ----------------------------------------------------
          */
        private void chbPE_CheckedChanged_1(object sender, EventArgs e)
        {
            // Cuando el estado del CheckBox cambia, se ejecutará este código
            CheckBox chbPE = (CheckBox)sender;
            if (chbPE.Checked)
            {
                // Si el CheckBox está marcado
                dgvPE.Enabled = true; cmb_periodo_extraordinario_reportado.Enabled = true;
                dtp_fecha_inicio_pe.Enabled = true; dtp_fecha_termino_pe.Enabled = true; Txt_sesiones_celebradas_pe.Enabled = true;
                btnAgregarPE.Enabled = true; BtnEliminarPE.Enabled = true;
            }
            else
            {
                // Si el CheckBox está desmarcado
                dgvPE.Enabled = false; cmb_periodo_extraordinario_reportado.Enabled = false;
                dtp_fecha_inicio_pe.Enabled = false; dtp_fecha_termino_pe.Enabled = false; Txt_sesiones_celebradas_pe.Enabled = false;
                btnAgregarPE.Enabled = false; BtnEliminarPE.Enabled = false;
            }
        }

        /*
         -------------------------------------------------- MENSAJES DE AYUDA ----------------------------------------------------
          */

        // COMISIONES LEGISLATIVAS
        private void Txt_consecutivo_comision_legislativa_MouseHover(object sender, EventArgs e)
        {
            // Mostrar mensaje al pasar el ratón sobre el TextBox
            System.Windows.Forms.ToolTip tooltip = new System.Windows.Forms.ToolTip();
            tooltip.SetToolTip(Txt_consecutivo_comision_legislativa, "Número asignado a la comisión legislativa." +
                " Para el caso de las comisiones ordinarias, permanentes u homólogas, " +
                "se sugiere respetar el orden descendente de las fracciones establecidas en el correspondiente " +
                "artículo de la Ley o Reglamento del Congreso de la entidad federativa.");
        }

        private void pboConsecutivoComision_Click(object sender, EventArgs e)
        {
            string mensaje = "Número asignado a la comisión legislativa.\n\n" +
                "Para el caso de las comisiones ordinarias, permanentes u homólogas, " +
                "se sugiere respetar el orden descendente de las fracciones establecidas en el correspondiente " +
                "artículo de la Ley o Reglamento del Congreso de la entidad federativa.";
            string titulo = "Consecutivo de la comisión";

            MessageBox.Show(mensaje,titulo,MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void pbo_cant_integrantes_comision_legislativa_Click(object sender, EventArgs e)
        {
            string mensaje = "Los valores permitidos son:\n\n" +
                "-1: NS(No se sabe),\r\n     -2: NA(No aplica),\r\n     -3: ND(No te lo quiero dar)\r\n\n" +
                "Utilizar está numeración para los casos que apliquen.";

            string titulo = "";

            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pbo_cant_reuniones_celebradas_comision_legislativa_Click(object sender, EventArgs e)
        {
            string mensaje = "Los valores permitidos son:\n\n" +
               "-1: NS(No se sabe),\r\n     -2: NA(No aplica),\r\n     -3: ND(No te lo quiero dar)\r\n\n" +
               "Utilizar está numeración para los casos que apliquen.";

            string titulo = "";

            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pbo_cant_reuniones_celebradas_transmitidas_comision_legislativa_Click(object sender, EventArgs e)
        {
            string mensaje = "Los valores permitidos son:\n\n" +
               "-1: NS(No se sabe),\r\n     -2: NA(No aplica),\r\n     -3: ND(No te lo quiero dar)\r\n\n" +
               "Utilizar está numeración para los casos que apliquen.";

            string titulo = "";

            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pbo_cant_iniciativas_turnadas_a_comision_legislativa_Click(object sender, EventArgs e)
        {
            string mensaje = "Los valores permitidos son:\n\n" +
               "-1: NS(No se sabe),\r\n     -2: NA(No aplica),\r\n     -3: ND(No te lo quiero dar)\r\n\n" +
               "Utilizar está numeración para los casos que apliquen.";

            string titulo = "";

            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pbo_cant_dictamenes_emitidos_por_comision_legislativa_Click(object sender, EventArgs e)
        {
            string mensaje = "Los valores permitidos son:\n\n" +
               "-1: NS(No se sabe),\r\n     -2: NA(No aplica),\r\n     -3: ND(No te lo quiero dar)\r\n\n" +
               "Utilizar está numeración para los casos que apliquen.";

            string titulo = "";

            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
