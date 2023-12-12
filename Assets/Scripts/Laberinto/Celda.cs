using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direccion{Inicio, Derecha, Frente, Izquierda, Atras};
public class Celda 
{
    public bool visitada = false;
    public bool paredDerecha = false;
    public bool paredFrente = false;
    public bool paredIzquierda = false;
    public bool paredAtras = false;
    public bool meta = false;
}
