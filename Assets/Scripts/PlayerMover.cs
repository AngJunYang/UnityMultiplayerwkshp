using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using FishNet.Object;

public class PlayerMover : NetworkBehaviour {
 
    public float speed;
    private Rigidbody2D rb2d;
 
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        GetComponent<SpriteRenderer>().color = UnityEngine.Random.ColorHSV();
    }
 
    void FixedUpdate()
    {
        if(!IsOwner)return;

        Vector2 kb_input = new Vector2(Input.GetAxisRaw ("Horizontal"),  Input.GetAxisRaw ("Vertical")).normalized;
        rb2d.velocity = kb_input * speed * Time.fixedDeltaTime;

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        FishNet.InstanceFinder.ClientManager.StopConnection();
    }
}
 
