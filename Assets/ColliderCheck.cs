using UnityEngine;
using UnityEngine.Events;

public class ColliderCheck : MonoBehaviour
{
    public GameObject checkedObject;
    public LayerMask layer;
    public UnityEvent OnEnterEvent;
    public UnityEvent OnExitEvent;
    public bool IsCheck => checkedObject != null;
   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & layer) != 0)
        {
            checkedObject = other.gameObject; 
            OnEnterEvent.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & layer) != 0)
        {
            checkedObject = null;
            OnExitEvent.Invoke();
        }
    }
}