using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Connection;

public class PlayerMover : NetworkBehaviour
{

    public float speed;
    private Rigidbody2D rb2d;

    public static bool MATCH_STARTED = false;

    [SyncVar(OnChange = nameof(OnBodyColorChange))]
    public Color bodyColor;

    public bool triggerSceneSwitch = false;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (triggerSceneSwitch && IsServer)
        {
            triggerSceneSwitch = false;
            MATCH_STARTED = true;
            SceneLoader.SwitchScene(gameObject.GetComponent<NetworkObject>());
        }
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        bodyColor = UnityEngine.Random.ColorHSV();
    }

    void OnBodyColorChange(Color prev, Color next, bool asServer)
    {
        GetComponent<SpriteRenderer>().color = next;
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
        if (!IsOwner) return;

        NetworkBehaviour behav = col.gameObject.GetComponentInChildren<NetworkBehaviour>();
        if (behav != null && behav.OwnerId == OwnerId)
        {
            return;
        }

        if (MATCH_STARTED)
            FishNet.InstanceFinder.ClientManager.StopConnection();
        else
        {
            if (behav != null)
            {
                ResetPosition();
            }

        }
    }

    void ResetPosition()
    {
        gameObject.transform.position = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));

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

