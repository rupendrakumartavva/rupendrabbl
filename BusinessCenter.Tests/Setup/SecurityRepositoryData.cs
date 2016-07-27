using System.Collections.Generic;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class SecurityRepositoryData
    {
        private readonly List<SecurityQuestion> _entities;
        public bool IsInitialized;

        public void AddSecurityQuestionEntity(SecurityQuestion obj)
        {
            _entities.Add(obj);
        }

        public List<SecurityQuestion> SecurityQuestionEntitiesList
        {
            get { return _entities; }
        }
        public SecurityRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<SecurityQuestion>();

            AddSecurityQuestionEntity(new SecurityQuestion()
            {
                id = 1,
                Question = "what is your favorite color"
            });
            AddSecurityQuestionEntity(new SecurityQuestion()
            {
                id = 2,
                Question = "what is your pet name"
            });
            AddSecurityQuestionEntity(new SecurityQuestion()
            {
                id = 3,
                Question = "what is your primary school name"
            });
            AddSecurityQuestionEntity(new SecurityQuestion()
            {
                id = 4,
                Question = "what is your phone color"
            });
        }
    }
}