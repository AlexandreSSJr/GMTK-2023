using UnityEngine;

public class Slot : MonoBehaviour
{
    public Env.Slots type
    {
        get{
            return type;
        }
        set{
            type = value;
            CheckType();
        }
    }

    private void Reset () {
        int i = 0;
        while (true){
            if (this.transform.GetChild(i)==null) {
                break;
            } else {
                this.transform.GetChild(i).gameObject.SetActive(false);
            }
            i++;
        }
    }

    private void SetPotion () {
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void SetChest () {
        this.transform.GetChild(1).gameObject.SetActive(true);
    }

    private void SetCoins () {
        this.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void CheckType () {
        if (type != Env.Slots.Empty) {
            Reset();
            if (type == Env.Slots.Potion) {
                SetPotion();
            } else if (type == Env.Slots.Chest) {
                SetChest();
            } else if (type == Env.Slots.Coins) {
                SetCoins();
            }
        }
    }
    
    void Start()
    {
        Reset();
        CheckType();
    }

    void Update()
    {

    }
}
