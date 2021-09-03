using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Asteroid : MonoBehaviour
{
    public GameObject Center;
    public Asteroid SmallAsteroidPrefab;

    public float Speed = 200f;
    public int health = 15;
    public bool isBig = true;

    private Asteroid asteroidSmall;
    private GameObject score;
    private bool splited = false;
    public void SetSplit(bool split) { splited = split; }
    

    void Start()
    {
        score = GameObject.Find("Score");
        if (splited)
        {
            Speed *= 0.5f;
        }
        Vector2 moveDir = (Center.transform.position + new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 5f), 0) - transform.position).normalized;
        gameObject.GetComponent<Rigidbody2D>().AddForce(moveDir * Speed * Random.Range(0.5f, 1.5f));
        Destroy(gameObject, 30f);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            health -= collision.gameObject.GetComponent<Projectile>().damage;
            if(health <= 0)
            {
                if(isBig) 
                {
                    asteroidSmall = Instantiate(SmallAsteroidPrefab, transform.position, new Quaternion(0, 0, Random.Range(0, 360), 0));
                    asteroidSmall.gameObject.GetComponent<Asteroid>().SetSplit(true);
                    asteroidSmall = Instantiate(SmallAsteroidPrefab, transform.position, new Quaternion(0, 0, Random.Range(0, 360), 0));
                    asteroidSmall.gameObject.GetComponent<Asteroid>().SetSplit(true);
                }
                string temp = score.GetComponent<Text>().text.Substring(6);
                int tempScore = int.Parse(temp);
                score.GetComponent<Text>().text = "Score: " + (tempScore + 5);
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Ship>().GetHit();
            Destroy(gameObject);
        }
    }
}
