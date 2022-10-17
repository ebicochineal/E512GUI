using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCalculatorButton : MonoBehaviour {
    E512GUI root;
    E512GUI gui;
    SimpleCalculator calculator;
    void Start () {
        this.gui = this.GetComponent<E512GUI>();
        this.gui.onclick.AddListener(this.Click);
        if (this.calculator == null) { this.SetDentakuRootWindow(); }
    }
    
    public void Click () {
        this.calculator.q += this.gui.text.value[1];
    }
    
    public void SetDentakuRootWindow () {
        Transform t = this.transform;
        while (t != null) {
            SimpleCalculator u = t.GetComponent<SimpleCalculator>();
            if (u != null) { this.calculator = u; }
            t = t.parent;
        }
    }
}
