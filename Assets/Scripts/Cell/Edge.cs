using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDGELEVEL
{
    LV0,
    LV1,
    LV2,
    LV3,
}

public class Edge : Grid
{
    public TEAM owner;
    public EDGELEVEL landLevel;

    void Start() 
    {
        cellType = CELLTYPE.EDGE;
    }
}
