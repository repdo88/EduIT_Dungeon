using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBaseState : MonoBehaviour
{
    public virtual void OnEnterState()
    {
    }

    public virtual void UpdateState() // this could be the Update Method from Unity too
    {
    }

    public virtual void OnExitState()
    {
    }
}
