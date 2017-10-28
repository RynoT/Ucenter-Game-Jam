using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGrid : MonoBehaviour
{

    public enum GridItemType
    {
        UNSET = 0,
        UNKNOWN = 1,
        HOUSE = 2,
        ROAD = 3
    }

    public float gridSize = 2.0f;
    private GridPathFinder pathfinder;

    public void Start()
    {
        // Get width and height of grid
        int width = 0, height = 0;
        foreach (Transform transform in base.transform)
        {
            int x = (int)(transform.position.x / this.gridSize), 
                    y = (int)(transform.position.z / this.gridSize);
            Debug.Assert(x >= 0 && y >= 0, "Don't use negative grid tiles");
            width = Mathf.Max(x, width);
            height = Mathf.Max(y, height);
        }
        if (width == 0 && height == 0)
        {
            return;
        }

        // Get grid tile type
        GridItemType[] grid = new GridItemType[++width * ++height];
        foreach (Transform transform in base.transform)
        {
            int x = (int)(transform.position.x / this.gridSize);
            int y = (int)(transform.position.z / this.gridSize);
            int index = x + y * width;
            if (index < 0)
                continue;
            //if(grid[index] != GridItemType.UNSET)continue;
            //Debug.Assert(grid[index] == GridItemType.UNSET, "Two grid items overlap. The grid size is probably wrong.");

            GridItemType type = GridItemType.UNKNOWN;
            switch (transform.gameObject.tag)
            {
                case "House":
                    type = GridItemType.HOUSE;
                    break;
                case "Road":
                    type = GridItemType.ROAD;
                    break;
            }
            grid[index] = type;
        }

        // Initialize the pathfinder
        this.pathfinder = base.gameObject.AddComponent<GridPathFinder>();
        this.pathfinder.init(grid, width, height);
    }

    public GridPathFinder.Node[] pathfind(float x1, float y1, float x2, float y2)
    {
        if (this.pathfinder == null)
        {
            return null;
        }
        return this.pathfinder.generate(this.pathfinder.node((int)(x1 / this.gridSize), (int)(y1 / this.gridSize)), 
            this.pathfinder.node((int)(x2 / this.gridSize), (int)(y2 / this.gridSize)));
    }
}
