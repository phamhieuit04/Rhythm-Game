using UnityEngine;

public class NoteManager : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;

    private Vector3 dLaneSpawnPosition = new Vector3(-1.5f, 0, 16);
    private Vector3 fLaneSpawnPosition = new Vector3(-0.5f, 0, 16);
    private Vector3 jLaneSpawnPosition = new Vector3(0.5f, 0, 16);
    private Vector3 kLaneSpawnPosition = new Vector3(1.5f, 0, 16);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject dNote = Instantiate(notePrefab, transform);
            dNote.transform.position = dLaneSpawnPosition;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject fNote = Instantiate(notePrefab, transform);
            fNote.transform.position = fLaneSpawnPosition;
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            GameObject jNote = Instantiate(notePrefab, transform);
            jNote.transform.position = jLaneSpawnPosition;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            GameObject kNote = Instantiate(notePrefab, transform);
            kNote.transform.position = kLaneSpawnPosition;
        }
    }
}
