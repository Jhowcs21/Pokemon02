using System;
using System.Collections.Generic;

namespace PokemonGame
{
    enum ElementType
    {
        Normal,
        Fire,
        Water,
        Grass,
        Ground,
        Electric
    }


    class Pokemon
    {


        //Metodo que adiciona poção ao jogador
        public int PotionCount { get; set; }
        public void GetPotion()
        {
            if (PotionCount >= 5)
            {
                Console.WriteLine($"{Name} já possui o número máximo de poções.");
                return;
            }

            PotionCount++;
            HasPotion = true;
            Console.WriteLine($"{Name} obteve uma poção!");
        }
        //Fim do Metodo poção jogador

        //Metodo Para adicionar a opção Poção
        public bool HasPotion { get; set; }
        public void UsePotion()
        {
            if (!HasPotion)
            {
                Console.WriteLine($"{Name} não possui uma poção.");
                return;
            }
            Health = MaxHealth;
            Console.WriteLine($"{Name} usou uma poção e recuperou toda a vida!");
            HasPotion = false;
        }
        //Fim do metodo para adicionar poção
        //Pokemon
        public string Name { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public bool IsDefeated { get; set; }

        //Potion

        //Elementos e Fraquezas
        public Dictionary<int, string> Attacks { get; set; }
        public ElementType Element { get; set; }
        public ElementType Weakness { get; set; }

        public Pokemon(string name, int level, ElementType element, ElementType weakness)
        {
            Name = name;
            Level = level;
            MaxHealth = level * 10;
            Health = MaxHealth;
            IsDefeated = false;
            Attacks = new Dictionary<int, string>();
            Element = element;
            Weakness = weakness;
        }

        public void AddAttack(int attackNumber, string attackName)
        {
            Attacks.Add(attackNumber, attackName);
        }

        public void Attack(int attackNumber, Pokemon target)
        {
            if (Health <= 0)
            {
                Console.WriteLine($"{Name} está derrotado e não pode mais atacar!");
                return;
            }

            Console.WriteLine($"{Name} usou {Attacks[attackNumber]} em {target.Name}!");

            bool isCritical = IsCritical();

            int damage = CalculateDamage(target, isCritical);
            target.TakeDamage(damage);

            if (isCritical)
                Console.WriteLine("Ataque crítico!");
        }

        private bool IsCritical()
        {
            return new Random().Next(1, 101) <= 10;
        }

        private int CalculateDamage(Pokemon target, bool isCritical)
        {
            int baseDamage = Level * 5;

            if (isCritical)
                baseDamage *= 2;

            if (target.Weakness == Element)
                baseDamage *= 2;

            return baseDamage;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0)
                Health = 0;
            Console.WriteLine($"{Name} sofreu {damage} de dano!");
            Console.WriteLine($"{Name} tem {Health} de vida restante.");
            DrawHealthBar();
            if (Health == 0)
            {
                IsDefeated = true;
                Console.WriteLine($"{Name} foi derrotado!");
            }
        }

        private void DrawHealthBar()

        {
            Console.WriteLine($"Vida: {Health}/{MaxHealth}");
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Pokemon pikachu = new Pokemon("Pikachu", 10, ElementType.Electric, ElementType.Ground);
            pikachu.AddAttack(1, "Choque do Trovão");
            pikachu.AddAttack(2, "Investida Elétrica");
            pikachu.AddAttack(3, "Onda de Choque");
            pikachu.AddAttack(4, "Cauda de Ferro");

            Pokemon charmander = new Pokemon("Charmander", 8, ElementType.Fire, ElementType.Water);
            charmander.AddAttack(1, "Brasas");
            charmander.AddAttack(2, "Garra de Fogo");
            charmander.AddAttack(3, "Lança-Chamas");
            charmander.AddAttack(4, "Garras");

            Pokemon squirtle = new Pokemon("Squirtle", 9, ElementType.Water, ElementType.Electric);
            squirtle.AddAttack(1, "Jato de Água");
            squirtle.AddAttack(2, "Tackle");
            squirtle.AddAttack(3, "Chuva Ácida");
            squirtle.AddAttack(4, "Cabeçada");

            Pokemon bulbasaur = new Pokemon("Bulbasaur", 7, ElementType.Grass, ElementType.Fire);
            bulbasaur.AddAttack(1, "Chicote de Vinha");
            bulbasaur.AddAttack(2, "Investida");
            bulbasaur.AddAttack(3, "Folha Navalha");
            bulbasaur.AddAttack(4, "Raio Solar");

            Pokemon milotic = new Pokemon("Milotic", 10, ElementType.Water, ElementType.Electric);
            milotic.AddAttack(1, "Hidro Bomba");
            milotic.AddAttack(2, "Aqua Tail");
            milotic.AddAttack(3, "Surf");
            milotic.AddAttack(4, "Cauda de Ferro");

            //Metodo poção para o pokemon
            pikachu.GetPotion();
            charmander.GetPotion();
            squirtle.GetPotion();
            bulbasaur.GetPotion();
            milotic.GetPotion();
            //Fim do Metodo poção pokemon

            List<Pokemon> availablePokemons = new List<Pokemon> { pikachu, charmander, squirtle, bulbasaur, milotic };
            List<Pokemon> defeatedPokemons = new List<Pokemon>();

            Console.WriteLine("Selecione o seu Pokémon:");

            for (int i = 0; i < availablePokemons.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availablePokemons[i].Name}");
            }

            int playerChoice = int.Parse(Console.ReadLine());
            Pokemon playerPokemon = availablePokemons[playerChoice - 1];
            availablePokemons.RemoveAt(playerChoice - 1);

            Console.WriteLine($"Você selecionou {playerPokemon.Name}!");

            Console.WriteLine("Batalha Pokémon - Jogador vs. CPU");

            while (playerPokemon.Health > 0 && availablePokemons.Count > 0)
            {
                Console.WriteLine($"\nSeu Pokémon: {playerPokemon.Name}");
                Console.WriteLine("Pokémons disponíveis para batalha:");

                for (int i = 0; i < availablePokemons.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {availablePokemons[i].Name}");
                }

                Console.Write("Escolha o Pokémon inimigo para batalhar: ");
                int enemyChoice = int.Parse(Console.ReadLine());
                Pokemon enemyPokemon = availablePokemons[enemyChoice - 1];

                Console.WriteLine($"\nSeu Pokémon: {playerPokemon.Name}");
                Console.WriteLine($"Inimigo: {enemyPokemon.Name}\n");

                Console.WriteLine("Opções de ataque:");

                foreach (var attack in playerPokemon.Attacks)
                {
                    Console.WriteLine($"{attack.Key}. {attack.Value}");
                }

                Console.WriteLine("5. Ir para o inventário");
                Console.Write("Escolha uma opção: ");
                int playerOption = int.Parse(Console.ReadLine());

                if (playerOption >= 1 && playerOption <= 4)
                {
                    int playerAttack = playerOption;
                    int cpuAttack = new Random().Next(1, 5);

                    playerPokemon.Attack(playerAttack, enemyPokemon);
                    enemyPokemon.Attack(cpuAttack, playerPokemon);

                    if (enemyPokemon.Health == 0)
                    {
                        defeatedPokemons.Add(enemyPokemon);
                        availablePokemons.RemoveAt(enemyChoice - 1);

                        if (availablePokemons.Count > 0)
                        {
                            Console.WriteLine("Você derrotou o Pokémon inimigo!");

                            Console.WriteLine("Pokémons disponíveis para batalha:");

                            for (int i = 0; i < availablePokemons.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {availablePokemons[i].Name}");
                            }

                            Console.WriteLine("5. Ir para o inventário");
                            Console.Write("Escolha uma opção: ");
                            playerOption = int.Parse(Console.ReadLine());

                            if (playerOption >= 1 && playerOption <= availablePokemons.Count)
                            {
                                enemyChoice = playerOption;
                                enemyPokemon = availablePokemons[enemyChoice - 1];
                            }
                            else if (playerOption == 5)
                            {
                                // Abrir inventário
                                Console.WriteLine("Você abriu o inventário.");
                                // Implemente o código para o inventário aqui
                            }
                        }
                    }
                }
                else if (playerOption == 5)
                {
                    Console.WriteLine("Você abriu o inventário.");

                    // Implemente o código para o inventário aqui
                    Console.WriteLine("1. Usar poção");
                    Console.WriteLine("2. Voltar à seleção de ataques");
                    Console.Write("Escolha uma opção: ");
                    int inventoryOption = int.Parse(Console.ReadLine());




                    if (inventoryOption == 1)
                    {
                        playerPokemon.GetPotion();
                    }

                    else if (inventoryOption == 2)
                    {
                        // Continuar com a seleção de ataques
                    }
                }
            }

            if (playerPokemon.Health == 0)
                Console.WriteLine($"\nSeu Pokémon {playerPokemon.Name} foi derrotado. Você perdeu!");
            else
                Console.WriteLine("\nVocê derrotou todos os Pokémon inimigos. Você venceu!");

            Console.ReadLine();
        }
    }
}
