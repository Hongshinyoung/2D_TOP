using UnityEngine;

public class RotationWeapon : MonoBehaviour
{
    public float damage;

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

    private void Start()
    {
        
    }

    private void Init()
    {
        Item_Data data = GoogleSheetLoader.Instance.ItemDataList.Find(m => m.ItemID == itemID);
        if(data != null)
        {

        }
    }

    private void Update()
    {
        
    }
}
