using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    public Sprite sprite1;
    Vector3 pos1 = new Vector3(-200 ,0 ,0);
    public Sprite sprite2;
    Vector3 pos2 = new Vector3(-100, 0, 0);
    public Sprite sprite3;
    Vector3 pos3 = new Vector3(0, 0, 0);
    public Sprite sprite4;
    Vector3 pos4 = new Vector3(100, 0, 0);
    public Sprite sprite5;
    Vector3 pos5 = new Vector3(200, 0, 0);

    // Start is called before the first frame update
    void Start()
    {


        Printer printer = Printer.Instance;

        printer.Print(sprite1, pos1, 2, PrimitiveType.Cube, false);
        printer.Print(sprite2, pos2, 2, PrimitiveType.Cube, false);
        printer.Print(sprite3, pos3, 2, PrimitiveType.Cube, false);
        printer.Print(sprite4, pos4, 2, PrimitiveType.Cube, false);
        printer.Print(sprite5, pos5, 2, PrimitiveType.Cube, false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
