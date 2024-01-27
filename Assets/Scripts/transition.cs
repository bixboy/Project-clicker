using System.Collections;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class transition : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private bool _animBossEntrance;

    [Header("Options"), SerializeField, ShowIf("_animBossEntrance")]
    private GameObject _top;
    [SerializeField, ShowIf("_animBossEntrance")]
    private GameObject _bottom;
    [SerializeField, ShowIf("_animBossEntrance")]
    private Image _imageBoss;
    [SerializeField, ShowIf("_animBossEntrance")]
    private TextMeshProUGUI _nameBoss;
    [SerializeField, ShowIf("_animBossEntrance")]
    private List<TextMeshProUGUI> _textLvl;

    void Start()
    {
        if (!_animBossEntrance)
        {
            _animator = GetComponent<Animator>();
        }else
        {
            _top.SetActive(true);
            _bottom.SetActive(true);
        }
    }

    public void SetUiTransitionBoss(Sprite imageBoss, string nameBoss, int lvlBoss)
    {
        gameObject.SetActive(true);
        _nameBoss.text = nameBoss;
        _imageBoss.sprite = imageBoss;
        foreach (var text in _textLvl)
        {
            text.text = ("/ BOSS LVL " + lvlBoss);
        }
    }

    public void ChangeScene(int lvl, LevelManager levelManager)
    {
        gameObject.GetComponent<Image>().enabled = true;
        _animator.SetTrigger("Start");
        StartCoroutine(LoadTransition(lvl, levelManager));
    }

    public void Disable()
    {
        gameObject.GetComponent<Image>().enabled = false;
    }

    private IEnumerator LoadTransition(int lvl, LevelManager levelManager)
    {
        yield return new WaitForSeconds(0.45f);
        levelManager.LoadLevel(lvl);
    }
}
