using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractionHandler : NestedComponent
{
    private List<GameObject> m_interactableInRange;
    private GameObject m_closestInteractable;
    private GameObject m_myGameObject;

    private void Awake()
    {
        m_myGameObject = transform.parent.gameObject;
        m_interactableInRange = new List<GameObject>();
        enabled = false;
    }

    public void AddInteractable(GameObject interactable)
    {
        if (m_interactableInRange.Count == 0)
            enabled = true;
        
        m_interactableInRange.Add(interactable);
    }

    public void RemoveInteractable(GameObject interactable)
    {
        if (interactable == m_closestInteractable)
        {
            m_closestInteractable.GetComponentInChildren<IInteractable>().OnDeselect();
            m_closestInteractable = null;
            OnDeselectInternal();
        }
        
        m_interactableInRange.Remove(interactable);

        if (m_interactableInRange.Count == 0)
            enabled = false;
    }

    public void Interact()
    {
        if (m_closestInteractable == null)
            return;
        
        m_closestInteractable.GetComponentInChildren<IInteractable>().Interact(m_myGameObject);
    }

    private void Update()
    {
        UpdateClosestInteractable();
    }

    private void UpdateClosestInteractable()
    {
        if (m_interactableInRange.Count == 1)
        {
            if (m_closestInteractable == m_interactableInRange[0])
                return;

            m_closestInteractable = m_interactableInRange[0];
        }
        else
        {
            var closestIndex = 0;
            var closestDistanceX = Mathf.Abs(m_interactableInRange[closestIndex].transform.position.x - m_myGameObject.transform.position.x);
            m_closestInteractable = m_interactableInRange[closestIndex];

            for (var i = 1; i < m_interactableInRange.Count; i++)
            {
                var distanceX = Mathf.Abs(m_interactableInRange[i].transform.position.x - m_myGameObject.transform.position.x);

                if (distanceX >= closestDistanceX)
                    continue;

                closestIndex = i;
                closestDistanceX = distanceX;
            }

            if (m_closestInteractable == m_interactableInRange[closestIndex])
                return;

            m_closestInteractable.GetComponentInChildren<IInteractable>().OnDeselect();
            OnDeselectInternal();
            m_closestInteractable = m_interactableInRange[closestIndex];
        }

        m_closestInteractable.GetComponentInChildren<IInteractable>().OnSelect();
        OnSelectInternal();
    }

    protected virtual void OnSelectInternal() {}
    protected virtual void OnDeselectInternal() {}
}