using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {
   
    public CityGrid city;
    public GameObject player;
    public CanvasScript canvas;

    private float objectiveTimer = 0.0f, objectiveMinDelay = 1.0f, objectiveMaxDelay = 10.0f;
    private DeliverObjective objective;
	
	public void FixedUpdate() {
        if (this.city == null || this.player == null || this.canvas == null)
        {
            return;
        }
        if (this.objective == null)
        {
            this.objectiveTimer += Time.deltaTime;
            if (this.objectiveTimer >= this.objectiveMinDelay && (this.objectiveTimer >= this.objectiveMaxDelay || Random.Range(0.0f, 1.0f) < 0.01f))
            {
                this.canvas.waiting = false;
                this.objective = base.gameObject.AddComponent<DeliverObjective>();
                this.objective.init(ref this.city, ref this.player);
            }
        }
        else
        {
            if (this.objective.complete)
            {
                Destroy(this.objective);
                this.objective = null;
                this.objectiveTimer = 0.0f;
                this.canvas.waiting = true;
                this.canvas.frustrationBar.percentage = 0.0f;
            }
        }
	}
}
