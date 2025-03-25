using UnityEngine;

[CreateAssetMenu(fileName = "WaveDataAssigment", menuName = "Scriptable Objects/WaveDataAssigment")]
public class WaveDataAssigment : ScriptableObject
{
    public GameObject enemyPrefab;
    public int enemyCount;
    public float enemySped;
    public float enemyhealth;
    public float enemyDamage;

}
