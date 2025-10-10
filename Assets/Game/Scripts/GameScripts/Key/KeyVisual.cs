using System.Collections;
using UnityEngine;

public class KeyVisual : MonoBehaviour
{
    private KeyManager.KeyNote key;
    private void Start()
    {
        key = GetComponentInParent<Key>().GetKeyNote();

        KeyInput.Instance.OnNotePerform += Instance_OnNotePerform;
        KeyInput.Instance.OnNoteCancel += Instance_OnNoteCancel;

        gameObject.SetActive(false);
    }
    private void Instance_OnNotePerform(object sender, KeyInput.NoteEventArgs e)
    {
        if (e.key == key)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
            gameObject.SetActive(true);
            StartCoroutine(SetNotePerform());
        }
    }

    private IEnumerator SetNotePerform()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }

    private void Instance_OnNoteCancel(object sender, KeyInput.NoteEventArgs e)
    {
        if (e.key == key)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
        }
    }
}
