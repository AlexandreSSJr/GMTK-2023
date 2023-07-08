using UnityEngine;

public class Path : MonoBehaviour
{
    public Env.Paths type
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

    private void SetNorth () {
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void SetSouth () {
        this.transform.GetChild(1).gameObject.SetActive(true);
    }

    private void SetWest () {
        this.transform.GetChild(2).gameObject.SetActive(true);
    }

    private void SetEast () {
        this.transform.GetChild(3).gameObject.SetActive(true);
    }

    public void CheckType () {
        if (type != Env.Paths.Empty) {
            Reset();
            if (type == Env.Paths.North) {
                SetNorth();
            }
            if (type == Env.Paths.South) {
                SetSouth();
            }
            if (type == Env.Paths.West) {
                SetWest();
            }
            if (type == Env.Paths.East) {
                SetEast();
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
