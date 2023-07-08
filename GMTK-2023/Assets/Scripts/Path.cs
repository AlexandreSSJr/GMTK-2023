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

    private void SetStraight () {
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void SetLeft () {
        this.transform.GetChild(1).gameObject.SetActive(true);
    }

    private void SetRight () {
        this.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void CheckType () {
        if (type != Env.Paths.Empty) {
            Reset();
            if (type == Env.Paths.Straight) {
                SetStraight();
            } else if (type == Env.Paths.Left) {
                SetLeft();
            } else if (type == Env.Paths.Right) {
                SetRight();
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
