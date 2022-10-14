using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderValue : MonoBehaviour {
    E512GUI gui;
    public float value = 0;
    float max = 0;
    void Start () {
        this.gui = this.GetComponent<E512GUI>();
        E512GUI p = this.gui.transform.parent.GetComponent<E512GUI>();
        int w = p.window.width - p.margin * 2 - this.gui.window.width;
        int h = p.window.height - p.margin * 2 - this.gui.window.height;
        this.max = Mathf.Max(w, h);
    }
    
    void Update () {
        this.value = this.gui.px / this.max;
    }
}
