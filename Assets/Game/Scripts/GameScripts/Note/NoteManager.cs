using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance { get; private set; }

    [Header("Property")]
    [SerializeField] private GameObject notePrefab;

    private List<GameObject> notePool = new List<GameObject>();

    private Vector3 dLaneSpawnPosition = new Vector3(-1.5f, 0, 16);
    private Vector3 fLaneSpawnPosition = new Vector3(-0.5f, 0, 16);
    private Vector3 jLaneSpawnPosition = new Vector3(0.5f, 0, 16);
    private Vector3 kLaneSpawnPosition = new Vector3(1.5f, 0, 16);

    private List<Beat> beats;

    [SerializeField] private float noteSpeed = 3f;

    private double travelTime;
    private int nextNoteIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject note = Instantiate(notePrefab, transform);
            note.SetActive(false);
            notePool.Add(note);
        }
        GameManager.Instance.OnGameStart += GameManager_OnGameStart;
    }

    private void GameManager_OnGameStart(object sender, GameManager.OnGameStartEventArgs e)
    {
        beats = e.beats;
        float distance = Vector3.Distance(dLaneSpawnPosition, new Vector3(dLaneSpawnPosition.x, dLaneSpawnPosition.y, -7.5f));
        travelTime = distance / noteSpeed;
    }

    private void Update()
    {
        if (GameManager.Instance.GetIsPlaying())
        {
            HandleNoteSpawn();
        }
    }

    private void HandleNoteSpawn()
    {
        double songPosition = AudioSettings.dspTime - GameManager.Instance.GetSongStartDsp();

        if (nextNoteIndex < beats.Count &&
            songPosition >= beats[nextNoteIndex].time - travelTime)
        {
            switch (beats[nextNoteIndex].lane)
            {
                case 1:
                    GetNotePool().GetComponent<Note>().SpawnNote(dLaneSpawnPosition, GameManager.Instance.GetSongStartDsp() + beats[nextNoteIndex].time, noteSpeed);
                    break;
                case 2:
                    GetNotePool().GetComponent<Note>().SpawnNote(fLaneSpawnPosition, GameManager.Instance.GetSongStartDsp() + beats[nextNoteIndex].time, noteSpeed);
                    break;
                case 3:
                    GetNotePool().GetComponent<Note>().SpawnNote(jLaneSpawnPosition, GameManager.Instance.GetSongStartDsp() + beats[nextNoteIndex].time, noteSpeed);
                    break;
                case 4:
                    GetNotePool().GetComponent<Note>().SpawnNote(kLaneSpawnPosition, GameManager.Instance.GetSongStartDsp() + beats[nextNoteIndex].time, noteSpeed);
                    break;
            }
            nextNoteIndex++;
        }
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

    public void SetNoteSpeed(float speed)
    {
        noteSpeed = speed;
    }
}


