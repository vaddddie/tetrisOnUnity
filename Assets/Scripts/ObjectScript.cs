using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    private int Choose_the_object;
    private int Choose_the_object_rotations;

    public int NumberOfSkins = 1;

    public Sprite[] SimpleColors = new Sprite[6];

    public GameObject SimpleBlock;
    public GameObject PredictionBlock;
    public GameObject adsMenu;
    public GameObject restartMenu;

    private int color;

    public PredictionScript PObject;
    public ScoreScript SScript;
    public LineCounterScript LCScript;
    public TimerScript TScript;
    public DeathTimerScript DTScript;
    public WritingScript writingScript;

    public float Move_interval = 1f;
    private float Move_interval_exp;
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
    private bool life;

    private bool CheckAccel = false;
    private bool adsCheck = true;

    public bool doubleClickOn = true;
    private float clickTime;
    private float clickDelay = 0.2f;

    void Start()
    {
        Move_interval_exp = Move_interval;

        adsMenu.SetActive(false);
        restartMenu.SetActive(false);

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

        SimpleBlock.GetComponent<SpriteRenderer>().sprite = SimpleColors[Random.Range(0, 6)];

        Init_State();
    }

    private void Init_State()
    {   
        Choose_the_object = NextObject[0];
        Choose_the_object_rotations = NextObject[1];

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
        }

        life = true;

        for (int i = 0; i < 4; i++)
        {
            if (coord[position[i, 0] + 2, position[i, 1] + 1] == true)
            {
                life = false;
                break;
            }
        }

        if (SC)
        {
            StopCoroutine(_Move_Down);
            SC = false;
        }

        if (life)
        {
            for (int i = 0; i < 4; i++)
            {
                blocks[i] = Instantiate(SimpleBlock, new Vector3(position[i, 0] * Step - Step/2, position[i, 1] * Step - Step/2, 100f), Quaternion.identity);
            }  


            _Move_Down = StartCoroutine(Move_down());
            SC = true;
        } else
        {
            Death();
        }

        color = Random.Range(0, 6);

        SimpleBlock.GetComponent<SpriteRenderer>().sprite = SimpleColors[color];
        PredictionBlock.GetComponent<SpriteRenderer>().sprite = SimpleColors[color];

        PredictionObject();
    }

    public void ExtraLife()
    {
        adsMenu.SetActive(false);
        DTScript.StopTimer();

        StartCoroutine(ELDeleting());
    }

    private IEnumerator ELDeleting()
    {
        for (int i = 1; i < 6; i++)
        {
            for (int iy = 0; iy < 23; iy++)
            {
                if (objects[5 - i, iy] != null)
                    Destroy(objects[5 - i, iy]);

                if (objects[4 + i, iy] != null)
                    Destroy(objects[4 + i, iy]);

            }

            yield return new WaitForSeconds(0.1f);
        }

        for(int i = 0; i < 16; i++)
        {
            coord[i, 0] = true;
            coord[i, 1] = true;

            for (int iy = 2; iy < 25; iy++)
            {
                coord[i, iy] = false;
            }
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
        
        this.enabled = true;
        TScript.StartTimer();

        if (Move_interval != Move_interval_exp)
        {
            Move_interval = Move_interval_exp;
        }

        Init_State();
    }

    private void PredictionObject()
    {
        //NextObject[0] = 1;
        NextObject[0] = Random.Range(1, 7);
        NextObject[1] = Random.Range(1, 5);

        PObject.ChangePredictionObject(NextObject[0], NextObject[1]);
    }

    private void Death()
    {
        this.enabled = false;

        TScript.StopTimer();

        if (adsCheck)
        {
            adsMenu.SetActive(true);
            DTScript.StartTimer();

            adsCheck = false;

        } else 
        {
            restartMenu.SetActive(true);
            
            writingScript.WrittingInData();
        }

    }

    private bool DoubleClick()
    {
        if (clickTime != 0 & Time.time - clickTime < clickDelay)
        {
            clickTime = 0;
            return true;
        } else
        {
            clickTime = Time.time;
            return false;
        }

    }

    private void FastMove()
    {
        bool temp = true;

        int iterator = 0;

        while (temp)
        {
            for (int i = 0; i < 4; i++)
            {
                if (coord[position[i, 0] + 2, position[i, 1]])
                {
                    temp = false;

                    for (int j = 0; j < 4; j++)
                    {
                        coord[position[j, 0] + 2, position[j, 1] + 1] = true;
                        objects[position[j, 0] - 1, position[j, 1] - 1] = blocks[j];

                        blocks[j].transform.position = blocks[j].transform.position + new Vector3(0, -Step * iterator, 0);
                    }

                    LineDeleting();
                        
                    break;
                }
            }

            if (temp)
            {
                for (int i = 0; i < 4; i++)
                {
                    position[i, 1] -= 1;
                }
            } 

            iterator++;
        }

        SScript.AddingPoints(iterator);
    }

    void Update()
    {
        // For developmers::
        if (Input.GetKeyDown("m"))
        {
            Death();
        }


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
            if (!temp)
            {
                SScript.AddingPoints(1);
                for (int i = 0; i < 4; i++)
                {
                    position[i, 1] -= 1;
                    blocks[i].transform.position = blocks[i].transform.position + new Vector3(0, -Step, 0);
                }
            }

            if (doubleClickOn & DoubleClick() & !temp)
            {
                StopCoroutine(_Move_Down);
                SC = false;

                FastMove();

            } else
            {
                CheckAccel = true;
                Move_interval /= 10;
            }


        }

        if (Input.GetKeyUp("s") & CheckAccel)
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
            yield break;
        }

        while(!temp)
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
                yield break;
            }
        }
    }

    private void LineDeleting()
    {
        int NumberOfLines = 0;
        int[] NoDL = new int[4];

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
                bool temp = true;
            
                for (int i = 0; i < NumberOfLines; i++)
                {
                    if (NoDL[i] == position[k, 1])
                    {
                        temp = false;
                    }
                }
                
                if (temp)
                {
                    NoDL[NumberOfLines] = position[k, 1];
                    NumberOfLines += 1;
                }


                // for (int ix = 0; ix < 10; ix++)
                // {
                //     Destroy(objects[ix, position[k, 1] - 1]);
                // }

                // for (int iy = position[k, 1]; iy < 23; iy++)
                // {
                //     for (int ix = 1; ix < 11; ix++)
                //     {
                //         coord[ix + 2, iy + 1] = coord[ix + 2, iy + 2];

                //         objects[ix - 1, iy - 1] = objects[ix - 1, iy];

                //         if (objects[ix - 1, iy - 1] != null)
                //         {
                //             objects[ix - 1, iy - 1].transform.Translate(0, -Step, 0);
                //         }
                //     }
                // }

                // k--;
            }
        }

        if (NumberOfLines == 0)
        {
            Init_State();
        } else
        {
            StartCoroutine(CoroutineDL(NoDL, NumberOfLines));
        }

        if (NumberOfLines == 4)
        {   
            foreach(int item in NoDL)
            {
                if (item != 0)
                {
                    for(int i = 0; i < 10; i++)
                    {
                        objects[i, item - 1].GetComponent<FlashScript>().Blessing();
                    }
                }
            }
        }
    }

    private IEnumerator CoroutineDL(int[] NoDL, int NumberOfLines)
    {
        for (int i = 1; i < 6; i++)
        {
            foreach(int item in NoDL)
            {
                if (item != 0)
                {
                    Destroy(objects[5 - i, item - 1]);
                    Destroy(objects[4 + i, item - 1]);
                }
            }

            yield return new WaitForSeconds(0.05f);
        }

        int stepNumber = 0;

        for (int iy = 1; iy < 23; iy++)
        {
            if (stepNumber == 0)
            {
                foreach(int item in NoDL)
                {
                    if (item == iy)
                    {
                        stepNumber = 1;
                    }
                }
            }

            if (stepNumber != 0)
            {
                while(true)
                {
                    bool temp = true;

                    foreach(int item in NoDL)
                    {
                        if(iy + stepNumber == item)
                        {
                            temp = false;
                            stepNumber++;
                            break;
                        }
                    }

                    if (temp)
                    {
                        break;
                    }
                }

                for (int ix = 1; ix < 11; ix++)
                {
                    if (iy + stepNumber < 24)
                    {
                        coord[ix + 2, iy + 1] = coord[ix + 2, iy + 1 + stepNumber];
                        objects[ix - 1, iy - 1] = objects[ix - 1, iy - 1 + stepNumber];

                    } else
                    {
                        coord[ix + 2, iy + 1] = false;
                        objects[ix - 1, iy - 1] = null;
                    }

                    if (objects[ix - 1, iy - 1] != null)
                    {
                        objects[ix - 1, iy - 1].transform.Translate(0, -Step * stepNumber, 0);
                    }
                }
            }
        }

        LCScript.AddingCounter(NumberOfLines);
        
        if (NumberOfLines == 1)
        {
            SScript.AddingPoints(100);
        } else if (NumberOfLines == 2)
        {
            SScript.AddingPoints(300);
        } else if (NumberOfLines == 3)
        {
            SScript.AddingPoints(500);
        } else if (NumberOfLines == 4)
        {
            SScript.AddingPoints(800);
        }

        Init_State();
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
