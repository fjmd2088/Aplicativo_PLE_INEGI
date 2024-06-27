namespace App_PLE
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuVertical = new System.Windows.Forms.Panel();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnDecargarInformacion = new System.Windows.Forms.Button();
            this.btnResumen = new System.Windows.Forms.Button();
            this.btnDG = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.barraTitulo = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.iconoMaximizar = new System.Windows.Forms.PictureBox();
            this.iconoMinimizar = new System.Windows.Forms.PictureBox();
            this.iconoRestaurar = new System.Windows.Forms.PictureBox();
            this.iconoCerrar = new System.Windows.Forms.PictureBox();
            this.btnSlide = new System.Windows.Forms.PictureBox();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.menuVertical.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.barraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconoMaximizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconoMinimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconoRestaurar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconoCerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSlide)).BeginInit();
            this.SuspendLayout();
            // 
            // menuVertical
            // 
            this.menuVertical.BackColor = System.Drawing.Color.Honeydew;
            this.menuVertical.Controls.Add(this.btnDashboard);
            this.menuVertical.Controls.Add(this.btnDecargarInformacion);
            this.menuVertical.Controls.Add(this.btnResumen);
            this.menuVertical.Controls.Add(this.btnDG);
            this.menuVertical.Controls.Add(this.pictureBox1);
            this.menuVertical.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuVertical.Location = new System.Drawing.Point(0, 0);
            this.menuVertical.Name = "menuVertical";
            this.menuVertical.Size = new System.Drawing.Size(195, 700);
            this.menuVertical.TabIndex = 0;
            // 
            // btnDashboard
            // 
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Image = ((System.Drawing.Image)(resources.GetObject("btnDashboard.Image")));
            this.btnDashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.Location = new System.Drawing.Point(12, 113);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(160, 37);
            this.btnDashboard.TabIndex = 4;
            this.btnDashboard.Text = "     Dashboard";
            this.btnDashboard.UseVisualStyleBackColor = true;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // btnDecargarInformacion
            // 
            this.btnDecargarInformacion.FlatAppearance.BorderSize = 0;
            this.btnDecargarInformacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDecargarInformacion.Image = ((System.Drawing.Image)(resources.GetObject("btnDecargarInformacion.Image")));
            this.btnDecargarInformacion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDecargarInformacion.Location = new System.Drawing.Point(12, 156);
            this.btnDecargarInformacion.Name = "btnDecargarInformacion";
            this.btnDecargarInformacion.Size = new System.Drawing.Size(160, 37);
            this.btnDecargarInformacion.TabIndex = 3;
            this.btnDecargarInformacion.Text = "        Descargar";
            this.btnDecargarInformacion.UseVisualStyleBackColor = true;
            this.btnDecargarInformacion.Click += new System.EventHandler(this.btnDecargarInformacion_Click);
            // 
            // btnResumen
            // 
            this.btnResumen.FlatAppearance.BorderSize = 0;
            this.btnResumen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResumen.Image = ((System.Drawing.Image)(resources.GetObject("btnResumen.Image")));
            this.btnResumen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnResumen.Location = new System.Drawing.Point(12, 61);
            this.btnResumen.Name = "btnResumen";
            this.btnResumen.Size = new System.Drawing.Size(160, 37);
            this.btnResumen.TabIndex = 2;
            this.btnResumen.Text = "     Inicio";
            this.btnResumen.UseVisualStyleBackColor = true;
            this.btnResumen.Click += new System.EventHandler(this.btnResumen_Click);
            // 
            // btnDG
            // 
            this.btnDG.FlatAppearance.BorderSize = 0;
            this.btnDG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDG.Image = ((System.Drawing.Image)(resources.GetObject("btnDG.Image")));
            this.btnDG.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDG.Location = new System.Drawing.Point(12, 199);
            this.btnDG.Name = "btnDG";
            this.btnDG.Size = new System.Drawing.Size(160, 37);
            this.btnDG.TabIndex = 1;
            this.btnDG.Text = "     Carga Masiva";
            this.btnDG.UseVisualStyleBackColor = true;
            this.btnDG.Click += new System.EventHandler(this.btnDG_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(160, 37);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // barraTitulo
            // 
            this.barraTitulo.Controls.Add(this.label1);
            this.barraTitulo.Controls.Add(this.iconoMaximizar);
            this.barraTitulo.Controls.Add(this.iconoMinimizar);
            this.barraTitulo.Controls.Add(this.iconoRestaurar);
            this.barraTitulo.Controls.Add(this.iconoCerrar);
            this.barraTitulo.Controls.Add(this.btnSlide);
            this.barraTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.barraTitulo.Location = new System.Drawing.Point(195, 0);
            this.barraTitulo.Name = "barraTitulo";
            this.barraTitulo.Size = new System.Drawing.Size(1089, 50);
            this.barraTitulo.TabIndex = 1;
            this.barraTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.barraTitulo_MouseDown);
            // 
            // label1
            // 
            this.label1.AllowDrop = true;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(126, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(792, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Registro de Datos estadísticos de los Congresos en entidades federativas";
            // 
            // iconoMaximizar
            // 
            this.iconoMaximizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconoMaximizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconoMaximizar.Image = ((System.Drawing.Image)(resources.GetObject("iconoMaximizar.Image")));
            this.iconoMaximizar.Location = new System.Drawing.Point(1021, 12);
            this.iconoMaximizar.Name = "iconoMaximizar";
            this.iconoMaximizar.Size = new System.Drawing.Size(20, 20);
            this.iconoMaximizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconoMaximizar.TabIndex = 4;
            this.iconoMaximizar.TabStop = false;
            this.iconoMaximizar.Click += new System.EventHandler(this.iconoMaximizar_Click);
            // 
            // iconoMinimizar
            // 
            this.iconoMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconoMinimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconoMinimizar.Image = ((System.Drawing.Image)(resources.GetObject("iconoMinimizar.Image")));
            this.iconoMinimizar.Location = new System.Drawing.Point(990, 12);
            this.iconoMinimizar.Name = "iconoMinimizar";
            this.iconoMinimizar.Size = new System.Drawing.Size(20, 20);
            this.iconoMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconoMinimizar.TabIndex = 3;
            this.iconoMinimizar.TabStop = false;
            this.iconoMinimizar.Click += new System.EventHandler(this.iconoMinimizar_Click);
            // 
            // iconoRestaurar
            // 
            this.iconoRestaurar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconoRestaurar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconoRestaurar.Image = ((System.Drawing.Image)(resources.GetObject("iconoRestaurar.Image")));
            this.iconoRestaurar.Location = new System.Drawing.Point(1021, 12);
            this.iconoRestaurar.Name = "iconoRestaurar";
            this.iconoRestaurar.Size = new System.Drawing.Size(20, 20);
            this.iconoRestaurar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconoRestaurar.TabIndex = 2;
            this.iconoRestaurar.TabStop = false;
            this.iconoRestaurar.Visible = false;
            this.iconoRestaurar.Click += new System.EventHandler(this.iconoRestaurar_Click);
            // 
            // iconoCerrar
            // 
            this.iconoCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconoCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconoCerrar.Image = ((System.Drawing.Image)(resources.GetObject("iconoCerrar.Image")));
            this.iconoCerrar.Location = new System.Drawing.Point(1050, 14);
            this.iconoCerrar.Name = "iconoCerrar";
            this.iconoCerrar.Size = new System.Drawing.Size(15, 15);
            this.iconoCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconoCerrar.TabIndex = 1;
            this.iconoCerrar.TabStop = false;
            this.iconoCerrar.Click += new System.EventHandler(this.iconoCerrar_Click);
            // 
            // btnSlide
            // 
            this.btnSlide.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSlide.Image = ((System.Drawing.Image)(resources.GetObject("btnSlide.Image")));
            this.btnSlide.Location = new System.Drawing.Point(8, 12);
            this.btnSlide.Name = "btnSlide";
            this.btnSlide.Size = new System.Drawing.Size(25, 25);
            this.btnSlide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnSlide.TabIndex = 0;
            this.btnSlide.TabStop = false;
            this.btnSlide.Click += new System.EventHandler(this.btnSlide_Click);
            // 
            // panelContenedor
            // 
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(195, 50);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(1089, 650);
            this.panelContenedor.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 700);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.barraTitulo);
            this.Controls.Add(this.menuVertical);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormPrincipal";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuVertical.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.barraTitulo.ResumeLayout(false);
            this.barraTitulo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconoMaximizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconoMinimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconoRestaurar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconoCerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSlide)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel menuVertical;
        private System.Windows.Forms.Panel barraTitulo;
        private System.Windows.Forms.PictureBox btnSlide;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox iconoCerrar;
        private System.Windows.Forms.PictureBox iconoMinimizar;
        private System.Windows.Forms.PictureBox iconoRestaurar;
        private System.Windows.Forms.PictureBox iconoMaximizar;
        private System.Windows.Forms.Button btnDG;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnResumen;
        private System.Windows.Forms.Button btnDecargarInformacion;
        private System.Windows.Forms.Button btnDashboard;
    }
}

