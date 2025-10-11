using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static AudioClip audioClip;
    public static RootNote rootNote;

    public static void LoadGameScene(AudioClip audio, RootNote root)
    {
        audioClip = audio;
        rootNote = root;
        SceneManager.LoadScene("GameScene");
    }
}
