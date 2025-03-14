using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private Monster_Controller[] monsterPrefabs;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private string monsterPath = "Prefabs/Monsters";
    [SerializeField] private int poolSize = 10;

    private Dictionary<string, ObjectPool<Monster_Controller>> monsterPools;
    private float spawnTimer;

    private void Start()
    {
        monsterPools = new Dictionary<string, ObjectPool<Monster_Controller>>();

        monsterPrefabs = Resources.LoadAll<Monster_Controller>(monsterPath);

        if(monsterPrefabs.Length == 0)
        {
            Debug.Log("몬스터 없음");
            return;
        }

        foreach( var prefab in monsterPrefabs)
        {
            monsterPools[prefab.name] = new ObjectPool<Monster_Controller>(prefab, poolSize);
        }
    }

    private void Update()
    {
        if (monsterPrefabs.Length == 0) return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;

            Monster_Controller selectedPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
            Monster_Controller monster = monsterPools[selectedPrefab.name].GetObject();

            if (monster != null)
            {
                monster.SetPrefabName(selectedPrefab.name);
                monster.transform.position = GetSpawnPosition();
            }
            else
            {
                Debug.LogWarning("풀에 몬스터 없음");
                return;
            }
        }
    }

    public void ReturnMonster(Monster_Controller monster)
    {
        if (monsterPools.ContainsKey(monster.PrefabName))
        {
            monsterPools[monster.PrefabName].ReturnObject(monster);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        float x = Random.Range(0, 2) == 0 ? Random.Range(-10f, -8f) : Random.Range(8f, 10f);
        float y = Random.Range(0, 2) == 0 ? Random.Range(-5f, -4f) : Random.Range(4f, 5f);
        return new Vector3(x, y, 0);
    }
}
