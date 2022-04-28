using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    void Update()
    {
        var currentRot = transform.rotation.eulerAngles;
        var newRot = new Vector3(currentRot.x, currentRot.y + 0.5f, currentRot.z);
        transform.rotation = Quaternion.Euler(newRot);
    }
}
