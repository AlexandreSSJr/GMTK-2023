using UnityEngine;
using UnityEngine.UIElements;

public class InGameUI : MonoBehaviour
{
    private void OnEnable()
    {
        // Getting the Root Element from the UIDocument
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button buttonStart = root.Q<Button>("nomedobutão");

    }
}
