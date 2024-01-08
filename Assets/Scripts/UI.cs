using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    private GameObject _settings;

    [SerializeField]
    private Image _spriteRenderer;
    [SerializeField]
    private Sprite _spriteRendererOpen;
    [SerializeField]
    private Sprite _spriteRendererClose;

    private void Start()
    {
        _spriteRenderer.sprite = _spriteRendererClose;
    }

    public void OpenSettings()
    {
        _settings.SetActive(true);
        _spriteRenderer.sprite = _spriteRendererOpen;
    }

    public void CloseSettings()
    {
        _settings.SetActive(false);
        _spriteRenderer.sprite = _spriteRendererClose;
    }
}
