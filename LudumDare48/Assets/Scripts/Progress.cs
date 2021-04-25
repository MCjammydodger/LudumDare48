using System.Collections.Generic;

public static class Progress
{
    private const int planetsToComplete = 2;
    private static List<Planets> planetsCompleted = new List<Planets>();
    public static void PlanetCompleted(Planets planet)
    {
        if (planetsCompleted.Contains(planet) == false)
        {
            planetsCompleted.Add(planet);
        }
    }

    public static int GetPlanetsCompletedCount()
    {
        return planetsCompleted.Count;
    }

    public static int GetTotalPlanetsToComplete()
    {
        return planetsToComplete;
    }
    
    public static bool HasCompletedAllPlanets()
    {
        return GetPlanetsCompletedCount() == GetTotalPlanetsToComplete();
    }

    public static bool HasCompletedPlanet(Planets planet)
    {
        return planetsCompleted.Contains(planet);
    }
}
