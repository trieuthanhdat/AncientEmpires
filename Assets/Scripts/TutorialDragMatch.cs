

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDragMatch : MonoBehaviour
{
	private const float DRAG_MOVE_COMPLETE_DELAY_DURATION = 0.3f;

	private bool isFirstBlockSelect;

	private Transform trHand;

	private Transform trBlockTile;

	public void Show(int _seq)
	{
		switch (_seq)
		{
		case 3:
			TutorialManager.SetDimmedClick(isClick: false);
			InGamePlayManager.BlockSelect = OnBlockSelectEvent;
			InGamePlayManager.PuzzleTouchEnd = OnPuzzleTouchEnd;
			InGamePlayManager.ActiveOnlySelectOneBlock(3, 2);
			InGamePlayManager.StopDeSelectAllBlock();
			ShowHandBlock(3, 2);
			ShowDragHighLightTutorialFirst();
			break;
		case 5:
			TutorialManager.SetDimmedClick(isClick: false);
			InGamePlayManager.BlockSelect = OnBlockSelectEvent;
			InGamePlayManager.ActiveOnlySelectOneBlock(4, 3);
			InGamePlayManager.StopDeSelectAllBlock();
			ShowHandBlock(4, 3, 1);
			ShowDragHighLightTutorialSecond();
			break;
		}
	}

	private void OnBlockSelectEvent(int _x, int _y)
	{
		switch (TutorialManager.Seq)
		{
		case 3:
			if (_x == 3 && _y == 2 && !isFirstBlockSelect)
			{
				isFirstBlockSelect = true;
			}
			else if (_x == 2 && _y == 2 && isFirstBlockSelect)
			{
				isFirstBlockSelect = false;
				ClearHand();
				InGamePlayManager.AllActiveBlock();
				TutorialManager.ReturnHighLightSpriteList();
				InGamePlayManager.BlockSelect = null;
				StartCoroutine(ProcessDelayStopMatchTimer());
				AnalyticsManager.FirebaseAnalyticsLogEvent(FBLog_Type.tuto12_puzzle_drag_1);
			}
			break;
		case 5:
			if (_x == 4 && _y == 3 && !isFirstBlockSelect)
			{
				isFirstBlockSelect = true;
			}
			else if (_x == 3 && _y == 2 && isFirstBlockSelect)
			{
				isFirstBlockSelect = false;
				ClearHand();
				InGamePlayManager.AllActiveBlock();
				TutorialManager.ReturnHighLightSpriteList();
				InGamePlayManager.BlockSelect = null;
				StartCoroutine(ProcessDelayStopMatchTimer());
				AnalyticsManager.FirebaseAnalyticsLogEvent(FBLog_Type.tuto12_puzzle_drag_2);
			}
			break;
		}
	}

	private IEnumerator ProcessDelayStopMatchTimer()
	{
		InGamePlayManager.TouchLock();
		yield return new WaitForSeconds(0.3f);
		InGamePlayManager.TouchActive();
		InGamePlayManager.StopMatchTimer();
		TutorialManager.NextSep();
	}

	private void OnPuzzleTouchEnd(Block first, Block second, bool isMatchBlock)
	{
	}

	private void ShowHandBlock(int _x, int _y, int type = 0)
	{
		ClearHand();
		trHand = MWPoolManager.Spawn("Tutorial", "Tutorial_Hand");
		trHand.position = InGamePlayManager.GetBlockPosition(_x, _y);
		switch (type)
		{
		case 0:
			trHand.GetComponent<TutorialHand>().ShowHandLeftAnim();
			break;
		case 1:
			trHand.GetComponent<TutorialHand>().ShowHandDiagonalAnim();
			break;
		}
		trBlockTile = MWPoolManager.Spawn("Tutorial", "Tutorial_Tile");
		trBlockTile.position = InGamePlayManager.GetBlockPosition(_x, _y);
	}

	private void ClearHand()
	{
		if (trHand != null)
		{
			MWPoolManager.DeSpawn("Tutorial", trHand);
			trHand = null;
		}
		if (trBlockTile != null)
		{
			MWPoolManager.DeSpawn("Tutorial", trBlockTile);
			trBlockTile = null;
		}
	}

	private void ShowDragHighLightTutorialFirst()
	{
		List<SpriteRenderer> list = new List<SpriteRenderer>();
		list.AddRange(GetHighLightBlock(2, 0));
		list.AddRange(GetHighLightBlock(2, 1));
		list.AddRange(GetHighLightBlock(2, 2));
		list.AddRange(GetHighLightBlock(2, 3));
		list.AddRange(GetHighLightBlock(2, 4));
		list.AddRange(GetHighLightBlock(3, 2));
		TutorialManager.ShowHighLightSpriteList(list);
	}

	private void ShowDragHighLightTutorialSecond()
	{
		List<SpriteRenderer> list = new List<SpriteRenderer>();
		list.AddRange(GetHighLightBlock(2, 0));
		list.AddRange(GetHighLightBlock(2, 1));
		list.AddRange(GetHighLightBlock(2, 2));
		list.AddRange(GetHighLightBlock(2, 3));
		list.AddRange(GetHighLightBlock(2, 4));
		list.AddRange(GetHighLightBlock(3, 2));
		list.AddRange(GetHighLightBlock(4, 2));
		list.AddRange(GetHighLightBlock(4, 3));
		TutorialManager.ShowHighLightSpriteList(list);
	}

	private List<SpriteRenderer> GetHighLightBlock(int _x, int _y)
	{
		List<SpriteRenderer> list = new List<SpriteRenderer>();
		Transform block = InGamePlayManager.GetBlock(_x, _y);
		SpriteRenderer[] componentsInChildren = block.GetComponentsInChildren<SpriteRenderer>(includeInactive: true);
		foreach (SpriteRenderer item in componentsInChildren)
		{
			list.Add(item);
		}
		return list;
	}
}
