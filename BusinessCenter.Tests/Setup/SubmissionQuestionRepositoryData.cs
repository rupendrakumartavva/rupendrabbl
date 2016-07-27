using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
 public class SubmissionQuestionRepositoryData
    {
     private readonly List<SubmissionQuestion> _entities;
        public bool IsInitialized;

        public void AddSubmissionQuestionEntity(SubmissionQuestion obj)
        {
            _entities.Add(obj);
        }

        public List<SubmissionQuestion>SubmissionQuestionEntitiesList
        {
            get { return _entities; }
        }


        public SubmissionQuestionRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<SubmissionQuestion>();

            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 1,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Question = "How many Rooms in your Hotel?",
                Answer = "20",
                OptionType = "Textbox",
                CreatedDate=DateTime.Now,
                UpdatedDate=null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 2,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Question = "How many Kitchens in your Hotel?",
                Answer = "2",
                OptionType = "Textbox",
                CreatedDate = DateTime.Now,
                UpdatedDate=null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 3,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Question = "How many Seats in your Restaurant?",
                Answer = "100",
                OptionType = "Textbox",
                CreatedDate = DateTime.Now,
                UpdatedDate=null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 4,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Question = "Would you like a two (2) or four (4) year license?",
                Answer = "Two (2) year",
                OptionType = "RadioButton",
                CreatedDate = DateTime.Now,
                UpdatedDate=null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 5,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Question = "Is this business already registered with DCRA’s Corporations Division?",
                Answer = "YES",
                OptionType="RadioButton",
                CreatedDate = DateTime.Now,
                UpdatedDate=null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 6,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Question = "What is your Business Structure ?",
                Answer="Select One",
                OptionType="Dropdown",
                CreatedDate = DateTime.Now,
                UpdatedDate=null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 7,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Question = "What is the Trade Name?",
                Answer="",
                OptionType="Textbox",
                CreatedDate = DateTime.Now,
                UpdatedDate=null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 8,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                Question = "Which Tax Identification Number is associated with your business: Federal Employer Identification Number (FEIN) or  Social Security Number (SSN)?",
                Answer="FEIN",
                OptionType="RadioButton",
                CreatedDate = DateTime.Now,
                UpdatedDate=null

            });

            //---------------------------------------------


            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 9,
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                Question = "Would you like a two (2) or four (4) year license?",
                Answer = "Two (2) year",
                OptionType = "RadioButton",
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 10,
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                Question = "Will this business be located in the District of Columbia?",
                Answer = "YES",
                OptionType = "RadioButton",
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 11,
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                Question = "Will this Business be Home based?",
                Answer = "YES",
                OptionType = "RadioButton",
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 12,
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                Question = "Do you have a Home Occupancy Permit (HOP) from Office of Zoning?",
                Answer = "YES",
                OptionType = "RadioButton",
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 13,
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                Question = "Is this business already registered with DCRA’s Corporations Division?",
                Answer = "YES",
                OptionType = "RadioButton",
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 14,
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                Question = "What is your Business Structure ?",
                Answer = "Select One",
                OptionType = "Dropdown",
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 15,
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                Question = "What is the Trade Name?",
                Answer = "",
                OptionType = "Textbox",
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 16,
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                Question = "Which Tax Identification Number is associated with your business: Federal Employer Identification Number (FEIN) or  Social Security Number (SSN)?",
                Answer = "FEIN",
                OptionType = "RadioButton",
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            });
            //-----------------------------
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 17,
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                Question = "Would you like a two (2) or four (4) year license?",
                Answer = "Two (2) year",
                OptionType = "RadioButton",
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 18,
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                Question = "Will this business be located in the District of Columbia?",
                Answer = "YES",
                OptionType = "RadioButton",
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 19,
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                Question = "Is this business already registered with DCRA’s Corporations Division?",
                Answer = "YES",
                OptionType = "RadioButton",
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 20,
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                Question = "What is your Business Structure ?",
                Answer = "Select One",
                OptionType = "Dropdown",
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 21,
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                Question = "What is the Trade Name?",
                Answer = "",
                OptionType = "Textbox",
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 22,
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                Question = "Which Tax Identification Number is associated with your business: Federal Employer Identification Number (FEIN) or  Social Security Number (SSN)?",
                Answer = "FEIN",
                OptionType = "RadioButton",
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            });

            //---------------------------------------------------------------------------------------------------------------

            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 23,
                MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
                Question = "Please enter the full name of the Business Owner:",
                Answer = "CODE IT",
                OptionType = "Textbox",
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 24,
                MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
                Question = "Would you like a two (2) or four (4) year license?",
                Answer = "Two (2) year",
                OptionType = "RadioButton",
                CreatedDate = DateTime.Now,
                UpdatedDate = null
            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 25,
                MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
                Question = "Will this business be located in the District of Columbia?",
                Answer = "YES",
                OptionType = "RadioButton",
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 26,
                MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
                Question = "Will this Business be Home based?",
                Answer = "YES",
                OptionType = "RadioButton",
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            });
            //-----------------------------
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 27,
                MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
                Question = "Do you have a Home Occupancy Permit (HOP) from Office of Zoning?",
                Answer = "YES",
                OptionType = "RadioButton",
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 28,
                MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
                Question = "Is this business already registered with DCRA’s Corporations Division?",
                Answer = "NO",
                OptionType = "RadioButton",
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 29,
                MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
                Question = "What is your Business Structure ?",
                Answer = "Select One",
                OptionType = "Dropdown",
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 30,
                MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
                Question = "Please enter the full name of the business owner (if applying for a business license) or full employee name( if applying for an individual license) as it will appear on the business license.  Cannot be a company or trade name, must be an individual.",
                Answer = "",
                OptionType = "Textbox",
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 31,
                MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
                Question = "Which Tax Identification Number is associated with your business: Federal Employer Identification Number (FEIN) or  Social Security Number (SSN)?",
                Answer = "FEIN",
                OptionType = "RadioButton",
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            });
            AddSubmissionQuestionEntity(new SubmissionQuestion()
            {
                SubmQuestionsId = 32,
                MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
                Question = "What is the Trade Name?",
                Answer = "",
                OptionType = "Textbox",
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            });
          
        }

    }
}
