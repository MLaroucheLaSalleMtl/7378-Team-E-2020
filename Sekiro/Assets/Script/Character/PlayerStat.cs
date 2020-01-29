using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : CharacterStat
{
    private Vector3 lastPosition;
    //private Transform currentPosition = Transform.fi;

    //public Vector3 PlayerPosition()
    //{

    //}
    public Vector3 LastPlayerPosition()
        => lastPosition;
}
