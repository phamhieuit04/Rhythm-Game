using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class NoteManager : MonoBehaviour
{

    [Header("Debug File")]
    [SerializeField] private TextAsset jsonFile;
    [SerializeField] private AudioResource audioResource;



    [Header("Property")]
    [SerializeField] private GameObject notePrefab;

    [SerializeField] private AudioSource audioSource;




    private List<GameObject> notePool = new List<GameObject>();

    private Vector3 dLaneSpawnPosition = new Vector3(-1.5f, 0, 16);
    private Vector3 fLaneSpawnPosition = new Vector3(-0.5f, 0, 16);
    private Vector3 jLaneSpawnPosition = new Vector3(0.5f, 0, 16);
    private Vector3 kLaneSpawnPosition = new Vector3(1.5f, 0, 16);

    private RootNote beatRoot;
    private List<Beat> beats;


    [SerializeField] private float noteSpeed = 3f;
    [SerializeField] private double audioStartDelay = 10f;

    private double songStartDspTime;
    private double travelTime;
    private int nextNoteIndex = 0;




    private void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject note = Instantiate(notePrefab, transform);
            note.SetActive(false);
            notePool.Add(note);
        }



        //TextAsset jsonFile = Resources.Load<TextAsset>("TtlsH");
        Debug.Log(jsonFile.text);
        beatRoot = JsonUtility.FromJson<RootNote>(jsonFile.text);
        beats = beatRoot.beats;



        float distance = Vector3.Distance(dLaneSpawnPosition, new Vector3(dLaneSpawnPosition.x, dLaneSpawnPosition.y, -7.5f));
        travelTime = distance / noteSpeed;

        songStartDspTime = AudioSettings.dspTime + audioStartDelay;
        audioSource.resource = audioResource;
        audioSource.PlayScheduled(songStartDspTime);
    }

    private void Update()
    {
        double songPosition = AudioSettings.dspTime - songStartDspTime;

        if (nextNoteIndex < beats.Count &&
            songPosition >= beats[nextNoteIndex].time - travelTime)
        {
            switch (beats[nextNoteIndex].lane)
            {
                case 1:
                    GetNotePool().GetComponent<Note>().SpawnNote(dLaneSpawnPosition, songStartDspTime + beats[nextNoteIndex].time, noteSpeed);
                    break;
                case 2:
                    GetNotePool().GetComponent<Note>().SpawnNote(fLaneSpawnPosition, songStartDspTime + beats[nextNoteIndex].time, noteSpeed);
                    break;
                case 3:
                    GetNotePool().GetComponent<Note>().SpawnNote(jLaneSpawnPosition, songStartDspTime + beats[nextNoteIndex].time, noteSpeed);
                    break;
                case 4:
                    GetNotePool().GetComponent<Note>().SpawnNote(kLaneSpawnPosition, songStartDspTime + beats[nextNoteIndex].time, noteSpeed);
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
