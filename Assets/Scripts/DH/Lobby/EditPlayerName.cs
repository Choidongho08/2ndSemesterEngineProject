using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditPlayerName : MonoSingleton<EditPlayerName>
{
    public event EventHandler OnNameChanged;
    private string playerName = "PlayerName";

    [SerializeField] private TextMeshProUGUI playerNameText;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {

        });
    }

}
