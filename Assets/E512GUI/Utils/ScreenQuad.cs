using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenQuad : MonoBehaviour {
    public Camera finalscreencamera;
    void Start () {
        
    }

    // Update is called once per frame
    void Update () {
        if (Screen.height > 0 && Screen.width > 0) {
            float a = (Screen.width - (Screen.height*1.7777f))*0.5f;
            float x = a > 0 ? a : 0;
            float t = this.finalscreencamera.ScreenToWorldPoint(new Vector2(x, 0f)).x * -2f;
            this.transform.localScale = new Vector3(t, t*0.5625f, 1f);
        }
    }
}
