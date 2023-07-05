using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CELLTYPE
{
    EDGE,
    LAND,
}

public class Grid : MonoBehaviour
{
    public CELLTYPE cellType;

    [SerializeField] Grid NextCell;
}
