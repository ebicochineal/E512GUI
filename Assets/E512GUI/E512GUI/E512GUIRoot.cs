using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E512GUIRoot : MonoBehaviour {
    public enum InputType {
        None,
        ScreenPosition,
        MeshUVPosition,
        E512GUIImagePosition
    };
    
    public InputType input = InputType.ScreenPosition;
    
    public bool hide = false;
    
    [HideInInspector] public Camera raycast;
    [HideInInspector] public Camera render;
    [HideInInspector] public E512GUI screengui;
    public Vector3 MousePos () {
        Vector3 ret = new Vector3(0, 0, -1);
        if (this.input == InputType.None) { return ret; }
        if (this.raycast == null || this.render == null || this.render.targetTexture == null || this.input == InputType.ScreenPosition) {
            ret = Input.mousePosition;
            ret.y = Screen.height - ret.y;
            ret.z = 1;
            return ret;
        }
        
        if (this.input == InputType.MeshUVPosition) {
            Ray ray = this.raycast.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                Vector2 uv = hit.textureCoord;
                ret.x = this.render.targetTexture.width*uv.x;
                ret.y = this.render.targetTexture.height - this.render.targetTexture.height*uv.y;
                ret.z = 1;
            }
            return ret;
        }
        
        if (this.input == InputType.E512GUIImagePosition && this.screengui != null) {
            Vector3 uv = new Vector3(0, 0, -1);
            this.screengui.ImagePosition(Screen.width, Screen.height, Input.mousePosition.x, Screen.height-Input.mousePosition.y, ref uv);
            if (uv.z > -1) {
                ret.x = uv.x * this.render.targetTexture.width;
                ret.y = uv.y * this.render.targetTexture.height;
                ret.z = 1;
            }
            return ret;
        }
        
        
        return ret;
    }
    
    public E512GUI MouseClick () {
        E512GUI hitui = null;
        if (this.hide) { return hitui; }
        Vector3 m = this.MousePos();
        if (m.z > -1) {
            for (int i = 0; i < this.transform.childCount; ++i) {
                E512GUI u = this.transform.GetChild(i).GetComponent<E512GUI>();
                if (u != null && !u.hide) { u.Test(m, ref hitui); }
            }
        }
        
        return hitui;
    }
    
    public E512GUI MouseOver () {
        E512GUI hitui = null;
        if (this.hide) { return hitui; }
        Vector3 m = this.MousePos();
        foreach (var u in this.GetComponentsInChildren<E512GUI>()) {
            if (!u.hide) { u.mouseover = false; }
        }
        if (m.z > -1) {
            for (int i = 0; i < this.transform.childCount; ++i) {
                Transform t = this.transform.GetChild(i);
                if (t == null) { continue; }
                E512GUI u = t.GetComponent<E512GUI>();
                if (u != null && !u.hide) { u.Test(m, ref hitui); }
            }
        }
        return hitui;
    }
    
    
    void OnRenderObject () { this.DrawUI(); }
    void OnDrawGizmos () { this.DrawUI(); }
    
    void DrawUI () {
        if (this.hide) { return; }
        if ((int)(Camera.current.cullingMask & (1<<this.gameObject.layer)) == 0) { return; }
        
        Vector3 m = this.MousePos();
        int sw = Screen.width;
        int sh = Screen.height;
        if (Camera.current.targetTexture != null) {
            sw = Camera.current.targetTexture.width;
            sh = Camera.current.targetTexture.height;
        }
        
        for (int i = 0; i < this.transform.childCount; ++i) {
            Transform t = this.transform.GetChild(i);
            if (t == null) { continue; }
            E512GUI u = t.GetComponent<E512GUI>();
            if (u != null && !u.hide) { u.DrawUI(sw, sh, m.x, m.y); }
        }
    }
    
}

