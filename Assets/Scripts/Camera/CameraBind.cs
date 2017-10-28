using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBind : MonoBehaviour 
{

	public float height;
    public Vector3 cameraSpeed;
    public float lookAtDeadZone = 2.0f;

	public GameObject bound;
    private Rigidbody body;

	public void Start() 
	{
		this.SetToBound();
        if (this.bound != null)
        {
            this.body = this.bound.GetComponent<Rigidbody>();
        }
	}

	public void SetToBound() 
	{
		if (this.bound != null) 
		{
			Vector3 position = this.bound.transform.position;
			this.transform.position = new Vector3(position.x, position.y + this.height, position.z);
		}
	}

	public void FixedUpdate() 
	{
		if (this.bound == null) 
		{
			return;
		}
        Vector3 current = this.transform.position, target = this.bound.transform.position;

		Vector3 frame = new Vector3();
         
        frame.x = Mathf.Lerp(current.x, target.x, this.cameraSpeed.x * Time.deltaTime);
        frame.y = Mathf.Lerp(current.y, target.y + this.height, this.cameraSpeed.y * Time.deltaTime);
        frame.z = Mathf.Lerp(current.z, target.z, this.cameraSpeed.z * Time.deltaTime);

        this.transform.position = frame;

        Quaternion rotation = this.transform.rotation;
        this.transform.LookAt(target);
        this.transform.rotation = Quaternion.Lerp(rotation, this.transform.rotation, this.lookAtDeadZone * Time.deltaTime);
	}
}
