using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using FishNet.Object;

public class PlayerMover : NetworkBehaviour
{

    public float speed;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = UnityEngine.Random.ColorHSV();
    }

    void FixedUpdate()
    {
        if (!IsOwner) return;

        Vector2 kb_input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb2d.velocity = kb_input * speed * Time.fixedDeltaTime;
        RotateGameObjectTowardsMouse();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        NetworkBehaviour behav = col.gameObject.GetComponentInChildren<NetworkBehaviour>();
        if (behav != null && behav.OwnerId == OwnerId)
        {
            return;
        }

        FishNet.InstanceFinder.ClientManager.StopConnection();
    }

    void RotateGameObjectTowardsMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}

