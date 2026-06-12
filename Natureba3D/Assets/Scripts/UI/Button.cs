using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class Button : MonoBehaviour
{
    public void OnMouseEnter()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    public void OnMouseExit()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
}
