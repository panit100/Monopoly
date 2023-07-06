using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager inst;

    [SerializeField] GameObject edgePrefab;
    [SerializeField] GameObject landPrefab;

    [SerializeField] int boardSize;
    [SerializeField] float landDistance = 1;
    
    // void Start()
    // {
    //     CreateBoard();
    // }

    // void CreateBoard()
    // {
    //     Vector3 tempPosition = Vector3.zero;
    //     Vector3 tempRotation = Vector3.zero;

    //     for(int x = 0; x < boardSize; x++)
    //     {
    //         tempPosition.x = x;

    //         for(int z = 0; z < boardSize; z++)
    //         {
    //             tempPosition.z = z;

    //             if((x == 0 || x == boardSize-1) && (z == 0 || z == boardSize-1))
    //             {
    //                 CraeteEdge(tempPosition,tempRotation);
    //             }
    //             else
    //             {
    //                 if(x == 0 || x == boardSize-1)
    //                 {
    //                     CraeteLand(tempPosition,tempRotation);
    //                 }
    //                 else if((z == 0 || z == boardSize-1))
    //                 {
    //                     CraeteLand(tempPosition,tempRotation);
    //                 }
    //             }
    //         }

    //         tempRotation.y += 90;
    //     }
    // }

    // void CraeteEdge(Vector3 position,Vector3 rotation)
    // {
    //     Instantiate(edgePrefab, position,Quaternion.Euler(rotation.x,rotation.y,rotation.z));
    // }

    // void CraeteLand(Vector3 position,Vector3 rotation)
    // {
    //     Instantiate(landPrefab, position,Quaternion.Euler(rotation.x,rotation.y,rotation.z));
    // }
}
