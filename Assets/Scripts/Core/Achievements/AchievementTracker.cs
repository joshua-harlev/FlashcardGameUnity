using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * By: Ren Peng & Ryn Reid
 * Course Info: GAME 245-02
 * Overview:
 * AchievementTracker.cs | Achievement Tracker for UI
 */

public class AchievementTracker : MonoBehaviour
{
    public Text mathematician;
    public Text thatWasClose;
    public Text InForTheLongHaulI;
    public Text InForTheLongHaulII;
    public Text InForTheLongHaulIII;
    public Text APlusI;
    public Text APlusII;
    public Text APlusIII;
    public Text MathMaster;
    public Text DailyStreak;
    public Text huh;
    public Text[] Perfects;

    private string[] perfectIDs;
    private string[] perfectIcons;

    public Achievements Achievements;

    private void Start()
    {
        var map = AchievementSaves.LoadAchievements();
        mathematician.text = map["mathematician"].Progress + "/" + Achievements.NumberOfAchievements;
        
        if (map["thatwasclose"].IsUnlocked)
        {
            thatWasClose.text = "1/1";
        }
        
        InForTheLongHaulI.text = map["inforthelonghaul_1"].Progress + "/15";
        InForTheLongHaulII.text = map["inforthelonghaul_2"].Progress + "/30";
        InForTheLongHaulIII.text = map["inforthelonghaul_3"].Progress + "/60";
        APlusI.text = map["APlus1"].Progress + "/1";
        APlusII.text = map["APlus2"].Progress + "/10";
        APlusIII.text = map["APlus3"].Progress + "/30";

        
        if (map["mathmaster"].IsUnlocked)
        {
            MathMaster.text = "1/1";
        }
        
        DailyStreak.text = map["dailyStreak"].Progress + "/5";

        if (map["huh"].IsUnlocked)
        {
            huh.text = "1/1";
        }
        
        perfectIDs = new[] { "perfectadder", "perfectsubtractor", "perfectmultiplier", "perfectdivider", "perfectmath" };
        perfectIcons = new[] { "+", "−", "×", "÷", "?" };
        UpdatePerfects(map);

    }

    private void UpdatePerfects(Dictionary<string, AchievementProgress> achievementMap) {
        for (int i = 0; i < perfectIDs.Length; i++) {
            if (achievementMap[perfectIDs[i]].IsUnlocked) {
                Perfects[i].text = perfectIcons[i];
            }
        }
    }
}
