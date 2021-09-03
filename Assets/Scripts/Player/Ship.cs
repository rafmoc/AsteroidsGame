using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    public Projectile projectilePrefab;
    public float AccelerationSpeed;
    public float TurnSpeed;

    private Rigidbody2D Rigidbody;
    private GameObject Hp1, Hp2, Hp3;
    private float rotationDirection;
    private float acceleration;
    private float powerUpTimer;
    private float powerUpTimerMax = 10f;
    private float shootTimerMax = 0.2f;
    private float shootTimer;
    private int health;
    private bool powerUped = false;
    public int Health { get => health; set => health = value; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Hp1 = GameObject.Find("Hp1");
        Hp2 = GameObject.Find("Hp2");
        Hp3 = GameObject.Find("Hp3");
        health = 3;
        powerUpTimer = powerUpTimerMax;
    }

    private void Update()
    {
        rotationDirection = -1 * Input.GetAxis("Horizontal") * TurnSpeed;
        acceleration = Input.GetAxis("Vertical") > 0.5f ? Input.GetAxis("Vertical") : 0;

        if (Input.GetMouseButton(1))
        {
            MoveByMouse();
        }

        if (powerUped)
        {
            powerUpTimer -= Time.deltaTime;
            if (powerUpTimer <= 0f)
            {
                powerUpTimer = powerUpTimerMax;
                powerUped = false;
            }
        }

        Shoot();
    }

    private void FixedUpdate()
    {
        if (acceleration != 0)
        {
            //Teoretcznie jakby ktoœ gra³ na padzie lub UI na telefonie to acceleration mo¿e byæ miêdzy 0.5f a 1 w tym przypadku.
            Rigidbody.AddForce(transform.up * acceleration * AccelerationSpeed);
        }
        if (rotationDirection != 0)
        {
            Rigidbody.AddTorque(rotationDirection);
        }
    }

    private void Shoot()
    {
        float acctualShootTimerMax = shootTimerMax;
        if (powerUped) { acctualShootTimerMax -= 0.1f; }

        shootTimer -= Time.deltaTime;
        if ((Input.GetKey(KeyCode.Space)) || (Input.GetMouseButton(0)))
        {
            if (shootTimer <= 0f)
            {
                shootTimer = acctualShootTimerMax;
                Projectile projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
                if (powerUped)
                {
                    Quaternion additionalProjectile = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + 0.2f, transform.rotation.w);
                    projectile = Instantiate(projectilePrefab, transform.position, additionalProjectile);
                    additionalProjectile = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z - 0.2f, transform.rotation.w);
                    projectile = Instantiate(projectilePrefab, transform.position, additionalProjectile);
                }
            }
        }
    }

    private void MoveByMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, AccelerationSpeed / 350);

        mousePosition = Input.mousePosition;
        Vector3 shipPosition = Camera.main.WorldToScreenPoint(transform.position);
        mousePosition.x -= shipPosition.x;
        mousePosition.y -= shipPosition.y;
        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle - 90, Vector3.forward), 0.05f);
    }

    public void GetHit()
    {
        health--;
        ChangeHpGraphic();
        if (health <= 0)
        {
            GameObject score = GameObject.Find("Score");
            int tempScore = int.Parse(score.GetComponent<Text>().text.Substring(6));
            PlayerPrefs.SetInt("score", tempScore);
            SceneManager.LoadScene(0);
        }
    }

    public void ChangeHpGraphic()
    {
        if (health == 0) { Hp1.SetActive(false); Hp2.SetActive(false); Hp3.SetActive(false); }
        if (health == 1) { Hp1.SetActive(true); Hp2.SetActive(false); Hp3.SetActive(false); }
        if (health == 2) { Hp1.SetActive(true); Hp2.SetActive(true); Hp3.SetActive(false); }
        if (health == 3) { Hp1.SetActive(true); Hp2.SetActive(true); Hp3.SetActive(true); }
    }

    public void PowerUp()
    {
        powerUped = true;
    }

}
