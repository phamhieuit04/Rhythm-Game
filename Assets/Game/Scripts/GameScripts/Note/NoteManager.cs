using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance { get; private set; }

    [SerializeField] private GameObject tapNotePrefab;
    [SerializeField] private GameObject holdNotePrefab;

    private List<GameObject> tapNotePool = new List<GameObject>();
    private List<GameObject> holdNotePool = new List<GameObject>();

    private Vector3 dLaneSpawnPosition = new Vector3(-1.5f, 0, 16);
    private Vector3 fLaneSpawnPosition = new Vector3(-0.5f, 0, 16);
    private Vector3 jLaneSpawnPosition = new Vector3(0.5f, 0, 16);
    private Vector3 kLaneSpawnPosition = new Vector3(1.5f, 0, 16);

    private List<Beat> beats;

    [SerializeField] private float noteSpeed = 3f;

    private double travelTime;
    private int nextNoteIndex = 0;
    private Vector3 laneSpawn;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject note = Instantiate(tapNotePrefab, transform);
            note.SetActive(false);
            tapNotePool.Add(note);
        }
        for (int i = 0; i < 5; i++)
        {
            GameObject note = Instantiate(holdNotePrefab, transform);
            note.SetActive(false);
            holdNotePool.Add(note);
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
            Beat beat = beats[nextNoteIndex];
            switch (beat.lane)
            {
                case 1:
                    laneSpawn = dLaneSpawnPosition;
                    break;
                case 2:
                    laneSpawn = fLaneSpawnPosition;
                    break;
                case 3:
                    laneSpawn = jLaneSpawnPosition;
                    break;
                case 4:
                    laneSpawn = kLaneSpawnPosition;
                    break;
            }
            if(beat.type == "hold")
            {
                Debug.Log("Lane: " + beat.lane + " " + beat.duration);
                double duration = beat.duration > 0.25f ? beat.duration - 0.2f : beat.duration;
                GetHoldNotePool().GetComponent<HoldNote>().SpawnNote(laneSpawn, GameManager.Instance.GetSongStartDsp() + beat.time, noteSpeed, duration);
            }
            else
            {
                GetTapNotePool().GetComponent<TapNote>().SpawnNote(laneSpawn, GameManager.Instance.GetSongStartDsp() + beat.time, noteSpeed);
            }
            nextNoteIndex++;
        }
    }

    public GameObject GetHoldNotePool()
    {
        for (int i = 0; i < holdNotePool.Count; i++)
        {
            if (!holdNotePool[i].activeInHierarchy)
            {
                return holdNotePool[i];
            }
        }
        GameObject newNote = Instantiate(holdNotePrefab, transform);
        newNote.SetActive(false);
        holdNotePool.Add(newNote);
        return newNote;
    }

    public GameObject GetTapNotePool()
    {
        for (int i = 0; i < tapNotePool.Count; i++)
        {
            if (!tapNotePool[i].activeInHierarchy)
            {
                return tapNotePool[i];
            }
        }
        GameObject newNote = Instantiate(tapNotePrefab, transform);
        newNote.SetActive(false);
        tapNotePool.Add(newNote);
        return newNote;
    }


    public void SetNoteSpeed(float speed)
    {
        noteSpeed = speed;
    }
}


