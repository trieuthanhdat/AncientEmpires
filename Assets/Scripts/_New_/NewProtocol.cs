using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

public partial class Protocol_Set
{
#if UNITY_EDITOR
    [UnityEditor.MenuItem("PlayerPrefs/DeleteAll")]
    public static void PlayerPrefsDeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
#endif

    public static void Protocol_check_version_Req()
    {
        check_version(BuildSet.CurrentPlatformType, BuildSet.AppVer);
        return;

        static void check_version(PlatformType platformType, string version)
        {
            // Server logic processing
            var response = new CHECK_VERSION
            {
                result = new CHECK_VERSION_RESULT
                {
                    force_update = 2
                }
            };

            // Client receives message
            if (response.result.force_update == 2)
            {
                Inst.InitSocialUser();
            }
            else
            {
                Inst.updateNotice.Show(response.result);
            }
        }
    }

    public static void New_Protocol_check_auth_Req()
    {
        check_auth_new(Inst.GetPlatformType(), GameInfo.sUid, MWPlatformService.GetUniqueDeviceId(),
            BuildSet.CurrentPlatformType, GameInfo.AD_ID);
        return;

        static void check_auth_new(int sType, string sUid, string deviceId, PlatformType platformType, string adId)
        {
            // Server logic processing
            var response = new CHECK_AUTH
            {
                result = new CHECK_AUTH_RESULT
                {
                    caUid = 62540
                }
            };

            // Client receives message
            GameInfo.caUid = response.result.caUid;
            Protocol_user_get_tutorial_Req();
        }
    }

    public static void Protocol_check_logout_Req()
    {
        check_logout(GameInfo.caUid);
        return;

        static void check_logout(int caUid)
        {
            // Server logic processing
        }
    }

    private const string KeyTutorialMustDataSIndex = nameof(KeyTutorialMustDataSIndex);
    private const string KeyTutorialMustDataSeq = nameof(KeyTutorialMustDataSeq);

    public static void Protocol_user_get_tutorial_Req(Action onCallBack = null)
    {
        user_get_tutorial(GameInfo.caUid, onCallBack);
        return;

        static void user_get_tutorial(int caUid, Action onCallBack)
        {
            // Server logic processing
            var response = new USER_GET_TUTORIAL_DATA
            {
                result = new USER_GET_TUTORIAL_RESULT
                {
                    eventData = new USER_GET_TUTORIAL_EVENT_INFO[]
                    {
                            new()
                            {
                                sIndex = 6,
                                passYn = PlayerPrefs.GetString("KeyTutorial6", "n")
                            },
                            new()
                            {
                                sIndex = 7,
                                passYn = PlayerPrefs.GetString("KeyTutorial7", "n")
                            },
                            new()
                            {
                                sIndex = 8,
                                passYn = PlayerPrefs.GetString("KeyTutorial8", "n")
                            },
                            new()
                            {
                                sIndex = 9,
                                passYn = PlayerPrefs.GetString("KeyTutorial9", "n")
                            },
                            new()
                            {
                                sIndex = 10,
                                passYn = PlayerPrefs.GetString("KeyTutorial10", "n")
                            },
                            new()
                            {
                                sIndex = 11,
                                passYn = PlayerPrefs.GetString("KeyTutorial11", "n")
                            },
                            new()
                            {
                                sIndex = 12,
                                passYn = PlayerPrefs.GetString("KeyTutorial12", "n")
                            },
                            new()
                            {
                                sIndex = 13,
                                passYn = PlayerPrefs.GetString("KeyTutorial13", "n")
                            },
                            new()
                            {
                                sIndex = 14,
                                passYn = PlayerPrefs.GetString("KeyTutorial14", "n")
                            },
                            new()
                            {
                                sIndex = 15,
                                passYn = PlayerPrefs.GetString("KeyTutorial15", "n")
                            },
                            new()
                            {
                                sIndex = 16,
                                passYn = PlayerPrefs.GetString("KeyTutorial16", "n")
                            },
                            new()
                            {
                                sIndex = 17,
                                passYn = PlayerPrefs.GetString("KeyTutorial17", "n")
                            },
                    },
                    mustData = new USER_GET_TUTORIAL_MUST_INFO
                    {
                        sIndex = PlayerPrefs.GetInt(KeyTutorialMustDataSIndex),
                        seq = PlayerPrefs.GetInt(KeyTutorialMustDataSeq)
                    }
                }
            };

            // Client receives message
            TutorialManager.SetTutorialData(response.result);
            GameDataManager.StartGame();
            onCallBack?.Invoke();
        }
    }

    public static void Protocol_user_set_tutorial_Req(int sIdx, int seq, Action onCallBack = null)
    {
        user_set_tutorial(GameInfo.caUid, sIdx > 5 ? "e" : "m", sIdx, seq, onCallBack);
        return;

        static void user_set_tutorial(int caUid, string type, int sIndex, int seq, Action onCallBack)
        {
            // TODO
            // Server logic processing
            if (type == "m")
            {
                PlayerPrefs.SetInt(KeyTutorialMustDataSIndex, sIndex);
                PlayerPrefs.SetInt(KeyTutorialMustDataSeq, seq);
            }
            else
            {
                PlayerPrefs.SetString($"KeyTutorial{sIndex}", "y");
            }

            // Client receives message
            onCallBack?.Invoke();
        }
    }

    private static UserHunterData[] GetHuntersArenaUseInfo()
    {
        return null;
    }

    private static UserHunterData[] GetHuntersOwnInfo()
    {
        return null;
    }

    private static UserHunterData[] GetHuntersUseInfo()
    {
        return null;
    }

    private static UserFloorStage[] GetUserFloorState()
    {
        return null;
    }

    private static UserHunterData[] GetUserHunterList()
    {
        return null;
    }

    private static UserInfoData GetUserInfoData()
    {
        return new UserInfoData
        {
            ad_chest_limit = PlayerPrefs.GetInt(nameof(UserInfoData.ad_chest_limit), 10),
            ad_energy_limit = PlayerPrefs.GetInt(nameof(UserInfoData.ad_energy_limit), 5),
            arenaAlarmYn = PlayerPrefs.GetString(nameof(UserInfoData.arenaAlarmYn), "n"),
            arenaLevel = PlayerPrefs.GetInt(nameof(UserInfoData.arenaLevel), 0),
            arenaPoint = PlayerPrefs.GetInt(nameof(UserInfoData.arenaPoint), 0),
            arenaTicket = PlayerPrefs.GetInt(nameof(UserInfoData.arenaTicket), 0),
            chestKey = PlayerPrefs.GetInt(nameof(UserInfoData.chestKey), 3),
            chestRemainTime = PlayerPrefs.GetInt(nameof(UserInfoData.chestRemainTime), 0),
            coin = PlayerPrefs.GetInt(nameof(UserInfoData.coin), 1000),
            dailyShopNewYn = PlayerPrefs.GetString(nameof(UserInfoData.dailyShopNewYn), "y"),
            dailyShopRemainTime = PlayerPrefs.GetInt(nameof(UserInfoData.dailyShopRemainTime), 0),
            energy = PlayerPrefs.GetInt(nameof(UserInfoData.energy), 25),
            energyRemainTime = PlayerPrefs.GetInt(nameof(UserInfoData.energyRemainTime), 300),
            exp = PlayerPrefs.GetInt(nameof(UserInfoData.exp), 0),
            freeChestRemainTime = PlayerPrefs.GetInt(nameof(UserInfoData.freeChestRemainTime), 0),
            jewel = PlayerPrefs.GetInt(nameof(UserInfoData.jewel), 50),
            level = PlayerPrefs.GetInt(nameof(UserInfoData.level), 1),
            levelUpYn = PlayerPrefs.GetString(nameof(UserInfoData.levelUpYn), "n"),
            maxEnergy = PlayerPrefs.GetInt(nameof(UserInfoData.maxEnergy), 30),
            oldUserYn = PlayerPrefs.GetString(nameof(UserInfoData.oldUserYn), "n"),
        };
    }

    private static UserItemData[] GetUserItemList()
    {
        return null;
    }

    private static UserStageState[] GetUserStageState()
    {
        return null;
    }

    public static void New_Protocol_user_info_Req(Action onCallBack = null, bool isLoading = true)
    {
        user_info(GameInfo.caUid, onCallBack);
        return;

        static void user_info(int caUid, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {
                result = new UserData
                {
                    forceCollectYn = "n",
                    huntersArenaUseInfo = GetHuntersArenaUseInfo(),
                    huntersOwnInfo = GetHuntersOwnInfo(),
                    huntersUseInfo = GetHuntersUseInfo(),
                    userDailyItemList = new SHOP_LIST_RESULT
                    {

                    },
                    userFloorState = GetUserFloorState(),
                    userHunterList = GetUserHunterList(),
                    userInfo = GetUserInfoData(),
                    userItemList = GetUserItemList(),
                    userStageState = GetUserStageState(),
                }
            };

            // Client receives message
            Inst.InsertUserData(response.result);
            GameDataManager.UpdateUserData();
            onCallBack?.Invoke();
            LobbyManager.CheckHunterAlert();
        }
    }

    public static void New_Protocol_user_item_info_Req(Action onCallBack = null, bool isLoading = true)
    {
        user_item_info(GameInfo.caUid, onCallBack);
        return;

        static void user_item_info(int caUid, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_ITEM_INFO
            {
                result = new USER_ITEM_INFO_RESULT
                {
                    userItemInfo = new UserData
                    {
                        userItemList = GetUserItemList()
                    }
                }
            };

            // Client receives message
            Inst.InsertUserData(response.result.userItemInfo);
            GameDataManager.UpdateUserData();
            onCallBack?.Invoke();
            LobbyManager.CheckHunterAlert();
        }
    }

    public static void New_Protocol_user_daily_bonus_Req(Action<USER_DAILY_BONUS_RESULT> onCallBack = null,
        bool isLoading = true)
    {
        user_daily_bonus(GameInfo.caUid, onCallBack);
        return;

        static void user_daily_bonus(int caUid, Action<USER_DAILY_BONUS_RESULT> onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_DAILY_BONUS
            {

            };

            // Client receives message
            onCallBack?.Invoke(response.result);
        }
    }

    public static void New_Protocol_user_get_daily_bonus_Req(int dayCnt, Action onCallBack = null,
        bool isLoading = true)
    {
        user_get_daily_bonus(GameInfo.caUid, dayCnt, onCallBack);
        return;

        static void user_get_daily_bonus(int caUid, int dayCnt, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result);
            GameDataManager.UpdateUserData();
            onCallBack?.Invoke();
        }
    }

    public static void New_Protocol_user_chapter_open_Req(int stage, int chapter, Action onCallBack = null)
    {
        user_chapter_open(GameInfo.caUid, stage, chapter, onCallBack);
        return;

        static void user_chapter_open(int caUid, int stage, int chapter, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result);
            GameDataManager.UpdateUserData();
            onCallBack?.Invoke();
        }
    }

    public static void New_Protocol_game_start_Req(int levelIdx, List<int> listBoostItem, Action onCallBack = null)
    {
        game_start(GameInfo.caUid, levelIdx, listBoostItem, onCallBack);
        return;

        static void game_start(int caUid, int levelIdx, List<int> boosterIdxArray, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var gameKey = PlayerPrefs.GetInt(nameof(UserPlayData.gameKey), 0) + 1;
            PlayerPrefs.SetInt(nameof(UserPlayData.gameKey), gameKey);
            var response = new GAME_START
            {
                result = new GAME_START_RESULT
                {
                    userInfo = GetUserInfoData(),
                    gameKey = gameKey
                }
            };

            // Client receives message
            GameInfo.userData.userInfo = response.result.userInfo;
            GameInfo.userPlayData.gameKey = response.result.gameKey;
            GameDataManager.UpdateUserData();
            onCallBack?.Invoke();
        }
    }

    public static void New_Protocol_game_end_Req(int levelIdx, int gameKey, int result, int resultReason, int turns,
        int monsterChestKey, List<List<int>> monsterArray, int[] waveArray, Action<GAME_END_RESULT> onCallBack = null)
    {
        game_end(GameInfo.caUid, levelIdx, gameKey, result, resultReason, turns, monsterChestKey, 0, monsterArray,
            waveArray, onCallBack);
        return;

        static void game_end(int caUid, int levelIdx, int gameKey, int result, int resultReason, int turns,
            int monsterChestKey, int continueCount, List<List<int>> monsterArray, int[] waveArray,
            Action<GAME_END_RESULT> onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new GAME_END
            {

            };

            // Client receives message
            onCallBack?.Invoke(response.result);
        }
    }

    public static void New_Protocol_game_continue_Req(Action onCallBack = null)
    {
        game_continue(GameInfo.caUid, GameInfo.userPlayData.gameKey, onCallBack);
        return;

        static void game_continue(int caUid, int gameKey, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result);
            GameDataManager.UpdateUserData();
            onCallBack?.Invoke();
        }
    }

    public static void New_Protocol_game_quick_loot_Req(int levelIdx, int monsterChestKey, int adKey,
        Action<GAME_QUICK_LOOT> onCallBack = null)
    {
        game_quick_loot(GameInfo.caUid, levelIdx, monsterChestKey, adKey, onCallBack);
        return;

        static void game_quick_loot(int caUid, int levelIdx, int monsterChestKey, int adKey,
            Action<GAME_QUICK_LOOT> onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new GAME_QUICK_LOOT
            {

            };

            // Client receives message
            onCallBack?.Invoke(response);
        }
    }

    // public static void New_Protocol_game_quick_loot_speed_up_Req(int levelIdx, Action onCallBack = null)
    // {
    //     game_quick_loot_speed_up(GameInfo.caUid, levelIdx, onCallBack);
    //     return;
    //
    //     static void game_quick_loot_speed_up(int caUid, int levelIdx, Action onCallBack)
    //     {
    //         // TODO
    //         // Server logic processing
    //         var response = new USER_INFO
    //         {
    //
    //         };
    //
    //         // Client receives message
    //         Inst.InsertUserData(response.result);
    //         GameDataManager.UpdateUserData();
    //         onCallBack?.Invoke();
    //     }
    // }

    public static void New_Protocol_game_chapter_collect_Req(int stage, int chapter, Action onCallBack = null)
    {
        game_chapter_collect(GameInfo.caUid, stage, chapter, onCallBack);
        return;

        static void game_chapter_collect(int caUid, int stage, int chapter, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result);
            onCallBack?.Invoke();
        }
    }

    // public static void New_Protocol_game_level_remain_time_Req(int levelIdx,
    //     Action<GAME_LEVEL_REAMAIN_TIME_RESULT> onCallBack = null)
    // {
    //     game_level_remain_time(GameInfo.caUid, levelIdx, onCallBack);
    //     return;
    //
    //     static void game_level_remain_time(int caUid, int levelIdx, Action<GAME_LEVEL_REAMAIN_TIME_RESULT> onCallBack)
    //     {
    //         // TODO
    //         // Server logic processing
    //         var response = new GAME_LEVEL_REAMAIN_TIME
    //         {
    //
    //         };
    //
    //         // Client receives message
    //         onCallBack?.Invoke(response.result);
    //     }
    // }

    public static void New_Protocol_chest_popup_buy_coin_Req(int needJewel, Action onCallBack)
    {
        chest_popup_buy_coin(GameInfo.caUid, needJewel, onCallBack);
        return;

        static void chest_popup_buy_coin(int caUid, int needJewel, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result);
            GameDataManager.UpdateUserData();
            LobbyManager.GetCoinEff(Vector3.zero);
            onCallBack?.Invoke();
        }
    }

    public static void New_Protocol_chest_collect_Req(int chestIdx, Action<ChestListDbData[]> onCallBack,
        string isFree = "n", bool isTutorial = false, int tutorialNo = 0)
    {
        chest_collect(GameInfo.caUid, chestIdx, isFree, isTutorial, tutorialNo, onCallBack);
        return;

        static void chest_collect(int caUid, int chestIdx, string isFree, bool isTutorial, int tutorialNo,
            Action<ChestListDbData[]> onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new CHEST_COLLECT
            {

            };

            // Client receives message
            onCallBack?.Invoke(response.result.rewardList);
        }
    }

    public static void New_Protocol_chest_req_reward_Req(int probIdx, bool isAd, Action onCallBack = null)
    {
        chest_req_reward(GameInfo.caUid, probIdx, isAd, onCallBack);
        return;

        static void chest_req_reward(int caUid, int probIdx, bool isAd, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result);
            GameDataManager.UpdateUserData();
            onCallBack?.Invoke();
        }
    }

    public static void New_Protocol_hunter_change_Req(int[] useHunter, HUNTERLIST_TYPE listType, Action onCallBack)
    {
        hunter_change(GameInfo.caUid, useHunter, listType, onCallBack);
        return;

        static void hunter_change(int caUid, int[] useHunter, HUNTERLIST_TYPE listType, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result);
            GameDataManager.UpdateUserData();
            onCallBack?.Invoke();
        }
    }

    public static void New_Protocol_hunter_is_not_new_Req(int hunterIdx, Action onCallBack)
    {
        hunter_is_not_new(GameInfo.caUid, hunterIdx, onCallBack);
        return;

        static void hunter_is_not_new(int caUid, int hunterIdx, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result);
            GameDataManager.UpdateUserData();
            onCallBack?.Invoke();
        }
    }

    public static void New_Protocol_hunter_level_up_Req(int hunterIdx, int targetLevel, Action onCallBack,
        bool isTutorial = false)
    {
        if (targetLevel < 0) throw new ArgumentOutOfRangeException(nameof(targetLevel));
        hunter_level_up(GameInfo.caUid, hunterIdx, targetLevel, isTutorial, onCallBack);
        return;

        static void hunter_level_up(int caUid, int hunterIdx, int targetLevel, bool isTutorial, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result);
            GameDataManager.UpdateUserData();
            onCallBack?.Invoke();
        }
    }

    public static void New_Protocol_hunter_promotion_Req(int hunterIdx, Action onCallBack)
    {
        hunter_promotion(GameInfo.caUid, hunterIdx, onCallBack);
        return;

        static void hunter_promotion(int caUid, int hunterIdx, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result);
            GameDataManager.UpdateUserData();
            onCallBack?.Invoke();
        }
    }

    public static void New_Protocol_store_collect_Req(int stage, int storeIdx, Action onCallBack)
    {
        store_collect(GameInfo.caUid, stage, storeIdx, onCallBack);
        return;

        static void store_collect(int caUid, int stage, int storeIdx, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result);
            GameDataManager.UpdateStoreData();
            onCallBack?.Invoke();
        }
    }

    public static void New_Protocol_store_open_Req(int stageIdx, int storeIdx, Action onCallBack)
    {
        store_open(GameInfo.caUid, stageIdx, storeIdx, onCallBack);
        return;

        static void store_open(int caUid, int stageIdx, int storeIdx, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result);
            GameDataManager.UpdateStoreData();
            onCallBack?.Invoke();
        }
    }

    public static void New_Protocol_store_speed_up_Req(int stage, int storeIdx, int jewel, Action onCallBack)
    {
        store_speed_up(GameInfo.caUid, stage, storeIdx, jewel, onCallBack);
        return;

        static void store_speed_up(int caUid, int stage, int storeIdx, int jewel, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result);
            GameDataManager.UpdateStoreData();
            onCallBack?.Invoke();
        }
    }

    public static void New_Protocol_store_upgrade_Req(int storeIdx, int storeTier, Action<string> onCallBack)
    {
        store_upgrade(GameInfo.caUid, storeIdx, storeTier, onCallBack);
        return;

        static void store_upgrade(int caUid, int storeIdx, int storeTier, Action<string> onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result);
            if (onCallBack != null)
            {
                onCallBack(response.result.forceCollectYn);
                if (response.result.forceCollectYn == "y")
                {
                    return;
                }
            }

            GameDataManager.UpdateUserData();
        }
    }

    public static void New_Protocol_shop_list_Req(Action onCallBack)
    {
        shop_list(GameInfo.caUid, onCallBack);
        return;

        static void shop_list(int caUid, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new SHOP_LIST
            {

            };

            // Client receives message
            GameInfo.userData.userDailyItemList = response.result;
            onCallBack?.Invoke();
            GameDataManager.UpdateUserData();
            if (GameInfo.userData.userInfo.dailyShopNewYn.Equals("y"))
            {
                Protocol_user_default_info_Req();
            }
        }
    }

    public static void New_Protocol_shop_buy_daily_Req(int productIdx, int buyCount, Action onCallBack)
    {
        shop_buy_daily(GameInfo.caUid, productIdx, buyCount, onCallBack);
        return;

        static void shop_buy_daily(int caUid, int productIdx, int buyCount, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new SHOP_BUY_DAILY
            {

            };

            // Client receives message
            GameInfo.userData.userInfo = response.result.userInfo;
            GameInfo.userData.userItemList = response.result.userItemList;
            GameInfo.userData.userDailyItemList.dailyShopInfo.dailyShopList = response.result.dailyShopList;
            onCallBack?.Invoke();
            GameDataManager.UpdateUserData();
        }
    }

    public static void New_Protocol_shop_buy_coin_Req(int productIdx, Action onCallBack)
    {
        shop_buy_coin(GameInfo.caUid, productIdx, onCallBack);
        return;

        static void shop_buy_coin(int caUid, int productIdx, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result);
            onCallBack?.Invoke();
            GameDataManager.UpdateUserData();
            LobbyManager.GetCoinEff(Vector3.zero);
        }
    }

    //    public static void New_Protocol_shop_buy_jewel_Req(int productIdx, Action onCallBack)
    // {
    //        shop_buy_jewel(GameInfo.caUid, productIdx, onCallBack);
    //        return;
    //
    //        static void shop_buy_jewel(int caUid, int productIdx, Action onCallBack)
    //        {
    //            // TODO
    //            // Server logic processing
    //            var response = new USER_INFO
    //            {
    //
    //            };
    //
    //            // Client receives message
    //            Inst.InsertUserData(response.result);
    //            onCallBack?.Invoke();
    //            GameDataManager.UpdateUserData();
    //            LobbyManager.GetCoinEff(Vector3.zero);
    //        }
    //    }

    public static void New_Protocol_shop_popup_hunter_buy_coin_Req(int hunterIdx, int targetLevel, int needJewel, Action onCallBack)
    {
        shop_popup_hunter_buy_coin(GameInfo.caUid, hunterIdx, targetLevel, needJewel, onCallBack);
        return;

        static void shop_popup_hunter_buy_coin(int caUid, int hunterIdx, int targetLevel, int needJewel, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result);
            GameDataManager.UpdateUserData();
            LobbyManager.GetCoinEff(Vector3.zero);
            onCallBack?.Invoke();
        }
    }

    public static void New_Protocol_shop_popup_hunter_promotion_buy_coin_Req(int hunterIdx, int needJewel, Action onCallBack)
    {
        shop_popup_hunter_promotion_buy_coin(GameInfo.caUid, hunterIdx, needJewel, onCallBack);
        return;

        static void shop_popup_hunter_promotion_buy_coin(int caUid, int hunterIdx, int needJewel, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result);
            GameDataManager.UpdateUserData();
            LobbyManager.GetCoinEff(Vector3.zero);
            onCallBack?.Invoke();
        }
    }

    public static void New_Protocol_shop_popup_store_buy_coin_Req(int storeIdx, int needJewel, Action onCallBack)
    {
        check_version(GameInfo.caUid, storeIdx, needJewel, onCallBack);
        return;

        static void check_version(int caUid, int storeIdx, int needJewel, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result);
            GameDataManager.UpdateUserData();
            LobbyManager.GetCoinEff(Vector3.zero);
            onCallBack?.Invoke();
        }
    }

    public static void New_Protocol_shop_package_list_Req(Action<SHOP_PACKAGE_LIST_RESULT> onCallBack)
    {
        shop_package_list(GameInfo.caUid, onCallBack);
        return;

        static void shop_package_list(int caUid, Action<SHOP_PACKAGE_LIST_RESULT> onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new SHOP_PACKAGE_LIST
            {

            };

            // Client receives message
            onCallBack?.Invoke(response.result);
        }
    }

    public static void New_Protocol_shop_ad_energy_Req(int energyCount, Action onCallBack = null)
    {
        shop_ad_energy(GameInfo.caUid, energyCount, onCallBack);
        return;

        static void shop_ad_energy(int caUid, int energyCount, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result);
            onCallBack?.Invoke();
        }
    }

    public static void New_Protocol_shop_ad_energy_start_Req()
    {
        shop_ad_energy_start(GameInfo.caUid);
        return;

        static void shop_ad_energy_start(int caUid)
        {
            // TODO
            // Server logic processing
        }
    }

    public static void New_Protocol_shop_buy_energy_Req(Action onCallBack)
    {
        shop_buy_energy(GameInfo.caUid, onCallBack);
        return;

        static void shop_buy_energy(int caUid, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result);
            onCallBack?.Invoke();
            GameDataManager.UpdateUserData();
        }
    }

    public static void New_Protocol_user_default_info_Req(Action onCallBack = null, bool isLoading = true)
    {
        user_default_info(GameInfo.caUid, onCallBack);
        return;

        static void user_default_info(int caUid, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_DEFAULT_INFO
            {

            };

            // Client receives message
            GameInfo.userData.userInfo = response.result.userDefaultInfo;
            onCallBack?.Invoke();
            GameDataManager.UpdateUserData();
        }
    }

    public static void New_Protocol_shop_buy_product_Req(int productId, string signature, string signedData,
        Action onCallBack = null)
    {
        shop_buy_product(GameInfo.caUid, BuildSet.CurrentPlatformType, "jewel", productId, signature, signedData,
            onCallBack);
        return;

        static void shop_buy_product(int caUid, PlatformType platformType, string purchaseType, int productId,
            string signature, string signedData, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_DEFAULT_INFO
            {

            };

            // Client receives message
            GameInfo.userData.userInfo = response.result.userDefaultInfo;
            if (response.success.Equals("true"))
            {
                onCallBack?.Invoke();
            }

            GameDataManager.UpdateUserData();
            LobbyManager.GetJewelEff(Vector3.zero);
        }
    }

    public static void New_Protocol_shop_buy_package_Req(int productType, string signature, string signedData,
        Action<ChestListDbData[]> onCallBack = null)
    {
        shop_buy_package(GameInfo.caUid, BuildSet.CurrentPlatformType, "package", productType, signature, signedData,
            onCallBack);
        return;

        static void shop_buy_package(int caUid, PlatformType platformType, string purchaseType, int productType,
            string signature, string signedData, Action<ChestListDbData[]> onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new SHOP_BUY_PACKAGE
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result.userData);
            if (response.success.Equals("true"))
            {
                onCallBack?.Invoke(response.result.rewardList);
            }
        }
    }

    public static void New_Protocol_shop_ad_start_Req(int adType, Action<int> onCallBack = null)
    {
        shop_ad_start(GameInfo.caUid, adType, onCallBack);
        return;

        static void shop_ad_start(int caUid, int adType, Action<int> onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new SHOP_AD_START
            {

            };

            // Client receives message
            if (response.success.Equals("true"))
            {
                onCallBack?.Invoke(response.result.adKey);
            }
        }
    }

    public static void New_Protocol_chest_ad_start_Req(Action onCallBack = null)
    {
        chest_ad_start(GameInfo.caUid, onCallBack);
        return;

        static void chest_ad_start(int caUid, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new CHECK_SUCCESS
            {

            };

            // Client receives message
            onCallBack?.Invoke();
        }
    }

    public static void New_Protocol_arena_info_Req(Action<ARENA_INFO_DATA_RESULT> onCallBack = null)
    {
        arena_info(GameInfo.caUid, onCallBack);
        return;

        static void arena_info(int caUid, Action<ARENA_INFO_DATA_RESULT> onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new ARENA_INFO_DATA
            {

            };

            // Client receives message
            GameInfo.userData.userInfo.arenaTicket = response.result.userArenaInfo.arenaTicket;
            GameInfo.userData.userInfo.arenaPoint = response.result.userArenaInfo.arenaPoint;
            GameInfo.userData.userHunterList = response.result.userHunterList;
            onCallBack?.Invoke(response.result);
        }
    }

    public static void New_Protocol_arena_game_start_Req(int levelIdx, List<int> listBoostItem, Action<ARENA_GAME_START_RESULT> onCallBack = null)
    {
        arena_game_start(GameInfo.caUid, levelIdx, listBoostItem, onCallBack);
        return;

        static void arena_game_start(int caUid, int levelIdx, List<int> listBoostItem, Action<ARENA_GAME_START_RESULT> onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new ARENA_GAME_START
            {

            };

            // Client receives message
            GameInfo.userPlayData.gameKey = response.result.gameKey;
            onCallBack?.Invoke(response.result);
        }
    }

    public static void New_Protocol_arena_game_end_Req(int levelIdx, int gameKey, int result, int resultReason,
        int wave, Action<ARENA_GAME_END_RESULT> onCallBack = null)
    {
        arena_game_end(GameInfo.caUid, levelIdx, gameKey, result, resultReason, wave, onCallBack);
        return;

        static void arena_game_end(int caUid, int levelIdx, int gameKey, int result, int resultReason, int wave, Action<ARENA_GAME_END_RESULT> onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new ARENA_GAME_END
            {

            };

            // Client receives message
            onCallBack?.Invoke(response.result);
        }
    }

    public static void New_Protocol_arena_game_continue_Req(Action onCallBack = null)
    {
        arena_game_continue(GameInfo.caUid, GameInfo.userPlayData.gameKey, onCallBack);
        return;

        static void arena_game_continue(int caUid, int gameKey, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result);
            GameDataManager.UpdateUserData();
            onCallBack?.Invoke();
        }
    }

    public static void New_Protocol_arena_buy_ticket_Req(Action onCallBack = null)
    {
        arena_buy_ticket(GameInfo.caUid, onCallBack);
        return;

        static void arena_buy_ticket(int caUid, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result);
            onCallBack?.Invoke();
        }
    }

    public static void New_Protocol_arena_store_list_Req(Action<ARENA_STORE_INFO[]> onCallBack = null)
    {
        arena_store_list(GameInfo.caUid, onCallBack);
        return;

        static void arena_store_list(int caUid, Action<ARENA_STORE_INFO[]> onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new ARENA_STORE_LIST
            {

            };

            // Client receives message
            onCallBack?.Invoke(response.result.arenaStoreInfo);
        }
    }

    public static void New_Protocol_arena_store_buy_product_Req(int productIdx, Action onCallBack = null)
    {
        arena_store_buy_product(GameInfo.caUid, productIdx, onCallBack);
        return;

        static void arena_store_buy_product(int caUid, int productIdx, Action onCallBack)
        {
            // TODO
            // Server logic processing
            var response = new USER_INFO
            {

            };

            // Client receives message
            Inst.InsertUserData(response.result);
            GameDataManager.UpdateUserData();
            onCallBack?.Invoke();
        }
    }
}