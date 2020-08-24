using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAdMobBanners : MonoBehaviour
{
    private string appId;
    private string upperBannerId;
    private string lowerBannerId;

    private BannerView upperBannerView;
    private BannerView lowerBannerView;

    private AdRequest upperBannerRequest;
    private AdRequest lowerBannerRequest;

    public void Start()
    {
        this.appId = "unexpected_platform";

        if (Application.platform == RuntimePlatform.Android)
            this.appId = SensibleData.androidAppId;         

        MobileAds.Initialize(this.appId);

        this.upperBannerId = "unexpected_platform";          
        this.lowerBannerId = "unexpected_platform";

        if (Application.platform == RuntimePlatform.Android)
        {
            this.upperBannerId = SensibleData.androidUpperBannerId;
            this.lowerBannerId = SensibleData.androidLowerBannerId;
        }

        // Create a 320x50 banner at the top and at the bottom of the screen
        this.upperBannerView = new BannerView(this.upperBannerId, AdSize.Banner, AdPosition.Top);
        this.lowerBannerView = new BannerView(this.lowerBannerId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request
        this.upperBannerRequest = new AdRequest.Builder().Build();
        this.lowerBannerRequest = new AdRequest.Builder().Build();
    }

    /* void RequestBanners(): makes the request for the two banners and then hides it */
    public void LoadBanners()
    {
        this.upperBannerView.LoadAd(this.upperBannerRequest);
        Debug.Log("upper banner view caricato");
        this.lowerBannerView.LoadAd(this.lowerBannerRequest);
        Debug.Log("lower banner view caricato");
        this.HideBanners();
    }

    /* void ShowBanners(): shows the two banners */
    public void ShowBanners()
    {
        this.upperBannerView.Show();
        this.lowerBannerView.Show();
        Debug.Log("banner mostrati");
    }

    /* void HideBanners(): hide the two banners */
    public void HideBanners()
    {
        this.upperBannerView.Hide();
        Debug.Log("upper banner view nascosto");
        this.lowerBannerView.Hide();
        Debug.Log("lower banner view nascosto");
    }
}
