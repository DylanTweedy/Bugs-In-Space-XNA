using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DevStomp
{
    class Inventory
    {
        public Weapon RHWeapon;
        public Weapon LHWeapon;
        public Head head;
        public Chest chest;
        public Gloves gloves;
        public Pants pants;
        public Boots boots;

        public float Timer;
        public List<List<ValueType>> ActionsRH;
        public List<List<ValueType>> ActionsLH;

        public void Initialize()
        {
            RHWeapon = new Weapon();
            LHWeapon = new Weapon();
            head = new Head();
            chest = new Chest();
            gloves = new Gloves();
            pants = new Pants();
            boots = new Boots();

            ActionsRH = new List<List<ValueType>>();
            ActionsLH = new List<List<ValueType>>();
        }

        public void Update()
        {
            RHWeapon.Update();
            LHWeapon.Update();
            head.Update();
            chest.Update();
            gloves.Update();
            pants.Update();
            boots.Update();

            ActionsRH.Clear();
            ActionsRH.AddRange(RHWeapon.Actions);
            ActionsRH.AddRange(head.Actions);
            ActionsRH.AddRange(chest.Actions);
            ActionsRH.AddRange(gloves.Actions);
            ActionsRH.AddRange(pants.Actions);
            ActionsRH.AddRange(boots.Actions);

            ActionsLH.Clear();
            ActionsLH.AddRange(LHWeapon.Actions);
            ActionsLH.AddRange(head.Actions);
            ActionsLH.AddRange(chest.Actions);
            ActionsLH.AddRange(gloves.Actions);
            ActionsLH.AddRange(pants.Actions);
            ActionsLH.AddRange(boots.Actions);
        }

        public void DrawBack(SpriteBatch spriteBatch)
        {
            RHWeapon.Draw(spriteBatch);
        }

        public void DrawFront(SpriteBatch spriteBatch)
        {
            //head.Draw(spriteBatch);
            chest.Draw(spriteBatch);
            gloves.Draw(spriteBatch);
            pants.Draw(spriteBatch);
            boots.Draw(spriteBatch);
            LHWeapon.Draw(spriteBatch);
        }
    }
}
