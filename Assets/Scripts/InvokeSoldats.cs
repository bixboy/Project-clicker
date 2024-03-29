using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.Purchasing;


namespace spawn
{
    public class InvokeSoldats : MonoBehaviour
    {

        // Field 
        [SerializeField] private GameObject _prefabSoldiers;

        private List<SoldiersLife> _soldats = new List<SoldiersLife>();

        [SerializeField]
        private int _soldatsCount;
        [SerializeField] private int _soldatsCountMax = 10;

        private UpgradeManager _upgradeManager;
        [SerializeField] private TextMeshProUGUI _textLimitSoldiers;
        private List<ISkill> _activeSkills = new List<ISkill>();

        public bool isSkillActive(SkillName skillName)
        {
            for (int i = 0; i < _activeSkills.Count; i++)
            {
                if (!_activeSkills[i].IsActive())
                {
                    _activeSkills.RemoveAt(i);
                    i--;
                    continue;
                }
                if (_activeSkills[i].GetSkillName() == skillName) return true;
            }
            return false;
        }

        public void AddSkill(ISkill iskill) => _activeSkills.Add(iskill);
        
        // Methodes
        #region EditorParametre

        private void OnValidate()
        {
            _textLimitSoldiers.text = "x" + _soldatsCount.ToString();
        }

        private void Start()
        {
            _soldatsCountMax--;
            GameObject.FindWithTag("GameManager").GetComponent<SkillsManager>().skillSummoned += AddSkill;
        }

        private void Reset()
        {
            Debug.Log("Reset");
        }

        #endregion

        private void Update()
        {
            _upgradeManager = GameObject.FindWithTag("GameManager").GetComponent<UpgradeManager>();
            _soldatsCountMax = (int)_upgradeManager.GetUpgradeStatByName(StatName.MaxSoldiers).Amount;
            _soldatsCountMax--;
        }

        public void spawnSoldier()
        {
            if (_soldatsCount <= _soldatsCountMax && _prefabSoldiers != null)
            {
                Transform spawnPointTransform = transform;
                GameObject newSoldier = Instantiate(_prefabSoldiers, spawnPointTransform.position, spawnPointTransform.rotation);
                SoldiersLife soldierLifeComponent = newSoldier.GetComponent<SoldiersLife>();
                newSoldier.GetComponent<MoveSoldats>().SetInvoker(GetComponent<InvokeSoldats>());
                soldierLifeComponent.SetInvoker(GetComponent<InvokeSoldats>());

                if (soldierLifeComponent != null)
                {
                    _soldats.Add(soldierLifeComponent);
                    _soldatsCount++;
                    _textLimitSoldiers.text = "x" + _soldatsCount.ToString();
                }
                else
                {
                    Debug.LogError("SoldiersLife n'a pas ete trouve.");
                }

            }
            else if (_soldatsCount >= _soldatsCountMax && _prefabSoldiers != null && _soldats.Count > 0)
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

