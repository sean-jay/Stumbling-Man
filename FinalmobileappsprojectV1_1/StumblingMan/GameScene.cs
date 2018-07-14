using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Timers;
using CoreGraphics;
using Foundation;
using SpriteKit;
using UIKit;

namespace StumblingMan
{
    public class GameScene : SKScene, ISKPhysicsContactDelegate
    {
        bool tappedHelp = false;
        bool pauseTouched = false;
		bool inprogress = false;
		bool gameended = false;
		float speed;
		bool DIR;
		float difficulty = 500;
		bool inexecution = false;


		System.Timers.Timer timer;
		System.Timers.Timer timer1;
		System.Timers.Timer timer2;
		System.Timers.Timer timer3;
		System.Timers.Timer timer4;
		int score = 0;


		List<int> scores = new List<int>();
		Random r = new Random();

		List<SKLabelNode> scorelist = new List<SKLabelNode>();

        SKSpriteNode leftBorder;
        SKSpriteNode rightBorder;
        SKSpriteNode pauseOverlay;
        SKSpriteNode pauseButton;
        SKSpriteNode helpButton;
		SKAudioNode startScreen;
        SKAudioNode gameSong;
        SKAudioNode playerDied;

        SKLabelNode highScores;
		SKLabelNode scorelabel;
		SKLabelNode gameTitle;
        SKSpriteNode instructions;

        SKSpriteNode sidewalk1;
        SKSpriteNode sidewalk2;
        SKSpriteNode sidewalk3;

        SKSpriteNode enemy;
        SKSpriteNode playerObject;
        SKSpriteNode obstacle;
        SKLabelNode endGame;
      
        SKAction deathSpin;
        SKAction playerAnimate;
		SKAction obstacleflame;
		SKAction obstacleflame4;
        SKAction walkAnimate;
        SKAction enemyAnimate;

        uint playerHit = 1;
        uint obstacleHit = 2;
        uint leftHit = 3;
        uint rightHit = 4;
        //uint enemyHit = 5;
        //new stuff

        double roadx = 678 + 51;
        double buildx = 327;
        double roadhalf = 0;
        double buildhalf = 0;
        double sidehalf = 202.5 - 1;
		double obhalf = 0;

        SKSpriteNode road1;
        SKSpriteNode road2;
        SKSpriteNode road3;
        SKSpriteNode road4;
        SKSpriteNode road5;
        SKSpriteNode road6;

        SKSpriteNode building1;
        SKSpriteNode building2;
        SKSpriteNode building3;
        SKSpriteNode building4;
        SKSpriteNode building5;
        SKSpriteNode building6;
        SKSpriteNode building7;
        SKSpriteNode building8;
        SKSpriteNode building9;
        SKSpriteNode building10;
        SKSpriteNode building11;
        SKSpriteNode building12;
        SKSpriteNode building13;
        SKSpriteNode building14;

		SKSpriteNode obstacle2;
		SKSpriteNode obstacle1;
		SKSpriteNode obstacle4;


        protected GameScene(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

		void sendobstacle1(object sender, ElapsedEventArgs e)
		{
			if (inprogress && !inexecution)
			{
				inexecution = true;

				if (obstacle1.Frame.Top >= Frame.Bottom)
				{
					if (!((obstacle2.Frame.Y >= ( Frame.Height / 2 + 200) ) && (obstacle2.Frame.Top <= Frame.Bottom)))
					{
						Scroll(obstacle1);
					}
				}
				timer1.Interval = r.Next(1000,5000);
				inexecution = false;
			}

		}

		void sendobstacle2(object sender, ElapsedEventArgs e)
		{
			
			if (inprogress && !inexecution)
			{
				inexecution = true;
				// send an obstacle 
				if (obstacle2.Frame.Top >= Frame.Bottom)
				{

					if ( !((obstacle1.Frame.Y >= (Frame.Height / 2 + 200 ) ) && (obstacle1.Frame.Top <= Frame.Bottom)) )
					{
                        Scroll(obstacle2);
					}


				}
				timer2.Interval = r.Next(1000,5000);
				inexecution = false;
			}

		}

		void sendobstacle3(object sender, ElapsedEventArgs e)
		{
			if (inprogress)
			{
				Console.WriteLine("madeittofinal");
				// send an obstacle 
				if (obstacle.Frame.Top <= Frame.Width/1.7)
				{
					Console.WriteLine("madeittofinal2");
					if (enemy.Frame.Top >= Frame.Bottom)
					{
						Console.WriteLine("madeittofinal3");
						var xpos = r.Next((int)(building1.Frame.Right + enemy.Frame.Width / 2 + 2), (int)(road1.Frame.Left - 10 + enemy.Frame.Width / 2 + 2));
						enemy.Position = new CGPoint(xpos, enemy.Position.Y);
						animateEnemy();
						Scroll(enemy);
					}

				}
			}
		}

		void sendobstacle(object sender, ElapsedEventArgs e)
		{
			if (inprogress)
			{
				// send an obstacle 
				if (obstacle.Frame.Top >= Frame.Bottom)
				{

					var xpos = r.Next((int)(Frame.Width/2), (int)(road1.Frame.Left - 10 + obstacle.Frame.Width / 2 + 2));
					obstacle.Position = new CGPoint(xpos, obstacle.Position.Y);
					animateobstacle();
					Scroll(obstacle);
				}
			}

		}

		void sendobstacle4(object sender, ElapsedEventArgs e)
		{
			if (inprogress)
			{
				if (obstacle.Frame.Top <= Frame.Width / 1.8)
				{

					if (enemy.Frame.Top <= Frame.Width / 1.8)
					{

						// send an obstacle 
						if (obstacle4.Frame.Top >= Frame.Bottom)
						{
							Console.WriteLine("4 is going");
							var xpos = r.Next((int)(building1.Frame.Right + obstacle4.Frame.Width / 2 + 2), (int)Frame.Width/2);
							obstacle4.Position = new CGPoint(xpos, obstacle4.Position.Y);
							animateobstacle4();
							Scroll(obstacle4);
						}

					}

				}
			}

		}

  #region Animation
        // definition of sidewalk scrolling action
        public void Scroll(SKSpriteNode sidewalk)
        {

            walkAnimate = SKAction.RepeatActionForever(SKAction.MoveBy(0.0f, -.09f, 0.0003f));
            sidewalk.RunAction(walkAnimate);
        }
        // definition of player walking animation
        void animatePlayer()
        {
            var textures = Enumerable.Range(1, 4).Select(
            (i) => SKTexture.FromImageNamed(String.Format("Stumbler-{0}", i))).ToArray();
            playerAnimate = SKAction.RepeatActionForever(SKAction.AnimateWithTextures(textures, 0.1));
            playerObject.RunAction(playerAnimate);

		}

		void animateobstacle()
		{
			var textures = Enumerable.Range(1, 4).Select(
			(i) => SKTexture.FromImageNamed(String.Format("trash-{0}", i))).ToArray();
			obstacleflame = SKAction.RepeatActionForever(SKAction.AnimateWithTextures(textures, 0.1));
			obstacle.RunAction(obstacleflame);
		}
		void animateobstacle4()
		{
			var textures = Enumerable.Range(1, 4).Select(
			(i) => SKTexture.FromImageNamed(String.Format("trash-{0}", i))).ToArray();
			obstacleflame4 = SKAction.RepeatActionForever(SKAction.AnimateWithTextures(textures, 0.1));
			obstacle4.RunAction(obstacleflame4);
		}
		void animateEnemy()
		{
			var textures = Enumerable.Range(1, 4).Select(
            (i) => SKTexture.FromImageNamed(String.Format("walker-{0}", i))).ToArray();
            enemyAnimate = SKAction.RepeatActionForever(SKAction.AnimateWithTextures(textures, 0.1));
            enemy.RunAction(enemyAnimate);
        }

#endregion


        #region audio
        public override void DidMoveToView(SKView view)
		{

 			var plist = NSUserDefaults.StandardUserDefaults; 			for (int i = 0; i < 10; i++) 			{ 				var s = plist.IntForKey(String.Format("Score-{0}", i));
				var ns = Convert.ToInt32(s);
				scores.Add(ns); 			}


			// music while player is walking
			gameSong = new SKAudioNode("walking")
			{
				AutoplayLooped = true,
				Positional = false,
			};

			// sound when player dies
			playerDied = new SKAudioNode("playerDeath")
			{
				AutoplayLooped = false,
				Positional = false,

			};

            //start screen music
            startScreen = new SKAudioNode("gameTitle")
            {
                AutoplayLooped = true,
                Positional = false,

            };
			#endregion

			#region sprites
			// sprites

			for (int i = 9; i >= 0; i--)
			{ 
			
				SKLabelNode ascore = new SKLabelNode("chalkduster")
				{
					Text = String.Format("Score: {0}", scores[i]),
					FontSize = 30,
					FontColor = UIColor.White,
					Position = new CGPoint(Frame.Width / 2, Frame.Height / 1.1),
					ZPosition = 20
				};

				scorelist.Add(ascore);

			}

            instructions = new SKSpriteNode("intro")
            {
                XScale = 1,
                YScale = 1,
                Position = new CGPoint(Frame.Width / 2, Frame.Height / 2),
                ZPosition = 50
            };
            var enemySize = new CGSize(25, 25);
            enemy = new SKSpriteNode("walker-1")
            {

                PhysicsBody = SKPhysicsBody.CreateRectangularBody(enemySize),
                Position = new CGPoint(Frame.Width / 2, Frame.Bottom + 40),
                XScale = 2.0f,
                YScale = 2.0f,

                ZPosition = 0.24f
            };
            helpButton = new SKSpriteNode("help")
            {
                XScale = .25f,
                YScale = .25f,
				Position = new CGPoint(buildx + 5, Frame.Bottom - 35),
                ZPosition = 25,
            };
            highScores = new SKLabelNode("chalkduster")
            {
                Text = "High Scores",
                FontSize = 30,
                FontColor = UIColor.White,
                Position = new CGPoint(Frame.Width / 2, Frame.Height / 1.1),
                ZPosition = 20
            };

			leftBorder = new SKSpriteNode("overlay")
            {
                XScale = 0.1f,
                YScale = 10,
                Position = new CGPoint(buildx - 10, Frame.Height / 2),
                ZPosition = .25f,
				Alpha = .01f,
				PhysicsBody = SKPhysicsBody.CreateRectangularBody(new CGSize(90, 1000)),

            };
			rightBorder = new SKSpriteNode("overlay")
            {
                XScale = 0.1f,
                YScale = 10,
                Position = new CGPoint(roadx + 42, Frame.Height / 2),
                ZPosition = .25f,
				Alpha = .01f,
				PhysicsBody = SKPhysicsBody.CreateRectangularBody(new CGSize(80, 1000)),

            };

            pauseOverlay = new SKSpriteNode("overlay")
            {
                XScale = 20,
                YScale = 10,
                Position = new CGPoint(Frame.Width / 2, Frame.Height / 2),
				Color = UIColor.Black,
                ZPosition = 10,
				Alpha = 0.75f,
            };
			pauseButton = new SKSpriteNode("pauseButton")
            {
                XScale = .1f,
                YScale = .1f,
                Position = new CGPoint(Frame.Width / 2, Frame.Height / 10),
                ZPosition = 21f,
            };
			road1 = new SKSpriteNode("Road")
            {
                XScale = 0.7f,
                YScale = 0.7f,
                Position = new CGPoint(roadx, 89),
                ZPosition = 0.2f
            };
			roadhalf = (road1.Frame.Height / 2) - 1;

            road1.Position = new CGPoint(roadx, roadhalf);

            road2 = new SKSpriteNode("Road")
            {
                XScale = 0.7f,
                YScale = 0.7f,
                Position = new CGPoint(roadx, roadhalf + (road1.Frame.Bottom)),
                ZPosition = 0.2f
            };

            road3 = new SKSpriteNode("Road")
            {
                XScale = 0.7f,
                YScale = 0.7f,
                Position = new CGPoint(roadx, roadhalf + (road2.Frame.Bottom)),
                ZPosition = 0.2f
            };

            road4 = new SKSpriteNode("Road")
            {
                XScale = 0.7f,
                YScale = 0.7f,
                Position = new CGPoint(roadx, roadhalf + (road3.Frame.Bottom)),
                ZPosition = 0.2f
            };

            road5 = new SKSpriteNode("Road")
            {
                XScale = 0.7f,
                YScale = 0.7f,
                Position = new CGPoint(roadx, roadhalf + (road4.Frame.Bottom)),
                ZPosition = 0.2f
            };

            road6 = new SKSpriteNode("Road")
            {
                XScale = 0.7f,
                YScale = 0.7f,
                Position = new CGPoint(roadx, roadhalf + (road5.Frame.Bottom)),
                ZPosition = 0.2f
            };
			building1 = new SKSpriteNode("Building")
			{
				XScale = 1.0f,
				YScale = 1.0f,
				Position = new CGPoint(buildx, 0),
				ZPosition = 0.3f
			};

			buildhalf = (building1.Frame.Height / 2) - 1;

			building2 = new SKSpriteNode("Building")
			{
				XScale = 1.0f,
				YScale = 1.0f,
				Position = new CGPoint(buildx, buildhalf + (building1.Frame.Bottom)),
                ZPosition = 0.3f
            };

            building3 = new SKSpriteNode("Building")
            {
                XScale = 1.0f,
                YScale = 1.0f,
                Position = new CGPoint(buildx, buildhalf + (building2.Frame.Bottom)),
                ZPosition = 0.3f
            };

			building4 = new SKSpriteNode("Building")
			{
				XScale = 1.0f,
				YScale = 1.0f,
				Position = new CGPoint(0 + 327, (65 / 2) + (building3.Frame.Bottom)),
				ZPosition = 0.3f
			};

			building5 = new SKSpriteNode("Building")
			{
				XScale = 1.0f,
				YScale = 1.0f,
				Position = new CGPoint(buildx, buildhalf + (building4.Frame.Bottom)),
				ZPosition = 0.3f
			};

			building6 = new SKSpriteNode("Building")
			{
				XScale = 1.0f,
				YScale = 1.0f,
				Position = new CGPoint(buildx, buildhalf + (building5.Frame.Bottom)),
				ZPosition = 0.3f
			};

			building7 = new SKSpriteNode("Building")
			{
				XScale = 1.0f,
				YScale = 1.0f,
				Position = new CGPoint(buildx, buildhalf + (building6.Frame.Bottom)),
				ZPosition = 0.3f
			};

			building8 = new SKSpriteNode("Building")
			{
				XScale = 1.0f,
				YScale = 1.0f,
				Position = new CGPoint(buildx, buildhalf + (building7.Frame.Bottom)),
				ZPosition = 0.3f
			};

			building9 = new SKSpriteNode("Building")
			{
				XScale = 1.0f,
				YScale = 1.0f,
				Position = new CGPoint(buildx, buildhalf + (building8.Frame.Bottom)),
				ZPosition = 0.3f
			};

			building10 = new SKSpriteNode("Building")
			{
				XScale = 1.0f,
				YScale = 1.0f,
				Position = new CGPoint(buildx, buildhalf + (building9.Frame.Bottom)),
				ZPosition = 0.3f
			};

			building11 = new SKSpriteNode("Building")
			{
				XScale = 1.0f,
				YScale = 1.0f,
				Position = new CGPoint(buildx, buildhalf + (building10.Frame.Bottom)),
				ZPosition = 0.3f
			};

			building12 = new SKSpriteNode("Building")
			{
				XScale = 1.0f,
				YScale = 1.0f,
				Position = new CGPoint(buildx, buildhalf + (building11.Frame.Bottom)),
				ZPosition = 0.3f
			};

			building13 = new SKSpriteNode("Building")
			{
				XScale = 1.0f,
				YScale = 1.0f,
				Position = new CGPoint(buildx, buildhalf + (building12.Frame.Bottom)),
				ZPosition = 0.3f
			};

			building14 = new SKSpriteNode("Building")
			{
				XScale = 1.0f,
				YScale = 1.0f,
				Position = new CGPoint(buildx, buildhalf + (building13.Frame.Bottom)),
				ZPosition = 0.3f
			};

			scorelabel = new SKLabelNode("Gillsans-UltraBold")
			{
				Text = String.Format("Score: {0}", score),
				FontSize = 25,
				FontColor = UIColor.White,
				Name = "SCORELABEL",
				Position = new CGPoint(Frame.Width / 2, Frame.Bottom - 27),
				ZPosition = .5f,
			};


			gameTitle = new SKLabelNode("zapfino")
			{
				Text = "Stumbling Man",
				FontSize = 27,
				FontColor = UIColor.Black,
				Name = "stumblingMan",
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2),
				ZPosition = .5f,
			};

			endGame = new SKLabelNode("GillSans-UltraBold")
			{
				Text = "Game Over",
				FontSize = 30,
				Name = "gameOver",
				FontColor = UIColor.Blue,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2),
				ZPosition = .5f
			};

			var playerSize = new CGSize(25, 25);
			playerObject = new SKSpriteNode("Stumbler-1")
			{

				PhysicsBody = SKPhysicsBody.CreateRectangularBody(playerSize),
				XScale = 2.0f,
				YScale = 2.0f,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 4),
				ZPosition = 0.25f
			};


			obstacle = new SKSpriteNode("trash-1")
			{
				PhysicsBody = SKPhysicsBody.CreateRectangularBody(playerSize),
				XScale = 0.3f,
				YScale = 0.3f,
				Position = new CGPoint(Frame.Width / 2, Frame.Bottom),
				ZPosition = 0.24f
			};

			obhalf = (obstacle.Frame.Height / 2);
			obstacle.Position = new CGPoint(Frame.Width / 2, Frame.Bottom + obhalf);

			obstacle4 = new SKSpriteNode("trash-1")
			{
				PhysicsBody = SKPhysicsBody.CreateRectangularBody(playerSize),
				XScale = 0.3f,
				YScale = 0.3f,
				Position = new CGPoint(Frame.Width / 2, Frame.Bottom + obhalf),
				ZPosition = 0.24f
			};

			obstacle1 = new SKSpriteNode("Viper")
			{
				PhysicsBody = SKPhysicsBody.CreateRectangularBody(new CGSize(1, 1)),
				XScale = 0.3f,
				YScale = 0.3f,
				Position = new CGPoint(roadx - 23, Frame.Bottom + obhalf + 1),
				ZPosition = 0.24f
			};

			obstacle2 = new SKSpriteNode("Taxi")
			{
				PhysicsBody = SKPhysicsBody.CreateRectangularBody(playerSize),
				XScale = .3f,
				YScale = .3f,
				Position = new CGPoint(roadx - 23, Frame.Bottom + obhalf + 1),
				ZPosition = 0.24f
			};

			sidewalk1 = new SKSpriteNode("Sidewalk")
			{
				XScale = 1.0f,
				YScale = 1.0f,
				Position = new CGPoint(Frame.Width / 2, Frame.Bottom - sidehalf),
				ZPosition = 0.0f
			};

			sidewalk2 = new SKSpriteNode("Sidewalk")
			{
				XScale = 1.0f,
				YScale = 1.0f,
				Position = new CGPoint(Frame.Width / 2, sidewalk1.Frame.Top - sidehalf),
				ZPosition = 0.0f
			};

			sidewalk3 = new SKSpriteNode("Sidewalk")
			{
				XScale = 1.0f,
				YScale = 1.0f,
				Position = new CGPoint(Frame.Width / 2, sidewalk2.Frame.Top - sidehalf),
				ZPosition = 0.0f
			};
			#endregion
			// calls to add sprites to scene
			playerObject.PhysicsBody.CategoryBitMask = playerHit;
			playerObject.PhysicsBody.ContactTestBitMask = obstacleHit | leftHit | rightHit;
			playerObject.PhysicsBody.CollisionBitMask = obstacleHit | leftHit | rightHit;
			//playerObject.PhysicsBody.Dynamic = false;

			obstacle.PhysicsBody.CategoryBitMask = obstacleHit;
			obstacle.PhysicsBody.ContactTestBitMask = playerHit;
			obstacle.PhysicsBody.CollisionBitMask = playerHit;

			obstacle1.PhysicsBody.CategoryBitMask = obstacleHit;
			obstacle1.PhysicsBody.ContactTestBitMask = playerHit;
			obstacle1.PhysicsBody.CollisionBitMask = playerHit;

			obstacle2.PhysicsBody.CategoryBitMask = obstacleHit;
			obstacle2.PhysicsBody.ContactTestBitMask = playerHit;
			obstacle2.PhysicsBody.CollisionBitMask = playerHit;

			obstacle4.PhysicsBody.CategoryBitMask = obstacleHit;
			obstacle4.PhysicsBody.ContactTestBitMask = playerHit;
			obstacle4.PhysicsBody.CollisionBitMask = playerHit;

			leftBorder.PhysicsBody.CategoryBitMask = leftHit;
			leftBorder.PhysicsBody.ContactTestBitMask = playerHit;
			leftBorder.PhysicsBody.CollisionBitMask = playerHit;

			rightBorder.PhysicsBody.CategoryBitMask = rightHit;
			rightBorder.PhysicsBody.ContactTestBitMask = playerHit;
			rightBorder.PhysicsBody.CollisionBitMask = playerHit;

            enemy.PhysicsBody.CategoryBitMask = obstacleHit;
            enemy.PhysicsBody.ContactTestBitMask = playerHit;
            enemy.PhysicsBody.CollisionBitMask = playerHit;
			////
			PhysicsWorld.ContactDelegate = this;
			/////

			#region gamestart
			void gameStart()

			{
				PhysicsWorld.Gravity = new CGVector(0, 0);

				AddChild(building1);
				AddChild(building2);
				AddChild(building3);
				AddChild(building4);
				AddChild(building5);
				AddChild(building6);
				AddChild(building7);
				AddChild(building8);
				AddChild(building9);
				AddChild(building10);
				AddChild(building11);
				AddChild(building12);
				AddChild(building13);
				AddChild(building14);
				AddChild(road1);
				AddChild(road2);
				AddChild(road3);
				AddChild(road4);
				AddChild(road5);
				AddChild(road6);
				AddChild(sidewalk1);
				AddChild(sidewalk2);
				AddChild(sidewalk3);
				AddChild(startScreen);
				AddChild(gameTitle);
				AddChild(playerDied);
				AddChild(leftBorder);
				AddChild(rightBorder);
				AddChild(scorelabel);
				AddChild(obstacle);
				AddChild(obstacle1);
				AddChild(obstacle2);
                AddChild(helpButton);
                AddChild(enemy);
				AddChild(obstacle4);
                animateEnemy();
			}

			gameStart();
        }
#endregion

        [Export("didBeginContact:")]
        public void DidBeginContact(SKPhysicsContact contact)
        {
			inprogress = false;

            Console.WriteLine("We Collided!");
            gameended = true;
            void gameOver()
            {
                var textures = Enumerable.Range(1, 4).Select(
                    (i) => SKTexture.FromImageNamed(String.Format("Death-{0}", i))).ToArray();
                deathSpin = SKAction.RepeatAction(SKAction.AnimateWithTextures(textures, .1), 4);
                var shrink = SKAction.ScaleTo(0f, 2f);
                var death = SKAction.Sequence(deathSpin, shrink);
                playerObject.RunAction(death);

                var playDeath = SKAction.PlaySoundFileNamed("playerDeath", false);
                playerDied.RunAction(playDeath);
                gameSong.RunAction(SKAction.CreateStop());

            }

            void animateOver() 
            {
                var grow = SKAction.ScaleTo(2.0f, 1.5);
                var shrink = SKAction.ScaleTo(.5f, 1.5);
                var animate = SKAction.RepeatActionForever(SKAction.Sequence(grow, shrink));
                endGame.RunAction(animate);
            }


            playerObject.RemoveAllActions();
            obstacle.RemoveAllActions();
            sidewalk1.RemoveAllActions();
            sidewalk2.RemoveAllActions();
            sidewalk3.RemoveAllActions();
			building1.RemoveAllActions();
			building2.RemoveAllActions();
			building3.RemoveAllActions();
			building4.RemoveAllActions();
			building5.RemoveAllActions();
			building6.RemoveAllActions();
			building7.RemoveAllActions();
			building8.RemoveAllActions();
			building9.RemoveAllActions();
			building10.RemoveAllActions();
			building11.RemoveAllActions();
			building12.RemoveAllActions();
			building13.RemoveAllActions();
			building14.RemoveAllActions();
			road1.RemoveAllActions();
			road2.RemoveAllActions();
			road3.RemoveAllActions();
			road4.RemoveAllActions();
			road5.RemoveAllActions();
			road6.RemoveAllActions();
			obstacle1.RemoveAllActions();
			obstacle2.RemoveAllActions();
			enemy.RemoveAllActions();
			obstacle4.RemoveAllActions();

			scores.Add(score);
			scores.Sort((a, b) => -1 * a.CompareTo(b));   			var sl = scores.Count; 			if (sl >= 10) 			{ 				sl = 10; 			}  			var plist = NSUserDefaults.StandardUserDefaults; 			for (int i = 0; i < sl; i++) 			{ 				Console.WriteLine(scores[i]); 				plist.SetInt(scores[i], String.Format("Score-{0}", i));  			}

			score = 0;

			//unsubscribe obstacle objects at the end of the round
			timer.Elapsed -= sendobstacle;
			timer1.Elapsed -= sendobstacle1;
			timer2.Elapsed -= sendobstacle2;
			timer3.Elapsed -= sendobstacle3;
			timer4.Elapsed -= sendobstacle4;

            gameOver();

            AddChild(endGame);

            animateOver();

            
            

        }
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            // Called when a touch begins

            // control player motion
            base.TouchesBegan(touches, evt);
            var touch = (UITouch)touches.AnyObject;
            var touchedNode = GetNodeAtPoint(touch.LocationInNode(this));


            Console.WriteLine(touch.LocationInNode(this));
            Console.WriteLine(gameTitle.Position);
            Console.WriteLine(touchedNode);

            //starts game on title tap
            if(touchedNode.Name == "stumblingMan")
            {
                Console.WriteLine("it worked");
                AddChild(gameSong);
                startScreen.RemoveFromParent();
                gameTitle.RemoveFromParent();
                AddChild(playerObject);
                AddChild(pauseButton);

                startGame();
            }
            else if (touchedNode == helpButton)
            {
                if (tappedHelp == false)
                {
                    tappedHelp = true;
                    AddChild(instructions);
                }
                else 
                {
                    tappedHelp = false;
                    instructions.RemoveFromParent();
                }
            }
            else if (touchedNode.Name == "gameOver")
            {
				

				obstacle.Position = new CGPoint(Frame.Width / 2, Frame.Bottom + obhalf + 5);
				enemy.Position = new CGPoint(Frame.Width / 2, Frame.Bottom + obhalf + 5);
				obstacle1.Position = new CGPoint(obstacle1.Frame.X + obstacle1.Frame.Width / 2, Frame.Bottom + obhalf + 1);
				obstacle2.Position = new CGPoint(obstacle2.Frame.X + obstacle2.Frame.Width / 2, Frame.Bottom + obhalf + 1);
				obstacle4.Position = new CGPoint(obstacle4.Frame.X + obstacle4.Frame.Width / 2, Frame.Bottom + obhalf + 1);

                playerObject.Paused = true;
                playerObject.RemoveAllActions();
				speed = 0;
                endGame.RemoveFromParent();
                gameSong.RunAction(SKAction.CreatePlay());
                startGame();

            }
            else if (touchedNode == pauseButton)
            {
                gamePause();
            }

            else if (!gameended)
            {
                playerMove(touch);
            }

            void gamePause()
            {
                if (pauseTouched == false)
                {
					Console.WriteLine("UNSstartpause");

					// all obstacles must unsubscribe at the begining of a pause
					if (inprogress)
					{
						timer.Elapsed -= sendobstacle;
						timer1.Elapsed -= sendobstacle1;
						timer2.Elapsed -= sendobstacle2;
						timer3.Elapsed -= sendobstacle3;
						timer4.Elapsed -= sendobstacle4;
					}

					pauseTouched = true;
                    AddChild(pauseOverlay);
					AddChild(highScores);

					for (int i = 0; i < 10; i++)
					{
						if (i >= 1)
						{
							scorelist[i].Text = String.Format("Score: {0}", scores[i]);
							scorelist[i].Position = new CGPoint(Frame.Width/2, scorelist[i-1].Position.Y - (scorelist[i-1].Frame.Height/2) - 42);
						}
						else
						{

							scorelist[i].Text = String.Format("Score: {0}", scores[i]);
							scorelist[i].Position = new CGPoint(Frame.Width/2 , highScores.Position.Y - (highScores.Frame.Height/2) - 42);

						}
						AddChild(scorelist[i]);

					}
					Scene.Paused = true;

                }
                else 
                {
					Console.WriteLine("SUBendpause");

					//all obstacle timers must subscribe at the end of a pause
					if (inprogress)
					{
						timer.Elapsed += sendobstacle;
						timer1.Elapsed += sendobstacle1;
						timer2.Elapsed += sendobstacle2;
						timer3.Elapsed += sendobstacle3;
						timer4.Elapsed += sendobstacle4;
					}

					pauseOverlay.RemoveFromParent();
                    highScores.RemoveFromParent();
					for (int i = 0; i< 10; i++)
					{
						scorelist[i].RemoveFromParent();

					}
                    pauseTouched = false;
                    Scene.Paused = false;
                
				}
            }
            // detects left and right screen taps, moves player accordingly
            void playerMove(UITouch location)
            {
				

				if (touch.LocationInNode(this).X >= playerObject.Frame.Right)
				{
					if (DIR)
					{
						Console.WriteLine("clickright + moveright" + speed);
						playerObject.RunAction(SKAction.RepeatActionForever(SKAction.MoveBy(difficulty, 0, 2.0f)));
						speed = speed + difficulty;

					}
					else
					{
						Console.WriteLine("clickright + moveleft" + speed);
						playerObject.RunAction(SKAction.RepeatActionForever(SKAction.MoveBy(difficulty - speed, 0, 2.0f)));
						speed = difficulty;
						DIR = true;
					}




                }
				if (touch.LocationInNode(this).X < playerObject.Frame.Left)
                {

					if (!DIR)
					{
						Console.WriteLine("clickleft + moveleft" + speed);
						playerObject.RunAction(SKAction.RepeatActionForever(SKAction.MoveBy(- difficulty, 0, 2.0f)));
						speed = speed - difficulty;

					}
					else
					{
						Console.WriteLine("clickleft + moveleft" + speed);
						playerObject.RunAction(SKAction.RepeatActionForever(SKAction.MoveBy(- difficulty - speed, 0, 2.0f)));
						speed = - difficulty;
						DIR = false;

					}

                    
                }
            }

            void startGame()
            {
				//setting the game is in progress to true
				playerObject.PhysicsBody.AllowsRotation = false;
				obstacle.PhysicsBody.AllowsRotation = false;
				enemy.PhysicsBody.AllowsRotation = false;
				obstacle1.PhysicsBody.AllowsRotation = false;
				obstacle2.PhysicsBody.AllowsRotation = false;
				obstacle4.PhysicsBody.AllowsRotation = false;

				inprogress = true;
                gameended = false;
                playerObject.Paused = false;

				#region for timers

				timer = new System.Timers.Timer();
				timer.Interval = 1000;
				Console.WriteLine("SUB");
				timer.Elapsed += sendobstacle;
				timer.AutoReset = true;
				timer.Enabled = true;

				timer1 = new System.Timers.Timer();
				timer1.Interval = 2000;
				Console.WriteLine("SUB");
				timer1.Elapsed += sendobstacle1;
				timer1.AutoReset = true;
				timer1.Enabled = true;

				timer2 = new System.Timers.Timer();
				timer2.Interval = 4000;
				Console.WriteLine("SUB");
				timer2.Elapsed += sendobstacle2;
				timer2.AutoReset = true;
				timer2.Enabled = true;

				timer3 = new System.Timers.Timer();
				timer3.Interval = 1150;
				Console.WriteLine("SUB");
				timer3.Elapsed += sendobstacle3;
				timer3.AutoReset = true;
				timer3.Enabled = true;

				timer4 = new System.Timers.Timer();
				timer4.Interval = 1250;
				Console.WriteLine("SUB");
				timer4.Elapsed += sendobstacle4;
				timer4.AutoReset = true;
				timer4.Enabled = true;

				#endregion


				startScreen.RunAction(SKAction.CreatePause());
				Scroll(sidewalk1);
				Scroll(sidewalk2);
                Scroll(sidewalk3);

                playerObject.XScale = 2.0f;
                playerObject.YScale = 2.0f;
                playerObject.Position = new CGPoint(Frame.Width / 2, Frame.Height / 4);
                Scroll(road1);
                Scroll(road2);
                Scroll(road3);
                Scroll(road4);
                Scroll(road5);
                Scroll(road6);

                Scroll(building1);
                Scroll(building2);
                Scroll(building3);
                Scroll(building4);
                Scroll(building5);
                Scroll(building6);
                Scroll(building7);
                Scroll(building8);
                Scroll(building9);
                Scroll(building10);
                Scroll(building11);
                Scroll(building12);
                Scroll(building13);
                Scroll(building14);


                animatePlayer();

				playerObject.RunAction(SKAction.RepeatActionForever(SKAction.MoveBy(0.001f, 0, 2.0f)));
				DIR = true;


                
                 
            }


        }

		public override void Update(double currentTime)
        {
			// Called before each frame is rendered
			// Checks location of each sidewalk sprite
			// if sprite is at acutal bottom(frame.top) resets it to acutual top (frame.bottom

			if (inprogress)
			{
				
				score = score + 1;
				scorelabel.Text = String.Format("Score: {0}", score);
			
			}

			if (enemy.Frame.Bottom < 0)
            {
				enemy.RemoveAllActions();
                enemy.Position = new CGPoint(Frame.Width / 2, Frame.Bottom + obhalf + 10);
            }

			if (obstacle4.Frame.Bottom < 0)
            {
				obstacle4.RemoveAllActions();
                obstacle4.Position = new CGPoint(Frame.Width / 2, Frame.Bottom + obhalf + 5);
				Console.WriteLine("4madeittoo the bottom");
            }

			if (obstacle.Frame.Bottom < 0)
            {
				obstacle.RemoveAllActions();
                obstacle.Position = new CGPoint(Frame.Width / 2, Frame.Bottom + obhalf + 5);
            }

			if (obstacle1.Frame.Bottom < 0)
            {
				obstacle1.RemoveAllActions();
                obstacle1.Position = new CGPoint(obstacle1.Frame.X + obstacle1.Frame.Width/2, Frame.Bottom + obhalf + 1);
            }

			if (obstacle2.Frame.Bottom < 0)
            {
				obstacle2.RemoveAllActions();
                obstacle2.Position = new CGPoint(obstacle2.Frame.X + obstacle2.Frame.Width/2, Frame.Bottom + obhalf + 1);
            }


            if (sidewalk1.Frame.Bottom < 0)
            {
                sidewalk1.Position = new CGPoint(Frame.Width / 2, sidewalk2.Frame.Bottom + sidehalf);
            }
            if (sidewalk2.Frame.Bottom < 0)
            {
                sidewalk2.Position = new CGPoint(Frame.Width / 2, sidewalk3.Frame.Bottom + sidehalf);
            }
            if (sidewalk3.Frame.Bottom < 0)
            {
                sidewalk3.Position = new CGPoint(Frame.Width / 2, sidewalk1.Frame.Bottom + sidehalf);
            }
            if (building1.Frame.Bottom < 0)
            {
                building1.Position = new CGPoint(buildx, building14.Frame.Bottom + buildhalf);
            }
            if (building2.Frame.Bottom < 0)
            {
                building2.Position = new CGPoint(buildx, building1.Frame.Bottom + buildhalf);
            }
            if (building3.Frame.Bottom < 0)
            {
                building3.Position = new CGPoint(buildx, building2.Frame.Bottom + buildhalf);
            }
            if (building4.Frame.Bottom < 0)
            {
                building4.Position = new CGPoint(buildx, building3.Frame.Bottom + buildhalf);
            }
            if (building5.Frame.Bottom < 0)
            {
                building5.Position = new CGPoint(buildx, building4.Frame.Bottom + buildhalf);
            }
            if (building6.Frame.Bottom < 0)
            {
                building6.Position = new CGPoint(buildx, building5.Frame.Bottom + buildhalf);
            }
            if (building7.Frame.Bottom < 0)
            {
                building7.Position = new CGPoint(buildx, building6.Frame.Bottom + buildhalf);
            }
            if (building8.Frame.Bottom < 0)
            {
                building8.Position = new CGPoint(buildx, building7.Frame.Bottom + buildhalf);
            }
            if (building9.Frame.Bottom < 0)
            {
                building9.Position = new CGPoint(buildx, building8.Frame.Bottom + buildhalf);
            }
            if (building10.Frame.Bottom < 0)
            {
                building10.Position = new CGPoint(buildx, building9.Frame.Bottom + buildhalf);
            }
            if (building11.Frame.Bottom < 0)
            {
                building11.Position = new CGPoint(buildx, building10.Frame.Bottom + buildhalf);
            }
            if (building12.Frame.Bottom < 0)
            {
                building12.Position = new CGPoint(buildx, building11.Frame.Bottom + buildhalf);
            }
            if (building13.Frame.Bottom < 0)
            {
                building13.Position = new CGPoint(buildx, building12.Frame.Bottom + buildhalf);
            }
            if (building14.Frame.Bottom < 0)
            {
                building14.Position = new CGPoint(buildx, building13.Frame.Bottom + buildhalf);
            }

            // if sprite is at acutal bottom(frame.top) resets it to acutual top (frame.bottom
            if (road1.Frame.Bottom < 0)
            {
                road1.Position = new CGPoint(roadx, road6.Frame.Bottom + roadhalf);
            }
            if (road2.Frame.Bottom < 0)
            {
                road2.Position = new CGPoint(roadx, road1.Frame.Bottom + roadhalf);
            }
            if (road3.Frame.Bottom < 0)
            {
                road3.Position = new CGPoint(roadx, road2.Frame.Bottom + roadhalf);
            }
            if (road4.Frame.Bottom < 0)
            {
                road4.Position = new CGPoint(roadx, road3.Frame.Bottom + roadhalf);
            }
            if (road5.Frame.Bottom < 0)
            {
                road5.Position = new CGPoint(roadx, road4.Frame.Bottom + roadhalf);
            }
            if (road6.Frame.Bottom < 0)
            {
                road6.Position = new CGPoint(roadx, road5.Frame.Bottom + roadhalf);
            }
        }
    }
}
