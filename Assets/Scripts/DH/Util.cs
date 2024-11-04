
using UnityEngine;

public class Util : MonoSingleton<Util>
{
    [SerializeField] private GameObject _loading;
    [SerializeField] private GameObject _mainMenu;


    public void LoadingShow()
    {
        _loading.SetActive(true);
    }
    public void LoadingHide()
    {
        _loading.SetActive(false);
    }
    public void MainMenuHide()
    {
        _mainMenu.SetActive(false);
    }
    public void MainMenuShow()
    {
        _mainMenu.SetActive(true);
    }
}
