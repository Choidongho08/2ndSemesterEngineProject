using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SO/CaseBookList")]
public class CaseBookListSO : ScriptableObject
{
    public List<CaseBookSO> list;
}
