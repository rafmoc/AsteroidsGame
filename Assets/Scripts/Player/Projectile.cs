using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 2f;
    public int damage = 5;

    public float lifeTimerMax = 2f;

    private void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * Speed);
        Destroy(gameObject, lifeTimerMax);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}