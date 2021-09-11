using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Transition
{
    public Func<bool> Condition {get; }
    public IState NextState { get; }
    
    public Transition(IState nextState, Func<bool> condition)
    {
        NextState = nextState;
        Condition = condition;
    }
}
