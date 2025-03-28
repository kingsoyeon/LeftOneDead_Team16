using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// UI 상태 
/// </summary>
public interface IUIState
{
    public void Enter() { }
    public void Exit() { }
    public void HandleInput();
    public void Update();
    public void PhysicsUpdate();
}
