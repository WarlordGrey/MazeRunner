using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsController : MonoBehaviour
{

    [SerializeField]
    private Text levelText = null;
    [SerializeField]
    private Text sizeText = null;
    [SerializeField]
    private Text entrancesText = null;
    [SerializeField]
    private Text trapsText = null;
    [SerializeField]
    private Text timerText = null;
    [SerializeField]
    private Text distanceText = null;

    private GameObject player = null;
    private Vector3 lastPlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        LevelController.Instance.Timer = 0;
        LevelController.Instance.Distance = 0;
        levelText.text = string.Format("Level: {0}", PlayerData.Instance.currentLevel);
        sizeText.text = string.Format("Size: {0} x {1}", LevelController.Instance.LevelWidth, LevelController.Instance.LevelLength);
        entrancesText.text = string.Format("Entrances: {0}", LevelController.Instance.EntrancesCount);
        trapsText.text = string.Format("Traps: {0}", LevelController.Instance.TrapsCount);
        StartCoroutine(FindPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        LevelController.Instance.Timer += Time.deltaTime;
        if(player != null)
        {
            LevelController.Instance.Distance += GetMovementDelta(lastPlayerPos, player.transform.position);
            lastPlayerPos = player.transform.position;
        }
        UpdateTexts();
    }

    private float GetMovementDelta(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 result = endPoint - startPoint;
        return Mathf.Sqrt(result.x * result.x + result.y * result.y + result.z * result.z);
    }

    private IEnumerator FindPlayer()
    {
        YieldInstruction endFrame = new WaitForEndOfFrame();
        while(player == null)
        {
            yield return endFrame;
            player = GameObject.FindGameObjectWithTag("Player");
        }
        lastPlayerPos = player.transform.position;
    }

    private void UpdateTexts()
    {
        timerText.text = string.Format("Time: {0}s", Mathf.RoundToInt(LevelController.Instance.Timer));
        distanceText.text = string.Format("Distance: {0}", Mathf.RoundToInt(LevelController.Instance.Distance));
    }

}
