

using UnityEngine;

public class HunterCharacter_Anim : MonoBehaviour
{
	[SerializeField]
	private HunterCharacter hunterCharacter;

	public void End_Attack_Anim()
	{
		MWLog.Log("22222222222222 = ");
		hunterCharacter.End_Attack_Anim();
	}

	public void SetMonsterHP_Gauge()
	{
		MWLog.Log("11111111111111 = ");
		SoundController.EffectSound_Play(EffectSoundType.HunterAttack);
		InGamePlayManager.ShakeCamera(isVibration: false);
		hunterCharacter.SetMonsterHP_Gauge();
	}

	public void CheckStun()
	{
		if (hunterCharacter.Hunter.IsHunterStun)
		{
			hunterCharacter.SetAnim(Anim_Type.STUN);
		}
		else
		{
			hunterCharacter.SetAnim(Anim_Type.IDLE);
		}
	}
}
