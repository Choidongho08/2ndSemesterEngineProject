using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;

public class interTNPC : MonoBehaviour
{
    [SerializeField] protected GameObject storyPan;

    private void StoryOn()
    {
        storyPan.SetActive(true);
    }

    public void InteractiveNPC()
    {
        StoryOn();
    }
}
