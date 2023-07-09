using UnityEngine;

public class Slot : MonoBehaviour
{
    public void Reset () {
        for (int i = 0; i < this.transform.childCount; i++) {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void ShowSlot (Env.Slots slot) {
        this.transform.Find(slot.ToString()).gameObject.SetActive(true);
    }

    public void CheckSlot () {
        Env.Slots tileSlot = this.transform.GetComponentInParent<Tile>().slot;

        if (tileSlot != Env.Slots.Empty) {
            ShowSlot(tileSlot);
        }
    }
    
    void Start()
    {
        Reset();
        CheckSlot();
    }
}
