
using System;

namespace Pkmn
{
    public class Fight
    {
        public Fight(Character character1, Character character2)
        {
            if (character1 == null || character2 == null) throw new ArgumentNullException();

            Character1 = character1;
            Character2 = character2;
        }

        public Character Character1 { get; }
        public Character Character2 { get; }
        /// <summary>
        /// Est-ce la condition de victoire/défaite a été rencontré ?
        /// </summary>
        public bool IsFightFinished => !(Character1.IsAlive && Character2.IsAlive);

        /// <summary>
        /// Jouer l'enchainement des attaques. Attention à bien gérer l'ordre des attaques par apport à la speed des personnages
        /// </summary>
        /// <param name="skillFromCharacter1">L'attaque selectionné par le joueur 1</param>
        /// <param name="skillFromCharacter2">L'attaque selectionné par le joueur 2</param>
        /// <exception cref="ArgumentNullException">si une des deux attaques est null</exception>
        public void ExecuteTurn(Skill skillFromCharacter1, Skill skillFromCharacter2)
        {
            Character firstToPlay = Character1;
            Character secondToPlay = Character2;
            if (Character2.Speed > Character1.Speed)
            {
                firstToPlay = Character2;
                secondToPlay = Character1;
            }

            if (Character1.Speed > Character2.Speed)
            {
                Character2.ReceiveAttack(skillFromCharacter1);
                if (Character2.IsAlive)
                    Character1.ReceiveAttack(skillFromCharacter2);
            }
            else
            {
                Character1.ReceiveAttack(skillFromCharacter2);
                if (Character1.IsAlive)
                    Character2.ReceiveAttack(skillFromCharacter1);
            }
        }

    }
}
