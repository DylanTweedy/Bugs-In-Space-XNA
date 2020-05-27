using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BugsInSpace
{
    class Levels
    {
        #region Variables

        public List<Enemy> enemies;

        Random random;
        TimeSpan enemiesPreviousTime;
        TimeSpan enemiesTime;
        TimeSpan enemiesSpawnTime;
        TimeSpan enemiesPreviousSpawnTime;
        bool load1;
        bool stopSpawn;
        public bool test;
        public bool level1;
        int enemyNumbers;
        public bool levelComplete;
        int enemyLocationMove;
        bool MoveCounter;
        TimeSpan locationMoveTime;
        TimeSpan previousLocationMoveTime;

        #endregion

        #region properties

        public int screenWidth
        {
            get { return Game1.Instance.GraphicsDevice.Viewport.Width; }
        }

        public int screenHeight
        {
            get { return Game1.Instance.GraphicsDevice.Viewport.Height; }
        }

        #endregion

        public void Initialize(GameTime gameTime)
        {
            enemies = new List<Enemy>();

            random = new Random();

            enemiesSpawnTime = TimeSpan.FromSeconds(0.1f);
            enemiesPreviousSpawnTime = TimeSpan.Zero;
            enemiesTime = TimeSpan.FromSeconds(9.0f);
            enemiesPreviousTime = gameTime.TotalGameTime;
            locationMoveTime = TimeSpan.FromSeconds(0.1f);
            previousLocationMoveTime = gameTime.TotalGameTime;
            enemyNumbers = 0;
            load1 = true;
            stopSpawn = false;
            test = false;
            levelComplete = false;
            enemyLocationMove = 0;
            MoveCounter = false;

            level1 = false;
        }

        public void Update(GameTime gameTime)
        {
            Level1(gameTime);

            if (enemies.Count == enemyNumbers)
                stopSpawn = true;

            if (stopSpawn && enemies.Count == 0)
                levelComplete = true;

            if (gameTime.TotalGameTime - enemiesPreviousSpawnTime > enemiesSpawnTime && enemies.Count < enemyNumbers)
            {
                enemiesPreviousSpawnTime = gameTime.TotalGameTime;

                if (stopSpawn == false)
                {
                    AddEnemy();

                    for (int e = 0; e < enemies.Count; e++)
                    {
                        enemies[e].previousStaticMoveTime = gameTime.TotalGameTime;
                    }
                }
            }

            for (int e = 0; e < enemies.Count; e++)
            {
                enemies[e].Update(gameTime);

                enemies[e].EnemyNumber = e + 1;

                enemies[e].enemyLocationMove = enemyLocationMove;

                if (gameTime.TotalGameTime - enemies[e].previousStaticMoveTime > enemies[e].staticMoveTime && enemies[e].customMovement == false)
                {
                    enemies[e].customMovement = true;
                    enemies[e].previousStaticMoveTime = gameTime.TotalGameTime;
                    enemies[e].staticMoveTime = TimeSpan.FromSeconds(random.Next(50, 100) / 10f);

                    enemies[e].movementPattern = 1;
                }

                if (gameTime.TotalGameTime - enemies[e].previousStaticMoveTime > enemies[e].staticMoveTime && enemies[e].customMovement == true)
                {
                    enemies[e].customMovement = false;
                    enemies[e].previousStaticMoveTime = gameTime.TotalGameTime;
                    enemies[e].staticMoveTime = TimeSpan.FromSeconds(random.Next(100, 500) / 10f);
                }

                if (gameTime.TotalGameTime - enemies[e].previousStaticMoveTime < enemies[e].staticMoveTime && enemies[e].customMovement == false)
                {
                    enemies[e].movementPattern = 2;
                }

                if (enemies[e].Active == false)
                {
                    enemies.RemoveAt(e);
                }
            }

            if (gameTime.TotalGameTime - previousLocationMoveTime > locationMoveTime)
            {
                previousLocationMoveTime = gameTime.TotalGameTime;

                if (MoveCounter)
                    enemyLocationMove += 3;
                else
                    enemyLocationMove -= 3;

                if (enemyLocationMove > 30)
                    MoveCounter = false;
                if (enemyLocationMove < -30)
                    MoveCounter = true;
            }
        }

        public void Level1(GameTime gameTime)
        {
            enemyNumbers = 50;
        }

        public void AddEnemy()
        {
            if (level1)
            {
                Enemy enemy = new Enemy();
                enemy.Initialize(21, 1, 1, 1, true, 10, 150);
                if (load1)
                {
                    enemy.LoadContent(new Vector2(-50, 327));
                    load1 = false;
                }
                else
                {
                    enemy.LoadContent(new Vector2(screenWidth + 50, 327));
                    load1 = true;
                }
                enemies.Add(enemy);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int e = 0; e < enemies.Count; e++)
            {
                enemies[e].Draw(spriteBatch);
            }
        }
    }
}
