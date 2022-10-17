using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRayCast : MonoBehaviour {
    public E512GUIEvent guievent;
    
    void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool objhit = false;
        if (Physics.Raycast(ray, out hit)) {
            objhit = hit.collider.gameObject == this.gameObject;
        }
        
        if (this.guievent.hit == false && objhit == true) {
            this.transform.localScale = new Vector3(2, 2, 2);
        } else {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
