using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using Color = System.Drawing.Color;

namespace sejuani
{
    class Program
    {
        public static string ChampName = "Sejuani"; // Declara o champion
        public static Orbwalking.Orbwalker Orbwalker; //Declara o orbwalker
        private static Obj_AI_Hero Player { get { return ObjectManager.Player; } } // Declara o ObjectManager.Player é necessario para trabalharmos com um champion

        public static Menu Haisenberg; //declara o menu

        static void Main(string[] args)
        {

            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;

        }

        static void Game_OnGameLoad(EventArgs args)
        {

            if (Player.ChampionName != "Sejuani") //Verifica se é o champion correto, se não, não executa o script
                return;

            Haisenberg = new Menu("Haisenberg" + ChampName, ChampName, true);


            //Criar o Maenu
            Haisenberg.AddSubMenu(new Menu("Orbwalker", "Orbwalker"));
            Orbwalker = new Orbwalking.Orbwalker(Haisenberg.SubMenu("Orbwalker"));

            //Target selector e menu
            var ts = new Menu("TargetSelector", "Target Selector");
            TargetSelector.AddToMenu(ts);
            Haisenberg.AddSubMenu(ts);
            //Nosso Menu
            Haisenberg.AddItem(new MenuItem("NossoMenu1", "Opção1").SetValue(true));
            Haisenberg.AddItem(new MenuItem("NossoMenu2", "Opção2").SetValue(new KeyBind("Y".ToCharArray()[0], KeyBindType.Toggle, true)));

            //Faz o menu ficar visivel
            Haisenberg.AddToMainMenu();

            Game.PrintChat("Haisenberg" + ChampName + "Injetado");
            Game.PrintChat("<font color =\"#87CEEB\">Heisenberg Sejuani</font>");



        }


    }
}
