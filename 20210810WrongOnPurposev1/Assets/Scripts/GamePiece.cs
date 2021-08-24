using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public int xIndex;
    public int yIndex;

    Board m_board;

    bool m_isMoving = false;

    public MatchValue matchValue;

    public enum MatchValue
    {
        Blue,
        Green,
        Orange,
        Purple,
        Red,
        Yellow,
        Wild
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(Board board)
    {
        m_board = board;
    }

    public void SetCoord(int x, int y)
    {
        xIndex = x;
        yIndex = y;
    }

    public void Move (int destX, int destY, float timeToMove)
    {
        if (!m_isMoving)
        {
            StartCoroutine(MoveRoutine(new Vector3(destX, destY, 0), timeToMove));
        }
    }

    IEnumerator MoveRoutine(Vector3 destination, float timeToMove)
    {
        Vector3 startPosition = transform.position;

        bool reachedDestination = false;

        float elapsedTime = 0f;

        m_isMoving = true;

        while (!reachedDestination)
        {
            if (Vector3.Distance(transform.position, destination) < 0.01f)
            {
                reachedDestination = true;

                if (m_board != null)
                {
                    m_board.PlaceGamePiece(this, (int)destination.x, (int)destination.y);
                }

                break;
            }

            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp(elapsedTime / timeToMove, 0f, 1f);

            //t = Mathf.Sin(t * Mathf.PI * 0.5f); //the apperance of a deaccelartion animation

            //t = 1 - Mathf.Cos(t * Mathf.PI * 0.5f); //the apperance of an accelartion animation

            //t = t * t;//same as the above two but uses "exponential function"

            //t = t * t * (3 - 2 * t);//Smoothstep: interpolation function. this is basically the first two put together to give a brief acceleration & decellaration "animation" look

            t = t * t * t * (t * (t * 6 - 15) + 10);//an even smoother step! FANCY

            transform.position = Vector3.Lerp(startPosition, destination, t);

            yield return null;
        }

        m_isMoving = false;

    }
}
