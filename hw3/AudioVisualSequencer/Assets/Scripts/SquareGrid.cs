using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareGrid : MonoBehaviour
{
    // Width and Height of SquareCell
    public int width = 1;
    public int height = 1;

    public SquareCell cellPrefab;

    SquareCell[] cells;

    void Awake() {
        cells = new SquareCell[height  * width];

        for(int z = 0, i = 0; z < height; z++) {
            for(int x = 0; x < width; x++) {
                CreateCell(x, z, i++);
            }
        }
    }

    void CreateCell(int x, int z, int i) {
        Vector3 position;
        position.x = x * 10f;
        position.y = 0f;
        position.z = z * 10f;

        SquareCell cell = cells[i] = Instantiate<SquareCell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
