using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyInput.KeyNote keyNote;

    private void Start()
    {
        KeyInput.Instance.OnNotePerform += KeyInput_OnNotePerform;
        KeyInput.Instance.OnNoteCancel += KeyInput_OnNoteCancel;
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
        float distanceFromNote = 10f;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.5f), transform.forward, out RaycastHit hit, 3f))
        {
            if (hit.transform.GetComponentInParent<Note>())
            {
                Note note = hit.transform.GetComponentInParent<Note>();
                distanceFromNote = Vector3.Distance(note.transform.localPosition, transform.position);
                note.DestroyNote();
            }
            if(hit.transform.position.z < transform.position.z)
            {
                Debug.Log("Bed");
            }
            else if(distanceFromNote < 0.35f)
            {
                Debug.Log("Perfect");
            } else
            {
                Debug.Log("Normal");
            }
        }
    }

}
