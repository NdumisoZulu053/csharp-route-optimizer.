using System;
using System.Collections.Generic;

namespace RouteOptimizer
{
    class Program
    {
        static void Main(string[] args)
        {
            CityMap map = new CityMap();

            map.AddLocation("Warehouse");
            map.AddLocation("Point A");
            map.AddLocation("Point B");
            map.AddLocation("Point C");
            map.AddLocation("Destination");

            map.AddRoad("Warehouse", "Point A", 4);
            map.AddRoad("Warehouse", "Point B", 2);
            map.AddRoad("Point A", "Point C", 5);
            map.AddRoad("Point B", "Point A", 1);
            map.AddRoad("Point B", "Point C", 8);
            map.AddRoad("Point B", "Destination", 10);
            map.AddRoad("Point C", "Destination", 2);

            Console.WriteLine("--- Route Optimization System ---");
            map.FindShortestPath("Warehouse", "Destination");
        }
    }

    public class CityMap
    {
        private readonly Dictionary<string, List<Road>> _adjacencyList = new Dictionary<string, List<Road>>();

        public void AddLocation(string name)
        {
            if (!_adjacencyList.ContainsKey(name))
            {
                _adjacencyList[name] = new List<Road>();
            }
        }

        public void AddRoad(string from, string to, int distance)
        {
            _adjacencyList[from].Add(new Road(to, distance));
            _adjacencyList[to].Add(new Road(from, distance));
        }

        public void FindShortestPath(string startNode, string targetNode)
        {
            // DEBUG: Verify initial start and destination inputs
            Console.WriteLine($"[DEBUG] Initializing Dijkstra search from '{startNode}' to '{targetNode}'");

            var distances = new Dictionary<string, int>();
            var previousNodes = new Dictionary<string, string>();
            var priorityQueue = new SortedSet<(int distance, string node)>(Comparer<(int, string)>.Create((a, b) => 
                a.Item1 == b.Item1 ? string.Compare(a.Item2, b.Item2, StringComparison.Ordinal) : a.Item1.CompareTo(b.Item1)));

            foreach (var vertex in _adjacencyList.Keys)
            {
                if (vertex == startNode)
                {
                    distances[vertex] = 0;
                    priorityQueue.Add((0, vertex));
                }
                else
                {
                    distances[vertex] = int.MaxValue;
                    priorityQueue.Add((int.MaxValue, vertex));
                }
                previousNodes[vertex] = null;
            }

            while (priorityQueue.Count > 0)
            {
                var current = priorityQueue.Min;
                priorityQueue.Remove(current);

                // DEBUG: Trace queue extraction to ensure Dijkstra visits nodes in order of shortest distance
                Console.WriteLine($"[DEBUG] Visiting node: {current.node} (Current shortest distance: {current.distance} km)");

                if (current.node == targetNode)
                {
                    // DEBUG: Target reached successfully
                    Console.WriteLine($"[DEBUG] Destination '{targetNode}' reached. Terminating search.");
                    break;
                }
                
                if (current.distance == int.MaxValue)
                {
                    // DEBUG: Warn if unreachable node encountered
                    Console.WriteLine("[DEBUG] Remaining nodes are unreachable.");
                    break;
                }

                foreach (var neighbor in _adjacencyList[current.node])
                {
                    int alt = distances[current.node] + neighbor.Distance;
                    if (alt < distances[neighbor.Destination])
                    {
                        // DEBUG: Log edge relaxation when a shorter path to a neighbor is found
                        Console.WriteLine($"[DEBUG] Relaxing edge ({current.node} -> {neighbor.Destination}): Updated distance from {distances[neighbor.Destination]} to {alt}");

                        priorityQueue.Remove((distances[neighbor.Destination], neighbor.Destination));
                        distances[neighbor.Destination] = alt;
                        previousNodes[neighbor.Destination] = current.node;
                        priorityQueue.Add((alt, neighbor.Destination));
                    }
                }
            }

            PrintResults(startNode, targetNode, distances, previousNodes);
        }

        private void PrintResults(string start, string target, Dictionary<string, int> distances, Dictionary<string, string> previous)
        {
            if (distances[target] == int.MaxValue)
            {
                Console.WriteLine($"No path exists from {start} to {target}.");
                return;
            }

            var path = new Stack<string>();
            string current = target;
            while (current != null)
            {
                path.Push(current);
                current = previous[current];
            }

            Console.WriteLine($"\nShortest distance from {start} to {target}: {distances[target]} km");
            Console.WriteLine("Route: " + string.Join(" -> ", path));
        }
    }

    public class Road
    {
        public string Destination { get; }
        public int Distance { get; }

        public Road(string destination, int distance)
        {
            Destination = destination;
            Distance = distance;
        }
    }
}