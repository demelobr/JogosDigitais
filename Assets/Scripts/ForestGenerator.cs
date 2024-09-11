using UnityEngine;

public class ForestGenerator : MonoBehaviour
{
    // Prefab de árvore que será replicado
    public GameObject treePrefab;

    // Dimensões da área da floresta
    public float forestWidth = 100f;
    public float forestLength = 100f;
    public int treeCount = 100;

    void Start()
    {
        GenerateForest();
    }

    void GenerateForest()
    {
        for (int i = 0; i < treeCount; i++)
        {
            // Determinar uma posição aleatória dentro das dimensões da floresta
            Vector3 position = new Vector3(
                Random.Range(-forestWidth / 2, forestWidth / 2),
                Random.Range(-forestLength / 2, forestLength / 2),  // Mantendo a altura das árvores constante
                0
            );

            // Instanciar o prefab de árvore na posição
            GameObject tree = Instantiate(treePrefab, position, Quaternion.identity);

            // Randomizar apenas a rotação no eixo Y para variar a orientação das árvores
            //float randomRotation = Random.Range(0f, 360f);
            //tree.transform.Rotate(0f, randomRotation, 0f);
        }
    }
}
