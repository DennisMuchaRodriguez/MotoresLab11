using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI Counter;
    public int puntacion = 0;

    private void Start()
    {

        ActualizarCounter();
    }

    private void Update()
    {
        if (puntacion == 11)
        {
            SceneManager.LoadScene("Victoria");
        }
    }

    public void IncrementarPuntacion(int cantidad)
    {
        puntacion += cantidad;
        ActualizarCounter();
    }

    
    private void ActualizarCounter()
    {
        if(Counter != null) 
        {
            Counter.text = "Destruye todas las capsulas";

        }
        
    }

    public void CambiarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }
}
