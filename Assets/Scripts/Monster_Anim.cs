

using UnityEngine;

public class Monster_Anim : MonoBehaviour
{
	[SerializeField]
	private Monster monster;

	public void End_Die_Anim()
	{
		MWLog.Log("End_Die_Anim()");
		monster.End_Die_Anim();
	}

	public void Damage()
	{
		MWLog.Log("monster.MonsterCurrentDamage = " + monster.MonsterCurrentDamage);
		SoundController.Monster_Play(monster.MonsterInfo.mIdx);
		Transform transform = null;
		transform = MWPoolManager.Spawn("Effect", "Fx_Damage_hit", null, 0.5f);
		transform.position = InGamePlayManager.DamagePosition;
		monster.SetAttackHunterDamageAnim();
		InGamePlayManager.Damage(monster.MonsterCurrentDamage);
	}

	public void End_Attack_Anim()
	{
		monster.End_Attack_Anim();
	}

	public void CheckStun()
	{
		if (monster.IsStun)
		{
			monster.SetAnim(Anim_Type.STUN);
		}
		else
		{
			monster.SetAnim(Anim_Type.IDLE);
		}
	}
}
