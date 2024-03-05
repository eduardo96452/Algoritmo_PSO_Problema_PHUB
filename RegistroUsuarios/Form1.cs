namespace RegistroUsuarios
{
    public partial class Form1 : Form
    {
        private List<Usuario> usuarios = new List<Usuario>();
        private string filePath = "usuarios.txt";
        public Form1()
        {
            InitializeComponent();
            dgvUsuarios.Columns.Add("Nombre", "Nombre");
            dgvUsuarios.Columns.Add("Apellido", "Apellido");
            dgvUsuarios.Columns.Add("Cedula", "Cédula");
            dgvUsuarios.Columns.Add("Genero", "Género");
            dgvUsuarios.Columns.Add("FechaNacimiento", "Fecha de Nacimiento");
            dgvUsuarios.Columns.Add("Direccion", "Dirección");
            dgvUsuarios.Columns.Add("FotoPerfil", "Foto de Perfil");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (CamposVacios())
            {
                MessageBox.Show("Todos los campos son requeridos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                string nombre = txtNombre.Text;
                string apellido = txtApellido.Text;
                string cedula = txtCedula.Text;
                string genero = rbMasculino.Checked ? "Masculino" : "Femenino";
                string fechaNacimiento = dtFechaNacimiento.Value.ToString();
                string direccion = txtDireccion.Text;
                string fotoPerfil = pbFotoPerfil.ImageLocation;

                Usuario usuario = new Usuario(nombre, apellido, cedula, genero, fechaNacimiento, direccion, fotoPerfil);
                usuarios.Add(usuario);

                GuardarUsuariosEnArchivo();
                CargarUsuariosEnDataGridView();
            }
        }
        private void GuardarUsuariosEnArchivo()
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (var usuario in usuarios)
                {
                    sw.WriteLine($"{usuario.Nombre},{usuario.Apellido},{usuario.Cedula},{usuario.Genero},{usuario.FechaNacimiento},{usuario.Direccion},{usuario.FotoPerfil}");
                }
            }
        }
        private void CargarUsuariosEnDataGridView()
        {
            dgvUsuarios.Rows.Clear();
            foreach (var usuario in usuarios)
            {
                dgvUsuarios.Rows.Add(usuario.Nombre, usuario.Apellido, usuario.Cedula, usuario.Genero, usuario.FechaNacimiento, usuario.Direccion, usuario.FotoPerfil);
            }
        }

        private void dgvUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvUsuarios.Rows.Count - 1)
            {
                DataGridViewRow row = dgvUsuarios.Rows[e.RowIndex];
                string nombre = row.Cells[0].Value.ToString();
                string apellido = row.Cells[1].Value.ToString();
                string cedula = row.Cells[2].Value.ToString();
                string genero = row.Cells[3].Value.ToString();
                string fechaNacimiento = row.Cells[4].Value.ToString();
                string direccion = row.Cells[5].Value.ToString();
                string fotoPerfil = row.Cells[6].Value.ToString();

                txtNombre.Text = nombre;
                txtApellido.Text = apellido;
                txtCedula.Text = cedula;
                rbMasculino.Checked = genero == "Masculino";
                rbFemenino.Checked = genero == "Femenino";
                dtFechaNacimiento.Value = DateTime.Parse(fechaNacimiento);
                txtDireccion.Text = direccion;
                pbFotoPerfil.ImageLocation = fotoPerfil;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (CamposVacios())
            {
                MessageBox.Show("Todos los campos son requeridos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (dgvUsuarios.SelectedRows.Count > 0)
                {
                    int selectedIndex = dgvUsuarios.SelectedRows[0].Index;
                    usuarios.RemoveAt(selectedIndex);
                    GuardarUsuariosEnArchivo();
                    CargarUsuariosEnDataGridView();
                }
            }
        }

        private void btnCargarImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog.Title = "Seleccionar imagen";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pbFotoPerfil.ImageLocation = openFileDialog.FileName;
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            usuarios.Clear();
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] datos = line.Split(',');
                    Usuario usuario = new Usuario(datos[0], datos[1], datos[2], datos[3], datos[4], datos[5], datos[6]);
                    usuarios.Add(usuario);
                }
            }

            CargarUsuariosEnDataGridView();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (CamposVacios())
            {
                MessageBox.Show("Todos los campos son requeridos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (dgvUsuarios.SelectedRows.Count > 0)
                {
                    int selectedIndex = dgvUsuarios.SelectedRows[0].Index;
                    Usuario usuario = usuarios[selectedIndex];

                    usuario.Nombre = txtNombre.Text;
                    usuario.Apellido = txtApellido.Text;
                    usuario.Cedula = txtCedula.Text;
                    usuario.Genero = rbMasculino.Checked ? "Masculino" : "Femenino";
                    usuario.FechaNacimiento = dtFechaNacimiento.Value.ToString();
                    usuario.Direccion = txtDireccion.Text;
                    usuario.FotoPerfil = pbFotoPerfil.ImageLocation;

                    GuardarUsuariosEnArchivo();
                    CargarUsuariosEnDataGridView();
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un usuario para modificar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private bool CamposVacios()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtCedula.Text) ||
                (!rbMasculino.Checked && !rbFemenino.Checked) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(pbFotoPerfil.ImageLocation))
            {
                return true;
            }

            return false;
        }

        private void btnLimpiarCampos_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtCedula.Text = "";
            rbMasculino.Checked = false;
            rbFemenino.Checked = false;
            dtFechaNacimiento.Value = DateTime.Today;
            txtDireccion.Text = "";
            pbFotoPerfil.ImageLocation = null;
        }
    }
}
