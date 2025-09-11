using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorMenu : MonoBehaviour
{
    public void Jogar()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void VoltarHome()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void AbirRanking()
    {

    }

    public void RemoverAnuncios()
    {

    }
}
