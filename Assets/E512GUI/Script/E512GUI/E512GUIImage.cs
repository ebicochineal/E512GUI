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
}
