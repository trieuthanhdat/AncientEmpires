using System;
using UnityEngine;

public class MWPlatformService : MonoBehaviour
{
    public static Action<bool, string> LoginResult;

    [SerializeField]
    private bool isAutoLogin = true;

    private static MWPlatformService instance;

    public static bool LoginState => Social.localUser.authenticated;

    public static string UserId => Social.localUser.id; //Guid.NewGuid().ToString(); // Test code, delete save data on each startup

    public static string UserName => Social.localUser.userName;

    public static void Init()
    {
        //PlayerPrefs.DeleteAll(); // Test code, delete save data on each startup
        if (instance.isAutoLogin)
        {
            Login(delegate (bool success, string errorMessage)
            {
                if (LoginResult != null)
                {
                    LoginResult(success, errorMessage);
                }
                Debug.Log("Platform Login!! :: " + success + ", errorMessage :: " + errorMessage);
            });
        }
    }

    public static void Login(Action<bool, string> onResult = null)
    {
        Social.localUser.Authenticate(onResult);
    }

    public static void ShowLeaderboard()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
    }

    public static void ShowAchievement()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
    }

    public static void ReportLeaderBoard(string leaderboardId, long score, Action<bool> onResult = null)
    {
        Social.ReportScore(score, leaderboardId, onResult);
    }

    public static void UnlockAchievement(string achievementId, int score, Action<bool> onResult = null)
    {
        Social.ReportProgress(achievementId, score, onResult);
    }

    public static string GetUniqueDeviceId()
    {
        return SystemInfo.deviceUniqueIdentifier;
    }

    private void Awake()
    {
        instance = this;
    }
}
