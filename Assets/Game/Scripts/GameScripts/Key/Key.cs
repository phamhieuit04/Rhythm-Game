using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyManager.KeyNote keyNote;
    Vector3 startPoint;
    Vector3 endPoint;

    private void Start()
    {
        startPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.5f);
        endPoint = transform.position;
        KeyInput.Instance.OnNotePerform += KeyInput_OnNotePerform;
        KeyInput.Instance.OnNoteCancel += KeyInput_OnNoteCancel;
    }


    private void Update()
    {
        Debug.DrawLine(startPoint, new Vector3(startPoint.x, startPoint.y, startPoint.z + KeyManager.Instance.GetKeyCheckDistance()), Color.red);
    }

    private void KeyInput_OnNotePerform(object sender, KeyInput.NoteEventArgs e)
    {
        if (e.key == keyNote)
        {
            CheckNotePerform();
        }
    }

    private void KeyInput_OnNoteCancel(object sender, KeyInput.NoteEventArgs e)
    {
        if(e.key == keyNote)
        {
            CheckNoteCancel();
        }
    }

    private void CheckNotePerform()
    {
        if (Physics.Raycast(startPoint, (endPoint - startPoint).normalized, out RaycastHit hit, KeyManager.Instance.GetKeyCheckDistance()))
        {
            // On Key Perform
            if (hit.transform.GetComponentInParent<TapNote>())
            {
                TapNote note = hit.transform.GetComponentInParent<TapNote>();
                note.DestroyNote();
            } else if (hit.transform.GetComponentInParent<HoldNote>())
            {
                HoldNote note = hit.transform.GetComponentInParent<HoldNote>();
                note.DestroyNote(true);
            }

        }
    }

    private void CheckNoteCancel()
    {
        if (Physics.Raycast(startPoint, (endPoint - startPoint).normalized, out RaycastHit hit, 23.5f))
        {
            // On Key Cancel
            if (hit.transform.GetComponentInParent<HoldNote>())
            {
                Debug.Log("Hold Note Release");
                HoldNote note = hit.transform.GetComponentInParent<HoldNote>();
                note.DestroyNote(false);
            }
        }
    }

    public KeyManager.KeyNote GetKeyNote()
    {
        return keyNote;
    }

}
