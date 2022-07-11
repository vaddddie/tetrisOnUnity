using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    private int Choose_the_object;
    private int Choose_the_object_rotations;

    public Animator anim;
    public Transform pos;
    public GameObject SimpleBlock;

    public float Move_interval = 1f;

    private bool[,] coord = new bool[12, 24];
    private int[,] position = new int[4, 2];
    private GameObject[,] objects = new GameObject[10, 23];
    private float Step = 0.4288888f;

    void Start()
    {
        anim = GetComponent<Animator>();
        pos = GetComponent<Transform>();

        Choose_the_object_rotations = 1;

        for(int i = 0; i < 12; i++)
        {
            coord[i, 0] = true;
        }
        
        for(int i = 0; i < 24; i++)
        {
            coord[0, i] = true;
            coord[11, i] = true;
        }

        Init_State();

    }

    private void Init_State()
    {
        Choose_the_object = 2;

        int temp = Choose_the_object_rotations;
        
        Choose_the_object_rotations = Random.Range(1, 5);
        //Choose_the_object_rotations = 2;
        anim.SetInteger("Choose_the_object", Choose_the_object);
        anim.SetInteger("Choose_the_object_rotations", Choose_the_object_rotations);

        if (Choose_the_object_rotations > temp){
            pos.Rotate(0, 0, -90 * (Choose_the_object_rotations - temp));
        }
        else
        {
            pos.Rotate(0, 0, -90 * (Choose_the_object_rotations + 4 - temp));
        }

        if (Choose_the_object == 1)
        {
            if (Choose_the_object_rotations % 2 == 1)
            {
                pos.position = new Vector2(0.202f, 3.654f - Step / 2 + Step*2);

                position[0, 0] = 5 + (Choose_the_object_rotations - 1);
                position[0, 1] = 21;

                position[1, 0] = 6;
                position[1, 1] = 21;

                position[2, 0] = 7 - (Choose_the_object_rotations - 1);
                position[2, 1] = 21;

                position[3, 0] = 7 - (Choose_the_object_rotations - 1);
                position[3, 1] = 20 + (Choose_the_object_rotations - 1);

            } else
            {
                pos.position = new Vector2(0.41644f - Step / 2, 3.439556f + Step*2);

                position[0, 0] = 6;
                position[0, 1] = 22 - (Choose_the_object_rotations - 2);

                position[1, 0] = 6;
                position[1, 1] = 21;

                position[2, 0] = 6;
                position[2, 1] = 20 + (Choose_the_object_rotations - 2);

                position[3, 0] = 5 + (Choose_the_object_rotations - 2);
                position[3, 1] = 20 + (Choose_the_object_rotations - 2);
            }
        }

        if (Choose_the_object == 2)
        {
            if (Choose_the_object_rotations % 2 == 1)
            {
                pos.position = new Vector2(0.202f - Step/2, 3.654f + Step*2);

                position[0, 0] = 4;
                position[1, 0] = 5;
                position[2, 0] = 6;
                position[3, 0] = 7;

                if (Choose_the_object_rotations == 1)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        position[i, 1] = 21;
                    }
                } else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        position[i, 1] = 20;
                    }
                }

            } else
            {
                pos.position = new Vector2(0.41644f - Step, 3.439556f + Step*5/2);

                position[0, 1] = 20;
                position[1, 1] = 21;
                position[2, 1] = 22;
                position[3, 1] = 23;

                if (Choose_the_object_rotations == 2)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        position[i, 0] = 5;
                    }
                } else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        position[i, 0] = 6;
                    }
                }
            }
        }

        StartCoroutine(Move_down());
    }

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            if (Choose_the_object == 1)
                Rotate_State_1_plus(true);

            if (Choose_the_object == 2)
                Rotate_State_2_plus(true);
        }

        if (Input.GetKeyDown("q"))
        {
            if (Choose_the_object == 1)
                Rotate_State_1_minus(true);

            if (Choose_the_object == 2)
                Rotate_State_2_minus(true);
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

        if (Input.GetKeyDown("s"))
        {
            Move_interval = Move_interval / 4;
        }

        if (Input.GetKeyUp("s"))
        {
            Move_interval = Move_interval * 4;
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
                    
                    for (int j = 0; j < 4; j++)
                    {
                        coord[position[j, 0], position[j, 1]] = true;
                        Vector2 TempPostition = new Vector2((position[j, 0] - 5) * Step - Step/2, (position[j, 1] - 11) * Step);
                        objects[position[j, 0] - 1, position[j, 1] - 1] = Instantiate(SimpleBlock, TempPostition, Quaternion.identity);
                    }

                    LineDeleting();
                    
                    break;
                } 
            }
            if (temp)
            {
                Init_State();
                break;
            }

        }
    } 

    private void LineDeleting()
    {
        for (int k = 0; k < 4; k++)
        {
            bool FullLine = true;

            for (int k1 = 1; k1 < 11; k1++)
            {
                if (coord[k1, position[k, 1]] == false)
                {
                    FullLine = false;
                    break;
                }    
            }

            if (FullLine)
            {
                bool check = true;

                for (int iy = position[k, 1]; iy < 23; iy++)
                {
                    if (check)
                    {
                        for (int ix = 1; ix < 11; ix++)
                        {
                            Destroy(objects[ix - 1, position[k, 1] - 1]);
                        }

                        check = false;
                    }

                    for (int ix = 1; ix < 11; ix++)
                    {
                        coord[ix, iy] = coord[ix, iy + 1];

                        objects[ix - 1, iy - 1] = objects[ix - 1, iy];

                        if (objects[ix - 1, iy - 1] != null)
                        {
                            objects[ix - 1, iy - 1].transform.Translate(0, -Step, 0);
                        }
                    }
                }

                k--;                     
            }
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

    private void Rotate_State_2_plus(bool check)
    {
        bool temp = true;

        if (Choose_the_object_rotations == 1) 
        {
            position[0, 0] += 2;
            position[0, 1] -= 2;
            
            position[1, 0] += 1;
            position[1, 1] -= 1;
            
            position[3, 0] -= 1;
            position[3, 1] += 1;
        }

        if (Choose_the_object_rotations == 2) 
        {
            position[0, 0] += 1;
            position[0, 1] += 1;
            
            position[2, 0] -= 1;
            position[2, 1] -= 1;
            
            position[3, 0] -= 2;
            position[3, 1] -= 2;
        }

        if (Choose_the_object_rotations == 3) 
        {
            position[0, 0] -= 2;
            position[0, 1] += 2;
            
            position[1, 0] -= 1;
            position[1, 1] += 1;
            
            position[3, 0] += 1;  
            position[3, 1] -= 1;          
        }

        if (Choose_the_object_rotations == 4) 
        {
            position[0, 0] -= 1;
            position[0, 1] -= 1;
            
            position[2, 0] += 1;
            position[2, 1] += 1;
            
            position[3, 0] += 2;
            position[3, 1] += 2;
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

    private void Rotate_State_2_minus(bool check)
    {       
        bool temp = true;

        if (Choose_the_object_rotations == 1) 
        {
            position[0, 0] += 1;
            position[0, 1] += 1;
            
            position[2, 0] -= 1;
            position[2, 1] -= 1;
            
            position[3, 0] -= 2;
            position[3, 1] -= 2;
        }

        if (Choose_the_object_rotations == 2) 
        {
            position[0, 0] -= 2;
            position[0, 1] += 2;
            
            position[1, 0] -= 1;
            position[1, 1] += 1;
            
            position[3, 0] += 1;  
            position[3, 1] -= 1; 
        }

        if (Choose_the_object_rotations == 3) 
        {
            position[0, 0] -= 1;
            position[0, 1] -= 1;
            
            position[2, 0] += 1;
            position[2, 1] += 1;
            
            position[3, 0] += 2;
            position[3, 1] += 2;           
        }

        if (Choose_the_object_rotations == 4) 
        {
            position[0, 0] += 2;
            position[0, 1] -= 2;
            
            position[1, 0] += 1;
            position[1, 1] -= 1;
            
            position[3, 0] -= 1;
            position[3, 1] += 1;
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
