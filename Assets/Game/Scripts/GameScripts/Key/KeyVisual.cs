using UnityEngine;

public class KeyVisual : MonoBehaviour
{
    [SerializeField] private KeyManager.KeyNote key;
    private void Start()
    {
        key = GetComponentInParent<Key>().GetKeyNote();

        KeyInput.Instance.OnNotePerform += Instance_OnNotePerform;
        KeyInput.Instance.OnNoteCancel += Instance_OnNoteCancel;
    }
    private void Instance_OnNotePerform(object sender, KeyInput.NoteEventArgs e)
    {
        if (e.key == key)
        {
            transform.position = new Vector3(transform.position.x, -0.05f, transform.position.z);
        }
    }

    private void Instance_OnNoteCancel(object sender, KeyInput.NoteEventArgs e)
    {
        if (e.key == key)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
}
