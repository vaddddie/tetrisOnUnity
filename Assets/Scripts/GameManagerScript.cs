using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject simpleBlock;
    [SerializeField] private GameObject predictionBlock;
    [SerializeField] private GameObject adsMenu;
    [SerializeField] private GameObject restartMenu;
    [SerializeField] private GameObject startTimer;
    [SerializeField] private GameObject darkPanel;

    [SerializeField] private PredictionScript predictionScript;
    [SerializeField] private ScoreScript scoreScript;
    [SerializeField] private LineCounterScript lineCounterScript;
    [SerializeField] private TimerScript timerScript;
    [SerializeField] private DeathTimerScript deathTimerScript;
    [SerializeField] private WritingScript writingScript;
    [SerializeField] private StartTimer startTimerScript;

    private bool watchAds = true;

    private bool[,] coord = new bool[16, 25];
    private int[,] position = new int[4, 2];

    private GameObject[,] objects = new GameObject[10, 23];
    private GameObject[] blocks = new GameObject[4];

    private int object_;
    private int objectRotate;
    private int nextObject;
    private int nextObjectRotate;

    private int rewardForStep;
    private int rewardForOneLine;
    private int rewardForTwoLine;
    private int rewardForThreeLine;
    private int rewardForFourLine;

    [SerializeField] private float Step = 1f;
    [SerializeField] private float Move_interval;
    [SerializeField] private float side_interval = 0.05f;
    private float MI_const;

    private Coroutine _move_down;
    private Coroutine _move_left;
    private Coroutine _move_right;

    private bool doubleClickAllow;
    private float clickTime;
    private float clickDelay = 0.2f;

    private Sprite[] ownColors;
    [SerializeField] private Sprite[] simpleColors;
    [SerializeField] private Sprite[] mainColors;
    private int color;

    private void Start()
    {
        // =========================== From database =======================================

        int temp = PlayerPrefs.GetInt("SpeedOwned", 0);

        if (temp != -1)
        {
            int x = (int)Mathf.Pow(2, temp);

            Move_interval = 0.5f / x;

            rewardForStep = 1 * x;
            rewardForOneLine = 100 * x;
            rewardForTwoLine = 300 * x;
            rewardForThreeLine = 500 * x;
            rewardForFourLine = 800 * x;
        } else 
        {
            int count = 0;

            for (int i = 0; i < 5; i++)
            {
                if (PlayerPrefs.GetInt("Speed" + (i + 1), 0) == 1)
                {
                    count += 1;
                }
            }

            int chose = Random.Range(0, count);
            count = 0;

            for (int i = 0; i < 5; i++)
            {
                if (PlayerPrefs.GetInt("Speed" + (i + 1), 0) == 1)
                {
                    count += 1;
                    if (count == chose)
                    {
                        int x = (int)Mathf.Pow(2, i);

                        Move_interval = 1f / x;

                        rewardForStep = 1 * x;
                        rewardForOneLine = 100 * x;
                        rewardForTwoLine = 300 * x;
                        rewardForThreeLine = 500 * x;
                        rewardForFourLine = 800 * x;

                        break;
                    }
                }
            }

        }


        // =========================== From database =======================================

        startTimer.SetActive(true);
        adsMenu.SetActive(false);
        restartMenu.SetActive(false);
        darkPanel.SetActive(true);

        ChoseColor();

        // =========================== DoubleClick from database ===========================

        if (PlayerPrefs.GetInt("DoubleTap", 1) == 1)
        {
            doubleClickAllow = true;
        } else
        {
            doubleClickAllow = false;
        }

        // ============================ end ===============================================

        

        this.enabled = false;
        MI_const = Move_interval;
        PredicitonObject();

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
    }

    private void ChoseColor()
    {
        switch (PlayerPrefs.GetInt("SkinOwned", 0))
        {
            case 0:
                ownColors = simpleColors;
                break;
            case 1:
                ownColors = mainColors; 
                break;
            default:
                break;
        }
    }

    private void PredicitonObject()
    {
        nextObject = Random.Range(1, 7);
        nextObjectRotate = Random.Range(1, 5);

        color = Random.Range(0, ownColors.Length);

        simpleBlock.GetComponent<SpriteRenderer>().sprite = ownColors[color];
        predictionBlock.GetComponent<SpriteRenderer>().sprite = ownColors[color];

        predictionScript.ChangePredictionObject(nextObject, nextObjectRotate);

    }

    private void Death()
    {
        this.enabled = false;

        darkPanel.SetActive(true);

        timerScript.StopTimer();

        if (watchAds)
        {
            adsMenu.SetActive(true);
            deathTimerScript.StartTimer();

            watchAds = false;

        } else 
        {
            restartMenu.SetActive(true);
            
            writingScript.WrittingInData();
        }

    }

    public void ExtraLife()
    {
        deathTimerScript.StopTimer();
        adsMenu.SetActive(false);

        StartCoroutine(CellDeleting());
    }

    private IEnumerator CellDeleting()
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
        

        startTimer.SetActive(true);
        startTimerScript.Start();

    }

// =============================================== Moving ===================================

    public void LeftMovingDown()
    {
        if (_move_left == null)
        {
            _move_left = StartCoroutine(MoveLeft());
        }
    }

    public void LeftMovingUp()
    {
        if (_move_left != null)
        {
            StopCoroutine(_move_left);
            _move_left = null;
        }
    }

    public void RightMovingDown()
    {
        if (_move_right == null)
        {
            _move_right = StartCoroutine(MoveRight());
        } 
    }

    public void RightMovingUp()
    {
        if (_move_right != null)
        {
            StopCoroutine(_move_right);
            _move_right = null;
        }
    }

    public void DownMovingDown()
    {
        if (!DoubleClick() | !doubleClickAllow)
        {
            OneStepDown(true);

            Move_interval = MI_const / 10;
        } else
        {
            StopCoroutine(_move_down);
            _move_down = null;

            FastMoveDown();
        }
    }

    public void DownMovingUp()
    {
        Move_interval = MI_const;
    }

    public void Rotate()
    {
        if (object_ == 1)
            Rotate_State_1();

        if (object_ == 2)
            Rotate_State_2();

        if (object_ == 3)
            Rotate_State_3();

        if (object_ == 5)
            Rotate_State_5();

        if (object_ == 6)
            Rotate_State_6();

        if (object_ == 7)
            Rotate_State_7();
    }

// ============================================== end ===============================

    private void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            if (object_ == 1)
                Rotate_State_1();

            if (object_ == 2)
                Rotate_State_2();

            if (object_ == 3)
                Rotate_State_3();

            if (object_ == 5)
                Rotate_State_5();

            if (object_ == 6)
                Rotate_State_6();

            if (object_ == 7)
                Rotate_State_7();
        }

        if (Input.GetKeyDown("a"))
        {
            if (_move_left == null)
            {
                _move_left = StartCoroutine(MoveLeft());
            }
        }

        if (Input.GetKeyUp("a"))
        {
            if (_move_left != null)
            {
                StopCoroutine(_move_left);
                _move_left = null;
            }
        }

        if (Input.GetKeyDown("d"))
        {
            if (_move_right == null)
            {
                _move_right = StartCoroutine(MoveRight());
            } 
        }

        if (Input.GetKeyUp("d"))
        {
            if (_move_right != null)
            {
                StopCoroutine(_move_right);
                _move_right = null;
            }
        }

        if (Input.GetKeyDown("s"))
        {
            if (!DoubleClick() | !doubleClickAllow)
            {
                OneStepDown(true);

                Move_interval = MI_const / 10;
            } else
            {
                StopCoroutine(_move_down);
                _move_down = null;

                FastMoveDown();
            }
        }

        if (Input.GetKeyUp("s"))
        {
            Move_interval = MI_const;
        }
    }

    // ============================================== DOUBLECLICK BLOCK ===========================================================

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

    private void FastMoveDown()
    {
        while (true)
        {
            if (OneStepDown(true))
            {
                break;
            }
        }
    }


    // ==============================================    BLOCK FOR MOVES    ========================================================

    private IEnumerator MoveLeft()
    {
        OneStepLeft();

        yield return new WaitForSeconds(side_interval * 5);

        while (true)
        {
            OneStepLeft();
            yield return new WaitForSeconds(side_interval);
        }
    }

    private IEnumerator MoveRight()
    {
        OneStepRight();

        yield return new WaitForSeconds(side_interval * 5);

        while (true)
        {
            OneStepRight();
            yield return new WaitForSeconds(side_interval);
        }
    }

    private IEnumerator MoveDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(Move_interval);

            OneStepDown(Move_interval == MI_const ? false : true);
        }
    }

    private void OneStepLeft()
    {
        bool temp = true;

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
    }

    private void OneStepRight()
    {
        bool temp = true;

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
    }

    private bool OneStepDown(bool point)
    {
        if (point)
        {
            scoreScript.AddingPoints(rewardForStep);
        }

        for (int i = 0; i < 4; i++)
        {
            if (coord[position[i, 0] + 2, position[i, 1]] == true)
            {               
                for (int j = 0; j < 4; j++)
                {
                    coord[position[j, 0] + 2, position[j, 1] + 1] = true;
                    objects[position[j, 0] - 1, position[j, 1] - 1] = blocks[j];
                }

                if (_move_down != null)
                {
                    StopCoroutine(_move_down);
                    _move_down = null; 
                }

                this.enabled = false;
                LineDeleting();
                
                return true;
            } 
        }

        for (int i = 0; i < 4; i++)
        {
            position[i, 1] -= 1;
            blocks[i].transform.position = blocks[i].transform.position + new Vector3(0, -Step, 0);
        }

        return false;
    }

    // ===============================   MOVES BLOCK WAS ENDED   =====================================
    // ===============================   LineDeleting BLOCK    =======================================

    private void LineDeleting()
    {
        int numberOfLines = 0;
        int[] numberOfDeletingLines = new int[4];

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
            
                for (int i = 0; i < numberOfLines; i++)
                {
                    if (numberOfDeletingLines[i] == position[k, 1])
                    {
                        temp = false;
                    }
                }
                
                if (temp)
                {
                    numberOfDeletingLines[numberOfLines] = position[k, 1];
                    numberOfLines += 1;
                }
            }
        }

        if (numberOfLines == 0)
        {
            InitState();
        } else
        {
            StartCoroutine(CoroutineDL(numberOfDeletingLines, numberOfLines));
        }

        if (numberOfLines == 4)
        {   
            foreach(int item in numberOfDeletingLines)
            {
                if (item != 0)
                {
                    for(int i = 0; i < 10; i++)
                    {
                        if (objects[i, item - 1] != null)
                        {
                            objects[i, item - 1].GetComponent<FlashScript>().Blessing();
                        }
                    }
                }
            }
        }
    }

    private IEnumerator CoroutineDL(int[] numberOfDeletingLines, int numberOfLines)
    {
        for (int i = 1; i < 6; i++)
        {
            foreach(int item in numberOfDeletingLines)
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
                foreach(int item in numberOfDeletingLines)
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

                    foreach(int item in numberOfDeletingLines)
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

        lineCounterScript.AddingCounter(numberOfLines);
        
        if (numberOfLines == 1)
        {
            scoreScript.AddingPoints(rewardForOneLine);
        } else if (numberOfLines == 2)
        {
            scoreScript.AddingPoints(rewardForTwoLine);
        } else if (numberOfLines == 3)
        {
            scoreScript.AddingPoints(rewardForThreeLine);
        } else if (numberOfLines == 4)
        {
            scoreScript.AddingPoints(rewardForFourLine);
        }

        InitState();
    }

    // ============================================= LineDeleting BLOCK WAS ENDED =====================================================

    public void InitState()
    {
        object_ = nextObject;
        objectRotate = nextObjectRotate;

        if (object_ == 1)
        {
            if (objectRotate % 2 == 1)
            {
                position[0, 0] = 5 + (objectRotate - 1);
                position[0, 1] = 21;

                position[1, 0] = 6;
                position[1, 1] = 21;

                position[2, 0] = 7 - (objectRotate - 1);
                position[2, 1] = 21;

                position[3, 0] = 7 - (objectRotate - 1);
                position[3, 1] = 20 + (objectRotate - 1);

            } else
            {
                if (objectRotate == 2)
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

        if (object_ == 2)
        {
            if (objectRotate % 2 == 1)
            {
                if (objectRotate == 1)
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
                if (objectRotate == 2)
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

        if (object_ == 3)
        {
            if (objectRotate < 3)
            {
                position[2, 0] = 6;
                position[2, 1] = 21; 

                if (objectRotate == 1)
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

            } else if (objectRotate == 3)
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

        if (object_ == 4)
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

        if (object_ == 5)
        {
            if (objectRotate % 2 == 1)
            {
                position[0, 0] = 4 + objectRotate;
                position[0, 1] = 21;

                position[1, 0] = 6;
                position[1, 1] = 23 - objectRotate;

                position[2, 0] = 6;
                position[2, 1] = 21;

                position[3, 0] = 8 - objectRotate;
                position[3, 1] = 21;

            } else if (objectRotate == 2)
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

        if (object_ == 6)
        {
            if (objectRotate % 2 == 1)
            {
                position[0, 0] = 7 - objectRotate;
                position[0, 1] = 21;

                position[1, 0] = 5;
                position[1, 1] = 21;

                position[2, 0] = 3 + objectRotate;
                position[2, 1] = 21;

                position[3, 0] = 3 + objectRotate;
                position[3, 1] = 19 + objectRotate;

            } else
            {
                if (objectRotate == 2)
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

        if (object_ == 7)
        {
            if (objectRotate < 3)
            {
                position[2, 0] = 5;
                position[2, 1] = 21; 

                if (objectRotate == 1)
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

            } else if (objectRotate == 3)
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

        bool life = true;

        for (int i = 0; i < 4; i++)
        {
            if (coord[position[i, 0] + 2, position[i, 1] + 1] == true)
            {
                life = false;
                Death();
                break;
            }
        }

        if (_move_down != null)
        {
            StopCoroutine(_move_down);
            _move_down = null;
        }

        if (_move_left != null)
        {
            StopCoroutine(_move_left);
            _move_left = null;
        }

        if (_move_right != null)
        {
            StopCoroutine(_move_right);
            _move_right = null;
        }

        if (life)
        {
            for (int i = 0; i < 4; i++)
            {
                blocks[i] = Instantiate(simpleBlock, new Vector3(position[i, 0] * Step - Step/2, position[i, 1] * Step - Step/2, 110f), Quaternion.identity);
            }  

            if (_move_down == null)
            {
                this.enabled = true;

                // if (Input.GetKey("s"))
                // {
                //     Move_interval = MI_const / 10;
                // } else
                // {
                //     Move_interval = MI_const;
                // }

                Move_interval = MI_const;

                if (Input.GetKey("a") & _move_left == null)
                {
                    _move_left = StartCoroutine(MoveLeft());
                }

                if (Input.GetKey("d") & _move_right == null)
                {
                    _move_right = StartCoroutine(MoveRight());
                }

                _move_down = StartCoroutine(MoveDown());
            }
        }

        PredicitonObject();

    }

    // =============================================== ROTATIONS BLOCK ==========================================================

    private void Rotate_State_1()
    {
        int[,] interim = new int[4, 2]; 

        bool checkcollid = true;

        if (objectRotate == 1) 
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

        if (objectRotate == 2) 
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

        if (objectRotate == 3) 
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

        if (objectRotate == 4) 
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

        if (!checkcollid && objectRotate == 2)
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

        if (!checkcollid && objectRotate == 4)
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

            if (objectRotate != 4)
            {
                objectRotate += 1;
            } else {
                objectRotate = 1;
            }
        }
    }

    private void Rotate_State_2()
    {
        int[,] interim = new int[4, 2];

        bool checkcollid = true;

        if (objectRotate == 1) 
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

        if (objectRotate == 2) 
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

        if (objectRotate == 3) 
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

        if (objectRotate == 4) 
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

        if (!checkcollid && objectRotate == 2)
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

        if (!checkcollid && objectRotate == 4)
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
            
            if (objectRotate != 4)
            {
                objectRotate += 1;
            } else {
                objectRotate = 1;
            }
        }
    }

    private void Rotate_State_3()
    {
        int[,] interim = new int[4, 2];

        bool checkcollid = true;

        if (objectRotate == 1) 
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

        if (objectRotate == 2) 
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

        if (objectRotate == 3) 
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

        if (objectRotate == 4) 
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

        if (!checkcollid && objectRotate == 1)
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

        if (!checkcollid && objectRotate == 3)
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
            
            if (objectRotate != 4)
            {
                objectRotate += 1;
            } else {
                objectRotate = 1;
            }
        }
    }

    private void Rotate_State_5()
    {
        int[,] interim = new int[4, 2];

        bool checkcollid = true;

        if (objectRotate == 1) 
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

        if (objectRotate == 2) 
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

        if (objectRotate == 3) 
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

        if (objectRotate == 4) 
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

        if (!checkcollid && objectRotate == 2)
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

        if (!checkcollid && objectRotate == 4)
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
            
            if (objectRotate != 4)
            {
                objectRotate += 1;
            } else {
                objectRotate = 1;
            }
        }
    }

    private void Rotate_State_6()
    {
        int[,] interim = new int[4, 2];

        bool checkcollid = true;

        if (objectRotate == 1) 
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

        if (objectRotate == 2) 
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

        if (objectRotate == 3) 
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

        if (objectRotate == 4) 
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

        if (!checkcollid && objectRotate == 2)
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

        if (!checkcollid && objectRotate == 4)
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
            
            if (objectRotate != 4)
            {
                objectRotate += 1;
            } else {
                objectRotate = 1;
            }
        }
    }

    private void Rotate_State_7()
    {
        int[,] interim = new int[4, 2];

        bool checkcollid = true;

        if (objectRotate == 1) 
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

        if (objectRotate == 2) 
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

        if (objectRotate == 3) 
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

        if (objectRotate == 4) 
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

        if (!checkcollid && objectRotate == 1)
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

        if (!checkcollid && objectRotate == 3)
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
            
            if (objectRotate != 4)
            {
                objectRotate += 1;
            } else {
                objectRotate = 1;
            }
        }
    }
}
