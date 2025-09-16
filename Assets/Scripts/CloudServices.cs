using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using NUnit.Framework;
using System.Collections.Generic;

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

    public string GetUserName()
    {
        return AuthenticationService.Instance.PlayerName;
    }

    public async Task SalvarPontuacao(int pontuacao)
    {
        await LeaderboardsService.Instance.AddPlayerScoreAsync("Pontuacoes", pontuacao);
    }

    public async Task<List<JogadorRanking>> GetPontuacoes()
    {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync("Pontuacoes");
        
        List<JogadorRanking> jogadoresRanking = new List<JogadorRanking>();
        
        foreach (LeaderboardEntry entry in scoresResponse.Results)
        {
            JogadorRanking jogador = new JogadorRanking();
            jogador.posicao = entry.Rank;
            jogador.userName = entry.PlayerName;
            jogador.pontuacao = (int) entry.Score;

            jogadoresRanking.Add(jogador);
        }

        return jogadoresRanking;
    }

    public async Task<int> GetPontuacaoJogador()
    {
        try
        {
            var result = await LeaderboardsService.Instance.GetPlayerScoreAsync("Pontuacoes");
            return (int)result.Score;
        }
        catch
        {
            return 0;
        }
    }
}
