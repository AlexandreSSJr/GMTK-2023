using UnityEngine;

public class Princess : MonoBehaviour
{
    public Env.Paths heading = Env.Paths.North;
    public bool walking = true;
    private bool enteringTile = false;
    private float tileTraversed = 0f;
    private Env.Paths currentTileExitDirection = Env.Paths.Empty;

    private void CheckTileEntry (Collider other) {
        if (other && other.GetComponent<Tile>() && other.GetComponent<Tile>().entry != Env.Paths.Empty) {

            float jumpDistance = Env.TileGridGap + 1;
            Env.Paths tileEntry = other.GetComponent<Tile>().entry;
            Env.Paths tileExit = other.GetComponent<Tile>().exit;

            if (heading == Env.Paths.North) {
                if (tileEntry == Env.Paths.South || tileExit == Env.Paths.South) {
                    this.transform.Translate((new Vector3(jumpDistance, 0f, 0f)));
                } else {
                    walking = false;
                }
            } else if (heading == Env.Paths.South) {
                if (tileEntry == Env.Paths.North || tileExit == Env.Paths.North) {
                    this.transform.Translate((new Vector3(-jumpDistance, 0f, 0f)));
                } else {
                    walking = false;
                }
            } else if (heading == Env.Paths.West) {
                if (tileEntry == Env.Paths.East || tileExit == Env.Paths.East) {
                    this.transform.Translate((new Vector3(0f, 0f, jumpDistance)));
                } else {
                    walking = false;
                }
            } else if (heading == Env.Paths.East) {
                if (tileEntry == Env.Paths.West || tileExit == Env.Paths.West) {
                    this.transform.Translate((new Vector3(0f, 0f, -jumpDistance)));
                } else {
                    walking = false;
                }
            } else {
                    walking = false;
            }
        }
    }

    private void HeadToExit () {
        if (currentTileExitDirection != Env.Paths.Empty) {
            heading = currentTileExitDirection;
        } else {
            walking = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other) {
            if (other.tag == "Tile") {
                enteringTile = true;
                currentTileExitDirection = other.GetComponent<Tile>().exit;
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
    }

    void OnTriggerStay(Collider other) {
        if (!walking && other) {
            CheckTileEntry(other);
        }
    }

    void FixedUpdate()
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

            if (enteringTile) {
                tileTraversed += Env.PrincessSpeed;
            }

            if (tileTraversed >= Env.TileSize / 2) {
                enteringTile = false;
                tileTraversed = 0f;
                HeadToExit();
            }
        }
    }
}
