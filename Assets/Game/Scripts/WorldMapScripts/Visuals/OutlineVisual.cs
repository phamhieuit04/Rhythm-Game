using UnityEngine;

public class OutlineVisual : MonoBehaviour
{
    public static OutlineVisual Instance { get; private set; }

    private const int LAYER_DEFAULT = 0;
    private const int LAYER_OUTLINE = 3;

    private GameObject visualObject;

    private void Awake()
    {
        Instance = this;
    }

    public void SetVisualObject(GameObject gameObject)
    {
        visualObject = gameObject;
    }

    public void SetVisual(bool isNear)
    {
        visualObject.layer = isNear ? LAYER_OUTLINE : LAYER_DEFAULT;
    }
}
