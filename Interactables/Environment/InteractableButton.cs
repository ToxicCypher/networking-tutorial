using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class InteractableButton : InteractableObject
{
    private NetworkVariable<bool> b_pressedFlag = new NetworkVariable<bool>(); //Syncronize Open State
    private NetworkVariable<bool> b_canBePressed = new NetworkVariable<bool>();

    [SerializeField] Transform _objectToMove;   //Transform of door/drawer/etc to move
    [SerializeField] int _buttonId;  


    private void Awake()
    {
        b_canBePressed.Value = true;
        b_pressedFlag.Value = false;
        if (!IsHost)
        {
            b_pressedFlag.OnValueChanged += (last, current) =>
            {
                //Do Nothing, this is just mandatory to "Listen" for change
            };
        }
    }


    private void Update()
    {
        if (b_pressedFlag.Value && b_canBePressed.Value)
        {
            _objectToMove.transform.position -= new Vector3(_objectToMove.transform.position.x - .5f, _objectToMove.transform.position.y, _objectToMove.transform.position.z);
            b_canBePressed.Value = false;
            SVS.UpdateButtonPress(_buttonId);
        }
    }

    public override void InteractWithObject()
    {
        if (IsHost)
        {
            b_pressedFlag.Value = !b_pressedFlag.Value;   //Straight up just affect the value
        }
        else
        {
            InteractWithObjectOnServerRpc();    //Tell server to straight up just affect the value
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void InteractWithObjectOnServerRpc()
    {
        b_pressedFlag.Value = !b_pressedFlag.Value; //Straight up just affect the value
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetMouseButtonDown(0))
            {
                b_pressedFlag.Value = true;
            }
        }
    }

}
