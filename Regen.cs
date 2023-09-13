using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regen : MonoBehaviour
{
    public Track_Builder Builder;
    
    void OnMouseDown(){
        Builder.Regenerate();
    }
}
