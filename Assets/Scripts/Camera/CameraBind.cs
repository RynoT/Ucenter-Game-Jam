using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBind : MonoBehaviour
{

    public float Height;
    public Vector3 CameraSpeed;
    public float LookAtDeadZone = 2.0f;

    public GameObject Bound;

    private CarController _controller;

    public void Start()
    {
        this.SetToBound();
        if (this.Bound != null)
        {
            this._controller = this.Bound.GetComponent<CarController>();
        }
    }

    public void SetToBound()
    {
        if (this.Bound != null)
        {
            Vector3 position = this.Bound.transform.position;
            this.transform.position = new Vector3(position.x, position.y + this.Height, position.z);
        }
    }

    public void FixedUpdate()
    {
        if (this.Bound == null)
        {
            return;
        }
        Vector3 current = this.transform.position, target = this.Bound.transform.position;
        this.transform.position = new Vector3
        {
            x = Mathf.Lerp(current.x, target.x, this.CameraSpeed.x * Time.deltaTime),
            y = Mathf.Lerp(current.y, target.y + this.Height, this.CameraSpeed.y * Time.deltaTime),
            z = Mathf.Lerp(current.z, target.z, this.CameraSpeed.z * Time.deltaTime)
        };
        if (this._controller != null && this._controller.Reversing)
        {
            
        }

        Quaternion rotation = this.transform.rotation;
        this.transform.LookAt(target);
        this.transform.rotation = Quaternion.Lerp(rotation, this.transform.rotation, this.LookAtDeadZone * Time.deltaTime);
    }
}
