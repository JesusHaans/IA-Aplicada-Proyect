using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Botones : MonoBehaviour
{
    public void reiniciarJuego()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
