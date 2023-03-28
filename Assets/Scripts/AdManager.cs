using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener
{
    private string gameId;
    string iOSGameId = "4771392";
    string androidGameId = "4771393";

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(NameConfig.adManager);

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        InitializeAds();
    }



    public void InitializeAds()
    {
        gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? iOSGameId
            : androidGameId;
        Advertisement.Initialize(gameId, this);
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

    public void PlayBanner()
    {
#if UNITY_IOS
        if (Advertisement.IsReady("Banner_iOS"))
        {
            Advertisement.Show("Banner_iOS");
        }
#else
        if (Advertisement.IsReady("Banner_Android"))
        {
            Advertisement.Show("Banner_Android");
        }
#endif
    }



    void IUnityAdsInitializationListener.OnInitializationComplete()
    {
        throw new System.NotImplementedException();
    }

    void IUnityAdsInitializationListener.OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        throw new System.NotImplementedException();
    }

    int deaths = 0;
    public void Death()
    {
        deaths++;
        if(deaths % 30 == 0)
        {
            PlayAd();
        }
    }
}
