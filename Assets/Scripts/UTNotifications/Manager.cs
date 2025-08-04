

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UTNotifications
{
	public abstract class Manager : MonoBehaviour
	{
		public delegate void OnInitializedHandler();

		public delegate void OnSendRegistrationIdHandler(string providerName, string registrationId);

		public delegate void OnNotificationClickedHandler(ReceivedNotification notification);

		public delegate void OnNotificationsReceivedHandler(IList<ReceivedNotification> receivedNotifications);

		private static Manager m_instance;

		private static bool m_destroyed;

		private bool m_initialized;

		public static Manager Instance
		{
			get
			{
				InstanceRequired();
				return m_instance;
			}
		}

		public bool Initialized
		{
			get
			{
				return m_initialized;
			}
			protected set
			{
				m_initialized = value;
				if (value && this.OnInitialized != null)
				{
					this.OnInitialized();
				}
			}
		}

		public event OnInitializedHandler OnInitialized;

		public event OnSendRegistrationIdHandler OnSendRegistrationId;

		public event OnNotificationClickedHandler OnNotificationClicked;

		public event OnNotificationsReceivedHandler OnNotificationsReceived;

		public abstract bool Initialize(bool willHandleReceivedNotifications, int startId = 0, bool incrementalId = false);

		public abstract void PostLocalNotification(string title, string text, int id, IDictionary<string, string> userData = null, string notificationProfile = null, int badgeNumber = -1, ICollection<Button> buttons = null);

		public abstract void ScheduleNotification(int triggerInSeconds, string title, string text, int id, IDictionary<string, string> userData = null, string notificationProfile = null, int badgeNumber = -1, ICollection<Button> buttons = null);

		public void ScheduleNotification(DateTime triggerDateTime, string title, string text, int id, IDictionary<string, string> userData = null, string notificationProfile = null, int badgeNumber = -1, ICollection<Button> buttons = null)
		{
			ScheduleNotification(TimeUtils.ToSecondsFromNow(triggerDateTime), title, text, id, userData, notificationProfile, badgeNumber, buttons);
		}

		public abstract void ScheduleNotificationRepeating(int firstTriggerInSeconds, int intervalSeconds, string title, string text, int id, IDictionary<string, string> userData = null, string notificationProfile = null, int badgeNumber = -1, ICollection<Button> buttons = null);

		public void ScheduleNotificationRepeating(DateTime firstTriggerDateTime, int intervalSeconds, string title, string text, int id, IDictionary<string, string> userData = null, string notificationProfile = null, int badgeNumber = -1, ICollection<Button> buttons = null)
		{
			ScheduleNotificationRepeating(TimeUtils.ToSecondsFromNow(firstTriggerDateTime), intervalSeconds, title, text, id, userData, notificationProfile, badgeNumber, buttons);
		}

		public abstract bool NotificationsEnabled();

		public abstract bool NotificationsAllowed();

		public abstract void SetNotificationsEnabled(bool enabled);

		public abstract bool PushNotificationsEnabled();

		public abstract bool SetPushNotificationsEnabled(bool enable);

		public abstract void CancelNotification(int id);

		public abstract void HideNotification(int id);

		public abstract void CancelAllNotifications();

		public abstract void HideAllNotifications();

		public abstract int GetBadge();

		public abstract void SetBadge(int bandgeNumber);

		protected bool OnSendRegistrationIdHasSubscribers()
		{
			return this.OnSendRegistrationId != null;
		}

		protected void _OnSendRegistrationId(string providerName, string registrationId)
		{
			this.OnSendRegistrationId(providerName, registrationId);
		}

		protected bool OnNotificationClickedHasSubscribers()
		{
			return this.OnNotificationClicked != null;
		}

		protected void _OnNotificationClicked(ReceivedNotification notification)
		{
			this.OnNotificationClicked(notification);
		}

		protected bool OnNotificationsReceivedHasSubscribers()
		{
			return this.OnNotificationsReceived != null;
		}

		protected void _OnNotificationsReceived(IList<ReceivedNotification> receivedNotifications)
		{
			this.OnNotificationsReceived(receivedNotifications);
		}

		protected virtual void OnDestroy()
		{
			m_instance = null;
			m_destroyed = true;
		}

		protected void NotSupported(string feature = null)
		{
			if (feature == null)
			{
				UnityEngine.Debug.LogWarning("UTNotifications: not supported on this platform");
			}
			else
			{
				UnityEngine.Debug.LogWarning("UTNotifications: " + feature + " feature is not supported on this platform");
			}
		}

		protected bool CheckInitialized()
		{
			if (!m_initialized)
			{
				UnityEngine.Debug.LogError("Please call UTNotifications.Manager.Instance.Initialize(...) first!");
			}
			return m_initialized;
		}

		private static void InstanceRequired()
		{
			if (!m_instance && !m_destroyed)
			{
				GameObject gameObject = new GameObject("UTNotificationsManager");
				m_instance = gameObject.AddComponent<ManagerImpl>();
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
			}
		}
	}
}
