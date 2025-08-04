using CodeStage.AntiCheat.ObscuredTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDataManager : GameObjectSingleton<GameDataManager>
{
	public static Action ChangeUserData;

	public static Action ChangeStoreData;

	private const string USER_SAVE_DATA_KEY = "UserData";

	private const string USER_DEFAULT_DATA_PATH = "Data/UserDefaultData";

	private const string INGAME_BATTLE_SPEED_KEY = "InGameBattleSpeed";

	[SerializeField]
	private bool isUserDataClear;

	[SerializeField]
	private GameObject goNetworkLoading;

	[SerializeField]
	private GameObject goSceneLoading;

	[SerializeField]
	private UserLevelUp userLevelUp;

	[SerializeField]
	private GameObject loadingText;

	[SerializeField]
	private GameObject goInGameRule;

	private LocalDB localDB;

	private GameResourceData gameResourceData;

	private Coroutine coroutineSceneLoading;

	public static int GetGameConfigData(ConfigDataType type)
	{
		return Inst.localDB.GetGameConfigData(type);
	}

	public static Sprite GetStageImage(int index)
	{
		return Inst.gameResourceData.GetStageMenuImage(index);
	}

	public static Sprite GetFloorTitleSprite(int index)
	{
		return Inst.gameResourceData.GetFloorTitleSprite(index);
	}

	public static Sprite GetStageCellSprite(int index)
	{
		return Inst.gameResourceData.GetStageCellSprite(index);
	}

	public static Sprite GetStagePreviewSprite(int index)
	{
		return Inst.gameResourceData.GetStagePreviewSprite(index);
	}

	public static Sprite GetStageStoreBlendSprite(int index)
	{
		return Inst.gameResourceData.GetStageStoreBlendSprite(index);
	}

	public static Dictionary<int, StageDbData> GetDicStageDbData()
	{
		return Inst.localDB.DicStageDbData;
	}

	public static Dictionary<int, ChapterDbData> GetDicChapterDbData(int stageId)
	{
		return Inst.localDB.GetChapterDataList(stageId);
	}

	public static List<LevelDbData> GetLevelListDbData(int stageId, int chapterId)
	{
		return Inst.localDB.GetLevelDbList(stageId, chapterId);
	}

	public static LevelDbData GetLevelIndexDbData(int levelIdx)
	{
		return Inst.localDB.GetLevelIndexDbData(levelIdx);
	}

	public static Dictionary<int, LevelDbData> GetDicLevelIndexDbData()
	{
		return Inst.localDB.GetDicLevelIndexDbData();
	}

	public static Dictionary<int, WaveDbData> GetDicWaveDbData(int levelIndex)
	{
		return Inst.localDB.GetWaveDbData(levelIndex);
	}

	public static MonsterDbData GetMonsterData(int mIdx)
	{
		return Inst.localDB.GetMonsterData(mIdx);
	}

	public static MonsterStatDbData GetMonsterStatData(int monsterIndex)
	{
		return Inst.localDB.GetMonsterStatData(monsterIndex);
	}

	public static MonsterSkillDbData GetMonsterSkillData(int skillIndex)
	{
		return Inst.localDB.GetMonsterSkillData(skillIndex);
	}

	public static Dictionary<int, HunterDbData> GetHunterList()
	{
		return Inst.localDB.GetHunterList();
	}

	public static HunterDbData GetHunterData(int hunterIdx)
	{
		return Inst.localDB.GetHunterData(hunterIdx);
	}

	public static HunterLevelDbData GetHunterLevelData(int hunterIdx, int hunterLevel, int hunterTier = 0)
	{
		return Inst.localDB.GetHunterLevelData(hunterIdx, hunterLevel, hunterTier);
	}

	public static HunterSkillDbData GetHunterSkillData(int skillIndex)
	{
		return Inst.localDB.GetHunterSkillData(skillIndex);
	}

	public static HunterLeaderSkillDbData GetHunterLeaderSkillData(int skillIndex)
	{
		return Inst.localDB.GetHunterLeaderSkillData(skillIndex);
	}

	public static HunterInfo GetHunterInfo(int hunterIdx, int hunterLevel, int hunterTier = 0)
	{
		HunterInfo hunterInfo = new HunterInfo();
		hunterInfo.Hunter = GetHunterData(hunterIdx);
		hunterInfo.Stat = GetHunterLevelData(hunterIdx, hunterLevel, hunterTier);
		MWLog.Log("info.Hunter.skillIdx = " + hunterInfo.Hunter.skillIdx);
		hunterInfo.Skill = GetHunterSkillData(hunterInfo.Hunter.skillIdx);
		return hunterInfo;
	}

	public static string GetHunterTribeName(int hunterAllyIdx)
	{
		return Inst.localDB.GetHunterTribeData(hunterAllyIdx);
	}

	public static HunterColorDbData GetHunterColorName(int hunterColorIdx)
	{
		return Inst.localDB.GetHunterColorData(hunterColorIdx);
	}

	public static HunterPromotionDbData GetHunterPromotionData(int hunterColor, int hunterMaxTier, int hunterTier)
	{
		return Inst.localDB.GetHunterPromotionData(hunterColor, hunterMaxTier, hunterTier);
	}

	public static List<StoreDbData> GetStoreListForStage(int stage)
	{
		return Inst.localDB.GetStoreListForStage(stage);
	}

	public static StoreDbData GetStoreData(int storeIdx)
	{
		return Inst.localDB.GetStoreData(storeIdx);
	}

	public static StoreProduceDbData GetStoreProduceData(int storeIdx, int storeTier)
	{
		return Inst.localDB.GetStoreProduceData(storeIdx, storeTier);
	}

	public static StoreUpgradeDbData GetStoreUpgradeData(int storeIdx, int storeTier)
	{
		return Inst.localDB.GetStoreUpgradeData(storeIdx, storeTier);
	}

	public static ItemListDbData GetItemListData(int itemIdx)
	{
		return Inst.localDB.GetItemListData(itemIdx);
	}

	public static BoostItemDbData GetBoostItemData(int itemIdx)
	{
		return Inst.localDB.GetBoostItemData(itemIdx);
	}

	public static Dictionary<int, ChestDbData> GetChestData()
	{
		return Inst.localDB.GetChestData();
	}

	public static List<ChestListDbData_Dummy> GetChestListData(int chestIdx)
	{
		return Inst.localDB.GetChestListData(chestIdx);
	}

	public static Dictionary<int, ShopDailyDbData> GetShopDailyData()
	{
		return Inst.localDB.GetShopDailyData();
	}

	public static Dictionary<int, ShopCoinDbData> GetShopCoinData()
	{
		return Inst.localDB.GetShopCoinData();
	}

	public static Dictionary<int, ShopJewelDbData> GetShopJewelData()
	{
		return Inst.localDB.GetShopJewelData();
	}

	public static UserLevelDbData GetUserLevelData(int userLevel)
	{
		return Inst.localDB.GetUserLevelData(userLevel);
	}

	public static Dictionary<int, Dictionary<int, TutorialDbData>> GetDicTutorialDbData()
	{
		return Inst.localDB.GetDicTutorialData();
	}

	public static Dictionary<int, Dictionary<int, ScenarioDbData>> GetDicScenarioDbData()
	{
		return Inst.localDB.GetDicScenarioDbData();
	}

	public static Dictionary<int, Dictionary<int, ScenarioDbData>> GetDicScenarioInGameDbData()
	{
		return Inst.localDB.GetDicScenarioInGameDbData();
	}

	public static bool HasUserHunterNew(int hunterIdx)
	{
		UserHunterData[] huntersOwnInfo = GameInfo.userData.huntersOwnInfo;
		foreach (UserHunterData userHunterData in huntersOwnInfo)
		{
			if (userHunterData.hunterIdx == hunterIdx)
			{
				return userHunterData.isNew;
			}
		}
		UserHunterData[] huntersUseInfo = GameInfo.userData.huntersUseInfo;
		foreach (UserHunterData userHunterData2 in huntersUseInfo)
		{
			if (userHunterData2.hunterIdx == hunterIdx)
			{
				return userHunterData2.isNew;
			}
		}
		UserHunterData[] huntersArenaUseInfo = GameInfo.userData.huntersArenaUseInfo;
		foreach (UserHunterData userHunterData3 in huntersArenaUseInfo)
		{
			if (userHunterData3.hunterIdx == hunterIdx)
			{
				return userHunterData3.isNew;
			}
		}
		return false;
	}

	public static int HasUserHunterEnchant(int hunterIdx)
	{
		UserHunterData[] huntersOwnInfo = GameInfo.userData.huntersOwnInfo;
		foreach (UserHunterData userHunterData in huntersOwnInfo)
		{
			if (userHunterData.hunterIdx == hunterIdx)
			{
				return userHunterData.hunterEnchant;
			}
		}
		UserHunterData[] huntersUseInfo = GameInfo.userData.huntersUseInfo;
		foreach (UserHunterData userHunterData2 in huntersUseInfo)
		{
			if (userHunterData2.hunterIdx == hunterIdx)
			{
				return userHunterData2.hunterEnchant;
			}
		}
		UserHunterData[] huntersArenaUseInfo = GameInfo.userData.huntersArenaUseInfo;
		foreach (UserHunterData userHunterData3 in huntersArenaUseInfo)
		{
			if (userHunterData3.hunterIdx == hunterIdx)
			{
				return userHunterData3.hunterEnchant;
			}
		}
		return 0;
	}

	public static void UpdateUserData()
    {
        ChangeUserData?.Invoke();
        if (GameInfo.userData.userInfo.levelUpYn == "y")
		{
			UserLevelUp();
		}
    }

	public static void UpdateStoreData()
    {
        ChangeStoreData?.Invoke();
        if (GameInfo.userData.userInfo.levelUpYn == "y")
		{
			UserLevelUp();
		}
    }

	public static void SaveUserData()
	{
		MWLog.LogError("You have tried the local save. server, server!!!!");
	}

	public static void LoadUserData()
	{
		MWLog.LogError("You have tried the local save. server, server!!!!");
	}

	public static void SaveInGameBattleSpeed(float _value)
	{
		PlayerPrefs.SetFloat("InGameBattleSpeed", _value);
	}

	public static float GetInGameBattleSpeed()
	{
		return PlayerPrefs.GetFloat("InGameBattleSpeed", 1f);
	}

	public static void ClearUserData()
	{
		ObscuredPrefs.DeleteKey("UserData");
		LoadUserData();
	}

	public static void SwitchHunterFromUseToUse(int fromIndex, int toIndex, HUNTERLIST_TYPE hunterListType)
	{
		if (hunterListType == HUNTERLIST_TYPE.NORMAL)
		{
			UserHunterData userHunterData = GameInfo.userData.huntersUseInfo[fromIndex];
			GameInfo.userData.huntersUseInfo[fromIndex] = GameInfo.userData.huntersUseInfo[toIndex];
			GameInfo.userData.huntersUseInfo[toIndex] = userHunterData;
		}
		else
		{
			UserHunterData userHunterData2 = GameInfo.userData.huntersArenaUseInfo[fromIndex];
			GameInfo.userData.huntersArenaUseInfo[fromIndex] = GameInfo.userData.huntersArenaUseInfo[toIndex];
			GameInfo.userData.huntersArenaUseInfo[toIndex] = userHunterData2;
		}
	}

	public static void SwitchHunterFromOwnToUse(int ownIndex, int useIndex, HUNTERLIST_TYPE hunterListType)
	{
		if (hunterListType == HUNTERLIST_TYPE.NORMAL)
		{
			UserHunterData userHunterData = GameInfo.userData.huntersOwnInfo[ownIndex];
			GameInfo.userData.huntersOwnInfo[ownIndex] = GameInfo.userData.huntersUseInfo[useIndex];
			GameInfo.userData.huntersUseInfo[useIndex] = userHunterData;
		}
		else
		{
			UserHunterData userHunterData2 = GameInfo.userData.huntersOwnInfo[ownIndex];
			GameInfo.userData.huntersOwnInfo[ownIndex] = GameInfo.userData.huntersArenaUseInfo[useIndex];
			GameInfo.userData.huntersArenaUseInfo[useIndex] = userHunterData2;
		}
	}

	public static int GetLevelStarCount(int stageIdx, int chapterIdx, int levelIdx)
	{
		if (stageIdx <= GameInfo.userData.userStageState.Length && chapterIdx <= GameInfo.userData.userStageState[stageIdx - 1].chapterList.Length)
		{
			if (!GameInfo.userData.userStageState[stageIdx - 1].chapterList[chapterIdx - 1].isOpen)
			{
				return -1;
			}
			UserLevelState[] levelList = GameInfo.userData.userStageState[stageIdx - 1].chapterList[chapterIdx - 1].levelList;
			UserLevelState[] array = levelList;
			foreach (UserLevelState userLevelState in array)
			{
				if (userLevelState.level == levelIdx)
				{
					return userLevelState.starCount;
				}
			}
		}
		return -1;
	}

	public static int GetUserClearStarCount()
	{
		int num = 0;
		UserStageState[] userStageState = GameInfo.userData.userStageState;
		UserStageState[] array = userStageState;
		foreach (UserStageState userStageState2 in array)
		{
			UserChapterState[] chapterList = userStageState2.chapterList;
			UserChapterState[] array2 = chapterList;
			foreach (UserChapterState userChapterState in array2)
			{
				UserLevelState[] levelList = userChapterState.levelList;
				UserLevelState[] array3 = levelList;
				foreach (UserLevelState userLevelState in array3)
				{
					num += userLevelState.starCount;
				}
			}
		}
		return num;
	}

	public static void ShowNetworkLoading(bool isSceneLoading = false)
    {
        if (Inst == null) return;
		MWLog.Log("ShowNetworkLoading");
		Inst.goNetworkLoading.SetActive(value: true);
		if (isSceneLoading)
		{
			Inst.goSceneLoading.SetActive(value: true);
		}
	}

	public static void HideNetworkLoading()
	{
        if (Inst == null) return;
        Inst.goNetworkLoading.SetActive(value: false);
	}

	public static void HideSceneLoading()
	{
        Inst.goSceneLoading.SetActive(value: false);
	}

	public static void UserLevelUp()
	{
		Inst.userLevelUp.Show();
		GameInfo.userData.userInfo.levelUpYn = "n";
	}

	public static void MoveScene(SceneType type)
	{
		if (Inst.coroutineSceneLoading != null)
		{
			Inst.StopCoroutine(Inst.coroutineSceneLoading);
			Inst.coroutineSceneLoading = null;
		}
		Inst.coroutineSceneLoading = Inst.StartCoroutine(Inst.LoadSceneProgress(type));
	}

	public static void ShowInGameDescription()
	{
		if (GameInfo.userData.userInfo.oldUserYn == "y")
		{
			Inst.goInGameRule.SetActive(value: true);
			InGamePlayManager.TouchLock();
			InGamePlayManager.PuzzleControlLock();
			GameInfo.userData.userInfo.oldUserYn = "n";
		}
	}

	public static void AddUserJewel(int _addJewel)
	{
		GameInfo.userData.userInfo.jewel += _addJewel;
		UpdateUserData();
	}

	public static bool UseUserJewel(int _useJewel)
	{
		if (GameInfo.userData.userInfo.jewel < _useJewel)
		{
			return false;
		}
		GameInfo.userData.userInfo.jewel -= _useJewel;
		UpdateUserData();
		return true;
	}

	public static void SaveScenarioIntroShow(int _index)
	{
		PlayerPrefs.SetInt($"ScenarioIntro_{_index}", 1);
	}

	public static bool LoadScenarioIntroShow(int _index)
	{
		MWLog.Log("LoadScenarioIntroShow - " + _index + " :: " + PlayerPrefs.GetInt($"ScenarioIntro_{_index}", 0));
		return PlayerPrefs.GetInt($"ScenarioIntro_{_index}", 0) == 0;
	}

	public static void SaveScenarioInGameShow(int _index)
	{
		PlayerPrefs.SetInt($"ScenarioInGame_{_index}", 1);
	}

	public static bool LoadScenarioInGameShow(int _index)
	{
		MWLog.Log("LoadScenarioIntroShow - " + _index + " :: " + PlayerPrefs.GetInt($"ScenarioInGame_{_index}", 0));
		return PlayerPrefs.GetInt($"ScenarioInGame_{_index}", 0) == 0;
	}

	public static void StartGame()
	{
		if (LoadScenarioIntroShow(1) && TutorialManager.Intro)
		{
			HideSceneLoading();
			ScenarioManager.EndScenarioEvent = Inst.OnEndScenarioEvent;
			ScenarioManager.Show(1);
		}
		else if (LoadScenarioIntroShow(2) && TutorialManager.SIdx == 1)
		{
			HideSceneLoading();
			ScenarioManager.EndScenarioEvent = Inst.OnEndScenarioEvent;
			ScenarioManager.Show(2);
		}
		else if (LoadScenarioIntroShow(3) && TutorialManager.SIdx == 1)
		{
			HideSceneLoading();
			ScenarioManager.EndScenarioEvent = Inst.OnEndScenarioEvent;
			ScenarioManager.Show(3);
		}
		else
		{
			MoveScene(SceneType.Lobby);
		}
	}

	public static void ShowScenario(int _index)
	{
		HideSceneLoading();
		ScenarioManager.EndScenarioEvent = Inst.OnEndScenarioEvent;
		ScenarioManager.Show(_index);
	}

	private IEnumerator ProcessSceneLoadingHide()
	{
		yield return null;
		goSceneLoading.SetActive(value: false);
	}

	private IEnumerator LoadSceneProgress(SceneType type)
	{
		MWPoolManager.DeSpawnPoolAll("Skill");
		MWPoolManager.DeSpawnPoolAll("Effect");
		MWPoolManager.DeSpawnPoolAll("Monster");
		MWPoolManager.DeSpawnPoolAll("Hunter");
		MWPoolManager.DeSpawnPoolAll("Puzzle");
		MWPoolManager.DeSpawnPoolAll("Stage");
		MWPoolManager.DeSpawnPoolAll("Grow");
		MWPoolManager.DeSpawnPoolAll("Info");
		MWPoolManager.DeSpawnPoolAll("Item");
		MWPoolManager.DeSpawnPoolAll("Scenario");
		MWPoolManager.DeSpawnPoolAll("Lobby");
		goSceneLoading.SetActive(value: true);
		if (type == SceneType.Lobby)
		{
			goNetworkLoading.SetActive(value: true);
		}
		GameInfo.currentSceneType = type;
		goSceneLoading.GetComponent<Animator>();
		AsyncOperation async = SceneManager.LoadSceneAsync(type.ToString());
		while (!async.isDone)
		{
			async.allowSceneActivation = ((double)async.progress > 0.8);
			yield return null;
		}
		while (goNetworkLoading.activeSelf)
		{
			yield return null;
		}
		goSceneLoading.SetActive(value: false);
		Inst.coroutineSceneLoading = null;
	}

	private void CheckUserDataClear()
	{
		if (isUserDataClear)
		{
			ObscuredPrefs.DeleteAll();
			PlayerPrefs.DeleteAll();
		}
	}

	private void OnEndScenarioEvent(int _index)
	{
		MWLog.Log("OnEndScenarioEvent");
		ScenarioManager.EndScenarioEvent = null;
		switch (_index)
		{
		case 1:
			MoveScene(SceneType.Lobby);
			break;
		case 2:
			ShowScenario(3);
			break;
		case 3:
			MoveScene(SceneType.Lobby);
			break;
		}
	}

	public void HideInGameNewRule()
	{
		goInGameRule.SetActive(value: false);
		InGamePlayManager.TouchActive();
		InGamePlayManager.PuzzleControlStart();
	}

	protected override void Awake()
	{
		base.Awake();
		CheckUserDataClear();
		localDB = base.gameObject.GetComponent<LocalDB>();
		gameResourceData = base.gameObject.GetComponent<GameResourceData>();
		GameInfo.inGameBattleSpeedRate = GetInGameBattleSpeed();
	}

	private void Start()
	{
		localDB.CallLocalDbData(() =>
        {
            loadingText.SetActive(value: true);
            Protocol_Set.Protocol_check_version_Req();
            GameInfo.chargeEnergyAdsValue = GetGameConfigData(ConfigDataType.UserGetenergyDefault);
        });
	}
}
