using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LANDLEVEL
{
    LV0,
    LV1,
    LV2,
    LV3,
}

public class Land : Grid
{
    public PLAYER owner;
    public LANDLEVEL landLevel;

    void Start() 
    {
        cellType = CELLTYPE.EDGE;
    }
}
