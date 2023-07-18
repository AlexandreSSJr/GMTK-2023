using UnityEngine;

public class Floating : MonoBehaviour
{
    private bool goingUp = true;
    private float speed = 0.002f;
    private float height = 0;
    private float maxHeight = 1;
    private float minHeight = 0;

    void Update()
    {
        if (goingUp) {
            this.transform.Translate(new Vector3(0, speed, 0));
            height += speed;
            if (height >= maxHeight) {
                goingUp = false;
            }
        } else {
            this.transform.Translate(new Vector3(0, -speed, 0));
            height -= speed;
            if (height <= minHeight) {
                goingUp = true;
            }
        }
    }
}
