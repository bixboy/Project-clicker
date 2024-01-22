using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _objectManager;

    private void Start()
    {
        _objectManager = GameObject.FindGameObjectWithTag("GameManager");
    }
    public void LaunchGame()
    {
        _objectManager.GetComponent<LevelManager>().LoadLevel(_objectManager.GetComponent<LevelManager>().GetLevelLoaded());
    }
}
