using System;
using System.Collections.Generic;
using BusinessCenter.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
    public class UserRepositoryData
    {
         private readonly List<User> _entities;
        public bool IsInitialized;

        public void AddUserEntity(User obj)
        {
            _entities.Add(obj);
        }

        public List<User> UsersEntitiesList
        {
            get { return _entities; }
        }

        public UserRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<User>();

            AddUserEntity(new User()
            {
                Id = "2AC53E53-87D0-468F-9628-4AEBA1120613",
                Address = "hilliary park",
                City = "cincinatti",
                FirstName = "mark",
                LastName = "hurd",
                MobileNumber = "68519484646",
                PostalCode = "88788",
                State = "ohio",
                LastLoginDateandTime = Convert.ToDateTime("2015-11-25"),
                IsActive = true,
                Email = "markhurd1@dc.gov",
                EmailConfirmed = true,
                ChangeEmailValidate = Convert.ToDateTime("2015-08-06"),
                ChangeEmailConfirmed = true,
                PreviousEmailValidate = Convert.ToDateTime("2015-07-31"),
                PreviousEmailConfirmed = true,
                ActivationCode = "lgr5v28my3dyNm5ksY26ltdcZFwr+/61ICajPXvGi0G9Vs6FctBSeXF7TOxM2PBu4+E/e54SGHx/sbAjEU4b2oeeszPecRKeOvJgtmhDeeaDHHzn44ic09jKijytXllaueefksGvhQVbdyhxJO5CPw==",
                Password = "AP9yfoBFrjwssnjfhpx++sxfFQ2p0kPNkfkYNjP4CNdnfxTEbMBus9NTsCk/VyoLXA==",
                SecurityStamp = "896883a2-4698-4828-be92-4ab52d3f305b",
                PhoneNumber = "",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEndDateUtc = Convert.ToDateTime("2015-08-07"),
                LockoutEnabled = true,
                AccessFailedCount = 0,
                UserName = "markhurd1",
                SecurityQuestion1 = "In what year was your father born?",
                SecurityQuestion2 = "Who is your favorite American president?",
                SecurityQuestion3 = "Where were you when you first heard about 9/11?",
                SecurityAnswer1 = "1960",
                SecurityAnswer2 = "obama",
                SecurityAnswer3 = "lasvegas",
                Title = "MR.",
                ActivationDate = Convert.ToDateTime("2015-08-07"),
                SecondaryEmail = "",
                IsDelete = false,
                CreatedDate = Convert.ToDateTime("2015-08-06"),
                IsForgot = false,
                DeleteComment = "",
                UpdatedDate = Convert.ToDateTime("2015-08-07"),
                IsLoggedIn=true,
                LoginSessionId = "a563064e-4372-40a1-ac50-42789e307879"
                //NormalizedName = "markhurd1"
            });
            AddUserEntity(new User()
            {
                Id = "8EB70E26-725E-4E52-9109-CF9C37F3B980",
                Address = "lincoln street",
                City = "san jose",
                FirstName = "chuck",
                LastName = "robbins",
                MobileNumber = "565545465",
                PostalCode = "50008",
                State = "california",
                LastLoginDateandTime = Convert.ToDateTime("2015-12-01"),
                IsActive = true,
                Email = "chuckrobbins@dc.gov",
                EmailConfirmed = true,
                ChangeEmailValidate = Convert.ToDateTime("2015-08-06"),
                ChangeEmailConfirmed = true,
                PreviousEmailValidate = Convert.ToDateTime("2015-07-31"),
                PreviousEmailConfirmed = true,
                ActivationCode = "yGdjaosJQI/vzWxkCOq3kPntXVPkkgloGW9lyK75vmOf3As8+7TA5qVFyVqp/EIHOEL6dBcT+i6b029AKKhspVZcjLZxWrZ9j4wpq9m4U8gtVcbEgfvz7FyBm8pp6gZk52cIaE/RdsGazhDsGRWYya/Yd2mrh+P12eJhwB8MT+mO+sPnf+eaNqW7bYizkp2l",
                Password = "AIu2V7Pou0UWt+BEigDJ8NB4lc6EsWeO+PngX9TEc50ZjCDbBRRa/KkC9saTrCvWWA==",
                SecurityStamp = "b6b38868-039f-42f0-a8b9-491c76890298",
                PhoneNumber = "",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEndDateUtc = Convert.ToDateTime("2015-08-07"),
                LockoutEnabled = true,
                AccessFailedCount = 0,
                UserName = "chuckrobbins12",
                SecurityQuestion1 = "In what city or town does your nearest sibling live?",
                SecurityQuestion2 = "In what year was your father born?",
                SecurityQuestion3 = "In what city did your mother and father meet?",
                SecurityAnswer1 = "miami",
                SecurityAnswer2 = "1972",
                SecurityAnswer3 = "1990",
                Title = "MR.",
                ActivationDate = Convert.ToDateTime("2015-08-07"),
                SecondaryEmail = "",
                IsDelete = false,
                CreatedDate = Convert.ToDateTime("2015-08-06"),
                IsForgot = false,
                DeleteComment = "",
                UpdatedDate = Convert.ToDateTime("2015-08-07"),
                //NormalizedName = "markhurd1"
            });
            AddUserEntity(new User()
            {
                Id = "F418805F-41B5-4CE7-AA8D-5F07311D03B3",
                Address = "Mountain View",
                City = "San ",
                FirstName = "",
                LastName = "",
                MobileNumber = "",
                PostalCode = "",
                State = "",
                LastLoginDateandTime = Convert.ToDateTime("2015-11-25"),
                IsActive = true,
                Email = "",
                EmailConfirmed = true,
                ChangeEmailValidate = Convert.ToDateTime("2015-08-06"),
                ChangeEmailConfirmed = true,
                PreviousEmailValidate = Convert.ToDateTime("2015-07-31"),
                PreviousEmailConfirmed = true,
                ActivationCode = "",
                Password = "",
                SecurityStamp = "",
                PhoneNumber = "",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEndDateUtc = Convert.ToDateTime("2015-08-07"),
                LockoutEnabled = true,
                AccessFailedCount = 0,
                UserName = "dcbctester01",
                SecurityQuestion1 = "",
                SecurityQuestion2 = "",
                SecurityQuestion3 = "",
                SecurityAnswer1 = "",
                SecurityAnswer2 = "",
                SecurityAnswer3 = "",
                Title = "MR.",
                ActivationDate = Convert.ToDateTime("2015-08-07"),
                SecondaryEmail = "",
                IsDelete = false,
                CreatedDate = Convert.ToDateTime("2015-08-06"),
                IsForgot = false,
                DeleteComment = "",
                UpdatedDate = Convert.ToDateTime("2015-08-07"),
                //NormalizedName = "markhurd1"
            });
            AddUserEntity(new User()
            {
                Id = "0A808817-F22A-49E9-A406-15B0955919F2",
                Address = "",
                City = "",
                FirstName = "",
                LastName = "",
                MobileNumber = "",
                PostalCode = "",
                State = "",
                LastLoginDateandTime = Convert.ToDateTime("2015-11-25"),
                IsActive = true,
                Email = "",
                EmailConfirmed = true,
                ChangeEmailValidate = Convert.ToDateTime("2015-08-06"),
                ChangeEmailConfirmed = true,
                PreviousEmailValidate = Convert.ToDateTime("2015-07-31"),
                PreviousEmailConfirmed = true,
                ActivationCode = "",
                Password = "",
                SecurityStamp = "",
                PhoneNumber = "",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEndDateUtc = Convert.ToDateTime("2015-08-07"),
                LockoutEnabled = true,
                AccessFailedCount = 0,
                UserName = "sanjayiyer242",
                SecurityQuestion1 = "",
                SecurityQuestion2 = "",
                SecurityQuestion3 = "",
                SecurityAnswer1 = "",
                SecurityAnswer2 = "",
                SecurityAnswer3 = "",
                Title = "MR.",
                ActivationDate = Convert.ToDateTime("2015-08-07"),
                SecondaryEmail = "",
                IsDelete = false,
                CreatedDate = Convert.ToDateTime("2015-08-06"),
                IsForgot = false,
                DeleteComment = "",
                UpdatedDate = Convert.ToDateTime("2015-08-07"),
                //NormalizedName = "markhurd1"
            });
            AddUserEntity(new User()
            {
                Id = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                Address = "",
                City = "",
                FirstName = "",
                LastName = "",
                MobileNumber = "",
                PostalCode = "",
                State = "",
                LastLoginDateandTime = Convert.ToDateTime("2015-11-25"),
                IsActive = true,
                Email = "",
                EmailConfirmed = true,
                ChangeEmailValidate = Convert.ToDateTime("2015-08-06"),
                ChangeEmailConfirmed = true,
                PreviousEmailValidate = Convert.ToDateTime("2015-07-31"),
                PreviousEmailConfirmed = true,
                ActivationCode = "",
                Password = "",
                SecurityStamp = "",
                PhoneNumber = "",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEndDateUtc = Convert.ToDateTime("2015-08-07"),
                LockoutEnabled = true,
                AccessFailedCount = 0,
                UserName = "sdgdsg235325",
                SecurityQuestion1 = "",
                SecurityQuestion2 = "",
                SecurityQuestion3 = "",
                SecurityAnswer1 = "",
                SecurityAnswer2 = "",
                SecurityAnswer3 = "",
                Title = "MR.",
                ActivationDate = Convert.ToDateTime("2015-08-07"),
                SecondaryEmail = "",
                IsDelete = false,
                CreatedDate = Convert.ToDateTime("2015-08-06"),
                IsForgot = false,
                DeleteComment = "",
                UpdatedDate = Convert.ToDateTime("2015-08-07"),
                //NormalizedName = "markhurd1"
            });
        }
    }
}
