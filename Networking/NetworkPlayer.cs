using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;

public class NetworkPlayer : NetworkBehaviour
{
    [SerializeField] private Rigidbody rigidBody;

    [SerializeField] private float movementSpeed;

    private float yaw;
    private float pitch;

    public override void OnNetworkSpawn()
    {
        if (IsLocalPlayer)
        {
            base.OnNetworkSpawn();
            GameObject obj = GameObject.FindGameObjectWithTag("Spawn");
            transform.position = obj.transform.position;
            transform.rotation = obj.transform.rotation;
            Camera.main.transform.parent= transform;
            Camera.main.transform.position = transform.position;
            Camera.main.transform.localEulerAngles = Vector3.zero;
        }
    }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsLocalPlayer)
        {
            yaw += 5 * Input.GetAxis("Mouse X");
            pitch -= 5 * Input.GetAxis("Mouse Y");
            pitch = Mathf.Clamp(pitch, -45, 45);

            Camera.main.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
            transform.eulerAngles = new Vector3(0, yaw, 0);
        }

    }

    private void FixedUpdate()
    {
        //rigidBody.MovePosition(transform.position + ((Input.GetAxisRaw("Horizontal") * transform.right) + (Input.GetAxisRaw("Vertical") * transform.forward) * movementSpeed * Time.deltaTime));
        rigidBody.MovePosition(transform.position + ((Input.GetAxisRaw("Vertical") * transform.forward) * movementSpeed * Time.deltaTime));
    }
}
