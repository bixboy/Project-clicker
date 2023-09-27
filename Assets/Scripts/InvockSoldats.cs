using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using System;
using UnityEngine.Events;


namespace spawn
{
    public class InvockSoldats : MonoBehaviour
    {

        // Field 
        [SerializeField] GameObject _prefabSoldats;
        public GameObject spawnPoint;

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
            
        }

        [Button] void spawn1() => spawn();
    }
}

