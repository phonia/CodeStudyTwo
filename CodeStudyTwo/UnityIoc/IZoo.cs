using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityIoc
{
    public interface IZoo
    {
        void ThinkAloud();
        void AddAnimal(IAnimal animal);
    }

    public class UsualZoo : IZoo
    {
        public IAnimal PAnimal { get; set; }
        public IAnimal CAnimal { get; set; }
        public IAnimal MAnimal { get; set; }
        public IAnimal[] Animals { get; set; }
        public String[] PetNames { get; set; }
        public String[] FeederNames { get; set; }


        public UsualZoo(IAnimal animal)
        {
            this.CAnimal = animal;
        }

        public void AddAnimal(IAnimal animal)
        {
            this.MAnimal = animal;
        }

        public void ThinkAloud()
        {
            Console.WriteLine("Three injection methods!");
            if (PAnimal != null) PAnimal.Say();
            if (CAnimal != null) CAnimal.Say();
            if (MAnimal != null) MAnimal.Say();
            Console.WriteLine("Injiection array");
            if (Animals != null && Animals.Length > 0) Animals.ToList().ForEach(it => it.Say());
            Console.WriteLine("Injiection array with diferent methods");
            if (FeederNames != null && FeederNames.Length > 0) FeederNames.ToList().ForEach(it => Console.WriteLine(it));
            Console.WriteLine("Injection array null");
            if (PetNames != null && PetNames.Length > 0) PetNames.ToList().ForEach(it => Console.WriteLine(it));
        }
    }
}
