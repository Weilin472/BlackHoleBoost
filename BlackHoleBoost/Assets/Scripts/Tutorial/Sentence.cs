using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/02/2024]
 * [holds information for each sentence in the game]
 */

[System.Serializable]
public class Sentence
{
    [TextArea(3, 10)]
    public string sentence;

    [Header("Object Outline")]
    public bool outline;
    public BaseHighlight highlight;
}
