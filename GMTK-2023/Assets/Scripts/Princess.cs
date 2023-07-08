using UnityEngine;

public class Princess : MonoBehaviour
{
    public Env.Paths heading = Env.Paths.North;
    public bool walking = true;

    private void CheckTileEntry (Collider other) {
        if (heading == Env.Paths.North) {
                if (other.GetComponent<Tile>().entry == Env.Paths.South) {
                    this.transform.Translate((new Vector3(Env.TileGridGap, 0f, 0f)));
                } else {
                    walking = false;
                }
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tile") {
            CheckTileEntry(other);
        }
        if (other.tag == "Slot") {
            // Implement Slot (Item/Enemy) interaction here
            print("You collected an item!");
        }
        if (other.tag == "Gate") {
            // Implement Next Phase or End Game here
            print("You win :D");
        }
    }

    private void OnTriggerStay(Collider other) {
        if (!walking) {
            CheckTileEntry(other);
        }
    }

    void Update()
    {
        if (walking) {
            if (heading == Env.Paths.North) {
                this.transform.Translate((new Vector3(Env.PrincessSpeed, 0f, 0f)));
            } else if (heading == Env.Paths.South) {
                this.transform.Translate((new Vector3(-Env.PrincessSpeed, 0f, 0f)));
            } else if (heading == Env.Paths.West) {
                this.transform.Translate((new Vector3(0f, 0f, Env.PrincessSpeed)));
            } else if (heading == Env.Paths.East) {
                this.transform.Translate((new Vector3(0f, 0f, -Env.PrincessSpeed)));
            }
        }
    }
}
