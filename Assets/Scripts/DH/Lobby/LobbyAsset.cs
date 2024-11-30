using UnityEngine;

public class LobbyAsset : MonoSingleton<LobbyAsset>
{
    //[SerializeField] private Sprite yes;
    //[SerializeField] private Sprite no;

    public string GetSprite(MainLobby.PlayerReadyEnum playerReady)
    {
        switch (playerReady.ToString())
        {
            case "True": 
                return "True";
            case "False": 
                return "False";
            default: 
                return null;
        }
    }
}
