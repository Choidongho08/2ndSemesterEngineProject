using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SO/CaseBook")]
public class CaseBookSO : ScriptableObject
{
    public string caseName;
    public int caseNumber;
    public bool isBlock;
    public Image lockImage;
    public Transform prefab;
    public Sprite sprite;
    public CaseType caseType;
}
