using UnityEngine;

public class Princess : MonoBehaviour
{
    public Env.Paths heading = Env.Paths.North;
    public bool walking = true;
    private bool enteringTile = false;
    private bool exitInverted = false;
    private float tileTraversed = 0f;
    private Env.Paths currentTileExitDirection = Env.Paths.Empty;
    private const float hopHeight = 1f;
    private const float hopSpeed = 0.08f;
    private bool goingUp = true;
    private const int turnMaxAngle = 30;
    private const int turnSpeed = 1;
    private bool turningRight = true;

    private void CheckTileEntry (Collider other) {
        if (other && other.GetComponent<Tile>() && other.GetComponent<Tile>().entry != Env.Paths.Empty) {

            float jumpDistance = Env.TileGridGap + 1;
            Env.Paths tileEntry = other.GetComponent<Tile>().entry;
            Env.Paths tileExit = other.GetComponent<Tile>().exit;

            if (heading == Env.Paths.North) {
                if (tileEntry == Env.Paths.South) {
                    this.transform.Translate((new Vector3(jumpDistance, 0f, 0f)));
                    exitInverted = false;
                } else if (tileExit == Env.Paths.South) {
                    this.transform.Translate((new Vector3(jumpDistance, 0f, 0f)));
                    exitInverted = true;
                } else {
                    walking = false;
                }
            } else if (heading == Env.Paths.South) {
                if (tileEntry == Env.Paths.North) {
                    this.transform.Translate((new Vector3(-jumpDistance, 0f, 0f)));
                    exitInverted = false;
                } else if (tileExit == Env.Paths.North) {
                    this.transform.Translate((new Vector3(-jumpDistance, 0f, 0f)));
                    exitInverted = true;
                } else {
                    walking = false;
                }
            } else if (heading == Env.Paths.West) {
                if (tileEntry == Env.Paths.East) {
                    this.transform.Translate((new Vector3(0f, 0f, jumpDistance)));
                    exitInverted = false;
                } else if (tileExit == Env.Paths.East) {
                    this.transform.Translate((new Vector3(0f, 0f, jumpDistance)));
                    exitInverted = true;
                } else {
                    walking = false;
                }
            } else if (heading == Env.Paths.East) {
                if (tileEntry == Env.Paths.West) {
                    this.transform.Translate((new Vector3(0f, 0f, -jumpDistance)));
                    exitInverted = false;
                } else if (tileExit == Env.Paths.West) {
                    this.transform.Translate((new Vector3(0f, 0f, -jumpDistance)));
                    exitInverted = true;
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
            if (exitInverted) {
                if (currentTileExitDirection == Env.Paths.North) {
                    currentTileExitDirection = Env.Paths.South;
                } else if (currentTileExitDirection == Env.Paths.South) {
                    currentTileExitDirection = Env.Paths.North;
                } else if (currentTileExitDirection == Env.Paths.West) {
                    currentTileExitDirection = Env.Paths.East;
                } else if (currentTileExitDirection == Env.Paths.East) {
                    currentTileExitDirection = Env.Paths.West;
                }
            } else {
                heading = currentTileExitDirection;
            }
        } else {
            walking = false;
        }
    }

    void HopAnimation () {
        if (goingUp) {
            this.transform.Find("Mesh").Translate(new Vector3(0f, hopSpeed, 0f));
            if (this.transform.Find("Mesh").position.y >= hopHeight) {
                goingUp = false;
            }
        } else {
            this.transform.Find("Mesh").Translate(new Vector3(0f, -hopSpeed, 0f));
            if (this.transform.Find("Mesh").position.y <= 0f) {
                goingUp = true;
            }
        }
    }

    void TurnAnimation () {
        if (turningRight) {
            this.transform.Find("Mesh").Rotate(new Vector3(0f, turnSpeed, 0f));
            if (this.transform.Find("Mesh").rotation.y >= turnMaxAngle) {
                turningRight = false;
            }
        } else {
            this.transform.Find("Mesh").Rotate(new Vector3(0f, -turnSpeed, 0f));
            if (this.transform.Find("Mesh").rotation.y <= -turnMaxAngle) {
                turningRight = true;
            }
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
                Env.Slots currentSlot = other.GetComponent<Tile>().slot;

                if (currentSlot == Env.Slots.Coins) {
                    Env.Instance.Coins += Env.CoinsAmount;
                } else if (currentSlot == Env.Slots.Slime) {
                    Env.Instance.PrincessHealth -= Env.SlimeDamage;
                } else if (currentSlot == Env.Slots.Sword) {
                    Env.Instance.PrincessAttack -= Env.SwordDamageUpgrade;
                    Env.Instance.PrincessEquipLeft = Env.Equips.IronSword;
                } else if (currentSlot == Env.Slots.Shield) {
                    Env.Instance.PrincessMaxHealth += Env.ShieldDefenseUpgrade;
                    Env.Instance.PrincessEquipLeft = Env.Equips.IronShield;
                }

                other.GetComponent<Tile>().slot = Env.Slots.Empty;
                other.GetComponent<Tile>().GetComponent<Slot>().CheckSlot();
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
            HopAnimation();
            // TurnAnimation();

            if (heading == Env.Paths.North) {
                this.transform.Translate((new Vector3(Env.PrincessSpeed, 0f, 0f)));
                this.transform.Find("Mesh").rotation = Quaternion.Euler(0, -90, 0);
            } else if (heading == Env.Paths.South) {
                this.transform.Translate((new Vector3(-Env.PrincessSpeed, 0f, 0f)));
                this.transform.Find("Mesh").rotation = Quaternion.Euler(0, 90, 0);
            } else if (heading == Env.Paths.West) {
                this.transform.Translate((new Vector3(0f, 0f, Env.PrincessSpeed)));
                this.transform.Find("Mesh").rotation = Quaternion.Euler(0, 180, 0);
            } else if (heading == Env.Paths.East) {
                this.transform.Translate((new Vector3(0f, 0f, -Env.PrincessSpeed)));
                this.transform.Find("Mesh").rotation = Quaternion.Euler(0, -180, 0);
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
