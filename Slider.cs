using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    public Transform Slide;
    public float Limit;
    public bool Active;

    public void OnMouseDown(){

        Active = true;

    }

    public void onMouseUp(){
        Active = false;
    }

    public void Update(){
        if (Input.GetMouseButtonUp(0)){
            Active = false;
        }
        if (Active){
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse -= Slide.localPosition;


            if (mouse.x <= Limit && mouse.x >= -Limit){
                transform.localPosition = new Vector3(mouse.x, 0, 0);
            }
        }
    }
}
