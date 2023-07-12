using UnityEngine;
using UnityEngine.SceneManagement;

public class Env : MonoBehaviour
{
    public static Env Instance { get; private set; }

    public enum Paths {Empty, North, South, West, East};

    public enum Slots {Empty, Potion, Sword, Shield, Coins, Chest, Slime, Scorpion, Wolf, Guard, Orc};

    public enum Stats {Coins, PrincessLevel, PrincessHealth, PrincessAttack, PrincessEquipLeft, PrincessEquipRight};

    public enum Equips {Empty, WoodenSword, IronSword, GoldSword, WoodenShield, IronShield, GoldShield};

    public float Timer = 0;
    public bool CountTimer = true;
    public int Level = 0;
    public int FirstLevel = 0;
    public int LastLevel = 5;

    public int[] LevelsTileHorizontalConfig = {3, 1, 2, 4, 3, 4};
    public int[] LevelsTileVerticalConfig = {1, 3, 2, 2, 3, 4};

    public int Coins = 100;
    public int CoinsAtStartOfLevel = 100;

    public Vector3 PrincessStartingPosition = new Vector3(-25, 0, 0);
    public float PrincessInitialSpeed = 0.05f;
    public float PrincessSpeedIncreaseAmount = 0.05f;
    public float PrincessSpeed = 0.05f;
    public int PrincessLevel = 1;
    public int PrincessXP = 0;
    public int PrincessMaxHealth = 3;
    public int PrincessHealth = 3;
    public int PrincessAttack = 1;
    public int PrincessDefense = 0;
    public Equips PrincessEquipLeft = Equips.Empty;
    public Equips PrincessEquipRight = Equips.Empty;
    public int[] PrincessLevelUpXPCosts = {0, 10, 30, 50, 100, 200};

    public const int TileSize = 10;
    public const int TileGridGap = 1;

    public Env.Paths pathEntrySelection = Env.Paths.Empty;
    public Env.Paths pathExitSelection = Env.Paths.Empty;
    public Env.Slots itemSelection = Env.Slots.Empty;
    public int itemSelectionCost = 0;

    public const int CoinsAmount = 50;
    public const int SlimeDamage = 1;
    public const int SlimeXPGain = 10;
    public const int SwordDamageUpgrade = 2;
    public const int ShieldDefenseUpgrade = 1;
    public const int PathBuildCost = 5;
    public const int SwordBuildCost = 20;
    public const int ShieldBuildCost = 20;
    public const int PotionBuildCost = 10;
    public const int PotionHealingAmount = 1;

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
            itemSelectionCost = SwordBuildCost;
        } else if (Input.GetKeyDown("w"))
        {
            itemSelection = Env.Slots.Shield;
            pathEntrySelection = Env.Paths.Empty;
            pathExitSelection = Env.Paths.Empty;
            itemSelectionCost = ShieldBuildCost;
        } else if (Input.GetKeyDown("e"))
        {
            itemSelection = Env.Slots.Potion;
            pathEntrySelection = Env.Paths.Empty;
            pathExitSelection = Env.Paths.Empty;
            itemSelectionCost = PotionBuildCost;
        }
    }

    void UpdateTimer () {
        Timer += Time.deltaTime;
    }

    public void CheckPrincessLevel () {
        if (PrincessXP >= PrincessLevelUpXPCosts[PrincessLevel]) {
            PrincessXP -= PrincessLevelUpXPCosts[PrincessLevel];
            PrincessLevel++;
            PrincessAttack++;
            PrincessDefense++;
            PrincessMaxHealth++;
            PrincessHealth++;
        }
    }

    public void GoToNextLevel () {
        if (Level < LastLevel) {
            Level++;
        }
        else {
            CountTimer = false;
            SceneManager.LoadScene("End");
        }

        PrincessSpeed = PrincessInitialSpeed;
        CoinsAtStartOfLevel = Coins;
        ResetLevel();
    }

    public void ResetLevel () {
        // TODO: Also need to restart princess stats such as level, xp, etc
        Coins = CoinsAtStartOfLevel;
        PrincessSpeed = PrincessInitialSpeed;
        this.transform.Find("Grid").GetComponent<Grid>().BuildLevel();
        this.transform.Find("Princess").GetComponent<Princess>().SendPrincessToStart();
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
        if (CountTimer) {
            UpdateTimer();
        }
    }
}
