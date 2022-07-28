using UnityEngine;

public class OnDistanceLostAIStateSwitch : AIStateSwitch
{
    public struct DistanceData
    {
        public Vector2 Target;
        public Vector2 Position;
    }

    public float MaximumDistance;

    private DistanceData m_currentData;

    public override void SetEvaluationData(object data)
    {
        m_currentData = (DistanceData)data;
    }

    protected override void Evaluate()
    {
        m_conditionValue = Mathf.Abs(m_currentData.Position.x - m_currentData.Target.x) >= MaximumDistance;
    }
}