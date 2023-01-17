using System.Diagnostics;
using GeneticSharp;
using UniScheduleGA.BLL;
using UniScheduleGA.Models.Configuration;

namespace UniScheduleGA.ConsoleApp;

class ConsoleApp
{
    static void Main(string[] args)
    {
        var selection = new EliteSelection();
        var crossover = new UniformCrossover(0.75f);
        var mutation = new UniformMutation(true);

        var configuration = new DataProvider();

        var fitness = new ScheduleFitness();

        var chromosome = new ScheduleChromosome(configuration.CourseClasses, configuration.Professors, configuration.Courses, configuration.Rooms);

        var population = new Population(50, 120, chromosome);

        var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
        ga.Termination = new AndTermination(new ITermination[] {new FitnessThresholdTermination(1.0d), new FitnessStagnationTermination(5) });
        ga.GenerationRan += (s, e) => Console.WriteLine($"Generation {ga.GenerationsNumber}. Best fitness: {ga.BestChromosome.Fitness.Value}");

        ga.Population.BestChromosomeChanged += new EventHandler(BestChromosomeChanged);
        
        Console.WriteLine("Genetic Algorithm Schedule running...");
        ga.Start();
        
        Console.WriteLine();
        Console.WriteLine($"Best solution found has fitness: {ga.BestChromosome.Fitness} and was found in {ga.GenerationsNumber} generations");
        Console.WriteLine($"Elapsed time: {ga.TimeEvolving}");
        
        void BestChromosomeChanged(object? populationEvt, EventArgs _)
        {
            var currentBestChromosome = (populationEvt as Population)?.BestChromosome;
            if (currentBestChromosome?.Fitness > 0.95f)
            {
                Console.WriteLine("Found 95+ percentile chromosomes");
            }
        }
        
        // Output the result in human readable format in html
        var htmlResult = HtmlOutput.GetResult(ga.BestChromosome as ScheduleChromosome);

        var tempFilePath = Path.GetTempPath() + "result.htm";
        using (StreamWriter outputFile = new StreamWriter(tempFilePath))
        {
            outputFile.WriteLine(htmlResult);
        }
        
        using (var proc = new Process())
        {
            proc.StartInfo.FileName = tempFilePath;
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.Verb = "open";
            proc.Start();
        }
    }
}