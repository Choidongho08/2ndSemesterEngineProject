using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _settingExitButton;
    [SerializeField] private Transform _inSetting;


    private void Awake()
    {
        _settingButton.onClick.AddListener(() => ShowSetting(true));
        _settingExitButton.onClick.AddListener(() => ShowSetting(false));
    }

    private void ShowSetting(bool value)
    {
        if (value)
        {
            Debug.Log("Down");
            _inSetting.DOLocalMoveY(0, 0.3f);
            return;
        }
        else
        {
            Debug.Log("Up");
            _inSetting.DOLocalMoveY(425 , 0.3f);
            return;
        }
    }
}
