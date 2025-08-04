

using BestHTTP;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Protocol_Set : GameObjectSingleton<Protocol_Set>
{
	public List<HTTPRequest> Reservation_Protocol = new List<HTTPRequest>();

	[SerializeField]
	private Text textUserInfo;

	[SerializeField]
	private GameObject goDebugButton;

	[SerializeField]
	private LoginFailPopup loginFailPopup;

	[SerializeField]
	private NetworkError networkError;

	[SerializeField]
	private UpdateNotice updateNotice;

	private HTTPRequest Sended_Protocol;

	private Action onCallBack_Default;

	private Action onCallBack_User_info;

	private Action onCallBack_User_Item_info;

	private Action<ChestListDbData[]> onCallBack_Chest;

	private Action<GAME_END_RESULT> OnCallBack_Game_End;

	private Action<string> OnCallBack_Floor_Upgrade;

	private Action<GAME_QUICK_LOOT> OnCallBack_Quick_Loot;

	private Action<GAME_LEVEL_REAMAIN_TIME_RESULT> OnCallBack_Level_Remain_Time;

	private Action<USER_DAILY_BONUS_RESULT> onCallBack_User_Daily_Bonus;

	private Action onCallBack_User_Daily_Bonus_Collect;

	private Action<ChestListDbData[]> OnCallBack_Buy_Package;

	private Action<SHOP_PACKAGE_LIST_RESULT> OnCallBack_Shop_Package_List;

	private Action<ARENA_INFO_DATA_RESULT> OnCallBack_Arena_Info_Data;

	private Action<ARENA_GAME_START_RESULT> OnCallBack_Arena_Game_Start_Data;

	private Action<ARENA_GAME_END_RESULT> OnCallBack_Arena_Game_End_Data;

	private Action<ARENA_STORE_INFO[]> OnCallBack_Arena_Store_Info_Data;

	private Action OnCallBack_Arena_Ticket;

	private Action<int> OnCallBack_Ad_Start;

	private Action OnCallBack_Tutorial;

	private Coroutine coroutineNetworkDelay;

	private List<HTTPRequest> listConnect = new();

	public static void CallSocialLoginConnect()
	{
		Inst.InitSocialUser();
	}

	public static void CallGuestLoginConnect()
	{
		Protocol_check_auth_Req();
	}

    #region 已处理

    public static void Old_Protocol_check_version_Req()
    {
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "check_version");
        var jsonObject2 = new JSONObject();
        switch (BuildSet.CurrentPlatformType)
        {
            case PlatformType.aos:
                jsonObject2.AddField("device", "aos");
                break;
            case PlatformType.ios:
                jsonObject2.AddField("device", "ios");
                break;
        }
        jsonObject2.AddField("version", BuildSet.AppVer);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("check_version = " + jsonObject);
        AnalyticsManager.FirebaseAnalyticsLogEvent(FBLog_Type.app_launch);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_check_version_Res);
    }

    public static void Protocol_check_auth_Req()
    {
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "check_auth_new");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("sType", Inst.GetPlatformType());
        jsonObject2.AddField("sUid", GameInfo.sUid);
        jsonObject2.AddField("deviceId", MWPlatformService.GetUniqueDeviceId());
        switch (BuildSet.CurrentPlatformType)
        {
            case PlatformType.aos:
                jsonObject2.AddField("device", "aos");
                break;
            case PlatformType.ios:
                jsonObject2.AddField("device", "ios");
                break;
        }
        jsonObject2.AddField("adId", GameInfo.AD_ID);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("check_auth = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_check_auth_Res);
    }

    public static void Old_Protocol_check_logout_Req()
    {
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "check_logout");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("check_logout = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, null, isLoading: false);
    }

    public static void Old_Protocol_user_get_tutorial_Req(Action onCallBack = null)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "user_get_tutorial");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("user_get_tutorial = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_user_get_tutorial_Res);
    }

    public static void Old_Protocol_user_set_tutorial_Req(int sIdx, int seq, Action onCallBack = null)
    {
        if (onCallBack != null)
        {
            Inst.OnCallBack_Tutorial = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "user_set_tutorial");
        var jsonObject2 = new JSONObject();
        var type = ((sIdx > 5) ? "e" : "m");
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("type", type);
        jsonObject2.AddField("sIndex", sIdx);
        jsonObject2.AddField("seq", seq);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("user_set_tutorial = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_user_set_tutorial_Res, isLoading: false);
    }

    #endregion






    #region OLD



    [FoldoutGroup("Old"), Button]
    public static void Protocol_user_info_Req(Action onCallBack = null, bool isLoading = true)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_User_info = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "user_info");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("user_info = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_user_info_Res, isLoading);
    }

    [FoldoutGroup("Old"), Button]
    public static void Protocol_user_item_info_Req(Action onCallBack = null, bool isLoading = true)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_User_Item_info = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "user_item_info");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("user_item_info = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_user_item_info_Res, isLoading);
    }

    public static void Protocol_user_daily_bonus_Req(Action<USER_DAILY_BONUS_RESULT> onCallBack = null, bool isLoading = true)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_User_Daily_Bonus = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "user_daily_bonus");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("user_daily_bonus = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_user_daily_bonus_Res, isLoading);
    }

    public static void Protocol_user_get_daily_bonus_Req(int dayCnt, Action onCallBack = null, bool isLoading = true)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_User_Daily_Bonus_Collect = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "user_get_daily_bonus");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("dayCnt", dayCnt);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("user_get_daily_bonus = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_user_get_daily_bonus_Res, isLoading);
    }

    public static void Protocol_user_chapter_open_Req(int stage, int chapter, Action onCallBack = null)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_User_info = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "user_chapter_open");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("stage", stage);
        jsonObject2.AddField("chapter", chapter);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("user_chapter_open = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_user_chapter_open_Res);
    }

    public static void Protocol_game_start_Req(int levelIdx, List<int> listBoostItem, Action onCallBack = null)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "game_start");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("levelIdx", levelIdx);
        var jsonObject3 = new JSONObject();
        if (listBoostItem is { Count: > 0 })
        {
            foreach (var item in listBoostItem)
            {
                jsonObject3.Add(item);
            }

            jsonObject2.AddField("boosterIdxArray", jsonObject3);
        }
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("game_start = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_game_start_Res, isLoading: true, isSceneLoading: true);
    }

    public static void Protocol_game_end_Req(int levelIdx, int gameKey, int result, int resultReason, int turns, int monsterChestKey, List<List<int>> monsterArray, int[] waveArray, Action<GAME_END_RESULT> onCallBack = null)
    {
        if (onCallBack != null)
        {
            Inst.OnCallBack_Game_End = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "game_end");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("levelIdx", levelIdx);
        jsonObject2.AddField("gameKey", gameKey);
        jsonObject2.AddField("result", result);
        jsonObject2.AddField("resultReason", resultReason);
        jsonObject2.AddField("turns", turns);
        jsonObject2.AddField("monsterChestKey", monsterChestKey);
        jsonObject2.AddField("continueCount", 0);
        var jsonObject3 = new JSONObject();
        for (int i = 0; i < monsterArray.Count; i++)
        {
            JSONObject jsonObject4;
            if (jsonObject3.Count < i + 1)
            {
                jsonObject4 = new JSONObject();
                jsonObject3.Add(jsonObject4);
            }
            else
            {
                jsonObject4 = jsonObject3[i];
            }
            for (int j = 0; j < monsterArray[i].Count; j++)
            {
                jsonObject4.Add(monsterArray[i][j]);
            }
        }
        jsonObject2.AddField("waveMonsterArray", jsonObject3);
        var jsonObject5 = new JSONObject();
        foreach (var item in waveArray)
        {
            jsonObject5.Add(item);
        }
        jsonObject2.AddField("waveArray", jsonObject5);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("game_end = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_game_end_Res, isLoading: true, resultReason == 2);
    }

    public static void Protocol_game_continue_Req(Action onCallBack = null)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "game_continue");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("gameKey", GameInfo.userPlayData.gameKey);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("game_continue = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_game_continue_Res);
    }

    public static void Protocol_game_quick_loot_Req(int levelIdx, int monsterChestKey, int adKey, Action<GAME_QUICK_LOOT> onCallBack = null)
    {
        if (onCallBack != null)
        {
            Inst.OnCallBack_Quick_Loot = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "game_quick_loot");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("levelIdx", levelIdx);
        jsonObject2.AddField("monsterChestKey", monsterChestKey);
        jsonObject2.AddField("adKey", adKey);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("game_quick_loot = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_game_quick_loot_Res);
    }

    public static void Protocol_game_quick_loot_speed_up_Req(int levelIdx, Action onCallBack = null)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "game_quick_loot_speed_up");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("levelIdx", levelIdx);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("game_quick_loot_speed_up = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_game_quick_loot_speed_up_Res);
    }

    public static void Protocol_game_chapter_collect_Req(int stage, int chapter, Action onCallBack = null)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "game_chapter_collect");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("stage", stage);
        jsonObject2.AddField("chapter", chapter);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("game_chapter_collect = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_game_chapter_collect_Res);
    }

    public static void Protocol_game_level_remain_time_Req(int levelIdx, Action<GAME_LEVEL_REAMAIN_TIME_RESULT> onCallBack = null)
    {
        MWLog.Log("Protocol_game_level_remain_time_Req");
        if (onCallBack != null)
        {
            Inst.OnCallBack_Level_Remain_Time = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "game_level_remain_time");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("levelIdx", levelIdx);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("game_level_remain_time = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_game_level_remain_time_Res);
    }

    public static void Protocol_chest_popup_buy_coin_Req(int needJewel, Action onCallBack)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "chest_popup_buy_coin");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("needJewel", needJewel);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("chest_popup_buy_coin = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_chest_popup_buy_coin_Res);
    }

    public static void Protocol_chest_collect_Req(int chestIdx, Action<ChestListDbData[]> onCallBack, string isFree = "n", bool isTutorial = false, int tutorialNo = 0)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Chest = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "chest_collect");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("chestIdx", chestIdx);
        jsonObject2.AddField("freeYn", isFree);
        if (isTutorial)
        {
            jsonObject2.AddField("tutorialYn", "y");
        }
        else
        {
            jsonObject2.AddField("tutorialYn", "n");
        }
        jsonObject2.AddField("tutorialNo", tutorialNo);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_chest_collect_Res);
    }

    public static void Protocol_chest_req_reward_Req(int probIdx, bool isAd, Action onCallBack = null)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "chest_req_reward");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("probIdx", probIdx);
        if (isAd)
        {
            jsonObject2.AddField("adYn", "y");
        }
        else
        {
            jsonObject2.AddField("adYn", "n");
        }
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_chest_req_reward_Res, isLoading: false);
    }

    public static void Protocol_hunter_change_Req(int[] useHunter, HUNTERLIST_TYPE listType, Action onCallBack)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "hunter_change");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        var jsonObject3 = new JSONObject();
        for (int i = 0; i < useHunter.Length; i++)
        {
            jsonObject3.Add(useHunter[i]);
        }
        jsonObject2.AddField("hunterIdxArray", jsonObject3);
        jsonObject2.AddField("type", (int)listType);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("hunter_change = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_hunter_change_Res);
    }

    public static void Protocol_hunter_is_not_new_Req(int hunterIdx, Action onCallBack)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "hunter_is_not_new");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("hunterIdx", hunterIdx);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_hunter_is_not_new_Res, isLoading: false);
    }

    public static void Protocol_hunter_level_up_Req(int hunterIdx, int targetLevel, Action onCallBack, bool isTutorial = false)
    {
        if (targetLevel < 0) throw new ArgumentOutOfRangeException(nameof(targetLevel));
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "hunter_level_up");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("hunterIdx", hunterIdx);
        jsonObject2.AddField("level", targetLevel);
        if (isTutorial)
        {
            jsonObject2.AddField("tutorialYn", "y");
        }
        else
        {
            jsonObject2.AddField("tutorialYn", "n");
        }
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_hunter_level_up_Res);
    }

    public static void Protocol_hunter_promotion_Req(int hunterIdx, Action onCallBack)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "hunter_promotion");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("hunterIdx", hunterIdx);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_hunter_promotion_Res);
    }

    public static void Protocol_store_collect_Req(int stage, int storeIdx, Action onCallBack)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "store_collect");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("stage", stage);
        jsonObject2.AddField("storeIdx", storeIdx);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("store_collect = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_store_collect_Res);
    }

    public static void Protocol_store_open_Req(int stageIdx, int storeIdx, Action onCallBack)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "store_open");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("stage", stageIdx);
        jsonObject2.AddField("storeIdx", storeIdx);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_store_open_Res);
    }

    public static void Protocol_store_speed_up_Req(int stage, int storeIdx, int jewel, Action onCallBack)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "store_speed_up");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("stage", stage);
        jsonObject2.AddField("storeIdx", storeIdx);
        jsonObject2.AddField("jewel", jewel);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_store_speed_up_Res);
    }

    public static void Protocol_store_upgrade_Req(int storeIdx, int storeTier, Action<string> onCallBack)
    {
        if (onCallBack != null)
        {
            Inst.OnCallBack_Floor_Upgrade = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "store_upgrade");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("storeIdx", storeIdx);
        jsonObject2.AddField("storeTier", storeTier);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("store_upgrade = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_store_upgrade_Res);
    }

    public static void Protocol_shop_list_Req(Action onCallBack)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "shop_list");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("shop_list = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_shop_list_Res);
    }

    public static void Protocol_shop_buy_daily_Req(int productIdx, int buyCount, Action onCallBack)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "shop_buy_daily");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("productIdx", productIdx);
        jsonObject2.AddField("buyCount", buyCount);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("shop_buy_daily = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_shop_buy_daily_Res);
    }

    public static void Protocol_shop_buy_coin_Req(int productIdx, Action onCallBack)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "shop_buy_coin");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("productIdx", productIdx);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("shop_buy_coin = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_shop_buy_coin_Res);
    }

    public static void Protocol_shop_buy_jewel_Req(int productIdx, Action onCallBack)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "shop_buy_jewel");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("productIdx", productIdx);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("shop_buy_jewel = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_shop_buy_jewel_Res);
    }

    public static void Protocol_shop_popup_hunter_buy_coin_Req(int hunterIdx, int targetLevel, int needJewel, Action onCallBack)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "shop_popup_hunter_buy_coin");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("hunterIdx", hunterIdx);
        jsonObject2.AddField("targetLevel", targetLevel);
        jsonObject2.AddField("needJewel", needJewel);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("shop_popup_hunter_buy_coin = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_shop_popup_hunter_buy_coin_Res);
    }

    public static void Protocol_shop_popup_hunter_promotion_buy_coin_Req(int hunterIdx, int needJewel, Action onCallBack)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "shop_popup_hunter_promotion_buy_coin");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("hunterIdx", hunterIdx);
        jsonObject2.AddField("needJewel", needJewel);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("shop_popup_hunter_promotion_buy_coin = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_shop_popup_hunter_promotion_buy_coin_Res);
    }

    public static void Protocol_shop_popup_store_buy_coin_Req(int storeIdx, int needJewel, Action onCallBack)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "shop_popup_store_buy_coin");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("storeIdx", storeIdx);
        jsonObject2.AddField("needJewel", needJewel);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("shop_popup_store_buy_coin = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_shop_popup_store_buy_coin_Res);
    }

    public static void Protocol_shop_package_list_Req(Action<SHOP_PACKAGE_LIST_RESULT> onCallBack)
    {
        if (onCallBack != null)
        {
            Inst.OnCallBack_Shop_Package_List = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "shop_package_list");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("shop_package_list = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_shop_package_list_Res);
    }

    public static void Protocol_shop_ad_energy_Req(int energyCount, Action onCallBack = null)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "shop_ad_energy");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("energyCount", energyCount);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("shop_ad_energy = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_shop_ad_energy_Res);
    }

    public static void Protocol_shop_ad_energy_start_Req()
    {
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "shop_ad_energy_start");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("shop_ad_energy_start = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, null, isLoading: false);
    }

    public static void Protocol_shop_buy_energy_Req(Action onCallBack)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "shop_buy_energy");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("shop_buy_energy = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_shop_buy_energy_Res);
    }

    public static void Protocol_user_default_info_Req(Action onCallBack = null, bool isLoading = true)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "user_default_info");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("user_default_info = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_user_default_info_Res, isLoading);
    }

    public static void Protocol_shop_buy_product_Req(int productId, string signature, string signedData, Action onCallBack = null)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "shop_buy_product");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        switch (BuildSet.CurrentPlatformType)
        {
            case PlatformType.aos:
                jsonObject2.AddField("device", "aos");
                break;
            case PlatformType.ios:
                jsonObject2.AddField("device", "ios");
                break;
        }
        jsonObject2.AddField("purchaseType", "jewel");
        jsonObject2.AddField("productIdx", productId);
        jsonObject2.AddField("signature", signature);
        jsonObject2.AddField("signedData", signedData);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("shop_buy_product = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_shop_buy_product_Res);
    }

    public static void Protocol_shop_buy_package_Req(int productType, string signature, string signedData, Action<ChestListDbData[]> onCallBack = null)
    {
        if (onCallBack != null)
        {
            Inst.OnCallBack_Buy_Package = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "shop_buy_package");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        switch (BuildSet.CurrentPlatformType)
        {
            case PlatformType.aos:
                jsonObject2.AddField("device", "aos");
                break;
            case PlatformType.ios:
                jsonObject2.AddField("device", "ios");
                break;
        }
        jsonObject2.AddField("purchaseType", "package");
        jsonObject2.AddField("productType", productType);
        jsonObject2.AddField("signature", signature);
        jsonObject2.AddField("signedData", signedData);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("shop_buy_package = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_shop_buy_package_Res);
    }

    public static void Protocol_shop_ad_start_Req(int adType, Action<int> onCallBack = null)
    {
        if (onCallBack != null)
        {
            Inst.OnCallBack_Ad_Start = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "shop_ad_start");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("adType", adType);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("shop_ad_start = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_shop_ad_start_Res);
    }

    public static void Protocol_chest_ad_start_Req(Action onCallBack = null)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "chest_ad_start");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("chest_ad_start = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_chest_ad_start_Res, isLoading: false);
    }

    public static void Protocol_arena_info_Req(Action<ARENA_INFO_DATA_RESULT> onCallBack = null)
    {
        if (onCallBack != null)
        {
            Inst.OnCallBack_Arena_Info_Data = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "arena_info");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("arena_info = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_arena_info_Res);
    }

    public static void Protocol_arena_game_start_Req(int levelIdx, List<int> listBoostItem, Action<ARENA_GAME_START_RESULT> onCallBack = null)
    {
        if (onCallBack != null)
        {
            Inst.OnCallBack_Arena_Game_Start_Data = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "arena_game_start");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("levelIdx", levelIdx);
        var jsonObject3 = new JSONObject();
        if (listBoostItem is { Count: > 0 })
        {
            foreach (var item in listBoostItem)
            {
                jsonObject3.Add(item);
            }

            jsonObject2.AddField("boosterIdxArray", jsonObject3);
        }
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("arena_game_start = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_arena_game_start_Res, isLoading: true, isSceneLoading: true);
    }

    public static void Protocol_arena_game_end_Req(int levelIdx, int gameKey, int result, int resultReason, int wave, Action<ARENA_GAME_END_RESULT> onCallBack = null)
    {
        if (onCallBack != null)
        {
            Inst.OnCallBack_Arena_Game_End_Data = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "arena_game_end");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("levelIdx", levelIdx);
        jsonObject2.AddField("gameKey", gameKey);
        jsonObject2.AddField("result", result);
        jsonObject2.AddField("resultReason", resultReason);
        jsonObject2.AddField("wave", wave);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("arena_game_end = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_arena_game_end_Res, isLoading: true, resultReason == 2);
    }

    public static void Protocol_arena_game_continue_Req(Action onCallBack = null)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "arena_game_continue");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("gameKey", GameInfo.userPlayData.gameKey);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("arena_game_continue = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_arena_game_continue_Res);
    }

    public static void Protocol_arena_buy_ticket_Req(Action onCallBack = null)
    {
        if (onCallBack != null)
        {
            Inst.OnCallBack_Arena_Ticket = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "arena_buy_ticket");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("arena_buy_ticket = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_arena_buy_ticket_Res);
    }

    public static void Protocol_arena_store_list_Req(Action<ARENA_STORE_INFO[]> onCallBack = null)
    {
        if (onCallBack != null)
        {
            Inst.OnCallBack_Arena_Store_Info_Data = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "arena_store_list");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("arena_store_list = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_arena_store_list_Res);
    }

    public static void Protocol_arena_store_buy_product_Req(int productIdx, Action onCallBack = null)
    {
        if (onCallBack != null)
        {
            Inst.onCallBack_Default = onCallBack;
        }
        var jsonObject = new JSONObject();
        jsonObject.AddField("cmd", "arena_store_buy_product");
        var jsonObject2 = new JSONObject();
        jsonObject2.AddField("caUid", GameInfo.caUid);
        jsonObject2.AddField("productIdx", productIdx);
        jsonObject.AddField("params", jsonObject2);
        var dictionary = new Dictionary<string, string> { { "commands", jsonObject.ToString() } };
        MWLog.Log("arena_store_buy_product = " + jsonObject);
        Inst.CallforByBestHTTP(dictionary, Inst.Protocol_arena_store_buy_product_Res);
    }

    #endregion





    public static void Send_Remain_Protocol()
	{
		Inst.Send_Reservation_Protocol();
	}

	private void Protocol_check_version_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("check_version : " + response.DataAsText);
			CHECK_VERSION[] array = JsonConvert.DeserializeObject<CHECK_VERSION[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode))
			{
				if (array[0].result.force_update == 2)
				{
					InitSocialUser();
				}
				else
				{
					updateNotice.Show(array[0].result);
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_check_auth_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("check_auth : " + response.DataAsText);
            CHECK_AUTH[] array = JsonConvert.DeserializeObject<CHECK_AUTH[]>(response.DataAsText);
            if (!CheckServerErrorCode(array[0].errorCode))
            {
                Debug.LogError($"CaUid: {array[0].result.caUid}");
                GameInfo.caUid = array[0].result.caUid;
                Protocol_user_get_tutorial_Req();
            }
        }
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_user_get_tutorial_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("user_get_tutorial : " + response.DataAsText);
			USER_GET_TUTORIAL_DATA[] array = JsonConvert.DeserializeObject<USER_GET_TUTORIAL_DATA[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				MWLog.Log("Protocol_user_get_tutorial_Res :: " + array.Length);
				MWLog.Log("Protocol_user_get_tutorial_Res ::: " + array[0].result);
				TutorialManager.SetTutorialData(array[0].result);
				GameDataManager.StartGame();
				if (onCallBack_Default != null)
				{
					onCallBack_Default();
					onCallBack_Default = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_user_set_tutorial_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("user_set_tutorial : " + response.DataAsText);
		}
		else
		{
			MWLog.Log("error = " + num);
		}
		if (num == 0 && OnCallBack_Tutorial != null)
		{
			OnCallBack_Tutorial();
			OnCallBack_Tutorial = null;
		}
	}

	private void Protocol_user_info_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("user_info : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				GameDataManager.UpdateUserData();
				if (Inst.onCallBack_User_info != null)
				{
					Inst.onCallBack_User_info();
					Inst.onCallBack_User_info = null;
				}
				LobbyManager.CheckHunterAlert();
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_user_item_info_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("user_item_info : " + response.DataAsText);
			USER_ITEM_INFO[] array = JsonConvert.DeserializeObject<USER_ITEM_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result.userItemInfo);
				GameDataManager.UpdateUserData();
				if (Inst.onCallBack_User_Item_info != null)
				{
					Inst.onCallBack_User_Item_info();
					Inst.onCallBack_User_Item_info = null;
				}
				LobbyManager.CheckHunterAlert();
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_user_daily_bonus_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("user_daily_bonus : " + response.DataAsText);
            USER_DAILY_BONUS[] array = JsonConvert.DeserializeObject<USER_DAILY_BONUS[]>(response.DataAsText);
            if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update) && Inst.onCallBack_User_Daily_Bonus != null)
            {
                Inst.onCallBack_User_Daily_Bonus(array[0].result);
                Inst.onCallBack_User_Daily_Bonus = null;
            }
        }
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_user_get_daily_bonus_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("user_get_daily_bonus : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				GameDataManager.UpdateUserData();
				if (Inst.onCallBack_User_Daily_Bonus_Collect != null)
				{
					Inst.onCallBack_User_Daily_Bonus_Collect();
					Inst.onCallBack_User_Daily_Bonus_Collect = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_user_chapter_open_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("user_chapter_open : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				GameDataManager.UpdateUserData();
				if (Inst.onCallBack_User_info != null)
				{
					Inst.onCallBack_User_info();
					Inst.onCallBack_User_info = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_game_start_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("game_start : " + response.DataAsText);
			GAME_START[] array = JsonConvert.DeserializeObject<GAME_START[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				GameInfo.userData.userInfo = array[0].result.userInfo;
				GameInfo.userPlayData.gameKey = array[0].result.gameKey;
				GameDataManager.UpdateUserData();
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_game_end_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("game_end : " + response.DataAsText);
            GAME_END[] array = JsonConvert.DeserializeObject<GAME_END[]>(response.DataAsText);
            if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update) && Inst.OnCallBack_Game_End != null)
            {
                Inst.OnCallBack_Game_End(array[0].result);
                Inst.OnCallBack_Game_End = null;
            }
        }
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_game_continue_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("game_continue : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				GameDataManager.UpdateUserData();
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_game_quick_loot_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("game_quick_loot : " + response.DataAsText);
            GAME_QUICK_LOOT[] array = JsonConvert.DeserializeObject<GAME_QUICK_LOOT[]>(response.DataAsText);
            if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update) && Inst.OnCallBack_Quick_Loot != null)
            {
                Inst.OnCallBack_Quick_Loot(array[0]);
                Inst.OnCallBack_Quick_Loot = null;
            }
        }
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_game_quick_loot_speed_up_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("game_quick_loot_speed_up : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				GameDataManager.UpdateUserData();
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_game_chapter_collect_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("game_chapter_collect : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_game_level_remain_time_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("game_level_remain_time : " + response.DataAsText);
            GAME_LEVEL_REAMAIN_TIME[] array = JsonConvert.DeserializeObject<GAME_LEVEL_REAMAIN_TIME[]>(response.DataAsText);
            if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update) && Inst.OnCallBack_Level_Remain_Time != null)
            {
                Inst.OnCallBack_Level_Remain_Time(array[0].result);
                Inst.OnCallBack_Level_Remain_Time = null;
            }
        }
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_chest_collect_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("chest_collect : " + response.DataAsText);
            CHEST_COLLECT[] array = JsonConvert.DeserializeObject<CHEST_COLLECT[]>(response.DataAsText);
            if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update) && Inst.onCallBack_Chest != null)
            {
                Inst.onCallBack_Chest(array[0].result.rewardList);
                Inst.onCallBack_Chest = null;
            }
        }
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_chest_req_reward_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("chest_req_reward : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				GameDataManager.UpdateUserData();
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_hunter_change_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("hunter_change : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				GameDataManager.UpdateUserData();
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_hunter_is_not_new_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("hunter_is_not_new : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				GameDataManager.UpdateUserData();
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_hunter_level_up_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("hunter_level_up : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				GameDataManager.UpdateUserData();
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_hunter_promotion_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("hunter_promotion : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				GameDataManager.UpdateUserData();
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_store_open_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("store_open : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				GameDataManager.UpdateStoreData();
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_store_collect_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("store_collect : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				GameDataManager.UpdateStoreData();
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_store_speed_up_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("store_speed_up : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				GameDataManager.UpdateStoreData();
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_store_upgrade_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("store_upgrade : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (CheckServerErrorCode(array[0].errorCode) || CheckForceUpdate(array[0].force_update))
			{
				return;
			}
			Inst.InsertUserData(array[0].result);
			if (Inst.OnCallBack_Floor_Upgrade != null)
			{
				Inst.OnCallBack_Floor_Upgrade(array[0].result.forceCollectYn);
				Inst.OnCallBack_Floor_Upgrade = null;
				if (array[0].result.forceCollectYn == "y")
				{
					return;
				}
			}
			GameDataManager.UpdateUserData();
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_shop_list_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("shop_list : " + response.DataAsText);
			SHOP_LIST[] array = JsonConvert.DeserializeObject<SHOP_LIST[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				GameInfo.userData.userDailyItemList = array[0].result;
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
				GameDataManager.UpdateUserData();
				if (GameInfo.userData.userInfo.dailyShopNewYn.Equals("y"))
				{
					Protocol_user_default_info_Req();
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_shop_buy_daily_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("shop_buy_daily : " + response.DataAsText);
			SHOP_BUY_DAILY[] array = JsonConvert.DeserializeObject<SHOP_BUY_DAILY[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				GameInfo.userData.userInfo = array[0].result.userInfo;
				GameInfo.userData.userItemList = array[0].result.userItemList;
				GameInfo.userData.userDailyItemList.dailyShopInfo.dailyShopList = array[0].result.dailyShopList;
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
				GameDataManager.UpdateUserData();
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_shop_buy_coin_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("shop_buy_coin : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
				GameDataManager.UpdateUserData();
				LobbyManager.GetCoinEff(Vector3.zero);
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_shop_buy_jewel_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("shop_buy_jewel : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode))
			{
				Inst.InsertUserData(array[0].result);
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
				GameDataManager.UpdateUserData();
				LobbyManager.GetCoinEff(Vector3.zero);
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_shop_popup_hunter_buy_coin_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("shop_popup_hunter_buy_coin : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				GameDataManager.UpdateUserData();
				LobbyManager.GetCoinEff(Vector3.zero);
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_shop_popup_hunter_promotion_buy_coin_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("shop_popup_hunter_promotion_buy_coin : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				GameDataManager.UpdateUserData();
				LobbyManager.GetCoinEff(Vector3.zero);
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_shop_popup_store_buy_coin_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("shop_popup_store_buy_coin : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				GameDataManager.UpdateUserData();
				LobbyManager.GetCoinEff(Vector3.zero);
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_shop_package_list_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("shop_package_list : " + response.DataAsText);
            SHOP_PACKAGE_LIST[] array = JsonConvert.DeserializeObject<SHOP_PACKAGE_LIST[]>(response.DataAsText);
            if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update) && Inst.OnCallBack_Shop_Package_List != null)
            {
                Inst.OnCallBack_Shop_Package_List(array[0].result);
                Inst.OnCallBack_Shop_Package_List = null;
            }
        }
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_shop_ad_energy_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("shop_ad_energy : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_shop_buy_energy_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("shop_buy_energy : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
				GameDataManager.UpdateUserData();
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_user_default_info_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("user_default_info : " + response.DataAsText);
			USER_DEFAULT_INFO[] array = JsonConvert.DeserializeObject<USER_DEFAULT_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode))
			{
				GameInfo.userData.userInfo = array[0].result.userDefaultInfo;
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
				GameDataManager.UpdateUserData();
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_shop_buy_product_Res(HTTPRequest request, HTTPResponse response)
	{
		RemoveConnectRequest(request);
		MWLog.Log(request.State.ToString());
		var num = Get_ErrorCode(request);
		if (num == 0)
		{
			MWLog.Log("shop_buy_product : " + response.DataAsText);
			USER_DEFAULT_INFO[] array = JsonConvert.DeserializeObject<USER_DEFAULT_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				GameInfo.userData.userInfo = array[0].result.userDefaultInfo;
				if (array[0].success.Equals("true") && Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
				GameDataManager.UpdateUserData();
				LobbyManager.GetJewelEff(Vector3.zero);
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_shop_buy_package_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("shop_buy_package : " + response.DataAsText);
			SHOP_BUY_PACKAGE[] array = JsonConvert.DeserializeObject<SHOP_BUY_PACKAGE[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result.userData);
				if (array[0].success.Equals("true") && Inst.OnCallBack_Buy_Package != null)
				{
					Inst.OnCallBack_Buy_Package(array[0].result.rewardList);
					Inst.OnCallBack_Buy_Package = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_shop_ad_start_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("shop_ad_start : " + response.DataAsText);
            SHOP_AD_START[] array = JsonConvert.DeserializeObject<SHOP_AD_START[]>(response.DataAsText);
            if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update) && Inst.OnCallBack_Ad_Start != null)
            {
                if (array[0].success.Equals("true"))
                {
                    Inst.OnCallBack_Ad_Start(array[0].result.adKey);
                    Inst.OnCallBack_Ad_Start = null;
                }
            }
        }
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_chest_popup_buy_coin_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("chest_popup_buy_coin : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				GameDataManager.UpdateUserData();
				LobbyManager.GetCoinEff(Vector3.zero);
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_chest_ad_start_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("chest_ad_start : " + response.DataAsText);
            CHECK_SUCCESS[] array = JsonConvert.DeserializeObject<CHECK_SUCCESS[]>(response.DataAsText);
            if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update) && Inst.onCallBack_Default != null)
            {
                Inst.onCallBack_Default();
                Inst.onCallBack_Default = null;
            }
        }
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_arena_info_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("arena_info : " + response.DataAsText);
			ARENA_INFO_DATA[] array = JsonConvert.DeserializeObject<ARENA_INFO_DATA[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				GameInfo.userData.userInfo.arenaTicket = array[0].result.userArenaInfo.arenaTicket;
				GameInfo.userData.userInfo.arenaPoint = array[0].result.userArenaInfo.arenaPoint;
				GameInfo.userData.userHunterList = array[0].result.userHunterList;
				if (Inst.OnCallBack_Arena_Info_Data != null)
				{
					Inst.OnCallBack_Arena_Info_Data(array[0].result);
					Inst.OnCallBack_Arena_Info_Data = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_arena_game_start_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("arena_game_start : " + response.DataAsText);
			ARENA_GAME_START[] array = JsonConvert.DeserializeObject<ARENA_GAME_START[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				GameInfo.userPlayData.gameKey = array[0].result.gameKey;
				if (Inst.OnCallBack_Arena_Game_Start_Data != null)
				{
					Inst.OnCallBack_Arena_Game_Start_Data(array[0].result);
					Inst.OnCallBack_Arena_Game_Start_Data = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_arena_game_end_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("arena_game_end : " + response.DataAsText);
            ARENA_GAME_END[] array = JsonConvert.DeserializeObject<ARENA_GAME_END[]>(response.DataAsText);
            if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update) && Inst.OnCallBack_Arena_Game_End_Data != null)
            {
                Inst.OnCallBack_Arena_Game_End_Data(array[0].result);
                Inst.OnCallBack_Arena_Game_End_Data = null;
            }
        }
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_arena_game_continue_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("arena_game_continue : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				GameDataManager.UpdateUserData();
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_arena_buy_ticket_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("arena_buy_ticket : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				if (Inst.OnCallBack_Arena_Ticket != null)
				{
					Inst.OnCallBack_Arena_Ticket();
					Inst.OnCallBack_Arena_Ticket = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_arena_store_list_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("arena_store_list : " + response.DataAsText);
            ARENA_STORE_LIST[] array = JsonConvert.DeserializeObject<ARENA_STORE_LIST[]>(response.DataAsText);
            if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update) && Inst.OnCallBack_Arena_Store_Info_Data != null)
            {
                Inst.OnCallBack_Arena_Store_Info_Data(array[0].result.arenaStoreInfo);
                Inst.OnCallBack_Arena_Store_Info_Data = null;
            }
        }
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private void Protocol_arena_store_buy_product_Res(HTTPRequest request, HTTPResponse response)
	{
        RemoveConnectRequest(request);
        MWLog.Log(request.State.ToString());
        var num = Get_ErrorCode(request);
        if (num == 0)
		{
			MWLog.Log("arena_store_buy_product : " + response.DataAsText);
			USER_INFO[] array = JsonConvert.DeserializeObject<USER_INFO[]>(response.DataAsText);
			if (!CheckServerErrorCode(array[0].errorCode) && !CheckForceUpdate(array[0].force_update))
			{
				Inst.InsertUserData(array[0].result);
				GameDataManager.UpdateUserData();
				if (Inst.onCallBack_Default != null)
				{
					Inst.onCallBack_Default();
					Inst.onCallBack_Default = null;
				}
			}
		}
		else
		{
			MWLog.Log("error = " + num);
		}
	}

	private int Get_ErrorCode(HTTPRequest request, bool isLoadingCheck = true)
	{
		int num = 0;
		switch (request.State)
		{
		case HTTPRequestStates.Finished:
			if (request.Response.IsSuccess)
			{
				num = 0;
				Sended_Protocol = null;
			}
			else
			{
				num = -1;
			}
			break;
		case HTTPRequestStates.Error:
			num = -1;
			break;
		case HTTPRequestStates.ConnectionTimedOut:
			num = 1;
			break;
		case HTTPRequestStates.TimedOut:
			num = 1;
			break;
		}
		if (num != 0)
		{
			networkError.Show();
		}
		return num;
	}

	private void CallforByBestHTTP(Dictionary<string, string> _obj, OnRequestFinishedDelegate callBack, bool isLoading = true, bool isSceneLoading = false)
	{
		HTTPRequest httpRequest = null;
		httpRequest = new HTTPRequest(new Uri(BuildSet.serverURL), HTTPMethods.Post, callBack);
		foreach (var item in _obj)
		{
			if (item.Value == null)
			{
				Debug.LogWarning(item.Key + " value is null");
			}
			else
			{
				httpRequest.AddField(item.Key, item.Value);
			}
		}
		if (IRVManager.CurrentNetStatus == InternetReachabilityVerifier.Status.NetVerified)
		{
			httpRequest.Send();
			Inst.Sended_Protocol = httpRequest;
			if (isLoading)
			{
				listConnect.Add(httpRequest);
				GameDataManager.ShowNetworkLoading(isSceneLoading);
			}
			if (coroutineNetworkDelay != null)
			{
				StopCoroutine(coroutineNetworkDelay);
				coroutineNetworkDelay = null;
			}
		}
		else
		{
			MWLog.Log("Network Error!!!!!!!");
			if (coroutineNetworkDelay != null)
			{
				coroutineNetworkDelay = StartCoroutine(CheckNetworkDelay());
			}
			Inst.Reservation_Protocol.Add(httpRequest);
			listConnect.Clear();
		}
	}

	private void RemoveConnectRequest(HTTPRequest request)
	{
		MWLog.Log("RemoveConnectRequest :: start :: " + listConnect.Count);
		listConnect.Remove(request);
		MWLog.Log("RemoveConnectRequest :: end :: " + listConnect.Count);
		if (listConnect.Count == 0)
		{
			GameDataManager.HideNetworkLoading();
		}
	}

	private IEnumerator CheckNetworkDelay()
	{
		yield return new WaitForSeconds(2f);
		if (IRVManager.CurrentNetStatus != InternetReachabilityVerifier.Status.NetVerified)
		{
			networkError.Show();
		}
	}

	private void Send_Reservation_Protocol()
	{
		networkError.Hide();
		if (IRVManager.CurrentNetStatus == InternetReachabilityVerifier.Status.NetVerified)
		{
			if (Sended_Protocol != null)
			{
				Sended_Protocol.Send();
            }
			else if (Reservation_Protocol.Count > 0)
			{
				StartCoroutine(Send_Reservation_Protocol_Coroutine());
			}
		}
	}

	private IEnumerator Send_Reservation_Protocol_Coroutine()
	{
		for (int i = 0; i < Reservation_Protocol.Count; i++)
		{
			Reservation_Protocol[i].Send();
			yield return new WaitForSeconds(0.1f);
		}
		Reservation_Protocol.Clear();
	}

	private bool CheckServerErrorCode(int errorCode)
	{
		if (errorCode == 0)
		{
			return false;
		}
		networkError.Show();
		return true;
	}

	private bool CheckForceUpdate(CHECK_VERSION_RESULT[] data)
	{
		if (data != null && data.Length > 0)
		{
			updateNotice.Show(data[0]);
			return true;
		}
		return false;
	}

	private void InsertUserData(UserData userData)
	{
		if (userData.userInfo != null)
		{
			GameInfo.userData.userInfo = userData.userInfo;
		}
		if (userData.userHunterList != null)
		{
			GameInfo.userData.userHunterList = userData.userHunterList;
		}
		if (userData.userItemList != null)
		{
			GameInfo.userData.userItemList = userData.userItemList;
		}
		if (userData.userStageState != null)
		{
			GameInfo.userData.userStageState = userData.userStageState;
		}
		if (userData.userFloorState != null)
		{
			GameInfo.userData.userFloorState = userData.userFloorState;
		}
		if (userData.userDailyItemList != null)
		{
			GameInfo.userData.userDailyItemList = userData.userDailyItemList;
		}
		GameUtil.SetUseHunterList();
		GameUtil.SetUseArenaHunterList();
	}

	private void InitSocialUser()
	{
        MWPlatformService.LoginResult = OnSocialLoginResultEvent;
		MWPlatformService.Init();
	}

	private void OnSocialLoginResultEvent(bool isSuccess, string errorMessage)
	{
		if (isSuccess)
		{
			GameInfo.sUid = MWPlatformService.UserId;
			PlayerPrefs.SetString("SocialLogin", "y");
		}
		else if (PlayerPrefs.GetString("SocialLogin") == "y")
		{
			loginFailPopup.Show();
			return;
		}
		Protocol_check_auth_Req();
		MWLog.Log("OnSocialLoginResultEvent - isSuccess :: " + isSuccess + ", errorMessage :: " + errorMessage);
		Debug.Log("suid :: " + GameInfo.sUid);
	}

	private int GetPlatformType()
	{
		MWLog.Log("GetPlatformType - " + GameInfo.sUid);
		if (!string.IsNullOrEmpty(GameInfo.sUid))
		{
			switch (BuildSet.CurrentPlatformType)
			{
			case PlatformType.aos:
				return 2;
			case PlatformType.ios:
				return 3;
			default:
				return 1;
			}
		}
		return 1;
	}

	public void OnClickShowUserInfo()
	{
		textUserInfo.gameObject.SetActive(!textUserInfo.gameObject.activeSelf);
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus && coroutineNetworkDelay != null)
		{
			StopCoroutine(coroutineNetworkDelay);
			coroutineNetworkDelay = null;
		}
	}

	private void OnApplicationQuit()
	{
		MWLog.Log("Session out");
		Protocol_check_logout_Req();
	}
}