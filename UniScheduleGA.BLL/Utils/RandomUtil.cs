namespace UniScheduleGA.BLL.Utils;

public class RandomUtil
{
    private static Random _random = new(DateTime.Now.Millisecond);

    public static int Rand()
    {
        return _random.Next(0, 32767);
    }
    public static double Random()
    {
        return _random.NextDouble();
    }

    public static int Rand(int size)
    {
        return _random.Next(size);
    }

    public static int Rand(int min, int max)
    {
        return min + Rand(max - min + 1);
    }
    
    public static int RandEvenNumber(int min, int max)
    {
        return 2 * Rand(min / 2, max / 2);
    }

    public static double Rand(double min, double max)
    {
        return min + _random.NextDouble() * (max - min);
    }
}