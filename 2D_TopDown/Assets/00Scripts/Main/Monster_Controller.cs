using System.Collections.Generic;
using UnityEngine;

public class Monster_Controller : MonoBehaviour
{
    [Header("MonsterData")]
    [SerializeField] private string monsterID;
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private int attack;
    [SerializeField] private float attackMul;
    [SerializeField] private int maxHP;
    [SerializeField] private float maxHPMul;
    [SerializeField] private int attackRange;
    [SerializeField] private float attackRangeMul;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int minExp;
    [SerializeField] private int maxExp;
    [SerializeField] private string dropItem;
    [SerializeField] private float moveSpeed;

    [SerializeField] private float currentHP;
    private string prefabName;
    public string PrefabName => prefabName;

    private Rigidbody2D target;
    private bool isDie;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        currentHP = maxHP;
        isDie = false;
    }

    private void Start()
    {
        LoadMonsterData();
        Init();
    }

    public void Init()
    {
        currentHP = maxHP;
    }

    public void SetPrefabName(string name)
    {
        prefabName = name;
    }

    private void FixedUpdate()
    {
        if (isDie || target == null) return;

        Vector2 direction = target.position - rb.position;
        Vector2 moveDirection = direction.normalized * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + moveDirection);
        rb.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (isDie || target == null) return;

        spriteRenderer.flipX = target.position.x < rb.position.x;
    }

    public void Die()
    {
        List<int> dropItemIDs = new List<int>();

        if (dropItem.Contains("/"))
        {
            string[] splitData = dropItem.Split('/');
            foreach(string value in splitData)
            {
                if(int.TryParse(value, out int parsedValue))
                {
                    dropItemIDs.Add(parsedValue);
                }
            }
        }
        else
        {
            if(int.TryParse(dropItem, out int parsedValue))
            {
                dropItemIDs.Add(parsedValue);
            }
        }

        foreach(int itemID in dropItemIDs)
        {
            GameObject itemPrefab = Resources.Load<GameObject>($"Prefabs/Items/{itemID}");
            if (itemPrefab != null)
            {
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("아이템 없음");
            }
        }

        gameObject.SetActive(false);
        isDie = true;
        GameManager.Instance.spawner.ReturnMonster(this);
    }

    private void LoadMonsterData()
    {
        if (GoogleSheetLoader.Instance == null || GoogleSheetLoader.Instance.MonsterDataList == null)
        {
            Debug.Log("데이터 로드 실패.");
            return;
        }

        Monster_Data data = GoogleSheetLoader.Instance.MonsterDataList.Find(m => m.MonsterID == monsterID);
        if (data != null)
        {
            monsterID = data.MonsterID;
            name = data.Name;
            description = data.Description;
            attack = data.Attack;
            attackMul = data.AttackMul;
            maxHP = data.MaxHP;
            maxHPMul = data.MaxHPMul;
            attackRange = data.AttackRange;
            attackRangeMul = data.AttackRangeMul;
            attackSpeed = data.AttackSpeed;
            moveSpeed = data.MoveSpeed;
            minExp = data.MinExp;
            maxExp = data.MaxExp;
            dropItem = data.DropItem;
        }
        else
        {
            Debug.Log("데이터 없음");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Weapon")) return;
        else
        {
            currentHP -= collision.GetComponent<Bullet>().damage;
        }

        if (currentHP > 0)
        {

        }
        else
        {
            Die();
        }
    }
}
