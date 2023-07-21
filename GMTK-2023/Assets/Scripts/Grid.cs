using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] public GameObject Tile;
    
    [SerializeField] public GameObject Gate;

    public Material GrassEasy;
    public Material GrassMedium;
    public Material GrassHard;
    public Material GroundHard;

    private float zModifier = 0;

    private void SpawnTile (int i, int j) {
        GameObject tile = Instantiate(Tile, new Vector3(i * (Env.TileSize + Env.TileGridGap), 0, (j * (Env.TileSize + Env.TileGridGap)) + zModifier), Quaternion.identity, this.transform);

        tile.GetComponent<Tile>().entry = Env.Paths.Empty;
        tile.GetComponent<Tile>().exit = Env.Paths.Empty;
        tile.GetComponent<Tile>().slot = Env.Slots.Empty;

        Material[] materials = tile.transform.Find("Mesh").GetComponent<MeshRenderer>().materials;

        if (Env.Instance.Level < 3) {
            materials[1] = GrassEasy;
            tile.transform.Find("Mesh").GetComponent<MeshRenderer>().materials = materials;
        } else if (Env.Instance.Level < 5) {
            materials[1] = GrassMedium;
            tile.transform.Find("Mesh").GetComponent<MeshRenderer>().materials = materials;
        } else {
            materials[0] = GroundHard;
            materials[1] = GrassHard;
            tile.transform.Find("Mesh").GetComponent<MeshRenderer>().materials = materials;
        }

        Env.Instance.SetInitialTilesMaterials(materials);
        
        int rand = Random.Range(0, 5);

        if (Env.Instance.Level == 0) {
            if (i == 1 && j == 0) {
                tile.GetComponent<Tile>().slot = Env.Slots.Coins;
            }
        } else if (Env.Instance.Level == 1) {
            if (i == 0 && j == 1) {
                tile.GetComponent<Tile>().slot = Env.Slots.Slime;
            }
        } else if (Env.Instance.Level == 2) {
            if (i == 0 && j == 0) {
                tile.GetComponent<Tile>().slot = Env.Slots.Coins;
            } else if (i == 1 && j == 1) {
                tile.GetComponent<Tile>().slot = Env.Slots.Slime;
            }
        } else if (Env.Instance.Level == 3) {
            if (i == 0 && j == 0) {
                tile.GetComponent<Tile>().slot = Env.Slots.Slime;
            } else if (i == 1 && j == 1) {
                tile.GetComponent<Tile>().slot = Env.Slots.Coins;
            } else if (i == 2 && j == 0) {
                tile.GetComponent<Tile>().slot = Env.Slots.Ghost;
            }
        } else if (Env.Instance.Level == 4) {
            if (i == 0 && j == 1) {
                tile.GetComponent<Tile>().slot = Env.Slots.Ghost;
            } else if (i == 1 && j == 0) {
                tile.GetComponent<Tile>().slot = Env.Slots.Coins;
            } else if (i == 2 && j == 1) {
                tile.GetComponent<Tile>().slot = Env.Slots.Ghost;
            } else if (i == 3 && j == 0) {
                tile.GetComponent<Tile>().slot = Env.Slots.Coins;
            }
        } else if (Env.Instance.Level == 5) {
            if (i == 1 && j == 0) {
                tile.GetComponent<Tile>().slot = Env.Slots.Ghost;
            } else if (i == 2 && j == 1) {
                tile.GetComponent<Tile>().slot = Env.Slots.Ghost;
            } else if (i == 1 && j == 1) {
                tile.GetComponent<Tile>().slot = Env.Slots.Coins;
            } else if (i == 1 && j == 2) {
                tile.GetComponent<Tile>().slot = Env.Slots.Ghost;
            } else if (i == 0 && j == 1) {
                tile.GetComponent<Tile>().slot = Env.Slots.Ghost;
            }
        } else if (Env.Instance.Level < 8) {
            if (rand == 0) {
                tile.GetComponent<Tile>().slot = Env.Slots.Ghost;
            } else if (rand == 1) {
                tile.GetComponent<Tile>().slot = Env.Slots.Coins;
            } else if (rand == 2 && Env.Instance.Level > 6) {
                tile.GetComponent<Tile>().slot = Env.Slots.Troll;
            }
        } else {
            if (i == 3 && j == 1) {
                tile.GetComponent<Tile>().slot = Env.Slots.Knight;
            } else if (rand < 3) {
                tile.GetComponent<Tile>().slot = Env.Slots.Troll;
            }
        }
    }

    private void ClearTiles () {
        for (int i = 0; i < this.transform.childCount; i++) {
            Object.Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    private void PlaceGate () {
        Gate.transform.position = new Vector3((Env.Instance.LevelsTileHorizontalConfig[Env.Instance.Level] * Env.TileSize) + 1 - (zModifier / 2), 0, ((Env.Instance.LevelsTileVerticalConfig[Env.Instance.Level] - 1) * (Env.TileSize + Env.TileGridGap)) + (zModifier * 2));
        if (Env.Instance.Level >= 8) {
            Gate.transform.localScale = new Vector3(2, 2, 2);
        } else {
            Gate.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void BuildLevel () {
        ClearTiles();

        if (Env.Instance.Level >= 8) {
            zModifier = -(Env.TileSize + Env.TileGridGap);
        } else {
            zModifier = 0;
        }

        for (int i = 0; i < Env.Instance.LevelsTileHorizontalConfig[Env.Instance.Level]; i++) {
            for (int j = 0; j < Env.Instance.LevelsTileVerticalConfig[Env.Instance.Level]; j++) {
                SpawnTile(i, j);
            }
        }

        PlaceGate();
    }

    void Start()
    {
        BuildLevel();
    }
}
