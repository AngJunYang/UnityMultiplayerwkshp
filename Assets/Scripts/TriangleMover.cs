using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class TriangleMover : NetworkBehaviour
{
    public float speed;
    // Update is called once per frame
    void FixedUpdate()
    {
        if(!IsServer)return;
        if (transform.position.y > 5.29){
            gameObject.transform.rotation = Quaternion.Euler(0,0,180);
        }
        else if(transform.position.y < -5.29){
            gameObject.transform.rotation = Quaternion.Euler(0,0,0);
        }

        transform.position += (transform.rotation * Vector3.up )* speed * Time.fixedDeltaTime;
    }
}
