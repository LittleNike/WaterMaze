using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

    public Rigidbody2D BulletPrefab;

    private BulletInfo _objectInfo;

    // Use this for initialization
    void Awake()
    {
        _objectInfo = gameObject.AddComponent<BulletInfo>();
        Destroy(gameObject, _objectInfo.GetLifetime());
    }

    void Update()
    {   
        Physics2D.Raycast(transform.position, transform.up);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((!_objectInfo.GetBounceOff() && other.gameObject.layer == 8) || other.gameObject.layer == 9 || other.gameObject.layer == 10)
        {
            Destroy(gameObject);
        }
    }
}
