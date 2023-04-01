using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class InteractableContainer : InteractableObject
{
    private NetworkVariable<bool> b_openFlag = new NetworkVariable<bool>(); //Syncronize Open State

    [SerializeField] Transform _objectToMove;   //Transform of door/drawer/etc to move

    [SerializeField] float _rotateSpeed;
    [SerializeField] float _openRotationClamp;

    private float _curRotation = 0;


    private void Awake()
    {
        if (!IsHost)
        {
            b_openFlag.OnValueChanged += (last, current) =>
            {
                //Do Nothing, this is just mandatory to "Listen" for change
            };
        }
    }

    private void Update()
    {
        if(b_openFlag.Value)
        {
            float adjust = _curRotation + Time.deltaTime * _rotateSpeed;
            _curRotation = Mathf.Clamp(adjust, 0, _openRotationClamp);
        }
        else
        {
            float adjust = _curRotation - Time.deltaTime * _rotateSpeed;
            _curRotation = Mathf.Clamp(adjust, 0, _openRotationClamp);
        }

        Vector3 euler = _objectToMove.localEulerAngles;
        euler.y = _curRotation;

        _objectToMove.eulerAngles = euler;
    }

    public override void InteractWithObject()
    {
        if (IsHost)
        {
            b_openFlag.Value = !b_openFlag.Value;   //Straight up just affect the value
        }
        else
        {
            InteractWithObjectOnServerRpc();    //Tell server to straight up just affect the value
        }
    }

    [ServerRpc]
    public void InteractWithObjectOnServerRpc()
    {
        b_openFlag.Value = !b_openFlag.Value; //Straight up just affect the value
    }

    
}
