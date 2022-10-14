using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour {
    public Vector3 rotate;
    void Update () {
        this.transform.Rotate(this.rotate);
    }
}
