using UnityEngine;

public class Princess : MonoBehaviour
{
    public Env.Paths heading = Env.Paths.North;
    public bool walking = true;
    private bool enteringTile = false;
    private bool exitInverted = false;
    private float tileTraversed = 0f;
    private Env.Paths currentTileEntryDirection = Env.Paths.Empty;
    private Env.Paths currentTileExitDirection = Env.Paths.Empty;
    private bool exitedTile = false;
    private float traversedAfterExitingTile = 0f;
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
            if (exitInverted && currentTileEntryDirection != Env.Paths.Empty) {
                heading = currentTileEntryDirection;
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

    void OnTriggerEnter (Collider other)
    {
        if (other) {
            if (other.GetComponent<Tile>()) {
                if (other.GetComponent<Tile>().entry != Env.Paths.Empty && other.GetComponent<Tile>().exit != Env.Paths.Empty) {
                    enteringTile = true;
                    currentTileEntryDirection = other.GetComponent<Tile>().entry;
                    currentTileExitDirection = other.GetComponent<Tile>().exit;
                    CheckTileEntry(other);
                }
                if (other.GetComponent<Tile>().slot != Env.Slots.Empty) {
                    Env.Slots currentSlot = other.GetComponent<Tile>().slot;

                    if (currentSlot == Env.Slots.Coins) {
                        Env.Instance.Coins += Env.CoinsAmount;
                        other.GetComponent<Tile>().CoinPickup();
                    } else if (currentSlot == Env.Slots.Potion) {
                        other.GetComponent<Tile>().StatusPickup();
                        if (Env.Instance.PrincessHealth + Env.PotionHealingAmount <= Env.Instance.PrincessMaxHealth) {
                            Env.Instance.PrincessHealth += Env.PotionHealingAmount;
                        }
                    } else if (currentSlot == Env.Slots.Sword) {
                        other.GetComponent<Tile>().StatusPickup();
                        Env.Instance.PrincessAttack += Env.SwordDamageUpgrade;
                        Env.Instance.PrincessEquipLeft = Env.Equips.IronSword;
                    } else if (currentSlot == Env.Slots.Shield) {
                        other.GetComponent<Tile>().StatusPickup();
                        Env.Instance.PrincessDefense += Env.ShieldDefenseUpgrade;
                        Env.Instance.PrincessEquipLeft = Env.Equips.IronShield;
                    } else if (currentSlot == Env.Slots.Slime) {
                        if (Env.Instance.PrincessHealth - (Env.SlimeAttack - Env.Instance.PrincessDefense) <= 0) {
                            Env.Instance.ResetLevel();
                        } else {
                            Env.Instance.PrincessHealth -= (Env.SlimeAttack - Env.Instance.PrincessDefense);
                            Env.Instance.PrincessXP += Env.SlimeXPGain;
                            Env.Instance.CheckPrincessLevel();
                            other.GetComponent<Tile>().Explode();
                        }
                    }

                    other.GetComponent<Tile>().EmptySlot();
                }
            }
            if (other.tag == "Gate") {
                SendPrincessToStart();
                Env.Instance.GoToNextLevel();
            }
            exitedTile = false;
            traversedAfterExitingTile = 0f;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other) {
            if (other.tag == "Tile") {
                exitedTile = true;
            }
        }
    }

    void OnTriggerStay (Collider other) {
        if (!walking && other) {
            CheckTileEntry(other);
        }
    }

    public void SendPrincessToStart () {
        this.transform.position = Env.Instance.PrincessStartingPosition;
        walking = true;
        heading = Env.Paths.North;
    }

    void Start () {
        SendPrincessToStart();
    }

    void FixedUpdate()
    {
        if (walking) {
            HopAnimation();
            // TurnAnimation();

            if (heading == Env.Paths.North) {
                this.transform.Translate((new Vector3(Env.Instance.PrincessSpeed, 0f, 0f)));
                this.transform.Find("Mesh").rotation = Quaternion.Euler(0, -90, 0);
            } else if (heading == Env.Paths.South) {
                this.transform.Translate((new Vector3(-Env.Instance.PrincessSpeed, 0f, 0f)));
                this.transform.Find("Mesh").rotation = Quaternion.Euler(0, 90, 0);
            } else if (heading == Env.Paths.West) {
                this.transform.Translate((new Vector3(0f, 0f, Env.Instance.PrincessSpeed)));
                this.transform.Find("Mesh").rotation = Quaternion.Euler(0, 180, 0);
            } else if (heading == Env.Paths.East) {
                this.transform.Translate((new Vector3(0f, 0f, -Env.Instance.PrincessSpeed)));
                this.transform.Find("Mesh").rotation = Quaternion.Euler(0, -180, 0);
            }

            if (enteringTile) {
                tileTraversed += Env.Instance.PrincessSpeed;
            }

            if (tileTraversed >= Env.TileSize / 2) {
                enteringTile = false;
                tileTraversed = 0f;
                HeadToExit();
            }
        }
        if (exitedTile) {
            traversedAfterExitingTile += Env.Instance.PrincessSpeed;
            if (traversedAfterExitingTile > Env.TileSize + (Env.TileGridGap * 2)) {
                Env.Instance.ResetLevel();
            }
        }
    }
}
