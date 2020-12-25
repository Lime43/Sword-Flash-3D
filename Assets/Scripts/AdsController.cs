using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsController : MonoBehaviour , IUnityAdsListener
{
    private string gameId = "3928243";
    string RewardVideo = "rewardedVideo";
    string video = "video";
    bool testMode = true;
    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId);
    }
    public void ShowAds()
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(video))
        {
            Advertisement.Show(video);
        }
        else
        {
            Advertisement.Show();
        }
    }
    public void ShowRewardVideo()
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(RewardVideo))
        {
            Advertisement.Show(RewardVideo);
        }
        else
        {
            //Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
            GetComponent<CanvasUI>().RewardButton.interactable = false;
        }
    }
    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(placementId != RewardVideo)
        {
            // Define conditional logic for each ad completion status:
            if (showResult == ShowResult.Finished)
            {
                GetComponent<CanvasUI>().TryAgain();
            }
            else if (showResult == ShowResult.Skipped)
            {
                GetComponent<CanvasUI>().TryAgain();
            }
            else if (showResult == ShowResult.Failed)
            {
                GetComponent<CanvasUI>().TryAgain();
            }
        }
        else
        {
            // Define conditional logic for each ad completion status:
            if (showResult == ShowResult.Finished)
            {
                /* Reward */
                StartCoroutine(GetComponent<CanvasUI>().C_cashEarn());
                GetComponent<CanvasUI>().RewardButton.interactable = false;
            }
            else if (showResult == ShowResult.Skipped)
            {
                /* Anim something */
            }
            else if (showResult == ShowResult.Failed)
            {
                /* Anim something */
            }
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == RewardVideo)
        {
            // Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
            //GetComponent<CanvasUI>().RewardButton.interactable = true;
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
}
