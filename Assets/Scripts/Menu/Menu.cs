using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject levelButtonsContainer;


    GameParametrs gameParametrs;

    private void Start()
    {
        ActivateButtuns();
    }

    void ActivateButtuns()
    {
        gameParametrs = SaveController.LoadGameParametrs();
        LevelParametrs levelParametrs;

        for (int i = 1; i < gameParametrs.levelCount; i++)
        {
            levelParametrs = SaveController.LoadLevelParametrs(i);

            levelButtonsContainer.transform.GetChild(i).GetComponent<Button>().interactable = levelParametrs.open;
        }
    }

    public void SetLevelIndex(int levelIndex)
    {
        SaveController.curLoadLevel = levelIndex;
    }

    public void Quit()
    {
        Application.Quit();
    }
}