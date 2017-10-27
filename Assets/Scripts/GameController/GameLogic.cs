using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

    public CityGrid city;
    public GameObject player;

    private DeliverObjective objective;

    public void Start() {
	}
	
	public void Update() {
        if (this.city == null || this.player == null)
        {
            return;
        }
        if (this.objective == null)
        {
            this.objective = base.gameObject.AddComponent<DeliverObjective>();
            this.objective.init(ref this.city);
        }
	}
}
