public class GameMaster
{
    public static int BattleRage;
    public IFighter PlayAndGetWinner( List<IFighter> fighters )
    {
        var survivingFighters = new List<IFighter>( fighters );
        int round = 1;
        Console.WriteLine( "Нажмите Enter для начала боя" );

        while ( survivingFighters.Count > 1 )
        {
            Console.ReadLine();
            BattleRage = round - ( round / 2 );
            Console.WriteLine( $"Раунд {round++}.\nЯрость бойцов увеличивает их урон на {BattleRage}" );

            // Сортировка по инициативе с перемешиванием внутри групп
            var rng = new Random();
            survivingFighters = survivingFighters
                .GroupBy( f => f.Initiative )
                .OrderByDescending( g => g.Key ) // Сортируем группы по убыванию инициативы
                .SelectMany( g => g
                    .OrderBy( f => rng.Next() ) // Перемешиваем внутри группы
                    .ToList() )
                .ToList();

            var deadFighters = new List<IFighter>();
            foreach ( var attacker in survivingFighters )
            {
                var defenders = survivingFighters
                    .Where( defender => defender != attacker && !deadFighters.Contains( defender ) )
                    .ToList();

                if ( defenders.Count == 0 ) continue;

                var defender = defenders[ rng.Next( defenders.Count ) ];
                if ( FightAndCheckIfOpponentDead( attacker, defender ) )
                {
                    deadFighters.Add( defender );
                }
            }

            survivingFighters.RemoveAll( f => deadFighters.Contains( f ) );
            Console.WriteLine( "\nНажмите Enter для перехода в следующий раунд\n" );
        }

        return survivingFighters.FirstOrDefault();
    }
    private bool FightAndCheckIfOpponentDead( IFighter roundOwner, IFighter opponent )
    {

        int evasionChance = opponent.Evasion + EvaidRandom();
        if ( evasionChance > 100 )
        {
            evasionChance = 100;
        }
        int damage = roundOwner.CalculateDamage() - opponent.CalculateProtect();
        if ( damage < 1 ) damage = 1;
        if ( evasionChance >= 100 )
        {
            Console.WriteLine(
            $"Боец {opponent.Name} уклоняется от атаки!\n" +
            $"Здоровье: {opponent.GetHealthDisplay()}" );
            return false;
        }


        opponent.TakeDamage( damage + BattleRage );

        Console.WriteLine(
            $"Боец {opponent.Name} получает {damage + BattleRage} урона. \n" +
            $"Здоровье: {opponent.GetHealthDisplay()}" );


        return opponent.CurrentHealth < 1;
    }
    public int EvaidRandom()
    {
        Random random = new Random();
        int Evasion = random.Next( 0, 101 );
        return Evasion;
    }
}
