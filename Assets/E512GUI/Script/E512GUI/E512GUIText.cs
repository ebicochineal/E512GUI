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
}
