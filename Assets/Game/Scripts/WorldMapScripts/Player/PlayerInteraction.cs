using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance { get; private set; }

    private IInteract interactableObject;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GameInput.Instance.OnInteract += GameInput_OnInteract;
    }

    void Update()
    {
        HandleInteracts();
    }

    public void HandleInteracts()
    {
        Vector3 centerScreen = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        Ray ray = Camera.main.ScreenPointToRay(centerScreen);

        RaycastHit[] hits = Physics.RaycastAll(ray);
        bool display = false;

        foreach (RaycastHit hit in hits)
        {
            IInteract interactableObject = hit.transform.GetComponentInParent<IInteract>();
            if (!hit.transform.tag.Equals("Player") && interactableObject != null && interactableObject.CanInteract)
            {
                this.interactableObject = interactableObject;
                display = true;
                break;
            }
        }

        InteractUI.Instance.SetDisplay(display);
    }

    private void GameInput_OnInteract(object sender, System.EventArgs e)
    {
        if (interactableObject != null)
        {
            interactableObject.Interact();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        SetInteractableObjectOutlineVisual(collision, true);
    }

    private void OnTriggerExit(Collider collision)
    {
        SetInteractableObjectOutlineVisual(collision, false);
    }

    private void SetInteractableObjectOutlineVisual(Collider collision, bool isNear)
    {
        IInteract interactableObject = collision.transform.GetComponentInParent<IInteract>();
        if (interactableObject != null)
        {
            interactableObject.CanInteract = isNear;

            OutlineVisual.Instance.SetVisualObject(collision.gameObject);
            OutlineVisual.Instance.SetVisual(isNear);
        }
    }
}
