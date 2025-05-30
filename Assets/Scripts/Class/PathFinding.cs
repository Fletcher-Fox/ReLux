using System;
using System.Collections.Generic;
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

        Queue<Vector3> path = new Queue<Vector3>();

        Dictionary<Vector3, PathNode> openSet = new Dictionary<Vector3, PathNode>();
        Dictionary<Vector3, PathNode> closedSet = new Dictionary<Vector3, PathNode>();

        Vector3 curVector = start;
        while (curVector != end)
        {
            FindOpenPaths(curVector, end, openSet, closedSet);
        }

        // Dictionary<Vector3, Space> OpenList = new Dictionary<Vector3, Space>();
        // Dictionary<Vector3, Space> ClosedList  = new Dictionary<Vector3, Space>();

        // Vector3 currentPos = start;
        // while (currentPos != end)
        // {
        //     FindOpenPaths(currentPos, end, OpenList, ClosedList);
        //     var (vector, space) = FindBestPath(OpenList); 
        //     OpenList.Remove(vector);
        //     ClosedList[vector] = space;
        //     currentPos = vector;
        // }

        // List<Space> path = new List<Space>{MapData[end]};
        // Space cursor = MapData[end];

        // while (cursor.SpaceGameObject.transform.position != start)
        // {
        //     cursor = ClosedList[cursor.SpaceGameObject.transform.position].Prev;
        //     path.Add(cursor);
        // }

        // path.Reverse();
        // return new Queue<Space>(path);


        return path; // TODO: return the path
    }




    private void FindOpenPaths(Vector3 current, Vector3 end, Dictionary<Vector3, PathNode> open, Dictionary<Vector3, PathNode> closed)
    {

        PathNode currentNode = new PathNode(current);

        foreach (var direction in cardinalDirections)
        {
            Vector3 newLocation = current + direction;

            if (_movementTiles.Contains(newLocation) && !closed.ContainsKey(newLocation))
            {
                PathNode neighbor = new PathNode(newLocation);
                if (_unitBag.ContainsKey(neighbor.Position)) // tile is occupied by a unit
                {
                    // TODO: Add check if unit is a ally
                    // Assuming the unit is an ally at this time

                    int G = neighbor.cost + currentNode.G;
                    double H = Math.Sqrt(Math.Pow(MapData[end].Position.x - newX, 2) + Math.Pow(MapData[end].Position.z - newZ, 2));
                    double F = G + H;

                    if (open.ContainsKey(newLocation))
                    {
                        if (F < open[newLocation].F)
                        {
                            neighbor.G = G;
                            neighbor.H = H;
                            neighbor.F = F;
                            neighbor.Prev = MapData[current];
                            Open[newLocation] = neighbor;
                        }
                    }
                    else
                    {
                        neighbor.G = G;
                        neighbor.H = H;
                        neighbor.F = F;
                        neighbor.Prev = MapData[current];
                        Open[newLocation] = neighbor;
                    }

                    
                }
            }
        }
    }
}

            // if (MapData.ContainsKey(newLocation) && !Closed.ContainsKey(newLocation) && !WallData.ContainsKey(wallLocation))
            // {
            // Space neighbor = MapData[newLocation];

    //         if (neighbor.State != SpaceState.Block)
    //         {
    //             if (neighbor.State == SpaceState.Occupied)
    //             {
    //                 if (neighbor.unit.Type == "hero")
    //                 {
    //                     int G = neighbor.Cost + MapData[current].G;
    //                     double H = Math.Sqrt(Math.Pow(MapData[end].Position.x - newX, 2) + Math.Pow(MapData[end].Position.z - newZ, 2));
    //                     double F = G + H;

    //                     if (Open.ContainsKey(newLocation))
    //                     {
    //                         if (F < Open[newLocation].F)
    //                         {
    //                             neighbor.G = G;
    //                             neighbor.H = H;
    //                             neighbor.F = F;
    //                             neighbor.Prev = MapData[current];
    //                             Open[newLocation] = neighbor;
    //                         }
    //                     }
    //                     else
    //                     {
    //                         neighbor.G = G;
    //                         neighbor.H = H;
    //                         neighbor.F = F;
    //                         neighbor.Prev = MapData[current];
    //                         Open[newLocation] = neighbor;
    //                     }
    //                 }
    //             }
    //             else
    //             {
    //                 int G = neighbor.Cost + MapData[current].G;
    //                 double H = Math.Sqrt(Math.Pow(MapData[end].Position.x - newX, 2) + Math.Pow(MapData[end].Position.z - newZ, 2));
    //                 double F = G + H;

    //                 if (Open.ContainsKey(newLocation))
    //                 {
    //                     if (F < Open[newLocation].F)
    //                     {
    //                         neighbor.G = G;
    //                         neighbor.H = H;
    //                         neighbor.F = F;
    //                         neighbor.Prev = MapData[current];
    //                         Open[newLocation] = neighbor;
    //                     }
    //                 }
    //                 else
    //                 {
    //                     neighbor.G = G;
    //                     neighbor.H = H;
    //                     neighbor.F = F;
    //                     neighbor.Prev = MapData[current];
    //                     Open[newLocation] = neighbor;
    //                 }
    //             }
    //         }
    //         else if (neighbor.State == SpaceState.Occupied)
    //         {
    //             if (neighbor.unit.Type == "hero")
    //             {

    //                 int G = neighbor.Cost + MapData[current].G;
    //                 double H = Math.Sqrt(Math.Pow(MapData[end].Position.x - newX, 2) + Math.Pow(MapData[end].Position.z - newZ, 2));
    //                 double F = G + H;

    //                 if (Open.ContainsKey(newLocation))
    //                 {
    //                     if (F < Open[newLocation].F)
    //                     {
    //                         neighbor.G = G;
    //                         neighbor.H = H;
    //                         neighbor.F = F;
    //                         neighbor.Prev = MapData[current];
    //                         Open[newLocation] = neighbor;
    //                     }
    //                 }
    //                 else
    //                 {
    //                     neighbor.G = G;
    //                     neighbor.H = H;
    //                     neighbor.F = F;
    //                     neighbor.Prev = MapData[current];
    //                     Open[newLocation] = neighbor;
    //                 }

    //             }
    //         }
    //     }
    //     }
    // }

    // public float DistanceFormula(Vector3 current, Vector3 end)
    // {
    //     return Vector3.Distance(current, end);
    // }


// };