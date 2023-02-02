using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class BulletMover : NetworkBehaviour
{
    public float speed;
    public float bulletTme;
    // Start is called before the first frame update

    public override void OnStartServer()
    {
        base.OnStartServer();
        StartCoroutine(bulletTimer());

    }

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.rotation * Vector3.up * speed * Time.fixedDeltaTime;
    }

    private IEnumerator bulletTimer()
    {
        yield return new WaitForSeconds(bulletTme);
        Despawn(gameObject);
    }
}
