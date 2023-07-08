using UnityEngine;

public class Env : MonoBehaviour
{
    public static Env Instance { get; private set; }
    public int Currency = 100;
    public static int UpgradeCargoCost = 200;
    public static int UpgradeSpeedCost = 100;
    public static int NewBuyerCost = 50;
    public static float Boundary = 60f;
    public float Speed = -0.02f;
    public int SpeedLevel = 1;
    public int BuyerLevel = 1;
    public enum Merchandise {Empty, Locked, Sword, Shield, Armor};
    public enum Building {Empty, Market, Buyer};
    public (Building B, Merchandise M)[] map = new (Building B, Merchandise M)[20];

    public GameObject MarketPrefab;
    public GameObject BuyerPrefab;

    public Material EmptyMaterial;
    public Material SwordMaterial;
    public Material ShieldMaterial;
    public Material ArmorMaterial;

    public enum Paths {Empty, Straight, Left, Right};

    // public enum Items {Potion, Money, Sword};
    // public enum Enemies {Slime, Scorpion, Wolf, Guard, Orc};

    public enum Slots {Empty, Potion, Money, Sword, Slime, Scorpion, Wolf, Guard, Orc};

    public const int TileSize = 11;

    private void FillSpot((Building B, Merchandise M) building)
    {
        int spot = Random.Range(0, 20);

        if(map[spot] == (Building.Empty, Merchandise.Empty))
        {
            map[spot] = building;
        }
        else
        {
            FillSpot(building);
        }
    }

    private void InitializeBuildings()
    {
        (Building B, Merchandise M)[] initialBuildings =
        {
            (Building.Market, Merchandise.Sword),
            (Building.Market, Merchandise.Shield),
            (Building.Market, Merchandise.Armor),
            (Building.Buyer, Merchandise.Sword),
            (Building.Buyer, Merchandise.Shield),
            (Building.Buyer, Merchandise.Armor)
        };

        for(int i = 0; i < map.Length; i++)
        {
            map[i] = (Building.Empty, Merchandise.Empty);
        }

        foreach((Building B, Merchandise M) building in initialBuildings)
        {
            FillSpot(building);
        }
    }

    private GameObject InstantiateBuilding(int spot, GameObject prefab)
    {
        if(spot >= 10)
        {
            return Instantiate(prefab, new Vector3(-2.5f, 1.5f, (float)(((spot - 10f) * 10f) - 45f + 5f)), new Quaternion(0f, 1f, 0f, 0f));
        }
        else
        {
            return Instantiate(prefab, new Vector3(2.5f, 1.5f, (float)((spot * 10f) - 45f)), Quaternion.identity);
        }
    }

    private void PlaceBuildings()
    {
        for(int i = 0; i < map.Length; i++)
        {
            if(map[i] != (Building.Empty, Merchandise.Empty))
            {
                // if(map[i].B == Building.Market)
                // {
                //     GameObject market = InstantiateBuilding(i, MarketPrefab);
                //     market.GetComponent<Market>().merchandise = map[i].M;
                //     market.GetComponent<Market>().SwordMaterial = SwordMaterial;
                //     market.GetComponent<Market>().EmptyMaterial = EmptyMaterial;
                //     market.GetComponent<Market>().ShieldMaterial = ShieldMaterial;
                //     market.GetComponent<Market>().ArmorMaterial = ArmorMaterial;
                // }
                // else if(map[i].B == Building.Buyer)
                // {
                //     GameObject buyer = InstantiateBuilding(i, BuyerPrefab);
                //     buyer.GetComponent<Buyer>().merchandise = map[i].M;
                //     buyer.GetComponent<Buyer>().SwordMaterial = SwordMaterial;
                //     buyer.GetComponent<Buyer>().EmptyMaterial = EmptyMaterial;
                //     buyer.GetComponent<Buyer>().ShieldMaterial = ShieldMaterial;
                //     buyer.GetComponent<Buyer>().ArmorMaterial = ArmorMaterial;
                // }
            }
        }
    }

    private void NewBuyer()
    {
        if(Env.Instance.Currency >= NewBuyerCost * BuyerLevel)
        {
            Env.Instance.Currency -= NewBuyerCost * BuyerLevel;
            FillSpot((Building.Buyer, Merchandise.Armor));
        }
    }

    private void UpgradeSpeed()
    {
        if(Env.Instance.Currency >= UpgradeSpeedCost * SpeedLevel)
        {
            Env.Instance.Currency -= UpgradeSpeedCost * SpeedLevel;
            Speed += 1;
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

        InitializeBuildings();

        PlaceBuildings();
    }

    void Update()
    {
        if (Input.GetKeyDown("2"))
        {
            NewBuyer();
        }
        if (Input.GetKeyDown("3"))
        {
            UpgradeSpeed();
        }
    }
}
