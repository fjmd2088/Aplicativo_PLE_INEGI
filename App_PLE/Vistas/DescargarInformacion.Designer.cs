namespace App_PLE.Vistas
{
    partial class DescargarInformacion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.clbTablasDB = new System.Windows.Forms.CheckedListBox();
            this.clbFormatos = new System.Windows.Forms.CheckedListBox();
            this.btnDescargarForm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // clbTablasDB
            // 
            this.clbTablasDB.FormattingEnabled = true;
            this.clbTablasDB.Items.AddRange(new object[] {
            "TR_DATOS_GENERALES",
            "Comisiones Legislativas",
            "Personas Legisladoras",
            "Personal de apoyo",
            "Iniciativas",
            "Iniciativas de Obvia Resolución",
            "Juicios Políticos",
            "Comparecencias",
            "Declaraciones Procedencias"});
            this.clbTablasDB.Location = new System.Drawing.Point(285, 57);
            this.clbTablasDB.Name = "clbTablasDB";
            this.clbTablasDB.Size = new System.Drawing.Size(204, 154);
            this.clbTablasDB.TabIndex = 0;
            // 
            // clbFormatos
            // 
            this.clbFormatos.FormattingEnabled = true;
            this.clbFormatos.Items.AddRange(new object[] {
            "XLSX",
            "CSV"});
            this.clbFormatos.Location = new System.Drawing.Point(194, 254);
            this.clbFormatos.Name = "clbFormatos";
            this.clbFormatos.Size = new System.Drawing.Size(326, 79);
            this.clbFormatos.TabIndex = 1;
            // 
            // btnDescargarForm
            // 
            this.btnDescargarForm.Location = new System.Drawing.Point(239, 371);
            this.btnDescargarForm.Name = "btnDescargarForm";
            this.btnDescargarForm.Size = new System.Drawing.Size(140, 39);
            this.btnDescargarForm.TabIndex = 2;
            this.btnDescargarForm.Text = "Aceptar";
            this.btnDescargarForm.UseVisualStyleBackColor = true;
            this.btnDescargarForm.Click += new System.EventHandler(this.btnDescargarForm_Click);
            // 
            // DescargarInformacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnDescargarForm);
            this.Controls.Add(this.clbFormatos);
            this.Controls.Add(this.clbTablasDB);
            this.Name = "DescargarInformacion";
            this.Text = "DescargarInformacion";
            this.Load += new System.EventHandler(this.DescargarInformacion_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbTablasDB;
        private System.Windows.Forms.CheckedListBox clbFormatos;
        private System.Windows.Forms.Button btnDescargarForm;
    }
}