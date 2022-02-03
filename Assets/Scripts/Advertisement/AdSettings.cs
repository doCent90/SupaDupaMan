using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class AdSettings : MonoBehaviour, IInterstitialAdListener
{
    private readonly int _adTypes = Appodeal.INTERSTITIAL;

    private const string AppKey = "5269a88332ca5db6b2ff4d332da46f26aab196926786f05e";
    private const string Placement = "NewPlacement";

    public void onInterstitialClicked() => throw new System.NotImplementedException();

    public void onInterstitialClosed() => throw new System.NotImplementedException();

    public void onInterstitialExpired() => throw new System.NotImplementedException();

    public void onInterstitialFailedToLoad() => throw new System.NotImplementedException();

    public void onInterstitialLoaded(bool isPrecache) => throw new System.NotImplementedException();

    public void onInterstitialShowFailed() => throw new System.NotImplementedException();

    public void onInterstitialShown() => throw new System.NotImplementedException();

    public void ShowInterstitial()
    {
        if (Appodeal.canShow(_adTypes, Placement) && !Appodeal.isPrecache(_adTypes) && enabled)
            Appodeal.show(_adTypes);
    }

    private void OnEnable()
    {
        Appodeal.initialize(AppKey, _adTypes, hasConsent: true);
        Appodeal.setInterstitialCallbacks(this);
    }
}
