using UnityEngine;

public class Env : MonoBehaviour
{
    public static Env Instance { get; private set; }

    public enum Paths {Empty, North, South, West, East};

    public enum Slots {Empty, Potion, Coins, Chest, Slime, Scorpion, Wolf, Guard, Orc};

    public enum Stats {Coins, PrincessLevel, PrincessHealth, PrincessAttack, PrincessEquipLeft, PrincessEquipRight};

    public enum Equips {Empty, WoodenSword, IronSword, GoldSword, WoodenShield, IronShield, GoldShield};

    public int Coins = 0;

    public static float PrincessSpeed = 0.01f;
    public int PrincessLevel = 1;
    public int PrincessMaxHealth = 3;
    public int PrincessHealth = 3;
    public int PrincessAttack = 1;
    public Equips PrincessEquipLeft = Equips.Empty;
    public Equips PrincessEquipRight = Equips.Empty;

    public const int TileSize = 10;
    public const int TileGridGap = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            print("1");
        }
        if (Input.GetKeyDown("2"))
        {
            print("2");
        }
    }
}
