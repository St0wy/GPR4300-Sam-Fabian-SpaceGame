using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private Transform[] lines;

        [Header("EnemyPrefabs")]
        [SerializeField] private GameObject enemyOne;
        [SerializeField] private GameObject enemyTwo;

        [Header("Screen size value")]
        [SerializeField] private float minPos = -8.0f;
        [SerializeField] private float maxPos = 8.0f;

        [Header("Timer")]
        [SerializeField] private float spawnEnemyOneTimerLimit = 5.0f;
        private float spawnEnemyOneTimer = 0.0f;
        [SerializeField] private float spawnEnemyTwoTimerLimit = 10.0f;
        float spawnEnemyTwoTimer = 0.0f;


        private void Update()
        {
            spawnEnemyOneTimer += Time.deltaTime;
            spawnEnemyTwoTimer += Time.deltaTime;
            
            if(spawnEnemyOneTimer >= spawnEnemyOneTimerLimit)
            {
                InstantiateEnemyOne();
                spawnEnemyOneTimer = 0.0f;
            }
            if(spawnEnemyTwoTimer >= spawnEnemyTwoTimerLimit)
            {
                InstantiateEnemyTwo();
                spawnEnemyTwoTimer = 0.0f;
            }
        }

        private void InstantiateEnemyOne()
        {
            Vector3 spawnPos = transform.position;
            spawnPos.x = Random.Range(minPos, maxPos);
            Instantiate(enemyOne, spawnPos, Quaternion.identity);
        }
        private void InstantiateEnemyTwo()
        {
            GameObject newEnemy = Instantiate(enemyTwo);
            newEnemy.GetComponent<EnemyTwoInputHandler>().Lines = lines;

        }

    }
}
