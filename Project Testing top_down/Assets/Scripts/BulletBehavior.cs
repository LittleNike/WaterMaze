using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

    public Rigidbody2D BulletPrefab;

    public float Bulletspeed = 10;
    public bool BounceOff;

    // Use this for initialization
    void Awake()
    {
        Destroy(gameObject, 1.0f);
    }

    void Update()
    {   
        Physics2D.Raycast(transform.position, transform.up);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if(other.gameObject.CompareTag("Walls") && bounceOff)
        //{


        //    //Debug.Log(gameObject.transform.rotation.eulerAngles.z);
        //    //gameObject.transform.Rotate(0, 0, 360 - gameObject.transform.rotation.eulerAngles.z);
        //    //Debug.Log(gameObject.transform.rotation.eulerAngles.z);
        //    //gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * bulletspeed;
            
        //    //if (gameObject.transform.rotation.eulerAngles.z <= 180)
        //    //{
        //    //    gameObject.transform.Rotate(0, 0, 360 - gameObject.transform.rotation.eulerAngles.z);
        //    //    Debug.Log(gameObject.transform.rotation.eulerAngles.z);
        //    //    gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * bulletspeed;
        //    //}
        //    //if (gameObject.transform.rotation.eulerAngles.z <= 180)
        //    //{
        //    //    gameObject.transform.Rotate(0, 0, 360 - gameObject.transform.rotation.eulerAngles.z);
        //    //    Debug.Log(gameObject.transform.rotation.eulerAngles.z);
        //    //    gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * bulletspeed;
        //    //}
        //    //else
        //    //{
        //    //    gameObject.transform.Rotate(0, 0, 180 - gameObject.transform.rotation.eulerAngles.z);
        //    //    Debug.Log(gameObject.transform.rotation.eulerAngles.z);
        //    //}
        //}
        if (other.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        
    }

    public float GetBulletspeed()
    {
        return Bulletspeed;
    }

    //public void Shoots(Transform spawn)
    //{
    //    Vector2 bulletSpawn = new Vector2(spawn.position.x, spawn.position.y);
    //    Rigidbody2D clone;
    //    clone = Instantiate(this.gameObject.GetComponent<Rigidbody2D>(), bulletSpawn, spawn.rotation);
    //    clone.velocity = spawn.up * bulletspeed;
    //}
}
