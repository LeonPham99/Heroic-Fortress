using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : SingleTon<AchievementManager>
{
    public static Action<Achievement> OnAchievementUnlock;
    public static Action<Achievement> OnProgressUpdated;

    [SerializeField] private AchievementCard achievementCardPrefab;
    [SerializeField] private Transform achievementPanelContainer;
    [SerializeField] private Achievement[] achievements;

    private void Start()
    {
        foreach (Achievement achievement in achievements)
        {
            achievement.ResetAchievement();
        }

        LoadAchievements();
    }

    private void LoadAchievements()
    {
        for (int i = 0; i < achievements.Length; i++)
        {
            AchievementCard card = Instantiate(achievementCardPrefab, achievementPanelContainer);
            card.SetupAchievement(achievements[i]);
        }
    }

    public void AddProgress(string achievementID, int amount)
    {
        Achievement achievementWanted = AchivementExisted(achievementID);
        if (achievementWanted != null)
        {
            achievementWanted.AddProgress(amount);
        }
    }

    private Achievement AchivementExisted(string achivementID)
    {
        for (int i = 0;i < achievements.Length;i++)
        {
            if (achievements[i].ID == achivementID)
            {
                return achievements[i];
            }
        }
        return null;
    }
}
