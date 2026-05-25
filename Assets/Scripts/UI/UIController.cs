using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject anxietyUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            anxietyUI.SetActive(!anxietyUI.activeSelf);
        }
    }
}
