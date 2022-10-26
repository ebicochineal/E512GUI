using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EbiColor : MonoBehaviour {
    int cnt = 0;
    // E512GUI gui;
    E512GUI pgui;
    float timer = 0;
    // Use this for initialization
    void Start () {
        // this.gui = this.GetComponent<E512GUI>();
        this.pgui = this.transform.parent.GetComponent<E512GUI>();
    }
    
    // Update is called once per frame
    void Update () {
        this.timer -= Time.deltaTime;
        
        if (this.timer < 0) {
            this.timer = 0.5f;
            List<E512GUI> ls = new List<E512GUI>();
            for (int i = 0; i < this.transform.childCount; i++) {
                ls.Add(this.transform.GetChild(i).GetComponent<E512GUI>());
            }
            if (ls.Count > 0) {
                int x = ls[cnt % ls.Count].image.tx;
                cnt += 1;
                if (x == 7) { this.pgui.window.color =  new Color32(200, 43, 85, 255); }
                if (x == 8) { this.pgui.window.color =  new Color32(239, 131, 0, 255); }
                if (x == 9) { this.pgui.window.color =  new Color32(0, 117, 198, 255); }
                if (x == 10) { this.pgui.window.color = new Color32(167, 189, 0, 255); }
                if (x == 11) { this.pgui.window.color = new Color32(255, 255, 255, 255); }
            }
            
        }
    }
}
