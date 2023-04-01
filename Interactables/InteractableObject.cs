using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;

public abstract class InteractableObject : NetworkBehaviour
{
    public abstract void InteractWithObject();
}
