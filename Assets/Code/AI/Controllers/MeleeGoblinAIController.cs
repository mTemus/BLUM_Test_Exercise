using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeleeGoblinAIController : ObjectGenericAIController
{
    public List<GameObject> PatrolPoints;
    public float PatrolSpeed;
    public float ChasingSpeed;
    public float IsHurtDuration;
    public float AttackCooldown;

    // protected override void CreateObjectAI()
    // {
    //     // base.CreateObjectAI();
    //     //
    //     // var aiConstructors = GetComponentsInChildren<AIStateConstructorMono>();
    //     // m_aiStates = new List<AIState>();
    //     //
    //     // foreach (var constructor in aiConstructors)
    //     // {
    //     //     var aiState = constructor.Construct(this);
    //     //
    //     //     if (constructor.IsStartingState)
    //     //     {
    //     //         CurrentAIState.Value = aiState;
    //     //         CurrentAIState.Value.OnStateSet();
    //     //     }
    //     //
    //     //     m_aiStates.Add(aiState);
    //     // }
    //     //
    //     //
    //     // var patrolPackage = new PatrolAIState.PatrolAIStatePackage
    //     // {
    //     //     PatrolPoints = PatrolPoints.Select(point => point.transform.position).ToList(),
    //     //     PatrolSpeed = PatrolSpeed,
    //     //     Controller = this
    //     // };
    //     //
    //     // var diePackage = new DieAIState.DieAIStatePackage
    //     // {
    //     //     Controller = this
    //     // };
    //     //
    //     // var isHurtPackage = new IsHurtAIState.IsHurtAIStatePackage
    //     // {
    //     //     Controller = this,
    //     //     StateTime = IsHurtDuration
    //     // };
    //     //
    //     // var chasePackage = new ChaseAIState.ChaseAIStatePackage
    //     // {
    //     //     Controller = this,
    //     //     ChasingSpeed = ChasingSpeed,
    //     //     TriggerHandler = GetComponentInRoot<ObjectAITriggerAreaHandler>(),
    //     // };
    //     //
    //     // var attackPackage = new AttackAIState.AttackAIStatePackage
    //     // {
    //     //     Controller = this,
    //     //     AttackCooldown = AttackCooldown,
    //     // };
    //     //
    //     // m_aiStates = new List<AIState>
    //     // {
    //     //     new PatrolAIState(patrolPackage),
    //     //     new DieAIState(diePackage),
    //     //     new IsHurtAIState(isHurtPackage),
    //     //     new ChaseAIState(chasePackage),
    //     //     new AttackAIState(attackPackage),
    //     // };
    //     //
    //     // CurrentAIState.Value = m_aiStates[0];
    //     // CurrentAIState.Value.OnStateSet();
    // }

   
}
