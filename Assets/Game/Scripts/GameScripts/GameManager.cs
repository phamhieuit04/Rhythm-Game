using System;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Debug File")]
    [SerializeField] private TextAsset jsonFile;
    [SerializeField] private AudioResource audioResource;

    private bool isPlaying = false;

    public event EventHandler<OnGameStartEventArgs> OnGameStart;
    public class OnGameStartEventArgs : EventArgs
    {
        public TextAsset json;
        public AudioResource audio;
    }


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnGameStart?.Invoke(this, new OnGameStartEventArgs
        {
            json = jsonFile,
            audio = audioResource
        });
        isPlaying = true;
    }


    public bool GetIsPlaying()
    {
        return isPlaying;
    }
}
