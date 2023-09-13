using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Static
{

    public static int Num_Blue_Cars = 20;
    public static int Num_Red_Cars = 20;

    public static int Maze_Complexity = 5;

    public static Network Apex_Blue;

    public static List<Network> Blues;
    public static List<Network> Reds;

    // Updated Each Iteration Aligned with Networks
    public static List<int> Blue_Scores;
    public static List<int> Red_Scores;

    public static List<GameObject> Blue_Cars;
    public static List<GameObject> Red_Cars;



}
