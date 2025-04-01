using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine
{
   protected IState currentState;
   public IState beforeState;


   public void ChangeState(IState newState)
   {
      currentState?.Exit();
      beforeState = currentState;
      currentState = newState;
      currentState?.Enter();
   }
   
   public void Update()
   {
      currentState?.Update();
   }

   public void FixedUpdate()
   {
      currentState?.FixedUpdate();
   }

}
