using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverLight : MonoBehaviour {
    E512GUI gui;
    Color bcolor = new Color();
    Color acolor = new Color();
    float t = 0f;
    void Start () {
        this.gui = this.GetComponent<E512GUI>();
        this.acolor = this.gui.window.color;
        this.bcolor.r = Mathf.Min(this.acolor.r * 1.25f, 1f);
        this.bcolor.g = Mathf.Min(this.acolor.g * 1.25f, 1f);
        this.bcolor.b = Mathf.Min(this.acolor.b * 1.25f, 1f);
        this.bcolor.a = Mathf.Min(this.acolor.a * 1.25f, 1f);
    }
     
    void FixedUpdate () {
        if (this.gui.mouseover) {
            this.t = Mathf.Min(this.t + 0.1f, 1f);
        } else {
            this.t = Mathf.Max(this.t - 0.1f, 0f);
        }
        this.gui.window.color = Color.Lerp(this.acolor, this.bcolor, this.t);
    }
}
