using Pkmn;
using NUnit.Framework;
using System;


namespace Pkmn.Tests
{
    public class FightMoreTests
    {
        // Tu as probablement remarqué qu'il y a encore beaucoup de code qui n'a pas été testé ...
        // À présent c'est à toi de créer des features et les TU sur le reste du projet

        // Ce que tu peux ajouter:
        // - Ajouter davantage de sécurité sur les tests apportés
        // + un heal ne régénère pas plus que les HP Max
        // + si on abaisse les HPMax les HP courant doivent suivre si c'est au dessus de la nouvelle valeur
        // - ajouter un equipement qui rend les attaques prioritaires puis l'enlever et voir que l'attaque n'est plus prioritaire etc)
        // - Le support des status (sleep et burn) qui font des effets à la fin du tour et/ou empeche le pkmn d'agir
        // - Gérer la notion de force/faiblesse avec les différentes attaques à disposition (skills.cs)
        // - Cumuler les force/faiblesses en ajoutant un type pour l'équipement qui rendrait plus sensible/résistant à un type
        // - L'utilisation d'objets : Potion, SuperPotion, Vitess+, Attack+ etc.
        // - Gérer les PP (limite du nombre d'utilisation) d'une attaque,
        // si on selectionne une attack qui a 0 PP on inflige 0

        // Choisis ce que tu veux ajouter comme feature et fait en au max.
        // Les nouveaux TU doivent être dans ce fichier.
        // Modifiant des features il est possible que certaines valeurs
        // des TU précédentes ne matchent plus, tu as le droit de réadapter les valeurs
        // de ces anciens TU pour ta nouvelle situation.


        [Test]
        public void MaxHPChangeTest()
        {
            var c = new Character(100, 30, 30, 20, TYPE.NORMAL);
            var e = new Equipment(50, 20, 10, 10);

            c.Equip(e);
            Assert.That(c.CurrentHealth, Is.EqualTo(100));
            c.Heal(100);
            Assert.That(c.CurrentHealth, Is.EqualTo(c.MaxHealth));
            c.Unequip();
            Assert.That(c.CurrentHealth, Is.EqualTo(100));
        }

        [Test]
        public void CharacterAttackMatters()
        {
            var pikachu = new Character(100, 30, 20, 40, TYPE.NORMAL);
            var bulbizarre = new Character(120, 15, 30, 20, TYPE.NORMAL);
            var punch = new Punch();

            bulbizarre.ReceiveAttack(pikachu.UseAttack(punch));
            Assert.That(bulbizarre.CurrentHealth, Is.EqualTo(50)); //120 - ((30 + 70) - 30)
        }
    }
}
