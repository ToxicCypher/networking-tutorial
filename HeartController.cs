using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class HeartController : NetworkBehaviour
{

    [SerializeField] private NetworkVariable<Transform> b_heartTransform = new NetworkVariable<Transform>(); 
    private bool canGrow;

    private void Awake()
    {
        canGrow = false;
        if (!IsHost)
        {
            b_heartTransform.OnValueChanged += (last, current) =>
            {
                //Do Nothing, this is just mandatory to "Listen" for change
            };
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canGrow)
        {
            b_heartTransform.Value.localScale *= 1.001f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canGrow= true;
        }
    }
}
