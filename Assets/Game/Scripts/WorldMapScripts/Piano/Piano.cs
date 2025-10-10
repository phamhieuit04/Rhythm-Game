using UnityEngine;
using UnityEngine.SceneManagement;

public class Piano : MonoBehaviour, IInteract
{
    [SerializeField] private Transform pianoVisual;
    [SerializeField] private Transform pianoOutline;

    public bool CanInteract { get; set; }

    private void Start()
    {

    }

    public void Interact()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ChangeVisual(bool isNear)
    {
        pianoVisual.gameObject.SetActive(!isNear);
        pianoOutline.gameObject.SetActive(isNear);
    }
}
