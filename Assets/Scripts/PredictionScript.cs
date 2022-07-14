using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PredictionScript : MonoBehaviour
{
    public Animator anim;
    public Transform pos;

    private int temp;

    void Start()
    {
        anim = GetComponent<Animator>();
        pos = GetComponent<Transform>();

        temp = 1;
    }

    public void ChangePredictionObject(int PreObject, int PreObjectRotate)
    {
        anim.SetInteger("ChangePredictionState", PreObject);

        if (PreObjectRotate > temp){
            pos.Rotate(0, 0, -90 * (PreObjectRotate - temp));
        }
        else
        {
            pos.Rotate(0, 0, -90 * (PreObjectRotate + 4 - temp));
        }

        temp = PreObjectRotate;
    }
}
