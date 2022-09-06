using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AndroidMoveScript : MonoBehaviour
{
    public UnityEvent OnClickDown;
    public UnityEvent OnClickUp;

    private void OnMouseDown()
    {
        OnClickDown.Invoke();
    }

    private void OnMouseUp()
    {
        OnClickUp.Invoke();
    }
}
