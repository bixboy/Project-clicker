using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UIElements;
using TMPro;


namespace spawn
{
    public class InvokeSoldats : MonoBehaviour
    {

        // Field 
        [SerializeField] private GameObject _prefabSoldiers;
        private GameObject _spawnPoint;

        private List<SoldiersLife> _soldats = new List<SoldiersLife>();

        [SerializeField]
        private int _soldatsCount;
        [SerializeField] private int _soldatsCountMax = 10;

        [SerializeField] private TextMeshProUGUI _textLimitSoldiers;

        // Methodes
        #region EditorParametre

        private void OnValidate()
        {
            _textLimitSoldiers.text = _soldatsCount.ToString();
        }

        private void Start()
        {
            _soldatsCountMax--;
            _spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        }

        private void Reset()
        {
            Debug.Log("Reset");
        }

        #endregion

        public void spawnSoldier()
        {
            if (_soldatsCount <= _soldatsCountMax && _prefabSoldiers != null && _spawnPoint != null)
            {
                Transform spawnPointTransform = _spawnPoint.transform;
                GameObject newSoldier = Instantiate(_prefabSoldiers, spawnPointTransform.position, spawnPointTransform.rotation);
                SoldiersLife soldierLifeComponent = newSoldier.GetComponent<SoldiersLife>();

                if (soldierLifeComponent != null)
                {
                    _soldats.Add(soldierLifeComponent);
                    _soldatsCount++;
                    _textLimitSoldiers.text = _soldatsCount.ToString();
                }
                else
                {
                    Debug.LogError("SoldiersLife n'a pas ete trouve.");
                }

            }
            else if (_soldatsCount >= _soldatsCountMax && _prefabSoldiers != null && _spawnPoint != null && _soldats.Count > 0)
            {
                SoldiersLife soldierToRemove = _soldats[0];
                if (soldierToRemove.GetCurrentHealth() < soldierToRemove.GetMaxHealth()/2)
                {
                    RemoveSoldierFromList(soldierToRemove);
                    Destroy(soldierToRemove.gameObject);
                    spawnSoldier();
                }

            }
            else
            {
                Debug.LogWarning("Conditions insuffisantes pour cr�er un nouveau soldat.");
            }

        }

        public void RemoveSoldierFromList(SoldiersLife soldier)
        {
            if (_soldats.Contains(soldier))
            {
                _soldats.Remove(soldier);
                _soldatsCount--;
                _textLimitSoldiers.text = _soldatsCount.ToString();
            }
        }

        [Button]
        private void spawn1() => spawnSoldier();
    }
}

