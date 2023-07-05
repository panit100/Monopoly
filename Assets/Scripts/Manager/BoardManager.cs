using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager inst;

    [SerializeField] GameObject edgePrefab;
    [SerializeField] GameObject landPrefab;

    [SerializeField] int boardSize;
    

    void CreateBoard()
    {

    }
}
