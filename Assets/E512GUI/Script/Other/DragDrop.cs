using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour {
    public E512GUIEvent ev;
    E512GUI gui;
    E512GUIRoot root;
    
    void Start () {
        this.gui = this.GetComponent<E512GUI>();
        this.root = this.gui.Root();
    }
    
    bool prev = false;
    void Update () {
        if (!this.prev && this.gui.mousedrag) { this.DragStart(); }
        if (this.prev && !this.gui.mousedrag) { this.Drop(); }
        this.prev = this.gui.mousedrag;
    }
    public void DragStart () {
        if (this.root == null) { return; }
        E512GUI hit = this.root.HitGUI(this.gui);
        
        this.gui.AbsolutePosition();
        this.ev.tmpx = this.gui.apx;
        this.ev.tmpy = this.gui.apy;
        this.gui.px = this.gui.apx;
        this.gui.py = this.gui.apy;
        this.transform.parent = this.root.transform;
        
    }
    public void Drop () {
        if (this.root == null) { return; }
        E512GUI hit = this.root.HitGUI(this.gui);
        DropField d = null;
        if (hit != null) {
            d = hit.GetComponent<DropField>();
            if (d == null) { d = hit.GetComponentInChildren<DropField>(); }
            if (d == null) { d = hit.GetComponentInParent<DropField>(); }
            if (d == null) {
                this.gui.AbsolutePosition();
                this.gui.px = this.gui.apx;
                this.gui.py = this.gui.apy;
                this.transform.parent = this.root.transform;
            } else {
                this.transform.parent = d.transform;
                this.gui.px = 0;
                this.gui.py = 0;
            }
        }
    }
}
