using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class E512GUI : MonoBehaviour {
    public enum ClipType {
        None,
        Clip,
        ParentAreaClip
    }
    
    public bool hide = false;
    public E512GUIWindow window = new E512GUIWindow();
    public E512GUIImage image = new E512GUIImage();
    public E512GUIText text = new E512GUIText();
    
    public int px = 0;
    public int py = 0;
    [HideInInspector] public int apx = 0;
    [HideInInspector] public int apy = 0;
    
    [Range(0, 64)] public int margin = 6;
    
    [Range(0, 1)] public float alpha = 1f;
    
    private float uvmargin = 0.0005f;
    
    public E512GUI.ClipType clip = E512GUI.ClipType.Clip;
    
    public bool collision = true;
    public bool clickfrontwindow = true;
    public bool move = false;
    public bool moveinsidex = false;
    public bool moveinsidey = false;
    
    public UnityEvent onclick = new UnityEvent();
    
    [HideInInspector] public bool mousedrag = false;
    [HideInInspector] public bool mouseover = false;
    private int wlo = 0;
    private int wro = 0;
    private int wuo = 0;
    private int wdo = 0;
    private int wli = 0;
    private int wri = 0;
    private int wui = 0;
    private int wdi = 0;
    
    private int gid = -1;
    
    private Shader defaultshader;
    private Texture defaultfont;
    
    
    public void Setup () {
        if (this.defaultshader == null) { this.defaultshader = Resources.Load<Shader>("Shader/E512GUIUnlitTextureColor"); }
        if (this.defaultfont== null) { this.defaultfont = Resources.Load<Texture>("Texture/e512font12x6"); }
        
        this.SetShader(ref this.window.shader);
        this.SetShader(ref this.image.shader);
        this.SetShader(ref this.text.shader);
        this.SetFont(ref this.text.texture);
        
        if (this.GetInstanceID() != this.gid) {
            this.gid = this.GetInstanceID();
            this.SetMaterial(ref this.window.material, ref this.window.shader);
            this.SetMaterial(ref this.image.material, ref this.image.shader);
            this.SetMaterial(ref this.text.material, ref this.text.shader);
        }
        this.SetMaterialACC(ref this.window.material, ref this.window.shader, ref this.window.texture);
        this.SetMaterialACC(ref this.image.material, ref this.image.shader, ref this.image.texture);
        this.SetMaterialACC(ref this.text.material, ref this.text.shader, ref this.text.texture);
    }
    public void SetShader (ref Shader shader) {
        if (shader == null) { shader = this.defaultshader; }
    }
    public void SetFont (ref Texture texture) {
        if (texture == null) { texture = this.defaultfont; }
    }
    public void SetMaterial (ref Material material, ref Shader shader) {
        material = new Material(shader);
    }
    public void SetMaterialACC (ref Material material, ref Shader shader, ref Texture texture) {
        if (material.shader != shader) {material.shader = shader; }
        if (material.mainTexture != texture) { material.mainTexture = texture; }
    }
    public void SetMaterialValue (ref Material material, Color color, int l, int u, int r, int d, int sw, int sh, float mx, float my) {
        material.SetColor("_Color", color);
        material.SetFloat("_Alpha", this.alpha);
        material.SetFloat("_WL", (float)l / sw);
        material.SetFloat("_WU", (float)u / sh);
        material.SetFloat("_WR", (float)r / sw);
        material.SetFloat("_WD", (float)d / sh);
        material.SetInt("_Clip", this.clip > 0 ? 1 : 0);
        material.SetFloat("_MX", mx);
        material.SetFloat("_MY", my);
    }
    
    public void DrawUI (int sw, int sh, float mx, float my) {
        this.Setup();
        
        
        this.AbsolutePosition();
        int px = this.apx;
        int py = this.apy;
        
        this.wlo = px;
        this.wro = px + this.window.width;
        this.wuo = py;
        this.wdo = py + this.window.height;
        
        this.wli = this.wlo + this.margin;
        this.wui = this.wuo + this.margin;
        this.wri = this.wro - this.margin;
        this.wdi = this.wdo - this.margin;
        
        if (this.clip == E512GUI.ClipType.ParentAreaClip) {
            this.wli = this.wlo + this.margin;
            this.wui = this.wuo + this.margin;
            this.wri = this.wro - this.margin;
            this.wdi = this.wdo - this.margin;
            
            Transform p = this.transform.parent;
            if (p != null) {
                E512GUI t = p.GetComponent<E512GUI>();
                if (t != null) {
                    this.wlo = Mathf.Max(this.wlo, t.wli);
                    this.wuo = Mathf.Max(this.wuo, t.wui);
                    this.wro = Mathf.Min(this.wro, t.wri);
                    this.wdo = Mathf.Min(this.wdo, t.wdi);
                    this.wli = Mathf.Max(this.wli, t.wli);
                    this.wui = Mathf.Max(this.wui, t.wui);
                    this.wri = Mathf.Min(this.wri, t.wri);
                    this.wdi = Mathf.Min(this.wdi, t.wdi);
                }
            }
        }
        
        this.DrawWindow(px, py, sw, sh, mx, my);
        
        px += this.margin;
        py += this.margin;
        
        this.DrawImage(px, py, sw, sh, mx, my);
        this.DrawText(px, py, sw, sh, mx, my);
        
        
        for (int i = 0; i < this.transform.childCount; ++i) {
            Transform t = this.transform.GetChild(i);
            E512GUI u = t.GetComponent<E512GUI>();
            if (u != null && !u.hide) { u.DrawUI(sw, sh, mx, my); }
        }
    }
    
    public void Test (Vector3 m, ref E512GUI hitui) {
        this.AbsolutePosition();
        int px = this.apx;
        int py = this.apy;
        
        this.wlo = px;
        this.wro = px + this.window.width;
        this.wuo = py;
        this.wdo = py + this.window.height;
        
        this.wli = this.wlo + this.margin;
        this.wui = this.wuo + this.margin;
        this.wri = this.wro - this.margin;
        this.wdi = this.wdo - this.margin;
        
        if (this.clip == E512GUI.ClipType.ParentAreaClip) {
            this.wli = this.wlo + this.margin;
            this.wui = this.wuo + this.margin;
            this.wri = this.wro - this.margin;
            this.wdi = this.wdo - this.margin;
            
            Transform p = this.transform.parent;
            if (p != null) {
                E512GUI t = p.GetComponent<E512GUI>();
                if (t != null) {
                    this.wlo = Mathf.Max(this.wlo, t.wli);
                    this.wuo = Mathf.Max(this.wuo, t.wui);
                    this.wro = Mathf.Min(this.wro, t.wri);
                    this.wdo = Mathf.Min(this.wdo, t.wdi);
                    this.wli = Mathf.Max(this.wli, t.wli);
                    this.wui = Mathf.Max(this.wui, t.wui);
                    this.wri = Mathf.Min(this.wri, t.wri);
                    this.wdi = Mathf.Min(this.wdi, t.wdi);
                }
            }
        }
        
        px += this.margin;
        py += this.margin;
        
        if (this.collision && m.x >= this.wlo && m.x < this.wro && m.y >= this.wuo && m.y < this.wdo) { hitui = this; }
        for (int i = 0; i < this.transform.childCount; ++i) {
            Transform t = this.transform.GetChild(i);
            E512GUI u = t.GetComponent<E512GUI>();
            if (u != null && !u.hide) { u.Test(m, ref hitui); }
        }
    }
    
    public void Test (Vector3 m, E512GUI ignore, ref E512GUI hitui) {
        this.AbsolutePosition();
        int px = this.apx;
        int py = this.apy;
        
        this.wlo = px;
        this.wro = px + this.window.width;
        this.wuo = py;
        this.wdo = py + this.window.height;
        
        this.wli = this.wlo + this.margin;
        this.wui = this.wuo + this.margin;
        this.wri = this.wro - this.margin;
        this.wdi = this.wdo - this.margin;
        
        if (this.clip == E512GUI.ClipType.ParentAreaClip) {
            this.wli = this.wlo + this.margin;
            this.wui = this.wuo + this.margin;
            this.wri = this.wro - this.margin;
            this.wdi = this.wdo - this.margin;
            
            Transform p = this.transform.parent;
            if (p != null) {
                E512GUI t = p.GetComponent<E512GUI>();
                if (t != null) {
                    this.wlo = Mathf.Max(this.wlo, t.wli);
                    this.wuo = Mathf.Max(this.wuo, t.wui);
                    this.wro = Mathf.Min(this.wro, t.wri);
                    this.wdo = Mathf.Min(this.wdo, t.wdi);
                    this.wli = Mathf.Max(this.wli, t.wli);
                    this.wui = Mathf.Max(this.wui, t.wui);
                    this.wri = Mathf.Min(this.wri, t.wri);
                    this.wdi = Mathf.Min(this.wdi, t.wdi);
                }
            }
        }
        
        px += this.margin;
        py += this.margin;
        
        if (this != ignore && this.collision && m.x >= this.wlo && m.x < this.wro && m.y >= this.wuo && m.y < this.wdo) { hitui = this; }
        for (int i = 0; i < this.transform.childCount; ++i) {
            Transform t = this.transform.GetChild(i);
            E512GUI u = t.GetComponent<E512GUI>();
            if (u != null && !u.hide) { u.Test(m, ignore, ref hitui); }
        }
    }
    
    public void ImagePosition (int sw, int sh, float mx, float my, ref Vector3 uv) {
        this.AbsolutePosition();
        int px = this.apx;
        int py = this.apy;
        
        this.wlo = px;
        this.wro = px + this.window.width;
        this.wuo = py;
        this.wdo = py + this.window.height;
        
        this.wli = this.wlo + this.margin;
        this.wui = this.wuo + this.margin;
        this.wri = this.wro - this.margin;
        this.wdi = this.wdo - this.margin;
        
        if (this.clip == E512GUI.ClipType.ParentAreaClip) {
            this.wli = this.wlo + this.margin;
            this.wui = this.wuo + this.margin;
            this.wri = this.wro - this.margin;
            this.wdi = this.wdo - this.margin;
            
            Transform p = this.transform.parent;
            if (p != null) {
                E512GUI t = p.GetComponent<E512GUI>();
                if (t != null) {
                    this.wlo = Mathf.Max(this.wlo, t.wli);
                    this.wuo = Mathf.Max(this.wuo, t.wui);
                    this.wro = Mathf.Min(this.wro, t.wri);
                    this.wdo = Mathf.Min(this.wdo, t.wdi);
                    this.wli = Mathf.Max(this.wli, t.wli);
                    this.wui = Mathf.Max(this.wui, t.wui);
                    this.wri = Mathf.Min(this.wri, t.wri);
                    this.wdi = Mathf.Min(this.wdi, t.wdi);
                }
            }
        }
        
        px += this.margin;
        py += this.margin;
        
        if (this.clip > 0) {
            if (mx >= this.wli && mx < this.wri && my >= this.wui && my < this.wdi) {
                uv.x = mx - px;
                uv.x = uv.x / (float)this.image.width;
                
                uv.y = my - py;
                uv.y = uv.y / (float)this.image.height;
                uv.z = 1;
            }
        } else {
            if (mx >= px && mx < px+this.image.width && my >= py && my < py+this.image.height) {
                uv.x = mx - px;
                uv.x = uv.x / (float)this.image.width;
                
                uv.y = my - py;
                uv.y = uv.y / (float)this.image.height;
                uv.z = 1;
            }
        }
        
        
    }
    
    
    public void DrawText (int px, int py, int sw, int sh, float mx, float my) {
        if (this.text.value.Length == 0) { return; }
        this.SetMaterialValue(ref this.text.material, this.text.color, this.wli, this.wui, this.wri, this.wdi, sw, sh, mx, my);
        
        GL.PushMatrix();
        GL.LoadOrtho();
        this.text.material.SetPass(0);
        GL.Begin(GL.TRIANGLES);
        
        int cx = 0;
        int cy = 0;
        int w = this.wri - this.wli;
        
        foreach (var i in this.text.value) {
            if (i == '\n') {
                cy += 1;
                cx = 0;
                continue;
            }
            if (this.text.wordwrap && w - (cx + 1) * this.text.fontwidth < 0) {
                cy += 1;
                cx = 0;
            }
            
            float cl = px + cx * this.text.fontwidth;
            float cr = px + (cx + 1) * this.text.fontwidth;
            float cu = py + cy * this.text.fontheight;
            float cd = py + (cy + 1) * this.text.fontheight;
            
            if (!(this.clip > 0 && (cr < this.wli || cl >= this.wri || cd < this.wui || cu >= this.wdi))) {
                this.DrawChar(px, py, cx, cy, (int)i, this.text.fontheight, this.text.fontwidth, sw, sh);
            }
            
            cx += 1;
        }
        
        GL.End();
        GL.PopMatrix();
    }
    
    
    public void DrawImage (int px, int py, int sw, int sh, float mx, float my) {
        if (this.image.texture == null) { return; }
        this.SetMaterialValue(ref this.image.material, this.image.color, this.wli, this.wui, this.wri, this.wdi, sw, sh, mx, my);
        
        GL.PushMatrix();
        GL.LoadOrtho();
        this.image.material.SetPass(0);
        GL.Begin(GL.TRIANGLES);
        
        this.DrawTexture(px, py, this.image.width, this.image.height, sw, sh);
        
        GL.End();
        GL.PopMatrix();
    }
    
    public void DrawWindow (int px, int py, int sw, int sh, float mx, float my) {
        if (this.window.texture == null) { return; }
        this.SetMaterialValue(ref this.window.material, this.window.color, this.wlo, this.wuo, this.wro, this.wdo, sw, sh, mx, my);
        
        GL.PushMatrix();
        GL.LoadOrtho();
        this.window.material.SetPass(0);
        GL.Begin(GL.TRIANGLES);
        
        int sph = this.window.material.mainTexture.width / 3;
        int spv = this.window.material.mainTexture.height / 3;
        this.window.width = Mathf.Max(sph * 3, this.window.width);
        this.window.height = Mathf.Max(spv * 3, this.window.height);
        
        int cw = this.window.width-sph*2;
        int ch = this.window.height-spv*2;
        int w = sph;
        int h = spv;
        
        this.DrawTexture(px, py, 0, 0, w, h, 0, 0, sw, sh);
        this.DrawTexture(px, py, 1, 0, cw, h, w, 0, sw, sh);
        this.DrawTexture(px, py, 2, 0, w, h, w+cw, 0, sw, sh);
        
        this.DrawTexture(px, py, 0, 1, w, ch, 0, h, sw, sh);
        this.DrawTexture(px, py, 1, 1, cw, ch, w, h, sw, sh);
        this.DrawTexture(px, py, 2, 1, w, ch, w+cw, h, sw, sh);
        
        this.DrawTexture(px, py, 0, 2, w, h, 0, h+ch, sw, sh);
        this.DrawTexture(px, py, 1, 2, cw, h, w, h+ch, sw, sh);
        this.DrawTexture(px, py, 2, 2, w, h, w+cw, h+ch, sw, sh);
        
        
        GL.End();
        GL.PopMatrix();
    }
    public void DrawTexture (int px, int py, int tx, int ty, int w, int h, int dx, int dy, int sw, int sh) {
        float spy = (1f / sh);
        float spx = (1f / sw);
        
        float fh = spy * h;
        float fw = spx * w;
        float sfx = spx*px+spx*dx;
        float efx = spx*px+spx*dx + fw;
        
        
        float sfy = 1f-(spy*py+spy*dy);
        float efy = 1f-(spy*py+spy*dy + fh);
        
        
        float th = 1f / 3;
        float tw = 1f / 3;
        float uvsx =  uvmargin + tw * tx;
        float uvex = -uvmargin + tw * (tx+1f);
        float uvsy =  uvmargin + 1f - th * (ty+1f);
        float uvey = -uvmargin + 1f - th * ty;
        
        GL.TexCoord(new Vector3(uvsx, uvey, 0));
        GL.Vertex(new Vector3(sfx, sfy, 0));
        GL.TexCoord(new Vector3(uvex, uvey, 0));
        GL.Vertex(new Vector3(efx, sfy, 0));
        GL.TexCoord(new Vector3(uvex, uvsy, 0));
        GL.Vertex (new Vector3(efx, efy, 0));
    
        GL.TexCoord(new Vector3(uvsx, uvey, 0));
        GL.Vertex(new Vector3(sfx, sfy, 0));
        GL.TexCoord(new Vector3(uvex, uvsy, 0));
        GL.Vertex(new Vector3(efx, efy, 0));
        GL.TexCoord(new Vector3(uvsx, uvsy, 0));
        GL.Vertex(new Vector3(sfx, efy, 0));
        
    }
    public void DrawTexture (int px, int py, float w, float h, int sw, int sh) {
        float spy = (1f / sh);
        float spx = (1f / sw);
        
        float sfx = spx*px;
        float efx = spx*px + spx * w;
        
        float sfy = 1f-(spy*py);
        float efy = 1f-(spy*py + spy * h);
        
        float uvsx =  uvmargin + (this.image.dx * this.image.tx);
        float uvex = -uvmargin + (this.image.dx * (this.image.tx+1));
        float uvsy =  uvmargin + 1f - (this.image.dy * (this.image.ty+1));
        float uvey = -uvmargin + 1f - (this.image.dy * this.image.ty);
        
        
        GL.TexCoord(new Vector3(uvsx, uvey, 0));
        GL.Vertex(new Vector3(sfx, sfy, 0));
        GL.TexCoord(new Vector3(uvex, uvey, 0));
        GL.Vertex(new Vector3(efx, sfy, 0));
        GL.TexCoord(new Vector3(uvex, uvsy, 0));
        GL.Vertex (new Vector3(efx, efy, 0));
    
        GL.TexCoord(new Vector3(uvsx, uvey, 0));
        GL.Vertex(new Vector3(sfx, sfy, 0));
        GL.TexCoord(new Vector3(uvex, uvsy, 0));
        GL.Vertex(new Vector3(efx, efy, 0));
        GL.TexCoord(new Vector3(uvsx, uvsy, 0));
        GL.Vertex(new Vector3(sfx, efy, 0));
    }
    
    public void DrawChar (int px, int py, int cx, int cy, int c, float fontheight, float fontwidth, int sw, int sh) {
        
        float fh = (1f / sh) * fontheight;
        float fw = (1f / sw) * fontwidth;
        
        float sfx = (fw*cx) + ((float)px / sw);
        float efx = sfx + fw;
        float sfy = 1f-(fh*cy)-fh - ((float)py / sh);
        float efy = sfy + fh;
        
        int tx = c % 16;
        int ty = c / 16;
        float th = 1f / 16;
        float tw = 1f / 16;
        float uvsx =  uvmargin + tw * tx;
        float uvex = -uvmargin + tw * (tx+1f);
        float uvsy = uvmargin + 1f - th * (ty+1f);
        float uvey =  -uvmargin + 1f - th * ty;
        
        
        GL.TexCoord(new Vector3(uvsx, uvey, 0));
        GL.Vertex(new Vector3(sfx, efy, 0));
        GL.TexCoord(new Vector3(uvex, uvey, 0));
        GL.Vertex(new Vector3(efx, efy, 0));
        GL.TexCoord(new Vector3(uvex, uvsy, 0));
        GL.Vertex (new Vector3(efx, sfy, 0));
    
        GL.TexCoord(new Vector3(uvsx, uvey, 0));
        GL.Vertex(new Vector3(sfx, efy, 0));
        GL.TexCoord(new Vector3(uvex, uvsy, 0));
        GL.Vertex(new Vector3(efx, sfy, 0));
        GL.TexCoord(new Vector3(uvsx, uvsy, 0));
        GL.Vertex(new Vector3(sfx, sfy, 0));
        
    }
    
    void OnDestroy () {
        if (this.window.material) { Object.Destroy(this.window.material); }
        if (this.image.material) { Object.Destroy(this.image.material); }
        if (this.text.material) { Object.Destroy(this.text.material); }
    }
    
    void OnValidate () {
        this.ChangedTexture();
    }
    
    public void ChangedTexture () {
        this.UpdateImage();
        this.UpdateText();
    }
    
    public void UpdateImage () {
        if (this.image.texture == null) { return; }
        this.image.split_horizontal = Mathf.Max(this.image.split_horizontal, 1);
        this.image.split_vertical = Mathf.Max(this.image.split_vertical, 1);
        this.image.width = this.image.texture.width / this.image.split_horizontal * this.image.scale;
        this.image.height = this.image.texture.height / this.image.split_vertical * this.image.scale;
        
        this.image.dx = 1f / this.image.split_horizontal;
        this.image.dy = 1f / this.image.split_vertical;
        this.image.tx = Mathf.Max(this.image.tx % this.image.split_horizontal, 0);
        this.image.ty = Mathf.Max(this.image.ty % this.image.split_vertical, 0);
    }
    
    public void UpdateText () {
        if (this.text.texture == null) { return; }
        this.text.fontheight = this.text.texture.height / 16 * this.text.scale;
        this.text.fontwidth = this.text.texture.width / 16 * this.text.scale;
    }
    
    public void ModifyToParentClipArea () {
        Transform parent = this.transform.parent;
        if (parent == null) { return; }
        E512GUI parentui = parent.GetComponent<E512GUI>();
        if (parentui == null) { return; }
        
        int px = this.px;
        int py = this.py;
        Transform p = this.transform.parent;
        while (p != null) {
            E512GUI t = p.GetComponent<E512GUI>();
            if (t != null) {
                px += t.margin + t.px;
                py += t.margin + t.py;
            }
            p = p.parent;
        }
        if (this.moveinsidex) {
            if (px < parentui.wli) { this.px += parentui.wli - px; }
            if (px+this.window.width > parentui.wri) { this.px += parentui.wri - px - this.window.width; }
        }
        if (this.moveinsidey) {
            if (py < parentui.wui) { this.py += parentui.wui - py; }
            if (py+this.window.height > parentui.wdi) { this.py += parentui.wdi - py - this.window.height; }
        }
    }
    
    public E512GUIRoot Root () {
        Transform p = this.transform.parent;
        while (p != null) {
            E512GUIRoot r = p.GetComponent<E512GUIRoot>();
            if (r != null) { return r; }
            p = p.parent;
        }
        return null;
    }
    
    public void AbsolutePosition () {
        this.apx = this.px;
        this.apy = this.py;
        Transform p = this.transform.parent;
        while (p != null) {
            E512GUI t = p.GetComponent<E512GUI>();
            if (t != null) {
                this.apx += t.margin + t.px;
                this.apy += t.margin + t.py;
            }
            p = p.parent;
        }
    }
    
}
