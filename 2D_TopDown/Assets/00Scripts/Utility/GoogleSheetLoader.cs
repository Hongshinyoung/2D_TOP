using System.Collections.Generic;
using UnityEngine;

public class GoogleSheetLoader : MonoBehaviour
{
    private string filePath = "GenerateGoogleSheet/GoogleSheetJson";

    public static GoogleSheetLoader Instance { get; private set; }
    public List<Monster_Data> MonsterDataList { get; private set; }
    public List<Item_Data> ItemDataList { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGoogleSheetData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadGoogleSheetData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(filePath);
        if (jsonFile != null)
        {
            GoogleSheetData data = JsonUtility.FromJson<GoogleSheetData>(jsonFile.text);
            MonsterDataList = data.MonsterData;
            ItemDataList = data.ItemData;
        }
        else
        {
            Debug.LogError("파일을 없음.");
        }
    }
}
