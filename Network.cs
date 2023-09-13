using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Network
{
    public int[] Nodes;
    public Layer[] Layers;

    public Network(int[] Nodes)
    {
        this.Nodes = Nodes;
        this.Layers = new Layer[Nodes.Length - 1];
        for (int i = 0; i < Layers.Length; i++) Layers[i] = new Layer(Nodes[i], Nodes[i + 1]);
    }

    public float[] FeedForward(float[] Inputs)
    {
        Layers[0].FeedForward(Inputs);
        for (int i = 1; i < Layers.Length; i++) Layers[i].FeedForward(Layers[i - 1].Outputs);
        return Layers[Layers.Length - 1].Outputs;
    }

    public void BackPropagate(float[] Expected)
    {
        Layers[Layers.Length - 1].BackPropagate(Expected);
        for (int i = Layers.Length - 2; i >= 0; i--) Layers[i].BackPropagate(Layers[i + 1].Gamma, Layers[i + 1].Weights);
        for (int i = 0; i < Layers.Length; i++) Layers[i].UpdateWeights();
    }
}

