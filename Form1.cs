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
using Algoritmo_PSO_Problema_PHUB;

namespace Algoritmo_PSO_Problema_PHUB
{
    public partial class Form1 : Form
    {
        // Definición de las variables del problema
        static int n; // número de clientes
        static int p; // número de hubs
        static int Q; // capacidad de los hubs
        static int[,] clientes; // matriz de clientes: [id, x, y, demanda]
        static List<double[]> todasLasSoluciones = new List<double[]>(); //Lista para guardar todas las soluciones

        // Definición de las variables del algoritmo PSO
        static int numParticulas = 2;
        static int numIteraciones = 1000;
        static int numMaxSoluciones = 1; // Cambia este valor al número deseado de soluciones
        static double w = 0.7; // inercia
        static double c1 = 1.5; // constante de aprendizaje cognitivo
        static double c2 = 1.5; // constante de aprendizaje social

        // Mejor solución encontrada
        static double[] mejorPosicionGlobal;
        static double mejorValorGlobal = double.MaxValue;

        public Form1()
        {
            InitializeComponent();
        }

        private void Btn_SelecciónDatos_Click(object sender, EventArgs e)
        {
            // Crear un OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Establecer las propiedades del OpenFileDialog
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            // Mostrar el cuadro de diálogo para abrir archivo
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Obtener la ruta del archivo seleccionado
                string filePath = openFileDialog.FileName;

                // Enviar la ruta del archivo al método LeerDatos
                LeerDatos(filePath);
            }
        }
        
        private void Btn_GenerarPso_Click(object sender, EventArgs e)
        {
            // Obtener el número de partículas desde Txt_NumeroIteraciones
            if (!int.TryParse(Txt_NumeroIteraciones.Text, out numParticulas))
            {
                MessageBox.Show("Por favor, ingresa un número válido de partículas.");
                return;
            }

            // Reinicializar todasLasSoluciones
            todasLasSoluciones.Clear();

            // Inicializar PSO
            InicializarPSO();

            // Ejecutar PSO
            EjecutarPSO();

            // Mostrar resultados
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Mejor solución encontrada:");
            for (int i = 0; i < p; i++)
            {
                sb.AppendLine($"Hub {i + 1}: ({mejorPosicionGlobal[i * 2]}, {mejorPosicionGlobal[i * 2 + 1]})");
            }
            sb.AppendLine("Valor de la función objetivo:" + mejorValorGlobal);
            sb.AppendLine("...............................................................................");
            // Mostrar asignación de clientes a hubs en la mejor solución
            sb.AppendLine("Asignación de clientes a hubs en la mejor solución encontrada:");
            var asignacion = AsignacionClientesHub(mejorPosicionGlobal);
            foreach (var kvp in asignacion)
            {
                sb.AppendLine($"Hub {kvp.Key + 1}: {string.Join(", ", kvp.Value.Select(x => x + 1))}");
            }
            Txt_MejorSolucion.Text = sb.ToString();

            // Mostrar todas las soluciones encontradas
            StringBuilder sbsoluciones = new StringBuilder();
            sbsoluciones.AppendLine("\nTodas las soluciones encontradas:");
            for (int i = 0; i < todasLasSoluciones.Count; i++)
            {
                sbsoluciones.AppendLine($"Solución {i + 1}:");
                for (int j = 0; j < p; j++)
                {
                    sbsoluciones.AppendLine($"Hub {j + 1}: ({todasLasSoluciones[i][j * 2]}, {todasLasSoluciones[i][j * 2 + 1]})");
                }
                sbsoluciones.AppendLine("Valor de la función objetivo: " + FuncionObjetivo(todasLasSoluciones[i]));
                sbsoluciones.AppendLine("...........................");

            }
            txtSoluciones.Text = sbsoluciones.ToString();

            // Pintar los nodos Hub y los clientes en el PictureBox
            Pbx_Nodos.Invalidate();
                        
        }

        static Dictionary<int, List<int>> AsignacionClientesHub(double[] posicion)
        {
            Dictionary<int, List<int>> asignacion = new Dictionary<int, List<int>>();

            // Inicializar asignación
            for (int i = 0; i < p; i++)
            {
                asignacion.Add(i, new List<int>());
            }

            // Asignar clientes al hub más cercano
            for (int i = 0; i < n; i++)
            {
                double distanciaMinima = double.MaxValue;
                int hubAsignado = -1;
                for (int j = 0; j < p; j++)
                {
                    double distancia = Distancia(clientes[i, 1], clientes[i, 2], posicion[j * 2], posicion[j * 2 + 1]);
                    if (distancia < distanciaMinima)
                    {
                        distanciaMinima = distancia;
                        hubAsignado = j;
                    }
                }
                asignacion[hubAsignado].Add(i);
            }

            return asignacion;
        }

        static void LeerDatos(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            string[] firstLine = lines[0].Trim().Split(' '); // Trim() elimina los espacios en blanco al principio y al final de la cadena
            n = int.Parse(firstLine[0]);
            p = int.Parse(firstLine[1]);
            Q = int.Parse(firstLine[2]);
            clientes = new int[n, 4];
            for (int i = 0; i < n; i++)
            {
                string line = lines[i + 1].Trim();

                // Verificar si la línea contiene al menos un número
                if (line.Split(' ').Any(s => int.TryParse(s, out _)))
                {
                    string[] parts = line.Split(' ');
                    for (int j = 0; j < 4; j++)
                    {
                        clientes[i, j] = int.Parse(parts[j]);
                    }
                }
            }
        }

        static void InicializarPSO()
        {
            mejorPosicionGlobal = new double[p * 2]; // Posiciones de los hubs (x, y)
            var rnd = new Random();
            for (int i = 0; i < p * 2; i++)
            {
                mejorPosicionGlobal[i] = rnd.Next(100); // Supongamos un espacio de búsqueda de 0 a 100
            }
        }

        static void EjecutarPSO()
        {
            double[][] particulas = new double[numParticulas][];
            double[][] velocidades = new double[numParticulas][];


            // Inicializar partículas
            var rnd = new Random();
            for (int i = 0; i < numParticulas; i++)
            {
                particulas[i] = new double[p * 2];
                velocidades[i] = new double[p * 2];
                for (int j = 0; j < p * 2; j++)
                {
                    particulas[i][j] = rnd.Next(100); // Supongamos un espacio de búsqueda de 0 a 100
                    velocidades[i][j] = 0;
                }
            }

            for (int iteracion = 0; iteracion < numIteraciones && todasLasSoluciones.Count < numMaxSoluciones; iteracion++)
            {
                for (int i = 0; i < numParticulas; i++)
                {
                    double[] posicionActual = particulas[i];
                    double[] velocidadActual = velocidades[i];

                    // Evaluar la función objetivo
                    double valorActual = FuncionObjetivo(posicionActual);

                    // Actualizar mejor posición local
                    if (valorActual < mejorValorGlobal)
                    {
                        mejorValorGlobal = valorActual;
                        Array.Copy(posicionActual, mejorPosicionGlobal, p * 2);
                    }

                    // Agregar solución actual a la lista de todas las soluciones
                    todasLasSoluciones.Add(posicionActual);

                    // Actualizar velocidad y posición
                    for (int j = 0; j < p * 2; j++)
                    {
                        double r1 = rnd.NextDouble();
                        double r2 = rnd.NextDouble();
                        velocidadActual[j] = w * velocidadActual[j] +
                                              c1 * r1 * (mejorPosicionGlobal[j] - posicionActual[j]) +
                                              c2 * r2 * (mejorPosicionGlobal[j] - posicionActual[j]);
                        posicionActual[j] += velocidadActual[j];

                        // Limitar posición a un rango válido (0 a 100)
                        if (posicionActual[j] < 0) posicionActual[j] = 0;
                        if (posicionActual[j] > 100) posicionActual[j] = 100;
                    }

                    // Actualizar partícula
                    particulas[i] = posicionActual;
                    velocidades[i] = velocidadActual;
                }
            }
        }

        static double FuncionObjetivo(double[] posicion)
        {
            // Calcular la suma de las distancias de los clientes a los hubs
            double sumaDistancias = 0;
            for (int i = 0; i < n; i++)
            {
                double distanciaMinima = double.MaxValue;
                for (int j = 0; j < p; j++)
                {
                    double distancia = Distancia(clientes[i, 1], clientes[i, 2], posicion[j * 2], posicion[j * 2 + 1]);
                    if (distancia < distanciaMinima)
                    {
                        distanciaMinima = distancia;
                    }
                }
                sumaDistancias += distanciaMinima;
            }

            // Verificar restricciones de capacidad

            return sumaDistancias;
        }

        static double Distancia(int x1, int y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        private void Pbx_Nodos_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black);

            // Verificar si mejorPosicionGlobal es nulo
            if (mejorPosicionGlobal == null)
            {
                return;
            }

            // Calcular el desplazamiento necesario para centrar el contenido
            int offsetX = (int)((Pbx_Nodos.Width / 2) / 1.75 - (mejorPosicionGlobal.Max(x => x) + mejorPosicionGlobal.Min(x => x)) / 2);
            int offsetY = (int)((Pbx_Nodos.Height / 2) / 1.75 - (mejorPosicionGlobal.Max(y => y) + mejorPosicionGlobal.Min(y => y)) / 2);

            // Escalar la matriz de transformación para aplicar un zoom del 75% y centrar el contenido
            g.TranslateTransform(offsetX, offsetY);
            g.ScaleTransform(1.75f, 1.75f);

            // Reducir el tamaño de la fuente en proporción al zoom
            Font font = new Font(Font.FontFamily, Font.Size / 1.75f); // Aquí 1.75f es el factor de escala

            // Dibujar los hubs
            for (int i = 0; i < p; i++)
            {
                int x = (int)(mejorPosicionGlobal[i * 2] * 1.75); // Aplicar el mismo zoom a las coordenadas
                int y = (int)(mejorPosicionGlobal[i * 2 + 1] * 1.75);
                g.FillEllipse(Brushes.Red, x - 5, y - 5, 10, 10);
                g.DrawString($"Hub {i + 1}", font, Brushes.Black, x + 5, y + 5);
            }

            // Dibujar los clientes y las líneas a los hubs correspondientes
            for (int i = 0; i < n; i++)
            {
                int xCliente = (int)(clientes[i, 1] * 1.75); // Aplicar el mismo zoom a las coordenadas
                int yCliente = (int)(clientes[i, 2] * 1.75);

                // Encontrar el hub más cercano
                double distanciaMinima = double.MaxValue;
                int hubAsignado = -1;
                for (int j = 0; j < p; j++)
                {
                    double distancia = Distancia(clientes[i, 1], clientes[i, 2], mejorPosicionGlobal[j * 2], mejorPosicionGlobal[j * 2 + 1]);
                    if (distancia < distanciaMinima)
                    {
                        distanciaMinima = distancia;
                        hubAsignado = j;
                    }
                }

                // Dibujar línea al hub correspondiente
                int xHub = (int)(mejorPosicionGlobal[hubAsignado * 2] * 1.75); // Aplicar el mismo zoom a las coordenadas
                int yHub = (int)(mejorPosicionGlobal[hubAsignado * 2 + 1] * 1.75);
                g.DrawLine(pen, xCliente, yCliente, xHub, yHub);

                // Dibujar el cliente
                g.FillEllipse(Brushes.Blue, xCliente - 3, yCliente - 3, 6, 6);
                g.DrawString($"Cliente {i + 1}", font, Brushes.Black, xCliente + 5, yCliente + 5);
            }
        }
    }
}