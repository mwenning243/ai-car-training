using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Iterator : MonoBehaviour
{
    public bool Completed = false;

    public void Reset_Cars(){


        Static.Blue_Cars = Static.Blue_Cars.OrderByDescending(x=>x.GetComponent<Move_Blue_Car>().Score).ToList();
        Static.Apex_Blue = Static.Blues[Static.Blue_Cars[0].GetComponent<Move_Blue_Car>().index];

        // Need to train reds with apex
        
        for(int i = 0; i + Mathf.CeilToInt(Static.Num_Blue_Cars / 2) < Static.Num_Blue_Cars; i++){
            int Bad_Network_Index = Static.Blue_Cars[i + Mathf.CeilToInt(Static.Num_Blue_Cars / 2)].GetComponent<Move_Blue_Car>().index;
            int Good_Network_Index = Static.Blue_Cars[i].GetComponent<Move_Blue_Car>().index;

            for (int a = 0; a < Static.Blues[Bad_Network_Index].Layers.GetLength(0); a++){
                for (int b = 0; b < Static.Blues[Bad_Network_Index].Layers[a].Weights.GetLength(0); b++){
                    for (int c = 0; c < Static.Blues[Bad_Network_Index].Layers[a].Weights.GetLength(1); c++){
                        Static.Blues[Bad_Network_Index].Layers[a].Weights[b, c] = Static.Blues[Good_Network_Index].Layers[a].Weights[b, c] * Random.Range(0.95f, 1.05f);
                    }
                }
            }
            
        }



        foreach (GameObject G in Static.Blue_Cars){
            Destroy(G);
        }
        foreach (GameObject G in Static.Red_Cars){
            Destroy(G);
        }
        Static.Blue_Cars = new List<GameObject>();
        Static.Red_Cars = new List<GameObject>();
        GetComponent<Red_Box>().Spawn_Cars();
        Initiate_Cars();
    }
        
    void OnEnable()
    {
        for (int i = 0; i < Static.Blue_Cars.Count; i++){
            Static.Blue_Cars[i].GetComponent<Move_Blue_Car>().index = i;
        }

        for (int i = 0; i < Static.Red_Cars.Count; i++){
            Static.Red_Cars[i].GetComponent<Move_Red_Car>().index = i;
        }
        Completed = false;
    }

    void Initiate_Cars(){
        for (int i = 0; i < Static.Blue_Cars.Count; i++){
            Static.Blue_Cars[i].GetComponent<Move_Blue_Car>().index = i;
        }

        for (int i = 0; i < Static.Red_Cars.Count; i++){
            Static.Red_Cars[i].GetComponent<Move_Red_Car>().index = i;
        }
        Completed = false;
    }

    void Update(){
        if (!Completed){
            bool Done = true;
            foreach (Move_Blue_Car B in GameObject.FindObjectsOfType<Move_Blue_Car>()){
                if (B.Active) Done = false;
            }
            foreach (Move_Red_Car R in GameObject.FindObjectsOfType<Move_Red_Car>()){
                if (R.Active) Done = false;
            }
            if (Done){
                Completed = true;
                Reset_Cars();
            }
        }
    }

}
