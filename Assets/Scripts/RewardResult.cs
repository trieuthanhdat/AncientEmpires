

using System;
using UnityEngine;

public class RewardResult : LobbyPopupBase
{
	public enum RewardResultType
	{
		DailyBonus,
		BundlePack
	}

	public Action<RewardResultType> GoBackEvent;

	public Action RewardComplete;

	[SerializeField]
	private Transform trItemAnchor;

	private RewardResultType rewardResultType;

	public void Show(RewardResultType _type, ChestListDbData[] _arrData)
	{
		base.Show();
		rewardResultType = _type;
		Init(_arrData);
	}

	public override void Hide()
	{
		base.Hide();
	}

	public override void HideProcessComplete()
	{
	}

	private void Init(ChestListDbData[] _arrData)
	{
		foreach (ChestListDbData data in _arrData)
		{
			ChestResultItem component = MWPoolManager.Spawn("Item", "Item02", trItemAnchor).GetComponent<ChestResultItem>();
			component.Init(data);
			component.transform.localPosition = Vector3.zero;
			component.transform.localScale = Vector3.one;
		}
	}

	public void OnClickConfirm()
	{
		if (GoBackEvent != null)
		{
			GoBackEvent(rewardResultType);
		}
		SoundController.EffectSound_Play(EffectSoundType.ButtonClick);
	}

	private void OnDisable()
	{
		ChestResultItem[] componentsInChildren = trItemAnchor.GetComponentsInChildren<ChestResultItem>(includeInactive: true);
		foreach (ChestResultItem chestResultItem in componentsInChildren)
		{
			MWPoolManager.DeSpawn("Item", chestResultItem.transform);
		}
	}
}
