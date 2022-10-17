using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTextScript : MonoBehaviour {
    public E512GUI target;
    SliderValue slider;
    E512GUI gui;
    void Start () {
        this.gui = this.GetComponent<E512GUI>();
        this.slider = this.target.GetComponent<SliderValue>();
    }
    
    // Update is called once per frame
    void Update () {
        if (this.target.mouseover || this.target.mousedrag) {
            this.gui.text.value = this.slider.value.ToString();
            this.gui.hide = false;
            
        } else {
            this.gui.hide = true;
        }
        
        
        
    }
}
