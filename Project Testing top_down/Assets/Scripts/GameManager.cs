using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //Variables
    public float SpawnTimer;
    public Rigidbody2D PickUpPrefab;

    private float _timeStamp;
    private int _rnd;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        SpawnPickUp();

    }

    void SpawnPickUp()
    {
        if (_timeStamp <= Time.time)
        {
            _rnd = Random.Range(0, 100);

            if (_rnd <= 50)
            {
                Vector2 pickUpSpawnPosition = new Vector2(Random.Range(-7, 7), Random.Range(-3, 4));
                Rigidbody2D clone;
                clone = Instantiate(PickUpPrefab, pickUpSpawnPosition, Quaternion.identity);
                clone.gameObject.tag = "Speedboost";
                clone.GetComponent<Renderer>().material.color = Color.yellow;

            }
            if (_rnd >= 50)
            {
                Vector2 pickUpSpawnPosition = new Vector2(Random.Range(-7, 7), Random.Range(-3, 4));
                Rigidbody2D clone;
                clone = Instantiate(PickUpPrefab, pickUpSpawnPosition, Quaternion.identity);
                clone.gameObject.tag = "NoReload";
                clone.GetComponent<Renderer>().material.color = Color.green;
            }
            _timeStamp = Time.time + SpawnTimer;
        }
    }
}
