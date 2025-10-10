using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyManager.KeyNote keyNote;

    private void Start()
    {
        KeyInput.Instance.OnNotePerform += KeyInput_OnNotePerform;
    }

    private void Update()
    {
        Debug.DrawLine(transform.position - transform.forward * 1.5f, transform.forward * KeyManager.Instance.GetKeyCheckDistance());
    }

    private void KeyInput_OnNotePerform(object sender, KeyInput.NoteEventArgs e)
    {
        CheckNote();
    }

    private void CheckNote()
    {
        if (Physics.Raycast(transform.position - transform.forward * 1.5f, transform.forward, out RaycastHit hit, KeyManager.Instance.GetKeyCheckDistance()))
        {
            if (hit.transform.GetComponentInParent<Note>())
            {
                Note note = hit.transform.GetComponentInParent<Note>();
                note.DestroyNote();
            }
        }
    }

    public KeyManager.KeyNote GetKeyNote()
    {
        return keyNote;
    }

}
