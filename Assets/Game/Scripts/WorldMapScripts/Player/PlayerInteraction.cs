using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance { get; private set; }

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

    }

    private void GameInput_OnInteract(object sender, System.EventArgs e)
    {
        Vector3 centerScreen = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        Ray ray = Camera.main.ScreenPointToRay(centerScreen);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits)
        {
            IInteract interacableObject = hit.transform.GetComponentInParent<IInteract>();
            if (!hit.transform.tag.Equals("Player") && interacableObject != null)
            {
                if (interacableObject.CanInteract)
                {
                    hit.transform.GetComponentInParent<IInteract>().Interact();
                }
            }
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
