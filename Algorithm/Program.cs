using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kruskalmst
{



    class Edge : IComparable<Edge>
    {
        public int Source { get; set; }
        public int Destination { get; set; }
        public int Weight { get; set; }

        public Edge(int source, int destination, int weight)
        {
            Source = source;
            Destination = destination;
            Weight = weight;
        }

        // Comparison function for sorting edges by weight
        public int CompareTo(Edge other)
        {
            return this.Weight.CompareTo(other.Weight);
        }
    }

    class Graph
    {
        private int Vertices;
        private List<Edge> Edges;

        public Graph(int vertices)
        {
            Vertices = vertices;
            Edges = new List<Edge>();
        }

        public void AddEdge(int source, int destination, int weight)
        {
            Edges.Add(new Edge(source, destination, weight));
        }

        // Find the root of a node with path compression
        private int FindParent(int[] parent, int node)
        {
            if (parent[node] != node)
                parent[node] = FindParent(parent, parent[node]);

            return parent[node];
        }

        // Union two sets by rank
        private void Union(int[] parent, int[] rank, int root1, int root2)
        {
            if (rank[root1] < rank[root2])
                parent[root1] = root2;
            else if (rank[root1] > rank[root2])
                parent[root2] = root1;
            else
            {
                parent[root2] = root1;
                rank[root1]++;
            }
        }

        public void KruskalMST()
        {
            List<Edge> result = new List<Edge>();

            // Step 1: Sort all edges by weight
            Edges.Sort();

            // Initialize parent and rank arrays
            int[] parent = new int[Vertices];
            int[] rank = new int[Vertices];
            for (int i = 0; i < Vertices; i++)
            {
                parent[i] = i;
                rank[i] = 0;
            }

            // Step 2: Iterate through sorted edges and build MST
            foreach (var edge in Edges)
            {
                int root1 = FindParent(parent, edge.Source);
                int root2 = FindParent(parent, edge.Destination);

                // If including this edge does not form a cycle
                if (root1 != root2)
                {
                    result.Add(edge);
                    Union(parent, rank, root1, root2);
                }
            }

            // Print the MST
            Console.WriteLine("Edges in the Minimum Spanning Tree:");
            foreach (var edge in result)
            {
                Console.WriteLine($"{edge.Source} -- {edge.Destination} == {edge.Weight}");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            int vertices = 4;  // Number of vertices
            Graph graph = new Graph(vertices);

            // Add edges to the graph
            graph.AddEdge(3, 1, 10);
            graph.AddEdge(0, 2, 16);
            graph.AddEdge(2, 3, 25);
            graph.AddEdge(1, 3, 5);
            graph.AddEdge(2, 3, 14);

            // Find the MST using Kruskal's algorithm
            graph.KruskalMST();
        }
    }
}

