using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Constraints;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PathFinder
{
    public class PathNode
    {
        public int cost;
        public int G;
        public double H;
        public double F;
        public Vector3 Position;
        public PathNode PrevPathNode;

        public PathNode(Vector3 pos, PathNode prev = null)
        {
            Position = pos;
            PrevPathNode = prev;
        }
    }

    private HashSet<Vector3> _movementTiles;
    private Dictionary<int, TileData> _tileCosts;
    private Dictionary<Vector3, int> _tileBag;
    private Dictionary<Vector3, int> _unitBag;

    List<Vector3> cardinalDirections = new List<Vector3> {
        Vector3.left, Vector3.right, Vector3.forward, Vector3.back
    };

    public PathFinder(HashSet<Vector3> movementTiles, Dictionary<int, TileData> tileCosts, Dictionary<Vector3, int> tileBag, Dictionary<Vector3, int> unitBag)
    {
        _movementTiles = movementTiles;
        _tileCosts = tileCosts;
        _tileBag = tileBag;
        _unitBag = unitBag;
    }

    public Queue<Vector3> FindPath(Vector3 start, Vector3 end)
    {
        Dictionary<Vector3, PathNode> open = new Dictionary<Vector3, PathNode>();
        Dictionary<Vector3, PathNode> closed = new Dictionary<Vector3, PathNode>();
        Dictionary<Vector3, PathNode> openPaths;

        Vector3 currPos = start;
        while (currPos != end)
        {
            openPaths = FindOpenPaths(currPos, end, open, closed);
            foreach ((Vector3 v, PathNode p) in openPaths)
            {
                open[v] = p;
            }
            var (position, node) = FindBestPath(open);
            open.Remove(position);
            closed[position] = node;
            currPos = position;
        }

        List<Vector3> path = new List<Vector3>{end};
        PathNode cursor = open[end];
        while (cursor.Position != start)
        {
            cursor = closed[cursor.Position].PrevPathNode;
            path.Add(cursor.Position);
        }

        path.Reverse();
        return new Queue<Vector3>(path);
    }


    private Dictionary<Vector3, PathNode> FindOpenPaths(Vector3 curr, Vector3 end, Dictionary<Vector3, PathNode> open, Dictionary<Vector3, PathNode> closed)
    {
        PathNode current = open[curr];
        Dictionary<Vector3, PathNode> openPaths = new Dictionary<Vector3, PathNode>();

        foreach (var direction in cardinalDirections)
        {
            Vector3 newPosition = current.Position + direction;

            if (_movementTiles.Contains(newPosition) && !closed.ContainsKey(newPosition))
            {
                PathNode neighbor = new PathNode(newPosition); // TODO: add tile costs if they need it.. From tile costs bag...WIP !!! 

                // Calculate neighbor values
                neighbor.G = neighbor.cost + current.G;
                neighbor.H = Vector3.Distance(neighbor.Position, end);
                neighbor.F = neighbor.G + neighbor.H;
                neighbor.PrevPathNode = current;

                // TODO: Add check if unit (ally / foe)
                // Assuming the unit is an ally at this time

                if (open.ContainsKey(neighbor.Position))
                {
                    if (neighbor.F < open[neighbor.Position].F)
                    {
                        openPaths[neighbor.Position] = neighbor;
                    }
                }
                else
                {
                    openPaths[neighbor.Position] = neighbor;
                }
            }
        }
        return openPaths;
    }

    private (Vector3, PathNode) FindBestPath(Dictionary<Vector3, PathNode> open)
    {
        double smallestF = double.PositiveInfinity;
        Vector3 smallestVector = new Vector3(0, 0, 0);
        PathNode bestBoyNode = null;

        foreach ((Vector3 vector, PathNode node) in open)
        {
            if (node.F < smallestF)
            {
                smallestF = node.F;
                smallestVector = vector;
                bestBoyNode = node;
            }
        }

        return (smallestVector, bestBoyNode);

    }
    
}