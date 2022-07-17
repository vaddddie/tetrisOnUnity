using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PredictionScript : MonoBehaviour
{
    public Transform transform;
    
    public GameObject SimpleBlock;
    public GameObject[] blocks = new GameObject[4];

    public ObjectScript ObjScr;
    private float Step;

    void Start()
    {
        transform = GetComponent<Transform>();
        Step = ObjScr.Step;
    }

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
                    blocks[i] = Instantiate(SimpleBlock, transform.position + new Vector3(-Step + (Step * i), -(Step/2) * (PreObjectRotate - 2), 0), Quaternion.identity);
                }
                
                blocks[3] = Instantiate(SimpleBlock, transform.position + new Vector3(-Step * (PreObjectRotate - 2), (PreObjectRotate - 2) * Step/2, 0), Quaternion.identity);

            }else
            {
                for (int i = 0; i < 3; i++)
                {
                    blocks[i] = Instantiate(SimpleBlock, transform.position + new Vector3((-Step/2) * (PreObjectRotate - 3), -Step + i, 0), Quaternion.identity);
                }

                blocks[3] = Instantiate(SimpleBlock, transform.position + new Vector3((PreObjectRotate - 3) * Step/2, (PreObjectRotate - 3) * Step, 0), Quaternion.identity);
            }
        }
        if (PreObject == 2)
        {

        }
        if (PreObject == 3)
        {

        }
        if (PreObject == 4)
        {
            
        }
        if (PreObject == 5)
        {

        }
        if (PreObject == 6)
        {
            
        }
        if (PreObject == 7)
        {

        }
    }
}
