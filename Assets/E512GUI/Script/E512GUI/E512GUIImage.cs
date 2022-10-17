using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class E512GUIImage {
    public int split_horizontal = 1;
    public int split_vertical = 1;
    
    public int tx = 0;
    public int ty = 0;
    
    [Range(0, 16)] public float scale = 1f;
    public Texture texture;
    public Shader shader;
    public Color32 color = Color.white;
    
    [HideInInspector] public float height = 0f;
    [HideInInspector] public float width = 0f;
    [HideInInspector] public float dx = 1f;
    [HideInInspector] public float dy = 1f;
    
    [HideInInspector]
    public Material material;
    
    private Shader defaultshader;
    
    private int gid = 0;
    
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
        this.split_horizontal = Mathf.Max(this.split_horizontal, 1);
        this.split_vertical = Mathf.Max(this.split_vertical, 1);
        this.width = this.texture.width / this.split_horizontal * this.scale;
        this.height = this.texture.height / this.split_vertical * this.scale;
        
        this.dx = 1f / this.split_horizontal;
        this.dy = 1f / this.split_vertical;
        
        this.tx = Mathf.Max(this.tx % this.split_horizontal, 0);
        this.ty = Mathf.Max(this.ty % this.split_vertical, 0);
    }
}
