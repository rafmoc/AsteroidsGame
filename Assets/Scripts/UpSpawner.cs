using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpSpawner : MonoBehaviour
{
    public PowerUp powerUp;
    public HealthUp healthUp;
    public Ship player;

    public float HealthUpTimerMax = 120f;
    public float PowerUpTimerMax = 80f;

    private float HealthUpTimer;
    private float PowerUpTimer;
    private float RandomRangeX;
    private float RandomRangeY;


    
    void Update()
    {
        HealthUpSpawn();
        PowerUpSpawn();
    }

    private void HealthUpSpawn()
    {
        HealthUpTimer -= Time.deltaTime;
        if (HealthUpTimer <= 0f)
        {
            HealthUpTimer = HealthUpTimerMax;
            if(player.GetComponent<Ship>().Health < 3)
            {
                RandomRangeX = Random.Range(-9, 9);
                RandomRangeY = Random.Range(-4.5f, 4.5f);
                HealthUp health = Instantiate(healthUp, transform.position + new Vector3(RandomRangeX, RandomRangeY, 0), new Quaternion(0, 0, 0, 0));
            }
        }
    }

    private void PowerUpSpawn()
    {
        PowerUpTimer -= Time.deltaTime;
        if (PowerUpTimer <= 0f)
        {
            PowerUpTimer = PowerUpTimerMax;
            RandomRangeX = Random.Range(-9, 9);
            RandomRangeY = Random.Range(-4.5f, 4.5f);
            PowerUp power = Instantiate(powerUp, transform.position + new Vector3(RandomRangeX, RandomRangeY, 0), new Quaternion(0, 0, 0, 0));
        }
    }
}
