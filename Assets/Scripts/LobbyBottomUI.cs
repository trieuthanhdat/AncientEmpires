

using UnityEngine;

public class LobbyBottomUI : MonoBehaviour
{
	[SerializeField]
	private Transform trBattleBtn;

	[SerializeField]
	private Transform trHunterNoticeAnchor;

	[SerializeField]
	private Transform trChestNoticeAnchor;

	[SerializeField]
	private Transform trShopNoticeAnchor;

	[SerializeField]
	private Transform trArenaNoticeAnchor;

	[SerializeField]
	private GameObject goArenaLock;

	[SerializeField]
	private Transform trArenaButton;

	private Transform trHunterNotice;

	private Transform trChestNotice;

	private Transform trShopNotice;

	private Transform trArenaNotice;

	public Transform BattleButton => trBattleBtn;

	public Transform ArenaButton => trArenaButton;

	public void RefreshNotice()
	{
		MWLog.Log("Alert 00");
		goArenaLock.SetActive(GameInfo.userData.userStageState[0].chapterList.Length < 4);
		SetHunterNotice(LobbyManager.CheckHunterAlert());
		SetChestNotice(LobbyManager.CheckChestAlert());
		SetShopNotice(GameInfo.userData.userInfo.dailyShopNewYn == "y");
		SetArenaNotice();
	}

	public void Exit()
	{
		SetHunterNotice(isShow: false);
		SetChestNotice(isShow: false);
		SetShopNotice(isShow: false);
		if (trArenaNotice != null)
		{
			MWPoolManager.DeSpawn("Lobby", trArenaNotice);
			trArenaNotice = null;
		}
	}

	private void SetHunterNotice(bool isShow)
	{
		if (trHunterNotice != null)
		{
			MWPoolManager.DeSpawn("Lobby", trHunterNotice);
			trHunterNotice = null;
		}
		if (isShow)
		{
			trHunterNotice = MWPoolManager.Spawn("Lobby", "Notice_Green", trHunterNoticeAnchor);
		}
	}

	private void SetChestNotice(bool isShow)
	{
		if (trChestNotice != null)
		{
			MWPoolManager.DeSpawn("Lobby", trChestNotice);
			trChestNotice = null;
		}
		if (isShow)
		{
			trChestNotice = MWPoolManager.Spawn("Lobby", "Notice_Red", trChestNoticeAnchor);
		}
	}

	private void SetShopNotice(bool isShow)
	{
		if (trShopNotice != null)
		{
			MWPoolManager.DeSpawn("Lobby", trShopNotice);
			trShopNotice = null;
		}
		if (isShow && GameInfo.userData.userInfo.level > 2)
		{
			trShopNotice = MWPoolManager.Spawn("Lobby", "Notice_Red", trShopNoticeAnchor);
		}
	}

	private void SetArenaNotice()
	{
		if (!goArenaLock.activeSelf)
		{
			if (trArenaNotice != null)
			{
				MWPoolManager.DeSpawn("Lobby", trArenaNotice);
				trArenaNotice = null;
			}
			if (GameInfo.userData.userInfo.arenaAlarmYn == "y" && GameInfo.userData.userInfo.arenaTicket > 0)
			{
				trArenaNotice = MWPoolManager.Spawn("Lobby", "Notice_Red", trArenaNoticeAnchor);
			}
		}
	}

	private void OnDisable()
	{
		SetHunterNotice(isShow: false);
		SetChestNotice(isShow: false);
		SetShopNotice(isShow: false);
		if (trArenaNotice != null)
		{
			MWPoolManager.DeSpawn("Lobby", trArenaNotice);
			trArenaNotice = null;
		}
	}
}
