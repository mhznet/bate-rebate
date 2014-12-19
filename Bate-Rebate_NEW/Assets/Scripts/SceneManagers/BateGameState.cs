using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using AquelaFrameWork.Core.State;
using AquelaFrameWork.Core.Asset;

namespace BateRebate
{
    public class BateGameState : AState
    {
        private GameObject ball;
        private GameObject bg;
        private GameObject touchRight;
        private GameObject touchLeft;
        private GameObject paddleRight;
        private GameObject paddleLeft;
        private GameObject pauseBtn;
        private GameObject scoreLeftShadow;
        private GameObject scoreRightShadow;
        private GameObject scoreLeftOver;
        private GameObject scoreRightOver;

        private GameObject pauseScene;

        private string pauseScenePreFabUrl = "preFabs/BatePauseScenePreFab";
        private string pauseBtnAssetUrl = "Scenes/Game/pausebt";
        private string paddleRightAssetUrl = "Scenes/Game/gamePaddle2";
        private string paddleLeftAssetUrl = "Scenes/Game/gamePaddle";
        private string bgAsset = "Scenes/Game/gameBg";
        private string ballAsset = "Scenes/Game/gameBall";
        private string hitBoxAsset = "Scenes/Game/gameHitOk";

        public int playerNumber = 1;
        private BateController main;
        private GameObject wallNorth;
        private GameObject wallSouth;
        private GameObject wallRight;
        private GameObject wallLeft;

        public override void BuildState()
        {
            main = BateController.Instance;

            VerifyScreenRes();
            CreateBackGround();
            CreateWalls();
            CreateUI();
            CreateBall();
            CreatePaddles();
            CreateTouches();
            touchLeft.GetComponent<PaddleBehaviour>().AwakenPaddle();
            touchRight.GetComponent<PaddleBehaviour>().AwakenPaddle();
            ball.GetComponent<BallBehaviour>().AwakeBall();
            base.BuildState();
        }

        private void VerifyScreenRes()
        {
            bgAsset = main.AddPlatformAndQualityToUrl(bgAsset);
            pauseBtnAssetUrl = main.AddPlatformAndQualityToUrl(pauseBtnAssetUrl);
            ballAsset = main.AddPlatformAndQualityToUrl(ballAsset);
            hitBoxAsset = main.AddPlatformAndQualityToUrl(hitBoxAsset);
            paddleRightAssetUrl = main.AddPlatformAndQualityToUrl(paddleRightAssetUrl);
            paddleLeftAssetUrl = main.AddPlatformAndQualityToUrl(paddleLeftAssetUrl);
        }
        private void CreateWalls()
        {
            Vector3 bgSize = GetExtents();
            Vector3 bgPos = GetPos();

            wallNorth = new GameObject();
            wallNorth.name = "wallU";
            wallNorth.AddComponent<BoxCollider2D>();
            wallNorth.transform.localScale = new Vector3(bgSize.x * 2f, 1f, 1f);
            wallNorth.transform.position = new Vector3(bgPos.x, bgPos.y + bgSize.y + wallNorth.transform.collider2D.bounds.extents.y, 0f);
            Add(wallNorth);

            wallSouth = new GameObject();
            wallSouth.name = "wallD";
            wallSouth.AddComponent<BoxCollider2D>();
            wallSouth.transform.localScale = new Vector3(bgSize.x * 2f, 1f, 1f);
            wallSouth.transform.position = new Vector3(bgPos.x, bgPos.y - bgSize.y - wallSouth.transform.collider2D.bounds.extents.y, 0f);
            Add(wallSouth);

            wallLeft = new GameObject();
            wallLeft.name = "wallL";
            wallLeft.AddComponent<BoxCollider2D>();
            wallLeft.transform.localScale = new Vector3(1f, bgSize.y * 2f, 1f);
            wallLeft.transform.position = new Vector3(bgPos.x - bgSize.x - wallLeft.transform.collider2D.bounds.extents.x, bgPos.y, 0f);
            Add(wallLeft);

            wallRight = new GameObject();
            wallRight.name = "wallR";
            wallRight.AddComponent<BoxCollider2D>();
            wallRight.transform.localScale = new Vector3(1f, bgSize.y * 2f, 1f);
            wallRight.transform.position = new Vector3(bgPos.x + bgSize.x + wallRight.transform.collider2D.bounds.extents.x, bgPos.y, 0f);
            Add(wallRight);
        }
        private Vector3 GetExtents()
        {
            return bg.GetComponent<SpriteRenderer>().sprite.bounds.extents;
        }
        private Vector3 GetPos()
        {
            return bg.transform.position;
        }
        private void CreateBackGround()
        {
            bg = new GameObject();
            bg.name = "background";
            bg.AddComponent<SpriteRenderer>().sprite = Instantiate(AFAssetManager.Instance.Load<Sprite>(bgAsset)) as Sprite;
            Debug.Log(GetExtents());
            Debug.Log(GetPos());
            bg.transform.position = new Vector3(0f, 0f, 10);
            Add(bg);
        }
        private void CreateUI()
        {
            //TODO!
            scoreLeftOver = GameObject.Find("scoreLeftOver");
            scoreRightOver = GameObject.Find("scoreRightOver");
            Add(scoreLeftOver);
            Add(scoreRightOver);
            pauseBtn = GameObject.Find("pausebtn");
            pauseBtn.GetComponent<Image>().sprite = Instantiate(AFAssetManager.Instance.Load<Sprite>(pauseBtnAssetUrl)) as Sprite;
            Add(pauseBtn);
            //Debug.Log(pauseBtn.GetComponent<Image>().sprite.bounds.extents.y);
            //pauseBtn.transform.position = new Vector3(GetPos().x, GetPos().y - GetExtents().y /*+ pauseBtn.GetComponent<Image>().sprite.bounds.extents.y*/,0f);
            pauseBtn.GetComponent<Button>().onClick.AddListener(OnClickPause);

            /*scoreLeftOver.transform.SetParent(main.uiCanvas.transform);
            scoreRightOver.transform.SetParent(main.uiCanvas.transform);
            pauseBtn.transform.SetParent(main.uiCanvas.transform);*/
        }

        private void CreateBall()
        {
            ball = new GameObject();
            ball.name = "ball";
            ball.AddComponent<SpriteRenderer>().sprite = Instantiate(AFAssetManager.Instance.Load<Sprite>(ballAsset)) as Sprite;
            ball.AddComponent<CircleCollider2D>();
            ball.AddComponent<Rigidbody2D>().mass = 0.00001f;
            ball.GetComponent<Rigidbody2D>().gravityScale = 0f;
            ball.collider2D.sharedMaterial = Instantiate(AFAssetManager.Instance.Load<PhysicsMaterial2D>("Materials/ballMaterial")) as PhysicsMaterial2D;
            ball.transform.position = new Vector3(GetPos().x, GetPos().y, 0);
            ball.AddComponent<BallBehaviour>();
            ball.GetComponent<BallBehaviour>().scoreLeftOver = scoreLeftOver;
            ball.GetComponent<BallBehaviour>().scoreRightOver = scoreRightOver;
            Add(ball);
        }
        private void CreateTouches()
        {
            touchLeft = new GameObject();
            touchRight = new GameObject();
            touchLeft.name = "touchBoxLeft";
            touchRight.name = "touchBoxRight";
            touchLeft.AddComponent<BoxCollider2D>().isTrigger = true;
            touchRight.AddComponent<BoxCollider2D>().isTrigger = true;

            touchRight.transform.localScale = new Vector3(GetExtents().x * 0.7f, GetExtents().y * 2f, 1f);
            touchRight.transform.position = new Vector3(GetPos().x + GetExtents().x - touchRight.collider2D.bounds.extents.x, GetPos().y, -9f);

            touchLeft.transform.localScale = new Vector3(GetExtents().x * 0.7f, GetExtents().y * 2f, 1f);
            touchLeft.transform.position = new Vector3(GetPos().x - GetExtents().x + touchLeft.collider2D.bounds.extents.x, GetPos().y, -9f);

            Add(touchRight.AddComponent<PaddleBehaviour>());
            Add(touchLeft.AddComponent<PaddleBehaviour>());
            touchRight.GetComponent<PaddleBehaviour>().enableTouch = (playerNumber == 2);
            touchRight.GetComponent<PaddleBehaviour>().paddleAsset = paddleRight;
            touchRight.GetComponent<PaddleBehaviour>().ball = ball;
            touchLeft.GetComponent<PaddleBehaviour>().enableTouch = true;
            touchLeft.GetComponent<PaddleBehaviour>().paddleAsset = paddleLeft;
            touchLeft.GetComponent<PaddleBehaviour>().ball = ball;

            Add(touchLeft);
            Add(touchRight);
        }
        private void CreatePaddles()
        {
            paddleRight = new GameObject();
            paddleLeft = new GameObject();
            paddleRight.name = "paddleRightAsset";
            paddleLeft.name = "paddleLeftAsset";
            paddleRight.AddComponent<SpriteRenderer>().sprite = Instantiate(AFAssetManager.Instance.Load<Sprite>(paddleRightAssetUrl)) as Sprite;
            paddleLeft.AddComponent<SpriteRenderer>().sprite = Instantiate(AFAssetManager.Instance.Load<Sprite>(paddleLeftAssetUrl)) as Sprite;
            paddleRight.AddComponent<BoxCollider2D>();
            paddleLeft.AddComponent<BoxCollider2D>();
            paddleRight.AddComponent<Rigidbody2D>().mass = 100000f;
            paddleRight.GetComponent<Rigidbody2D>().fixedAngle = true;
            paddleRight.GetComponent<Rigidbody2D>().gravityScale = 0f;
            paddleLeft.AddComponent<Rigidbody2D>().mass = 100000f;
            paddleLeft.GetComponent<Rigidbody2D>().fixedAngle = true;
            paddleLeft.GetComponent<Rigidbody2D>().gravityScale = 0f;
            paddleRight.transform.position = new Vector3(GetPos().x + GetExtents().x - paddleRight.collider2D.bounds.extents.x * 3f, GetPos().y, 9f);
            paddleLeft.transform.position = new Vector3(GetPos().x - GetExtents().x + paddleLeft.collider2D.bounds.extents.x * 3f, GetPos().y, 9f);
            Add(paddleLeft);
            Add(paddleRight);
        }
        public void OnClickPause()
        {
            ball.GetComponent<BallBehaviour>().Pause();
            touchLeft.GetComponent<PaddleBehaviour>().Pause();
            touchRight.GetComponent<PaddleBehaviour>().Pause();

            /*Application.LoadLevel("sceneName");*/
            if (pauseScene == null)
            {
                pauseScene = Instantiate(AFAssetManager.Instance.Load<GameObject>(pauseScenePreFabUrl)) as GameObject;
            }
            pauseScene.transform.position = new Vector3(GetPos().x, GetPos().y, 1f);
            pauseScene.SetActive(true);
        }
        public void OnClickUnPause()
        {
            ball.GetComponent<BallBehaviour>().Pause();
            paddleLeft.GetComponent<PaddleBehaviour>().Pause();
            paddleRight.GetComponent<PaddleBehaviour>().Pause();

            pauseScene.SetActive(false);
        }
    }
}