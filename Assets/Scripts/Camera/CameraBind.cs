using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBind : MonoBehaviour 
{

	public float height;
	public Vector3 cameraSpeed;
	public GameObject bound;

	public void Start() 
	{
		this.SetToBound();
	}

	public void SetToBound() 
	{
		if (this.bound != null) 
		{
			Vector3 position = this.bound.transform.position;
			this.transform.position = new Vector3(position.x, position.y + this.height, position.z);
		}
	}

	public void Update() 
	{
		if (this.bound == null) 
		{
			return;
		}
		// Height interpolate
        Vector3 current = this.transform.position;
        Vector3 target = this.bound.transform.position;
		target.y += this.height;

		Vector3 frame = new Vector3();
         
        frame.x = Mathf.SmoothDamp(current.x, target.x, ref this.cameraSpeed.x, Time.deltaTime);
        frame.y = Mathf.SmoothDamp(current.y, target.y, ref this.cameraSpeed.y, Time.deltaTime);
        frame.z = Mathf.SmoothDamp(current.z, target.z, ref this.cameraSpeed.z, Time.deltaTime);

		this.transform.position = target;
	}
}
