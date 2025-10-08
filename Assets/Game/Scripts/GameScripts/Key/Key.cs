using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyInput.KeyNote keyNote;

    private void Start()
    {
        KeyInput.Instance.OnNotePerform += KeyInput_OnNotePerform;
        KeyInput.Instance.OnNoteCancel += KeyInput_OnNoteCancel;
    }

    private void Update()
    {
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), new Vector3(transform.position.x, transform.position.y, transform.position.z + 2.5f), Color.red);
    }


    private void KeyInput_OnNoteCancel(object sender, KeyInput.NoteEventArgs e)
    {
        if (e.key == keyNote)
        {
            transform.position = new Vector3(transform.position.x, -0.25f, transform.position.z);
        }
    }

    private void KeyInput_OnNotePerform(object sender, KeyInput.NoteEventArgs e)
    {
        if (e.key == keyNote)
        {
            CheckNote();
            transform.position = new Vector3(transform.position.x, -0.3f, transform.position.z);
        }
    }

    private void CheckNote()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 3f))
        {
            if (hit.transform.GetComponentInParent<Note>())
            {
                Note note = hit.transform.GetComponentInParent<Note>();
                note.DestroyNote();
            }
        }
    }

}
