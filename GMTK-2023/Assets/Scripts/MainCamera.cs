using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Color easyColor = new Color(8f/255f, 69f/255f, 115f/255f);
    private Color mediumColor = new Color(86f/255f, 8f/255f, 115f/255f);
    private Color hardColor = new Color(9f/255f, 115f/255f, 66f/255f);
    private Color bossColor = new Color(50f/255f, 7f/255f, 0f/255f);

    void Start () {
        this.transform.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
    }

    void Update () {
        if (Env.Instance.Level < 1) {
            this.transform.position = new Vector3(0, 0, 0);
            this.transform.GetComponent<Camera>().orthographicSize = 25;
            this.transform.GetComponent<Camera>().backgroundColor = easyColor;
        } else if (Env.Instance.Level < 3) {
            this.transform.position = new Vector3(-10, 5, 0);
            this.transform.GetComponent<Camera>().orthographicSize = 27;
            this.transform.GetComponent<Camera>().backgroundColor = easyColor;
        } else if (Env.Instance.Level < 5) {
            this.transform.position = new Vector3(5, 5, 0);
            this.transform.GetComponent<Camera>().backgroundColor = mediumColor;
        } else if (Env.Instance.Level < 8) {
            this.transform.position = new Vector3(0, 10, 0);
            this.transform.GetComponent<Camera>().orthographicSize = 29;
            this.transform.GetComponent<Camera>().backgroundColor = hardColor;
        } else {
            this.transform.position = new Vector3(10, 0, 0);
            this.transform.GetComponent<Camera>().orthographicSize = 31;
            this.transform.GetComponent<Camera>().backgroundColor = bossColor;
        }
    }
}
