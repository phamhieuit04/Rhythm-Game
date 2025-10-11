using UnityEngine;
using UnityEngine.SceneManagement;

public class Piano : MonoBehaviour, IInteract
{
    public bool CanInteract { get; set; }

    public void Interact()
    {
        SceneManager.LoadScene("GameScene");
    }
}
