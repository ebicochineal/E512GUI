using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InertialSlide : MonoBehaviour {
    float prevx = 0;
    float prevy = 0;
    float dx = 0f;
    float dy = 0f;
    E512GUI u;
    
    [Range(0.75f, 0.98f)]
    public float f = 0.95f;
    
    
    void Start () {
        this.u = this.GetComponent<E512GUI>();
        this.prevx = this.u.px;
        this.prevy = this.u.py;
    }
    
    void FixedUpdate () {
        if (u.mousedrag) {
            this.dx = this.dx * 3 + this.u.px - this.prevx;
            this.dy = this.dy * 3 + this.u.py - this.prevy;
            this.dx *= 0.25f;
            this.dy *= 0.25f;
            this.prevx = this.u.px;
            this.prevy = this.u.py;
        } else {
            if (Mathf.Abs(this.dx) > 0.1f || Mathf.Abs(this.dy) > 0.1f) {
                this.prevx += this.dx;
                this.prevy += this.dy;
                this.dx *= 0.95f;
                this.dy *= 0.95f;
                this.u.px = (int)this.prevx;
                this.u.py = (int)this.prevy;
                
                this.u.ModifyToParentClipArea();
                
                
            } else {
                this.prevx = this.u.px;
                this.prevy = this.u.py;
            }
        }
    }
}
