using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Anm : MonoBehaviour
{
    private Vector3 Delta = new Vector3(0.03f, 0.03f, 0f);
    public bool Spawner;

    void Start()
    {
        transform.localScale = 0.3f * Vector3.one;
    }

    void Update()
    {
        transform.localScale += Delta;
        if (transform.localScale.y > 1.1f){
            transform.localScale = Vector3.one;
            if (Spawner){
                 GetComponent<Red_Box>().Spawn_Cars();
            }
            this.enabled = false;
        }
    }
}
