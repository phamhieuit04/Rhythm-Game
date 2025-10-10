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
    }

    private void Update()
    {
        Debug.DrawLine(startPoint, new Vector3(startPoint.x, startPoint.y, startPoint.z + KeyManager.Instance.GetKeyCheckDistance()), Color.red);
    }

    private void KeyInput_OnNotePerform(object sender, KeyInput.NoteEventArgs e)
    {
        if (e.key == keyNote)
        {
            CheckNote();
        }
    }

    private void CheckNote()
    {
        if (Physics.Raycast(startPoint, (endPoint - startPoint).normalized, out RaycastHit hit, KeyManager.Instance.GetKeyCheckDistance()))
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
