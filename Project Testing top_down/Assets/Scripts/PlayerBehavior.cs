using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour {

    public int Health = 100;
    public float Speed = 3;
    public float RotationSpeed = 3;
    //public float bulletspeed = 10;
    public float ShootCooldown = 0.5f;

    public float SpeedboostTimer;
    public float NoReloadTimer;
    
    public Rigidbody2D BulletPrefab;
    public float Bulletspeed = 10;
    public Transform BulletSpawnRight;
    public Transform BulletSpawnLeft;

    public KeyCode Top;
    public KeyCode Left;
    public KeyCode Down;
    public KeyCode Right;
    public KeyCode LeftCannon;
    public KeyCode RightCannon;

    public Image Fill;

    private Rigidbody2D _rb;
    private float _timeStamp;
    private Slider _healthSlider;

    //private BulletBehavior shootingScript;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();
        _healthSlider = GetComponentInChildren<Slider>();
        //shootingScript = gameObject.AddComponent<BulletBehavior>();
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

        if (Input.GetKeyDown(LeftCannon))
        {
            Fire('l');
        }
        if (Input.GetKeyDown(RightCannon))
        {
            Fire('r');
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Bullet":
                Health -= 10;
                _healthSlider.value = Health;
                Fill.color = Color.Lerp(Color.red, Color.green, 1);
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
            Destroy(gameObject, 0.0f);
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

    void Fire(char side)
    {
        if (_timeStamp <= Time.time)
        {
            if (side == 'l')
            {
                Vector2 bulletSpawnPositionLeft = new Vector2(BulletSpawnLeft.position.x, BulletSpawnLeft.position.y);
                Rigidbody2D cloneLeft;
                cloneLeft = Instantiate(BulletPrefab, bulletSpawnPositionLeft, transform.rotation);
                cloneLeft.velocity = transform.up * Bulletspeed; //shootingScript.GetBulletspeed();

                //shootingScript.Shoots(bulletSpawnLeft);
            }

            if (side == 'r')
            {
                Vector2 bulletSpawnPositionRight = new Vector2(BulletSpawnRight.position.x, BulletSpawnRight.position.y);
                Rigidbody2D cloneRight;
                cloneRight = Instantiate(BulletPrefab, bulletSpawnPositionRight, transform.rotation);
                cloneRight.velocity = transform.up * Bulletspeed;
            }
            _timeStamp = Time.time + ShootCooldown;
        }
    }
}
