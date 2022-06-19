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

    private bool[,] coord = new bool[12, 21];
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

        for(int i = 0; i < 12; i++)
        {
            coord[i, 0] = true;
        }
        
        for(int i = 0; i < 21; i++)
        {
            coord[0, i] = true;
            coord[11, i] = true;
        }
        
        pos.Rotate(0, 0, -90 * (Choose_the_object_rotations - 1));

        Init_State_1();

        StartCoroutine(Move_down());
    }

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            Rotate_State_1_minus(true);
        }

        if (Input.GetKeyDown("e"))
        {
            Rotate_State_1_plus(true);
        }

        if (Input.GetKeyDown("a"))
        {   
            bool temp = true;

            for (int i = 0; i < 4; i++)
            {
                if (coord[position[i, 0] - 1, position[i, 1]] == true)
                {
                    temp = false;
                    break;
                }
            }
            
            if (temp)
            {

                pos.position = pos.position + new Vector3(-Step, 0, 0);

                for (int i = 0; i < 4; i++)
                {
                    position[i, 0] -= 1;
                }
                
            }
        }

        if (Input.GetKeyDown("d"))
        {
            bool temp = true;

            for (int i = 0; i < 4; i++)
            {
                if (coord[position[i, 0] + 1, position[i, 1]] == true)
                {
                    temp = false;
                    break;
                }
            }
            
            if (temp)
            {

                pos.position = pos.position + new Vector3(Step, 0, 0);

                for (int i = 0; i < 4; i++)
                {
                    position[i, 0] += 1;
                }
                
            }
        }

    }

    IEnumerator Move_down()
    {
        bool temp = false;

        yield return new WaitForSeconds(Move_interval);
        while(true){
            pos.position = pos.position + new Vector3(0, -Step, 0);
            for (int i = 0; i < 4; i++)
            {
                position[i, 1] -= 1;
            }
            yield return new WaitForSeconds(Move_interval);
            for (int i = 0; i < 4; i++)
            {
                if (coord[position[i, 0], position[i, 1] - 1] == true)
                {
                    temp = true;
                    break;
                } 
            }
            if (temp)
            {
                Init_State_1();
                break;
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
            position[0, 1] = 19;

            position[1, 0] = 6;
            position[1, 1] = 19;

            position[2, 0] = 7 - (Choose_the_object_rotations - 1);
            position[2, 1] = 19;

            position[3, 0] = 7 - (Choose_the_object_rotations - 1);
            position[3, 1] = 18 + (Choose_the_object_rotations - 1);

        } else
        {
            pos.position = new Vector2(0.41644f - Step / 2, 3.439556f);

            position[0, 0] = 6;
            position[0, 1] = 20 - (Choose_the_object_rotations - 2);

            position[1, 0] = 6;
            position[1, 1] = 19;

            position[2, 0] = 6;
            position[2, 1] = 18 + (Choose_the_object_rotations - 2);

            position[3, 0] = 5 + (Choose_the_object_rotations - 2);
            position[3, 1] = 18 + (Choose_the_object_rotations - 2);
        }
    }


    private void Rotate_State_1_plus(bool check)
    {
        bool temp = true;

        if (Choose_the_object_rotations == 1) 
        {
            position[0, 0] += 1;
            position[0, 1] += 1;
            
            position[2, 0] -= 1;
            position[2, 1] -= 1;
            
            position[3, 0] -= 2;
        }

        if (Choose_the_object_rotations == 2) 
        {
            position[0, 0] += 1;
            position[0, 1] -= 1;
            
            position[2, 0] -= 1;
            position[2, 1] += 1;
            
            position[3, 1] += 2;
        }

        if (Choose_the_object_rotations == 3) 
        {
            position[0, 0] -= 1;
            position[0, 1] -= 1;
            
            position[2, 0] += 1;
            position[2, 1] += 1;
            
            position[3, 0] += 2;            
        }

        if (Choose_the_object_rotations == 4) 
        {
            position[0, 0] -= 1;
            position[0, 1] += 1;
            
            position[2, 0] += 1;
            position[2, 1] -= 1;
            
            position[3, 1] -= 2;
        }

        if (Choose_the_object_rotations != 4)
        {
            Choose_the_object_rotations += 1;
        } else {
            Choose_the_object_rotations = 1;
        }

        if (check)
        {
            for (int i = 0; i < 4; i++)
            {
                if (coord[position[i, 0],position[i, 1]] == true)
                {
                    Rotate_State_1_minus(false);
                    temp = false;
                    break;
                }
            }
        }

        if (temp && check)
        {
            pos.Rotate(0, 0, -90);
        }


    }

    private void Rotate_State_1_minus(bool check)
    {       
        bool temp = true;

        if (Choose_the_object_rotations == 1) 
        {
            position[0, 0] += 1;
            position[0, 1] -= 1;
            
            position[2, 0] -= 1;
            position[2, 1] += 1;
            
            position[3, 1] += 2;
        }

        if (Choose_the_object_rotations == 2) 
        {
            position[0, 0] -= 1;
            position[0, 1] -= 1;
            
            position[2, 0] += 1;
            position[2, 1] += 1;
            
            position[3, 0] += 2;
        }

        if (Choose_the_object_rotations == 3) 
        {
            position[0, 0] -= 1;
            position[0, 1] += 1;
            
            position[2, 0] += 1;
            position[2, 1] -= 1;
            
            position[3, 1] -= 2;            
        }

        if (Choose_the_object_rotations == 4) 
        {
            position[0, 0] += 1;
            position[0, 1] += 1;
            
            position[2, 0] -= 1;
            position[2, 1] -= 1;
            
            position[3, 0] -= 2;
        }

        if (Choose_the_object_rotations != 1)
        {
            Choose_the_object_rotations -= 1;
        } else {
            Choose_the_object_rotations = 4;
        }

        if (check)
        {
            for (int i = 0; i < 4; i++)
            {
                if (coord[position[i, 0],position[i, 1]] == true)
                {
                    Rotate_State_1_plus(false);
                    temp = false;
                    break;
                }
            }
        }

        if (temp && check)
        {
            pos.Rotate(0, 0, 90);
        }

    }

}
