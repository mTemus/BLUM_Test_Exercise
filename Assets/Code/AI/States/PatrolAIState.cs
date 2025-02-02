﻿using System.Collections.Generic;
using UnityEngine;

public class PatrolAIState : AIState
{
    public class PatrolAIStatePackage : AIStatePackage
    {
        public List<Vector3> PatrolPoints;
        public float PatrolSpeed;
        public bool Patrol;
    }

    public PatrolAIState(AIStatePackage package) : base(package)
    {
        var concretePackage = package as PatrolAIStatePackage;
        AIStateType = AIStateType.Patrol;

        if (concretePackage.Patrol)
            m_patrolPoints = new List<Vector3>(concretePackage.PatrolPoints);

        m_patrol = concretePackage.Patrol;
        m_patrolSpeed = concretePackage.PatrolSpeed;
        m_movement = concretePackage.Controller.GetComponentInRoot<ObjectMovement2DController>();
    }

    private readonly List<Vector3> m_patrolPoints;
    private readonly float m_patrolSpeed;
    private readonly ObjectMovement2DController m_movement;
    private readonly bool m_patrol;
    private int m_currentPointIndex;

    public override void Update(ObjectGenericAIController controller)
    {
        if (!m_patrol)
            return;

        if (!m_movement.MoveOnXTo(m_patrolSpeed))
        {
            
        }
        else
        {
            m_currentPointIndex++;

            if (m_currentPointIndex >= m_patrolPoints.Count)
                m_currentPointIndex = 0;

            m_movement.PrepareToMoveOnXTo(m_patrolPoints[m_currentPointIndex].x);
        }
    }

    public override void OnStateSet()
    {
        if (!m_patrol)
            return;

        m_movement.PrepareToMoveOnXTo(m_patrolPoints[m_currentPointIndex].x);
    }

    public override void OnStateChanged() { }
}