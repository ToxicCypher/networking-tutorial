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
    private bool fuckoff;


    private void Awake()
    {
        fuckoff = true;
        b_canBePressed.Value = false;
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
        if (fuckoff == true)
        {
            if (b_canBePressed.Value)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    InteractWithObject();
                }
            }
            if (b_pressedFlag.Value)
            {
                _objectToMove.transform.position = new Vector3(_objectToMove.transform.position.x - .25f, _objectToMove.transform.position.y, _objectToMove.transform.position.z);
                fuckoff = false;
                SVS.UpdateButtonPress(_buttonId);
            }
        }
    }

    public override void InteractWithObject()
    {
        if (IsHost)
        {
            b_pressedFlag.Value = true;   //Straight up just affect the value
        }
        else
        {
            InteractWithObjectOnServerRpc();    //Tell server to straight up just affect the value
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void InteractWithObjectOnServerRpc()
    {
        b_pressedFlag.Value = true; //Straight up just affect the value
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            b_canBePressed.Value = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            b_canBePressed.Value = false;
        }
    }

}
