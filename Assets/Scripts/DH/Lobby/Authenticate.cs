using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Authenticate : MonoBehaviour
{
    [SerializeField] private GameObject _authenticateUI;
    [SerializeField] private Button _authenticateBtn;
    [SerializeField] private TextMeshProUGUI _mainTitleText, _buttonText;
    [SerializeField] private OnMouseEvent _onMouseEvent;

    private Vector3 _buttonTextScale;

    private void Awake()
    {
        _buttonTextScale = _buttonText.transform.localScale;

        _authenticateBtn.onClick.AddListener(() =>
        {
            Util.instance.LoadingShow();
            MainLobby.instance.Authenticate(ChangeNameUI.instance.GetPlayerName());
            _authenticateUI.gameObject.SetActive(false);
        });
        _onMouseEvent.onMouseEnter += () =>
        {
            _buttonText.transform.DOScale(_buttonTextScale + new Vector3(0.3f, 0.3f, 1), 0.4f);
        };
        _onMouseEvent.onMouseExit += () =>
        {
            _buttonText.transform.DOScale(_buttonTextScale, 0.5f);
        };
    }

}
