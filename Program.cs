using System;
using System.Collections.Generic;

namespace FrechetDistance
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //Example Curves
            List<double[]> P = new List<double[]>()
            {
                new double[] {0, 1},
                new double[] {1, 3},
                new double[] {2, 5},
                new double[] {4, 10},
                new double[] {6, 55},
                new double[] {7, 11},
                new double[] {8, -1}
            };
            List<double[]> Q = new List<double[]>()
            {
                new double[] {0, -1},
                new double[] {1, 50},
                new double[] {2, 45},
                new double[] {4, 30},
                new double[] {6, 10},
                new double[] {7, 5},
                new double[] {8, 15}
            };

            //Frechet Distance Between P and Q
            double fDistance = FrechetDistance(P, Q);

            Console.WriteLine(fDistance);
        }

        public static double EuclideanDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }

        public static double ComputeDistance(double[,] distances, int i, int j, List<double[]> P, List<double[]> Q)
        {
            if (distances[i, j] > -1)
                return distances[i, j];

            if (i == 0 && j == 0)
                distances[i, j] = EuclideanDistance(P[0][0], P[0][1], Q[0][0], Q[0][1]);
            else if (i > 0 && j == 0)
                distances[i, j] = Math.Max(ComputeDistance(distances, i - 1, 0, P, Q), 
                                           EuclideanDistance(P[i][0], P[i][1], Q[0][0], Q[0][1]));
            else if (i == 0 && j > 0)
                distances[i, j] = Math.Max(ComputeDistance(distances, 0, j - 1, P, Q), 
                                           EuclideanDistance(P[0][0], P[0][1], Q[j][0], Q[j][1]));
            else if (i > 0 && j > 0)
                distances[i, j] = Math.Max(Math.Min(ComputeDistance(distances, i - 1, j, P, Q), 
                                           Math.Min(ComputeDistance(distances, i - 1, j - 1, P, Q), 
                                                    ComputeDistance(distances, i, j - 1, P, Q))), 
                                                    EuclideanDistance(P[i][0], P[i][1], Q[j][0], Q[j][1]));
            else
                distances[i, j] = Double.PositiveInfinity;

            return distances[i, j];
        }

        public static double FrechetDistance(List<double[]> P, List<double[]> Q)
        {
            double[,] distances = new double[P.Count, Q.Count];
            for (int y = 0; y < P.Count; y++)
                for (int x = 0; x < Q.Count; x++)
                    distances[y, x] = -1;

            return ComputeDistance(distances, P.Count - 1, Q.Count - 1, P, Q);
        }
    }
}
