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
        Debug.Log("sn");
        Env.Instance.pathEntrySelection = Env.Paths.South;
        Env.Instance.pathExitSelection = Env.Paths.North;
        Env.Instance.itemSelection = Env.Slots.Empty;
    }

    private void OnPathEastWestClicked()
    {
        Debug.Log("ew");
        Env.Instance.pathEntrySelection = Env.Paths.East;
        Env.Instance.pathExitSelection = Env.Paths.West;
        Env.Instance.itemSelection = Env.Slots.Empty;
    }

    private void OnPathSouthWestClicked()
    {
        Debug.Log("sw");
        Env.Instance.pathEntrySelection = Env.Paths.South;
        Env.Instance.pathExitSelection = Env.Paths.West;
        Env.Instance.itemSelection = Env.Slots.Empty;
    }

    private void OnPathSouthEastClicked()
    {
        Debug.Log("se");
        Env.Instance.pathEntrySelection = Env.Paths.South;
        Env.Instance.pathExitSelection = Env.Paths.East;
        Env.Instance.itemSelection = Env.Slots.Empty;
    }

    private void OnPathEastNorthClicked()
    {
        Debug.Log("en");
        Env.Instance.pathEntrySelection = Env.Paths.East;
        Env.Instance.pathExitSelection = Env.Paths.North;
        Env.Instance.itemSelection = Env.Slots.Empty;
    }

    private void OnPathWestNorthClicked()
    {
        Debug.Log("wn");
        Env.Instance.pathEntrySelection = Env.Paths.West;
        Env.Instance.pathExitSelection = Env.Paths.North;
        Env.Instance.itemSelection = Env.Slots.Empty;
    }

    private void OnSwordClicked()
    {
        Debug.Log("sword");
        Env.Instance.pathEntrySelection = Env.Paths.Empty;
        Env.Instance.pathExitSelection = Env.Paths.Empty;
        Env.Instance.itemSelection = Env.Slots.Sword;
    }

    private void OnShieldClicked()
    {
        Debug.Log("shield");
        Env.Instance.pathEntrySelection = Env.Paths.Empty;
        Env.Instance.pathExitSelection = Env.Paths.Empty;
        Env.Instance.itemSelection = Env.Slots.Shield;
    }

    private void OnPotionClicked()
    {
        Debug.Log("potion");
        Env.Instance.pathEntrySelection = Env.Paths.Empty;
        Env.Instance.pathExitSelection = Env.Paths.Empty;
        Env.Instance.itemSelection = Env.Slots.Potion;
    }

    private void OnResetClicked()
    {
        Debug.Log("reset");
        Env.Instance.PrincessSpeed = 0.03f;
        Application.LoadLevel(Application.loadedLevel);
    }

    private void OnFastforwardClicked()
    {
        Debug.Log("ff");
        Env.Instance.PrincessSpeed += 0.02f;
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
