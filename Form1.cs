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
        public TextBox TxtResults { get { return txtResults; } }
        List<Nodo> nodos = new List<Nodo>();

        int totalNodos =0;
        int p = 0;
        int capacidadServidor = 0;
        Swarm enjambre = new Swarm();   //posible error
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de texto|*.txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string rutaArchivo = openFileDialog.FileName;

                try
                {

                    using (StreamReader sr = new StreamReader(rutaArchivo))
                    {
                        string linea;
                        if ((linea = sr.ReadLine()) != null)
                        {
                            string[] infoGeneral = linea.Split(' ');
                             totalNodos = int.Parse(infoGeneral[0]);
                             p = int.Parse(infoGeneral[1]);
                             capacidadServidor = int.Parse(infoGeneral[2]);

                            while ((linea = sr.ReadLine()) != null)
                            {
                                string[] datosNodo = linea.Split(' ');
                                Nodo nodo = new Nodo
                                {
                                    Numero = int.Parse(datosNodo[0]),
                                    CoordenadaX = double.Parse(datosNodo[1]),
                                    CoordenadaY = double.Parse(datosNodo[2]),
                                    Demanda = int.Parse(datosNodo[3])
                                };
                                nodos.Add(nodo);
                            }
                        }
                    }
                    enjambre.Nodos = nodos; 
                    enjambre.NumberOfServers = p; 
                    enjambre.ServerCapacity = capacidadServidor; 
                    enjambre.Iteraciones = 1000;
                    List<Nodo> respuesta =enjambre.GenerateRandomSolutions(this);


                    foreach (var resp in respuesta)
                    {
                        MessageBox.Show($"Nodo {resp.Numero}: ({resp.CoordenadaX}, {resp.CoordenadaY}), Demanda: {resp.Demanda}");
                    }
                    //foreach (var nodo in nodos)
                    //{
                    //    MessageBox.Show($"Nodo {nodo.Numero}: ({nodo.CoordenadaX}, {nodo.CoordenadaY}), Demanda: {nodo.Demanda}");
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al leer el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
