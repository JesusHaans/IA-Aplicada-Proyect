using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreadorLaberinto : MonoBehaviour
{
    public enum LabGenAlgoritmo { PureRecursive }
    public LabGenAlgoritmo Algoritmo = LabGenAlgoritmo.PureRecursive;
    public bool TotalmenteRandom = false;
    public int SemillaRandom = 32561;
    public GameObject Piso = null;
    public GameObject Pared = null;
    public GameObject Pillar = null;
    public int Filas = 15;
    public int Columnas = 15;
    public float CeldaAncho = 5;
    public float CeldaAlto = 5;
    public bool AñadirBrechas = false;
    public GameObject Meta = null;
    private GeneradorBasicoLaberinto mLabGenerador = null;
    

    // Start is called before the first frame update
    void Start()
    {
        if (!TotalmenteRandom)
        {
            Random.InitState(SemillaRandom);
        }
        switch (Algoritmo)
        {
            case LabGenAlgoritmo.PureRecursive:
                mLabGenerador = new LaberintoLogico(Filas, Columnas);
                break;
        }
        mLabGenerador.GeneraLaberinto();
        int mitadx = Filas / 2;
        int mitadz = Columnas / 2;
        int rx = Random.Range(mitadx, Filas - 1);
        int rz = Random.Range(mitadz, Columnas - 1);
        mLabGenerador.GetCelda(rx, rz).meta = true;
        //Debug.Log(mLabGenerador);
        for (int fila = 0; fila < Filas; fila++)
        {
            for(int columna = 0; columna < Columnas; columna++)
            {
                float x = columna * (CeldaAncho + (AñadirBrechas ? .2f : 0));
                float z = fila * (CeldaAlto + (AñadirBrechas ? .2f : 0));
                Celda celda = mLabGenerador.GetCelda(fila, columna);
                GameObject tmp;
                tmp = Instantiate(Piso, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
                tmp.transform.parent = transform;
                if (celda.paredDerecha)
                {
                    tmp = Instantiate(Pared, new Vector3(x + CeldaAncho / 2, 0, z) + Pared.transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;
                    tmp.transform.parent = transform;
                }
                if (celda.paredFrente)
                {
                    tmp = Instantiate(Pared, new Vector3(x, 0, z + CeldaAlto / 2) + Pared.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
                    tmp.transform.parent = transform;
                }
                if (celda.paredIzquierda)
                {
                    tmp = Instantiate(Pared, new Vector3(x - CeldaAncho / 2, 0, z) + Pared.transform.position, Quaternion.Euler(0, 270, 0)) as GameObject;
                    tmp.transform.parent = transform;
                }
                if (celda.paredAtras)
                {
                    tmp = Instantiate(Pared, new Vector3(x, 0, z - CeldaAlto / 2) + Pared.transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;
                    tmp.transform.parent = transform;
                }
                if (celda.meta && Meta != null)
                {
                    tmp = Instantiate(Meta, new Vector3(x, 1, z), Quaternion.Euler(0, 0, 0)) as GameObject;
                    tmp.transform.parent = transform;
                }
            }
        }
        UnityEditor.AI.NavMeshBuilder.ClearAllNavMeshes();
        UnityEditor.AI.NavMeshBuilder.BuildNavMesh();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
