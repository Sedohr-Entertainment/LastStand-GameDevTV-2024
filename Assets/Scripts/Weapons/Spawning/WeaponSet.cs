using System.Linq;
using UnityEngine;

namespace LastStand.Weapons
{
    [CreateAssetMenu(menuName = "Last Stand/Weapon Set", fileName = "WeaponSet.asset")]
    public class WeaponSet : ScriptableObject
    {
        [System.Serializable]
        public class WeaponSetItem
        {
            [field: SerializeField]
            public GameObject Weapon { get; private set; }

            [field: SerializeField]
            public float SpawnProbability { get; private set; } = 0.5f;
        }

        [field: SerializeField]
        public WeaponSetItem[] Items { get; private set; }

        public float TotalProbability => Items.Sum(item => item.SpawnProbability);

        public GameObject GetRandomWeapon()
        {
            float randomValue = Random.Range(0, TotalProbability);
            return Items.Aggregate((obj, cumulative) =>
            {
                randomValue -= cumulative.SpawnProbability;
                return randomValue <= 0
                    ? cumulative
                    : obj;
            }).Weapon;
        }
    }
}