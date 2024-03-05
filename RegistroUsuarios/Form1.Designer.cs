namespace RegistroUsuarios
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            txtCedula = new TextBox();
            txtDireccion = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            rbMasculino = new RadioButton();
            rbFemenino = new RadioButton();
            rbGenero = new Panel();
            dtFechaNacimiento = new DateTimePicker();
            pbFotoPerfil = new PictureBox();
            label7 = new Label();
            dgvUsuarios = new DataGridView();
            btnGuardar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            btnCargarImagen = new Button();
            btnCargar = new Button();
            btnLimpiarCampos = new Button();
            rbGenero.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbFotoPerfil).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            SuspendLayout();
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(125, 22);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(165, 23);
            txtNombre.TabIndex = 0;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(125, 56);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(165, 23);
            txtApellido.TabIndex = 1;
            // 
            // txtCedula
            // 
            txtCedula.Location = new Point(125, 94);
            txtCedula.Name = "txtCedula";
            txtCedula.Size = new Size(165, 23);
            txtCedula.TabIndex = 2;
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(766, 97);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(165, 23);
            txtDireccion.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(40, 30);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 6;
            label1.Text = "Nombre";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(40, 59);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 7;
            label2.Text = "Apellido";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(40, 97);
            label3.Name = "label3";
            label3.Size = new Size(44, 15);
            label3.TabIndex = 8;
            label3.Text = "Cedula";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(309, 26);
            label4.Name = "label4";
            label4.Size = new Size(45, 15);
            label4.TabIndex = 9;
            label4.Text = "Genero";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(314, 100);
            label5.Name = "label5";
            label5.Size = new Size(103, 15);
            label5.TabIndex = 10;
            label5.Text = "Fecha Nacimiento";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(681, 97);
            label6.Name = "label6";
            label6.Size = new Size(57, 15);
            label6.TabIndex = 11;
            label6.Text = "Dirección";
            // 
            // rbMasculino
            // 
            rbMasculino.AutoSize = true;
            rbMasculino.Location = new Point(13, 8);
            rbMasculino.Name = "rbMasculino";
            rbMasculino.Size = new Size(80, 19);
            rbMasculino.TabIndex = 12;
            rbMasculino.TabStop = true;
            rbMasculino.Text = "Masculino";
            rbMasculino.UseVisualStyleBackColor = true;
            // 
            // rbFemenino
            // 
            rbFemenino.AutoSize = true;
            rbFemenino.Location = new Point(13, 34);
            rbFemenino.Name = "rbFemenino";
            rbFemenino.Size = new Size(78, 19);
            rbFemenino.TabIndex = 13;
            rbFemenino.TabStop = true;
            rbFemenino.Text = "Femenino";
            rbFemenino.UseVisualStyleBackColor = true;
            // 
            // rbGenero
            // 
            rbGenero.Controls.Add(rbMasculino);
            rbGenero.Controls.Add(rbFemenino);
            rbGenero.Location = new Point(383, 10);
            rbGenero.Name = "rbGenero";
            rbGenero.Size = new Size(139, 65);
            rbGenero.TabIndex = 14;
            // 
            // dtFechaNacimiento
            // 
            dtFechaNacimiento.Location = new Point(423, 94);
            dtFechaNacimiento.Name = "dtFechaNacimiento";
            dtFechaNacimiento.Size = new Size(232, 23);
            dtFechaNacimiento.TabIndex = 15;
            // 
            // pbFotoPerfil
            // 
            pbFotoPerfil.BorderStyle = BorderStyle.FixedSingle;
            pbFotoPerfil.Location = new Point(652, 16);
            pbFotoPerfil.Name = "pbFotoPerfil";
            pbFotoPerfil.Size = new Size(165, 64);
            pbFotoPerfil.SizeMode = PictureBoxSizeMode.StretchImage;
            pbFotoPerfil.TabIndex = 16;
            pbFotoPerfil.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(549, 44);
            label7.Name = "label7";
            label7.Size = new Size(80, 15);
            label7.TabIndex = 17;
            label7.Text = "Foto de  Perfil";
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.Location = new Point(12, 200);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.Size = new Size(954, 238);
            dgvUsuarios.TabIndex = 18;
            dgvUsuarios.CellDoubleClick += dgvUsuarios_CellDoubleClick;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(259, 148);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 23);
            btnGuardar.TabIndex = 19;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(427, 148);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(75, 23);
            btnModificar.TabIndex = 20;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(624, 148);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 21;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnCargarImagen
            // 
            btnCargarImagen.Location = new Point(843, 30);
            btnCargarImagen.Name = "btnCargarImagen";
            btnCargarImagen.Size = new Size(97, 23);
            btnCargarImagen.TabIndex = 22;
            btnCargarImagen.Text = "Cargar Imagen";
            btnCargarImagen.UseVisualStyleBackColor = true;
            btnCargarImagen.Click += btnCargarImagen_Click;
            // 
            // btnCargar
            // 
            btnCargar.Location = new Point(12, 148);
            btnCargar.Name = "btnCargar";
            btnCargar.Size = new Size(113, 23);
            btnCargar.TabIndex = 23;
            btnCargar.Text = "Cargar datos en la tabla";
            btnCargar.UseVisualStyleBackColor = true;
            btnCargar.Click += btnCargar_Click;
            // 
            // btnLimpiarCampos
            // 
            btnLimpiarCampos.Location = new Point(811, 148);
            btnLimpiarCampos.Name = "btnLimpiarCampos";
            btnLimpiarCampos.Size = new Size(155, 23);
            btnLimpiarCampos.TabIndex = 24;
            btnLimpiarCampos.Text = "Limpiar campos";
            btnLimpiarCampos.UseVisualStyleBackColor = true;
            btnLimpiarCampos.Click += btnLimpiarCampos_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(978, 450);
            Controls.Add(btnLimpiarCampos);
            Controls.Add(btnCargar);
            Controls.Add(btnCargarImagen);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnGuardar);
            Controls.Add(dgvUsuarios);
            Controls.Add(label7);
            Controls.Add(pbFotoPerfil);
            Controls.Add(dtFechaNacimiento);
            Controls.Add(rbGenero);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtDireccion);
            Controls.Add(txtCedula);
            Controls.Add(txtApellido);
            Controls.Add(txtNombre);
            Name = "Form1";
            Text = "Form1";
            rbGenero.ResumeLayout(false);
            rbGenero.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbFotoPerfil).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNombre;
        private TextBox txtApellido;
        private TextBox txtCedula;
        private TextBox txtDireccion;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private RadioButton rbMasculino;
        private RadioButton rbFemenino;
        private Panel rbGenero;
        private DateTimePicker dtFechaNacimiento;
        private PictureBox pbFotoPerfil;
        private Label label7;
        private DataGridView dgvUsuarios;
        private Button btnGuardar;
        private Button btnModificar;
        private Button btnEliminar;
        private Button btnCargarImagen;
        private Button btnCargar;
        private Button btnLimpiarCampos;
    }
}
