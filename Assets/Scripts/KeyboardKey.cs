using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

enum Validity {None, Valid, Potential, Invalid}
public class KeyboardKey : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI letterDisplay;
    [SerializeField] private Image _renderer;

    private Validity validity;
    public static Action<char> onKeyPressedEvent;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SendKeyPressedEvent);
        Initialize();
    }

    private void SendKeyPressedEvent()
    {
        onKeyPressedEvent.Invoke(letterDisplay.text[0]);
    }

    public char GetLetter()
    {
        return letterDisplay.text[0];
    }

    public void Initialize()
    {
        _renderer.color = Color.white;
        validity = Validity.None;
    }
    public void SetValid()
    {
        _renderer.color = Color.green;
        validity = Validity.Valid;
    }
    public void SetPotential()
    {
        _renderer.color = Color.yellow;
        validity = Validity.Potential;
    }
    public void SetInvalid()
    {
        if(validity == Validity.Valid || validity == Validity.Potential)
        {
            return;
        }
        _renderer.color = Color.gray;
        validity = Validity.Invalid;
    }

    public bool IsUnTouched()
    {
        return validity == Validity.None;
    }
}
