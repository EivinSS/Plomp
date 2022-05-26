using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(NameConfig.adManager);

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

#if UNITY_IOS
    string gameId = "4771392"
#else
    string gameId = "4771393";
#endif
    void Start()
    {
        Advertisement.Initialize(gameId);
    }

    public void PlayAd()
    {
#if UNITY_IOS
        if (Advertisement.IsReady("Interstitial_iOS"))
        {
            Advertisement.Show("Interstitial_iOS");
        }
#else
        if (Advertisement.IsReady("Interstitial_Android"))
        {
            Advertisement.Show("Interstitial_Android");
        }
#endif
    }
}
