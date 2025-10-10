using UnityEngine;

public class Piano : MonoBehaviour, IInteract
{
    [SerializeField] private Transform pianoVisual;

    public bool CanInteract { get; set; }

    public void Interact()
    {
        Debug.Log(gameObject.name);
    }

    public void ChangeVisual(bool isNear)
    {
        Renderer renderer = pianoVisual.GetComponent<Renderer>();
        renderer.material.color = isNear ? Color.white : Color.black;
    }
}
