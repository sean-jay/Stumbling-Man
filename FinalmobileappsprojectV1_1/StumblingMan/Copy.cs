using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using CoreGraphics;
using Foundation;
using SpriteKit;
using UIKit;

namespace StumblingMan
{
    public class GameScene : SKScene
    {
		double roadx = 678;
		double buildx = 327;
		double roadhalf = 0;
		double buildhalf = 0;
		double sidehalf = 202.5 - 1;


        SKSpriteNode sidewalk1;
        SKSpriteNode sidewalk2;
		SKSpriteNode sidewalk3;

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


		SKSpriteNode playerObject;
		SKAction playerAnimate;
		SKAction walkAnimate;
		protected GameScene(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void DidMoveToView(SKView view)
		{

			road1 = new SKSpriteNode("road")
			{
				XScale = 0.7f,
				YScale = 0.7f,
				Position = new CGPoint(roadx, 89),
				ZPosition = 0.3f
			};
			roadhalf = (road1.Frame.Height / 2)-1;

			road1.Position = new CGPoint(roadx, roadhalf);

			road2 = new SKSpriteNode("road")
			{
				XScale = 0.7f,
				YScale = 0.7f,
				Position = new CGPoint(roadx, roadhalf + (road1.Frame.Bottom)),
				ZPosition = 0.3f
			};

			road3 = new SKSpriteNode("road")
			{
				XScale = 0.7f,
				YScale = 0.7f,
				Position = new CGPoint(roadx, roadhalf + (road2.Frame.Bottom)),
				ZPosition = 0.3f
			};

			road4 = new SKSpriteNode("road")
			{
				XScale = 0.7f,
				YScale = 0.7f,
				Position = new CGPoint(roadx, roadhalf + (road3.Frame.Bottom)),
				ZPosition = 0.3f
			};

			road5 = new SKSpriteNode("road")
			{
				XScale = 0.7f,
				YScale = 0.7f,
				Position = new CGPoint(roadx, roadhalf + (road4.Frame.Bottom)),
				ZPosition = 0.3f
			};

			road6 = new SKSpriteNode("road")
			{
				XScale = 0.7f,
				YScale = 0.7f,
				Position = new CGPoint(roadx, roadhalf + (road5.Frame.Bottom)),
				ZPosition = 0.3f
			};

			Console.WriteLine(road1.Frame);

			building1 = new SKSpriteNode("Building")
			{
				XScale = 1.0f,
				YScale = 1.0f,
				Position = new CGPoint(buildx, 0),
				ZPosition = 0.3f
			};

			buildhalf = (building1.Frame.Height / 2)-1;

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
				Position = new CGPoint(buildx , buildhalf + (building11.Frame.Bottom)),
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


			// Setup player character
			playerObject = new SKSpriteNode("Stumbler-1")
			{
				XScale = 1.6f,
				YScale = 1.6f,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 4),
				ZPosition = 0.1f
			};

			// sidewalk sprites, set up to overlap to remove gaps 4 at top 1 at bottom

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


			// definition of player walking animation
			void animatePlayer()

			{
				var textures = Enumerable.Range(1, 4).Select(
				(i) => SKTexture.FromImageNamed(String.Format("Stumbler-{0}", i))).ToArray();
				playerAnimate = SKAction.RepeatActionForever(SKAction.AnimateWithTextures(textures, 0.1));
				playerObject.RunAction(playerAnimate);

			}
			// definition of sidewalk scrolling action
			void Scroll(SKSpriteNode node)
            {

				walkAnimate = SKAction.RepeatActionForever(SKAction.MoveBy(0.0f, -0.1f, 0.0007f));
				node.RunAction(walkAnimate);

			}

			// calls to add sprites to scene
            AddChild(road1);
            AddChild(road2);
			AddChild(road3);
			AddChild(road4);
			AddChild(road5);
			AddChild(road6);

			AddChild(sidewalk1);
			AddChild(sidewalk2);
			AddChild(sidewalk3);

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

			AddChild(playerObject);

			// calls to animation for sprites
			Scroll(sidewalk1);
			Scroll(sidewalk2);
			Scroll(sidewalk3);

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

			//Console.WriteLine(Frame.Bottom);

		}

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			// Called when a touch begins

			// control player motion
			var moveRight = SKAction.RepeatActionForever(SKAction.MoveBy(75.0f, 0, 2.0f));
			var moveLeft = SKAction.RepeatActionForever(SKAction.MoveBy(-75.0f, 0, 2.0f));
			base.TouchesBegan(touches, evt);
			var touch = (UITouch)touches.AnyObject;
			playerMove(touch);

			// detects left and right screen taps, moves player accordingly
			void playerMove(UITouch location)
            {
				if (touch.LocationInNode(this).X > (Frame.Width / 2))
				{
					playerObject.RunAction(moveRight);
				}
				if (touch.LocationInNode(this).X < (Frame.Width / 2))
				{
					playerObject.RunAction(moveLeft);
				}
				Console.WriteLine(touch.LocationInNode(this));
			}



		}

		public override void Update(double currentTime)
		{
            // Called before each frame is rendered
            // Checks location of each sidewalk sprite
            // if sprite is at acutal bottom(frame.top) resets it to acutual top (frame.bottom
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
				building1.Position = new CGPoint(buildx, building14.Frame.Bottom + buildhalf );
            }
			if (building2.Frame.Bottom < 0)
            {
				building2.Position = new CGPoint(buildx, building1.Frame.Bottom + buildhalf );
            }
			if (building3.Frame.Bottom < 0)
            {
				building3.Position = new CGPoint(buildx, building2.Frame.Bottom + buildhalf );
            }
			if (building4.Frame.Bottom < 0)
            {
				building4.Position = new CGPoint(buildx, building3.Frame.Bottom + buildhalf );
            }
			if (building5.Frame.Bottom < 0)
            {
				building5.Position = new CGPoint(buildx, building4.Frame.Bottom + buildhalf );
            }
			if (building6.Frame.Bottom < 0)
            {
				building6.Position = new CGPoint(buildx, building5.Frame.Bottom + buildhalf );
            }
			if (building7.Frame.Bottom < 0)
            {
				building7.Position = new CGPoint(buildx, building6.Frame.Bottom + buildhalf );
            }
			if (building8.Frame.Bottom < 0)
            {
				building8.Position = new CGPoint(buildx, building7.Frame.Bottom + buildhalf );
            }
			if (building9.Frame.Bottom < 0)
            {
				building9.Position = new CGPoint(buildx, building8.Frame.Bottom + buildhalf );
            }
			if (building10.Frame.Bottom < 0)
            {
				building10.Position = new CGPoint(buildx, building9.Frame.Bottom + buildhalf );
            }
			if (building11.Frame.Bottom < 0)
            {
				building11.Position = new CGPoint(buildx, building10.Frame.Bottom + buildhalf );
            }
			if (building12.Frame.Bottom < 0)
            {
				building12.Position = new CGPoint(buildx, building11.Frame.Bottom + buildhalf );
            }
			if (building13.Frame.Bottom < 0)
            {
				building13.Position = new CGPoint(buildx, building12.Frame.Bottom + buildhalf );
            }
			if (building14.Frame.Bottom < 0)
            {
				building14.Position = new CGPoint(buildx, building13.Frame.Bottom + buildhalf );
            }
          
			 // if sprite is at acutal bottom(frame.top) resets it to acutual top (frame.bottom
            if (road1.Frame.Bottom < 0)
            {
				road1.Position = new CGPoint(roadx, road6.Frame.Bottom + roadhalf);
            }
            if (road2.Frame.Bottom< 0)
            {
                road2.Position = new CGPoint(roadx, road1.Frame.Bottom + roadhalf);
            }
            if (road3.Frame.Bottom< 0)
            {
                road3.Position = new CGPoint(roadx, road2.Frame.Bottom + roadhalf);
            }
			if (road4.Frame.Bottom < 0)
            {
				road4.Position = new CGPoint(roadx, road3.Frame.Bottom + roadhalf);
            }
            if (road5.Frame.Bottom< 0)
            {
                road5.Position = new CGPoint(roadx, road4.Frame.Bottom + roadhalf);
            }
            if (road6.Frame.Bottom< 0)
            {
                road6.Position = new CGPoint(roadx, road5.Frame.Bottom + roadhalf);
            }	                             

        }
    }
}
