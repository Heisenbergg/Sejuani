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
            Q.SetSkillshot(0.3f, 50, 1000, true, SkillshotType.SkillshotLine);

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
            Menu spellMenu = Menu.AddSubMenu(new Menu("COMBO", "COMBO"));
            spellMenu.AddItem(new MenuItem("useQ", "Use Q").SetValue(true));
            spellMenu.AddItem(new MenuItem("useW", "Use W").SetValue(true));
            spellMenu.AddItem(new MenuItem("useE", "Use E").SetValue(true));
            spellMenu.AddItem(new MenuItem("useR", "Use R").SetValue(true));
            
            
            
            
            
            
            
            
            
            //Faz o menu ficar visivel
            Menu.AddToMainMenu();


            Game.OnGameUpdate += Game_OnGameUpdate;
            Game.PrintChat("Heisenberg " + ChampName + " Injetado");
            Game.PrintChat("<font color =\"#87CEEB\">Heisenberg Sejuani</font>");



        }

          private static void Drawing_OnDraw(EventArgs args)
          {
            //Se estiver morto retorna
            if (Player.IsDead)
                return;

            // checa se o Q está pronto
            if (Q.IsReady())
            {
            //Desenha uma circulo color Aqua ao redor do jogador
                Utility.DrawCircle(Player.Position, Q.Range, Color.Aqua);
            }
            else
            {
            //Desenha um circulo vermelho ao redor do jogador
                Utility.DrawCircle(Player.Position, Q.Range, Color.DarkRed);
            }
          }
        
        
        
        
        
        
        
        private static void Game_OnGameUpdate(EventArgs args)
        {
            if (Player.IsDead)
                return;

            // checks the current Orbwalker mode Combo/Mixed/LaneClear/LastHit
            if (Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo)
            {
                //Combo para matar o inimigo
                QSpell();
            }
         }
        private static void QSpell()
        {
            //checa se o jogador quer usar o Q
            if (!Menu.Item("useQ").GetValue<bool>())
                return;

            Obj_AI_Hero target = TargetSelector.GetTarget(650, TargetSelector.DamageType.Magical);
            //checa se o E está pronto
            if (Q.IsReady())
            {
                //checa se achou um target valido no range
                if (target.IsValidTarget(Q.Range))
                {
                //Ataque Ele
                Q.CastOnUnit(target);
                }
            }
        }

    }
}
    

