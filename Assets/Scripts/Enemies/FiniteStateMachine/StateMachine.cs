// based on code from https://game.courses/bots-ai-statemachines/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine
{
    private IState currentState;

    private Dictionary<Type, List<Transition>> transitions = new Dictionary<Type,List<Transition>>();
    private List<Transition> anyTransitions = new List<Transition>();
    private List<Transition> currentTransitions;
    private static List<Transition> EmptyTransitions = new List<Transition>(0);


    public void UpdateStateMachine()
    {
        var transition = GetTransition();
        if (transition != null)
        {
            SetState(transition.NextState);
        }
        currentState.StateUpdate();
    }


    public void SetState(IState state)
    {
        if (state == currentState)
            return;

        currentState?.OnStateExit();
        currentState = state;

        transitions.TryGetValue(currentState.GetType(), out currentTransitions);
        if (currentTransitions == null)
        {
            currentTransitions = EmptyTransitions;
        }

        currentState.OnStateEnter();
    }


    public IState GetCurrentState()
    {
        return currentState;
    }

    
    public void AddTransition(IState from, IState nextState, Func<bool> predicate)
    {
      if (transitions.TryGetValue(from.GetType(), out var stateTransitions) == false)
      {
          stateTransitions = new List<Transition>();
          transitions[from.GetType()] = stateTransitions;
      }
      
      stateTransitions.Add(new Transition(nextState, predicate));
    }
    

    public void AddAnyTransition(IState state, Func<bool> predicate)
    {
        anyTransitions.Add(new Transition(state, predicate));
    }


    private Transition GetTransition()
    {
        foreach(var transition in anyTransitions)
            if (transition.Condition())
            return transition;

        foreach (var transition in currentTransitions)
            if (transition.Condition())
            return transition;

        return null;
    }
}
