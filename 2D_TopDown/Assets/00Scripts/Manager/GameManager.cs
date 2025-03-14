using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player_Controller player;
    public MonsterSpawner spawner;

    private void Awake()
    {
        if(instance == null) instance = this;
    }
}
