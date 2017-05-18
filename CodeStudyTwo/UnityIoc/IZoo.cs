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
            if (PAnimal != null) PAnimal.Say();
            if (CAnimal != null) CAnimal.Say();
            if (MAnimal != null) MAnimal.Say();
        }
    }
}
