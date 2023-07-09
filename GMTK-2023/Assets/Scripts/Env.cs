using UnityEngine;

public class Env : MonoBehaviour
{
    public static Env Instance { get; private set; }

    public enum Paths {Empty, North, South, West, East};

    public enum Slots {Empty, Potion, Sword, Shield, Coins, Chest, Slime, Scorpion, Wolf, Guard, Orc};

    public enum Stats {Coins, PrincessLevel, PrincessHealth, PrincessAttack, PrincessEquipLeft, PrincessEquipRight};

    public enum Equips {Empty, WoodenSword, IronSword, GoldSword, WoodenShield, IronShield, GoldShield};

    public int Coins = 0;

    public static float PrincessSpeed = 0.03f;
    public int PrincessLevel = 1;
    public int PrincessMaxHealth = 3;
    public int PrincessHealth = 3;
    public int PrincessAttack = 1;
    public Equips PrincessEquipLeft = Equips.Empty;
    public Equips PrincessEquipRight = Equips.Empty;

    public const int TileSize = 10;
    public const int TileGridGap = 1;

    public Env.Paths pathEntrySelection = Env.Paths.Empty;
    public Env.Paths pathExitSelection = Env.Paths.Empty;
    public Env.Slots itemSelection = Env.Slots.Empty;

    void Controls () {
        if (Input.GetKeyDown("1"))
        {
            pathEntrySelection = Env.Paths.South;
            pathExitSelection = Env.Paths.North;
            itemSelection = Env.Slots.Empty;
        } else if (Input.GetKeyDown("2"))
        {
            pathEntrySelection = Env.Paths.East;
            pathExitSelection = Env.Paths.West;
            itemSelection = Env.Slots.Empty;
        } else if (Input.GetKeyDown("3"))
        {
            pathEntrySelection = Env.Paths.South;
            pathExitSelection = Env.Paths.West;
            itemSelection = Env.Slots.Empty;
        } else if (Input.GetKeyDown("4"))
        {
            pathEntrySelection = Env.Paths.South;
            pathExitSelection = Env.Paths.East;
            itemSelection = Env.Slots.Empty;
        } else if (Input.GetKeyDown("5"))
        {
            pathEntrySelection = Env.Paths.East;
            pathExitSelection = Env.Paths.North;
            itemSelection = Env.Slots.Empty;
        } else if (Input.GetKeyDown("6"))
        {
            pathEntrySelection = Env.Paths.West;
            pathExitSelection = Env.Paths.North;
            itemSelection = Env.Slots.Empty;
        } else if (Input.GetKeyDown("q"))
        {
            itemSelection = Env.Slots.Sword;
            pathEntrySelection = Env.Paths.Empty;
            pathExitSelection = Env.Paths.Empty;
        } else if (Input.GetKeyDown("w"))
        {
            // itemSelection = Env.Slots.Sword;
            itemSelection = Env.Slots.Shield;
            pathEntrySelection = Env.Paths.Empty;
            pathExitSelection = Env.Paths.Empty;
        } else if (Input.GetKeyDown("e"))
        {
            // itemSelection = Env.Slots.Shield;
            itemSelection = Env.Slots.Potion;
            pathEntrySelection = Env.Paths.Empty;
            pathExitSelection = Env.Paths.Empty;
        }
    }

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

    void Update () {
        Controls();
    }
}
