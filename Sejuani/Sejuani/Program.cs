﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using Color = System.Drawing.Color;

namespace sejuani //HEISENBERG SEJUANI V1.0
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
            Game.PrintChat("Heisenberg " + ChampName + " Injetado outraskills");
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
            
            //Checa se o W está Pronto
            if (W.IsReady())
            {
                //Desenha uma circulo color Aqua ao redor do jogador
                Utility.DrawCircle(Player.Position, W.Range, Color.Aqua);
            }
            else
            {
                //Desenha um circulo vermelho ao redor do jogador
                Utility.DrawCircle(Player.Position, W.Range, Color.DarkRed);
            }

            //Checa se o E está Pronto
            if (E.IsReady())
            {
                //Desenha uma circulo color Aqua ao redor do jogador
                Utility.DrawCircle(Player.Position, E.Range, Color.Aqua);
            }
            else
            {
                //Desenha um circulo vermelho ao redor do jogador
                Utility.DrawCircle(Player.Position, E.Range, Color.DarkRed);
            }
            
            //Checa se o R está Pronto
            if (R.IsReady())
            {
                //Desenha uma circulo color Aqua ao redor do jogador
                Utility.DrawCircle(Player.Position, R.Range, Color.Aqua);
            }
            else
            {
                //Desenha um circulo vermelho ao redor do jogador
                Utility.DrawCircle(Player.Position, R.Range, Color.DarkRed);
            }

        }


        private static void Game_OnGameUpdate(EventArgs args)
        {
            if (Player.IsDead)
                return;

            // Checa o modo atual do Orbwalker Combo/Mixed/LaneClear/LastHit
            if (Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo)
            {
                //Combo para matar o inimigo
                QSpell();
                WSpell();
                ESpell();
                RSpell();
            }
        }
        private static void QSpell()
        {
            //checa se o jogador quer usar o Q
            if (!Menu.Item("useQ").GetValue<bool>())
                return;

            Obj_AI_Hero target = TargetSelector.GetTarget(650, TargetSelector.DamageType.Magical);
            //checa se o Q está pronto
            if (Q.IsReady())
            {
                //checa se achou um target valido no range
                if (target.IsValidTarget(Q.Range))
                {
                    //Ataque Ele
                    Q.Cast(target.Position);
                }
            }
        }
        private static void WSpell()
        {

            if (!Menu.Item("useW").GetValue<bool>())
                return;

            Obj_AI_Hero target = TargetSelector.GetTarget(350, TargetSelector.DamageType.Magical);
            //checa se o W está pronto
            if (W.IsReady())
            {
                //checa se achou um target valido no range
                if (target.IsValidTarget(W.Range))
                {
                    //Ataque Ele
                    W.Cast(target.Position);
                }
            }


        }
        private static void ESpell()
        {
            if (!Menu.Item("useE").GetValue<bool>())
                return;

            Obj_AI_Hero target = TargetSelector.GetTarget(1000, TargetSelector.DamageType.Magical);
            //checa se o E está pronto
            if (E.IsReady())
            {
                //checa se achou um target valido no range
                if (target.IsValidTarget(E.Range))
                {
                    //Ataque Ele
                    E.Cast(target.Position);
                }
            }
        }
        private static void RSpell()
        {
            if (!Menu.Item("useR").GetValue<bool>())
                return;

            Obj_AI_Hero target = TargetSelector.GetTarget(1175, TargetSelector.DamageType.Magical);
            //checa se o E está pronto
            if (R.IsReady())
            {
                //checa se achou um target valido no range
                if (target.IsValidTarget(R.Range))
                {
                    //Ataque Ele
                    R.Cast(target.Position);
                }
            }
        }
    }
}
