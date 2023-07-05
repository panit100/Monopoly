using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public int diceFace = 6;

    public int diceNumber = 0;

    void Tossing()
    {
        diceNumber = Random.Range(0,diceFace);
    }
}
