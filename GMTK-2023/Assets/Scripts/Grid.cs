using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] public int Horizontal = 3;
    [SerializeField] public int Vertical = 3;

    [SerializeField] public GameObject Tile;

    private void SpawnTile (int i, int j) {
        GameObject tile = Instantiate(Tile, new Vector3(i * (Env.TileSize + Env.TileGridGap), 0, j * (Env.TileSize + Env.TileGridGap)), Quaternion.identity);
        // tile.GetComponent<Tile>().path = Env.Paths.Straight;
        // tile.GetComponent<Tile>().slot = Env.Slots.Potion;
    }

    void Start()
    {
        for (int i = 0; i < Horizontal; i++) {
            for (int j = 0; j < Vertical; j++) {
                SpawnTile(i, j);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
