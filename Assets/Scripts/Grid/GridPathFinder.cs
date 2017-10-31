using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPathFinder : MonoBehaviour
{

    private int _width, _height;
    private CityGrid.GridItemType[] _grid;

    public class Node
    {
        public int X, Y;
        public CityGrid.GridItemType Type;

        public Node Parent;
        public float F = 0.0f, G = 0.0f, H = 0.0f;

        public Node(int x, int y, ref CityGrid.GridItemType type)
        {
            this.X = x;
            this.Y = y;
            this.Type = type;
        }

        public Node Init(ref Node to, ref Node parent)
        {
            this.H = this.Heuristic(ref to.X, ref to.Y);
            this.G = (int)this.Type + parent.G;
            this.F = this.G + this.H;
            this.Parent = parent;
            return this;
        }

        private float Heuristic(ref int x, ref int y)
        {
            return Mathf.Pow(this.X - x, 2.0f) + Mathf.Pow(this.Y - y, 2.0f);
        }

    }

    public void Init(CityGrid.GridItemType[] grid, int width, int height)
    {
        this._grid = grid;
        this._width = width;
        this._height = height;
    }

    public Node node(int x, int y)
    {
        return new Node(x, y, ref this._grid[x + y * this._width]);
    }

    private Node[] trace(ref Node end)
    {
        if (end == null || end.Parent == null)
        {
            return null;
        }
        List<Node> path = new List<Node>();
        Node current = end;
        while (current.Parent != null)
        {
            path.Add(current);
            current = current.Parent;
        }
        path.Add(current);
        return path.ToArray();
    }

    public Node[] Generate(Node from, Node to)
    {
        int[][] offsets = new int[][]{ new int[]{ 1, 0 }, new int[]{ -1, 0 },
            new int[]{ 0, 1 }, new int[]{ 0, -1 } };

        List<Node> open = new List<Node>(), closed = new List<Node>();
        open.Add(from);

        while (open.Count > 0)
        {
            Node q = null;
            foreach (Node next in open)
            {
                if (q == null || next.F < q.F)
                {
                    q = next;
                }
            }
            Debug.Assert(q != null);
            closed.Add(q);
            open.Remove(q);
            foreach (int[] offset in offsets)
            {
                int newX = q.X + offset[0], newY = q.Y + offset[1];
                if (newX == to.X && newY == to.Y)
                {
                    to.Parent = q;
                    return this.trace(ref to);
                }
                if (newX < 0 || newX >= this._width || newY < 0 || newY >= this._height)
                {
                    continue;
                }
                CityGrid.GridItemType type = this._grid[newX + newY * this._width];
                if (type != CityGrid.GridItemType.Road)
                {
                    continue;
                }

                Node match = closed.Find(obj => obj.X == newX && obj.Y == newY);
                //Node match = from node in closed where (node.x == newX && node.y == newY) select node;
                if (match != null)
                {
                    continue;
                }
                match = open.Find(obj => obj.X == newX && obj.Y == newY);
                if (match == null)
                {
                    open.Add(new Node(newX, newY, ref type).Init(ref to, ref q));
                }
                else
                {
                    float newG = q.G + (int)type;
                    if (newG + q.H < match.F)
                    {
                        match.G = newG;
                        match.Parent = q;
                    }
                }
            }

        }
        if (to.Parent == null)
        {
            foreach (Node next in closed)
            {
                if (to.Parent == null || next.F < to.Parent.F)
                {
                    to.Parent = next;
                }
            }
        }
        return this.trace(ref to);
    }
}
