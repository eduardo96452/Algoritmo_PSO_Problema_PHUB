using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Algoritmo_PSO_Problema_PHUB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Archivos de texto|*.txt";
            openFileDialog.Title = "Selecciona archivos de texto";

            List<string> archivosSeleccionados = new List<string>();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<string> contenidoArchivos = new List<string>();
                foreach (string archivo in archivosSeleccionados)
                {
                    string[] lineas = File.ReadAllLines(archivo);
                    contenidoArchivos.AddRange(lineas);
                }

                MessageBox.Show("Contenido de los archivos seleccionados:");
                foreach (string linea in contenidoArchivos)
                {
                    MessageBox.Show(linea);
                }
            }
            else
            {
                MessageBox.Show("No se seleccionaron archivos.");
            }

        }
    }
}
