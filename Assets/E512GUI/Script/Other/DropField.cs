using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropField : MonoBehaviour {
    E512GUI gui;
    int cnt = 0;
    void Start () {
        this.gui = this.GetComponent<E512GUI>();
    }
    
    void Update () {
        if (this.transform.childCount != this.cnt) {
            this.Alignment();
        }
        this.cnt = this.transform.childCount;
    }
    
    public void Alignment () {
        List<E512GUI> l = new List<E512GUI>();
        for (int i = 0; i < this.transform.childCount; i++) {
            l.Add(this.transform.GetChild(i).GetComponent<E512GUI>());
        }
        int px = 0;
        int py = 0;
        
        int w = this.gui.window.width - this.gui.margin * 2;
        int h = this.gui.window.height - this.gui.margin * 2;
        int ph = 0;
        foreach (var i in l) {
            ph = Mathf.Max(i.window.height, ph);
            if (px + i.window.width > w) {
                px = 0;
                py += ph;
            }
            i.px = px;
            i.py = py;
            px += i.window.width;
        }
    }
}
