using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour, IManager
{
    private GameManager _parent;
    private EnemyManager _enemyManager;
    private GridManager _gridManager;

    [SerializeField] private TextAsset jsonStageDataFile;
    private StageData _stageData;
    private Coroutine _stageCoroutine;
    private Coroutine _timerCoroutine;
    private float _timer = 0;

    private int _currentWaveIndex = 0;

    public void Initialize(GameManager parent)
    {
        _parent = parent;
        _enemyManager = _parent.GetEnemyManager();
        _gridManager = _parent.GetGridManager();
        LoadStageData();
    }

    public void LoadStageData(string jsonFileName)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);
        if (jsonFile == null)
        {
            Debug.LogError("Stage data file not found");
            return;
        }

        _stageData = JsonUtility.FromJson<StageData>(jsonFile.text);

        if (_stageData == null)
        {
            Debug.LogError("Stage data could not be loaded");
            return;
        }

        Debug.Log("Stage data loaded successfully");
    }

    public void LoadStageData()
    {
        if (jsonStageDataFile == null)
        {
            Debug.LogError("No stage data file assigned");
            return;
        }

        _stageData = JsonUtility.FromJson<StageData>(jsonStageDataFile.text);

        if (_stageData == null)
        {
            Debug.LogError("Stage data could not be loaded");
            return;
        }

        Debug.Log("Stage data loaded successfully");
        //print out the whole stage data
    }

    public void StartStage()
    {
        if (_stageData == null)
        {
            Debug.LogError("No stage data loaded");
            return;
        }

        _stageCoroutine = StartCoroutine(StageCoroutine());
        _timerCoroutine = StartCoroutine(TimerCoroutine());
    }
    private IEnumerator TimerCoroutine()
    {
        while (true)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator StageCoroutine()
    {
        if (_stageData.waveDataList == null)
        {
            Debug.LogError("Stage data has no waveDataList");
            yield break;
        }
        while (_currentWaveIndex < _stageData.waveDataList.Count)
        {
            WaveData currentWave = _stageData.waveDataList[_currentWaveIndex];
            if (_timer >= currentWave.waveStartTimestamp)
            {
                Debug.Log("Spawning wave " + _currentWaveIndex);
                yield return StartCoroutine(SpawnWave(currentWave));
                _currentWaveIndex++;
            }
            yield return null;
        }
    }


private IEnumerator SpawnWave(WaveData waveData)
{
    // For each spawn event in the current wave...
    for (int i = 0; i < waveData.spawnDataList.Count; i++)
    {
        SpawnData spawnData = waveData.spawnDataList[i];
        // Wait until the current time since the wave started reaches the spawn timestamp.
        while ((_timer - waveData.waveStartTimestamp) < spawnData.spawnTimestamp)
        {
            yield return null; // Wait for the next frame.
        }
        Debug.Log("Spawning enemy " + spawnData.enemyType + " at lane " + spawnData.lane);
        _enemyManager.SpawnEnemyAtLane(spawnData.enemyType, spawnData.lane);
    }
}


    public void Dispose()
    {
        StopCoroutine(_stageCoroutine);
        StopCoroutine(_timerCoroutine);

        _stageData = null;
        _stageCoroutine = null;
        _timerCoroutine = null;
        _currentWaveIndex = 0;
        _timer = 0;

    }

}
