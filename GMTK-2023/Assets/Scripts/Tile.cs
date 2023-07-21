using UnityEngine;

public class Tile : MonoBehaviour
{
    public Env.Paths entry = Env.Paths.Empty;
    public Env.Paths exit = Env.Paths.Empty;
    public Env.Slots slot = Env.Slots.Empty;

    public bool locked = false;
    public bool pathLocked = false;

    private void RandomizeRotation () {
        int rand = Random.Range(0, 4);
        this.transform.Find("Mesh").gameObject.transform.Rotate(new Vector3(0, rand * 90, 0), Space.World);
    }

    public void EmptySlot () {
        slot = Env.Slots.Empty;
        this.transform.Find("Slot").gameObject.GetComponent<Slot>().Reset();
    }

    public void Explode () {
        this.transform.Find("Explosion").GetComponent<ParticleSystem>().Play();
    }

    public void CoinPickup () {
        this.transform.Find("CoinPickup").GetComponent<ParticleSystem>().Play();
    }
    
    public void StatusPickup () {
        this.transform.Find("StatusPickup").GetComponent<ParticleSystem>().Play();
    }

    public void HealthPickup () {
        this.transform.Find("HealthPickup").GetComponent<ParticleSystem>().Play();
    }

    void OnMouseDown () {
        if (!locked && !pathLocked && Env.Instance.pathEntrySelection != Env.Paths.Empty && Env.Instance.pathExitSelection != Env.Paths.Empty && Env.Instance.Coins >= Env.PathBuildCost && (entry != Env.Instance.pathEntrySelection || exit != Env.Instance.pathExitSelection)) {
            entry = Env.Instance.pathEntrySelection;
            exit = Env.Instance.pathExitSelection;
            Env.Instance.Coins -= Env.PathBuildCost;
            this.transform.Find("Path").gameObject.GetComponent<Path>().CheckPaths();
        }
        if (!locked && slot == Env.Slots.Empty && Env.Instance.itemSelection != Env.Slots.Empty && Env.Instance.Coins >= Env.Instance.itemSelectionCost) {
            slot = Env.Instance.itemSelection;
            Env.Instance.Coins -= Env.Instance.itemSelectionCost;
            this.transform.Find("Slot").gameObject.GetComponent<Slot>().CheckSlot();
        }
    }

    void OnMouseEnter () {
        this.transform.Find("Hover").gameObject.SetActive(true);
    }

    void OnMouseExit () {
        this.transform.Find("Hover").gameObject.SetActive(false);
    }

    void Start () {
        this.transform.Find("Hover").gameObject.SetActive(false);
        RandomizeRotation();
    }
}
