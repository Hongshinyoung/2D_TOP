using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int itemID;
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private int unlockLev;
    [SerializeField] private int maxHP;
    [SerializeField] private float maxHPMul;
    [SerializeField] private int maxMp;
    [SerializeField] private float maxMpMul;
    [SerializeField] private int maxAtk;
    [SerializeField] private float maxAtkMul;
    [SerializeField] private int maxDef;
    [SerializeField] private float maxDefMul;
    [SerializeField] private int status;

    private void Awake()
    {
        LoadMonsterData();
    }

    private void LoadMonsterData()
    {
        if (GoogleSheetLoader.Instance == null || GoogleSheetLoader.Instance.ItemDataList == null)
        {
            Debug.Log("데이터 로드 실패.");
            return;
        }

        Item_Data data = GoogleSheetLoader.Instance.ItemDataList.Find(m => m.ItemID == itemID);
        if (data != null)
        {
            itemID = data.ItemID;
            name = data.Name;
            description = data.Description;
            unlockLev = data.UnlockLev;
            maxHP = data.MaxHP;
            maxHPMul = data.MaxHPMul;
            maxMp = data.MaxMp;
            maxMpMul = data.MaxMpMul;
            maxAtk = data.MaxAtk;
            maxAtkMul = data.MaxAtkMul;
            maxDef = data.MaxDef;
            maxDefMul = data.MaxDefMul;
            status = data.Status;
        }
        else
        {
            Debug.Log("데이터 없음");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        else
        {
            Destroy(gameObject);
        }
    }
}
