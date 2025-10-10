using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private float noteSpeed = 5f;

    public Vector3 spawnPos;
    public Vector3 targetPos;
    public double spawnDspTime;
    public double hitDspTime;
    public double dsp;

    void Update()
    {
        //dsp = AudioSettings.dspTime;

        //if (dsp < spawnDspTime)
        //    return;

        //float t = Mathf.InverseLerp((float)spawnDspTime, (float)hitDspTime, (float)dsp);

        //transform.position = Vector3.Lerp(spawnPos, targetPos, t);

        transform.position += Vector3.back * noteSpeed * Time.deltaTime;

        if (transform.position.z < -8)
        {
            DestroyNote();
            InGameUI.Instance.SetInGameText("Miss");
        }
    }


    public void SpawnNote(Vector3 lanePos, double hitTime, float speed)
    {
        spawnPos = lanePos;
        targetPos = new Vector3(spawnPos.x, spawnPos.y, -7.5f);
        hitDspTime = hitTime;
        noteSpeed = speed;

        float distance = Vector3.Distance(spawnPos, targetPos);
        double travelTime = distance / noteSpeed;
        spawnDspTime = hitDspTime - travelTime;

        transform.position = lanePos;
        gameObject.SetActive(true);
    }


    public void DestroyNote()
    {
        float distanceFromKey = transform.position.z - -7.5f;
        if (transform.position.z <= -7.5f)
        {
            InGameUI.Instance.SetInGameText("Bed");
        }
        else if (distanceFromKey < 0.35f)
        {
            InGameUI.Instance.SetInGameText("Perfect");
        }
        else
        {
            InGameUI.Instance.SetInGameText("Good");
        }
        gameObject.SetActive(false);
    }
}
