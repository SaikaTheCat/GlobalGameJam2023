using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScren : MonoBehaviour
{
    public Button winButton;
    // Start is called before the first frame update
    void Start()
    {
        winButton.onClick.AddListener(() => { SceneManager.LoadScene(0); });
    }


}
