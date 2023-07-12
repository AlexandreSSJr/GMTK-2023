using UnityEngine;
using UnityEngine.UIElements;

public class InGameUI : MonoBehaviour
{
    Label labelCoins;
    Label labelPrincessTitle;
    Label labelPrincessLevel;
    Label labelPrincessHealth;
    Label labelPrincessAttack;
    Label labelPrincessDefense;

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

        labelCoins = root.Q<Label>("GoldValue");
        labelPrincessTitle = root.Q<Label>("PrincessTitle");
        labelPrincessLevel = root.Q<Label>("PrincessLevel");
        labelPrincessHealth = root.Q<Label>("PrincessHealth");
        labelPrincessAttack = root.Q<Label>("PrincessAttack");
        labelPrincessDefense = root.Q<Label>("PrincessDefense");

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
        Env.Instance.ResetLevel();
    }

    private void OnFastforwardClicked()
    {
        Env.Instance.PrincessSpeed += Env.Instance.PrincessSpeedIncreaseAmount;
    }

    private void UpdateUIData()
    {
        labelCoins.text = Env.Instance.Coins.ToString();
        labelPrincessTitle.text = "Princess ( Lv. " + Env.Instance.PrincessLevel.ToString() + " )";
        labelPrincessLevel.text = Env.Instance.PrincessXP.ToString() + " xp";
        labelPrincessHealth.text = Env.Instance.PrincessHealth.ToString() + " / " + Env.Instance.PrincessMaxHealth.ToString();
        labelPrincessAttack.text = Env.Instance.PrincessAttack.ToString();
        labelPrincessDefense.text = Env.Instance.PrincessDefense.ToString();

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
