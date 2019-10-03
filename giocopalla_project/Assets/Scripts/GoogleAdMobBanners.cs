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
            this.appId = SensibleData.testAppId;         

        MobileAds.Initialize(this.appId);

        this.upperBannerId = "unexpected_platform";          
        this.lowerBannerId = "unexpected_platform";

        if (Application.platform == RuntimePlatform.Android)
        {
            this.upperBannerId = SensibleData.testBannerId;
            this.lowerBannerId = SensibleData.testBannerId;
        }

        // Create a 320x50 banner at the top of the screen
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
        this.lowerBannerView.LoadAd(this.lowerBannerRequest);
        this.HideBanners();
    }

    /* void ShowBanners(): shows the two banners */
    public void ShowBanners()
    {
        this.upperBannerView.Show();
        this.lowerBannerView.Show();
    }

    /* void HideBanners(): hide the two banners */
    public void HideBanners()
    {
        this.upperBannerView.Hide();
        this.lowerBannerView.Hide();
    }
}
