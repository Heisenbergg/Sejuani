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

        public static Menu Menu; //declara o menu
        
        private static Spell Q, W, E, R;  //Declara habilidades

        static void Main(string[] args)
        {

            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;

        }

        static void Game_OnGameLoad(EventArgs args)
        {

            if (Player.ChampionName != "Sejuani") //Verifica se é o champion correto, se não, não executa o script
                return;


            Q = new Spell(SpellSlot.Q, 650);
            W = new Spell(SpellSlot.W, 350);
            E = new Spell(SpellSlot.E, 1000);
            R = new Spell(SpellSlot.R, 1175);

            //Criar o Maenu
            Menu = new Menu("Heisenberg" + ChampName, ChampName, true);

            //Menu Orbwalker
            Menu orbwalkerMenu = Menu.AddSubMenu(new Menu("Orbwalker", "Orbwalker"));
            Orbwalker = new Orbwalking.Orbwalker(orbwalkerMenu);

            //Target selector e menu
            var ts = new Menu("TargetSelector", "Target Selector");
            TargetSelector.AddToMenu(ts);
            Menu.AddSubMenu(ts);


            //Habilidades Menu
            Menu spellMenu = Menu.AddSubMenu(new Menu("Habilidades", "Habilidades"));
            spellMenu.AddItem(new MenuItem("useQ", "Use Q").SetValue(true));
            spellMenu.AddItem(new MenuItem("useW", "Use W").SetValue(true));
            spellMenu.AddItem(new MenuItem("useE", "Use E").SetValue(true));
            spellMenu.AddItem(new MenuItem("useR", "Use R").SetValue(true));
            
            
            
            
            
            
            
            
            
            //Faz o menu ficar visivel
            Menu.AddToMainMenu();

            Game.PrintChat("Heisenberg" + ChampName + "Injetado");
            Game.PrintChat("<font color =\"#87CEEB\">Heisenberg Sejuani</font>");



        }


    }
}
