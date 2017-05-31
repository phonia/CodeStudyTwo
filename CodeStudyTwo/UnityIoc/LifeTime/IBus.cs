using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityIoc
{
    public interface IBus
    {
        void AddPassengers(IHumanbeings humanbeings);
        void Drive();
    }

    public class Bus : IBus
    {
        public String Name { get; set; }

        public List<IHumanbeings> Passengers { get; set; }

        #region IBus 成员

        public void Drive()
        {
            if (Passengers != null)
                Passengers.ForEach(it => Console.WriteLine(it.Name + " on the Bus!"));
            Console.WriteLine(Name + " begin Driving!");
        }

        #endregion

        #region IBus 成员

        public void AddPassengers(IHumanbeings humanbeings)
        {
            if (Passengers == null) Passengers = new List<IHumanbeings>();
            if (humanbeings != null) Passengers.Add(humanbeings);
        }

        #endregion
    }

    public interface IHumanbeings
    {
        String Name { get; set; }
        void Aboard();
    }

    public class Children : IHumanbeings
    {
        public IBus Bus { get; set; }
        public String Name { get; set; }

        #region IHumanbeings 成员

        public void Aboard()
        {
            Console.WriteLine("Children Abord!");
            if (Bus != null)
            {
                Bus.AddPassengers(this);
                Bus.Drive();
            }
        }

        #endregion
    }

    public class Man : IHumanbeings
    {
        public IBus Bus { get; set; }
        public String Name { get; set; }

        #region IHumanbeings 成员

        public void Aboard()
        {
            Console.WriteLine("Man Abord!");
            if (Bus != null)
            {
                Bus.AddPassengers(this);
                Bus.Drive();
            }
        }

        #endregion
    }

    public class Women : IHumanbeings
    {
        public IBus Bus { get; set; }
        public String Name { get; set; }

        #region IHumanbeings 成员

        public void Aboard()
        {
            Console.WriteLine("Women Abord!");
            if (Bus != null)
            {
                Bus.AddPassengers(this);
                Bus.Drive();
            }
        }

        #endregion
    }
}
