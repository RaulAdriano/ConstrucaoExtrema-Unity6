using System.Collections.Generic;
using Unity.Services.CloudSave;
using UnityEngine;
using UnityEngine.Events;

public class RemovedorAnuncios : MonoBehaviour
{
    private bool removerAnuncios;
    [SerializeField] private UnityEvent OnRemoverAnuncios;

    public bool GetRemoverAnuncios()
    {
        return removerAnuncios;
    }

    public async void SaveCloudService()
    {
        var data = new Dictionary<string, object> { { "no_ads", true } };
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);

        removerAnuncios = true;
    }

    public async void LoadCloudData()
    {
       
        try
        {
            var dadosSalvos = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "no_ads" });
            removerAnuncios = dadosSalvos["no_ads"].Value.GetAs<bool>();

            if (removerAnuncios)
            {
                OnRemoverAnuncios.Invoke();
            }
        }
        catch
        {

        }
    }
}
