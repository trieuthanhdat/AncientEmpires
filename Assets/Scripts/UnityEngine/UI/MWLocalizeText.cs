

using System;

namespace UnityEngine.UI
{
	[Serializable]
	public class MWLocalizeText : Text
	{
		public string localizedId;

		private void OnLocalizedChange()
		{
			if (MWLocalize.GameFont != null)
			{
				base.font = MWLocalize.GameFont;
				if (localizedId != string.Empty)
				{
					text = MWLocalize.GetData(localizedId);
				}
			}
		}

		protected override void Start()
		{
			base.Start();
			MWLocalize.RefreshFont = (Action)Delegate.Combine(MWLocalize.RefreshFont, new Action(OnLocalizedChange));
			MWLog.Log("GameFont :: " + MWLocalize.GameFont);
			if (MWLocalize.GameFont != null)
			{
				base.font = MWLocalize.GameFont;
				string data = MWLocalize.GetData(localizedId);
				if (localizedId != string.Empty && data != null)
				{
					text = data;
				}
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			MWLocalize.RefreshFont = (Action)Delegate.Remove(MWLocalize.RefreshFont, new Action(OnLocalizedChange));
		}
	}
}
