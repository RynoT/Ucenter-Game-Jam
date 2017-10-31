using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGrid : MonoBehaviour
{

    public enum GridItemType
    {
        Unset = 0,
        Unknown = 1,
        House = 2,
        Road = 3
    }

    public float GridSize = 2.0f;
    private GridPathFinder _pathfinder;

    public void Start()
    {
        // Get width and height of grid
        int width = 0, height = 0;
        foreach (Transform transform in base.transform)
        {
            int x = (int)(transform.position.x / this.GridSize),
                    y = (int)(transform.position.z / this.GridSize);
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
            int x = (int)(transform.position.x / this.GridSize);
            int y = (int)(transform.position.z / this.GridSize);
            int index = x + y * width;
            if (index < 0)
                continue;
            //if(grid[index] != GridItemType.UNSET)continue;
            //Debug.Assert(grid[index] == GridItemType.UNSET, "Two grid items overlap. The grid size is probably wrong.");

            GridItemType type = GridItemType.Unknown;
            switch (transform.gameObject.tag)
            {
                case "House":
                    type = GridItemType.House;
                    break;
                case "Road":
                    type = GridItemType.Road;
                    break;
                default: break;
            }
            grid[index] = type;
        }

        // Initialize the pathfinder
        this._pathfinder = base.gameObject.AddComponent<GridPathFinder>();
        this._pathfinder.Init(grid, width, height);
    }

    public GridPathFinder.Node[] pathfind(float x1, float y1, float x2, float y2)
    {
        if (this._pathfinder == null)
        {
            return null;
        }
        return this._pathfinder.Generate(this._pathfinder.node((int)(x1 / this.GridSize), (int)(y1 / this.GridSize)),
            this._pathfinder.node((int)(x2 / this.GridSize), (int)(y2 / this.GridSize)));
    }
}
