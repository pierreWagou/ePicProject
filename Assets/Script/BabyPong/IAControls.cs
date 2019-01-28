using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAControls : MonoBehaviour {
    public float speed = 10.0f;
    public float boundY = 1.14f;
    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        var vel = rb2d.velocity;
        var pos = transform.position;
        vel.y = speed;
        rb2d.velocity = vel;
    }
	
	// Update is called once per frame
	void Update () {
        var vel = rb2d.velocity;
        var pos = transform.position;
        
        rb2d.velocity = vel;
        if (pos.y > boundY) {
            pos.y = boundY;
            vel.y = -speed;
            rb2d.velocity = vel;
        }
        if (pos.y < -boundY) {
            pos.y = -boundY;
            vel.y = speed;
            rb2d.velocity = vel;
        }
        rb2d.velocity = vel;
        transform.position = pos;
	}
}
