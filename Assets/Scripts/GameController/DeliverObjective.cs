using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverObjective : MonoBehaviour {

    private GameObject start, end;
    private int minDistance = 10;

    private CityGrid city;
    private ObjectiveWrapper objective;

	public void Start() {
        
	}

    public void init(ref CityGrid city)
    {
        this.city = city;
    }

    private GridPathFinder.Node[] NewObjective()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("DestinationPoint");
        if (this.city == null || objects.Length <= 1)
        {
            return null;
        }
        this.start = objects[Random.Range(0, objects.Length)];
        this.end = objects[Random.Range(0, objects.Length)];
        if (this.start == this.end)
        {
            return null;
        }
        GridPathFinder.Node[] nodes = this.city.pathfind(this.start.transform.position.x,
            this.start.transform.position.z, this.end.transform.position.x, this.end.transform.position.z);
        if (nodes == null)
        {
            return null;
        }
        if (nodes.Length < this.minDistance)
        {
            return this.NewObjective();
        }
        return nodes;
    }
	
	public void Update() {
        if (this.objective == null)
        {
            GridPathFinder.Node[] nodes = this.NewObjective();
            if (nodes != null)
            {
                this.objective = new ObjectiveWrapper(nodes);
            }
        }
        else
        {
            this.objective.display(ref this.city.gridSize);
        }
	}
     
    public class ObjectiveWrapper
    {
        private GridPathFinder.Node[] path;

        public ObjectiveWrapper(GridPathFinder.Node[] path)
        {
            this.path = path;
        }

        public void display(ref float gridSize)
        {
            for (int i = 1; i < this.path.Length; i++)
            {
                GridPathFinder.Node prev = this.path[i - 1], curr = this.path[i];
                Debug.DrawLine(new Vector3(prev.x * gridSize + gridSize * 0.5f, 1.0f, prev.y * gridSize + gridSize * 0.5f), 
                    new Vector3(curr.x * gridSize + gridSize * 0.5f, 1.0f, curr.y * gridSize + gridSize * 0.5f));
            }
        }
    }
}
