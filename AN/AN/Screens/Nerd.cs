﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AN
{
    class Nerd : Sprite
    {

        #region properties 

        const string NERD_ASSETNAME = "tux";
        const int START_POSITION_X = 125;
        const int START_POSITION_Y = 645;
        const int NERD_SPEED = 160;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;

        Boolean painettuna = false;
        Boolean ammuttu = false;
        Boolean maassa = false;
        Vector2 mouseStart;
        Vector2 mouseEnd;
        Vector2 nerdPosition;
        Vector2 liike;
        float vauhti = 0;
        double aika = -5;
		int testi;


        enum State
        {
            Walking
        }
        State mCurrentState = State.Walking;

        Vector2 mDirection = Vector2.Zero;
        Vector2 mSpeed = Vector2.Zero;

        KeyboardState mPreviousKeyboardState;
        
        #endregion

        #region load

        public void LoadContent(ContentManager theContentManager)
        {
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(theContentManager, "tux");
            nerdPosition.X = START_POSITION_X;
            nerdPosition.Y = START_POSITION_Y;

        }

        #endregion

        #region UpdateMethods

        public void Update(GameTime theGameTime)
        {
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();
            MouseState aCurrentMouseState = Mouse.GetState();

            UpdateMovement(aCurrentKeyboardState);
            UpdateMouseMovement(aCurrentMouseState);
            Position = nerdPosition;
            mPreviousKeyboardState = aCurrentKeyboardState;

            //base.Update(theGameTime, mSpeed, mDirection);
        }

        private void UpdateMovement(KeyboardState aCurrentKeyboardState)
        {
            if (mCurrentState == State.Walking)
            {
                mSpeed = Vector2.Zero;
                mDirection = Vector2.Zero;

                if (aCurrentKeyboardState.IsKeyDown(Keys.Left) == true)
                {
                    mSpeed.X = NERD_SPEED;
                    mDirection.X = MOVE_LEFT;
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.Right) == true)
                {
                    mSpeed.X = NERD_SPEED;
                    mDirection.X = MOVE_RIGHT;
                }

                if (aCurrentKeyboardState.IsKeyDown(Keys.Up) == true)
                {
                    mSpeed.Y = NERD_SPEED;
                    mDirection.Y = MOVE_UP;
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.Down) == true)
                {
                    mSpeed.Y = NERD_SPEED;
                    mDirection.Y = MOVE_DOWN;
                }
            }
        }

        //Checks mouse movements for shooting Nerd
        private void UpdateMouseMovement(MouseState aCurrentMouseState)
        {

            //jos hiiren vasen namikka alhaalla, niin aloitetaan virittäminen
            if (aCurrentMouseState.LeftButton == ButtonState.Pressed && painettuna == false && ammuttu ==false)
            {
                //sets mouse start position 
                mouseStart.X = aCurrentMouseState.X;
                mouseStart.Y = aCurrentMouseState.Y;

                //sets nerds start position
                nerdPosition.X = aCurrentMouseState.X;
                nerdPosition.Y = aCurrentMouseState.Y;

                painettuna = true;
            }


            // kun hiirin vasen namikka päästetään
            if (aCurrentMouseState.LeftButton == ButtonState.Released && painettuna == true && ammuttu == false)
            {
                mouseEnd.X = aCurrentMouseState.X;
                mouseEnd.Y = aCurrentMouseState.Y;

                painettuna = false;
                ammuttu = true;
                laskeliike();
            }

            //vielä yksi metodi lisää jossa painettuna on true, jotta saadaan nörtti liikkumaan hiiren mukana kun nappi painettuna
            if (aCurrentMouseState.LeftButton == ButtonState.Pressed && painettuna == true && ammuttu == false)
            {
                nerdPosition.X = aCurrentMouseState.X;
                nerdPosition.Y = aCurrentMouseState.Y;
            }

            if ( ammuttu == true && maassa ==false)
            {
                laskeAmmuksenLentorata();
                    
            }
         
            
               
            


        }


        private void laskeliike()
        {
            liike.X = Math.Abs(mouseStart.X - mouseEnd.X)/5;
            liike.Y = Math.Abs(mouseStart.Y - mouseEnd.Y)/5;
            
         
        }

        private void laskeAmmuksenLentorata()
        {

            
            double x = 0;
            double y = 0;
            
            double painovoimakiihtyvyys = 0.98;
            liike.X = 2; //kertoo kuinka jyrkästi x-akselin suuntaan ammus lähtee.. mitä isompi niin sitä jyrkempi
            liike.Y = 30; //kertoo kuinka jyrkkään y akselin suuntaan ammus lähtee.. mitä pienempi arvo niin sitä jyrkempi
          

            // Ammuksen lentorata on paraabeli, jonka y-arvo lasketaan kaavalla:
            //y = t * ut
            x = ((liike.X * aika) * aika)+500;

            // ja x-arvo lasketaan kaavalla:
            y =  (painovoimakiihtyvyys * (aika * aika)) + (liike.Y * aika )-400 ;

           nerdPosition.Y = (int)x + mouseEnd.X;
           nerdPosition.X = (int)(y + mouseEnd.Y);


           //kertoo ammuksen nopeuden ajan suhteen. tämän voisi fixata toimimaan gametimen perusteella, mutta on nyt vakio testausta varten
           aika= (aika+0.08);

            //tähän pitäisi hahmotella jokin systeemi huomaamaan jos ammus on jo maassa.
           if (y == 460) maassa = true;
       
        }


        #endregion

    }
}