using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class E512GUIText {
    [Multiline(3)] public string  value = "";
    public bool wordwrap = true;
    
    [Range(0, 16)] public float scale = 1f;
    public Texture texture;
    public Shader shader;
    public Color32 color = Color.white;
    
    [HideInInspector] public float fontheight = 12;
    [HideInInspector] public float fontwidth = 6;
    [HideInInspector] public Material material;
    
    private Shader defaultshader;
    private Texture defaulttexture;
    
    private int gid = 0;
    
    public void Setup (int gid, float alpha, int wl, int wu, int wr, int wd, int clip, int sw, int sh, float mx, float my) {
        if (this.defaultshader == null) { this.defaultshader = Resources.Load<Shader>("Shader/E512GUIUnlitTextureColor"); }
        if (this.shader == null) { this.shader = this.defaultshader; }
        
        if (this.defaulttexture == null) { this.defaulttexture = Resources.Load<Texture>("Texture/e512font12x6"); }
        if (this.texture == null) { this.texture = this.defaulttexture; }
        
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
        this.fontheight = this.texture.height / 16 * this.scale;
        this.fontwidth = this.texture.width / 16 * this.scale;
    }
}
