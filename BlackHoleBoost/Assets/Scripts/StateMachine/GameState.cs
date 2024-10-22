using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GameState
{
    public abstract void StateStart();


    public abstract void StateInProgress();
  
}
