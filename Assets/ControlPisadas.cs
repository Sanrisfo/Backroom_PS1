using UnityEngine;

public class ControlPisadas : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] sonidosPisadas;

    public float intervaloPisadas = 0.5f; // Ritmo entre pasos en segundos
    private float temporizadorPasos = 0f;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        // 1. Detectar si estás presionando teclas de movimiento
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");
        bool seEstaMoviendo = (Mathf.Abs(inputX) > 0.1f || Mathf.Abs(inputZ) > 0.1f);

        // 2. Si se mueve y está en el suelo (o si el controller se desplaza)
        if (seEstaMoviendo)
        {
            // Acumulamos tiempo
            temporizadorPasos += Time.deltaTime;

            // Si llegamos al intervalo, suena la pisada
            if (temporizadorPasos >= intervaloPisadas)
            {
                ReproducirPisada();
                temporizadorPasos = 0f; // Reiniciamos a 0 limpios
            }
        }
        else
        {
            // Si te detienes, dejamos el contador listo para el próximo arranque
            // pero sin forzar que suene instantáneo para no romper el bucle
            temporizadorPasos = 0f;
        }
    }

    void ReproducirPisada()
    {
        if (sonidosPisadas == null || sonidosPisadas.Length == 0) return;

        int indiceAleatorio = Random.Range(0, sonidosPisadas.Length);

        // Un toque de variación sutil en el tono
        audioSource.pitch = Random.Range(0.9f, 1.1f);

        audioSource.PlayOneShot(sonidosPisadas[indiceAleatorio]);
    }
}