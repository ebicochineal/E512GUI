using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(E512GUI))]
[CanEditMultipleObjects]
public class E512GUIEditor : Editor {
    bool s = false;
    public override void OnInspectorGUI () {
        E512GUI u = this.target as E512GUI;
        
        
        this.s = EditorGUILayout.Toggle("Show Button", s);
        if (this.s) {
            if (GUILayout.Button("Reset Position")) {
                Undo.RegisterCompleteObjectUndo(u, "E512GUI Reset Position");
                u.px = 0;
                u.py = 0;
            }
            if (GUILayout.Button("Window To Parent Size")) {
                E512GUI p = this.ParentGUI(u);
                if (p != null) {
                    Undo.RegisterCompleteObjectUndo(u, "E512GUI Window To Parent Size");
                    u.window.width = p.window.width - p.margin * 2;
                    u.window.height = p.window.height - p.margin * 2;
                }
            }
            if (GUILayout.Button("Window To Image Size")) {
                Undo.RegisterCompleteObjectUndo(u, "E512GUI Window To Image Size");
                u.window.width = (int)(u.image.width + u.margin * 2);
                u.window.height = (int)(u.image.height + u.margin * 2);
            }
            if (GUILayout.Button("Image To Window Size H")) {
                if (u.image.texture != null) {
                    Undo.RegisterCompleteObjectUndo(u, "Image To Window Size H");
                    float uh = u.window.height - u.margin * 2;
                    float h = u.image.texture.height / Mathf.Max(u.image.split_vertical, 1);
                    u.image.scale = uh / h;
                    u.UpdateTexture();
                }
            }
            if (GUILayout.Button("Image To Window Size W")) {
                if (u.image.texture != null) {
                    Undo.RegisterCompleteObjectUndo(u, "Image To Window Size W");
                    float uw = u.window.width - u.margin * 2;
                    float w = u.image.texture.width / Mathf.Max(u.image.split_horizontal, 1);;
                    u.image.scale = uw / w;
                    u.UpdateTexture();
                }
            }
        }
        base.OnInspectorGUI();
    }
    E512GUI ParentGUI (E512GUI u) {
        Transform t = u.transform.parent;
        if (t == null) { return null; }
        E512GUI r = t.GetComponent<E512GUI>();
        if (r == null) { return null; }
        return r;
    }
}


[CustomEditor(typeof(E512GUIRoot))]
[CanEditMultipleObjects]
public class E512GUIEditorRoot : Editor {
    public override void OnInspectorGUI () {
        E512GUIRoot root = this.target as E512GUIRoot;
        
        base.OnInspectorGUI();
        
        if (root.input == E512GUIRoot.InputType.MeshUVPosition) {
            root.raycast = EditorGUILayout.ObjectField("Raycast Camera", root.raycast, typeof(Camera), true) as Camera;
            root.render = EditorGUILayout.ObjectField("Render Texture Camera", root.render, typeof(Camera), true) as Camera;
        }
        
        if (root.input == E512GUIRoot.InputType.E512GUIImagePosition) {
            root.screengui = EditorGUILayout.ObjectField("Screen GUI", root.screengui, typeof(E512GUI), true) as E512GUI;
            root.render = EditorGUILayout.ObjectField("Render Texture Camera", root.render, typeof(Camera), true) as Camera;
        }
        
    }
    
}