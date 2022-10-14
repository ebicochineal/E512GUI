using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E512GUIEvent : MonoBehaviour {
    
    
    public List<E512GUIRoot> guis = new List<E512GUIRoot>();
    
    Vector3 prev;
    int tmpx = 0;
    int tmpy = 0;
    E512GUI drag = null;
    int index = -1;
    
    void Update () {
        if (Input.GetMouseButtonUp(0) || !Input.GetMouseButton(0)) {
            if (this.drag != null) { this.drag.mousedrag = false; }
            this.drag = null;
            this.index = -1;
        }
        
        E512GUI hitui = null;
        foreach (var i in this.guis) {
            E512GUI u = i.MouseOver();
            if (u != null) { hitui = u; }
        }
        if (hitui != null) { hitui.mouseover = true; }
        
        hitui = null;
        
        Vector3 hitm = new Vector3();
        int hitindex = -1;
        if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0)) {
            for (int i = 0; i < this.guis.Count; i++) {
                E512GUI u = this.guis[i].MouseClick();
                if (u != null) {
                    hitui = u;
                    hitindex = i;
                }
            }
        }
        
        if (Input.GetMouseButtonDown(0) && hitui != null) { this.index = hitindex; }
        if (this.index >= 0) { hitm = this.guis[this.index].MousePos(); }
        
        if (hitui != null) {
            Transform t = hitui.transform;
            while (t != null) {
                if (Input.GetMouseButtonDown(0)) {
                    E512GUI u = t.GetComponent<E512GUI>();
                    if (u != null && u.clickfrontwindow) { t.SetAsLastSibling(); }
                }
                if (t.parent == this.transform) { break; }
                t = t.parent;
            }
            
            if (hitui.onclick != null && Input.GetMouseButtonUp(0)) { hitui.onclick.Invoke(); }
            
            
            while (hitui != null && hitui.move == false) {
                hitui = hitui.transform.parent.GetComponent<E512GUI>();
            }
            
            // drag start
            if (Input.GetMouseButtonDown(0) && hitui != null) {
                this.drag = hitui;
                this.prev = hitm;
                this.tmpx = this.drag.px;
                this.tmpy = this.drag.py;
                this.drag.mousedrag = true;
            }
        }
        
        // drag move
        if (Input.GetMouseButton(0) && this.drag != null) {
            Vector3 d = hitm - this.prev;
            this.drag.px = this.tmpx + (int)d.x;
            this.drag.py = this.tmpy + (int)d.y;
            this.drag.ModifyToParentClipArea();
        }
        
    }
    
}
