using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour {

    public static AdsManager instance;

    void Awake() {
        instance = this;
    }

    public void ShowAd() {
        if (Advertisement.IsReady()) {
            Advertisement.Show();
        }
    }
}
