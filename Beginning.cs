using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beginning : MonoBehaviour
{
    void Start()
    {
        Static.Blues = new List<Network>();
        Static.Reds = new List<Network>();

        for (int i = 0; i < Static.Num_Blue_Cars; i++){
            Static.Blues.Add(new Network(new int[] { 7, 10, 5, 1 }));
        }

        for (int i = 0; i < Static.Num_Red_Cars; i++){
            Static.Reds.Add(new Network(new int[] { 7, 10, 5, 1 }));
        }

       
        Static.Blue_Cars = new List<GameObject>();
        Static.Red_Cars = new List<GameObject>();
    }

}
