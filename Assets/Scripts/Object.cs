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

    private bool[,] coord = new bool[10, 21];
    private int[,] position = new int[4, 2];
    private float Step = 0.4288888f;

    void Start()
    {
        anim = GetComponent<Animator>();
        pos = GetComponent<Transform>();
        Choose_the_object = 1;
        Choose_the_object_rotations = Random.Range(1, 5);
        anim.SetInteger("Choose_the_object", Choose_the_object);
        anim.SetInteger("Choose_the_object_rotations", Choose_the_object_rotations);

        for(int i = 0; i < 10; i++)
        {
            coord[i, 0] = true;
        }
        
        pos.Rotate(0, 0, -90 * (Choose_the_object_rotations - 1));

        Init_State_1();

        StartCoroutine(Move_down());
    }

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            pos.Rotate(0, 0, 90);

            Rotate_State_1_minus();
            
            if (Choose_the_object_rotations != 1)
            {
                Choose_the_object_rotations -= 1;
            } else {
                Choose_the_object_rotations = 4;
            }

        }

        if (Input.GetKeyDown("e"))
        {
            pos.Rotate(0, 0, -90);

            Rotate_State_1_plus();

            if (Choose_the_object_rotations != 4)
            {
                Choose_the_object_rotations += 1;
            } else {
                Choose_the_object_rotations = 1;
            }
            
        }
    }

    IEnumerator Move_down()
    {
        bool temp = true;

        yield return new WaitForSeconds(Move_interval);
        while(temp){
            pos.position = pos.position + new Vector3(0, -Step, 0);
            yield return new WaitForSeconds(Move_interval);

            for (int i = 0; i < 4; i++)
            {
                position[i, 1] -= 1;
            }

            for (int i = 0; i < 4; i++)
            {
                if (coord[position[i, 0] - 1, position[i, 1] - 2] == true)
                {
                    Invoke("Init_State_1()", 0.1f);
                    temp = false;
                    break;
                }
            }
        }
    } 

    private void Init_State_1()
    {
        pos.position = new Vector2(0.202f, 3.654f);
        if (Choose_the_object_rotations % 2 == 1)
        {
            pos.position = new Vector2(0.202f, 3.654f - Step / 2);

            position[0, 0] = 5 + (Choose_the_object_rotations - 1);
            position[0, 1] = 18;

            position[1, 0] = 6;
            position[1, 1] = 18;

            position[2, 0] = 7 - (Choose_the_object_rotations - 1);
            position[2, 1] = 18;

            position[3, 0] = 7 - (Choose_the_object_rotations - 1);
            position[3, 1] = 17 + (Choose_the_object_rotations - 1);

        } else
        {
            pos.position = new Vector2(0.41644f - Step / 2, 3.439556f);

            position[0, 0] = 6;
            position[0, 1] = 19 - (Choose_the_object_rotations - 2);

            position[1, 0] = 6;
            position[1, 1] = 18;

            position[2, 0] = 6;
            position[2, 1] = 17 + (Choose_the_object_rotations - 2);

            position[3, 0] = 5 + (Choose_the_object_rotations - 2);
            position[3, 1] = 17 + (Choose_the_object_rotations - 2);
        }
    }


    private void Rotate_State_1_plus()
    {
        position[0, 0] += (Choose_the_object_rotations % 2) * (Choose_the_object_rotations - 2) * (-1) + ((Choose_the_object_rotations % 2) - 1) * (Choose_the_object_rotations - 3);
        position[0, 1] += (Choose_the_object_rotations % 2) * (Choose_the_object_rotations - 2) * (-1) + ((Choose_the_object_rotations % 2) - 1) * (Choose_the_object_rotations - 3) * (-1);

        position[2, 0] += (Choose_the_object_rotations % 2) * (Choose_the_object_rotations - 2) + ((Choose_the_object_rotations % 2) - 1) * (Choose_the_object_rotations - 3) * (-1);
        position[2, 1] += (Choose_the_object_rotations % 2) * (Choose_the_object_rotations - 2) + ((Choose_the_object_rotations % 2) - 1) * (Choose_the_object_rotations - 3);
        
        position[3, 0] += (Choose_the_object_rotations % 2) * 2 * (Choose_the_object_rotations - 2);
        position[3, 1] += ((Choose_the_object_rotations % 2) - 1) * 2 * (Choose_the_object_rotations - 3);

    }

    private void Rotate_State_1_minus()
    {
        position[0, 0] += (Choose_the_object_rotations % 2) * (Choose_the_object_rotations - 2) * (-1) + ((Choose_the_object_rotations % 2) - 1) * (Choose_the_object_rotations - 3);
        position[0, 1] += (Choose_the_object_rotations % 2) * (Choose_the_object_rotations - 2) + ((Choose_the_object_rotations % 2) - 1) * (Choose_the_object_rotations - 3);

        position[2, 0] += (Choose_the_object_rotations % 2) * (Choose_the_object_rotations - 2) + ((Choose_the_object_rotations % 2) - 1) * (Choose_the_object_rotations - 3) * (-1);
        position[2, 1] += (Choose_the_object_rotations % 2) * (Choose_the_object_rotations - 2) * (-1) + ((Choose_the_object_rotations % 2) - 1) * (Choose_the_object_rotations - 3) * (-1);
        
        position[3, 0] += ((Choose_the_object_rotations % 2) - 1) * 2 * (Choose_the_object_rotations - 3);
        position[3, 1] += (Choose_the_object_rotations % 2) * (-2) * (Choose_the_object_rotations - 2);

    }

}
