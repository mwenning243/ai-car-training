using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_Box : MonoBehaviour
{
    public float Spawn_Range;
    public float Spawn_Y;
    public float Spawn_X;

    public GameObject Blue_Car;
    public GameObject Red_Car;

    public void Spawn_Cars(){
        for (int i = 0; i < Static.Num_Blue_Cars; i++){
            Static.Blue_Cars.Add(Instantiate(Blue_Car, new Vector3(Spawn_X + Random.Range(-Spawn_Range, Spawn_Range), Spawn_Y, 0f), Quaternion.identity));
        }
        for (int i = 0; i < Static.Num_Red_Cars; i++){
            Static.Red_Cars.Add(Instantiate(Red_Car, new Vector3(Spawn_X + Random.Range(-Spawn_Range, Spawn_Range), Spawn_Y, 0f), Quaternion.identity));
        }

        GetComponent<Iterator>().enabled = true;

        // start iterator loop, enable some disabled script on prefab

        // another script will iterate through list of cars, index will carry to network in list at same index, networks output applys to car body inputs
        // ignores cars that deem themselves inactive from crashing, cars also store lines crossed as a score? maybe that is stored in static
    }
}
