using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashScript : MonoBehaviour
{
    public Material flashMaterial;
    public Material originalMaterial;

    private float duration = 0.02f;

    private SpriteRenderer SR;

    private Coroutine flashRoutine;

    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        originalMaterial = SR.material;
    }

    public void Blessing()
    {
        if(flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        for(int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(duration);
            SR.material = flashMaterial;
            yield return new WaitForSeconds(duration);
            SR.material = originalMaterial;
        }
        flashRoutine = null;
    }
}
