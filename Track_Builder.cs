using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track_Builder : MonoBehaviour
{
    public int Size;

    public Track_Piece[] Pieces;
    public Track_Piece[,] Track;

    public int Final_Y = -1;
    public int Final_X = -1;

    public void Start(){
        Size = Static.Maze_Complexity * 2 + 1;
        Track = new Track_Piece[Size, Size];
        Track[0, Static.Maze_Complexity] = Pieces[0];
        int Tracker = 0;
        bool Continue = true;
        while(Continue && Tracker < 1000){
            Continue = Find_And_Fill_Position();
            Tracker++;
        }
        for (int i = 0; i < Size; i++){
            for (int j = 0; j < Size; j++){
                if (Track[i,j] != null) Instantiate(Track[i, j], new Vector3(j - Size/2f + 0.5f, i - 3f, 0), Quaternion.identity);
            }
        }
        Set_End();
    }

    public void Regenerate(){
        foreach (Track_Piece P in GameObject.FindObjectsOfType<Track_Piece>()){
            Destroy(P.gameObject);
        }
        foreach (GameObject G in Static.Blue_Cars){
            Destroy(G);
        }
        foreach (GameObject G in Static.Red_Cars){
            Destroy(G);
        }
        Static.Blue_Cars = new List<GameObject>();
        Static.Red_Cars = new List<GameObject>();
        Start();
    }

    public void Update(){
        if (Input.GetKeyDown(KeyCode.R)){
            Regenerate();
        }
        if (Application.targetFrameRate != 60){
            Application.targetFrameRate = 60;
        }
    }

    public bool Can_Piece_Fit(Track_Piece Piece, int Y, int X){
        if (Y >= Size || Y < 0 || X >= Size || X < 0) return false;
        if (Track[Y, X] != null) return false;
        int num_neighbors = 0;
        bool will_connect = false;
        if (Piece.N){
            if (Y >= Size - 1) return false;
            if (Track[Y + 1, X] != null){
                num_neighbors += 1;
                if (!Track[Y + 1, X].S) return false;
                will_connect = true;
            }
        }
        else{
            if (Y < Size - 1){
                if (Track[Y + 1, X] != null){
                    num_neighbors += 1;
                    if (Track[Y + 1, X].S) return false;
                }   
            }
        }
        if (Piece.S){
            if (Y <= 0) return false;
            if (Track[Y - 1, X] != null){
                num_neighbors += 1;
                if (!Track[Y - 1, X].N) return false;
                will_connect = true;
            }
        }
        else{
            if (Y > 0){
                if (Track[Y - 1, X] != null){
                    num_neighbors += 1;
                    if (Track[Y - 1, X].N) return false;
                }   
            }
        }
        if (Piece.E){
            if (X >= Size - 1) return false;
            if (Track[Y, X + 1] != null){
                num_neighbors += 1;
                if (!Track[Y, X + 1].W) return false;
                will_connect = true;
            }
        }
        else{
            if (X < Size - 1){
                if (Track[Y, X + 1] != null){
                    num_neighbors += 1;
                    if (Track[Y, X + 1].W) return false;
                }   
            }
        }
        if (Piece.W){
            if (X <= 0) return false;
            if (Track[Y, X - 1] != null){
                num_neighbors += 1;
                if (!Track[Y, X - 1].E) return false;
                will_connect = true;
            }
        }
        else{
            if (X > 0){
                if (Track[Y, X - 1] != null){
                    num_neighbors += 1;
                    if (Track[Y, X - 1].E) return false;
                }   
            }
        }
        if (num_neighbors <= 0) return false;
        if (!will_connect) return false;
        return true;
    }

    public int Get_Num_Possible_Pieces(int Y, int X){
        int Sum = 0;
        for (int i = 5; i < Pieces.Length - 1; i++){
            if (Can_Piece_Fit(Pieces[i], Y, X)) Sum++;
        }
        return Sum;
    }

    public void Fill_Position(int Y, int X){
        List<Track_Piece> Possibilities = new List<Track_Piece>();
        for (int i = 5; i < Pieces.Length - 1; i++){
            if (Can_Piece_Fit(Pieces[i], Y, X)){
                Possibilities.Add(Pieces[i]);
            }
        }
        if (Possibilities.Count <= 0){
            Track[Y, X] = Pieces[Pieces.Length - 1];
        }
        else{
            Track[Y, X] = Possibilities[Random.Range(0, Possibilities.Count)];
            Final_X = X;
            Final_Y = Y;
        }
    }

    public void Set_End(){
        if (Track[Final_Y, Final_X].N){
            if (Track[Final_Y + 1, Final_X] == null){
                Track[Final_Y + 1, Final_X] = Pieces[2];
                Instantiate(Track[Final_Y + 1, Final_X], new Vector3(Final_X - Size/2f + 0.5f, Final_Y + 1 - 3f, 0), Quaternion.identity);
                return;
            }
        }
        if (Track[Final_Y, Final_X].S){
            if (Track[Final_Y - 1, Final_X] == null){
                Track[Final_Y - 1, Final_X] = Pieces[4];
                Instantiate(Track[Final_Y - 1, Final_X], new Vector3(Final_X - Size/2f + 0.5f, Final_Y - 1 - 3f, 0), Quaternion.identity);
                return;
            }
        }
        if (Track[Final_Y, Final_X].E){
            if (Track[Final_Y, Final_X + 1] == null){
                Track[Final_Y, Final_X + 1] = Pieces[3];
                Instantiate(Track[Final_Y, Final_X + 1], new Vector3(Final_X + 1 - Size/2f + 0.5f, Final_Y - 3f, 0), Quaternion.identity);
                return;
            }
        }
        if (Track[Final_Y, Final_X].W){
            if (Track[Final_Y, Final_X - 1] == null){
                Track[Final_Y, Final_X - 1] = Pieces[1];
                Instantiate(Track[Final_Y, Final_X - 1], new Vector3(Final_X - 1 - Size/2f + 0.5f, Final_Y - 3f, 0), Quaternion.identity);
                return;
            }
        }
    }

    public bool Find_And_Fill_Position(){
        int[,] Amount = new int[Size, Size];
        for (int i = 0; i < Size; i++){
            for (int j = 0; j < Size; j++){
                if (Track[i,j] != null){
                    Amount[i,j] = 0;
                }
                else{
                    Amount[i,j] = Get_Num_Possible_Pieces(i, j);
                }
            }
        }
        int Least_Y = -1;
        int Least_X = -1;
        int Max = 0;
        for (int i = 0; i < Size; i++){
            for (int j = 0; j < Size; j++){
                if (Amount[i, j] > Max){
                    Least_X = j;
                    Least_Y = i;
                    Max = Amount[i, j];
                }
            }
        }
        if (Least_X == -1 || Least_Y == -1) return false;
        Fill_Position(Least_Y, Least_X);
        return true;
    }

}
