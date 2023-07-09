using UnityEngine;

public class Tile : MonoBehaviour
{
    public Env.Paths entry = Env.Paths.Empty;
    public Env.Paths exit = Env.Paths.Empty;
    public Env.Slots slot = Env.Slots.Empty;

    void RandomizeRotation () {
        int rand = Random.Range(0, 4);
        this.transform.Find("Mesh").gameObject.transform.Rotate(new Vector3(0, rand * 90, 0), Space.World);
    }

    void OnMouseDown () {
        if (Env.Instance.pathEntrySelection != Env.Paths.Empty) {
            entry = Env.Instance.pathEntrySelection;
            this.transform.Find("Path").gameObject.GetComponent<Path>().CheckPaths();
        }
        if (Env.Instance.pathExitSelection != Env.Paths.Empty) {
            exit = Env.Instance.pathExitSelection;
            this.transform.Find("Path").gameObject.GetComponent<Path>().CheckPaths();
        }
        if (slot == Env.Slots.Empty && Env.Instance.itemSelection != Env.Slots.Empty) {
            slot = Env.Instance.itemSelection;
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
