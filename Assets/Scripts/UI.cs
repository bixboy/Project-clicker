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

    [SerializeField]
    private Slider _slider;

    private void Start()
    {
        _spriteRenderer.sprite = _spriteRendererClose;
    }

    public void SetEnemyCount(int count)
    {
        _slider.value = 0;
        _slider.maxValue = count;
    }

    public void SetCurrentCount(int count)
    {
        _slider.value += count;
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
