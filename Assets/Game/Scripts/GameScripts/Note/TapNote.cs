using UnityEngine;

public class TapNote : MonoBehaviour
{
    [SerializeField] private float noteSpeed = 5f;

    public Vector3 spawnPos;
    public Vector3 targetPos;
    public double spawnDspTime;
    public double hitDspTime;
    public double dsp;

    private bool isSlowDown = false;

    void Update()
    {
        transform.position += Vector3.back * noteSpeed * Time.deltaTime;

        if (transform.position.z < -8.5)
        {
            DestroyNote();
            InGameUI.Instance.SetInGameText("Miss");
        }
        if(transform.position.z < -7.45f && !isSlowDown)
        {
            noteSpeed = noteSpeed / 2;
            isSlowDown = true;
        }
    }




    public void SpawnNote(Vector3 lanePos, double hitTime, float speed)
    {
        spawnPos = lanePos;
        targetPos = new Vector3(spawnPos.x, spawnPos.y, -7.5f);
        hitDspTime = hitTime;
        noteSpeed = speed;
        isSlowDown = false;

        float distance = Vector3.Distance(spawnPos, targetPos);
        double travelTime = distance / noteSpeed;
        spawnDspTime = hitDspTime - travelTime;

        transform.position = lanePos;
        gameObject.SetActive(true);
    }


    public void DestroyNote()
    {
        GameManager.Instance.SetJudment(transform.position);
        gameObject.SetActive(false);
    }
}
