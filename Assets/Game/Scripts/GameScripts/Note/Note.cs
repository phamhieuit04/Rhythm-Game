using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private float noteSpeed = 2f;
    void Update()
    {
        transform.Translate(Vector3.back * noteSpeed * Time.deltaTime);
    }
    public void DestroyNote()
    {
        Destroy(this);
    }
}
