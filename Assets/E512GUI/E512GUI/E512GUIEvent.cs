using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E512GUIEvent : MonoBehaviour {
    public List<E512GUIRoot> guis = new List<E512GUIRoot>();
    
    Vector3 prev;
    int tmpx = 0;
    int tmpy = 0;
    E512GUI drag = null;
    E512GUI down = null;
    int index = -1;
    public bool hit = false;
    void Update () {
        
        if (Input.GetMouseButtonUp(0) || !Input.GetMouseButton(0)) {
            if (this.drag != null) { this.drag.mousedrag = false; }
            this.drag = null;
            this.index = -1;
        }
        
        E512GUI hitui = null;
        int hitindex = -1;
        for (int i = 0; i < this.guis.Count; i++) {
            E512GUI u = this.guis[i].HitGUI();
            if (u != null) {
                hitui = u;
                hitindex = i;
            }
        }
        
        if (hitui != null) { hitui.mouseover = true; }
        this.hit = hitui != null;
        
        
        Vector3 hitm = new Vector3();
        if (Input.GetMouseButtonDown(0) && hitui != null) { this.index = hitindex; }
        if (this.index >= this.guis.Count) { this.index = -1; }
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
            
            if (Input.GetMouseButtonDown(0)) { this.down = hitui; }
            if (hitui.onclick != null && Input.GetMouseButtonUp(0) && hitui == this.down) { hitui.onclick.Invoke(); }
            if (Input.GetMouseButtonUp(0)) { this.down = null; }
            
            E512GUI canmoveui = hitui;
            while (canmoveui != null && canmoveui.move == false) {
                canmoveui = canmoveui.transform.parent.GetComponent<E512GUI>();
            }
            
            // drag start
            if (Input.GetMouseButtonDown(0) && canmoveui != null) {
                this.drag = canmoveui;
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
