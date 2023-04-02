using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class InteractableDoor : InteractableObject
{
    private NetworkVariable<bool> b_doorCanOpen = new NetworkVariable<bool>(); //Syncronize Open State
    private NetworkVariable<bool> b_doorIsOpen = new NetworkVariable<bool>(); //Syncronize Open State

    [SerializeField] GameObject _leftDoor;   //Transform of door/drawer/etc to move
    [SerializeField] GameObject _rightDoor;   //Transform of door/drawer/etc to move
    [SerializeField] int _doorId;  


    private void Awake()
    {
        b_doorCanOpen.Value = false;
        b_doorIsOpen.Value = false;
        if (!IsHost)
        {
            b_doorCanOpen.OnValueChanged += (last, current) =>
            {
                //Do Nothing, this is just mandatory to "Listen" for change
            };
        }
    }


    private void Update()
    {
        if (_doorId == 1)
        {
            b_doorCanOpen.Value = SVS.CheckIfDoorOneCanOpen();
        }
        if (_doorId == 2)
        {
            b_doorCanOpen.Value = SVS.CheckIfDoorTwoCanOpen();
        }
        if (b_doorCanOpen.Value && !b_doorIsOpen.Value)
        {
            Destroy(_leftDoor);
            Destroy(_rightDoor);
            b_doorIsOpen.Value = true;
        }
    }

    public override void InteractWithObject()
    {
        if (IsHost)
        {
            b_doorCanOpen.Value = !b_doorCanOpen.Value;   //Straight up just affect the value
        }
        else
        {
            InteractWithObjectOnServerRpc();    //Tell server to straight up just affect the value
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void InteractWithObjectOnServerRpc()
    {
        b_doorCanOpen.Value = !b_doorCanOpen.Value; //Straight up just affect the value
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetMouseButtonDown(0))
            {
                b_doorCanOpen.Value = true;
            }
        }
    }

}
