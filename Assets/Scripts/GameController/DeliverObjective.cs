using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverObjective : MonoBehaviour
{
    public bool Complete = false;

    public GameObject Node, Passenger;
    private readonly int _minDistance = 10;

    private CityGrid _city;
    private GameObject _player;
    private ObjectiveWrapper _objective;

    private int _stage = 0; // 0 = pickup, 1 = drop off
    private float _counter = 0.0f;

    public void Init(ref CityGrid city, ref GameObject player)
    {
        this._city = city;
        this._player = player;
    }

    private GridPathFinder.Node[] NewObjective()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("DestinationPoint");
        if (this._city == null || this._player == null || objects.Length <= 1)
        {
            return null;
        }
        this.Node = objects[Random.Range(0, objects.Length)];

        GridPathFinder.Node[] nodes = this._city.pathfind(this._player.transform.position.x,
            this._player.transform.position.z, this.Node.transform.position.x, this.Node.transform.position.z);
        if (nodes == null)
        {
            return null;
        }
        if (nodes.Length < this._minDistance)
        {
            return null;
        }
        return nodes;
    }

    private void RenewObjective()
    {
        if (this._objective == null || this._player == null)
        {
            return;
        }
        GridPathFinder.Node[] nodes = this._city.pathfind(this._player.transform.position.x,
            this._player.transform.position.z, this.Node.transform.position.x, this.Node.transform.position.z);
        if (nodes != null)
        {
            this._objective.path = nodes;
        }
    }

    public void Update()
    {
        if (this._objective == null)
        {
            GridPathFinder.Node[] nodes = this.NewObjective();
            if (nodes != null)
            {
                this._objective = new ObjectiveWrapper(nodes);

                this.Passenger = GameObject.FindGameObjectWithTag("Passenger");
                if (this.Passenger != null)
                {
                    switch (this._stage)
                    {
                        case 0:
                            this.Passenger.transform.position = this.Node.transform.position;
                            this.Passenger.transform.rotation = Quaternion.Euler(Vector3.zero);
                            break;
                        case 1:
                            this.Passenger.transform.position = new Vector3(0.0f, -1000.0f, 0.0f);
                            break;
                        default: break;
                    }
                }
            }
        }
        else
        {
            this._objective.display(ref this._city.GridSize, this.Node.transform.position);

            this._counter += Time.deltaTime;
            if (this._counter > 2.0f)
            {
                this.RenewObjective();
                this._counter = 0.0f;
            }

            if (this._stage == 0 && this.Passenger != null)
            {
                float dist = (this._player.transform.position - this.Passenger.transform.position).magnitude;
                if (dist < 0.6f)
                {
                    this._stage = 1;
                    this._objective = null;
                    AkSoundEngine.PostEvent("Pick_up", gameObject);

                }
            }
            else if (this._stage == 1)
            {
                float dist = (this._player.transform.position - this.Node.transform.position).magnitude;
                if (dist < 0.6f)
                {
                    this._stage = 0;
                    this._objective = null;
                    this.Complete = true;
                    AkSoundEngine.PostEvent("Win", gameObject);

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
                Vector3 start = new Vector3(prev.X * gridSize + gridSize * 0.5f, 0.25f, prev.Y * gridSize + gridSize * 0.5f);
                Vector3 end = new Vector3(curr.X * gridSize + gridSize * 0.5f, 0.25f, curr.Y * gridSize + gridSize * 0.5f);
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
