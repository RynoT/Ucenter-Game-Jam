using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverObjective : MonoBehaviour
{
    public bool complete = false;

    public GameObject node, passenger;
    private int minDistance = 10;

    private CityGrid city;
    private GameObject player;
    private ObjectiveWrapper objective;

    private int stage = 0; // 0 = pickup, 1 = drop off
    private float counter = 0.0f;

    public void init(ref CityGrid city, ref GameObject player)
    {
        this.city = city;
        this.player = player;
    }

    private GridPathFinder.Node[] NewObjective()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("DestinationPoint");
        if (this.city == null || this.player == null || objects.Length <= 1)
        {
            return null;
        }
        this.node = objects[Random.Range(0, objects.Length)];

        GridPathFinder.Node[] nodes = this.city.pathfind(this.player.transform.position.x,
            this.player.transform.position.z, this.node.transform.position.x, this.node.transform.position.z);
        if (nodes == null)
        {
            return null;
        }
        if (nodes.Length < this.minDistance)
        {
            return null;
        }
        return nodes;
    }

    private void RenewObjective()
    {
        if (this.objective == null || this.player == null)
        {
            return;
        }
        GridPathFinder.Node[] nodes = this.city.pathfind(this.player.transform.position.x,
            this.player.transform.position.z, this.node.transform.position.x, this.node.transform.position.z);
        if (nodes != null)
        {
            this.objective.path = nodes;
        }
    }

    public void Update()
    {
        if (this.objective == null)
        {
            GridPathFinder.Node[] nodes = this.NewObjective();
            if (nodes != null)
            {
                this.objective = new ObjectiveWrapper(nodes);

                this.passenger = GameObject.FindGameObjectWithTag("Passenger");
                if (this.passenger != null)
                {
                    switch (this.stage)
                    {
                        case 0:
                            this.passenger.transform.position = this.node.transform.position;
                            this.passenger.transform.rotation = Quaternion.Euler(Vector3.zero);
                            break;
					case 1:
						this.passenger.transform.position = new Vector3 (0.0f, -1000.0f, 0.0f);
                            break;
                    }
                }
            }
        }
        else
        {
            this.objective.display(ref this.city.gridSize, this.node.transform.position);

            this.counter += Time.deltaTime;
            if (this.counter > 2.0f)
            {
                this.RenewObjective();
                this.counter = 0.0f;
            }

            if (this.stage == 0 && this.passenger != null)
            {
                float dist = (this.player.transform.position - this.passenger.transform.position).magnitude;
                if (dist < 0.6f)
                {
                    this.stage = 1;
                    this.objective = null;
					AkSoundEngine.PostEvent ("Pick_up", gameObject);

                }
            }
            else if (this.stage == 1)
            {
                float dist = (this.player.transform.position - this.node.transform.position).magnitude;
                if (dist < 0.6f)
                {
                    this.stage = 0;
                    this.objective = null;
                    this.complete = true;
					AkSoundEngine.PostEvent ("Win", gameObject);

                }
            }
        }
    }

    public class ObjectiveWrapper
    {
        public GridPathFinder.Node[] path;

        public ObjectiveWrapper(GridPathFinder.Node[] path)
        {
            this.path = path;
        }

        public void display(ref float gridSize, Vector3 nodePos)
        {
            for (int i = 1; i < this.path.Length; i++)
            {
                GridPathFinder.Node prev = this.path[i - 1], curr = this.path[i];
                Vector3 start = new Vector3(prev.x * gridSize + gridSize * 0.5f, 0.25f, prev.y * gridSize + gridSize * 0.5f);
                Vector3 end = new Vector3(curr.x * gridSize + gridSize * 0.5f, 0.25f, curr.y * gridSize + gridSize * 0.5f);
                if (i == 1)
                {
                    start = nodePos;
                }
                DrawLine(start, end, Color.green, 0.02f);
            }
        }

        public void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.1f)
        {
            GameObject myLine = new GameObject();
            myLine.transform.position = start;
            LineRenderer lr = myLine.AddComponent<LineRenderer>();
            lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
            lr.startColor = lr.endColor = color;
            lr.startWidth = lr.endWidth = 0.05f;
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
            GameObject.Destroy(myLine, duration);
        }
    }
}
