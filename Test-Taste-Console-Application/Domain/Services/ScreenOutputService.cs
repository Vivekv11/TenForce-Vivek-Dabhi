using System;
using System.Linq;
using Test_Taste_Console_Application.Constants;
using Test_Taste_Console_Application.Domain.Objects;
using Test_Taste_Console_Application.Domain.Services.Interfaces;
using Test_Taste_Console_Application.Utilities;

namespace Test_Taste_Console_Application.Domain.Services
{
    /// <inheritdoc />
    public class ScreenOutputService : IOutputService
    {
        private readonly IPlanetService _planetService;

        private readonly IMoonService _moonService;

        public ScreenOutputService(IPlanetService planetService, IMoonService moonService)
        {
            _planetService = planetService;
            _moonService = moonService;
        }

        public void OutputAllPlanetsAndTheirMoonsToConsole()
        {
            try
            {
                #region Custom 'Loading...' Text to Display In Console
                // Added Custom Loading Text for Displaying in Console
                var columnSizesForLoadingMessage = new[] { 90 };
                var columnLabelsForLoadingMessage = new[]
                {
                    OutputString.LOADING_DATA_MESSAGE_PLANET_AND_THEIR_MOON
                };
                ConsoleWriter.CreateHeader(columnLabelsForLoadingMessage, columnSizesForLoadingMessage);
                ConsoleWriter.CreateEmptyLines(2);
                #endregion


                //The service gets all the planets from the API.
                var planets = _planetService.GetAllPlanets().ToArray();

                //If the planets aren't found, then the function stops and tells that to the user via the console.
                if (!planets.Any())
                {
                    Console.WriteLine(OutputString.NoPlanetsFound);
                    return;
                }

                #region Custom 'Writing...' Text to Display in Console
                // Added Custom Writing Text for Displaying in Console
                var columnSizesForWritingMessage = new[] { 90 };
                var columnLabelsForWritingMessage = new[]
                {
                    OutputString.WRITING_DATA_MESSAGE_PLANET_AND_THEIR_MOON
                };
                ConsoleWriter.CreateHeader(columnLabelsForWritingMessage, columnSizesForWritingMessage);
                ConsoleWriter.CreateEmptyLines(2);
                #endregion

                //The column sizes and labels for the planets are configured here. 
                var columnSizesForPlanets = new[] { 20, 20, 30, 20 };
                var columnLabelsForPlanets = new[]
                {
                    OutputString.PlanetNumber, OutputString.PlanetId, OutputString.PlanetSemiMajorAxis,
                    OutputString.TotalMoons
                };

                //The column sizes and labels for the moons are configured here.
                //The second moon's column needs the 2 extra '-' characters so that it's aligned with the planet's column.
                var columnSizesForMoons = new[] { 20, 70 + 2 };
                var columnLabelsForMoons = new[]
                {
                    OutputString.MoonNumber, OutputString.MoonId
                };

                //The for loop creates the correct output.
                for (int i = 0, j = 1; i < planets.Length; i++, j++)
                {
                    //First the line is created.
                    ConsoleWriter.CreateLine(columnSizesForPlanets);

                    //Under the line the header is created.
                    ConsoleWriter.CreateText(columnLabelsForPlanets, columnSizesForPlanets);

                    //Under the header the planet data is shown.
                    ConsoleWriter.CreateText(
                        new[]
                        {
                        j.ToString(), CultureInfoUtility.TextInfo.ToTitleCase(planets[i].Id),
                        planets[i].SemiMajorAxis.ToString(),
                        planets[i].Moons.Count.ToString()
                        },
                        columnSizesForPlanets);

                    //Under the planet data the header for the moons is created.
                    ConsoleWriter.CreateLine(columnSizesForPlanets);
                    ConsoleWriter.CreateText(columnLabelsForMoons, columnSizesForMoons);

                    //The for loop creates the correct output.
                    for (int k = 0, l = 1; k < planets[i].Moons.Count; k++, l++)
                    {
                        ConsoleWriter.CreateText(
                            new[]
                            {
                            l.ToString(), CultureInfoUtility.TextInfo.ToTitleCase(planets[i].Moons.ElementAt(k).Id)
                            },
                            columnSizesForMoons);
                    }

                    //Under the data the footer is created.
                    ConsoleWriter.CreateLine(columnSizesForMoons);
                    ConsoleWriter.CreateEmptyLines(2);

                    /*
                        This is an example of the output for the planet Earth:
                        --------------------+--------------------+------------------------------+--------------------
                        Planet's Number     |Planet's Id         |Planet's Semi-Major Axis      |Total Moons
                        10                  |Terre               |0                             |1
                        --------------------+--------------------+------------------------------+--------------------
                        Moon's Number       |Moon's Id
                        1                   |La Lune
                        --------------------+------------------------------------------------------------------------
                    */
                }
            }
            catch (Exception exception)
            {
                // Added Exception Handling & Error Logging
                Logger.Instance.Error($"{LoggerMessage.ScreenOutputOperationFailed}{exception.Message}");
                Console.WriteLine($"{ExceptionMessage.ScreenOutputOperationFailed}{exception.Message}");
                throw;
            }
        }

        public void OutputAllMoonsAndTheirMassToConsole()
        {
            try
            {
                #region Custom 'Loading...' Text to Display In Console
                // Added Custom Loading Text for Displaying in Console
                var columnSizesForLoadingMessage = new[] { 90 };
                var columnLabelsForLoadingMessage = new[]
                {
                    OutputString.LOADING_DATA_MESSAGE_MOON_AND_THEIR_MASS
                };
                ConsoleWriter.CreateHeader(columnLabelsForLoadingMessage, columnSizesForLoadingMessage);
                ConsoleWriter.CreateEmptyLines(2);
                #endregion

                //The function works the same way as the PrintAllPlanetsAndTheirMoonsToConsole function. You can find more comments there.
                var moons = _moonService.GetAllMoons().ToArray();

                if (!moons.Any())
                {
                    Console.WriteLine(OutputString.NoMoonsFound);
                    return;
                }

                #region Custom 'Writing...' Text to Display in Console
                // Added Custom Writing Text for Displaying in Console
                var columnSizesForWritingMessage = new[] { 90 };
                var columnLabelsForWritingMessage = new[]
                {
                    OutputString.WRITING_DATA_MESSAGE_MOON_AND_THEIR_MASS
                };
                ConsoleWriter.CreateHeader(columnLabelsForWritingMessage, columnSizesForWritingMessage);
                ConsoleWriter.CreateEmptyLines(2);
                #endregion

                var columnSizesForMoons = new[] { 20, 20, 30, 20 };
                var columnLabelsForMoons = new[]
                {
                    OutputString.MoonNumber, OutputString.MoonId, OutputString.MoonMassExponent, OutputString.MoonMassValue
                };

                ConsoleWriter.CreateHeader(columnLabelsForMoons, columnSizesForMoons);

                for (int i = 0, j = 1; i < moons.Length; i++, j++)
                {
                    ConsoleWriter.CreateText(
                        new[]
                        {
                        j.ToString(), CultureInfoUtility.TextInfo.ToTitleCase(moons[i].Id),
                        moons[i].MassExponent.ToString(), moons[i].MassValue.ToString()
                        },
                        columnSizesForMoons);
                }

                ConsoleWriter.CreateLine(columnSizesForMoons);
                ConsoleWriter.CreateEmptyLines(2);

                /*
                    This is an example of the output for the moon around the earth:
                    --------------------+--------------------+------------------------------+--------------------
                    Moon's Number       |Moon's Id           |Moon's Mass Exponent          |Moon's Mass Value
                    --------------------+--------------------+------------------------------+--------------------
                    1                   |Lune                |22                            |7,346             
                    ...more data...
                    --------------------+--------------------+------------------------------+--------------------
                 */
            }
            catch (Exception exception)
            {
                // Added Exception Handling & Error Logging
                Logger.Instance.Error($"{LoggerMessage.ScreenOutputOperationFailed}{exception.Message}");
                Console.WriteLine($"{ExceptionMessage.ScreenOutputOperationFailed}{exception.Message}");
                throw;
            }
        }

        public void OutputAllPlanetsAndTheirAverageMoonGravityToConsole()
        {
            try
            {
                #region Custom 'Loading...' Text to Display In Console
                // Added Custom Loading Text for Displaying in Console
                var columnSizesForLoadingMessage = new[] { 50 };
                var columnLabelsForLoadingMessage = new[]
                {
                    OutputString.LOADING_DATA_MESSAGE_PLANET_AVG_GRAVITY
                };
                ConsoleWriter.CreateHeader(columnLabelsForLoadingMessage, columnSizesForLoadingMessage);
                ConsoleWriter.CreateEmptyLines(2);
                #endregion

                //The function works the same way as the PrintAllPlanetsAndTheirMoonsToConsole function. You can find more comments there.
                var planets = _planetService.GetAllPlanets().ToArray();
                if (!planets.Any())
                {
                    Console.WriteLine(OutputString.NoMoonsFound);
                    return;
                }
                #region Custom 'Writing...' Text to Display in Console
                // Added Custom Writing Text for Displaying in Console
                var columnSizesForWritingMessage = new[] { 50 };
                var columnLabelsForWritingMessage = new[]
                {
                    OutputString.WRITING_DATA_MESSAGE_PLANET_AVG_GRAVITY
                };
                ConsoleWriter.CreateHeader(columnLabelsForWritingMessage, columnSizesForWritingMessage);
                ConsoleWriter.CreateEmptyLines(2);
                #endregion

                var columnSizes = new[] { 20, 30 };
                var columnLabels = new[]
                {
                    OutputString.PlanetId, OutputString.PlanetMoonAverageGravity
                };


                ConsoleWriter.CreateHeader(columnLabels, columnSizes);

                foreach(Planet planet in planets)
                {
                    if(planet.HasMoons())
                    {
                        // Added Proper String Formatting to Display 3 Digit After Floating point for Better UI Experience & Accuracy Purpose
                        ConsoleWriter.CreateText(new string[] { $"{planet.Id}", $"{String.Format("{0:0.000}", planet.AverageMoonGravity)}" }, columnSizes);
                    }
                    else
                    {
                        ConsoleWriter.CreateText(new string[] { $"{planet.Id}", $"-" }, columnSizes);
                    }
                }

                ConsoleWriter.CreateLine(columnSizes);
                ConsoleWriter.CreateEmptyLines(2);

                /*
                    --------------------+--------------------------------------------------
                    Planet's Number     |Planet's Average Moon Gravity
                    --------------------+--------------------------------------------------
                    1                   |0.0f
                    --------------------+--------------------------------------------------
                */
            }
            catch (Exception exception)
            {
                // Added Exception Handling & Error Logging
                Logger.Instance.Error($"{LoggerMessage.ScreenOutputOperationFailed}{exception.Message}");
                Console.WriteLine($"{ExceptionMessage.ScreenOutputOperationFailed}{exception.Message}");
                throw;
            }
        }
    }
}
