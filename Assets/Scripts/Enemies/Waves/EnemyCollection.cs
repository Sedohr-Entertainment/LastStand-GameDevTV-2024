using System.Linq;
using UnityEngine;

namespace Enemies.Waves
{
    [CreateAssetMenu(menuName = "Last Stand/Enemy Collection", fileName = "EnemyCollection.asset")]
    public class EnemyCollection : ScriptableObject
    {
        [System.Serializable]
        public class EnemyCollectionItem
        {
            [field: SerializeField]
            public GameObject Prefab { get; private set; }

            [field: SerializeField]
            public float SpawnProbability { get; private set; } = 1;
        }

        [field: SerializeField]
        public EnemyCollectionItem[] Enemies { get; private set; }

        private float totalProbability => Enemies.Select(enemy => enemy.SpawnProbability).Sum(); 
        
        [SerializeField]
        private string seedForRandomEnemy;

        public GameObject GetRandomEnemy()
        {
            Random.InitState(seedForRandomEnemy.GetHashCode());
            float randomEnemyProbability = Random.Range(0, totalProbability);

            GameObject lastEnemy = null;
            foreach (var enemy in Enemies)
            {
                randomEnemyProbability -= enemy.SpawnProbability;

                if (randomEnemyProbability < 0)
                {
                    lastEnemy ??= enemy.Prefab;
                    break;
                }

                lastEnemy = enemy.Prefab;
            }

            return lastEnemy;
        }
    }
}