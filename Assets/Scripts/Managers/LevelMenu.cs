using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    public Image[] lockIcons;

    public GameObject mapSelectButtons;

    private void Awake()
    {
        InitializeButton();
    }

    public void InitializeButton()
    {
        ButtonToArray();
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].enabled = false;
            lockIcons[i].enabled = true;
        }

        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].enabled = true;
            lockIcons[i].enabled = false;
        }
    }

    public void OpenLevel(int mapID)
    {
        if (mapID <= PlayerPrefs.GetInt("UnlockedLevel"))
        {
            SceneController.Instance.LoadMap(mapID);
        }
    }

    public void ResetGameProgress()
    {
        LevelManager.Instance.ResetUnlockedMap();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ButtonToArray()
    {
        int childCount = mapSelectButtons.transform.childCount;
        buttons = new Button[childCount];
        lockIcons = new Image[childCount];
        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = mapSelectButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
            lockIcons[i] = mapSelectButtons.transform.GetChild(i).Find("LockIcon").GetComponent<Image>();
        }
    }
}
