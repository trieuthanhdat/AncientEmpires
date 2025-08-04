

using GoogleMobileAds.Api;
using System;
using System.Collections;
using UnityEngine;

public class AdNetworkManager : MonoSingleton<AdNetworkManager>
{
	public Action OnRewardShowComplete;

	public bool isReward;

	public bool isForcePlay;

	private static AdNetworkManager instance;

	//private RewardBasedVideoAd rewardBasedVideo;

	public static void PlayReward(Action _onCallback = null)
	{
		if (GameInfo.currentSceneType == SceneType.Lobby)
		{
			AnalyticsManager.RewardRequestAppEnvent("get_energy");
		}
		else
		{
			AnalyticsManager.RewardRequestAppEnvent("battle_reward_pick_more");
		}
		if (_onCallback != null)
		{
			instance.OnRewardShowComplete = _onCallback;
		}
		instance.isReward = false;
		if (instance.IsRwdAdLoaded())
		{
			instance.ShowRewardAd();
			return;
		}
		instance.isForcePlay = true;
		instance.RequestRewardBasedVideo();
	}

	public static void PlayInterstitial()
	{
	}

	public static void RequestRewardVideo()
	{
		//instance.RequestRewardBasedVideo();
	}

	private AdRequest CreateAdRequest()
	{
		//return new AdRequest.Builder().TagForChildDirectedTreatment(tagForChildDirectedTreatment: true).Build();
		return null;
	}

	private IEnumerator CheckRequestReward()
	{
		yield return new WaitForSeconds(0.5f);
		RequestRewardBasedVideo();
	}

	public void RequestRewardBasedVideo()
	{
		string adUnitId = "ca-app-pub-1076272347919893/3232901682";
		//if (!IsRwdAdLoaded())
		//{
		//	rewardBasedVideo.LoadAd(CreateAdRequest(), adUnitId);
		//}
	}

	public bool IsRwdAdLoaded()
	{
		//return rewardBasedVideo.IsLoaded();
		return false; // Placeholder, as we don't have the actual implementation of RewardBasedVideoAd
    }

	public void ShowRewardAd()
	{
		//if (IsRwdAdLoaded())
		//{
		//	rewardBasedVideo.Show();
		//}
	}

	public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
		if (instance.isForcePlay)
		{
			isForcePlay = false;
			PlayReward();
		}
	}

	//public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	//{
	//	MonoBehaviour.print("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
	//}

	public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
	}

	public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
	}

	public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
		if (OnRewardShowComplete != null)
		{
			OnRewardShowComplete();
		}
	}

	public void HandleRewardBasedVideoRewarded(object sender, Reward args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoRewarded event received");
		isReward = true;
	}

	public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
	}

	private new void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		//string appId = "ca-app-pub-6855474545715317~6352665015";
		//MobileAds.SetiOSAppPauseOnBackground(pause: true);
		////MobileAds.Initialize(appId);
		/////rewardBasedVideo = RewardBasedVideoAd.Instance;
		////rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
		//////rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
		////rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
		////rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
		////rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
		////rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
		////rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
		//RequestRewardBasedVideo();s
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		MWLog.Log("pauseStatus = " + pauseStatus);
		if (!pauseStatus)
		{
			StartCoroutine(CheckRequestReward());
		}
	}
}
