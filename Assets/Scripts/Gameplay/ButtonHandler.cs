using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class ButtonHandler : MonoBehaviour
{
    public string Name;
    public void SetDownState() { }
    public void SetUpState() { }
    public void SetAxisPositiveState() { } 
    public void SetAxisNeutralState() { } 
    public void SetAxisNegativeState() { } 
}
