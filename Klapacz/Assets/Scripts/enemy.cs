using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

    public float speed;
    private Transform target;

	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	void FixedUpdate () {
        if(target.transform.position.x-transform.position.x<0.25f && target.transform.position.y - transform.position.y < 0.25f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(Random.value, Random.value), speed * Time.deltaTime);
        }
        
	}
}
