using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCalculator : MonoBehaviour {
    E512GUI gui;
    public string q = "";
    public string v = "";
    void Start () {
        this.gui = this.GetComponent<E512GUI>();
    }
    
    void FixedUpdate () {
        foreach (var j in this.q) {
            if (j != 'C' && this.v.Length >= 16) { break; }
            this.v += j;
            if (this.v.Contains("C")) { this.v = ""; }
            if (this.v.Contains("=")) {
                long ans = 0;
                string tmp = "";
                long x = 1;
                foreach (var i in this.v) {
                    if (i == '+') {
                        ans += long.Parse("0"+tmp) * x;
                        tmp = "";
                        x = 1;
                    } else if (i == '-') {
                        ans += long.Parse("0"+tmp) * x;
                        tmp = "";
                        x = -1;
                    } else if (i == '=') {
                        ans += long.Parse("0"+tmp) * x;
                    } else {
                        tmp += i;
                    }
                }
                this.v = ans.ToString();
            }
            this.gui.text.value = string.Format("{0, 16}", this.v);
        }
        this.q = "";
    }
}
