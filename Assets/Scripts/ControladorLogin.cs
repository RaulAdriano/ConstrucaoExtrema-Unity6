using System;
using TMPro;
using Unity.Services.Core;
using UnityEngine;

public class ControladorLogin : MonoBehaviour
{

    [SerializeField] private CloudServices cloudServices;
    [SerializeField] private TMP_Text userNameText;
    [SerializeField] private TMP_InputField userNameInputField;
    [SerializeField] private TMP_Text recordeText;


    private async void Awake()
    {
        try
        {
            await UnityServices.InitializeAsync();
            await cloudServices.SignUpAnonymouslyAsync();

            AtualizarUserNameUI();
            AtualizarRecordUI();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    private void AtualizarUserNameUI()
    {
        String userName = cloudServices.GetUserName();
        userNameText.text = userName;
        userNameInputField.text = userName.Substring(0, userName.IndexOf("#"));
    }

    public async void SalvarNovoUserName()
    {
        await cloudServices.AtualizarUserName(userNameInputField.text);
        AtualizarUserNameUI();
    }

    public async void AtualizarRecordUI()
    {
        int recorde = await cloudServices.GetPontuacaoJogador();
        recordeText.text = "MEU RECORDE: " + recorde;

    }
}
