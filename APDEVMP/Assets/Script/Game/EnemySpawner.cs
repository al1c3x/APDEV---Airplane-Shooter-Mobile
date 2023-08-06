using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    int waves = 25;
    public GameObject EnemyGO; 
    public GameObject EnemyBossGO;
    public GameObject EnemyBossGO2;
    public GameObject EnemyBossGO3;

    GameObject anEnemy;
    public GameObject playerShip;
    public float spawnTimer = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update() {
       

    }




    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

       anEnemy = (GameObject)Instantiate(EnemyGO);
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        
        SpawnEnemyRelease();
        waves--;
       
    }
    void SpawnBoss()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        int randomboss = Random.Range(0, 2);
        
        if (randomboss == 0)
        {
           anEnemy = (GameObject)Instantiate(EnemyBossGO);
        }
        else if(randomboss == 1)
        {
            anEnemy = (GameObject)Instantiate(EnemyBossGO2);
        }
        else
        {
            anEnemy = (GameObject)Instantiate(EnemyBossGO3);
        }
       
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        
    }
    void SpawnEnemyRelease()
    {
        float timer;

        if (spawnTimer > 1.0f)
        {
            timer = Random.Range(1.0f, spawnTimer);
        }
        else
        {
            timer = 1.0f;
        }

        CallEnemy(timer);
        
    }
    public void CallEnemy(float timer)
    {
        if (waves == 0)
        {
            Invoke("SpawnBoss", timer);
        }
        else
        {
            Invoke("SpawnEnemy", timer);
        }
    }

    void LevelDiff()
    {
        if (spawnTimer > 1.0f)
        {
            spawnTimer--;
        }
        if (spawnTimer == 1)
        {
            CancelInvoke("LevelDiff");
        }
    }
    public void StartSpawner()
    {
        spawnTimer = 5.0f;
        Invoke("SpawnEnemy", spawnTimer);
        InvokeRepeating("LevelDiff", 0, 30);

    }

    public void StopSpawner()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("LevelDiff");
        Destroy(anEnemy);
    }

  
}
