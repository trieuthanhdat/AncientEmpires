

using UnityEngine;

namespace PathologicalGames
{
	[AddComponentMenu("Path-o-logical/PoolManager/Pre-Runtime Pool Item")]
	public class PreRuntimePoolItem : MonoBehaviour
	{
		public string poolName = string.Empty;

		public string prefabName = string.Empty;

		public bool despawnOnStart = true;

		public bool doNotReparent;

		private void Start()
		{
			if (!PoolManager.Pools.TryGetValue(poolName, out SpawnPool spawnPool))
			{
				string format = "PreRuntimePoolItem Error ('{0}'): No pool with the name '{1}' exists! Create one using the PoolManager Inspector interface or PoolManager.CreatePool().See the online docs for more information at http://docs.poolmanager.path-o-logical.com";
				UnityEngine.Debug.LogError(string.Format(format, base.name, poolName));
			}
			else
			{
				spawnPool.Add(base.transform, prefabName, despawnOnStart, !doNotReparent);
			}
		}
	}
}
