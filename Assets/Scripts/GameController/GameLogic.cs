using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {
   
    public CityGrid City;
    public GameObject Player;
    public CanvasScript Canvas;

    private float _objectiveTimer = 0.0f;
    private readonly float _objectiveMinDelay = 1.0f, _objectiveMaxDelay = 10.0f;
    private DeliverObjective _objective;
	
	public void FixedUpdate() {
        if (this.City == null || this.Player == null || this.Canvas == null)
        {
            return;
        }
        if (this._objective == null)
        {
            this._objectiveTimer += Time.deltaTime;
            if (this._objectiveTimer >= this._objectiveMinDelay && (this._objectiveTimer >= this._objectiveMaxDelay || Random.Range(0.0f, 1.0f) < 0.01f))
            {
                this.Canvas.Waiting = false;
                this._objective = base.gameObject.AddComponent<DeliverObjective>();
                this._objective.Init(ref this.City, ref this.Player);
            }
        }
        else
        {
            if (this._objective.Complete)
            {
                Destroy(this._objective);
                this._objective = null;
                this._objectiveTimer = 0.0f;
                this.Canvas.Waiting = true;
                this.Canvas.FrustrationBar.Percentage = 0.0f;
            }
        }
	}
}
