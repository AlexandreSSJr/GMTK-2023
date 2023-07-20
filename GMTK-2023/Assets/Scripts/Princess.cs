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
    private bool beingKickedToStart = false;
    private float kickSpeed = 0.5f;
    private float kickUpSpeed = 0.2f;
    private float beingKickedXMidPoint = 0f;

    private void CheckPathTraversal (Collider other) {
        if (other && other.GetComponent<Tile>() && other.GetComponent<Tile>().entry != Env.Paths.Empty && !beingKickedToStart) {
            float jumpDistance = 0;
            Env.Paths tileEntry = other.GetComponent<Tile>().entry;
            Env.Paths tileExit = other.GetComponent<Tile>().exit;

            if (heading == Env.Paths.North) {
                if (tileEntry == Env.Paths.South) {
                    this.transform.Translate((new Vector3(jumpDistance, 0f, 0f)));
                    exitInverted = false;
                } else if (tileExit == Env.Paths.South) {
                    this.transform.Translate((new Vector3(jumpDistance, 0f, 0f)));
                    exitInverted = true;
                } else if (tileEntry == Env.Paths.North && tileExit == Env.Paths.North) {
                    this.transform.Translate((new Vector3(jumpDistance, 0f, 0f)));
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

    private void CheckTileSlot (Collider other) {
        Env.Slots currentSlot = other.GetComponent<Tile>().slot;

        if (currentSlot == Env.Slots.Coins) {
            Env.Instance.Coins += Env.CoinsAmount;
            other.GetComponent<Tile>().CoinPickup();
            other.GetComponent<Tile>().EmptySlot();
        } else if (currentSlot == Env.Slots.Potion) {
            if (Env.Instance.PrincessHealth + Env.PotionHealingAmount <= Env.Instance.PrincessMaxHealth) {
                Env.Instance.PrincessHealth += Env.PotionHealingAmount;
                other.GetComponent<Tile>().HealthPickup();
                other.GetComponent<Tile>().EmptySlot();
            }
        } else if (currentSlot == Env.Slots.Sword) {
            other.GetComponent<Tile>().StatusPickup();
            Env.Instance.PrincessAttack += Env.SwordDamageUpgrade;
            Env.Instance.PrincessEquipLeft = Env.Equips.IronSword;
            other.GetComponent<Tile>().EmptySlot();
        } else if (currentSlot == Env.Slots.Shield) {
            other.GetComponent<Tile>().StatusPickup();
            Env.Instance.PrincessDefense += Env.ShieldDefenseUpgrade;
            Env.Instance.PrincessEquipLeft = Env.Equips.IronShield;
            other.GetComponent<Tile>().EmptySlot();
        } else if (currentSlot == Env.Slots.Slime) {
            Slime slime = other.GetComponent<Tile>().transform.Find("Slot").transform.Find("Slime").GetComponent<Slime>();
            if (Env.Instance.PrincessHealth - Mathf.Max(slime.Attack - Env.Instance.PrincessDefense, 0) <= 0) {
                Env.Instance.ResetLevel();
            } else {
                Env.Instance.PrincessHealth -= Mathf.Max(slime.Attack - Env.Instance.PrincessDefense, 0);
                slime.Health -= Env.Instance.PrincessAttack - slime.Defense;
                if (slime.Health <= 0) {
                    Env.Instance.PrincessXP += slime.XPGain;
                    Env.Instance.CheckPrincessLevel();
                    other.GetComponent<Tile>().Explode();
                    other.GetComponent<Tile>().EmptySlot();
                } else {
                    KickPrincessToStart();
                }
            }
        } else if (currentSlot == Env.Slots.Ghost) {
            Ghost ghost = other.GetComponent<Tile>().transform.Find("Slot").transform.Find("Ghost").GetComponent<Ghost>();
            if (Env.Instance.PrincessHealth - Mathf.Max(ghost.Attack - Env.Instance.PrincessDefense, 0) <= 0) {
                Env.Instance.ResetLevel();
            } else {
                Env.Instance.PrincessHealth -= Mathf.Max(ghost.Attack - Env.Instance.PrincessDefense, 0);
                ghost.Health -= Env.Instance.PrincessAttack - ghost.Defense;
                if (ghost.Health <= 0) {
                    Env.Instance.PrincessXP += ghost.XPGain;
                    Env.Instance.CheckPrincessLevel();
                    other.GetComponent<Tile>().Explode();
                    other.GetComponent<Tile>().EmptySlot();
                } else {
                    KickPrincessToStart();
                }
            }
        } else if (currentSlot == Env.Slots.Troll) {
            Troll troll = other.GetComponent<Tile>().transform.Find("Slot").transform.Find("Troll").GetComponent<Troll>();
            if (Env.Instance.PrincessHealth - Mathf.Max(troll.Attack - Env.Instance.PrincessDefense, 0) <= 0) {
                Env.Instance.ResetLevel();
            } else {
                Env.Instance.PrincessHealth -= Mathf.Max(troll.Attack - Env.Instance.PrincessDefense, 0);
                troll.Health -= Env.Instance.PrincessAttack - troll.Defense;
                if (troll.Health <= 0) {
                    Env.Instance.PrincessXP += troll.XPGain;
                    Env.Instance.CheckPrincessLevel();
                    other.GetComponent<Tile>().Explode();
                    other.GetComponent<Tile>().EmptySlot();
                } else {
                    KickPrincessToStart();
                }
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
            if (other.tag == "Tile" && other.GetComponent<Tile>()) {
                if (other.GetComponent<Tile>().entry != Env.Paths.Empty && other.GetComponent<Tile>().exit != Env.Paths.Empty) {
                    enteringTile = true;
                    currentTileEntryDirection = other.GetComponent<Tile>().entry;
                    currentTileExitDirection = other.GetComponent<Tile>().exit;
                    CheckPathTraversal(other);
                }
                if (other.GetComponent<Tile>().slot != Env.Slots.Empty) {
                    CheckTileSlot(other);
                }
            }
            if (other.tag == "Gate") {
                SendPrincessToStart();
                Env.Instance.GoToNextLevel();
            }
        }
        exitedTile = false;
        traversedAfterExitingTile = 0f;
    }

    void OnTriggerExit (Collider other)
    {
        if (other && other.tag == "Tile") {
            exitedTile = true;
        }
    }

    void OnTriggerStay (Collider other) {
        if (other) {
            if (other.tag == "Tile" && other.GetComponent<Tile>() && other.GetComponent<Tile>().slot != Env.Slots.Empty) {
                CheckTileSlot(other);
            }
            if (!walking) {
                CheckPathTraversal(other);
            }
        }
    }

    public void SendPrincessToStart () {
        beingKickedToStart = false;
        this.transform.position = Env.Instance.PrincessStartingPosition;
        walking = true;
        heading = Env.Paths.North;
    }

    public void KickPrincessToStart () {
        beingKickedToStart = true;
        beingKickedXMidPoint = (this.transform.position.x + Env.Instance.PrincessStartingPosition.x) / 2;
    }

    void Start () {
        SendPrincessToStart();
    }

    void FixedUpdate()
    {
        if (beingKickedToStart) {
            float xMove = 0f;
            float zMove = 0f;
            float yMove = 0f;

            if (this.transform.position.x > Env.Instance.PrincessStartingPosition.x) {
                xMove = Mathf.Min(kickSpeed, this.transform.position.x - Env.Instance.PrincessStartingPosition.x);
            }
            if (this.transform.position.z > Env.Instance.PrincessStartingPosition.z) {
                zMove = Mathf.Min(kickSpeed, this.transform.position.z - Env.Instance.PrincessStartingPosition.z);
            }

            if (this.transform.position.x > beingKickedXMidPoint) {
                yMove = kickUpSpeed;
            } else {
                if (this.transform.position.y > kickUpSpeed) {
                    yMove = -kickUpSpeed;
                }
            }

            if (xMove <= 0f && zMove <= 0f) {
                beingKickedToStart = false;
                this.transform.position = new Vector3(this.transform.position.x, Env.Instance.PrincessStartingPosition.y, this.transform.position.z);
                heading = Env.Paths.North;
            } else {
                this.transform.Translate(new Vector3(-xMove, yMove, -zMove));
            }
        } else {
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
}
