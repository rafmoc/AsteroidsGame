using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 30f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Ship>().Health++;
            collision.gameObject.GetComponent<Ship>().ChangeHpGraphic();
            Destroy(gameObject);
        }
    }
}
