using UnityEngine;

public class Princess : MonoBehaviour
{
    public Env.Paths heading = Env.Paths.North;

    void Start()
    {
        
    }

    void Update()
    {
        this.transform.Translate((new Vector3(Env.PrincessSpeed, 0f, 0f)));
    }
}
