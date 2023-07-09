using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] public int Horizontal = 3;
    [SerializeField] public int Vertical = 3;

    [SerializeField] public GameObject Tile;
    
    [SerializeField] public GameObject Gate;

    private void SpawnTile (int i, int j) {
        GameObject tile = Instantiate(Tile, new Vector3(i * (Env.TileSize + Env.TileGridGap), 0, j * (Env.TileSize + Env.TileGridGap)), Quaternion.identity);
        // TODO: After testing, reset all of these to Empty.
        tile.GetComponent<Tile>().entry = Env.Paths.South;
        tile.GetComponent<Tile>().exit = Env.Paths.North;
        tile.GetComponent<Tile>().slot = Env.Slots.Potion;
    }

    private void PlaceGate () {
        Gate.transform.Translate(new Vector3((Horizontal * Env.TileSize) - 1, 0, (Vertical - 1) * (Env.TileSize + Env.TileGridGap)));
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
