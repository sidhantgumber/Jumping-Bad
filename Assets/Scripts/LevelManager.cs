using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LevelManager : MonoBehaviour
{
    private const float pipeWidth = 1.8f;
    private const float CameraSize = 5f;
    private const float pipeMoveSpeed = 3f;
    private const float destroyPipeXposition = -10f;
    private const float pipeSpawnXpos = 10f;
    private int noOfPipesSpawned;
    private int pipePassedCount;
    private const float birdXpos = 0f;


    private int currentScore;

    public static LevelManager instance;

    public static LevelManager GetInstance()
    {
        return instance;
    }

    private List<Transform> groundList;
    private List<Pipe> pipeList;
    private float timerBetweenSpwans;
    private float maxTimerBetweenSpawns;
    private float gapSize;

    private enum Difficulty
    {
        easy,
        medium,
        crappy,
        constipation,

    }

    private void Awake()
    {
        instance = this;
        groundList = new List<Transform>();
        pipeList = new List<Pipe>();
        maxTimerBetweenSpawns = 2f;
        SetDifficulty(Difficulty.easy);

    }

   
    private void Update()
    {
        PipeMovement();
        SpawnPipes(); 

    }


    private void SpawnPipes()
    {
        timerBetweenSpwans -= Time.deltaTime;
        if (timerBetweenSpwans <= 0)
        {
            timerBetweenSpwans += maxTimerBetweenSpawns;
            float edgeLimit = 0.5f;
            float minHeight = gapSize * .5f + edgeLimit+0.5f;
            float totalHeight = CameraSize * 2f;
            float maxHeight = totalHeight - gapSize  - edgeLimit;

            float height = Random.Range(minHeight, maxHeight);
            CreatePipesWithGap(height, gapSize, pipeSpawnXpos);

        }

    }

    private void PipeMovement()
    {
        for(int i=0; i<pipeList.Count; i++)
        {
            Pipe pipe = pipeList[i];
            bool isToTheRightOfBird = pipe.GetXposition() > birdXpos;
            pipe.Move();
            if (isToTheRightOfBird && pipe.GetXposition() <= birdXpos)
            {
                // pipe passed the bird
                pipePassedCount++;
                AudioManager.GetInstance().PlayScoreSound();
            }
            if (pipe.GetXposition() < destroyPipeXposition)
            {
                pipe.DestroyPipe();
                pipeList.Remove(pipe);
                i--;
            }
        }

    }

    private void SetDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.easy: gapSize = 3.5f;
                maxTimerBetweenSpawns = 1.5f;
                break;
            case Difficulty.medium: gapSize = 3f;
                maxTimerBetweenSpawns = 1.2f;
                break;
            case Difficulty.crappy: gapSize = 2.7f;
                maxTimerBetweenSpawns = 1f;
                break;
            case Difficulty.constipation: gapSize = 2.5f;
                maxTimerBetweenSpawns = 0.8f;

                break;
        }


    }

    private Difficulty GetDifficulty()
    {
        if (noOfPipesSpawned > 50) return Difficulty.constipation;
        if (noOfPipesSpawned >= 30) return Difficulty.crappy;
        if (noOfPipesSpawned >= 10) return Difficulty.medium;
        return Difficulty.easy;


    }

    private void CreatePipesWithGap(float gapYPos, float gapSize, float xPosition)
    {
        CreatePipe(gapYPos - gapSize * 0.5f , xPosition, true);
        CreatePipe(CameraSize * 2f - gapYPos - gapSize, xPosition, false);
        noOfPipesSpawned++;
        SetDifficulty(GetDifficulty());


    }

    public void CreatePipe(float height, float xPos, bool bottom)
    {
        if (Bird.GetInstance().isBirdDead() == false && Bird.GetInstance().HasGameStarted()==true)
        {
            Transform pipe = Instantiate(GameAssets.GetInstance().pipe);
            float pipeYpos;
            if (bottom)
            {
                pipeYpos = -CameraSize;
            }
            else
            {
                pipeYpos = CameraSize;
                pipe.localScale = new Vector3(1, -1, 1);
            }



            pipe.position = new Vector2(xPos, pipeYpos);
            SpriteRenderer pipeSr = pipe.GetComponent<SpriteRenderer>();
            pipeSr.size = new Vector2(pipeWidth, height);

            BoxCollider2D pipeCollider = pipe.GetComponent<BoxCollider2D>();
            pipeCollider.size = new Vector2((pipeWidth / 3.3f), height);
            pipeCollider.offset = new Vector2(0f, height * 0.5f);

            Pipe pipeReference = new Pipe(pipe);
            pipeList.Add(pipeReference);

        }
    }

    public int GetPipesSpawned()
    {

        return noOfPipesSpawned;
    }

    public int GetCurrentScore()
    {
        currentScore = pipePassedCount / 2;
        return currentScore;
    }

   

    private class Pipe
    {
        private Transform pipe;
        public Pipe(Transform pipe)
        {
            this.pipe = pipe;

        }

        public void Move()
        {
            if (Bird.GetInstance().isBirdDead() == false ) 
            {
                pipe.position += new Vector3(-1, 0, 0) * pipeMoveSpeed * Time.deltaTime;
            }

        }
        public float GetXposition()
        {
            return pipe.position.x;
        }

        public void DestroyPipe()
        {
            Destroy(pipe.gameObject);
        }

    }
 
    
}


