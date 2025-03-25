using UnityEngine;

public class wavecontroler : MonoBehaviour
{
    public WaveDataAssigment waveData;

    private void Start()
    {
        Debug.Log(waveData.enemyCount);
        //for (int i = 0; i < waveData.enemyCount; i++)
        //{
        //    Instantiate(waveData.enemyPrefab);
        //}
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(2) ) {
            Instantiate(waveData.enemyPrefab);
            Instantiate(waveData.enemyPrefab);
            Instantiate(waveData.enemyPrefab);
            Instantiate(waveData.enemyPrefab);
        }
    }
}
