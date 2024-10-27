using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeNameUI : MonoBehaviour
{
    private TMP_InputField _inputPlayerName;
    [SerializeField] private Button _changeButton;
    [SerializeField] private Button _cancelButton;

    private string _playerName = string.Empty;
    public static event Action<string> OnChangePlayerNamed;

    private void Awake()
    {
        _inputPlayerName = GetComponentInChildren<TMP_InputField>();
        MainMenu.OnChangeName += () => gameObject.SetActive(false);
        _changeButton.onClick.AddListener(() =>
        {
            _playerName = _inputPlayerName.text;
            gameObject.SetActive(false);
            OnChangePlayerNamed?.Invoke(_playerName);
        });
    }

}
