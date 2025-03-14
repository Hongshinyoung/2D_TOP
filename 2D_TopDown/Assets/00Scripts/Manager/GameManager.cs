using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player_Controller player;
    public MonsterSpawner spawner;

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }
}
