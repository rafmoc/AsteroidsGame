using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid BigAsteroidPrefab;
    public Asteroid SmallAsteroidPrefab;

    public int amountOfSmallAsteroids = 2;
    public int amountOfBigAsteroids = 1;
    public float shootTimerBigMax = 2f;
    public float shootTimerSmallMax = 5f;

    private Asteroid asteroidBig;
    private Asteroid asteroidSmall;

    private float shootTimerBig;
    private float shootTimerSmall;
    private float RandomRangeX;
    private float RandomRangeY;

    void Update()
    {
        AsteroidSpawnerFunction();
    }

    private void AsteroidSpawnerFunction()
    {
        shootTimerBig -= Time.deltaTime;
        if (shootTimerBig <= 0f)
        {
            shootTimerBig = shootTimerBigMax;
            for (int i = 0; i < amountOfBigAsteroids; i++)
            {
                RandomRangeX = Random.Range(-18, 18);
                RandomRangeY = Random.Range(-18, 18);

                if ((RandomRangeX < 12) && (RandomRangeX > -12)) { RandomRangeX = RandomRangeX < 0 ? -12 : 12; }
                if ((RandomRangeY < 7) && (RandomRangeY > -7)) { RandomRangeY = RandomRangeY < 0 ? -7 : 7; }

                asteroidBig = Instantiate(BigAsteroidPrefab, transform.position + new Vector3(RandomRangeX, RandomRangeY, 0), new Quaternion(0, 0, Random.Range(0, 360), 0));
            }
        }

        shootTimerSmall -= Time.deltaTime;
        if (shootTimerSmall <= 0f)
        {
            shootTimerSmall = shootTimerSmallMax;
            for (int i = 0; i < amountOfSmallAsteroids; i++)
            {
                RandomRangeX = Random.Range(-18, 18);
                RandomRangeY = Random.Range(-18, 18);

                if ((RandomRangeX < 12) && (RandomRangeX > -12)) { RandomRangeX = RandomRangeX < 0 ? -12 : 12; }
                if ((RandomRangeY < 7) && (RandomRangeY > -7)) { RandomRangeY = RandomRangeY < 0 ? -7 : 7; }

                asteroidSmall = Instantiate(SmallAsteroidPrefab, transform.position + new Vector3(RandomRangeX, RandomRangeY, 0), new Quaternion(0, 0, Random.Range(0, 360), 0));
            }
        }
    }
}
