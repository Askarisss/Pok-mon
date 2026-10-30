using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// Ici est rangé un set de compétences afin d'être prête à être utilisé en combat
/// Vous pouvez changer/adapter ces skills au besoin du moment que c'est pertinent dans le reste du code.
namespace Pkmn
{
    /// <summary>
    /// Coup de poing basique
    /// </summary>
    public class Punch : Skill
    {
        public Punch() : base(TYPE.NORMAL, 70, StatusPotential.NONE) { }
    }

    /// <summary>
    /// Coup de poing basique ++
    /// </summary>
    public class MegaPunch : Skill
    {
        public MegaPunch() : base(TYPE.NORMAL, 7000, StatusPotential.NONE) { }
    }

    /// <summary>
    /// Boule de feu qui ajoute le statut BURN
    /// </summary>
    public class FireBall : Skill
    {
        public FireBall() : base(TYPE.FIRE, 50, StatusPotential.BURN) { }
    }
    /// <summary>
    /// Attaque eau basique
    /// </summary>
    public class WaterBlouBlou : Skill
    {
        public WaterBlouBlou() : base(TYPE.WATER, 20, StatusPotential.NONE) { }
    }
    /// <summary>
    /// Attaque plante qui inflige le statut SLEEP
    /// </summary>
    public class MagicalGrass : Skill
    {
        public MagicalGrass() : base(TYPE.GRASS, 70, StatusPotential.SLEEP) { }
    }

    public class UsedAttack : Skill
    {
        public UsedAttack(Skill baseSkill, int characterAttack) :base(baseSkill.Type, baseSkill.Power + characterAttack, baseSkill.Status) { }
    }

    public class NullAttack : Skill

        {
            public NullAttack() : base(TYPE.NORMAL, 0, StatusPotential.NONE) { }
        }
}
