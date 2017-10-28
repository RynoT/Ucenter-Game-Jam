using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour {

    public Camera playerCamera;
    public GameObject player;

    public float moveSpeed = 1.5f;
    public Vector3 positionOffset = new Vector3(0.0f, 0.075f, 0.0f);

<<<<<<< HEAD
    public Text waitingText;
    public Image directionCursor;
    public FrustrationBar frustrationBar;

    private bool _waiting = true; 
    public bool waiting { 
        get { return _waiting; } 
        set { 
            this._waiting = value; 
            if (this.waitingText != null) {
                this.waitingText.gameObject.SetActive(value);
            } 
            if (this.directionCursor != null) {
                this.directionCursor.gameObject.SetActive(false);
            } 
        } 
    }

    public void Start() {
        this.waiting = this._waiting;
=======
    public FrustrationBar frustrationBar;

    public void Start() {
>>>>>>> f9583fce3646315743dcd176ef96bda44faccc17
    }

    public void Update() {
        if (this.player == null || this.playerCamera == null || this.frustrationBar == null)
        {
            return;
        }
        base.transform.position = this.player.transform.position + this.positionOffset;

        Quaternion current = base.transform.rotation;
        base.transform.rotation = Quaternion.FromToRotation(this.playerCamera.transform.position,
            base.transform.position) * Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));
        base.transform.RotateAround(base.transform.position, Vector3.up, this.playerCamera.transform.rotation.eulerAngles.y);

        base.transform.rotation = Quaternion.Slerp(current, base.transform.rotation, this.moveSpeed * Time.deltaTime);
<<<<<<< HEAD

        // Cursor

=======
>>>>>>> f9583fce3646315743dcd176ef96bda44faccc17
    }
}
