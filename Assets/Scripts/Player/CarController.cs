using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public float velocityPower = 15.0f;
    public float rotationPower = 15.0f;

    public float velocityRequiredForRot = 2.0f;

    public void Start()
    {
        Rigidbody body = base.GetComponent<Rigidbody>();
        if (body != null)
        {
            body.maxAngularVelocity = 0.0f;
        }
    }

    public void Update() 
    {
        Rigidbody body = base.GetComponent<Rigidbody>();
        if (body == null)
        {
            return;
        }
        // Velocity
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        Quaternion rotation = base.gameObject.transform.rotation;

        body.AddForce(this.velocityPower * (rotation * Quaternion.Euler(0.0f, -90.0f, 0.0f) * new Vector3(0.0f, 0.0f, direction.z)) * Time.deltaTime);
        
        // Rotation
        float speed = Mathf.Min(body.velocity.magnitude, this.velocityRequiredForRot) / this.velocityRequiredForRot;
        body.rotation *= Quaternion.Euler(new Vector3(0.0f, this.rotationPower * direction.x * speed, 0.0f) * Time.deltaTime);
    }
}
