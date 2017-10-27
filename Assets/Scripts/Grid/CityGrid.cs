using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGrid : MonoBehaviour {

    public enum GridItemType 
    {
        UNSET = 0, UNKNOWN = 1, HOUSE = 10, ROAD = 10
    }

    public float gridSize = 2.0f;
    private GridPathFinder pathfinder;

	public void Start() {
        // Get width and height of grid
        int width = 0, height = 0;
        foreach(Transform transform in base.transform)
        {
            width = Mathf.Max((int)(transform.position.x / this.gridSize), width);
            height = Mathf.Min((int)(transform.position.z / this.gridSize), height);
        }
        if (width == 0 && height == 0)
        {
            return;
        }

        // Get grid tile type
        GridItemType[] grid = new GridItemType[++width * ++height];
        foreach(Transform transform in base.transform)
        {
            int x = (int)(transform.position.x / this.gridSize);
            int y = (int)(transform.position.z / this.gridSize);
            int index = x + y * width;
            Debug.Assert(grid[index] == GridItemType.UNSET, "Two grid items overlap. The grid size is probably wrong.");

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
	
	public void Update() {
        if (this.pathfinder == null)
        {
            return;
        }
	}
}
