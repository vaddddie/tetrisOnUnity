using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    private int Choose_the_object;
    private int Choose_the_object_rotations;

    public Animator anim;
    public Transform pos;

    public float Move_interval = 1f;

    private bool[,] coord = new bool[20, 10];
    private int[] position = new int[2];
    private float Step = 0.4288888f;

    void Start()
    {
        anim = GetComponent<Animator>();
        pos = GetComponent<Transform>();
        Choose_the_object = 1;
        Choose_the_object_rotations = Random.Range(1, 5);
        anim.SetInteger("Choose_the_object", Choose_the_object);
        anim.SetInteger("Choose_the_object_rotations", Choose_the_object_rotations);
        
        position[0] = 5;
        position[1] = 17;

        pos.Rotate(0, 0, -90 * (Choose_the_object_rotations - 1));

        Init_State_1();

        // StartCoroutine(Move_down());
    }

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            pos.Rotate(0, 0, 90);

            if (Choose_the_object_rotations == 1)
            {
                pos.position = pos.position + new Vector3(Step / 2, Step / 2, 0);

            } else if (Choose_the_object_rotations == 2)
            {
                pos.position = pos.position + new Vector3(Step / 2, -Step / 2, 0);

            } else if (Choose_the_object_rotations == 3)
            {
                pos.position = pos.position + new Vector3(-Step / 2, -Step / 2, 0);

            } else if (Choose_the_object_rotations == 4)
            {
                pos.position = pos.position + new Vector3(-Step / 2, Step / 2, 0);

            }
            
            if (Choose_the_object_rotations != 1)
            {
                Choose_the_object_rotations -= 1;
            } else {
                Choose_the_object_rotations = 4;
            }
            Debug.Log(Choose_the_object_rotations);
        }

        if (Input.GetKeyDown("e"))
        {
            pos.Rotate(0, 0, -90);

            if (Choose_the_object_rotations == 1)
            {
                pos.position = pos.position + new Vector3(-Step / 2, Step / 2, 0);

            } else if (Choose_the_object_rotations == 2)
            {
                pos.position = pos.position + new Vector3(Step / 2, Step / 2, 0);

            } else if (Choose_the_object_rotations == 3)
            {
                pos.position = pos.position + new Vector3(Step / 2, -Step / 2, 0);

            } else if (Choose_the_object_rotations == 4)
            {
                pos.position = pos.position + new Vector3(-Step / 2, -Step / 2, 0);

            }


            if (Choose_the_object_rotations != 4)
            {
                Choose_the_object_rotations += 1;
            } else {
                Choose_the_object_rotations = 1;
            }
            
            Debug.Log(Choose_the_object_rotations);
        }
    }

    IEnumerator Move_down()
    {
        while(true){
            if (position[1] > 1){
                yield return new WaitForSeconds(Move_interval);
                pos.position = pos.position + new Vector3(0, -Step, 0);
                position[1] -= 1;
            }
        }
    } 

    private void Init_State_1()
    {
        pos.position = new Vector2(0.202f, 3.654f);
        if (Choose_the_object_rotations % 2 == 1)
        {
            pos.position = new Vector2(0.202f, 3.654f);

        } else
        {
            pos.position = new Vector2(0.41644f, 3.439556f);
        }
    }

}
