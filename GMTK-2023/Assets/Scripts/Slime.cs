using UnityEngine;
using TMPro;

public class Slime : MonoBehaviour
{
    public int Health = 1;
    public int Attack = 1;
    public int Defense = 0;
    public int XPGain = 10;

    private bool goingUp = true;
    private float speed = 0.005f;
    private float height = 0;
    private float maxHeight = 0.3f;
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

        this.transform.Find("SlimeStatus").transform.Find("Health").transform.Find("HealthValue").GetComponent<TMP_Text>().text = Health.ToString();
    }
}
