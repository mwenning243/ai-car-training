using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Network_Test : MonoBehaviour
{
    void Start()
    {
        Network Net = new Network(new int[] { 2, 15, 15, 1 });
        for (int i = 0; i < 10000; i++)
        {
            float A = Random.Range(1, 10) * 0.1f;
            float B = Random.Range(1, 10) * 0.1f;
            float C = A * B;

            Net.FeedForward(new float[] { A, B });
            Net.BackPropagate(new float[] { C });

        }
        Debug.Log(Net.FeedForward(new float[] { 0.5f, 0.9f })[0]);
        Debug.Log(Net.FeedForward(new float[] { 0.5f, 0.8f })[0]);
        Debug.Log(Net.FeedForward(new float[] { 0.5f, 0.7f })[0]);
        Debug.Log(Net.FeedForward(new float[] { 0.5f, 0.6f })[0]);
        Debug.Log(Net.FeedForward(new float[] { 0.5f, 0.5f })[0]);
        Debug.Log(Net.FeedForward(new float[] { 0.5f, 0.4f })[0]);
        Debug.Log(Net.FeedForward(new float[] { 0.5f, 0.3f })[0]);
        Debug.Log(Net.FeedForward(new float[] { 0.5f, 0.2f })[0]);
        Debug.Log(Net.FeedForward(new float[] { 0.5f, 0.1f })[0]);
    }

}
