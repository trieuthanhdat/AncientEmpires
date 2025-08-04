

using System;
using UnityEngine;
using UnityEngine.UI;

public class DeckEdit : LobbyPopupBase
{
	public Action GoBackEvent;

	[SerializeField]
	private Transform trOwnHunterContent;

	[SerializeField]
	private Transform trUseHunterContent;

	[SerializeField]
	private HunterCard SelectHunter_1;

	[SerializeField]
	private HunterCard SelectHunter_2;

	[SerializeField]
	private Text totalHealth_Text;

	[SerializeField]
	private Text totalAttack_Text;

	[SerializeField]
	private Text totalRecovery_Text;

	[SerializeField]
	private Text leaderSkill_Text;

	[SerializeField]
	private int totalHealth_current;

	[SerializeField]
	private int totalAttack_current;

	[SerializeField]
	private int totalRecovery_current;

	[SerializeField]
	private int totalHealth_after;

	[SerializeField]
	private int totalAttack_after;

	[SerializeField]
	private int totalRecovery_after;

	[SerializeField]
	private GameObject healthArrow_Up;

	[SerializeField]
	private GameObject healthArrow_Down;

	[SerializeField]
	private GameObject attackArrow_Up;

	[SerializeField]
	private GameObject attackArrow_Down;

	[SerializeField]
	private GameObject recoveryArrow_Up;

	[SerializeField]
	private GameObject recoveryArrow_Down;

	[SerializeField]
	private Transform huntercardSelect;

	[SerializeField]
	private Transform huntercardChange1;

	[SerializeField]
	private Transform huntercardChange2;

	[SerializeField]
	private UserHunterData[] origin_UseHunterData;

	[SerializeField]
	private UserHunterData[] origin_OwnHunterData;

	[SerializeField]
	private Transform trGetHunter1;

	[SerializeField]
	private Transform trGetHunter2;

	[SerializeField]
	private HUNTERLIST_TYPE hunterListType;

	public HUNTERLIST_TYPE EditType => hunterListType;

	public Transform GetHunter1 => trGetHunter1;

	public Transform GetHunter2 => trGetHunter2;

	public void Show(HUNTERLIST_TYPE _listType)
	{
		base.Show();
		base.gameObject.SetActive(value: true);
		Init(_listType);
		MWLog.Log("_listType = " + _listType);
	}

	public override void Hide()
	{
		base.Hide();
	}

	public override void HideProcessComplete()
	{
		HunterCard[] componentsInChildren = trOwnHunterContent.GetComponentsInChildren<HunterCard>();
		foreach (HunterCard hunterCard in componentsInChildren)
		{
			MWPoolManager.DeSpawn("Hunter", hunterCard.transform);
		}
		HunterCard[] componentsInChildren2 = trUseHunterContent.GetComponentsInChildren<HunterCard>();
		foreach (HunterCard hunterCard2 in componentsInChildren2)
		{
			MWPoolManager.DeSpawn("Hunter", hunterCard2.transform);
		}
		if (huntercardChange1 != null)
		{
			MWPoolManager.DeSpawn("Effect", huntercardChange1);
		}
		if (huntercardChange2 != null)
		{
			MWPoolManager.DeSpawn("Effect", huntercardChange2);
		}
	}

	public void OnSelect_HunterCardForTutorial(HunterCard _hunterCard)
	{
		OnSelect_HunterCard(_hunterCard);
	}

	private void Init(HUNTERLIST_TYPE _listType)
	{
		hunterListType = _listType;
		GameUtil.SetOwnHunterList(hunterListType);
		origin_UseHunterData = null;
		origin_OwnHunterData = null;
		if (hunterListType == HUNTERLIST_TYPE.NORMAL)
		{
			origin_UseHunterData = new UserHunterData[GameInfo.userData.huntersUseInfo.Length];
			for (int i = 0; i < origin_UseHunterData.Length; i++)
			{
				origin_UseHunterData[i] = GameInfo.userData.huntersUseInfo[i];
			}
		}
		else
		{
			origin_UseHunterData = new UserHunterData[GameInfo.userData.huntersArenaUseInfo.Length];
			for (int j = 0; j < origin_UseHunterData.Length; j++)
			{
				origin_UseHunterData[j] = GameInfo.userData.huntersArenaUseInfo[j];
			}
		}
		origin_OwnHunterData = new UserHunterData[GameInfo.userData.huntersOwnInfo.Length];
		for (int k = 0; k < origin_OwnHunterData.Length; k++)
		{
			origin_OwnHunterData[k] = GameInfo.userData.huntersOwnInfo[k];
		}
		SelectHunter_1 = null;
		SelectHunter_2 = null;
		huntercardSelect.gameObject.SetActive(value: false);
		for (int l = 0; l < GameInfo.userData.huntersOwnInfo.Length; l++)
		{
			HunterCard component = MWPoolManager.Spawn("Hunter", "HunterCard_" + GameInfo.userData.huntersOwnInfo[l].hunterIdx, trOwnHunterContent).GetComponent<HunterCard>();
			if (hunterListType == HUNTERLIST_TYPE.NORMAL)
			{
				component.Init(HUNTERCARD_TYPE.DECK, GameDataManager.GetHunterInfo(GameInfo.userData.huntersOwnInfo[l].hunterIdx, GameInfo.userData.huntersOwnInfo[l].hunterLevel, GameInfo.userData.huntersOwnInfo[l].hunterTier), _isOwn: true, _isArena: false);
			}
			else
			{
				component.Init(HUNTERCARD_TYPE.DECK, GameDataManager.GetHunterInfo(GameInfo.userData.huntersOwnInfo[l].hunterIdx, GameInfo.userData.huntersOwnInfo[l].hunterLevel, GameInfo.userData.huntersOwnInfo[l].hunterTier), _isOwn: true, _isArena: true);
			}
			component.HunterIdx = l;
			component.IsUseHunter = false;
			component.transform.localPosition = Vector3.zero;
			component.transform.localScale = Vector3.one;
			component.Select_HunterCard = OnSelect_HunterCard;
			component.DeSelect_HunterCard = OnDeSelect_HunterCard;
		}
		if (hunterListType == HUNTERLIST_TYPE.NORMAL)
		{
			for (int m = 0; m < GameInfo.userData.huntersUseInfo.Length; m++)
			{
				HunterCard component2 = MWPoolManager.Spawn("Hunter", "HunterCard_" + GameInfo.userData.huntersUseInfo[m].hunterIdx, trUseHunterContent).GetComponent<HunterCard>();
				component2.Init(HUNTERCARD_TYPE.DECK, GameDataManager.GetHunterInfo(GameInfo.userData.huntersUseInfo[m].hunterIdx, GameInfo.userData.huntersUseInfo[m].hunterLevel, GameInfo.userData.huntersUseInfo[m].hunterTier), _isOwn: true, _isArena: false);
				component2.HunterIdx = m;
				component2.IsUseHunter = true;
				component2.transform.localPosition = Vector3.zero;
				component2.transform.localScale = Vector3.one;
				component2.Select_HunterCard = OnSelect_HunterCard;
				component2.DeSelect_HunterCard = OnDeSelect_HunterCard;
				if (m == 0)
				{
					if (component2.HunterInfo.Stat.hunterLeaderSkill == 0)
					{
						leaderSkill_Text.text = string.Format(MWLocalize.GetData("Popup_hunter_leaderskill_02"));
					}
					else
					{
						leaderSkill_Text.text = string.Format(MWLocalize.GetData(GameDataManager.GetHunterLeaderSkillData(component2.HunterInfo.Stat.hunterLeaderSkill).leaderSkillDescription));
					}
				}
				if (LobbyManager.OpenDeckEdit != null)
				{
					if (m == 0)
					{
						trGetHunter2 = component2.transform;
					}
					if (m == 4)
					{
						trGetHunter1 = component2.transform;
					}
				}
			}
		}
		else
		{
			for (int n = 0; n < GameInfo.userData.huntersArenaUseInfo.Length; n++)
			{
				HunterCard component3 = MWPoolManager.Spawn("Hunter", "HunterCard_" + GameInfo.userData.huntersArenaUseInfo[n].hunterIdx, trUseHunterContent).GetComponent<HunterCard>();
				component3.Init(HUNTERCARD_TYPE.DECK, GameDataManager.GetHunterInfo(GameInfo.userData.huntersArenaUseInfo[n].hunterIdx, GameInfo.userData.huntersArenaUseInfo[n].hunterLevel, GameInfo.userData.huntersArenaUseInfo[n].hunterTier), _isOwn: true, _isArena: true);
				component3.HunterIdx = n;
				component3.IsUseHunter = true;
				component3.transform.localPosition = Vector3.zero;
				component3.transform.localScale = Vector3.one;
				component3.Select_HunterCard = OnSelect_HunterCard;
				component3.DeSelect_HunterCard = OnDeSelect_HunterCard;
				if (n == 0)
				{
					if (component3.HunterInfo.Stat.hunterLeaderSkill == 0)
					{
						leaderSkill_Text.text = string.Format(MWLocalize.GetData("Popup_hunter_leaderskill_02"));
					}
					else
					{
						leaderSkill_Text.text = string.Format(MWLocalize.GetData(GameDataManager.GetHunterLeaderSkillData(component3.HunterInfo.Stat.hunterLeaderSkill).leaderSkillDescription));
					}
				}
				if (LobbyManager.OpenDeckEdit != null)
				{
					if (n == 0)
					{
						trGetHunter2 = component3.transform;
					}
					if (n == 4)
					{
						trGetHunter1 = component3.transform;
					}
				}
			}
		}
		totalHealth_current = 0;
		totalAttack_current = 0;
		totalRecovery_current = 0;
		totalHealth_after = 0;
		totalAttack_after = 0;
		totalRecovery_after = 0;
		SetTotalHunterStat(_isInit: true);
		if (LobbyManager.OpenDeckEdit != null)
		{
			LobbyManager.OpenDeckEdit();
		}
	}

	private int CheckArenaBuff(HunterInfo _hunterinfo)
	{
		int num = 1;
		if (hunterListType != HUNTERLIST_TYPE.NORMAL)
		{
			if (GameInfo.inGamePlayData.arenaInfo == null)
			{
				return num;
			}
			if (GameInfo.inGamePlayData.arenaInfo.color == _hunterinfo.Hunter.color)
			{
				num *= GameInfo.inGamePlayData.arenaInfo.color_buff;
			}
			if (GameInfo.inGamePlayData.arenaInfo.tribe == _hunterinfo.Hunter.hunterTribe)
			{
				num *= GameInfo.inGamePlayData.arenaInfo.tribe_buff;
			}
		}
		return num;
	}

	private void SetTotalHunterStat(bool _isInit)
	{
		healthArrow_Up.SetActive(value: false);
		healthArrow_Down.SetActive(value: false);
		attackArrow_Up.SetActive(value: false);
		attackArrow_Down.SetActive(value: false);
		recoveryArrow_Up.SetActive(value: false);
		recoveryArrow_Down.SetActive(value: false);
		totalHealth_after = 0;
		totalAttack_after = 0;
		totalRecovery_after = 0;
		for (int i = 0; i < trUseHunterContent.childCount; i++)
		{
			HunterInfo hunterInfo = trUseHunterContent.GetChild(i).GetComponent<HunterCard>().HunterInfo;
			if (_isInit)
			{
				totalHealth_current += (int)GameUtil.GetHunterReinForceHP(hunterInfo.Stat.hunterHp, GameDataManager.HasUserHunterEnchant(hunterInfo.Hunter.hunterIdx));
				totalAttack_current += CheckArenaBuff(hunterInfo) * (int)GameUtil.GetHunterReinForceAttack(hunterInfo.Stat.hunterAttack, GameDataManager.HasUserHunterEnchant(hunterInfo.Hunter.hunterIdx));
				totalRecovery_current += (int)GameUtil.GetHunterReinForceHeal(hunterInfo.Stat.hunterRecovery, GameDataManager.HasUserHunterEnchant(hunterInfo.Hunter.hunterIdx));
			}
			else
			{
				totalHealth_after += (int)GameUtil.GetHunterReinForceHP(hunterInfo.Stat.hunterHp, GameDataManager.HasUserHunterEnchant(hunterInfo.Hunter.hunterIdx));
				totalAttack_after += CheckArenaBuff(hunterInfo) * (int)GameUtil.GetHunterReinForceAttack(hunterInfo.Stat.hunterAttack, GameDataManager.HasUserHunterEnchant(hunterInfo.Hunter.hunterIdx));
				totalRecovery_after += (int)GameUtil.GetHunterReinForceHeal(hunterInfo.Stat.hunterRecovery, GameDataManager.HasUserHunterEnchant(hunterInfo.Hunter.hunterIdx));
			}
		}
		if (_isInit)
		{
			totalHealth_Text.text = "<color=#ffffff>" + string.Format(MWLocalize.GetData("popup_ingame_level_text_health"), StatTranslate(totalHealth_current)) + "</color>";
			totalAttack_Text.text = "<color=#ffffff>" + string.Format(MWLocalize.GetData("popup_ingame_level_text_attack"), StatTranslate(totalAttack_current)) + "</color>";
			totalRecovery_Text.text = "<color=#ffffff>" + string.Format(MWLocalize.GetData("popup_ingame_level_text_recovery"), StatTranslate(totalRecovery_current)) + "</color>";
			return;
		}
		if (totalHealth_current < totalHealth_after)
		{
			healthArrow_Up.SetActive(value: true);
			totalHealth_Text.text = "<color=#19c6ff>" + string.Format(MWLocalize.GetData("popup_ingame_level_text_health"), StatTranslate(totalHealth_after)) + "</color>";
		}
		else if (totalHealth_current > totalHealth_after)
		{
			healthArrow_Down.SetActive(value: true);
			totalHealth_Text.text = "<color=#ff0000>" + string.Format(MWLocalize.GetData("popup_ingame_level_text_health"), StatTranslate(totalHealth_after)) + "</color>";
		}
		else
		{
			totalHealth_Text.text = "<color=#ffffff>" + string.Format(MWLocalize.GetData("popup_ingame_level_text_health"), StatTranslate(totalHealth_after)) + "</color>";
		}
		if (totalAttack_current < totalAttack_after)
		{
			attackArrow_Up.SetActive(value: true);
			totalAttack_Text.text = "<color=#19c6ff>" + string.Format(MWLocalize.GetData("popup_ingame_level_text_attack"), StatTranslate(totalAttack_after)) + "</color>";
		}
		else if (totalAttack_current > totalAttack_after)
		{
			attackArrow_Down.SetActive(value: true);
			totalAttack_Text.text = "<color=#ff0000>" + string.Format(MWLocalize.GetData("popup_ingame_level_text_attack"), StatTranslate(totalAttack_after)) + "</color>";
		}
		else
		{
			totalAttack_Text.text = "<color=#ffffff>" + string.Format(MWLocalize.GetData("popup_ingame_level_text_attack"), StatTranslate(totalAttack_after)) + "</color>";
		}
		if (totalRecovery_current < totalRecovery_after)
		{
			recoveryArrow_Up.SetActive(value: true);
			totalRecovery_Text.text = "<color=#19c6ff>" + string.Format(MWLocalize.GetData("popup_ingame_level_text_recovery"), StatTranslate(totalRecovery_after)) + "</color>";
		}
		else if (totalRecovery_current > totalRecovery_after)
		{
			recoveryArrow_Down.SetActive(value: true);
			totalRecovery_Text.text = "<color=#ff0000>" + string.Format(MWLocalize.GetData("popup_ingame_level_text_recovery"), StatTranslate(totalRecovery_after)) + "</color>";
		}
		else
		{
			totalRecovery_Text.text = "<color=#ffffff>" + string.Format(MWLocalize.GetData("popup_ingame_level_text_recovery"), StatTranslate(totalRecovery_after)) + "</color>";
		}
	}

	private string StatTranslate(float _stat)
	{
		string empty = string.Empty;
		float num = 0f;
		if (_stat >= 1000f)
		{
			num = _stat / 1000f;
			return (Math.Truncate(num * 100f) / 100.0).ToString() + "K";
		}
		return _stat.ToString();
	}

	private void OnSelect_HunterCard(HunterCard _hunterCard)
	{
		if (LobbyManager.OpenDeckEdit != null)
		{
			LobbyManager.OpenDeckEdit();
		}
		bool flag = true;
		if (SelectHunter_1 == null)
		{
			flag = true;
			MWLog.Log("Select Card 11");
			SelectHunter_1 = _hunterCard;
			huntercardSelect.gameObject.SetActive(value: true);
			huntercardSelect.transform.position = SelectHunter_1.transform.position;
		}
		else if (SelectHunter_2 == null)
		{
			MWLog.Log("Select Card 22");
			flag = false;
			SelectHunter_2 = _hunterCard;
			huntercardSelect.gameObject.SetActive(value: true);
			huntercardSelect.transform.position = SelectHunter_2.transform.position;
		}
		if (!(SelectHunter_1 != null) || !(SelectHunter_2 != null))
		{
			return;
		}
		if (SelectHunter_1.IsUseHunter && SelectHunter_2.IsUseHunter)
		{
			SwapCard_UsetoUse();
		}
		else if (!SelectHunter_1.IsUseHunter && !SelectHunter_2.IsUseHunter)
		{
			if (flag)
			{
				MWLog.Log("Cancel Card 11");
				SelectHunter_2.SelectCard_Cancel();
				SelectHunter_2 = null;
			}
			else
			{
				MWLog.Log("Cancel Card 22");
				SelectHunter_1.SelectCard_Cancel();
				SelectHunter_1 = null;
			}
		}
		else
		{
			SwapCard_OwntoUse();
		}
	}

	private void OnDeSelect_HunterCard(HunterCard _hunterCard)
	{
		if (SelectHunter_1 != null && SelectHunter_1.GetInstanceID() == _hunterCard.GetInstanceID())
		{
			MWLog.Log("Deselect Card 11");
			SelectHunter_1 = null;
			huntercardSelect.gameObject.SetActive(value: false);
		}
		if (SelectHunter_2 != null && SelectHunter_2.GetInstanceID() == _hunterCard.GetInstanceID())
		{
			MWLog.Log("Deselect Card 22");
			SelectHunter_2 = null;
			huntercardSelect.gameObject.SetActive(value: false);
		}
	}

	private void SwapCard_OwntoUse()
	{
		MWLog.Log("JY ------------------------ SwapCard_OwntoUse()");
		if (SelectHunter_1.IsUseHunter)
		{
			GameDataManager.SwitchHunterFromOwnToUse(SelectHunter_2.HunterIdx, SelectHunter_1.HunterIdx, hunterListType);
		}
		else
		{
			GameDataManager.SwitchHunterFromOwnToUse(SelectHunter_1.HunterIdx, SelectHunter_2.HunterIdx, hunterListType);
		}
		SwapCard_Complete(_isUseToUse: false);
		SetTotalHunterStat(_isInit: false);
	}

	private void SwapCard_UsetoUse()
	{
		MWLog.Log("JY ------------------------ SwapCard_UsetoUse()");
		GameDataManager.SwitchHunterFromUseToUse(SelectHunter_1.HunterIdx, SelectHunter_2.HunterIdx, hunterListType);
		SwapCard_Complete(_isUseToUse: true);
		SetTotalHunterStat(_isInit: false);
	}

	private void SwapCard_Complete(bool _isUseToUse)
	{
		int siblingIndex = SelectHunter_1.transform.GetSiblingIndex();
		int siblingIndex2 = SelectHunter_2.transform.GetSiblingIndex();
		if (_isUseToUse)
		{
			SelectHunter_1.transform.SetSiblingIndex(siblingIndex2);
			SelectHunter_2.transform.SetSiblingIndex(siblingIndex);
			SelectHunter_1.HunterIdx = siblingIndex2;
			SelectHunter_2.HunterIdx = siblingIndex;
		}
		else
		{
			if (SelectHunter_1.IsUseHunter)
			{
				SelectHunter_1.transform.SetParent(trOwnHunterContent);
				SelectHunter_1.transform.SetSiblingIndex(siblingIndex2);
				SelectHunter_1.HunterIdx = siblingIndex2;
			}
			else
			{
				SelectHunter_1.transform.SetParent(trUseHunterContent);
				SelectHunter_1.transform.SetSiblingIndex(siblingIndex2);
				SelectHunter_1.HunterIdx = siblingIndex2;
			}
			if (SelectHunter_2.IsUseHunter)
			{
				SelectHunter_2.transform.SetParent(trOwnHunterContent);
				SelectHunter_2.transform.SetSiblingIndex(siblingIndex);
				SelectHunter_2.HunterIdx = siblingIndex;
			}
			else
			{
				SelectHunter_2.transform.SetParent(trUseHunterContent);
				SelectHunter_2.transform.SetSiblingIndex(siblingIndex);
				SelectHunter_2.HunterIdx = siblingIndex;
			}
		}
		SelectHunter_1.ChangedUseStatus(_isUseToUse);
		SelectHunter_2.ChangedUseStatus(_isUseToUse);
		huntercardSelect.gameObject.SetActive(value: false);
		huntercardChange1 = MWPoolManager.Spawn("Effect", "FX_cha_select_02", SelectHunter_1.transform, 1f);
		huntercardChange1.localPosition = Vector3.zero;
		huntercardChange2 = MWPoolManager.Spawn("Effect", "FX_cha_select_02", SelectHunter_2.transform, 1f);
		huntercardChange2.localPosition = Vector3.zero;
		SoundController.EffectSound_Play(EffectSoundType.HunterSwitching);
		SelectHunter_1 = null;
		SelectHunter_2 = null;
		if (hunterListType == HUNTERLIST_TYPE.NORMAL)
		{
			if (GameDataManager.GetHunterInfo(GameInfo.userData.huntersUseInfo[0].hunterIdx, GameInfo.userData.huntersUseInfo[0].hunterLevel, GameInfo.userData.huntersUseInfo[0].hunterTier).Stat.hunterLeaderSkill == 0)
			{
				leaderSkill_Text.text = string.Format(MWLocalize.GetData("Popup_hunter_leaderskill_02"));
			}
			else
			{
				leaderSkill_Text.text = string.Format(MWLocalize.GetData(GameDataManager.GetHunterLeaderSkillData(GameDataManager.GetHunterInfo(GameInfo.userData.huntersUseInfo[0].hunterIdx, GameInfo.userData.huntersUseInfo[0].hunterLevel, GameInfo.userData.huntersUseInfo[0].hunterTier).Stat.hunterLeaderSkill).leaderSkillDescription));
			}
		}
		else if (GameDataManager.GetHunterInfo(GameInfo.userData.huntersArenaUseInfo[0].hunterIdx, GameInfo.userData.huntersArenaUseInfo[0].hunterLevel, GameInfo.userData.huntersArenaUseInfo[0].hunterTier).Stat.hunterLeaderSkill == 0)
		{
			leaderSkill_Text.text = string.Format(MWLocalize.GetData("Popup_hunter_leaderskill_02"));
		}
		else
		{
			leaderSkill_Text.text = string.Format(MWLocalize.GetData(GameDataManager.GetHunterLeaderSkillData(GameDataManager.GetHunterInfo(GameInfo.userData.huntersArenaUseInfo[0].hunterIdx, GameInfo.userData.huntersArenaUseInfo[0].hunterLevel, GameInfo.userData.huntersArenaUseInfo[0].hunterTier).Stat.hunterLeaderSkill).leaderSkillDescription));
		}
	}

	private int[] UseHunterArray()
	{
		if (hunterListType == HUNTERLIST_TYPE.NORMAL)
		{
			int[] array = new int[GameInfo.userData.huntersUseInfo.Length];
			for (int i = 0; i < GameInfo.userData.huntersUseInfo.Length; i++)
			{
				array[i] = GameInfo.userData.huntersUseInfo[i].hunterIdx;
			}
			return array;
		}
		int[] array2 = new int[GameInfo.userData.huntersArenaUseInfo.Length];
		for (int j = 0; j < GameInfo.userData.huntersArenaUseInfo.Length; j++)
		{
			array2[j] = GameInfo.userData.huntersArenaUseInfo[j].hunterIdx;
		}
		return array2;
	}

	public void OnClickGoBack()
	{
		SoundController.EffectSound_Play(EffectSoundType.Cancel);
		if (GoBackEvent != null)
		{
			GoBackEvent();
		}
		if (LobbyManager.OpenDeckEdit != null)
		{
			LobbyManager.OpenDeckEdit();
		}
	}

	public void OnClickConfirm()
	{
		SoundController.EffectSound_Play(EffectSoundType.ButtonClick);
		Protocol_Set.Protocol_hunter_change_Req(UseHunterArray(), hunterListType, OnClickGoBack);
	}

	public void OnClickCancel()
	{
		if (hunterListType == HUNTERLIST_TYPE.NORMAL)
		{
			GameInfo.userData.huntersUseInfo = origin_UseHunterData;
		}
		else
		{
			GameInfo.userData.huntersArenaUseInfo = origin_UseHunterData;
		}
		GameInfo.userData.huntersOwnInfo = origin_OwnHunterData;
		GameDataManager.LoadUserData();
		SoundController.EffectSound_Play(EffectSoundType.Cancel);
		if (GoBackEvent != null)
		{
			GoBackEvent();
		}
	}
}
