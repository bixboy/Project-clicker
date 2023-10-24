using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UIElements;


namespace spawn
{
    public class InvockSoldats : MonoBehaviour
    {

        // Field 
        [SerializeField] private GameObject _prefabSoldiers;
        private GameObject _spawnPoint;

        // Methodes
        #region EditorParametre

        private void Start()
        {
            _spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        }

        private void Reset()
        {
            Debug.Log("Reset");
        }

        #endregion

        private void spawn()
        {
            Transform spawnPointTransform = _spawnPoint.transform;
            Instantiate(_prefabSoldiers, spawnPointTransform.position, spawnPointTransform.rotation);
        }

        [Button]
        private void spawn1() => spawn();
    }
}

