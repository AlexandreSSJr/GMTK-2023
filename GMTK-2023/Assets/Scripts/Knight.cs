using UnityEngine;
using TMPro;

public class Knight : MonoBehaviour
{
    public int Health = 30;
    public int Attack = 7;
    public int Defense = 5;
    public int XPGain = 100;

    private bool turningLeft = true;
    private float speed = 0.001f;
    private float turn = 0;
    private float maxTurn = 0.2f;
    private float minTurn = -0.2f;

    void Update()
    {
        if (turningLeft) {
            this.transform.Find("Mesh").transform.Rotate(new Vector3(0, speed, 0));
            turn += speed;
            if (turn >= maxTurn) {
                turningLeft = false;
            }
        } else {
            this.transform.Find("Mesh").transform.Rotate(new Vector3(0, -speed, 0));
            turn -= speed;
            if (turn <= minTurn) {
                turningLeft = true;
            }
        }

        this.transform.Find("KnightStatus").transform.Find("Health").transform.Find("HealthValue").GetComponent<TMP_Text>().text = Health.ToString();
    }
}
