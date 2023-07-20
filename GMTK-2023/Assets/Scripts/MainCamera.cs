using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Color easyColor = new Color(8f/255f, 69f/255f, 115f/255f);
    private Color mediumColor = new Color(86f/255f, 8f/255f, 115f/255f);
    private Color hardColor = new Color(9f/255f, 115f/255f, 66f/255f);

    void Start () {
        this.transform.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
    }

    void Update () {
        if (Env.Instance.Level < 3) {
            this.transform.GetComponent<Camera>().backgroundColor = easyColor;
        } else if (Env.Instance.Level < 5) {
            this.transform.GetComponent<Camera>().backgroundColor = mediumColor;
        } else {
            this.transform.GetComponent<Camera>().backgroundColor = hardColor;
        }
    }
}
