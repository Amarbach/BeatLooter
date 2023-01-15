using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public void RotateZ(float eulerAngle)
    {
        transform.Rotate(new Vector3(0, 0, eulerAngle));
    }
}
