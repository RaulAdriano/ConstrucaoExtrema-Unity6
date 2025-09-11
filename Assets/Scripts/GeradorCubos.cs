using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GeradorCubos : MonoBehaviour
{
    [SerializeField] private GameObject cuboPrefab;
    private GameObject ultimoCuboGerado;
    private AlturaConstrucao alturaConstrucao;
    private Transform myCamera;
    [SerializeField] private UnityEvent onSoltarCubo;
    public Vector3 entradasJogador;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myCamera = Camera.main.transform;
        alturaConstrucao = GetComponent<AlturaConstrucao>();
        GerarCubo();         
    }

    // Update is called once per frame
    void Update()
    {
        if (ultimoCuboGerado == null) return;

       // Vector3 entradasJogador = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));
        Vector3 direcaoCamera = myCamera.TransformDirection(entradasJogador);
        direcaoCamera.y = 0;

        ultimoCuboGerado.transform.position += direcaoCamera.normalized * Time.deltaTime * 3;
    }

    private void GerarCubo()
    {
        ultimoCuboGerado = Instantiate(cuboPrefab, 
            new Vector3(Random.Range(-3,4),alturaConstrucao.AlturaAtual() + 2 , Random.Range(-3, 4)), 
            Quaternion.identity);

        int tamanhoX = Random.Range(1, 7);
        int tamanhoY = Random.Range(1, 5);
        int tamanhoz = Random.Range(1, 7);

        ultimoCuboGerado.transform.localScale = new Vector3(tamanhoX,tamanhoY,tamanhoz);
        ultimoCuboGerado.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
    }

    public void SoltarCubo()
    {
        ultimoCuboGerado.GetComponent<Rigidbody>().useGravity = true;
        //ao soltar cubo desativa a linha
        ultimoCuboGerado.transform.GetChild(0).gameObject.SetActive(false);

        ultimoCuboGerado = null;

        onSoltarCubo.Invoke();
        Invoke(nameof(GerarCubo), 3); //chama o gerar cubo depois de 3 segundos
    }

    public void MoverCubo(InputAction.CallbackContext value)
    {
        Vector2 input = value.ReadValue<Vector2>();
        entradasJogador = new Vector3(input.x,0,input.y);
    }

    public void SoltarCubo(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            SoltarCubo();
        }
    }
}
