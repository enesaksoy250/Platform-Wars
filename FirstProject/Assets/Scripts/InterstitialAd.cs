using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InterstitialAd : MonoBehaviour
{
    private static InterstitialAd instance;

#if UNITY_ANDROID
    private string _adUnitId = "";
#elif UNITY_IPHONE
  private string _adUnitId = "";
#else
  private string _adUnitId = "unused";
#endif

    private GoogleMobileAds.Api.InterstitialAd _interstitialAd;

    private void Awake()
    {

        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
           


        }

        else
        {


            Destroy(gameObject);


        }



    }


    void Start()
    {


        MobileAds.Initialize((InitializationStatus initStatus) =>
        {

        });


    }

   

    public void LoadInterstitialAd()
    {

        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        var adRequest = new AdRequest();


        GoogleMobileAds.Api.InterstitialAd.Load(_adUnitId, adRequest,
            (GoogleMobileAds.Api.InterstitialAd ad, LoadAdError error) =>
            {

                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                _interstitialAd = ad;

            });

        RegisterEventHandlers(_interstitialAd);

    }


    public void ShowInterstitialAd()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            _interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }


    private void RegisterEventHandlers(GoogleMobileAds.Api.InterstitialAd interstitialAd)
    {

        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };

        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");

        };

        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };

        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };

        interstitialAd.OnAdFullScreenContentClosed += () =>
        {

            Debug.Log("Interstitial ad full screen content closed.");
            LoadInterstitialAd();

        };

        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
            LoadInterstitialAd();
        };
    }



}
