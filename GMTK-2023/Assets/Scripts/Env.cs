using UnityEngine;

public class Env : MonoBehaviour
{
    public static Env Instance { get; private set; }

    public enum Paths {Empty, North, South, West, East};

    public enum Slots {Empty, Potion, Sword, Shield, Coins, Chest, Slime, Scorpion, Wolf, Guard, Orc};

    public enum Stats {Coins, PrincessLevel, PrincessHealth, PrincessAttack, PrincessEquipLeft, PrincessEquipRight};

    public enum Equips {Empty, WoodenSword, IronSword, GoldSword, WoodenShield, IronShield, GoldShield};

    public int Coins = 100;

    public float PrincessSpeed = 0.03f;
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

    public const int CoinsAmount = 100;
    public const int SlimeDamage = 1;
    public const int SwordDamageUpgrade = 2;
    public const int ShieldDefenseUpgrade = 1;
    public const int PathBuildCost = 5;
    public const int SwordBuildCost = 20;
    public const int ShieldBuildCost = 20;
    public const int PotionBuildCost = 10;

    void Controls () {
        if (Input.GetKeyDown("1"))
        {
            pathEntrySelection = Env.Paths.South;
            pathExitSelection = Env.Paths.North;
            itemSelection = Env.Slots.Empty;
            Coins -= PathBuildCost;
        } else if (Input.GetKeyDown("2"))
        {
            pathEntrySelection = Env.Paths.East;
            pathExitSelection = Env.Paths.West;
            itemSelection = Env.Slots.Empty;
            Coins -= PathBuildCost;
        } else if (Input.GetKeyDown("3"))
        {
            pathEntrySelection = Env.Paths.South;
            pathExitSelection = Env.Paths.West;
            itemSelection = Env.Slots.Empty;
            Coins -= PathBuildCost;
        } else if (Input.GetKeyDown("4"))
        {
            pathEntrySelection = Env.Paths.South;
            pathExitSelection = Env.Paths.East;
            itemSelection = Env.Slots.Empty;
            Coins -= PathBuildCost;
        } else if (Input.GetKeyDown("5"))
        {
            pathEntrySelection = Env.Paths.East;
            pathExitSelection = Env.Paths.North;
            itemSelection = Env.Slots.Empty;
            Coins -= PathBuildCost;
        } else if (Input.GetKeyDown("6"))
        {
            pathEntrySelection = Env.Paths.West;
            pathExitSelection = Env.Paths.North;
            itemSelection = Env.Slots.Empty;
            Coins -= PathBuildCost;
        } else if (Input.GetKeyDown("q"))
        {
            itemSelection = Env.Slots.Sword;
            pathEntrySelection = Env.Paths.Empty;
            pathExitSelection = Env.Paths.Empty;
            Coins -= SwordBuildCost;
        } else if (Input.GetKeyDown("w"))
        {
            itemSelection = Env.Slots.Shield;
            pathEntrySelection = Env.Paths.Empty;
            pathExitSelection = Env.Paths.Empty;
            Coins -= ShieldBuildCost;
        } else if (Input.GetKeyDown("e"))
        {
            itemSelection = Env.Slots.Potion;
            pathEntrySelection = Env.Paths.Empty;
            pathExitSelection = Env.Paths.Empty;
            Coins -= PotionBuildCost;
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
