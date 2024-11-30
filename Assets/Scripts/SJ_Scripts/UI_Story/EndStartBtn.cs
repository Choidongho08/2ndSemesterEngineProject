using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndStartBtn : MonoBehaviour
{
    public void RestartBtn()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void QuitBtn()
    {
        Application.Quit();
    }
}
