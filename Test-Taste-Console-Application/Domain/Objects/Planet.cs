using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Test_Taste_Console_Application.Domain.DataTransferObjects;

namespace Test_Taste_Console_Application.Domain.Objects
{
    public class Planet
    {
        public string Id { get; set; }
        public float SemiMajorAxis { get; set; }
        public ICollection<Moon> Moons { get; set; }
        // Changed Signature to Getter & Setter type 
        // SO that later in COnstructor we can Compute values & Set
        public float AverageMoonGravity { get; set; } = 0.0f;

        public Planet(PlanetDto planetDto)
        {
            Id = planetDto.Id;
            SemiMajorAxis = planetDto.SemiMajorAxis;
            Moons = new Collection<Moon>();
            if(planetDto.Moons != null)
            {
                foreach (MoonDto moonDto in planetDto.Moons)
                {
                    Moons.Add(new Moon(moonDto));
                }
                // Using LINQ to Get Average of All Moon Gravity
                // Using built in AVG ( )  Method
                // Assigning Average Gravity back to current Object's AverageMoonGravity
                this.AverageMoonGravity = planetDto.Moons.Average(x => x.Gravity);
            }            
        }

        public Boolean HasMoons()
        {
            return (Moons != null && Moons.Count > 0);
        }
    }
}
