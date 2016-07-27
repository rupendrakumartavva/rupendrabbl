using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class MasterCategoryQuestionData
    {
     private readonly List<MasterCategoryQuestion> _entities;
        public void AddCategoryQuestionEntity(MasterCategoryQuestion obj)
        {
            _entities.Add(obj);
        }

        public List<MasterCategoryQuestion> CategoryQuestionEntitiesList
        {
            get { return _entities; }
        }

        public MasterCategoryQuestionData()
        {

            _entities = new List<MasterCategoryQuestion>();

            AddCategoryQuestionEntity(new MasterCategoryQuestion()
            {
             MasterCategoryQuestionId=1,
             CategoryName = "Hotel",
             Quantity = "Rooms",
             UserQuestion = "How many Rooms in your Hotel?"
            });
            AddCategoryQuestionEntity(new MasterCategoryQuestion()
            {
                MasterCategoryQuestionId = 2,
                CategoryName = "Hotel",
                Quantity = "Kitchens",
                UserQuestion = "How many Kitchens in your Hotel?"
            });
            AddCategoryQuestionEntity(new MasterCategoryQuestion()
            {
                MasterCategoryQuestionId = 3,
                CategoryName = "Restaurant",
                Quantity = "Seats",
                UserQuestion = "How many Seats in your Restaurant?"
            });

        }
    }
}
