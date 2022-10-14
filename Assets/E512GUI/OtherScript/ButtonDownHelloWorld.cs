using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDownHelloWorld : MonoBehaviour {
    void Start () {
        this.GetComponent<E512GUI>().onclick.AddListener(this.HelloWorld);
    }
    
    public void HelloWorld () { print("HelloWorld"); }
}
