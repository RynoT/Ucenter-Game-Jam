using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{

    public Vector3 LockTo = new Vector3(0.0f, 0.0f, 0.0f);
    public bool LockX = true, LockY = false, LockZ = true;

    public void Update()
    {
        if (!this.LockX && !this.LockY && !this.LockZ)
        {
            return;
        }
        Vector3 rotation = gameObject.transform.rotation.eulerAngles;
        if (this.LockX)
        {
            rotation.x = this.LockTo.x;
        }
        if (this.LockY)
        {
            rotation.y = this.LockTo.y;
        }
        if (this.LockZ)
        {
            rotation.z = this.LockTo.z;
        }
        gameObject.transform.localRotation = Quaternion.Euler(rotation);
    }
}
