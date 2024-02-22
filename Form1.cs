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
            // Crear un nuevo cuadro de diálogo de OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Establecer propiedades del cuadro de diálogo
            openFileDialog.Title = "Seleccionar archivo de datos";
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            // Mostrar el cuadro de diálogo y esperar a que el usuario seleccione un archivo
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Obtener la ruta del archivo seleccionado por el usuario
                string rutaArchivo = openFileDialog.FileName;

                // Lista para almacenar los nodos
                List<Nodo> nodos = new List<Nodo>();

                // Leer el archivo de texto línea por línea
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    string linea;
                    // Leer la primera línea para obtener información general
                    if ((linea = sr.ReadLine()) != null)
                    {
                        string[] infoGeneral = linea.Split(' ');
                        int totalNodos = int.Parse(infoGeneral[0]);
                        int p = int.Parse(infoGeneral[1]);
                        int capacidadServidor = int.Parse(infoGeneral[2]);
                        // Procesar el resto de las líneas
                        while ((linea = sr.ReadLine()) != null)
                        {
                            string[] datosNodo = linea.Split(' ');
                            Nodo nodo = new Nodo
                            {
                                NumeroNodo = int.Parse(datosNodo[0]),
                                CoordenadaX = double.Parse(datosNodo[1]),
                                CoordenadaY = double.Parse(datosNodo[2]),
                                Demanda = int.Parse(datosNodo[3])
                            };
                            nodos.Add(nodo);
                        }
                    }
                }

                // Mostrar los nodos en mensajes emergentes (MessageBox)
                foreach (var nodo in nodos)
                {
                    MessageBox.Show($"Nodo {nodo.NumeroNodo}: ({nodo.CoordenadaX}, {nodo.CoordenadaY}), Demanda: {nodo.Demanda}");
                }
            }
        }
    }
}
