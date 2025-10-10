using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Debug File")]
    [SerializeField] private TextAsset jsonFile;
    [SerializeField] private AudioResource audioResource;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private double audioStartDelay = 10f;


    [Header("Note")]
    [SerializeField] private float hardNoteSpeed = 15f;
    [SerializeField] private float normalNoteSpeed = 10f;
    [SerializeField] private float easyNoteSpeed = 5f;

    [Header("Key")]
    [SerializeField] private float hardKeyDistance = 6f;
    [SerializeField] private float normalKeyDistance = 5f;
    [SerializeField] private float easyKeyDistance = 4f;

    private double songStartDspTime;
    private bool isPlaying = false;
    private RootNote beatRoot;

    public event EventHandler<OnGameStartEventArgs> OnGameStart;
    public class OnGameStartEventArgs : EventArgs
    {
        public List<Beat> beats;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        songStartDspTime = AudioSettings.dspTime + audioStartDelay;
        beatRoot = JsonUtility.FromJson<RootNote>(jsonFile.text);

        audioSource.resource = audioResource;
        audioSource.PlayScheduled(songStartDspTime);

        switch (beatRoot.difficulty)
        {
            case "hard":
                NoteManager.Instance.SetNoteSpeed(hardNoteSpeed);
                KeyManager.Instance.SetKeyCheckDistance(hardKeyDistance);
                break;
            case "normal":
                NoteManager.Instance.SetNoteSpeed(normalNoteSpeed);
                KeyManager.Instance.SetKeyCheckDistance(normalKeyDistance);
                break;
            case "easy":
                NoteManager.Instance.SetNoteSpeed(easyNoteSpeed);
                KeyManager.Instance.SetKeyCheckDistance(easyKeyDistance);
                break;
        }

        OnGameStart?.Invoke(this, new OnGameStartEventArgs
        {
            beats = beatRoot.beats,
        });
        isPlaying = true;
    }

    public double GetSongStartDsp()
    {
        return songStartDspTime;
    }

    public bool GetIsPlaying()
    {
        return isPlaying;
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
