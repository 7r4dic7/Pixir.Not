namespace Pixir.Not.View.Views.Common.Persona
{
    partial class frmPersonaPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPersonaPrincipal));
            this.picModulo = new System.Windows.Forms.PictureBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblFiltros = new System.Windows.Forms.Label();
            this.lblResultados = new System.Windows.Forms.Label();
            this.lblDetalle = new System.Windows.Forms.Label();
            this.pnlPrincipalDetalle = new System.Windows.Forms.Panel();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.pnlBusqueda = new System.Windows.Forms.Panel();
            this.dgvFiltroComCatSexo = new System.Windows.Forms.DataGridView();
            this.dgvComCatEstadoCivil = new System.Windows.Forms.DataGridView();
            this.dgvResultadoPersona = new System.Windows.Forms.DataGridView();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.dgvDetalleComDatoContacto = new System.Windows.Forms.DataGridView();
            this.btnSms = new System.Windows.Forms.Button();
            this.btnCorreoElectronico = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picModulo)).BeginInit();
            this.pnlPrincipalDetalle.SuspendLayout();
            this.pnlBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiltroComCatSexo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComCatEstadoCivil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultadoPersona)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleComDatoContacto)).BeginInit();
            this.SuspendLayout();
            // 
            // picModulo
            // 
            this.picModulo.Image = ((System.Drawing.Image)(resources.GetObject("picModulo.Image")));
            this.picModulo.Location = new System.Drawing.Point(12, 5);
            this.picModulo.Name = "picModulo";
            this.picModulo.Size = new System.Drawing.Size(36, 35);
            this.picModulo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picModulo.TabIndex = 0;
            this.picModulo.TabStop = false;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(65, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(60, 20);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Persona";
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(892, 11);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(94, 29);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(12, 46);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(94, 29);
            this.btnAgregar.TabIndex = 3;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(221, 46);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(294, 27);
            this.txtBuscar.TabIndex = 4;
            // 
            // lblFiltros
            // 
            this.lblFiltros.AutoSize = true;
            this.lblFiltros.Location = new System.Drawing.Point(12, 102);
            this.lblFiltros.Name = "lblFiltros";
            this.lblFiltros.Size = new System.Drawing.Size(49, 20);
            this.lblFiltros.TabIndex = 6;
            this.lblFiltros.Text = "Filtros";
            // 
            // lblResultados
            // 
            this.lblResultados.AutoSize = true;
            this.lblResultados.Location = new System.Drawing.Point(208, 102);
            this.lblResultados.Name = "lblResultados";
            this.lblResultados.Size = new System.Drawing.Size(81, 20);
            this.lblResultados.TabIndex = 8;
            this.lblResultados.Text = "Resultados";
            // 
            // lblDetalle
            // 
            this.lblDetalle.AutoSize = true;
            this.lblDetalle.Location = new System.Drawing.Point(3, 8);
            this.lblDetalle.Name = "lblDetalle";
            this.lblDetalle.Size = new System.Drawing.Size(57, 20);
            this.lblDetalle.TabIndex = 10;
            this.lblDetalle.Text = "Detalle";
            this.lblDetalle.Click += new System.EventHandler(this.lblDetalle_Click);
            // 
            // pnlPrincipalDetalle
            // 
            this.pnlPrincipalDetalle.Controls.Add(this.dgvDetalleComDatoContacto);
            this.pnlPrincipalDetalle.Controls.Add(this.btnImprimir);
            this.pnlPrincipalDetalle.Controls.Add(this.lblDetalle);
            this.pnlPrincipalDetalle.Location = new System.Drawing.Point(633, 94);
            this.pnlPrincipalDetalle.Name = "pnlPrincipalDetalle";
            this.pnlPrincipalDetalle.Size = new System.Drawing.Size(353, 564);
            this.pnlPrincipalDetalle.TabIndex = 11;
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(592, 47);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(94, 29);
            this.btnActualizar.TabIndex = 12;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Location = new System.Drawing.Point(692, 47);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(94, 29);
            this.btnSeleccionar.TabIndex = 13;
            this.btnSeleccionar.Text = "Seleccionar";
            this.btnSeleccionar.UseVisualStyleBackColor = true;
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(792, 47);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(94, 29);
            this.btnEditar.TabIndex = 14;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(892, 47);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(94, 29);
            this.btnEliminar.TabIndex = 15;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            // 
            // pnlBusqueda
            // 
            this.pnlBusqueda.Controls.Add(this.dgvResultadoPersona);
            this.pnlBusqueda.Location = new System.Drawing.Point(4, 46);
            this.pnlBusqueda.Name = "pnlBusqueda";
            this.pnlBusqueda.Size = new System.Drawing.Size(982, 612);
            this.pnlBusqueda.TabIndex = 16;
            this.pnlBusqueda.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBusqueda_Paint);
            // 
            // dgvFiltroComCatSexo
            // 
            this.dgvFiltroComCatSexo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiltroComCatSexo.Location = new System.Drawing.Point(12, 125);
            this.dgvFiltroComCatSexo.Name = "dgvFiltroComCatSexo";
            this.dgvFiltroComCatSexo.RowHeadersWidth = 51;
            this.dgvFiltroComCatSexo.RowTemplate.Height = 29;
            this.dgvFiltroComCatSexo.Size = new System.Drawing.Size(190, 132);
            this.dgvFiltroComCatSexo.TabIndex = 17;
            // 
            // dgvComCatEstadoCivil
            // 
            this.dgvComCatEstadoCivil.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComCatEstadoCivil.Location = new System.Drawing.Point(12, 263);
            this.dgvComCatEstadoCivil.Name = "dgvComCatEstadoCivil";
            this.dgvComCatEstadoCivil.RowHeadersWidth = 51;
            this.dgvComCatEstadoCivil.RowTemplate.Height = 29;
            this.dgvComCatEstadoCivil.Size = new System.Drawing.Size(190, 178);
            this.dgvComCatEstadoCivil.TabIndex = 18;
            // 
            // dgvResultadoPersona
            // 
            this.dgvResultadoPersona.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultadoPersona.Location = new System.Drawing.Point(204, 31);
            this.dgvResultadoPersona.Name = "dgvResultadoPersona";
            this.dgvResultadoPersona.RowHeadersWidth = 51;
            this.dgvResultadoPersona.RowTemplate.Height = 29;
            this.dgvResultadoPersona.Size = new System.Drawing.Size(395, 530);
            this.dgvResultadoPersona.TabIndex = 0;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(256, 31);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(94, 29);
            this.btnImprimir.TabIndex = 11;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            // 
            // dgvDetalleComDatoContacto
            // 
            this.dgvDetalleComDatoContacto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleComDatoContacto.Location = new System.Drawing.Point(29, 348);
            this.dgvDetalleComDatoContacto.Name = "dgvDetalleComDatoContacto";
            this.dgvDetalleComDatoContacto.RowHeadersWidth = 51;
            this.dgvDetalleComDatoContacto.RowTemplate.Height = 29;
            this.dgvDetalleComDatoContacto.Size = new System.Drawing.Size(300, 188);
            this.dgvDetalleComDatoContacto.TabIndex = 12;
            // 
            // btnSms
            // 
            this.btnSms.Location = new System.Drawing.Point(768, 664);
            this.btnSms.Name = "btnSms";
            this.btnSms.Size = new System.Drawing.Size(94, 29);
            this.btnSms.TabIndex = 19;
            this.btnSms.Text = "SMS";
            this.btnSms.UseVisualStyleBackColor = true;
            // 
            // btnCorreoElectronico
            // 
            this.btnCorreoElectronico.Location = new System.Drawing.Point(868, 664);
            this.btnCorreoElectronico.Name = "btnCorreoElectronico";
            this.btnCorreoElectronico.Size = new System.Drawing.Size(94, 29);
            this.btnCorreoElectronico.TabIndex = 20;
            this.btnCorreoElectronico.Text = "Correo Electronico";
            this.btnCorreoElectronico.UseVisualStyleBackColor = true;
            // 
            // frmPersonaPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(998, 719);
            this.Controls.Add(this.btnCorreoElectronico);
            this.Controls.Add(this.btnSms);
            this.Controls.Add(this.dgvComCatEstadoCivil);
            this.Controls.Add(this.dgvFiltroComCatSexo);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnSeleccionar);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.lblResultados);
            this.Controls.Add(this.lblFiltros);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.picModulo);
            this.Controls.Add(this.pnlPrincipalDetalle);
            this.Controls.Add(this.pnlBusqueda);
            this.ForeColor = System.Drawing.Color.Black;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPersonaPrincipal";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPersonaPrincipal";
            this.Load += new System.EventHandler(this.frmPersonaPrincipal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picModulo)).EndInit();
            this.pnlPrincipalDetalle.ResumeLayout(false);
            this.pnlPrincipalDetalle.PerformLayout();
            this.pnlBusqueda.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiltroComCatSexo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComCatEstadoCivil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultadoPersona)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleComDatoContacto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox picModulo;
        private Label lblTitulo;
        private Button btnSalir;
        private Button btnAgregar;
        private TextBox txtBuscar;
        private Label lblFiltros;
        private Label lblResultados;
        private Label lblDetalle;
        private Panel pnlPrincipalDetalle;
        private Button btnActualizar;
        private Button btnSeleccionar;
        private Button btnEditar;
        private Button btnEliminar;
        private Panel pnlBusqueda;
        private DataGridView dgvFiltroComCatSexo;
        private DataGridView dgvComCatEstadoCivil;
        private DataGridView dgvResultadoPersona;
        private DataGridView dgvDetalleComDatoContacto;
        private Button btnImprimir;
        private Button btnSms;
        private Button btnCorreoElectronico;
    }
}