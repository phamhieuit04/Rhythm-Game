using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private float noteSpeed = 1f;
    void Update()
    {
        transform.Translate(Vector3.back * noteSpeed * Time.deltaTime);
        if (transform.position.z < -30) DestroyNote();
    }
    public void DestroyNote()
    {
        Destroy(this.gameObject);
    }
}
