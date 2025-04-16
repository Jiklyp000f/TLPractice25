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
            Console.WriteLine( $"Раунд {round++}.\n" +
                              $"Ярость бойцов увеличивает их урон на {BattleRage}" );

            // Перемешаем список бойцов для случайного порядка атаки
            survivingFighters = ShuffleList( survivingFighters );

            // Создаем список мертвых бойцов
            var deadFighters = new List<IFighter>();

            foreach ( var attacker in survivingFighters )
            {
                // Выбираем случайного противника из оставшихся в живых
                var defenders = survivingFighters.Where( defender => defender != attacker && !deadFighters.Contains( defender ) ).ToList();
                if ( defenders.Count == 0 )
                    continue;

                var defender = defenders[ new Random().Next( defenders.Count ) ];

                if ( FightAndCheckIfOpponentDead( attacker, defender ) )
                {
                    deadFighters.Add( defender ); // Помечаем бойца как мертвого
                }
            }

            // Удаляем мертвых бойцов из списка выживших
            foreach ( var deadFighter in deadFighters )
            {
                survivingFighters.Remove( deadFighter );
            }

            Console.WriteLine( "\nНажмите Enter для перехода в следующий раунд\n" );
        }

        return survivingFighters.FirstOrDefault(); // Возвращаем победителя или null, если не найден
    }
    private List<T> ShuffleList<T>( List<T> list )
    {
        Random rng = new Random();
        int n = list.Count;
        while ( n > 1 )
        {
            n--;
            int k = rng.Next( n + 1 );
            T value = list[ k ];
            list[ k ] = list[ n ];
            list[ n ] = value;
        }
        return list;
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
            $"Количество жизней: {opponent.CurrentHealth}" );
            return false;
        }


        opponent.TakeDamage( damage + BattleRage );

        Console.WriteLine(
            $"Боец {opponent.Name} получает {damage + BattleRage} урона. \n" +
            $"Количество жизней: {opponent.CurrentHealth}" );


        return opponent.CurrentHealth < 1;
    }
    public int EvaidRandom()
    {
        Random random = new Random();
        int Evasion = random.Next( 0, 101 );
        return Evasion;
    }
}
