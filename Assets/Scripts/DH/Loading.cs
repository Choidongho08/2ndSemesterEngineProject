
using UnityEngine;

public class Loading : MonoSingleton<Loading>
{
    [SerializeField] private GameObject _child;


    public void Show()
    {
        _child.SetActive(true);
    }
    public void Hide()
    {
        _child.SetActive(false);
    }
}
