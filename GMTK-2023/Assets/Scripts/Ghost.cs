using UnityEngine;
using TMPro;

public class Ghost : MonoBehaviour
{
    public int Health = 3;
    public int Attack = 2;
    public int Defense = 1;
    public int XPGain = 20;
    
    private bool goingUp = true;
    private float speed = 0.002f;
    private float height = 0;
    private float maxHeight = 1;
    private float minHeight = 0;

    void Update()
    {
        if (goingUp) {
            this.transform.Find("Mesh").transform.Translate(new Vector3(0, speed, 0));
            height += speed;
            if (height >= maxHeight) {
                goingUp = false;
            }
        } else {
            this.transform.Find("Mesh").transform.Translate(new Vector3(0, -speed, 0));
            height -= speed;
            if (height <= minHeight) {
                goingUp = true;
            }
        }

        this.transform.Find("GhostStatus").transform.Find("Health").transform.Find("HealthValue").GetComponent<TMP_Text>().text = Health.ToString();
    }
}
