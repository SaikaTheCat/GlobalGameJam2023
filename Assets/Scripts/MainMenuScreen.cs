	using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScreen : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<Button> selectedLevelsButtons;
    [SerializeField] private Button exitButton;
    void Start()
    {
        exitButton.onClick.AddListener(() => Application.Quit());
        int level = 1;
        selectedLevelsButtons.ForEach(buttom =>
        {
            var l = level;
            buttom.onClick.AddListener(
                () => SceneManager.LoadScene(l)
                );
            level++;
        });
    }


}
