using UnityEngine;

public abstract class NestedComponent : MonoBehaviour
{
    protected T GetComponentFromRoot<T>()
    {
        var component = gameObject.transform.parent.GetComponent<T>();

        if (component != null) 
            return component;

        Debug.LogError($"Can't get {typeof(T)} component from {gameObject.transform.parent.name}!");
        return default;
    }
    
    protected T GetComponentInRoot<T>()
    {
        var component = gameObject.transform.parent.GetComponentInChildren<T>();

        if (component != null) 
            return component;

        Debug.LogError($"Can't find {typeof(T)} component in {gameObject.transform.parent.name}!");
        return default;
    }
}