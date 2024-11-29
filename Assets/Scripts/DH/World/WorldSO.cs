using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SO/WorldSO")]
public class WorldSO : ScriptableObject
{
    public string worldName;
    public int worldNumber;
    public bool isOwner;
}
