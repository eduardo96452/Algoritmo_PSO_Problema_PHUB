using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Algoritmo_PSO_Problema_PHUB
{
    internal class Swarm
    {
        public List<Nodo> Nodos { get; set; }
        public int NumberOfServers { get; set; }
        public double ServerCapacity { get; set; }
        public int Iteraciones { get; set; }
        public Swarm()
        {
            Nodos = new List<Nodo>();
        }

        // Este método genera una solución aleatoria
        public List<Nodo> GenerateRandomSolution()
        {
            var random = new Random();
            var servers = Nodos.OrderBy(x => random.Next()).Take(NumberOfServers).ToList();
            return servers;
        }


        //Metodo Principal
        // Este método genera 1000 soluciones aleatorias y devuelve la mejor
        public List<Nodo> GenerateRandomSolutions(Form1 form)
        {
            List<Nodo> bestSolution = null;
            double bestObjectiveValue = double.MaxValue;

            for (int i = 0; i < Iteraciones; i++)
            {
                var solution = GenerateRandomSolution();
                var objectiveValue = CalculateObjectiveValue(solution);

                if (objectiveValue < bestObjectiveValue)
                {
                    bestSolution = solution;
                    bestObjectiveValue = objectiveValue;
                    //form.TxtResults.AppendText($"Iteración {i + 1}: Mejor valor objetivo = {bestObjectiveValue}\r\n");
                }
            }

            return bestSolution;
        }

        // Este método calcula el valor objetivo de una solución
        public double CalculateObjectiveValue(List<Nodo> solution)
        {
            double totalDistance = 0;

            foreach (var server in solution)
            {
                foreach (var nodo in Nodos)
                {
                    if (!solution.Contains(nodo))
                    {
                        totalDistance += Math.Sqrt(Math.Pow(server.CoordenadaX - nodo.CoordenadaX, 2) + Math.Pow(server.CoordenadaY - nodo.CoordenadaY, 2));
                    }
                }
            }

            return totalDistance;
        }
    }
}
