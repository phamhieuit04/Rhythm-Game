using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager Instance;

    [SerializeField] private float keyCheckDistance = 5f;

    public enum KeyNote
    {
        K, J, F, D,
    }

    private void Awake()
    {
        Instance = this;
    }


    public float GetKeyCheckDistance()
    {
        return keyCheckDistance;
    }
    public void SetKeyCheckDistance(float distance)
    {
        keyCheckDistance = distance;
    }
}
