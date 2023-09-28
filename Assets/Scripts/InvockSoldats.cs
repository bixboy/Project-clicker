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
        [SerializeField] GameObject _prefabSoldats;
        GameObject spawnPoint;

        // Methodes
        #region EditorParametre

        private void Start()
        {
            spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        }

        private void Reset()
        {
            Debug.Log("Reset");
        }

        #endregion

        void spawn()
        {
            Transform spawnPointTransform = spawnPoint.transform;
            Instantiate(_prefabSoldats, spawnPointTransform.position, spawnPointTransform.rotation);
        }

        [Button] void spawn1() => spawn();
    }
}

