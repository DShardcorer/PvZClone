using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StageDataExporter : MonoBehaviour
{
    // This function creates an instance of StageData with two waves and writes it out as JSON.
    [ContextMenu("Export Stage Data JSON")]
    public void Export()
    {
        // Create StageData instance.
        StageData stageData = new StageData();
        stageData.waveDataList = new List<WaveData>();

        // Create the first wave.
        WaveData wave1 = new WaveData();
        wave1.waveStartTimestamp = 0.0f;
        wave1.spawnDataList = new List<SpawnData>
        {
            new SpawnData { spawnTimestamp = 0.5f, lane = 0, enemyType = NameHelper.ZombieBasic },
            new SpawnData { spawnTimestamp = 1.2f, lane = 2, enemyType = NameHelper.ZombieCone },
            new SpawnData { spawnTimestamp = 2.0f, lane = 1, enemyType = NameHelper.ZombieBasic }
        };
        stageData.waveDataList.Add(wave1);

        // Create the second wave.
        WaveData wave2 = new WaveData();
        wave2.waveStartTimestamp = 10.0f;
        wave2.spawnDataList = new List<SpawnData>
        {
            new SpawnData { spawnTimestamp = 0.2f, lane = 1, enemyType = NameHelper.ZombieBasic },
            new SpawnData { spawnTimestamp = 1.0f, lane = 3, enemyType = NameHelper.ZombieCone },
            new SpawnData { spawnTimestamp = 1.8f, lane = 0, enemyType = NameHelper.ZombieBasic }
        };
        stageData.waveDataList.Add(wave2);

        // Convert the stageData object to a formatted JSON string.
        string json = JsonUtility.ToJson(stageData, true);
        
        // Determine the output path (here, we save it into the Resources folder).
        string path = Application.dataPath + "/Resources/StageDataExport.json";
        File.WriteAllText(path, json);
        
        Debug.Log("Stage data exported to: " + path);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightShift))
        {
            Export();
        }
    }
}