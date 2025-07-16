using UnityEngine;

public class DeleteAttack : MonoBehaviour
{
    [Header("Configuraci�n de disparo")]
    public GameObject fireballPrefab;
    public Transform firePoint;            // Punto de salida de la bola
    public float shootInterval = 5f;       // Cada cu�ntos segundos dispara

    private float shootTimer;

    // Referencia al jugador
    [SerializeField] private Transform playerTransform;

    void Start()
    {
        Shoot();
    }

    void Update()
    {
        // Cuenta regresiva para disparar
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootInterval;
        }
    }

    void Shoot()
    {
        if (playerTransform == null) return;

        // Instanciamos la bola de fuego
        var fireballObj = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);

        // Inicializamos su direcci�n hacia el jugador
        var fireball = fireballObj.GetComponent<FireBall>();
        if (fireball != null)
            fireball.Initialized(playerTransform);
        else
            Debug.LogError("El prefab no tiene el componente Fireball.");
    }
}

