using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class InteractableDoor : NetworkBehaviour
{

    [SerializeField] GameObject _leftDoor;   //Transform of door/drawer/etc to move
    [SerializeField] GameObject _rightDoor;   //Transform of door/drawer/etc to move
    [SerializeField] int _doorId;  


    private void Awake()
    {

    }


    private void Update()
    {
        
        if (_doorId == 1 && SVS.CheckIfDoorOneCanOpen())
        {
            Debug.Log("Door 1 can be opened");
            if (IsHost)
            {
                Debug.Log("Am Host");
                DestoryDoors();
                DestoryDoorOnServerRpc();
            }
            else if (IsClient)
            {
                Debug.Log("Am Client");
                DestoryDoors();
                DestoryDoorOnClientRpc();
            }
        } 

        if (_doorId == 2 && SVS.CheckIfDoorTwoCanOpen())
        {
            Debug.Log("Door 2 can be opened");
            if (IsHost)
            {
                Debug.Log("Am Host");
                DestoryDoors();
                DestoryDoorOnServerRpc();
            }
            else if (IsClient)
            {
                Debug.Log("Am Client");
                DestoryDoors();
                DestoryDoorOnClientRpc();
            }
        }
    }


    [ServerRpc]
    private void DestoryDoorOnServerRpc()
    {
        DestoryDoors();
        DestoryDoorOnClientRpc();
    }

    [ClientRpc]
    private void DestoryDoorOnClientRpc()
    {
        if (!IsLocalPlayer)
        {
            DestoryDoors();
        }
    }
    
    private void DestoryDoors()
    {
        Destroy(_leftDoor);
        Destroy(_rightDoor);
    }
}
