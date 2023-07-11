using UnityEngine;
using UnityEngine.UIElements;

public class InGameUI : MonoBehaviour
{
    public Env.Paths entry = Env.Paths.Empty;
    public Env.Paths exit = Env.Paths.Empty;
    public Env.Slots slot = Env.Slots.Empty;

    public int LastPrincessHealth;

    private void OnEnable()
    {
        // Getting the Root Element from the UIDocument
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button buttonPathSouthNorth = root.Q<Button>("PathSouthNorth");
        Button buttonPathEastWest = root.Q<Button>("PathEastWest");
        Button buttonPathSouthWest = root.Q<Button>("PathSouthWest");
        Button buttonPathSouthEast = root.Q<Button>("PathSouthEast");
        Button buttonPathEastNorth = root.Q<Button>("PathEastNorth");
        Button buttonPathWestNorth = root.Q<Button>("PathWestNorth");

        Button buttonSword = root.Q<Button>("SwordButton");
        Button buttonShield = root.Q<Button>("ShieldButton");
        Button buttonPotion = root.Q<Button>("PotionButton");

        Button buttonReset = root.Q<Button>("ResetButton");
        Button buttonFastforward = root.Q<Button>("FastforwardButton");

        buttonPathSouthNorth.clicked += () => OnPathSouthNorthClicked();
        buttonPathEastWest.clicked += () => OnPathEastWestClicked();
        buttonPathSouthWest.clicked += () => OnPathSouthWestClicked();
        buttonPathSouthEast.clicked += () => OnPathSouthEastClicked();
        buttonPathEastNorth.clicked += () => OnPathEastNorthClicked();
        buttonPathWestNorth.clicked += () => OnPathWestNorthClicked();

        buttonSword.clicked += () => OnSwordClicked();
        buttonShield.clicked += () => OnShieldClicked();
        buttonPotion.clicked += () => OnPotionClicked();

        buttonReset.clicked += () => OnResetClicked();
        buttonFastforward.clicked += () => OnFastforwardClicked();
    }

    private void OnPathSouthNorthClicked()
    {
        Env.Instance.pathEntrySelection = Env.Paths.South;
        Env.Instance.pathExitSelection = Env.Paths.North;
        Env.Instance.itemSelection = Env.Slots.Empty;
    }

    private void OnPathEastWestClicked()
    {
        Env.Instance.pathEntrySelection = Env.Paths.East;
        Env.Instance.pathExitSelection = Env.Paths.West;
        Env.Instance.itemSelection = Env.Slots.Empty;
    }

    private void OnPathSouthWestClicked()
    {
        Env.Instance.pathEntrySelection = Env.Paths.South;
        Env.Instance.pathExitSelection = Env.Paths.West;
        Env.Instance.itemSelection = Env.Slots.Empty;
    }

    private void OnPathSouthEastClicked()
    {
        Env.Instance.pathEntrySelection = Env.Paths.South;
        Env.Instance.pathExitSelection = Env.Paths.East;
        Env.Instance.itemSelection = Env.Slots.Empty;
    }

    private void OnPathEastNorthClicked()
    {
        Env.Instance.pathEntrySelection = Env.Paths.East;
        Env.Instance.pathExitSelection = Env.Paths.North;
        Env.Instance.itemSelection = Env.Slots.Empty;
    }

    private void OnPathWestNorthClicked()
    {
        Env.Instance.pathEntrySelection = Env.Paths.West;
        Env.Instance.pathExitSelection = Env.Paths.North;
        Env.Instance.itemSelection = Env.Slots.Empty;
    }

    private void OnSwordClicked()
    {
        Env.Instance.pathEntrySelection = Env.Paths.Empty;
        Env.Instance.pathExitSelection = Env.Paths.Empty;
        Env.Instance.itemSelection = Env.Slots.Sword;
    }

    private void OnShieldClicked()
    {
        Env.Instance.pathEntrySelection = Env.Paths.Empty;
        Env.Instance.pathExitSelection = Env.Paths.Empty;
        Env.Instance.itemSelection = Env.Slots.Shield;
    }

    private void OnPotionClicked()
    {
        Env.Instance.pathEntrySelection = Env.Paths.Empty;
        Env.Instance.pathExitSelection = Env.Paths.Empty;
        Env.Instance.itemSelection = Env.Slots.Potion;
    }

    private void OnResetClicked()
    {
        Env.Instance.PrincessSpeed = Env.Instance.PrincessInitialSpeed;
        Env.Instance.ResetLevel();
    }

    private void OnFastforwardClicked()
    {
        Env.Instance.PrincessSpeed += Env.Instance.PrincessSpeedIncreaseAmount;
    }

    private void UpdateUIData()
    {
        // Getting the Root Element from the UIDocument
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Label labelCoins = root.Q<Label>("GoldValue");
        Label labelAttackStatus = root.Q<Label>("AttackStats");

        labelCoins.text = "" + Env.Instance.Coins;
        labelAttackStatus.text = "" + Env.Instance.PrincessAttack;

        if (Env.Instance.PrincessHealth != LastPrincessHealth) { }
    }

    private void UpdateHearts()
    {
        // // Getting the Root Element from the UIDocument
        // VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        // VisualElement heartsContainer = root.Q<VisualElement>("HeartContainer");
        // IMGUIContainer heart = root.Q<IMGUIContainer>("Heart");

        // LastPrincessHealth = Env.Instance.PrincessHealth;

        // IMGUIContainer heartCopy = heart.CloneTree();

        // heartsContainer.Clear();

        // for (int i = 0; i < LastPrincessHealth; i++)
        // {
        //     heartsContainer.Add(heartCopy);
        // }
    }

    void Update()
    {
        UpdateUIData();
    }
}
