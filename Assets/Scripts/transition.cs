using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transition : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeScene()
    {
        StartCoroutine(LoadTransition());
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator LoadTransition()
    {
        yield return new WaitForSeconds(1f);
        _animator.SetTrigger("Start");
    }
}
