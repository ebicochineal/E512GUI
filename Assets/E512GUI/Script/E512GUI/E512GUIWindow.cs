using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class E512GUIWindow {
    public int width = 24;
    public int height = 24;
    
    public Texture texture;
    public Shader shader;
    public Color32 color = Color.white;
    
    [HideInInspector] public Material material;
    
    private Shader defaultshader;
    private int gid;
    public void Setup (int gid, float alpha, int wl, int wu, int wr, int wd, int clip, int sw, int sh, float mx, float my) {
        if (this.defaultshader == null) { this.defaultshader = Resources.Load<Shader>("Shader/E512GUIUnlitTextureColor"); }
        if (this.shader == null) { this.shader = this.defaultshader; }
        
        if (this.material == null || gid != this.gid) {
            if (this.shader) {
                this.material = new Material(this.shader);
                this.gid = gid;
            } else {
                this.material = new Material(this.defaultshader);
                this.gid = gid;
            }
        }
        this.material.SetColor("_Color", this.color);
        this.material.SetFloat("_Alpha", alpha);
        
        this.material.SetFloat("_WL", (float)wl / sw);
        this.material.SetFloat("_WU", (float)wu / sh);
        this.material.SetFloat("_WR", (float)wr / sw);
        this.material.SetFloat("_WD", (float)wd / sh);
        this.material.SetInt("_Clip", clip);
        this.material.SetFloat("_MX", mx);
        this.material.SetFloat("_MY", my);
        
        if (this.material.shader != this.shader) {
            this.material.shader = this.shader;
        }
        if (this.material.mainTexture != this.texture) {
            this.material.mainTexture = this.texture;
        }
    }
    
    
    public void UpdateTexture (int gid) {
        if (this.texture == null) { return; }
    }
    
}
