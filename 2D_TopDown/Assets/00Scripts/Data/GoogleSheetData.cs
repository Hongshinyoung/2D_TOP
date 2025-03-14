using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GoogleSheetData
{
    public List<Monster_Data> MonsterData;
    public List<Item_Data> ItemData;
}

[System.Serializable]
public class Monster_Data
{
    public string MonsterID;
    public string Name;
    public string Description;
    public int Attack;
    public float AttackMul;
    public int MaxHP;
    public float MaxHPMul;
    public int AttackRange;
    public float AttackRangeMul;
    public float AttackSpeed;
    public float MoveSpeed;
    public int MinExp;
    public int MaxExp;
    public string DropItem;
}

[System.Serializable]
public class Item_Data
{
    public int ItemID;
    public string Name;
    public string Description;
    public int UnlockLev;
    public int MaxHP;
    public float MaxHPMul;
    public int MaxMp;
    public float MaxMpMul;
    public int MaxAtk;
    public float MaxAtkMul;
    public int MaxDef;
    public float MaxDefMul;
    public int Status;
}

