using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/02/2024]
 * [Holds information for each dialogue]
 */

[System.Serializable]
public class Dialogue
{
    public Sentence[] sentences;

    public bool unlockAcceleration = false;
    public bool unlockStrafing = false;
    public bool unlockBlackhole = false;
    public bool unlockShooting = false;

    [TextArea(3, 10)]
    public string objectiveText;
}
