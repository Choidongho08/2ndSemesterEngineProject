using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTEvi : MonoBehaviour
{
    public CheckTEvi instance;

    private void Awake()
    {
        if (instance = null)
            instance = this;
    }

    public void ThisEviIsCorrect()
    {

    }
}
