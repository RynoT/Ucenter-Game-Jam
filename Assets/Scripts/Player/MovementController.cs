using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float power = 15.0f;
   
    public void Update() 
    {
        Rigidbody body = base.GetComponent<Rigidbody>();
        if (body == null)
        {
            return;
        }
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        body.AddForce(direction * power);
        body.AddTorque(direction.normalized);
	}
}
