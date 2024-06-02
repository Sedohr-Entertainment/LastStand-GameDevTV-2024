using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace LastStand.Weapons
{
    [RequireComponent(typeof(BoxCollider))]
    public class WeaponSpawner : MonoBehaviour
    {
        [FormerlySerializedAs("_startOnAwake")]
        [SerializeField]
        private bool _spawnOnAwake = true;

        [SerializeField]
        private WeaponSet _weaponSet;

        [SerializeField]
        private Transform _spawnLocation;

        [SerializeField]
        private float _cooldown;

        [SerializeField]
        private UnityEvent onCooldownFinish = new();

        private GameObject lastSpawned;

        private void Awake()
        {
            onCooldownFinish.AddListener(SpawnWeapon);

            if (_spawnOnAwake)
            {
                SpawnWeapon();
            }
        }

        private void OnDestroy()
        {
            onCooldownFinish.RemoveListener(SpawnWeapon);
        }

        private IEnumerator WeaponCooldown()
        {
            yield return new WaitForSeconds(_cooldown);
            onCooldownFinish?.Invoke();
        }

        private void SpawnWeapon()
        {
            var weapon = _weaponSet.GetRandomWeapon();
            lastSpawned = Instantiate(weapon, _spawnLocation.position, Quaternion.identity);
        }

        private void OnTriggerExit(Collider other)
        {
            if (lastSpawned == null || other.gameObject != lastSpawned)
            {
                return;
            }

            lastSpawned = null;
            StartCoroutine(WeaponCooldown());
        }
    }
}