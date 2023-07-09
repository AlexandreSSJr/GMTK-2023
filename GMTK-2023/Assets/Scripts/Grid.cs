using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] public int Horizontal = 3;
    [SerializeField] public int Vertical = 3;

    [SerializeField] public GameObject Tile;
    
    [SerializeField] public GameObject Gate;

    private void SpawnTile (int i, int j) {
        GameObject tile = Instantiate(Tile, new Vector3(i * (Env.TileSize + Env.TileGridGap), 0, j * (Env.TileSize + Env.TileGridGap)), Quaternion.identity);
        tile.GetComponent<Tile>().entry = Env.Paths.Empty;
        tile.GetComponent<Tile>().exit = Env.Paths.Empty;
        tile.GetComponent<Tile>().slot = Env.Slots.Empty;
        
        // int rand = Random.Range(0, 5);
        // if (rand == 0) {
        //     tile.GetComponent<Tile>().slot = Env.Slots.Slime;
        // } else if (rand == 1) {
        //     tile.GetComponent<Tile>().slot = Env.Slots.Coins;
        // }
    }

    private void PlaceGate () {
        Gate.transform.Translate(new Vector3((Horizontal * Env.TileSize) + 1, 0, (Vertical - 1) * (Env.TileSize + Env.TileGridGap)));
    }

    void Start()
    {
        for (int i = 0; i < Horizontal; i++) {
            for (int j = 0; j < Vertical; j++) {
                SpawnTile(i, j);
            }
        }
        PlaceGate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
