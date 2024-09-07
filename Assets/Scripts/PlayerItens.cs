using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItens : MonoBehaviour
{
    public static PlayerItens instance;

    [Header("Amounts")]
    public int totalWood;
    public int carrots;
    public float currentWater;
    public int fishes;

    [Header("Limits")]
    public float waterLimit = 50;
    public float woodLimit = 10;
    public float carrotLimit = 5;
    public float fishesLimit = 3;

    private void Awake()
    {
        // Certifique-se de que só existe uma instância do Player no jogo
        if (instance == null)
        {
            instance = this;  // Atribui esta instância
        }
        else
        {
            Destroy(gameObject); // Garante que não haja mais de um Player
        }
    }

    public void WaterLimit(float water)
    {
        if (currentWater < waterLimit)
        {
            currentWater += water;
        }
    }
}
