using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/CaseBook")]
public class CaseBookSO : ScriptableObject
{
    public int caseNumber;
    public Transform prefab;
    public Sprite sprite;
    public CaseType caseType;
}
