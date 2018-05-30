using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour {

    public int Health = 100;
    public float Speed = 3;
    public float RotationSpeed = 10;
    public float ShootCooldown = 0.5f;

    public bool IsControler;

    public float SpeedboostTimer;
    public float NoReloadTimer;
    
    public GameObject BulletPrefab;
    public Transform BulletSpawn;

    public KeyCode Top;
    public KeyCode Left;
    public KeyCode Down;
    public KeyCode Right;
    public KeyCode Cannon;
    public KeyCode Use;

    public Image Fill;

    private Rigidbody2D _rb;
    private float _timeStamp;
    private Slider _healthSlider;

    private BulletInfo _shootingScript;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();
        _healthSlider = GetComponentInChildren<Slider>();
        _shootingScript = gameObject.AddComponent<BulletInfo>();
        if(IsControler)
        {
            RotationSpeed = 1.5f;
        }
	}

    void FixedUpdate()
    {
        if (IsControler)
        {
            _rb.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Speed);
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            float angle = Mathf.Atan2(-h, v) * Mathf.Rad2Deg;
            Quaternion newDir = Quaternion.identity;
            newDir.eulerAngles = new Vector3(0, 0, angle);
            transform.rotation = Quaternion.Slerp(transform.rotation, newDir, Time.deltaTime * RotationSpeed);

            if(Input.GetKeyDown("joystick button 0"))
            {
                Fire();
            }
        }
        else
        { 
            if (Input.GetKey(Top))
            {
                _rb.AddForce(transform.up * Speed);
            }
            if (Input.GetKey(Left))
            {
                _rb.AddTorque(RotationSpeed);
            }
            //if (Input.GetKey(Down))
            //{
            //    _rb.AddForce(-transform.up * Speed / 2);
            //}
            if (Input.GetKey(Right))
            {
                _rb.AddTorque(-RotationSpeed);
            }
            if (Input.GetKeyDown(Cannon))
            {
                Fire();
            }
            if(Input.GetKeyDown(Use))
            {
                Debug.Log("Using...");
            }
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
            clone.GetComponentInChildren<Rigidbody2D>().velocity = transform.up * _shootingScript.GetBulletspeed();
            clone.layer = gameObject.layer;
            _timeStamp = Time.time + ShootCooldown;
        }
    }

    void IsHit()
    {
        Health -= (int)_shootingScript.GetBulletDamage();
        _healthSlider.value = Health;
        //Fill.color = Color.Lerp(Color.red, Color.green, 1);
    }
}
