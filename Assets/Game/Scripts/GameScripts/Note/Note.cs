using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private float noteSpeed = 2f;
    void Update()
    {
        transform.Translate(Vector3.back * noteSpeed * Time.deltaTime);
        if (transform.position.z < -8)
        {
            DestroyNote();
            InGameUI.Instance.SetInGameText("Miss");
        }
    }
    public void DestroyNote()
    {
        gameObject.SetActive(false);
    }
}
