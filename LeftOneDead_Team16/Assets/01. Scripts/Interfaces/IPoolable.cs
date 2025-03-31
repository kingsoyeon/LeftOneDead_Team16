using UnityEngine.Pool;

/// <summary>
/// OnSpawn : 초기화 OnDespawn : 풀로 돌아갈 때 
/// </summary>
public interface IPoolable
{
   void OnSpawn(); 
   void OnDespawn(); 
}
