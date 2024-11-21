using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelChange : MonoBehaviour
{
    [SerializeField] private List<GameObject> background;
    [Range(0, 7)] public int numOfBackGround;

    public void ChangeBackground()
    {

    }
}
