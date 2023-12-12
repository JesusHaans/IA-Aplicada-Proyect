using UnityEngine;
using System.Collections;

public abstract class GeneradorBasicoLaberinto
{
    public int contarFilas { get { return nFilasLaberinto; } }
    public int contarColumnas { get { return nColumnasLaberinto; } }

    private int nFilasLaberinto;
    private int nColumnasLaberinto;
    private Celda[,] nLaberinto;
    public GeneradorBasicoLaberinto(int rows, int columns)
    {
        nFilasLaberinto = Mathf.Abs(rows);
        nColumnasLaberinto = Mathf.Abs(columns);
        if (nFilasLaberinto == 0)
        {
            nFilasLaberinto = 1;
        }
        if (nColumnasLaberinto == 0)
        {
            nColumnasLaberinto = 1;
        }
        nLaberinto = new Celda[rows, columns];
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                nLaberinto[row, column] = new Celda();
            }
        }
    } // Constructor

    public abstract void GeneraLaberinto();

    public Celda GetCelda(int row, int column)
    {
        if (row >= 0 && column >= 0 && row < nFilasLaberinto && column < nFilasLaberinto)
        {
            return nLaberinto[row, column];
        }
        else
        {
            Debug.Log(row + " " + column);
            throw new System.ArgumentOutOfRangeException();
        }
    }
}
