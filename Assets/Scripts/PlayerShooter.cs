using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

public class PlayerShooter : NetworkBehaviour
{
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        if (Input.GetMouseButtonDown(0))
        {
            SpawnBullet(Owner);
        }
    }

    [ServerRpc]
    private void SpawnBullet(NetworkConnection conn)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Spawn(bullet, conn);

    }
}
