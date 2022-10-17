using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour {
    E512GUI gui;
    E512GUIRoot root;
    
    void Start () {
        this.gui = this.GetComponent<E512GUI>();
        this.root = this.gui.Root();
    }
    
    bool prev = false;
    void Update () {
        if (this.prev && !this.gui.mousedrag) { this.Drop(); }
        this.prev = this.gui.mousedrag;
    }
    
    public void Drop () {
        if (this.root == null) { return; }
        ChildAlignment a = this.transform.parent.GetComponent<ChildAlignment>();
        
        E512GUI t = this.root.HitGUI();
        if (t != this.gui) {
            if (a != null) { a.Alignment(); }
            return;
        }
        
        E512GUI hit = this.root.HitGUI(this.gui);
        if (hit == null) {
            this.gui.AbsolutePosition();
            this.gui.px = this.gui.apx;
            this.gui.py = this.gui.apy;
            this.transform.parent = this.root.transform;
            if (a != null) { a.Alignment(); }
        } else {
            ChildAlignment b = hit.GetComponent<ChildAlignment>();
            if (b == null) {
                if (a != null) { a.Alignment(); }
                return;
            }
            this.transform.parent = hit.transform;
            this.gui.px = 0;
            this.gui.py = 0;
            b.Alignment();
            if (a != null) { a.Alignment(); }
        }
    }
}
