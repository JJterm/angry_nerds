#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AN;
#endregion

namespace GameStateManagement
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class GameplayScreen : GameScreen
    {


        #region Fields


        ContentManager content;
        SpriteFont gameFont;

        //PISTEIDENLASKUA VARTEN
        int points = 0;
        int ammukset = 0;
        


        //
        Random random = new Random();

        float pauseAlpha;

        KeyboardState current;
        Nerd nerd;
        Sika target;
        Sika target2;
        Sika target3;
        Tile wall;
        Tile wall2;
        Tile wall3;
        Tile wall4;
        Tile wall5;
        Tile wall6;
        Tile wall7;
        Sprite mBackgroundOne;

       // Boolean nextlevel = false;

       // Camera camera;       
        //GraphicsDeviceManager graphics;
       // SpriteBatch spriteBatch;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
            {
                content = new ContentManager(ScreenManager.Game.Services, "Content/GraphicsContent");
            }
 


            gameFont = content.Load<SpriteFont>("menufont");
           // spriteBatch = new SpriteBatch(GraphicsDevice);

 

            //m��ritell��n background
            mBackgroundOne = new Sprite();
            mBackgroundOne.LoadContent(this.content, "Background01");
            mBackgroundOne.Position = new Vector2(0, 0);
            mBackgroundOne.Scale = 1;

            //m��rritell��n tuxi
            nerd = new Nerd();
            nerd.LoadContent(this.content, 150, 600);
            ammukset = nerd.ammukset;
            
            //tarkoitus tehd� t�st� "possu"
            target = new Sika();
            target.value = 3;
            target.Scale = 0.5F;
            target.LoadContent(this.content, 800, 500, "birdy");

            target2 = new Sika();
            target2.Scale = 1;
            target2.value = 5;
            target2.LoadContent(this.content, 700, 600, "tux");

            target3 = new Sika();
            target3.Scale = 0.5F;
            target3.value = 10;
            target3.LoadContent(this.content, 700, 300, "birdy");

            wall = new Tile();

            wall.LoadContent(this.content, 400, 500, "tile2");
            

            wall.LoadContent(this.content, 400, 500, "tile3");

            wall2 = new Tile();
            wall2.LoadContent(this.content, 400, 530, "tile3");

            wall3 = new Tile();
            wall3.LoadContent(this.content, 400, 560, "tile3");

            wall4 = new Tile();
            wall4.LoadContent(this.content, 400, 590, "tile3");

            wall5 = new Tile();
            wall5.LoadContent(this.content, 400, 620, "tile3");

            wall6 = new Tile();
            wall6.LoadContent(this.content, 400, 650, "tile3");

            wall7 = new Tile();
            wall7.LoadContent(this.content, 400, 680, "tile3");
               
    



           //t�m�n voi poistaa lopullisesta, mutta antaa paremman fiiliksen kun load screeni n�kyy hetken :)
            Thread.Sleep(1000);

 
            ScreenManager.Game.ResetElapsedTime();


  
        }


 
        /// Unload graphics content used by the game.
    
        public override void UnloadContent()
        {
            content.Unload();
        }

        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {



            base.Update(gameTime, otherScreenHasFocus, false);


            


            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
            {
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);


                current = Keyboard.GetState();
                Camera.Update(current);



                /*
                 * Updates
                 */ 
                nerd.Update(gameTime);
                target.Update(gameTime);
                target2.Update(gameTime);
                target3.Update(gameTime);
                wall.Update(gameTime);

                if (wall.tileRectangle.Intersects(nerd.nerdRectangle))
                {
                    nerd.reset();
                }

                wall2.Update(gameTime);
                wall3.Update(gameTime);
                wall4.Update(gameTime);
                wall5.Update(gameTime);
                wall6.Update(gameTime);
                wall7.Update(gameTime);
                /*
                 * Collision detection
                 */

                if (target.sikaRectangle.Intersects(nerd.nerdRectangle) && !target.sikaHit)
                {
                    target.sikaHit = true;
                    target.Scale = 0.1F;
                    points = points + target.value;
                }

                if (target2.sikaRectangle.Intersects(nerd.nerdRectangle) && !target2.sikaHit)
                {
                    target2.sikaHit = true;
                    target2.Scale = 0.1F;
                    points = points + target2.value;
                }
                //Console.Write(target3.sikaRectangle.Intersects(nerd.nerdRectangle));
                if (target3.sikaRectangle.Intersects(nerd.nerdRectangle) && !target3.sikaHit)
                {
                    target3.sikaHit = true;
                    target3.Scale = 0.1F;
                    points = points + target3.value;
                }
                ammukset = nerd.ammukset;


                Vector2 aDirection = new Vector2(-1, 0);
                Vector2 aSpeed = new Vector2(160, 0);
            }
        }



            //base.Update(gameTime);
   
        /*
        void NextLevel(PlayerIndexEventArgs e)
        {
        LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                                  new GameplayScreen());
        }
        */




        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;
            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!

            if (input.IsPauseGame(ControllingPlayer))
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
        }



        

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
         

            
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
           
            spriteBatch.Begin();




            //piirret��n n�rtti ja tausta.. t�h�n voisi v��nt�� sellaisen systeemin ett� pistet��n kaikki piirrett�v�t systeemit listaan ja iteroidaan niille piirtofunktio
            mBackgroundOne.Draw(spriteBatch);
            nerd.Draw(spriteBatch);
            target.Draw(spriteBatch);
            target2.Draw(spriteBatch);
            target3.Draw(spriteBatch);
            wall.Draw(spriteBatch);
            wall2.Draw(spriteBatch);
            wall3.Draw(spriteBatch);
            wall4.Draw(spriteBatch);
            wall5.Draw(spriteBatch);
            wall6.Draw(spriteBatch);
            wall7.Draw(spriteBatch);

            //For drawing the points
            spriteBatch.DrawString( 
            gameFont, 
            "Points: " + points.ToString(),
            new Vector2( 
            600,
            10.0f),
            Color.Black);

            spriteBatch.DrawString(
            gameFont,
            "Projectiles: " + ammukset.ToString(),
            new Vector2(
            300,
            10.0f),
            Color.Black); 

            spriteBatch.End();

    




            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }


        #endregion

        public ContentManager Content { get; set; }
    }
}
