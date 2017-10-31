using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{

    public Camera PlayerCamera;
    public GameObject Player;

    public float MoveSpeed = 1.5f;
    public Vector3 PositionOffset = new Vector3(0.0f, 0.075f, 0.0f);

    public Text WaitingText;
    public Image DirectionCursor;
    public FrustrationBar FrustrationBar;

    private bool _waiting = true;
    public bool Waiting
    {
        get { return _waiting; }
        set
        {
            this._waiting = value;
            if (this.WaitingText != null)
            {
                this.WaitingText.gameObject.SetActive(value);
            }
            if (this.DirectionCursor != null)
            {
                this.DirectionCursor.gameObject.SetActive(false);
            }
        }
    }

    public void Start()
    {
        this.Waiting = this._waiting;
    }

    public void Update()
    {
        if (this.Player == null || this.PlayerCamera == null || this.FrustrationBar == null)
        {
            return;
        }
        base.transform.position = this.Player.transform.position + this.PositionOffset;

        Quaternion current = base.transform.rotation;
        base.transform.rotation = Quaternion.FromToRotation(this.PlayerCamera.transform.position,
            base.transform.position) * Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));
        base.transform.RotateAround(base.transform.position, Vector3.up, this.PlayerCamera.transform.rotation.eulerAngles.y);

        base.transform.rotation = Quaternion.Slerp(current, base.transform.rotation, this.MoveSpeed * Time.deltaTime);
    }
}
