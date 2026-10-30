using System;

namespace Pkmn
{
    /// <summary>
    /// Définition d'un personnage
    /// </summary>
    public class Character
    {
        /// <summary>
        /// Stat de base, HP
        /// </summary>
        int _baseHealth;
        /// <summary>
        /// Stat de base, ATK
        /// </summary>
        int _baseAttack;
        /// <summary>
        /// Stat de base, DEF
        /// </summary>
        int _baseDefense;
        /// <summary>
        /// Stat de base, SPE
        /// </summary>
        int _baseSpeed;
        /// <summary>
        /// Type de base
        /// </summary>
        TYPE _baseType;

        public Character(int baseHealth, int baseAttack, int baseDefense, int baseSpeed, TYPE baseType)
        {
            _baseHealth = baseHealth;
            _baseAttack = baseAttack;
            _baseDefense = baseDefense;
            _baseSpeed = baseSpeed;
            _baseType = baseType;

            CurrentStatus = new NoneStatus();

            CurrentHealth = baseHealth;
        }
        /// <summary>
        /// HP actuel du personnage
        /// </summary>
        public int CurrentHealth { get; private set; }
        public TYPE BaseType { get => _baseType;}
        /// <summary>
        /// HPMax, prendre en compte base et equipement potentiel
        /// </summary>
        public int MaxHealth
        {
            get
            {
                if (CurrentEquipment != null)
                    return _baseHealth + CurrentEquipment.BonusHealth;
                else 
                    return _baseHealth;
            }
        }
        /// <summary>
        /// ATK, prendre en compte base et equipement potentiel
        /// </summary>
        public int Attack
        {
            get
            {
                if (CurrentEquipment != null)
                    return _baseAttack + CurrentEquipment.BonusAttack;
                else
                    return _baseAttack;
            }
        }
        /// <summary>
        /// DEF, prendre en compte base et equipement potentiel
        /// </summary>
        public int Defense
        {
            get
            {
                if (CurrentEquipment != null)
                    return _baseDefense + CurrentEquipment.BonusDefense;
                else
                    return _baseDefense;
            }
        }
        /// <summary>
        /// SPE, prendre en compte base et equipement potentiel
        /// </summary>
        public int Speed
        {
            get
            {
                if (CurrentEquipment != null)
                    return _baseSpeed + CurrentEquipment.BonusSpeed;
                else 
                    return _baseSpeed;
            }
        }
        /// <summary>
        /// Equipement unique du personnage
        /// </summary>
        public Equipment CurrentEquipment { get; private set; }
        /// <summary>
        /// null si pas de status
        /// </summary>
        public StatusEffect CurrentStatus { get; private set; }

        public bool IsAlive => CurrentHealth > 0;


        /// <summary>
        /// Application d'un skill contre le personnage
        /// On pourrait potentiellement avoir besoin de connaitre le personnage attaquant,
        /// Vous pouvez adapter au besoin
        /// </summary>
        /// <param name="s">skill attaquant</param>
        /// <exception cref="NotImplementedException"></exception>
        public void ReceiveAttack(Skill s)
        {
            if (s == null) throw new ArgumentNullException();

            CurrentHealth -= s.Power - Defense;

            if (s.Status == StatusPotential.BURN)
            {
                CurrentStatus = new BurnStatus();
            }
            if (s.Status == StatusPotential.SLEEP)
            {
                CurrentStatus = new SleepStatus();
            }
            if (s.Status == StatusPotential.CRAZY)
            {
                CurrentStatus = new CrazyStatus();
            }

            ClampHealth();
        }

        public Skill UseAttack(Skill s)
        {
            if (s == null) throw new ArgumentNullException();

            Skill usedAttack;

            if (CurrentStatus.CanAttack) usedAttack = new UsedAttack(s, Attack);
            usedAttack = new NullAttack();

            TakeDamage((int)(usedAttack.Power * CurrentStatus.DamageOnAttack));

            return usedAttack;
        }
        /// <summary>
        /// Equipe un objet au personnage
        /// </summary>
        /// <param name="newEquipment">equipement a appliquer</param>
        /// <exception cref="ArgumentNullException">Si equipement est null</exception>
        public void Equip(Equipment newEquipment)
        {
            if (newEquipment == null) throw new ArgumentNullException();

            CurrentEquipment = newEquipment;
            ClampHealth();
        }
        /// <summary>
        /// Desequipe l'objet en cours au personnage
        /// </summary>
        public void Unequip()
        {
            CurrentEquipment = null;

            ClampHealth();
        }

        public void Heal(int amount)
        {
            CurrentHealth += amount;

            ClampHealth();
        }

        private void ClampHealth()
        {
            if (CurrentHealth < 0) CurrentHealth = 0;
            if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
        }

        public void TakeDamage(int amount)
        {
            CurrentHealth -= amount;
            ClampHealth();
        }
    }
}
