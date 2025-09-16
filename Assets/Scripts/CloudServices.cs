using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEditor.PackageManager;
using UnityEngine;

public class CloudServices : MonoBehaviour
{
    [SerializeField] private GameObject erroLoginPopUp;


    

    public async Task SignUpAnonymouslyAsync()
    {
        if(AuthenticationService.Instance.IsSignedIn) return;

        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            if(AuthenticationService.Instance.PlayerName == "" || AuthenticationService.Instance.PlayerName == null)
            {
                await AtualizarUserName("Player");
                Debug.Log(AuthenticationService.Instance.PlayerName);
            }

            Debug.Log("Sign in anonymously succeeded!");

            // Shows how to get the playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

        }
        catch
        {
            erroLoginPopUp.SetActive( true );
        }
    }

    public async void TentarLoginNovamente()
    {
        erroLoginPopUp.SetActive ( false );
        await SignUpAnonymouslyAsync();
    }

    public async Task AtualizarUserName(string userName)
    {
        await AuthenticationService.Instance.UpdatePlayerNameAsync(userName);
    }

    public String GetUserName()
    {
        return AuthenticationService.Instance.PlayerName;
    }
}
