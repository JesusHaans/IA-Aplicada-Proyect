using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaberintoLogico : GeneradorBasicoLaberinto
{
    public LaberintoLogico(int filas, int columnas): base(filas, columnas)
    {

    }

    public override void GeneraLaberinto()
    {
        VisitaCelda(0, 0, Direccion.Inicio);
    }

    private void VisitaCelda(int filas, int columnas, Direccion movimientoHecho)
    {
        Direccion[] movimientosDisponibles = new Direccion[4];
        int cantidadMoviminetosDisponibles = 0;
        do {
            cantidadMoviminetosDisponibles = 0;
            //checa derecha
            if(columnas + 1 < contarColumnas && !GetCelda(filas, columnas + 1).visitada)
            {
                movimientosDisponibles[cantidadMoviminetosDisponibles] = Direccion.Derecha;
                cantidadMoviminetosDisponibles++;
            }else if(!GetCelda(filas,columnas).visitada && movimientoHecho != Direccion.Izquierda)
            {
                GetCelda(filas, columnas).paredDerecha = true;
            }
            //checa frente
            if (filas + 1 < contarFilas && !GetCelda(filas+1,columnas).visitada)
            {
                movimientosDisponibles[cantidadMoviminetosDisponibles] = Direccion.Frente;
                cantidadMoviminetosDisponibles++;
            }else if (!GetCelda(filas,columnas).visitada &&movimientoHecho != Direccion.Atras)
            {
                GetCelda(filas, columnas).paredFrente = true;
            }
            //checa izquierda
            if(columnas > 0 && columnas - 1 >= 0 && !GetCelda(filas,columnas - 1).visitada)
            {
                movimientosDisponibles[cantidadMoviminetosDisponibles] = Direccion.Izquierda;
                cantidadMoviminetosDisponibles++;
            }else if(!GetCelda(filas,columnas).visitada && movimientoHecho != Direccion.Derecha)
            {
                GetCelda(filas, columnas).paredIzquierda = true;
            }
            //checa atras
            if(filas > 0 && filas - 1 >=0 && !GetCelda(filas-1,columnas).visitada)
            {
                movimientosDisponibles[cantidadMoviminetosDisponibles] = Direccion.Atras;
                cantidadMoviminetosDisponibles++;
            }else if(!GetCelda(filas,columnas).visitada && movimientoHecho != Direccion.Frente)
            {
                GetCelda(filas, columnas).paredAtras = true;
            }
            GetCelda(filas, columnas).visitada = true;

            if (cantidadMoviminetosDisponibles > 0)
            {
                switch (movimientosDisponibles[Random.Range(0, cantidadMoviminetosDisponibles)])
                {
                    case Direccion.Inicio:
                        break;
                    case Direccion.Derecha:
                        VisitaCelda(filas, columnas + 1, Direccion.Derecha);
                        break;
                    case Direccion.Frente:
                        VisitaCelda(filas + 1, columnas, Direccion.Frente);
                        break;
                    case Direccion.Izquierda:
                        VisitaCelda(filas, columnas - 1, Direccion.Izquierda);
                        break;
                    case Direccion.Atras:
                        VisitaCelda(filas - 1, columnas, Direccion.Atras);
                        break;
                }
            }
        } while (cantidadMoviminetosDisponibles > 0);
    }// celda visitada

}
