using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    class SubMenu
    {
    }

    class Menu
    {
        Menu subMenu;
        List<InfoBox> Menus = new List<InfoBox>();
        int SelectedMenuItem = -1;
        bool MenuActive = false;

        public Menu()
        {
            Menus.Add(new InfoBox());
        }

        public void UpdateMenuItems(InfoBox infoBox)
        {
            //Menus[0].Update(
        }

    }
}
