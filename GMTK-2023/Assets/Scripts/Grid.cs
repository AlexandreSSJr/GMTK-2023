using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] public GameObject Tile;
    
    [SerializeField] public GameObject Gate;

    public Material GrassEasy;
    public Material GrassMedium;
    public Material GrassHard;
    public Material GroundHard;

    private void SpawnTile (int i, int j) {
        GameObject tile = Instantiate(Tile, new Vector3(i * (Env.TileSize + Env.TileGridGap), 0, j * (Env.TileSize + Env.TileGridGap)), Quaternion.identity, this.transform);
        tile.GetComponent<Tile>().entry = Env.Paths.Empty;
        tile.GetComponent<Tile>().exit = Env.Paths.Empty;
        tile.GetComponent<Tile>().slot = Env.Slots.Empty;

        Material[] materials = tile.transform.Find("Mesh").GetComponent<MeshRenderer>().materials;

        if (Env.Instance.Level < 2) {
            materials[1] = GrassEasy;
            tile.transform.Find("Mesh").GetComponent<MeshRenderer>().materials = materials;
        } else if (Env.Instance.Level < 4) {
            materials[1] = GrassMedium;
            tile.transform.Find("Mesh").GetComponent<MeshRenderer>().materials = materials;
        } else {
            materials[0] = GroundHard;
            materials[1] = GrassHard;
            tile.transform.Find("Mesh").GetComponent<MeshRenderer>().materials = materials;
        }

        Env.Instance.SetInitialTilesMaterials(materials);
        
        int rand = Random.Range(0, 5);

        if (rand == 0) {
            tile.GetComponent<Tile>().slot = Env.Slots.Slime;
        } else if (rand == 1) {
            tile.GetComponent<Tile>().slot = Env.Slots.Coins;
        }
    }

    private void ClearTiles () {
        for (int i = 0; i < this.transform.childCount; i++) {
            Object.Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    private void PlaceGate () {
        Gate.transform.position = new Vector3((Env.Instance.LevelsTileHorizontalConfig[Env.Instance.Level] * Env.TileSize) + 1, 0, (Env.Instance.LevelsTileVerticalConfig[Env.Instance.Level] - 1) * (Env.TileSize + Env.TileGridGap));
    }

    public void BuildLevel () {
        ClearTiles();

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
