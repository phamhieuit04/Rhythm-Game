using TMPro;
using UnityEngine;

public class RuntimeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private AudioSource audioSource;

    private void Update()
    {
        timeText.text = ((int)audioSource.time).ToString();
    }
}
