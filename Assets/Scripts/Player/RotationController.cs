using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour {

    public Vector3 lockTo = new Vector3(0.0f, 0.0f, 0.0f);
    public bool lockX = true, lockY = false, lockZ = true;
	
	public void Update() 
    {
        if (!this.lockX && !this.lockY && !this.lockZ)
        {
            return;
        }
        Vector3 rotation = gameObject.transform.rotation.eulerAngles;
        if (this.lockX)
        {
            rotation.x = this.lockTo.x;
        }
        if (this.lockY)
        {
            rotation.y = this.lockTo.y;
        }
        if (this.lockZ)
        {
            rotation.z = this.lockTo.z;
        }
        gameObject.transform.localRotation = Quaternion.Euler(rotation);
	}
}
