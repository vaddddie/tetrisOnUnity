using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomChooseScript : MonoBehaviour
{
    public int RAND_CHOOSE(string topic, int count)
    {
        Queue<int> vector = new Queue<int>();

        for (int i = 0; i < count; i++)
        {
            if (PlayerPrefs.GetInt(topic + i) == 1)
            {
                vector.Enqueue(i);
            }
        }

        int temp = Random.Range(0, vector.Count);

        for (int i = 0; i < temp; i++)
        {
            vector.Dequeue();
        }

        return vector.Dequeue();
    }
}
