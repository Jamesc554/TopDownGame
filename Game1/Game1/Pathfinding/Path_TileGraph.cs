﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Path_TileGraph
    {
        public Dictionary<Tile, Path_Node<Tile>> nodes;

        public Path_TileGraph(World world)
        {
            nodes = new Dictionary<Tile, Path_Node<Tile>>();

            for(int x = 0; x < world.size.X; x++)
            {
                for (int y = 0; y < world.size.Y; y++)
                {
                    Tile t = world.GetTileAt(x, y);
                    Path_Node<Tile> n = new Path_Node<Tile>();
                    n.data = t;
                    nodes.Add(t, n);
                }
            }
        }

        public void RegenerateGraphAtTile(Tile changedTile)
        {
            GenerateEdgesByTile(changedTile);
            foreach(Tile tile in changedTile.GetNeighbours())
            {
                GenerateEdgesByTile(tile);
            }
        }

        private bool IsClippingCorner(Tile curr, Tile neigh)
        {
            int dX = (int)curr.position.X - (int)neigh.position.X;
            int dY = (int)curr.position.Y - (int)neigh.position.Y;

            if (Math.Abs(dX) + Math.Abs(dY) == 2)
            {
                if (WorldController.instance.world.GetTileAt((int)curr.position.X - dX, (int)curr.position.Y).movementCost == 0)
                {
                    return true;
                }

                if (WorldController.instance.world.GetTileAt((int)curr.position.X, (int)curr.position.Y - dY).movementCost == 0)
                {
                    return true;
                }
            }

            return false;
        }

        private void GenerateEdgesByTile(Tile t)
        {
            Path_Node<Tile> n = nodes[t];
            List<Path_Edge<Tile>> edges = new List<Path_Edge<Tile>>();
            Tile[] neighbours = t.GetNeighbours();

            for (int i = 0; i < neighbours.Length; i++)
            {
                if (neighbours[i] != null && neighbours[i].movementCost > 0 && IsClippingCorner(t, neighbours[i]) == false)
                {
                    Path_Edge<Tile> e = new Path_Edge<Tile>();
                    e.cost = neighbours[i].movementCost;
                    e.node = nodes[neighbours[i]];

                    edges.Add(e);
                }
            }
            n.edges = edges.ToArray();
        }
    }
}