using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Red_Car : MonoBehaviour
{
    public int index = -1;
    public bool Active = true;
    public LayerMask Layer;
    public LayerMask Lines;
    public float Sight_Range = 0.5f;
    public Rigidbody2D Body;
    public Collider2D Box;
    public float Speed = 0.2f;

    public float Delta_Direction;

    public int Score;
    public int Countdown = 600;
    public int Max_Countdown = 600;
    public List<Collider2D> Crossed;


    // Start is called before the first frame update
    void Start()
    {
        Crossed = new List<Collider2D>();
        Countdown = Max_Countdown;
    }

    // Update is called once per frame
    void Update()
    {
        if(index >= 0 && Active){

            RaycastHit2D hit1 = Physics2D.Raycast(transform.position, new Vector3(-1f, 0f, 0f), Sight_Range, Layer);
            RaycastHit2D hit2 = Physics2D.Raycast(transform.position, new Vector3(-0.66f, 0.33f, 0f), Sight_Range, Layer);
            RaycastHit2D hit3 = Physics2D.Raycast(transform.position, new Vector3(-0.33f, 0.66f, 0f), Sight_Range, Layer);
            RaycastHit2D hit4 = Physics2D.Raycast(transform.position, new Vector3(0f, 1f, 0f), Sight_Range, Layer);
            RaycastHit2D hit5 = Physics2D.Raycast(transform.position, new Vector3(0.33f, 0.66f, 0f), Sight_Range, Layer);
            RaycastHit2D hit6 = Physics2D.Raycast(transform.position, new Vector3(0.66f, 0.33f, 0f), Sight_Range, Layer);
            RaycastHit2D hit7 = Physics2D.Raycast(transform.position, new Vector3(1f, 0f, 0f), Sight_Range, Layer);

            float val1 = Sight_Range;
            if (hit1.collider != null){
                val1 = Vector3.Distance(transform.position, hit1.point);
            }
            float val2 = Sight_Range;
            if (hit2.collider != null){
                val2 = Vector3.Distance(transform.position, hit2.point);
            }
            float val3 = Sight_Range;
            if (hit3.collider != null){
                val3 = Vector3.Distance(transform.position, hit3.point);
            }
            float val4 = Sight_Range;
            if (hit4.collider != null){
                val4 = Vector3.Distance(transform.position, hit4.point);
            }
            float val5 = Sight_Range;
            if (hit5.collider != null){
                val5 = Vector3.Distance(transform.position, hit5.point);
            }
            float val6 = Sight_Range;
            if (hit6.collider != null){
                val6 = Vector3.Distance(transform.position, hit6.point);
            }
            float val7 = Sight_Range;
            if (hit7.collider != null){
                val7 = Vector3.Distance(transform.position, hit7.point);
            }

            Delta_Direction = Static.Reds[index].FeedForward(new float[] {val1, val2, val3, val4, val5, val6, val7})[0];
            Delta_Direction = Delta_Direction * 20f - 10;

            transform.Rotate(0f, 0f, Delta_Direction);
            Body.velocity = Speed * transform.up;

            if (Box.IsTouchingLayers(Layer)){
                Active = false;
                Body.velocity = Vector2.zero;
            }

            Countdown--;
            if (Countdown <= 0){
                Active = false;
                Body.velocity = Vector2.zero;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.layer == 7){
            if (!Crossed.Contains(col)) {
                Crossed.Add(col);
                Countdown = Max_Countdown;
                Score++;
            }
        }
    }
}
