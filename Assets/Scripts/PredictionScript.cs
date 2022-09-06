using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PredictionScript : MonoBehaviour
{
    public GameObject SimpleBlock;
    public GameObject[] blocks = new GameObject[4];

    private float Step = 0.6f;

    public void ChangePredictionObject(int PreObject, int PreObjectRotate)
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            if (blocks[i] != null)
            {
                Destroy(blocks[i]);
            }
        }

        if (PreObject == 1)
        {
            if (PreObjectRotate % 2 == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    blocks[i] = Instantiate(SimpleBlock, transform.position + new Vector3(Step * (i - 1), -(Step/2) * (PreObjectRotate - 2), 0), Quaternion.identity);
                }
                
                blocks[3] = Instantiate(SimpleBlock, transform.position + new Vector3(Step * (2 - PreObjectRotate), (PreObjectRotate - 2) * Step/2, 0), Quaternion.identity);
        
            }else
            {
                for (int i = 0; i < 3; i++)
                {
                   blocks[i] = Instantiate(SimpleBlock, transform.position + new Vector3((Step/2) * (3 - PreObjectRotate), Step * (i - 1), 0), Quaternion.identity);
                }
        
                blocks[3] = Instantiate(SimpleBlock, transform.position + new Vector3((PreObjectRotate - 3) * Step/2, (PreObjectRotate - 3) * Step, 0), Quaternion.identity);
            }
        }
        if (PreObject == 2)
        {
            if (PreObjectRotate % 2 == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    blocks[i] = Instantiate(SimpleBlock, transform.position + new Vector3(Step * (i - 2) + Step/2, 0, 0), Quaternion.identity);
                }
            } else
            {
                for (int i = 0; i < 4; i++)
                {
                    blocks[i] = Instantiate(SimpleBlock, transform.position + new Vector3(0, Step * (i - 2) + Step/2, 0), Quaternion.identity);
                }
            }
        }
        if (PreObject == 3)
        {
            if (PreObjectRotate % 2 == 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    blocks[i] = Instantiate(SimpleBlock, transform.position + new Vector3(-Step/2, Step * i, 0), Quaternion.identity);
                    blocks[i + 2] = Instantiate(SimpleBlock, transform.position + new Vector3(Step/2, -Step * i, 0), Quaternion.identity);
                }
            }else 
            {
                for (int i = 0; i < 2; i++)
                {
                    blocks[i] = Instantiate(SimpleBlock, transform.position + new Vector3(Step * i, Step/2, 0), Quaternion.identity);
                    blocks[i + 2] = Instantiate(SimpleBlock, transform.position + new Vector3(-Step*i, -Step/2, 0), Quaternion.identity);
                }
            }
        }
        if (PreObject == 4)
        {
            for (int i = 0; i < 2; i++)
            {
                blocks[i] = Instantiate(SimpleBlock, transform.position + new Vector3((Step * i) - Step/2, Step/2, 0), Quaternion.identity);
                blocks[i + 2] = Instantiate(SimpleBlock, transform.position + new Vector3((Step * i) - Step/2, -Step/2, 0), Quaternion.identity);
            }
        }
        if (PreObject == 5)
        {
            if (PreObjectRotate % 2 == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    blocks[i] = Instantiate(SimpleBlock, transform.position + new Vector3(Step * (i - 1), (PreObjectRotate - 2) * Step/2, 0), Quaternion.identity);
                }

                blocks[3] = Instantiate(SimpleBlock, transform.position + new Vector3(0, (2 - PreObjectRotate) * Step/2, 0), Quaternion.identity);

            } else
            {
                for (int i = 0; i < 3; i++)
                {
                    blocks[i] = Instantiate(SimpleBlock, transform.position + new Vector3((PreObjectRotate - 3) * Step/2, Step * (i - 1), 0), Quaternion.identity);
                }

                blocks[3] = Instantiate(SimpleBlock, transform.position + new Vector3((3 - PreObjectRotate) * Step/2, 0, 0), Quaternion.identity);

            }
        }
        if (PreObject == 6)
        {
             if (PreObjectRotate % 2 == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    blocks[i] = Instantiate(SimpleBlock, transform.position + new Vector3(Step * (i - 1), -(Step/2) * (PreObjectRotate - 2), 0), Quaternion.identity);
                }
                
                blocks[3] = Instantiate(SimpleBlock, transform.position + new Vector3(Step * (PreObjectRotate - 2), (PreObjectRotate - 2) * Step/2, 0), Quaternion.identity);
        
            }else
            {
                for (int i = 0; i < 3; i++)
                {
                   blocks[i] = Instantiate(SimpleBlock, transform.position + new Vector3((Step/2) * (3 - PreObjectRotate), Step * (i - 1), 0), Quaternion.identity);
                }
        
                blocks[3] = Instantiate(SimpleBlock, transform.position + new Vector3((PreObjectRotate - 3) * Step/2, (3 - PreObjectRotate) * Step, 0), Quaternion.identity);
            }
        }
        if (PreObject == 7)
        {
            if (PreObjectRotate % 2 == 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    blocks[i] = Instantiate(SimpleBlock, transform.position + new Vector3(-Step/2, -Step * i, 0), Quaternion.identity);
                    blocks[i + 2] = Instantiate(SimpleBlock, transform.position + new Vector3(Step/2, Step * i, 0), Quaternion.identity);
                }
            }else 
            {
                for (int i = 0; i < 2; i++)
                {
                    blocks[i] = Instantiate(SimpleBlock, transform.position + new Vector3(-Step * i, Step/2, 0), Quaternion.identity);
                    blocks[i + 2] = Instantiate(SimpleBlock, transform.position + new Vector3(Step*i, -Step/2, 0), Quaternion.identity);
                }
            }
        }
    }
}
