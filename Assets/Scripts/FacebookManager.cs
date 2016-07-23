using UnityEngine;
using System.Collections.Generic;
using Facebook.Unity;

public class FacebookManager : MonoBehaviour {

    public static FacebookManager instance;
    public delegate void LoginCallback(bool status);
    public LoginCallback loginCallback;
    public bool isLogin = false;

    private List<string> permissions;

    void Awake() {
        instance = this;
    }

    void Start() {

        if (!FB.IsInitialized)
            FB.Init(InitCallback, OnHideUnity);
    }

    #region "Facebook Init"
    private void InitCallback() {

        if (FB.IsInitialized){
            FB.ActivateApp();
        }
        else {
            Debug.Log("Fallo al inicializar Facebook");
        }
    }
    private void OnHideUnity(bool isGameShown)
    {

        if (!isGameShown)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    #endregion

    #region "Facebook Login"
    public void LogIn() {
        //permissions = new List<string> { "public_profile" };
        permissions = new List<string> { "public_actions" };
        //FB.LogInWithReadPermissions(permissions, AuthCallback);
        FB.LogInWithPublishPermissions(permissions, AuthCallback);
    }

    private void AuthCallback(ILoginResult result) {

        if (FB.IsLoggedIn){

            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            Debug.Log("Facebook: "+ aToken.UserId);
            FB.API("/me?fields=name,picture", HttpMethod.GET, ProfileAPICallback);
            isLogin = true;
        }
        else {

            Debug.Log("Problema para login en facebook");
            isLogin = false;
        }

    }

    private void ProfileAPICallback(IGraphResult result) {

        Debug.Log(result.RawResult);
    }

    #endregion

    #region "Facebook Share"
    public void ShareMessage(string message, Texture2D image2Share) {

        byte[] imageContent = image2Share.EncodeToPNG();
        var wwwForm = new WWWForm();
        wwwForm.AddBinaryData("image", imageContent, "Gamo.png");
        wwwForm.AddField("message", message);

        FB.API("me/photos", HttpMethod.POST, ShareMessageAPICallback, wwwForm);

    }

    private void ShareMessageAPICallback(IGraphResult result) {

        Debug.Log(result.RawResult);
    }

    #endregion
}