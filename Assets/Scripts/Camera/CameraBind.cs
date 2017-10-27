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
			Vector3 position = this.bound.transform.localPosition;
			this.transform.localPosition = new Vector3(position.x, position.y + this.height, position.z);
		}
	}

	public void Update() 
	{
		if (this.bound == null) 
		{
			return;
		}
		// Height interpolate
		Vector3 current = this.transform.localPosition;
		Vector3 target = this.bound.transform.localPosition;
		target.y += this.height;

		Vector3 frame = new Vector3();
		frame.x = Mathf.Lerp(current.x, target.x, this.cameraSpeed.x * Time.deltaTime);
		frame.y = Mathf.Lerp(current.y, target.y, this.cameraSpeed.y * Time.deltaTime);
		frame.z = Mathf.Lerp(current.z, target.z, this.cameraSpeed.z * Time.deltaTime);

		this.transform.localPosition = frame;
	}
}
