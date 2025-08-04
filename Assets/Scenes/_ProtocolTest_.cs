
using UnityEngine;

public class _ProtocolTest_ : MonoBehaviour
{
    void Start()
    {
        GameInfo.caUid = 62539;
        IRVManager.CurrentNetStatus = InternetReachabilityVerifier.Status.NetVerified;
    }
}
