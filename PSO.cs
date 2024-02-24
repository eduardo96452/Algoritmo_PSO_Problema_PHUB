using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Algoritmo_PSO_Problema_PHUB
{
    internal class PSO
    {
        // Definir constantes del PSO
        private const int NumParticulas = 20;
        private const double C1 = 2.0;
        private const double C2 = 2.0;
        private const double Inercia = 1.0;

        // Definir campos
        private List<Nodo> nodos;
        private double[][] posiciones;
        private double[][] velocidades;
        private double[][] mejoresPosiciones;
        private double[] mejorGlobal;
        private double mejorFitnessGlobal;
        private Random random;
        private TextBox textBoxResult;
        private TextBox textHubs;
        private PictureBox pictureBox;

        public PSO(List<Nodo> nodos, TextBox textBoxResult, TextBox textHubs, PictureBox pictureBox)
        {
            this.nodos = nodos;
            this.textBoxResult = textBoxResult;
            this.textHubs = textHubs;
            random = new Random();
            Inicializar();
            this.pictureBox = pictureBox;
        }

        private void Inicializar()
        {
            posiciones = new double[NumParticulas][];
            velocidades = new double[NumParticulas][];
            mejoresPosiciones = new double[NumParticulas][];
            mejorGlobal = new double[NumParticulas];
            mejorFitnessGlobal = double.MaxValue;

            // Obtener el número de hubs disponibles
            int numHubsDisponibles = nodos.Count < NumParticulas ? nodos.Count : NumParticulas;

            // Inicializar posiciones y velocidades aleatoriamente
            for (int i = 0; i < NumParticulas; i++)
            {
                posiciones[i] = GenerarSolucionAleatoria(numHubsDisponibles); // Generar una solución aleatoria para cada partícula
                velocidades[i] = new double[nodos.Count];
                mejoresPosiciones[i] = new double[nodos.Count];

                for (int j = 0; j < nodos.Count; j++)
                {
                    // Inicializar velocidad aleatoria entre -0.5 y 0.5
                    velocidades[i][j] = random.NextDouble() - 0.5;
                    mejoresPosiciones[i][j] = posiciones[i][j];
                }
            }
        }

        private double[] GenerarSolucionAleatoria(int numHubsDisponibles)
        {
            // Generar una solución aleatoria donde cada posición representa la asignación de un cliente al hub más cercano
            double[] solucionAleatoria = new double[nodos.Count];
            for (int i = 0; i < nodos.Count; i++)
            {
                double distanciaMinima = double.MaxValue;
                int hubMasCercano = -1;

                // Calcular la distancia entre el cliente actual y cada uno de los hubs disponibles
                for (int j = 0; j < numHubsDisponibles; j++)
                {
                    double distancia = CalcularDistancia(nodos[i], nodos[j]);
                    if (distancia < distanciaMinima)
                    {
                        distanciaMinima = distancia;
                        hubMasCercano = j;
                    }
                }

                // Asignar el cliente al hub más cercano
                solucionAleatoria[i] = hubMasCercano;
            }
            return solucionAleatoria;
        }

        private double CalcularDistancia(Nodo cliente, Nodo hub)
        {
            // Calcular la distancia euclidiana entre dos nodos
            double dx = cliente.CoordenadaX - hub.CoordenadaX;
            double dy = cliente.CoordenadaY - hub.CoordenadaY;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public void EjecutarPSO()
        {
            // Definir criterio de parada (por ejemplo, número de iteraciones)
            int numIteraciones = 100;
            for (int iteracion = 0; iteracion < numIteraciones; iteracion++)
            {
                // Actualizar las posiciones y velocidades de las partículas
                for (int i = 0; i < NumParticulas; i++)
                {
                    for (int j = 0; j < nodos.Count; j++)
                    {
                        // Actualizar velocidad
                        double r1 = random.NextDouble();
                        double r2 = random.NextDouble();
                        velocidades[i][j] = Inercia * velocidades[i][j] +
                                            C1 * r1 * (mejoresPosiciones[i][j] - posiciones[i][j]) +
                                            C2 * r2 * (mejorGlobal[j] - posiciones[i][j]);

                        // Actualizar posición
                        posiciones[i][j] += velocidades[i][j];

                        // Limitar las posiciones dentro del rango [0, 1]
                        if (posiciones[i][j] < 0)
                            posiciones[i][j] = 0;
                        else if (posiciones[i][j] > 1)
                            posiciones[i][j] = 1;
                    }
                }

                // Calcular el fitness de cada partícula y actualizar el mejor global
                for (int i = 0; i < NumParticulas; i++)
                {
                    double fitness = CalcularFitness(posiciones[i]);
                    if (fitness < mejorFitnessGlobal)
                    {
                        mejorFitnessGlobal = fitness;
                        Array.Copy(posiciones[i], mejorGlobal, nodos.Count);
                    }
                }
            }

            // Construir la cadena con los resultados
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("El mejor valor es:" + mejorFitnessGlobal);
            sb.AppendLine("Resultados:");
            HashSet<int> hubsUtilizados = new HashSet<int>();
            for (int i = 0; i < nodos.Count; i++)
            {
                int hub = (int)mejorGlobal[i];
                hubsUtilizados.Add(hub);
                sb.AppendLine($"Cliente {nodos[i].Numero}: ({nodos[i].CoordenadaX}, {nodos[i].CoordenadaY}) asignado al hub {hub}");
            }

            // Mostrar los resultados en el TextBox
            textBoxResult.Text = sb.ToString();

            // Mostrar las coordenadas de los hubs utilizados
            StringBuilder sbHubs = new StringBuilder();
            sbHubs.AppendLine("Coordenadas de los hubs utilizados:");
            foreach (var hubIndex in hubsUtilizados)
            {
                Nodo hub = nodos[hubIndex];
                sbHubs.AppendLine($"Hub {hubIndex}: ({hub.CoordenadaX}, {hub.CoordenadaY})");
            }

            // Mostrar las coordenadas de los hubs utilizados en la consola
            textHubs.Text = sbHubs.ToString();

            // Llamar al método para graficar los nodos
            GraficarNodos(nodos, hubsUtilizados, pictureBox);
        }

        private void GraficarNodos(List<Nodo> nodos, HashSet<int> hubsUtilizados, PictureBox pictureBox)
        {
            // Encontrar las coordenadas máximas y mínimas en ambos ejes
            double maxX = nodos.Max(n => n.CoordenadaX);
            double maxY = nodos.Max(n => n.CoordenadaY);
            double minX = nodos.Min(n => n.CoordenadaX);
            double minY = nodos.Min(n => n.CoordenadaY);

            // Calcular el tamaño del plano cartesiano según las coordenadas máximas y mínimas
            int offsetX = 50; // Desplazamiento horizontal
            int offsetY = 50; // Desplazamiento vertical
            int bitmapWidth = (int)(maxX - minX) + offsetX * 2;
            int bitmapHeight = (int)(maxY - minY) + offsetY * 2;

            // Crear un nuevo Bitmap para dibujar los nodos y las líneas
            Bitmap bitmap = new Bitmap(bitmapWidth, bitmapHeight);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Dibujar las líneas que conectan los clientes con los hubs
                foreach (Nodo cliente in nodos)
                {
                    // Coordenadas del cliente en el PictureBox con desplazamiento
                    int xCliente = (int)(cliente.CoordenadaX - minX) + offsetX;
                    int yCliente = (int)(cliente.CoordenadaY - minY) + offsetY;

                    // Buscar el hub al que está asignado el cliente
                    int hubIndex = (int)mejorGlobal[nodos.IndexOf(cliente)];
                    Nodo hub = nodos[hubIndex];

                    // Coordenadas del hub en el PictureBox con desplazamiento
                    int xHub = (int)(hub.CoordenadaX - minX) + offsetX;
                    int yHub = (int)(hub.CoordenadaY - minY) + offsetY;

                    // Dibujar una línea que conecta el cliente con el hub
                    g.DrawLine(Pens.Gray, xCliente, yCliente, xHub, yHub);
                }

                // Dibujar los clientes
                foreach (Nodo cliente in nodos)
                {
                    // Coordenadas del cliente en el PictureBox con desplazamiento
                    int x = (int)(cliente.CoordenadaX - minX) + offsetX;
                    int y = (int)(cliente.CoordenadaY - minY) + offsetY;
                    // Dibujar un círculo para representar el cliente
                    g.FillEllipse(Brushes.Blue, x - 5, y - 5, 10, 10);
                }

                // Dibujar los hubs utilizados
                foreach (int hubIndex in hubsUtilizados)
                {
                    Nodo hub = nodos[hubIndex];
                    // Coordenadas del hub en el PictureBox con desplazamiento
                    int x = (int)(hub.CoordenadaX - minX) + offsetX;
                    int y = (int)(hub.CoordenadaY - minY) + offsetY;
                    // Dibujar un círculo más grande para representar el hub
                    g.FillEllipse(Brushes.Red, x - 8, y - 8, 16, 16);
                }

                // Dibujar el plano cartesiano en el lado derecho superior del dibujo
                using (Pen pen = new Pen(Color.Black))
                {
                    // Dibujar eje x
                    g.DrawLine(pen, offsetX, offsetY, offsetX + bitmapWidth, offsetY);
                    // Dibujar etiqueta de eje x
                    g.DrawString("X", new Font("Arial", 8), Brushes.Black, offsetX + bitmapWidth - 15, offsetY - 15);

                    // Dibujar eje y
                    g.DrawLine(pen, offsetX, offsetY, offsetX, offsetY + bitmapHeight);
                    // Dibujar etiqueta de eje y
                    g.DrawString("Y", new Font("Arial", 8), Brushes.Black, offsetX - 15, offsetY + bitmapHeight - 15);
                }
            }

            // Mostrar el bitmap en el PictureBox
            pictureBox.Image = bitmap;
        }


        private double CalcularFitness(double[] posicion)
        {
            double fitness = 0.0;

            // Crear una lista para almacenar las demandas asignadas a cada servidor
            Dictionary<double, double> demandasPorServidor = new Dictionary<double, double>();

            // Calcular la suma de las distancias de los clientes a los servidores
            for (int i = 0; i < nodos.Count; i++)
            {
                double distanciaMinima = double.MaxValue;
                int indiceServidor = (int)posicion[i]; // Obtener el índice del servidor asignado al cliente

                // Calcular la distancia entre el cliente y su servidor asignado
                double distancia = CalcularDistancia(nodos[i], nodos[indiceServidor]);

                // Agregar la distancia al fitness total
                fitness += distancia;

                // Actualizar la demanda asignada al servidor
                if (!demandasPorServidor.ContainsKey(indiceServidor))
                {
                    demandasPorServidor.Add(indiceServidor, 0);
                }
                demandasPorServidor[indiceServidor] += nodos[i].Demanda;

                // Verificar si la capacidad del servidor se excede
                if (demandasPorServidor[indiceServidor] > nodos[indiceServidor].Demanda)
                {
                    // Penalizar el fitness si la capacidad del servidor se excede
                    fitness += (demandasPorServidor[indiceServidor] - nodos[indiceServidor].Demanda) * 1000; // Ajusta el factor de penalización según sea necesario
                }
            }

            return fitness;
        }

    }
}
