using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private bool randomNote = false;

    [SerializeField] private AudioSource audioSource;

    private List<GameObject> notePool = new List<GameObject>();
    private float noteReleaseTimer = 0f;

    private Vector3 dLaneSpawnPosition = new Vector3(-1.5f, 0, 16);
    private Vector3 fLaneSpawnPosition = new Vector3(-0.5f, 0, 16);
    private Vector3 jLaneSpawnPosition = new Vector3(0.5f, 0, 16);
    private Vector3 kLaneSpawnPosition = new Vector3(1.5f, 0, 16);

    private RootNote beatRoot;

    private void Start()
    {
        audioSource.PlayScheduled(AudioSettings.dspTime + 5);
        for (int i = 0; i < 40; i++)
        {
            GameObject note = Instantiate(notePrefab, transform);
            note.SetActive(false);
            notePool.Add(note);
        }



        TextAsset jsonFile = Resources.Load<TextAsset>("Ttls");
        Debug.Log(jsonFile.text);
        beatRoot = JsonUtility.FromJson<RootNote>(jsonFile.text);
        foreach (Beat beat in beatRoot.beats)
        {
            Debug.Log(beat.time);
        }

    }

    private void Update()
    {
        if (randomNote)
        {
            noteReleaseTimer -= Time.deltaTime;
            if (noteReleaseTimer < 0)
            {
                RandomNotes();
                noteReleaseTimer = 0.5f;
            }
        }
    }

    private void RandomNotes()
    {
        int lane = Random.Range(1, 5);
        switch (lane)
        {
            case 1:
                SetupNote(GetNotePool(), dLaneSpawnPosition);
                break;
            case 2:
                SetupNote(GetNotePool(), fLaneSpawnPosition);
                break;
            case 3:
                SetupNote(GetNotePool(), jLaneSpawnPosition);
                break;
            case 4:
                SetupNote(GetNotePool(), kLaneSpawnPosition);
                break;
        }
    }

    public void SetupNote(GameObject note, Vector3 lane)
    {
        note.SetActive(true);
        note.transform.position = lane;
    }

    public GameObject GetNotePool()
    {
        for (int i = 0; i < notePool.Count; i++)
        {
            if (!notePool[i].activeInHierarchy)
            {
                return notePool[i];
            }
        }
        GameObject newNote = Instantiate(notePrefab, transform);
        newNote.SetActive(false);
        notePool.Add(newNote);
        return newNote;
    }
}

[System.Serializable]
public class Beat
{
    public double time;
    public int lane;
    public string type;
    public double energy;
}
[System.Serializable]
public class RootNote
{
    public string difficulty;
    public List<Beat> beats;
}
