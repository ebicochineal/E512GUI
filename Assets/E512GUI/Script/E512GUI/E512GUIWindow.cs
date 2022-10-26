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
}
