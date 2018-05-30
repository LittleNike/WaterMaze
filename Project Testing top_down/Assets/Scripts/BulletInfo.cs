using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInfo : MonoBehaviour {


    private float _bulletspeed = 10;
    private float _bulletdamage = 10;
    private float _lifetime = 2;
    private bool _bounceOff;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public float GetBulletspeed()
    {
        return _bulletspeed;
    }

    public float GetBulletDamage()
    {
        return _bulletdamage;
    }

    public float GetLifetime()
    {
        return _lifetime;
    }
    public bool GetBounceOff()
    {
        return _bounceOff;
    }
}
