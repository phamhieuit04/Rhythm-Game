using UnityEngine;

public class HoldNote : MonoBehaviour
{
    [SerializeField] private Transform startNote;
    [SerializeField] private Transform endNote;
    private LineRenderer lineRenderer;

    [SerializeField] private float noteSpeed = 5f;
    public Vector3 spawnPos;
    public Vector3 targetPos;
    public double spawnDspTime;
    public double hitDspTime;
    public double dsp;
    private bool isHolding = false;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        SetupLine(startNote, endNote);
        transform.position += Vector3.back * noteSpeed * Time.deltaTime;
        if (isHolding)
        {
            startNote.localPosition += Vector3.forward * noteSpeed * Time.deltaTime;
        }

        if (endNote.position.z < -8.5f)
        {
            InGameUI.Instance.SetInGameText("Miss");
            gameObject.SetActive(false);
        }
    }

    private void SetupLine(Transform start, Transform end)
    {
        lineRenderer.SetPosition(0, start.localPosition);
        lineRenderer.SetPosition(1, end.localPosition);
    }


    public void SpawnNote(Vector3 lanePos, double hitTime, float speed, double duration)
    {
        spawnPos = lanePos;
        targetPos = new Vector3(spawnPos.x, spawnPos.y, -7.5f);
        hitDspTime = hitTime;
        noteSpeed = speed;

        float distance = Vector3.Distance(spawnPos, targetPos);
        double travelTime = distance / noteSpeed;
        spawnDspTime = hitDspTime - travelTime;

        startNote.transform.localPosition = Vector3.zero;
        startNote.transform.position = lanePos;

        Debug.Log(duration);
        Vector3 endNotePosition = new Vector3(startNote.transform.localPosition.x, 0.01f, startNote.transform.localPosition.z + ((float)duration * speed));
        endNote.transform.localPosition = endNotePosition;

        isHolding = false;
        gameObject.SetActive(true);
        foreach(Transform chil in transform)
        {
            chil.gameObject.SetActive(true);
        }
    }

    public void DestroyNote(bool isStartNote)
    {
        if(isStartNote)
        {
            GameManager.Instance.SetJudment(startNote.position);
            isHolding = true;
            startNote.gameObject.SetActive(false);
        }
        else
        {
            if (isHolding == false) return;
            GameManager.Instance.SetJudment(endNote.position);
            isHolding = false;
            gameObject.SetActive(false);
        }
    }
}
