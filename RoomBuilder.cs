using System.Collections.Generic;
using UnityEngine;
using Array = System.Array;
using Random = System.Random;

public class RoomBuilder : MonoBehaviour
{
    public GameObject hrzWallPrefab; // Horizontal Wall
    public GameObject vrtWallPrefab; // Vertical Wall
    public float[] valsA = new float[] { -0.4f, -0.3f, -0.2f, -0.1f, 0.0f, 0.1f, 0.2f, 0.3f, 0.4f }; // 8
    public float[] valsB = new float[] { -0.4f, -0.3f, -0.2f, -0.1f, 0.0f, 0.1f, 0.2f, 0.3f, 0.4f }; // 9
    private Random rnd = new Random();

    // Start is called before the first frame update
    void Start()
    {
        BuildRoom(rnd, valsA, valsB);
    }

    // Recursive Division Based Room Builder Algorithm
    private void BuildRoom(Random rnd, float[] valsA, float[] valsB)
    {
        int lenA = valsA.Length;
        int lenB = valsB.Length;

        if (lenA <= 1 || lenB <= 1)
        {
            return;
        }

        bool isHorizontal = GetOrientation(rnd, lenA, lenB);

        float[] arrA; // Split if Vertical
        float[] arrB; // Split if Horizontal

        if (isHorizontal)
        {
            // Draw Horizontal Wall Line
            int start = rnd.Next(0, lenB); // Start Point Index
            int gap = rnd.Next(0, lenA); // Random Gap Index
            PlaceLine(valsA, valsB, start, gap, hrzWallPrefab, true);
            arrA = valsA;

            if (start == 0 || start == (lenB - 1))
            {
                arrB = RemoveIndex(valsB, start);
                BuildRoom(rnd, arrA, arrB);
            }
            else if (start > 0 || start < (lenB - 1))
            {
                int splitRight = (lenB - start) - 1;
                float[] arrC;
                float[][] splits = SplitArray(valsB, start, splitRight);
                arrB = splits[0];
                arrC = splits[1];

                BuildRoom(rnd, arrA, arrB);
                BuildRoom(rnd, arrA, arrC);
            }
        }
        else if (!isHorizontal)
        {
            // Draw Vertical Wall Line
            int start = rnd.Next(0, lenA); // Start Point Index
            int gap = rnd.Next(0, lenB); // Random Gap Index
            PlaceLine(valsB, valsA, start, gap, vrtWallPrefab, false);
            arrA = valsB;

            if (start == 0 || start == (lenA - 1))
            {
                arrB = RemoveIndex(valsA, start);
                BuildRoom(rnd, arrA, arrB);
            }
            else if (start > 0 || start < (lenA - 1))
            {
                int splitRight = (lenA - start) - 1;
                float[] arrC;
                float[][] splits = SplitArray(valsA, start, splitRight);
                arrB = splits[0];
                arrC = splits[1];

                BuildRoom(rnd, arrA, arrB);
                BuildRoom(rnd, arrA, arrC);
            }
        }
    }

    // Place a line from the starting row OR col point
    private void PlaceLine(float[] lineArr, float[] startArr, int startIndex, int gapIndex, GameObject prefab, bool isHorizantal)
    {
        for (int i = 0; i < lineArr.Length; i++)
        {
            if (!(i == gapIndex))
            {
                GameObject wall = Instantiate(prefab, transform.position,
                                    Quaternion.identity) as GameObject;
                wall.name = "wall";
                wall.transform.parent = transform.transform;
                if (isHorizantal)
                {
                    wall.transform.localPosition = new Vector3(startArr[startIndex], 25, lineArr[i]);
                }
                else {
                    wall.transform.localPosition = new Vector3(lineArr[i], 25, startArr[startIndex]);
                }
                
            }
        }
    }

    // Determines the direction of the wall to be placed
    private bool GetOrientation(Random rnd, int width, int height)
    {
        if (width < height)
        {
            return true;
        }
        else if (height < width)
        {
            return false;
        }
        else
        {
            int rand = rnd.Next(0, 2);
            return rand == 1;
        }
    }

    // Removes Specified Index from Array
    private float[] RemoveIndex(float[] arr, int index)
    {
        var arrList = new List<float>(arr);
        arrList.RemoveAt(index);
        return arrList.ToArray();
    }

    // Splits Array into two subparts from specified index
    private float[][] SplitArray(float[] arr, int index, int right)
    {
        float[][] splits = new float[2][];
        splits[0] = new float[index];
        splits[1] = new float[right];

        Array.Copy(arr, 0, splits[0], 0, index);
        Array.Copy(arr, index + 1, splits[1], 0, right);

        return splits;
    }
}
