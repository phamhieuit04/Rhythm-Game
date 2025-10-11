using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance { get; private set; }

    [SerializeField] private GameObject interactUI;

    private IInteract interactableObject;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        interactUI.SetActive(false);

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
        interactUI.SetActive(display);
    }

    private void GameInput_OnInteract(object sender, System.EventArgs e)
    {
        if (interactableObject != null)
        {
            interactableObject.Interact();
        }
    }

    public void SetVisual(Collider collision, bool isNear)
    {
        IInteract interacableObject = collision.transform.GetComponentInParent<IInteract>();
        if (interacableObject != null)
        {
            interacableObject.ChangeVisual(isNear);
            interacableObject.CanInteract = isNear;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        SetVisual(collision, true);
    }

    private void OnTriggerExit(Collider collision)
    {
        SetVisual(collision, false);
    }
}
