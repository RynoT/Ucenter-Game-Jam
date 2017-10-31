using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    public float SpeedPercentage = 0.0f;
    public float MaxSpeed = 3.5f;

    public float VelocityPower = 15.0f;
    public float RotationPower = 15.0f;

    public float VelocityRequiredForRot = 2.0f;

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
        //Debug.Log(body.velocity.magnitude);
        this.SpeedPercentage = Mathf.Min(body.velocity.magnitude / this.MaxSpeed, 1.0f) * 100.0f;

        AkSoundEngine.SetRTPCValue("Car_Speed", SpeedPercentage);

        if (Input.GetKeyDown(KeyCode.W))
        {
            AkSoundEngine.PostEvent("Car_Forward", gameObject);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            AkSoundEngine.PostEvent("Car_Stop", gameObject);
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            AkSoundEngine.PostEvent("Car_Turn", gameObject);
        }

        // Velocity
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        Quaternion rotation = base.gameObject.transform.rotation;

        body.AddForce(this.VelocityPower * (rotation * Quaternion.Euler(0.0f, -90.0f, 0.0f) * new Vector3(0.0f, 0.0f, direction.z)) * Time.deltaTime);

        // Rotation
        float speed = Mathf.Min(body.velocity.magnitude, this.VelocityRequiredForRot) / this.VelocityRequiredForRot;
        body.rotation *= Quaternion.Euler(new Vector3(0.0f, this.RotationPower * direction.x * speed, 0.0f) * Time.deltaTime);
    }
}


