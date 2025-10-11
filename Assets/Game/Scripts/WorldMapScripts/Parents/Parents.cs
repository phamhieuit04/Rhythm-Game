using UnityEngine;

public class Parents : MonoBehaviour, IInteract
{
    public bool CanInteract { get; set; }

    public void Interact()
    {
        Debug.Log("Oanh nhau!");
    }
}
