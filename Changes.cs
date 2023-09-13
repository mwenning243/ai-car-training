using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changes : MonoBehaviour
{
    public Slider Comp;
    public Slider Red;
    public Slider Blue;

    void OnMouseDown(){
        int Previous_Blues = Static.Num_Blue_Cars;
        int Previous_Reds = Static.Num_Red_Cars;

        Static.Maze_Complexity = Mathf.RoundToInt(Comp.transform.localPosition.x * 2.5f + 2.5f);
        Static.Num_Red_Cars = Mathf.RoundToInt(Red.transform.localPosition.x * 10f + 20f);
        Static.Num_Blue_Cars = Mathf.RoundToInt(Blue.transform.localPosition.x * 10f + 20f);

        if (Previous_Blues > Static.Num_Blue_Cars){
            int Num_To_Delete = Previous_Blues - Static.Num_Blue_Cars;
            for (int i = 0; i < Num_To_Delete; i++){
                int index = Random.Range(0, Static.Blues.Count);
                Static.Blues.RemoveAt(index);
            }
        }
        else if (Previous_Blues < Static.Num_Blue_Cars){
            int Num_To_Add = Static.Num_Blue_Cars - Previous_Blues;
            for (int i = 0; i < Num_To_Add; i++){
                Static.Blues.Add(new Network(new int[] { 7, 10, 5, 1 }));
            }
        }

        if (Previous_Reds > Static.Num_Red_Cars){
            int Num_To_Delete = Previous_Reds - Static.Num_Red_Cars;
            for (int i = 0; i < Num_To_Delete; i++){
                int index = Random.Range(0, Static.Reds.Count);
                Static.Reds.RemoveAt(index);
            }
        }
        else if (Previous_Reds < Static.Num_Red_Cars){
            int Num_To_Add = Static.Num_Red_Cars - Previous_Reds;
            for (int i = 0; i < Num_To_Add; i++){
                Static.Reds.Add(new Network(new int[] { 7, 10, 5, 1 }));
            }
        }

    }
}
