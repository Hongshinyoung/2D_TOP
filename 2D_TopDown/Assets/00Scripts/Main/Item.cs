using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private int itemID;
    private Item_Data itemData;

    public void Interact(GameObject interactor)
    {
        if(interactor.CompareTag("Player"))
        {
            ApplyEffect(interactor);
            Destroy(gameObject);
        }
    }

    private void ApplyEffect(GameObject player)
    {
        Player_Controller playerController = player.GetComponent<Player_Controller>();
        if (playerController != null)
        {
            playerController.ApplyItemEffect(itemData);
        }
    }

    private void Awake()
    {
        LoadMonsterData();
    }

    private void LoadMonsterData()
    {
        if (GoogleSheetLoader.Instance == null || GoogleSheetLoader.Instance.ItemDataList == null)
        {
            Debug.Log("데이터 로드실패");
            return;
        }

        itemData = GoogleSheetLoader.Instance.ItemDataList.Find(m => m.ItemID == itemID);
        if (itemData == null)
        {
            Debug.Log("아이템 데이터 없음");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        else
        {
            Interact(collision.gameObject);
        }
    }
}
