using UnityEngine;
using UnityEngine.UI;

public class Authenticate : MonoBehaviour
{
    [SerializeField] private Button authenticateBtn;

    private void Start()
    {
        authenticateBtn.gameObject.SetActive(true);
        authenticateBtn.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            Util.instance.LoadingShow();
            MainLobby.instance.Authenticate(ChangeNameUI.instance.GetPlayerName());
            authenticateBtn.gameObject.SetActive(false);
        });
    }
}
