using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    private int Choose_the_object;
    private int Choose_the_object_rotations;

    public int NumberOfSkins = 1;
    public GameObject SimpleBlock;

    public PredictionScript PObject;
    public ScoreScript SScript;

    public float Move_interval = 1f;
    public float Move_intervalWithAcc = 0.1f;

    public float SideMove = 0.1f;

    private bool[,] coord = new bool[16, 25];
    private int[,] position = new int[4, 2];
    private GameObject[,] objects = new GameObject[10, 23];
    private GameObject[] blocks = new GameObject[4];

    private int[] NextObject = new int[2];

    private float Step = 1f;

    private Coroutine _Move_Left;
    private Coroutine _Move_Right;
    private Coroutine _Move_Down;

    private bool SC = false;
    private bool SCL = false;
    private bool SCR = false;

    private bool CheckAccel = false;

    void Start()
    {
        Choose_the_object_rotations = 1;

        for(int i = 0; i < 16; i++)
        {
            coord[i, 0] = true;
            coord[i, 1] = true;
        }
        
        for(int i = 0; i < 25; i++)
        {
            coord[0, i] = true;
            coord[1, i] = true;
            coord[2, i] = true;
            coord[13, i] = true;
            coord[14, i] = true;
            coord[15, i] = true;
        }

        NextObject[0] = Random.Range(1, 7);
        NextObject[1] = Random.Range(1, 5);

        //NextObject[0] = 2;
        //NextObject[1] = 2;

        Init_State();
    }

    private void Init_State()
    {   
        Choose_the_object = NextObject[0];
        Choose_the_object_rotations = NextObject[1];

        PredictionObject();

        if (Choose_the_object == 1)
        {
            if (Choose_the_object_rotations % 2 == 1)
            {
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
                if (Choose_the_object_rotations == 2)
                {
                    position[0, 0] = 6;
                    position[0, 1] = 22;

                    position[1, 0] = 6;
                    position[1, 1] = 21;

                    position[2, 0] = 6;
                    position[2, 1] = 20;

                    position[3, 0] = 5;
                    position[3, 1] = 20;

                } else
                {
                    position[0, 0] = 5;
                    position[0, 1] = 20;

                    position[1, 0] = 5;
                    position[1, 1] = 21;

                    position[2, 0] = 5;
                    position[2, 1] = 22;

                    position[3, 0] = 6;
                    position[3, 1] = 22;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                blocks[i] = Instantiate(SimpleBlock, new Vector2(position[i, 0] * Step - Step/2, position[i, 1] * Step - Step/2), Quaternion.identity);
            }
        }

        if (Choose_the_object == 2)
        {
            if (Choose_the_object_rotations % 2 == 1)
            {
                if (Choose_the_object_rotations == 1)
                {
                    position[0, 0] = 4;
                    position[1, 0] = 5;
                    position[2, 0] = 6;
                    position[3, 0] = 7;

                    for (int i = 0; i < 4; i++)
                    {
                        position[i, 1] = 21;
                    }
                } else
                {
                    position[0, 0] = 7;
                    position[1, 0] = 6;
                    position[2, 0] = 5;
                    position[3, 0] = 4;

                    for (int i = 0; i < 4; i++)
                    {
                        position[i, 1] = 20;
                    }
                }

            } else
            {
                if (Choose_the_object_rotations == 2)
                {
                    position[0, 1] = 20;
                    position[1, 1] = 21;
                    position[2, 1] = 22;
                    position[3, 1] = 23;

                    for (int i = 0; i < 4; i++)
                    {
                        position[i, 0] = 6;
                    }

                } else
                {
                    position[0, 1] = 23;
                    position[1, 1] = 22;
                    position[2, 1] = 21;
                    position[3, 1] = 20;

                    for (int i = 0; i < 4; i++)
                    {
                        position[i, 0] = 5;
                    }
                }
            }
            for (int i = 0; i < 4; i++)
            {
                blocks[i] = Instantiate(SimpleBlock, new Vector2(position[i, 0] * Step - Step/2, position[i, 1] * Step - Step/2), Quaternion.identity);
            }
        }

        if (Choose_the_object == 3)
        {
            if (Choose_the_object_rotations < 3)
            {
                position[2, 0] = 6;
                position[2, 1] = 21; 

                if (Choose_the_object_rotations == 1)
                {
                    position[0, 0] = 5;
                    position[0, 1] = 22;

                    position[1, 0] = 5;
                    position[1, 1] = 21;

                    position[3, 0] = 6;
                    position[3, 1] = 20;

                } else
                {
                    position[0, 0] = 7;
                    position[0, 1] = 22;

                    position[1, 0] = 6;
                    position[1, 1] = 22;

                    position[3, 0] = 5;
                    position[3, 1] = 21;


                }

            } else if (Choose_the_object_rotations == 3)
            {
                position[0, 0] = 6;
                position[0, 1] = 20;

                position[1, 0] = 6;
                position[1, 1] = 21;

                position[2, 0] = 5;
                position[2, 1] = 21;

                position[3, 0] = 5;
                position[3, 1] = 22;

            } else 
            {
                position[0, 0] = 5;
                position[0, 1] = 20;

                position[1, 0] = 6;
                position[1, 1] = 20;

                position[2, 0] = 6;
                position[2, 1] = 21;

                position[3, 0] = 7;
                position[3, 1] = 21;
            }

            for (int i = 0; i < 4; i++)
            {
                blocks[i] = Instantiate(SimpleBlock, new Vector2(position[i, 0] * Step - Step/2, position[i, 1] * Step - Step/2), Quaternion.identity);
            }
        }

        if (Choose_the_object == 4)
        {     
            position[0, 0] = 5;
            position[0, 1] = 21;

            position[1, 0] = 5;
            position[1, 1] = 22;

            position[2, 0] = 6;
            position[2, 1] = 21;

            position[3, 0] = 6;
            position[3, 1] = 22;

            for (int i = 0; i < 4; i++)
            {
                blocks[i] = Instantiate(SimpleBlock, new Vector2(position[i, 0] * Step - Step/2, position[i, 1] * Step - Step/2), Quaternion.identity);
            }
        }

        if (Choose_the_object == 5)
        {
            if (Choose_the_object_rotations % 2 == 1)
            {
                position[0, 0] = 4 + Choose_the_object_rotations;
                position[0, 1] = 21;

                position[1, 0] = 6;
                position[1, 1] = 23 - Choose_the_object_rotations;

                position[2, 0] = 6;
                position[2, 1] = 21;

                position[3, 0] = 8 - Choose_the_object_rotations;
                position[3, 1] = 21;

            } else if (Choose_the_object_rotations == 2)
            {
                position[0, 0] = 5;
                position[0, 1] = 22;

                position[1, 0] = 6;
                position[1, 1] = 21;

                position[2, 0] = 5;
                position[2, 1] = 21;

                position[3, 0] = 5;
                position[3, 1] = 20;
            } else
            {  
                position[0, 0] = 6;
                position[0, 1] = 20;

                position[1, 0] = 5;
                position[1, 1] = 21;

                position[2, 0] = 6;
                position[2, 1] = 21;

                position[3, 0] = 6;
                position[3, 1] = 22;
            }

            for (int i = 0; i < 4; i++)
            {
                blocks[i] = Instantiate(SimpleBlock, new Vector2(position[i, 0] * Step - Step/2, position[i, 1] * Step - Step/2), Quaternion.identity);
            }
        }

        if (Choose_the_object == 6)
        {
            if (Choose_the_object_rotations % 2 == 1)
            {
                position[0, 0] = 7 - Choose_the_object_rotations;
                position[0, 1] = 21;

                position[1, 0] = 5;
                position[1, 1] = 21;

                position[2, 0] = 3 + Choose_the_object_rotations;
                position[2, 1] = 21;

                position[3, 0] = 3 + Choose_the_object_rotations;
                position[3, 1] = 19 + Choose_the_object_rotations;

            } else
            {
                if (Choose_the_object_rotations == 2)
                {
                    position[0, 0] = 6;
                    position[0, 1] = 20;

                    position[1, 0] = 6;
                    position[1, 1] = 21;

                    position[2, 0] = 6;
                    position[2, 1] = 22;

                    position[3, 0] = 5;
                    position[3, 1] = 22;

                } else
                {
                    position[0, 0] = 5;
                    position[0, 1] = 22;

                    position[1, 0] = 5;
                    position[1, 1] = 21;

                    position[2, 0] = 5;
                    position[2, 1] = 20;

                    position[3, 0] = 6;
                    position[3, 1] = 20;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                blocks[i] = Instantiate(SimpleBlock, new Vector2(position[i, 0] * Step - Step/2, position[i, 1] * Step - Step/2), Quaternion.identity);
            }            
        }

        if (Choose_the_object == 7)
        {
            if (Choose_the_object_rotations < 3)
            {
                position[2, 0] = 5;
                position[2, 1] = 21; 

                if (Choose_the_object_rotations == 1)
                {
                    position[0, 0] = 6;
                    position[0, 1] = 22;

                    position[1, 0] = 6;
                    position[1, 1] = 21;

                    position[3, 0] = 5;
                    position[3, 1] = 20;

                } else
                {
                    position[0, 0] = 6;
                    position[0, 1] = 20;

                    position[1, 0] = 5;
                    position[1, 1] = 20;

                    position[3, 0] = 4;
                    position[3, 1] = 21;


                }

            } else if (Choose_the_object_rotations == 3)
            {
                position[0, 0] = 5;
                position[0, 1] = 20;

                position[1, 0] = 5;
                position[1, 1] = 21;

                position[2, 0] = 6;
                position[2, 1] = 21;

                position[3, 0] = 6;
                position[3, 1] = 22;

            } else 
            {
                position[0, 0] = 4;
                position[0, 1] = 22;

                position[1, 0] = 5;
                position[1, 1] = 22;

                position[2, 0] = 5;
                position[2, 1] = 21;

                position[3, 0] = 6;
                position[3, 1] = 21;
            }

            for (int i = 0; i < 4; i++)
            {
                blocks[i] = Instantiate(SimpleBlock, new Vector2(position[i, 0] * Step - Step/2, position[i, 1] * Step - Step/2), Quaternion.identity);
            }            
        }

        if (SC)
        {
            StopCoroutine(_Move_Down);
        }

        _Move_Down = StartCoroutine(Move_down());
        SC = true;        
    }

    private void PredictionObject()
    {
        //NextObject[0] = 1;
        NextObject[0] = Random.Range(1, 7);
        NextObject[1] = Random.Range(1, 5);

        PObject.ChangePredictionObject(NextObject[0], NextObject[1]);
    }

    void Update()
    {
        // For developmers::

        if (Input.GetKeyDown("1"))
            NextObject[0] = 1;

        if (Input.GetKeyDown("2"))
            NextObject[0] = 2;

        if (Input.GetKeyDown("3"))
            NextObject[0] = 3;

        if (Input.GetKeyDown("4"))
            NextObject[0] = 4;

        if (Input.GetKeyDown("5"))
            NextObject[0] = 5;

        if (Input.GetKeyDown("6"))
            NextObject[0] = 6;

        if (Input.GetKeyDown("7"))
            NextObject[0] = 7;
        

        if (Input.GetKeyDown("u"))
            NextObject[1] = 1;

        if (Input.GetKeyDown("i"))
            NextObject[1] = 2;

        if (Input.GetKeyDown("o"))
            NextObject[1] = 3;

        if (Input.GetKeyDown("p"))
            NextObject[1] = 4;

        // For developmers::
        if (Input.GetKeyDown("e"))
        {
            if (Choose_the_object == 1)
                Rotate_State_1_plus();

            if (Choose_the_object == 2)
                Rotate_State_2_plus();

            if (Choose_the_object == 3)
                Rotate_State_3_plus();

            if (Choose_the_object == 5)
                Rotate_State_5_plus();

            if (Choose_the_object == 6)
                Rotate_State_6_plus();

            if (Choose_the_object == 7)
                Rotate_State_7_plus();

        }

        if (Input.GetKeyDown("q"))
        {
            if (Choose_the_object == 1)
                Rotate_State_1_minus();

            if (Choose_the_object == 2)
                Rotate_State_2_minus();

            if (Choose_the_object == 3)
                Rotate_State_3_minus();
            
            if (Choose_the_object == 5)
                Rotate_State_5_minus();

            if (Choose_the_object == 6)
                Rotate_State_6_minus();

            if (Choose_the_object == 7)
                Rotate_State_7_minus();

        }

        if (Input.GetKey("a"))
        {   
            if (!SCL)
                _Move_Left = StartCoroutine(Move_Left());

        } else if (SCL)
        {
            StopCoroutine(_Move_Left);
            SCL = false;
        }
        
        if (Input.GetKey("d"))
        {   
            if (!SCR)
                _Move_Right = StartCoroutine(Move_Right());

        } else if (SCR)
        {
            StopCoroutine(_Move_Right);
            SCR = false;
        }

        if (Input.GetKeyDown("s"))
        {
            bool temp = false;

            for (int i = 0; i < 4; i++)
            {
                if (coord[position[i, 0] + 2, position[i, 1]] == true)
                {
                    temp = true;
                    
                    for (int j = 0; j < 4; j++)
                    {
                        coord[position[j, 0] + 2, position[j, 1] + 1] = true;
                        objects[position[j, 0] - 1, position[j, 1] - 1] = blocks[j];
                    }

                    LineDeleting();
                    
                    break;
                } 
            }
            if (temp)
            {
                Init_State();
            } else
            {
                SScript.AddingPoints(1);
                for (int i = 0; i < 4; i++)
                {
                    position[i, 1] -= 1;
                    blocks[i].transform.position = blocks[i].transform.position + new Vector3(0, -Step, 0);
                }
            }

            CheckAccel = true;
            Move_interval /= 10;
        }

        if (Input.GetKeyUp("s"))
        {
            CheckAccel = false;
            Move_interval *= 10;
        }
    }

    private IEnumerator Move_Left()
    {
        bool temp = true;

        SCL = true;

        for (int i = 0; i < 4; i++)
        {
            if (coord[position[i, 0] + 1, position[i, 1] + 1] == true)
            {
                temp = false;
                break;
            }
        }
        
        if (temp)
        {
            for (int i = 0; i < 4; i++)
            {
                position[i, 0] -= 1;
                blocks[i].transform.position = blocks[i].transform.position + new Vector3(-Step, 0, 0);
            }
            
        }

        yield return new WaitForSeconds(0.3f);

        while (true)
        {
            temp = true;

            for (int i = 0; i < 4; i++)
            {
                if (coord[position[i, 0] + 1, position[i, 1] + 1] == true)
                {
                    temp = false;
                    break;
                }
            }
            
            if (temp)
            {
                for (int i = 0; i < 4; i++)
                {
                    position[i, 0] -= 1;
                    blocks[i].transform.position = blocks[i].transform.position + new Vector3(-Step, 0, 0);
                }
            }

            yield return new WaitForSeconds(SideMove);
        }
    }

    private IEnumerator Move_Right()
    {
        bool temp = true;

        SCR = true;

        for (int i = 0; i < 4; i++)
        {
            if (coord[position[i, 0] + 3, position[i, 1] + 1] == true)
            {
                temp = false;
                break;
            }
        }
        
        if (temp)
        {
            for (int i = 0; i < 4; i++)
            {
                position[i, 0] += 1;
                blocks[i].transform.position = blocks[i].transform.position + new Vector3(Step, 0, 0);
            }
        }

        yield return new WaitForSeconds(0.3f);
        
        while (true)
        {
            temp = true;

            for (int i = 0; i < 4; i++)
            {
                if (coord[position[i, 0] + 3, position[i, 1] + 1] == true)
                {
                    temp = false;
                    break;
                }
            }
            
            if (temp)
            {
                for (int i = 0; i < 4; i++)
                {
                    position[i, 0] += 1;
                    blocks[i].transform.position = blocks[i].transform.position + new Vector3(Step, 0, 0);
                }
            }

            yield return new WaitForSeconds(SideMove);
        }
    }
    
    private IEnumerator Move_down()
    {
        bool temp = false;

        yield return new WaitForSeconds(Move_interval);
        while(true)
        {
            if (CheckAccel)
            {
                SScript.AddingPoints(1);
            }
            for (int i = 0; i < 4; i++)
            {
                position[i, 1] -= 1;
                blocks[i].transform.position = blocks[i].transform.position + new Vector3(0, -Step, 0);
            }
            
            yield return new WaitForSeconds(Move_interval);

            for (int i = 0; i < 4; i++)
            {
                if (coord[position[i, 0] + 2, position[i, 1]] == true)
                {
                    temp = true;
                    
                    for (int j = 0; j < 4; j++)
                    {
                        coord[position[j, 0] + 2, position[j, 1] + 1] = true;
                        objects[position[j, 0] - 1, position[j, 1] - 1] = blocks[j];
                    }

                    LineDeleting();
                    
                    break;
                } 
            }
            if (temp)
            {
                Init_State();
                yield break;
            }
        }
    }

    private void LineDeleting()
    {
        int GlobalNumber = 0;

        for (int k = 0; k < 4; k++)
        {
            bool FullLine = true;

            for (int k1 = 1; k1 < 11; k1++)
            {
                if (coord[k1 + 2, position[k, 1] + 1] == false)
                {
                    FullLine = false;
                    break;
                }    
            }

            if (FullLine)
            {
                bool check = true;
                GlobalNumber += 1;

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
                        coord[ix + 2, iy + 1] = coord[ix + 2, iy + 2];

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

        if (GlobalNumber != 0)
        {
            if (GlobalNumber == 1)
            {
                SScript.AddingPoints(100);
            } else if (GlobalNumber == 2)
            {
                SScript.AddingPoints(300);
            } else if (GlobalNumber == 3)
            {
                SScript.AddingPoints(500);
            } else if (GlobalNumber == 4)
            {
                SScript.AddingPoints(800);
            }
        }
    }


    private void Rotate_State_1_plus()
    {
        int[,] interim = new int[4, 2]; 

        bool checkcollid = true;

        if (Choose_the_object_rotations == 1) 
        {
            interim[0, 0] = position[0, 0] + 1;
            interim[0, 1] = position[0, 1] + 1;

            interim[1, 0] = position[1, 0];
            interim[1, 1] = position[1, 1];
            
            interim[2, 0] = position[2, 0] - 1;
            interim[2, 1] = position[2, 1] - 1;
            
            interim[3, 0] = position[3, 0] - 2;
            interim[3, 1] = position[3, 1];
        }

        if (Choose_the_object_rotations == 2) 
        {
            interim[0, 0] = position[0, 0] + 1;
            interim[0, 1] = position[0, 1] - 1;

            interim[1, 0] = position[1, 0];
            interim[1, 1] = position[1, 1];
            
            interim[2, 0] = position[2, 0] - 1;
            interim[2, 1] = position[2, 1] + 1;
            
            interim[3, 0] = position[3, 0];
            interim[3, 1] = position[3, 1] + 2;
        }

        if (Choose_the_object_rotations == 3) 
        { 
            interim[0, 0] = position[0, 0] - 1;
            interim[0, 1] = position[0, 1] - 1;

            interim[1, 0] = position[1, 0];
            interim[1, 1] = position[1, 1];
            
            interim[2, 0] = position[2, 0] + 1;
            interim[2, 1] = position[2, 1] + 1;
            
            interim[3, 0] = position[3, 0] + 2;
            interim[3, 1] = position[3, 1];
        }

        if (Choose_the_object_rotations == 4) 
        {
            interim[0, 0] = position[0, 0] - 1;
            interim[0, 1] = position[0, 1] + 1;

            interim[1, 0] = position[1, 0];
            interim[1, 1] = position[1, 1];
            
            interim[2, 0] = position[2, 0] + 1;
            interim[2, 1] = position[2, 1] - 1;
            
            interim[3, 0] = position[3, 0];
            interim[3, 1] = position[3, 1] - 2;
        }

        for (int i = 0; i < 4; i++)
        {
            if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
            {
                checkcollid = false;
            }
        }

        if (!checkcollid && Choose_the_object_rotations == 2)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] -= 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            } 
        }

        if (!checkcollid && Choose_the_object_rotations == 4)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] += 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            } 
        }

        if (checkcollid)
        {
            for (int i = 0; i < 4; i++)
            {
                blocks[i].transform.position = blocks[i].transform.position + new Vector3((interim[i, 0] - position[i, 0]) * Step, (interim[i, 1] - position[i, 1]) * Step, 0);

                position[i, 0] = interim[i, 0];
                position[i, 1] = interim[i, 1];
            }

            if (Choose_the_object_rotations != 4)
            {
                Choose_the_object_rotations += 1;
            } else {
                Choose_the_object_rotations = 1;
            }
        }



    }

    private void Rotate_State_1_minus()
    {       
        int[,] interim = new int[4, 2];

        bool checkcollid = true;

        if (Choose_the_object_rotations == 1) 
        {
            interim[0, 0] = position[0, 0] + 1;
            interim[0, 1] = position[0, 1] - 1;

            interim[1, 0] = position[1, 0];
            interim[1, 1] = position[1, 1];
            
            interim[2, 0] = position[2, 0] - 1;
            interim[2, 1] = position[2, 1] + 1;
            
            interim[3, 0] = position[3, 0];
            interim[3, 1] = position[3, 1] + 2;
        }

        if (Choose_the_object_rotations == 2) 
        {
            interim[0, 0] = position[0, 0] - 1;
            interim[0, 1] = position[0, 1] - 1;

            interim[1, 0] = position[1, 0];
            interim[1, 1] = position[1, 1];
            
            interim[2, 0] = position[2, 0] + 1;
            interim[2, 1] = position[2, 1] + 1;
            
            interim[3, 0] = position[3, 0] + 2;
            interim[3, 1] = position[3, 1];
        }

        if (Choose_the_object_rotations == 3) 
        {
            interim[0, 0] = position[0, 0] - 1;
            interim[0, 1] = position[0, 1] + 1;

            interim[1, 0] = position[1, 0];
            interim[1, 1] = position[1, 1];
            
            interim[2, 0] = position[2, 0] + 1;
            interim[2, 1] = position[2, 1] - 1;
            
            interim[3, 0] = position[3, 0];
            interim[3, 1] = position[3, 1] - 2;    
        }

        if (Choose_the_object_rotations == 4) 
        {
            interim[0, 0] = position[0, 0] + 1;
            interim[0, 1] = position[0, 1] + 1;

            interim[1, 0] = position[1, 0];
            interim[1, 1] = position[1, 1];
            
            interim[2, 0] = position[2, 0] - 1;
            interim[2, 1] = position[2, 1] - 1;
            
            interim[3, 0] = position[3, 0] - 2;
            interim[3, 1] = position[3, 1];
        }

        for (int i = 0; i < 4; i++)
        {
            if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
            {
                checkcollid = false;
            }
        }

        if (!checkcollid && Choose_the_object_rotations == 4)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] += 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            } 
        }

        if (!checkcollid && Choose_the_object_rotations == 2)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] -= 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            } 
        }

        if (checkcollid)
        {
            for (int i = 0; i < 4; i++)
            {
                blocks[i].transform.position = blocks[i].transform.position + new Vector3((interim[i, 0] - position[i, 0]) * Step, (interim[i, 1] - position[i, 1]) * Step, 0);

                position[i, 0] = interim[i, 0];
                position[i, 1] = interim[i, 1];
            }
            
            if (Choose_the_object_rotations != 1)
            {
                Choose_the_object_rotations -= 1;
            } else {
                Choose_the_object_rotations = 4;
            }
        }

    }

    private void Rotate_State_2_plus()
    {
        int[,] interim = new int[4, 2];

        bool checkcollid = true;

        if (Choose_the_object_rotations == 1) 
        {
            interim[0, 0] = position[0, 0] + 2;
            interim[0, 1] = position[0, 1] - 2;

            interim[1, 0] = position[1, 0] + 1;
            interim[1, 1] = position[1, 1] - 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] - 1;
            interim[3, 1] = position[3, 1] + 1;
        }

        if (Choose_the_object_rotations == 2) 
        {
            interim[0, 0] = position[0, 0] + 1;
            interim[0, 1] = position[0, 1] + 1;

            interim[1, 0] = position[1, 0];
            interim[1, 1] = position[1, 1];
            
            interim[2, 0] = position[2, 0] - 1;
            interim[2, 1] = position[2, 1] - 1;
            
            interim[3, 0] = position[3, 0] - 2;
            interim[3, 1] = position[3, 1] - 2;
        }

        if (Choose_the_object_rotations == 3) 
        {
            interim[0, 0] = position[0, 0] - 2;
            interim[0, 1] = position[0, 1] + 2;

            interim[1, 0] = position[1, 0] - 1;
            interim[1, 1] = position[1, 1] + 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] + 1;
            interim[3, 1] = position[3, 1] - 1;  
        }

        if (Choose_the_object_rotations == 4) 
        {
            interim[0, 0] = position[0, 0] - 1;
            interim[0, 1] = position[0, 1] - 1;

            interim[1, 0] = position[1, 0];
            interim[1, 1] = position[1, 1];
            
            interim[2, 0] = position[2, 0] + 1;
            interim[2, 1] = position[2, 1] + 1;
            
            interim[3, 0] = position[3, 0] + 2;
            interim[3, 1] = position[3, 1] + 2;
        }

        for (int i = 0; i < 4; i++)
        {
            if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
            {
                checkcollid = false;
            }
        }

        if (!checkcollid && Choose_the_object_rotations == 2)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] -= 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            }

            if (!checkcollid)
            {
                checkcollid = true;

                for (int i = 0; i < 4; i++)
                {
                    interim[i, 0] += 3; 

                    if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                    {
                        checkcollid = false;
                    }
                }
            } 
        }

        if (!checkcollid && Choose_the_object_rotations == 4)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] += 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            }

            if (!checkcollid)
            {
                checkcollid = true;

                for (int i = 0; i < 4; i++)
                {
                    interim[i, 0] -= 3; 

                    if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                    {
                        checkcollid = false;
                    }
                }
            } 
        }

        if (checkcollid)
        {
            for (int i = 0; i < 4; i++)
            {
                blocks[i].transform.position = blocks[i].transform.position + new Vector3((interim[i, 0] - position[i, 0]) * Step, (interim[i, 1] - position[i, 1]) * Step, 0);

                position[i, 0] = interim[i, 0];
                position[i, 1] = interim[i, 1];
            }
            
            if (Choose_the_object_rotations != 4)
            {
                Choose_the_object_rotations += 1;
            } else {
                Choose_the_object_rotations = 1;
            }
        }
    }

    private void Rotate_State_2_minus()
    {       
        int[,] interim = new int[4, 2];

        bool checkcollid = true;

        if (Choose_the_object_rotations == 1) 
        {
            interim[0, 0] = position[0, 0] + 1;
            interim[0, 1] = position[0, 1] + 1;

            interim[1, 0] = position[1, 0];
            interim[1, 1] = position[1, 1];
            
            interim[2, 0] = position[2, 0] - 1;
            interim[2, 1] = position[2, 1] - 1;
            
            interim[3, 0] = position[3, 0] - 2;
            interim[3, 1] = position[3, 1] - 2;
        }

        if (Choose_the_object_rotations == 2) 
        {
            interim[0, 0] = position[0, 0] - 2;
            interim[0, 1] = position[0, 1] + 2;

            interim[1, 0] = position[1, 0] - 1;
            interim[1, 1] = position[1, 1] + 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] + 1;
            interim[3, 1] = position[3, 1] - 1;
        }

        if (Choose_the_object_rotations == 3) 
        {
            interim[0, 0] = position[0, 0] - 1;
            interim[0, 1] = position[0, 1] - 1;

            interim[1, 0] = position[1, 0];
            interim[1, 1] = position[1, 1];
            
            interim[2, 0] = position[2, 0] + 1;
            interim[2, 1] = position[2, 1] + 1;
            
            interim[3, 0] = position[3, 0] + 2;
            interim[3, 1] = position[3, 1] + 2;           
        }

        if (Choose_the_object_rotations == 4) 
        {
            interim[0, 0] = position[0, 0] + 2;
            interim[0, 1] = position[0, 1] - 2;

            interim[1, 0] = position[1, 0] + 1;
            interim[1, 1] = position[1, 1] - 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] - 1;
            interim[3, 1] = position[3, 1] + 1;
        }

        for (int i = 0; i < 4; i++)
        {
            if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
            {
                checkcollid = false;
            }
        }

        if (!checkcollid && Choose_the_object_rotations == 2)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] -= 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            }

            if (!checkcollid)
            {
                checkcollid = true;

                for (int i = 0; i < 4; i++)
                {
                    interim[i, 0] += 3; 

                    if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                    {
                        checkcollid = false;
                    }
                }
            } 
        }

        if (!checkcollid && Choose_the_object_rotations == 4)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] += 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            }

            if (!checkcollid)
            {
                checkcollid = true;

                for (int i = 0; i < 4; i++)
                {
                    interim[i, 0] -= 3; 

                    if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                    {
                        checkcollid = false;
                    }
                }
            } 
        }

        if (checkcollid)
        {
            for (int i = 0; i < 4; i++)
            {
                blocks[i].transform.position = blocks[i].transform.position + new Vector3((interim[i, 0] - position[i, 0]) * Step, (interim[i, 1] - position[i, 1]) * Step, 0);

                position[i, 0] = interim[i, 0];
                position[i, 1] = interim[i, 1];
            }
            
            if (Choose_the_object_rotations != 1)
            {
                Choose_the_object_rotations -= 1;
            } else {
                Choose_the_object_rotations = 4;
            }
        }
    }

    private void Rotate_State_3_plus()
    {
        int[,] interim = new int[4, 2];

        bool checkcollid = true;

        if (Choose_the_object_rotations == 1) 
        {
            interim[0, 0] = position[0, 0] + 2;
            interim[0, 1] = position[0, 1];

            interim[1, 0] = position[1, 0] + 1;
            interim[1, 1] = position[1, 1] + 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] - 1;
            interim[3, 1] = position[3, 1] + 1;
        }

        if (Choose_the_object_rotations == 2) 
        {
            interim[0, 0] = position[0, 0];
            interim[0, 1] = position[0, 1] - 2 ;

            interim[1, 0] = position[1, 0] + 1;
            interim[1, 1] = position[1, 1] - 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] + 1;
            interim[3, 1] = position[3, 1] + 1;
        }

        if (Choose_the_object_rotations == 3) 
        {
            interim[0, 0] = position[0, 0] - 2;
            interim[0, 1] = position[0, 1];

            interim[1, 0] = position[1, 0] - 1;
            interim[1, 1] = position[1, 1] - 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] + 1;
            interim[3, 1] = position[3, 1] - 1;     
        }

        if (Choose_the_object_rotations == 4) 
        {
            interim[0, 0] = position[0, 0];
            interim[0, 1] = position[0, 1] + 2;

            interim[1, 0] = position[1, 0] - 1;
            interim[1, 1] = position[1, 1] + 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] - 1;
            interim[3, 1] = position[3, 1] - 1;
        }

        for (int i = 0; i < 4; i++)
        {
            if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
            {
                checkcollid = false;
            }
        }

        if (!checkcollid && Choose_the_object_rotations == 1)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] -= 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            }
        }

        if (!checkcollid && Choose_the_object_rotations == 3)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] += 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            }
        }

        if (checkcollid)
        {
            for (int i = 0; i < 4; i++)
            {
                blocks[i].transform.position = blocks[i].transform.position + new Vector3((interim[i, 0] - position[i, 0]) * Step, (interim[i, 1] - position[i, 1]) * Step, 0);

                position[i, 0] = interim[i, 0];
                position[i, 1] = interim[i, 1];
            }
            
            if (Choose_the_object_rotations != 4)
            {
                Choose_the_object_rotations += 1;
            } else {
                Choose_the_object_rotations = 1;
            }
        }
    }

    private void Rotate_State_3_minus()
    {       
        int[,] interim = new int[4, 2];

        bool checkcollid = true;

        if (Choose_the_object_rotations == 1) 
        {
            interim[0, 0] = position[0, 0];
            interim[0, 1] = position[0, 1] - 2;

            interim[1, 0] = position[1, 0] + 1;
            interim[1, 1] = position[1, 1] - 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] + 1;
            interim[3, 1] = position[3, 1] + 1;
        }

        if (Choose_the_object_rotations == 2) 
        {
            interim[0, 0] = position[0, 0] - 2;
            interim[0, 1] = position[0, 1];

            interim[1, 0] = position[1, 0] - 1;
            interim[1, 1] = position[1, 1] - 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] + 1;
            interim[3, 1] = position[3, 1] - 1;
        }

        if (Choose_the_object_rotations == 3) 
        {
            interim[0, 0] = position[0, 0];
            interim[0, 1] = position[0, 1] + 2;

            interim[1, 0] = position[1, 0] - 1;
            interim[1, 1] = position[1, 1] + 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] - 1;
            interim[3, 1] = position[3, 1] - 1;           
        }

        if (Choose_the_object_rotations == 4) 
        {
            interim[0, 0] = position[0, 0] + 2;
            interim[0, 1] = position[0, 1];

            interim[1, 0] = position[1, 0] + 1;
            interim[1, 1] = position[1, 1] + 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] - 1;
            interim[3, 1] = position[3, 1] + 1;
        }

        for (int i = 0; i < 4; i++)
        {
            if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
            {
                checkcollid = false;
            }
        }

        if (!checkcollid && Choose_the_object_rotations == 1)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] -= 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            }
        }

        if (!checkcollid && Choose_the_object_rotations == 3)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] += 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            }
        }

        if (checkcollid)
        {
            for (int i = 0; i < 4; i++)
            {
                blocks[i].transform.position = blocks[i].transform.position + new Vector3((interim[i, 0] - position[i, 0]) * Step, (interim[i, 1] - position[i, 1]) * Step, 0);

                position[i, 0] = interim[i, 0];
                position[i, 1] = interim[i, 1];
            }
            
            if (Choose_the_object_rotations != 1)
            {
                Choose_the_object_rotations -= 1;
            } else {
                Choose_the_object_rotations = 4;
            }
        }
    }

    private void Rotate_State_5_plus()
    {
        int[,] interim = new int[4, 2];

        bool checkcollid = true;

        if (Choose_the_object_rotations == 1) 
        {
            interim[0, 0] = position[0, 0] + 1;
            interim[0, 1] = position[0, 1] + 1;

            interim[1, 0] = position[1, 0] + 1;
            interim[1, 1] = position[1, 1] - 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] - 1;
            interim[3, 1] = position[3, 1] - 1;
        }

        if (Choose_the_object_rotations == 2) 
        {
            interim[0, 0] = position[0, 0] + 1;
            interim[0, 1] = position[0, 1] - 1;

            interim[1, 0] = position[1, 0] - 1;
            interim[1, 1] = position[1, 1] - 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] - 1;
            interim[3, 1] = position[3, 1] + 1;
        }

        if (Choose_the_object_rotations == 3) 
        {
            interim[0, 0] = position[0, 0] - 1;
            interim[0, 1] = position[0, 1] - 1;

            interim[1, 0] = position[1, 0] - 1;
            interim[1, 1] = position[1, 1] + 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] + 1;
            interim[3, 1] = position[3, 1] + 1;       
        }

        if (Choose_the_object_rotations == 4) 
        {
            interim[0, 0] = position[0, 0] - 1;
            interim[0, 1] = position[0, 1] + 1;

            interim[1, 0] = position[1, 0] + 1;
            interim[1, 1] = position[1, 1] + 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] + 1;
            interim[3, 1] = position[3, 1] - 1;
        }

        for (int i = 0; i < 4; i++)
        {
            if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
            {
                checkcollid = false;
            }
        }

        if (!checkcollid && Choose_the_object_rotations == 2)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] += 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            }
        }

        if (!checkcollid && Choose_the_object_rotations == 4)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] -= 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            }
        }

        if (checkcollid)
        {
            for (int i = 0; i < 4; i++)
            {
                blocks[i].transform.position = blocks[i].transform.position + new Vector3((interim[i, 0] - position[i, 0]) * Step, (interim[i, 1] - position[i, 1]) * Step, 0);

                position[i, 0] = interim[i, 0];
                position[i, 1] = interim[i, 1];
            }
            
            if (Choose_the_object_rotations != 4)
            {
                Choose_the_object_rotations += 1;
            } else {
                Choose_the_object_rotations = 1;
            }
        }
    }

    private void Rotate_State_5_minus()
    {       
        int[,] interim = new int[4, 2];

        bool checkcollid = true;

        if (Choose_the_object_rotations == 1) 
        {
            interim[0, 0] = position[0, 0] + 1;
            interim[0, 1] = position[0, 1] - 1;

            interim[1, 0] = position[1, 0] - 1;
            interim[1, 1] = position[1, 1] - 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] - 1;
            interim[3, 1] = position[3, 1] + 1;
        }

        if (Choose_the_object_rotations == 2) 
        {
            interim[0, 0] = position[0, 0] - 1;
            interim[0, 1] = position[0, 1] - 1;

            interim[1, 0] = position[1, 0] - 1;
            interim[1, 1] = position[1, 1] + 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] + 1;
            interim[3, 1] = position[3, 1] + 1;
        }

        if (Choose_the_object_rotations == 3) 
        {
            interim[0, 0] = position[0, 0] - 1;
            interim[0, 1] = position[0, 1] + 1;

            interim[1, 0] = position[1, 0] + 1;
            interim[1, 1] = position[1, 1] + 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] + 1;
            interim[3, 1] = position[3, 1] - 1;         
        }

        if (Choose_the_object_rotations == 4) 
        {
            interim[0, 0] = position[0, 0] + 1;
            interim[0, 1] = position[0, 1] + 1;

            interim[1, 0] = position[1, 0] + 1;
            interim[1, 1] = position[1, 1] - 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] - 1;
            interim[3, 1] = position[3, 1] - 1;
        }

        for (int i = 0; i < 4; i++)
        {
            if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
            {
                checkcollid = false;
            }
        }

        if (!checkcollid && Choose_the_object_rotations == 2)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] += 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            }
        }

        if (!checkcollid && Choose_the_object_rotations == 4)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] -= 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            }
        }

        if (checkcollid)
        {
            for (int i = 0; i < 4; i++)
            {
                blocks[i].transform.position = blocks[i].transform.position + new Vector3((interim[i, 0] - position[i, 0]) * Step, (interim[i, 1] - position[i, 1]) * Step, 0);

                position[i, 0] = interim[i, 0];
                position[i, 1] = interim[i, 1];
            }
            
            if (Choose_the_object_rotations != 1)
            {
                Choose_the_object_rotations -= 1;
            } else {
                Choose_the_object_rotations = 4;
            }
        }
    }

    private void Rotate_State_6_plus()
    {
        int[,] interim = new int[4, 2];

        bool checkcollid = true;

        if (Choose_the_object_rotations == 1) 
        {
            interim[0, 0] = position[0, 0] - 1;
            interim[0, 1] = position[0, 1] - 1;

            interim[1, 0] = position[1, 0];
            interim[1, 1] = position[1, 1];
            
            interim[2, 0] = position[2, 0] + 1;
            interim[2, 1] = position[2, 1] + 1;
            
            interim[3, 0] = position[3, 0];
            interim[3, 1] = position[3, 1] + 2;
        }

        if (Choose_the_object_rotations == 2) 
        {
            interim[0, 0] = position[0, 0] - 1;
            interim[0, 1] = position[0, 1] + 1;

            interim[1, 0] = position[1, 0];
            interim[1, 1] = position[1, 1];
            
            interim[2, 0] = position[2, 0] + 1;
            interim[2, 1] = position[2, 1] - 1;
            
            interim[3, 0] = position[3, 0] + 2;
            interim[3, 1] = position[3, 1];
        }

        if (Choose_the_object_rotations == 3) 
        {
            interim[0, 0] = position[0, 0] + 1;
            interim[0, 1] = position[0, 1] + 1;

            interim[1, 0] = position[1, 0];
            interim[1, 1] = position[1, 1];
            
            interim[2, 0] = position[2, 0] - 1;
            interim[2, 1] = position[2, 1] - 1;
            
            interim[3, 0] = position[3, 0];
            interim[3, 1] = position[3, 1] - 2;     
        }

        if (Choose_the_object_rotations == 4) 
        {
            interim[0, 0] = position[0, 0] + 1;
            interim[0, 1] = position[0, 1] - 1;

            interim[1, 0] = position[1, 0];
            interim[1, 1] = position[1, 1];
            
            interim[2, 0] = position[2, 0] - 1;
            interim[2, 1] = position[2, 1] + 1;
            
            interim[3, 0] = position[3, 0] - 2;
            interim[3, 1] = position[3, 1];
        }

        for (int i = 0; i < 4; i++)
        {
            if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
            {
                checkcollid = false;
            }
        }

        if (!checkcollid && Choose_the_object_rotations == 2)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] -= 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            } 
        }

        if (!checkcollid && Choose_the_object_rotations == 4)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] += 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            } 
        }

        if (checkcollid)
        {
            for (int i = 0; i < 4; i++)
            {
                blocks[i].transform.position = blocks[i].transform.position + new Vector3((interim[i, 0] - position[i, 0]) * Step, (interim[i, 1] - position[i, 1]) * Step, 0);

                position[i, 0] = interim[i, 0];
                position[i, 1] = interim[i, 1];
            }
            
            if (Choose_the_object_rotations != 4)
            {
                Choose_the_object_rotations += 1;
            } else {
                Choose_the_object_rotations = 1;
            }
        }
    }

    private void Rotate_State_6_minus()
    {       
        int[,] interim = new int[4, 2];

        bool checkcollid = true;

        if (Choose_the_object_rotations == 1) 
        {
            interim[0, 0] = position[0, 0] - 1;
            interim[0, 1] = position[0, 1] + 1;

            interim[1, 0] = position[1, 0];
            interim[1, 1] = position[1, 1];
            
            interim[2, 0] = position[2, 0] + 1;
            interim[2, 1] = position[2, 1] - 1;
            
            interim[3, 0] = position[3, 0] + 2;
            interim[3, 1] = position[3, 1];
        }

        if (Choose_the_object_rotations == 2) 
        {
            interim[0, 0] = position[0, 0] + 1;
            interim[0, 1] = position[0, 1] + 1;

            interim[1, 0] = position[1, 0];
            interim[1, 1] = position[1, 1];
            
            interim[2, 0] = position[2, 0] - 1;
            interim[2, 1] = position[2, 1] - 1;
            
            interim[3, 0] = position[3, 0];
            interim[3, 1] = position[3, 1] - 2;
        }

        if (Choose_the_object_rotations == 3) 
        {
            interim[0, 0] = position[0, 0] + 1;
            interim[0, 1] = position[0, 1] - 1;

            interim[1, 0] = position[1, 0];
            interim[1, 1] = position[1, 1];
            
            interim[2, 0] = position[2, 0] - 1;
            interim[2, 1] = position[2, 1] + 1;
            
            interim[3, 0] = position[3, 0] - 2;
            interim[3, 1] = position[3, 1];           
        }

        if (Choose_the_object_rotations == 4) 
        {
            interim[0, 0] = position[0, 0] - 1;
            interim[0, 1] = position[0, 1] - 1;

            interim[1, 0] = position[1, 0];
            interim[1, 1] = position[1, 1];
            
            interim[2, 0] = position[2, 0] + 1;
            interim[2, 1] = position[2, 1] + 1;
            
            interim[3, 0] = position[3, 0];
            interim[3, 1] = position[3, 1] + 2;
        }

        for (int i = 0; i < 4; i++)
        {
            if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
            {
                checkcollid = false;
            }
        }

        if (!checkcollid && Choose_the_object_rotations == 2)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] -= 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            } 
        }

        if (!checkcollid && Choose_the_object_rotations == 4)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] += 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            } 
        }

        if (checkcollid)
        {
            for (int i = 0; i < 4; i++)
            {
                blocks[i].transform.position = blocks[i].transform.position + new Vector3((interim[i, 0] - position[i, 0]) * Step, (interim[i, 1] - position[i, 1]) * Step, 0);

                position[i, 0] = interim[i, 0];
                position[i, 1] = interim[i, 1];
            }
            
            if (Choose_the_object_rotations != 1)
            {
                Choose_the_object_rotations -= 1;
            } else {
                Choose_the_object_rotations = 4;
            }
        }
    }

    private void Rotate_State_7_plus()
    {
        int[,] interim = new int[4, 2];

        bool checkcollid = true;

        if (Choose_the_object_rotations == 1) 
        {
            interim[0, 0] = position[0, 0];
            interim[0, 1] = position[0, 1] - 2;

            interim[1, 0] = position[1, 0] - 1;
            interim[1, 1] = position[1, 1] - 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] - 1;
            interim[3, 1] = position[3, 1] + 1;
        }

        if (Choose_the_object_rotations == 2) 
        {
            interim[0, 0] = position[0, 0] - 2;
            interim[0, 1] = position[0, 1];

            interim[1, 0] = position[1, 0] - 1;
            interim[1, 1] = position[1, 1] + 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] + 1;
            interim[3, 1] = position[3, 1] + 1;
        }

        if (Choose_the_object_rotations == 3) 
        {
            interim[0, 0] = position[0, 0];
            interim[0, 1] = position[0, 1] + 2;

            interim[1, 0] = position[1, 0] + 1;
            interim[1, 1] = position[1, 1] + 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] + 1;
            interim[3, 1] = position[3, 1] - 1;       
        }

        if (Choose_the_object_rotations == 4) 
        {
            interim[0, 0] = position[0, 0] + 2;
            interim[0, 1] = position[0, 1];

            interim[1, 0] = position[1, 0] + 1;
            interim[1, 1] = position[1, 1] - 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] - 1;
            interim[3, 1] = position[3, 1] - 1;
        }

        for (int i = 0; i < 4; i++)
        {
            if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
            {
                checkcollid = false;
            }
        }

        if (!checkcollid && Choose_the_object_rotations == 1)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] += 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            }
        }

        if (!checkcollid && Choose_the_object_rotations == 3)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] -= 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            }
        }

        if (checkcollid)
        {
            for (int i = 0; i < 4; i++)
            {
                blocks[i].transform.position = blocks[i].transform.position + new Vector3((interim[i, 0] - position[i, 0]) * Step, (interim[i, 1] - position[i, 1]) * Step, 0);

                position[i, 0] = interim[i, 0];
                position[i, 1] = interim[i, 1];
            }
            
            if (Choose_the_object_rotations != 4)
            {
                Choose_the_object_rotations += 1;
            } else {
                Choose_the_object_rotations = 1;
            }
        }
    }

    private void Rotate_State_7_minus()
    {       
        int[,] interim = new int[4, 2];

        bool checkcollid = true;

        if (Choose_the_object_rotations == 1) 
        {
            interim[0, 0] = position[0, 0] - 2;
            interim[0, 1] = position[0, 1];

            interim[1, 0] = position[1, 0] - 1;
            interim[1, 1] = position[1, 1] + 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] + 1;
            interim[3, 1] = position[3, 1] + 1;
        }

        if (Choose_the_object_rotations == 2) 
        {
            interim[0, 0] = position[0, 0];
            interim[0, 1] = position[0, 1] + 2;

            interim[1, 0] = position[1, 0] + 1;
            interim[1, 1] = position[1, 1] + 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] + 1;
            interim[3, 1] = position[3, 1] - 1;
        }

        if (Choose_the_object_rotations == 3) 
        {        
            interim[0, 0] = position[0, 0] + 2;
            interim[0, 1] = position[0, 1];

            interim[1, 0] = position[1, 0] + 1;
            interim[1, 1] = position[1, 1] - 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] - 1;
            interim[3, 1] = position[3, 1] - 1;  
        }

        if (Choose_the_object_rotations == 4) 
        {
            interim[0, 0] = position[0, 0];
            interim[0, 1] = position[0, 1] - 2;

            interim[1, 0] = position[1, 0] - 1;
            interim[1, 1] = position[1, 1] - 1;
            
            interim[2, 0] = position[2, 0];
            interim[2, 1] = position[2, 1];
            
            interim[3, 0] = position[3, 0] - 1;
            interim[3, 1] = position[3, 1] + 1;
        }

        for (int i = 0; i < 4; i++)
        {
            if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
            {
                checkcollid = false;
            }
        }

        if (!checkcollid && Choose_the_object_rotations == 1)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] += 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            }
        }

        if (!checkcollid && Choose_the_object_rotations == 3)
        {
            checkcollid = true;

            for (int i = 0; i < 4; i++)
            {
                interim[i, 0] -= 1; 

                if (coord[interim[i, 0] + 2, interim[i, 1] + 1])
                {
                    checkcollid = false;
                }
            }
        }

        if (checkcollid)
        {
            for (int i = 0; i < 4; i++)
            {
                blocks[i].transform.position = blocks[i].transform.position + new Vector3((interim[i, 0] - position[i, 0]) * Step, (interim[i, 1] - position[i, 1]) * Step, 0);

                position[i, 0] = interim[i, 0];
                position[i, 1] = interim[i, 1];
            }
            
            if (Choose_the_object_rotations != 1)
            {
                Choose_the_object_rotations -= 1;
            } else {
                Choose_the_object_rotations = 4;
            }
        }
    }

}
