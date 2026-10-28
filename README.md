# C# Route Optimizer

A C# console application that uses **Dijkstra's algorithm** to find the shortest delivery route between locations in a simulated city network.

## Overview

Delivery routing is a classic graph problem: locations are nodes, roads are weighted edges, and the goal is the lowest-cost path between two points. This project models a small simulated city and calculates the shortest route between locations.

## Features

* Models a city as a weighted graph of locations and connections
* Finds the shortest route between two locations using Dijkstra's algorithm
* Runs as a simple console application
* Prints step-by-step queue updates and Dijkstra edge relaxations for debugging
* Computes and displays the exact total distance and final traversal path

## Concepts Demonstrated

* Graph data structures (nodes, weighted edges)
* Dijkstra's shortest-path algorithm
* Problem decomposition and algorithmic thinking
* Clean, structured C# code

## Tech Stack

* C#
* .NET 8 / .NET Core
* Console application

## Getting Started

**Prerequisites:** .NET SDK installed.

```bash
git clone [https://github.com/NdumisoZulu053/csharp-route-optimizer.git](https://github.com/NdumisoZulu053/csharp-route-optimizer.git)
cd csharp-route-optimizer
dotnet run

## Example Output
```
--- Route Optimization System ---
[DEBUG] Initializing Dijkstra search from 'Warehouse' to 'Destination'
[DEBUG] Visiting node: Warehouse (Current shortest distance: 0 km)
[DEBUG] Relaxing edge (Warehouse -> Point A): Updated distance from 2147483647 to 4
[DEBUG] Relaxing edge (Warehouse -> Point B): Updated distance from 2147483647 to 2
[DEBUG] Visiting node: Point B (Current shortest distance: 2 km)
[DEBUG] Relaxing edge (Point B -> Point A): Updated distance from 4 to 3
[DEBUG] Relaxing edge (Point B -> Point C): Updated distance from 2147483647 to 10
[DEBUG] Relaxing edge (Point B -> Destination): Updated distance from 2147483647 to 12
[DEBUG] Visiting node: Point A (Current shortest distance: 3 km)
[DEBUG] Relaxing edge (Point A -> Point C): Updated distance from 10 to 8
[DEBUG] Visiting node: Point C (Current shortest distance: 8 km)
[DEBUG] Relaxing edge (Point C -> Destination): Updated distance from 12 to 10
[DEBUG] Visiting node: Destination (Current shortest distance: 10 km)
[DEBUG] Destination 'Destination' reached. Terminating search.

Shortest distance from Warehouse to Destination: 10 km
Route: Warehouse -> Point B -> Point A -> Point C -> Destination
```

## How It Works
1. The city is represented as a graph with weighted connections.
2. Dijkstra's algorithm repeatedly selects the unvisited location with the smallest known distance from the start.
3. It updates the distances to neighbouring locations until the destination is reached.
4. The shortest route and its total cost are returned.

## Possible Improvements
- Load the city map from a file
- Add a user menu for choosing start and end points
- Add unit tests for the algorithm
- Compare with other algorithms such as A*

## Author
Ndumiso Blessing Zulu | IT student (UNISA) | github.com/NdumisoZulu053
