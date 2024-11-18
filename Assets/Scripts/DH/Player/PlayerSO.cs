using UnityEngine;

[CreateAssetMenu(menuName = "SO/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public string playerName;
    public string playerReady = "false";
}
