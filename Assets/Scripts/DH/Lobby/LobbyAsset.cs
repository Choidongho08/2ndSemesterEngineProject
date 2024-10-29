using UnityEngine;

public class LobbyAsset : MonoSingleton<LobbyAsset>
{
    [SerializeField] private Sprite yes;
    [SerializeField] private Sprite no;

    public Sprite GetSprite(MainLobby.PlayerReadyEnum playerReady)
    {
        switch (playerReady)
        {
            default:
            case MainLobby.PlayerReadyEnum.True: return yes;
            case MainLobby.PlayerReadyEnum.False: return no;
        }
    }
}
