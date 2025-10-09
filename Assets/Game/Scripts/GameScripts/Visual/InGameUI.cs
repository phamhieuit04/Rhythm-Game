using System.Collections;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public static InGameUI Instance;

    [SerializeField] private TextMeshProUGUI judmentText;

    private void Awake()
    {
        Instance = this;
    }


    public void SetInGameText(string text)
    {
        judmentText.text = text;
        StartCoroutine(OnChangeAnim());
    }
    
    public IEnumerator OnChangeAnim()
    {
        judmentText.fontSize = 0.55f;
        yield return new WaitForSeconds(0.05f);
        judmentText.fontSize = 0.5f;
    }
}
