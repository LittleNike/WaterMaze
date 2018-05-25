using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour {

    public int Health = 100;
    public float Speed = 3;
    public float RotationSpeed = 3;
    public float ShootCooldown = 0.5f;

    public float SpeedboostTimer;
    public float NoReloadTimer;
    
    public GameObject BulletPrefab;
    public Transform BulletSpawn;

    public KeyCode Top;
    public KeyCode Left;
    public KeyCode Down;
    public KeyCode Right;
    public KeyCode Cannon;

    public Image Fill;

    private Rigidbody2D _rb;
    private float _timeStamp;
    private Slider _healthSlider;

    private BulletInfo shootingScript;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();
        _healthSlider = GetComponentInChildren<Slider>();
        shootingScript = gameObject.AddComponent<BulletInfo>();
	}

    void FixedUpdate()
    {
        if (Input.GetKey(Top))
        {
            _rb.AddForce(transform.up * Speed);
        }
        if (Input.GetKey(Left))
        {
            _rb.AddTorque(RotationSpeed);
        }
        if (Input.GetKey(Down))
        {
            _rb.AddForce(-transform.up * Speed / 2);
        }
        if (Input.GetKey(Right))
        {
            _rb.AddTorque(-RotationSpeed);
        }
        if (Input.GetKeyDown(Cannon))
        {
            Fire();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Bullet":
                IsHit();
                break;
            case "Speedboost":
                Speed = 6;
                Destroy(other.gameObject);
                SpeedboostTimer = 3;
                break;
            case "NoReload":
                ShootCooldown = 0;
                Destroy(other.gameObject);
                NoReloadTimer = 3;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame  
    void Update ()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }

        if(SpeedboostTimer > 0)
        {
            SpeedboostTimer -= Time.deltaTime;
            if(SpeedboostTimer <= 0)
            {
                Speed = 3;
            }
        }

        if(NoReloadTimer > 0)
        {
            NoReloadTimer -= Time.deltaTime;
            if(NoReloadTimer <= 0)
            {
                ShootCooldown = 0.5f;
            }
        }
	}

    void Fire()
    {
        if (_timeStamp <= Time.time)
        {
            Vector2 bulletspawnPosition = new Vector2(BulletSpawn.position.x, BulletSpawn.position.y);
            GameObject clone;
            clone = Instantiate(BulletPrefab, bulletspawnPosition, transform.rotation);
            clone.GetComponentInChildren<Rigidbody2D>().velocity = transform.up * shootingScript.GetBulletspeed();
            clone.layer = gameObject.layer;
            _timeStamp = Time.time + ShootCooldown;
        }
    }

    void IsHit()
    {
        Health -= (int)shootingScript.GetBulletDamage();
        _healthSlider.value = Health;
        //Fill.color = Color.Lerp(Color.red, Color.green, 1);
    }
}
