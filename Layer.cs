using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer
{
    public int NumInputs;
    public int NumOutputs;
    public float[] Inputs;
    public float[,] Weights;
    public float[] Outputs;
    public float[] Gamma;
    public float[,] Delta;

    public Layer(int NumInputs, int NumOutputs)
    {
        this.NumInputs = NumInputs;
        this.NumOutputs = NumOutputs;

        this.Inputs = new float[NumInputs];
        this.Weights = new float[NumInputs + 1, NumOutputs];
        this.Outputs = new float[NumOutputs];
        this.Gamma = new float[NumOutputs];
        this.Delta = new float[NumInputs + 1, NumOutputs];

        for (int j = 0; j <= NumInputs; j++) for (int i = 0; i < NumOutputs; i++) Weights[j, i] = Random.Range(-1f, 1f);
    }

    public float Sigmoid(float Value) { return 1 / (1 + Mathf.Exp(-Value)); }

    public float Derivative(float Value) { return Value * (1 - Value); }

    public float[] FeedForward(float[] Input)
    {
        Inputs = Input;
        for (int i = 0; i < NumOutputs; i++)
        {
            Outputs[i] = 0;
            for (int j = 0; j < NumInputs; j++) Outputs[i] += Inputs[j] * Weights[j, i];
            Outputs[i] = Sigmoid(Outputs[i] + Weights[NumInputs, i]);
        }
        return Outputs;
    }

    public void BackPropagate(float[] Expected)
    {
        for (int i = 0; i < NumOutputs; i++)
        {
            Gamma[i] = (Outputs[i] - Expected[i]) * Derivative(Sigmoid(Outputs[i]));
            for (int j = 0; j < NumInputs; j++) Delta[j, i] = Gamma[i] * Inputs[j];
            Delta[NumInputs, i] = Gamma[i];
        }
    }

    public void BackPropagate(float[] GammaForward, float[,] WeightsForward)
    {
        for (int i = 0; i < NumOutputs; i++)
        {
            Gamma[i] = 0;
            for (int j = 0; j < GammaForward.Length; j++) Gamma[i] += GammaForward[j] * WeightsForward[i, j];
            Gamma[i] *= Derivative(Sigmoid(Outputs[i]));
        }
        for (int i = 0; i < NumOutputs; i++)
        {
            for (int j = 0; j < NumInputs; j++) Delta[j, i] = Gamma[i] * Inputs[j];
            Delta[NumInputs, i] = Gamma[i];
        }
    }

    public void UpdateWeights()
    {
        for (int i = 0; i < NumOutputs; i++) for (int j = 0; j <= NumInputs; j++) Weights[j, i] -= Delta[j, i] * .2f;
    }
}

