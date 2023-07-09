using UnityEngine;

public class Path : MonoBehaviour
{
    private void Reset () {
        for (int i = 0; i < this.transform.childCount; i++) {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void ShowPath (Env.Paths path) {
        this.transform.Find(path.ToString()).gameObject.SetActive(true);
        this.transform.Find("Center").gameObject.SetActive(true);
    }

    public void CheckPaths () {
        Env.Paths tileEntry = this.transform.GetComponentInParent<Tile>().entry;
        Env.Paths tileExit = this.transform.GetComponentInParent<Tile>().exit;

        Reset();

        if (tileEntry != Env.Paths.Empty) {
            ShowPath(tileEntry);
        }

        if (tileExit != Env.Paths.Empty) {
            ShowPath(tileExit);
        }
    }
    
    void Start()
    {
        CheckPaths();
    }
}
