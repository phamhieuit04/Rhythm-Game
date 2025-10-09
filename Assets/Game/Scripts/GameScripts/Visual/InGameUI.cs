using System.Collections;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public static InGameUI Instance;

    [SerializeField] private TextMeshProUGUI judmentText;
    [SerializeField] private TextMeshProUGUI comboText;

    int comboNumber = 0;

    private void Awake()
    {
        Instance = this;
    }


    public void SetInGameText(string text)
    {
        if (judmentText.text == "Perfect" && judmentText.text == text)
        {
            comboNumber++;
        }
        else
        {
            comboNumber = 1;
        }
        if (comboNumber > 1)
        {
            comboText.text = "x" + comboNumber.ToString();
        }
        else
        {
            comboText.text = "";
        }
        judmentText.text = text;
        StartCoroutine(OnChangeAnim());
    }

    public IEnumerator OnChangeAnim()
    {
        judmentText.fontSize = 0.45f;
        yield return new WaitForSeconds(0.05f);
        judmentText.fontSize = 0.4f;
    }
}
