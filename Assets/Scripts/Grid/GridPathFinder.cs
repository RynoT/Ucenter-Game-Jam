using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPathFinder : MonoBehaviour {

    private int width, height;
    private CityGrid.GridItemType[] grid;

    public class Node 
    {
        public int x, y;
        public CityGrid.GridItemType type;

        public Node parent;
        public float f = 0.0f, g = 0.0f, h = 0.0f;

        public Node(int x, int y, ref CityGrid.GridItemType type) 
        {
            this.x = x;
            this.y = y;
            this.type = type;
        }

        public Node init(ref Node to, ref Node parent)
        {
            this.h = this.heuristic(ref to.x, ref to.y);
            this.g = (int)this.type + parent.g;
            this.f = this.g + this.h;
            this.parent = parent;
            return this;
        }

        private float heuristic(ref int x, ref int y)
        {
            return Mathf.Pow(this.x - x, 2.0f) + Mathf.Pow(this.y - y, 2.0f);
        }
    }

    public void init(CityGrid.GridItemType[] grid, int width, int height)
    {
        this.grid = grid;
        this.width = width;
        this.height = height;
    }

    private Node[] trace(ref Node end) {
        if (end == null || end.parent == null)
        {
            return null;
        }
        List<Node> path = new List<Node>();
        Node current = end;
        while (current.parent != null)
        {
            path.Add(current);
            current = current.parent;
        }
        return path.ToArray();
    }
   
    public Node[] generate(ref Node from, ref Node to)
    {
        int[][] offsets = new int[][]{ new int[]{ 1, 0 }, new int[]{ -1, 0 }, 
            new int[]{ 0, 1 }, new int[]{ 0, -1 } };

        List<Node> open = new List<Node>(), closed = new List<Node>();
        open.Add(from);

        while (open.Count > 0)
        {
            Node q = null;
            foreach(Node next in open)
            {
                if (q == null || next.f < q.f)
                {
                    q = next;
                }
            }
            Debug.Assert(q != null);
            closed.Add(q);
            open.Remove(q);
            foreach (int[] offset in offsets)
            {
                int newX = q.x + offset[0], newY = q.y + offset[1];
                if(newX == to.x && newY == to.y)
                {
                    to.parent = q;
                    return this.trace(ref to);
                }
                Node match = closed.Find(delegate(Node obj) { return obj.x == newX && obj.y == newY; });
                //Node match = from node in closed where (node.x == newX && node.y == newY) select node;
                if (match != null)
                {
                    continue;
                }
                match = open.Find(delegate(Node obj) { return obj.x == newX && obj.y == newY; });
                if (match == null)
                {
                    open.Add(new Node(newX, newY, ref this.grid[newY + newX * this.width]).init(ref to, ref q));
                }
                else
                {
                    float newG = q.g + (int)this.grid[newX + newY * this.width];
                    if (newG + q.h < match.f)
                    {
                        match.g = newG;
                        match.parent = q;
                    }
                }
            }

        }
        if (to.parent == null)
        {
            foreach (Node next in closed)
            {
                if (to.parent == null || next.f < to.parent.f)
                {
                    to.parent = next;
                }
            }
        }
        return this.trace(ref to);
    }
}
