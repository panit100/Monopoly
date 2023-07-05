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

public class LandScript : MonoBehaviour
{
    public PLAYER owner;
    public LANDLEVEL landLevel;
}
