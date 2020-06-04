using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicNumberKeeper : SingletonMonoBehaviour<MagicNumberKeeper>
{
    //member 2-6
    private int numberOfPeople;
    public Dictionary<int, string> nameList = new Dictionary<int, string>();

    public bool isSolo;
    public int NumberOfPeople
    {
        get { return numberOfPeople; }
        set { numberOfPeople = value;
            index = numberOfPeople - 2;
        }
    }
    private int index;
    Vector3 center;

    private float[][] pointX = new float[5][];
    private float[][] pointY = new float[5][];

    private float[][] victoryX = new float[5][];
    private float[][] victoryY = new float[5][];

    private float[][] expectationX = new float[5][];
    private float[][] expectationY = new float[5][];

    private float[][] trashX = new float[5][];
    private float[][] trashY = new float[5][];

    private float[][] NameX = new float[5][];
    private float[][] NameY = new float[5][];

    private float[] plusMinusX = new float[] { };
    private float scaryMaryY = 100f;
    private float decideButtonY = -80f;

    // Start is called before the first frame update
    void Start()
    {
        
        center = new Vector3(0f, 0f, 0f);

        pointX[0] = new float[] { -600f, 600f };
        pointY[0] = new float[] { 100f, 100f };
        pointX[1] = new float[] { -600f ,- 200f,600f};
        pointY[1] = new float[] { -150f,400f,-150f };
        pointX[2] = new float[] { -600f ,- 600f, 600f, 600f };
        pointY[2] = new float[] { -150f,350f, 350f, -150f };
        pointX[3] = new float[] { -600f ,- 600f,-200f, 600f, 600f };
        pointY[3] = new float[] { -150f,350f,400f, 350f, -150f,  };
        pointX[4] = new float[] { -600f , -600f,-350f,150f, 600f, 600f  };
        pointY[4] = new float[] { -150f ,350f,400f,400f, 350f, -150f };

        victoryX[0] = new float[] { -600f, 600f };
        victoryY[0] = new float[] { -100f, -100f };
        victoryX[1] = new float[] { -600f ,200f,600f};
        victoryY[1] = new float[] { -350f,400f,-350f };
        victoryX[2] = new float[] { -600f ,- 600f, 600f, 600f };
        victoryY[2] = new float[] { -350f,150f, 150f, -350f };
        victoryX[3] = new float[] { -600f ,- 600f,200f, 600f, 600f  };
        victoryY[3] = new float[] { -350f ,150f,400f, 150f, -350f };
        victoryX[4] = new float[] { -600f - 600f,-150f,350f, 600f, 600f };
        victoryY[4] = new float[] { -350f,150f,400f,400f, 150f, -350f };

        expectationX[0] = new float[] { -600f, 600f };
        expectationY[0] = new float[] { 0f, 0f };
        expectationX[1] = new float[] { -600f,0f,600f };
        expectationY[1] = new float[] { -250f,400f,-250f };
        expectationX[2] = new float[] { -600f ,- 600f, 600f, 600f };
        expectationY[2] = new float[] { -250f,250f, 250f, -250f };
        expectationX[3] = new float[] { -600f ,- 600f,0f, 600f, 600f };
        expectationY[3] = new float[] { -250f,250f,400f, 250f, -250f };
        expectationX[4] = new float[] { -600f ,- 600f,-250f,250f, 600f, 600f  };
        expectationY[4] = new float[] { -250f,250f,400f,400f, 250f, -250f };

        trashX[0] = new float[] {-4f,4f };
        trashY[0] = new float[] {0f,0f };
        trashX[1] = new float[] { -4f,0f,4f };
        trashY[1] = new float[] { -2f,3f,-2f };
        trashX[2] = new float[] { -4f ,- 4f,4f,4f, };
        trashY[2] = new float[] { -2f,3f,3f,-2f };
        trashX[3] = new float[] { -4f ,- 4f,0f, 4f, 4f, };
        trashY[3] = new float[] { -2f,2f,3f, 2f, -2f };
        trashX[4] = new float[] { -4f ,- 4f,-2f,2f, 4f, 4f };
        trashY[4] = new float[] { -2f,2f,3f,3f, 2f, -2f };

        NameX[0] = new float[] { -600f, 600f };
        NameY[0] = new float[] {-200f,-200f};
        NameX[1] = new float[] { -600f, -500f, 600f };
        NameY[1] = new float[] { -450f, 400f, -450f };
        NameX[2] = new float[] { -600f, -600f, 600f, 600f };
        NameY[2] = new float[] { -450f, 50f, 50f, -450f };
        NameX[3] = new float[] { -600f, -600f, 400f, 600f, 600f };
        NameY[3] = new float[] { -450f, 50f, 400f, 50f, -450f };
        NameX[4] = new float[] { -600f - 600f, -450f, 450f, 600f, 600f };
        NameY[4] = new float[] { -450f, 50f, 400f, 400f, 50f, -450f };
    }

    public Vector3 PointPos(int player)
    {
        Vector3 vec = center;
        vec.x = vec.x + pointX[index][player - 1];
        vec.y = vec.y + pointY[index][player - 1];
        return vec;
    }

    public Vector3 VictoryPos(int player)
    {
        Vector3 vec = center;
        vec.x = vec.x + victoryX[index][player - 1];
        vec.y = vec.y + victoryY[index][player - 1];
        return vec;
    }
    public Vector3 ExpectationPos(int player)
    {
        Vector3 vec = center;
        vec.x = vec.x + expectationX[index][player - 1];
        vec.y = vec.y + expectationY[index][player - 1];
        return vec;
    }

    public Vector3 CardTrashPos(int player)
    {
        Vector3 vec = center;
        vec.x = vec.x + trashX[index][player - 1];
        vec.y = vec.y + trashY[index][player - 1];
        return vec;
    }

    public Vector3 NamePos(int player)
    {
        Vector3 vec = center;
        vec.x = vec.x + NameX[index][player - 1];
        vec.y = vec.y + NameY[index][player - 1];
        return vec;
    }

    public Vector3 HandCardPos()
    {
        Vector3 vec = center;
        vec.x = vec.x - 4.5f;
        vec.y = vec.y - 4.5f;
        return vec;
    }

    public Vector3 ConsolePos()
    {
        Vector3 vec = center;
        return vec;
    }

    public Vector3 ThisTurnPlayerPos()
    {
        Vector3 vec = center;
        vec.y = vec.y - 200f;
        return vec;
    }

    public Vector3 DecideButtonPos()
    {
        Vector3 vec = center;
        vec.y = vec.y + decideButtonY;
        return vec;
    }

    public Vector3 ScaryMaryPos(bool isPirate)
    {
        if (isPirate)
        {
            Vector3 vec = center;
            vec.y = vec.y + scaryMaryY;
            return vec;
        }
        else
        {
            Vector3 vec = center;
            vec.y = vec.y - scaryMaryY;
            return vec;
        }
    }

    public Vector3 StartUIPos()
    {
        return center;
    }
}
