using System;
using System.Collections.Generic;
using System.Linq;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using System.Security.Cryptography;

namespace BusinessCenter.Data.Implementation
{
    public class UserPasswordTrackingRepository : GenericRepository<UserPasswordTracking>,  IUserPasswordTrackingRepository
    {
        //private const int PBKDF2IterCount = 1000;
        //private const int PBKDF2SubkeyLength = 32;
        //private const int SaltSize = 16;
        public UserPasswordTrackingRepository(IUnitOfWork context)
            : base(context)
        {
        }
        /// <summary>
        /// This method is used to get All user password tracking 
        /// </summary>
        /// <returns>Return All user password tracking</returns>
        public IEnumerable<UserPasswordTracking> UserPasswordTrackingData()
        {
            return GetAll().AsQueryable();
        }
        /// <summary>
        /// This method is used to Insert User password tracking based on user id and password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns>Return Bool Value</returns>
        public bool InsertUserPasswordTracking(string userId, string password)
        {
            bool result;
            try
            {
                var userPasswordTracking = new UserPasswordTracking
                {
                    PasswordTrackId = Guid.NewGuid().ToString(),
                    UserId = userId,
                    PasswordTrack = password,
                    CreatedDate = DateTime.Now
                    };
                    Add(userPasswordTracking);
                    Save();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// This method is used to Get password exist or not based on hased password and password
        /// </summary>
        /// <param name="hashedPassword"></param>
        /// <param name="password"></param>
        /// <returns>Return Bool Value</returns>
        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (hashedPassword == null)
                return false;
            if (password == null)
                throw new ArgumentNullException("password");
            byte[] numArray = Convert.FromBase64String(hashedPassword);
            if (numArray.Length != 49 || (int)numArray[0] != 0)
                return false;
            byte[] salt = new byte[16];
            Buffer.BlockCopy((Array)numArray, 1, (Array)salt, 0, 16);
            byte[] a = new byte[32];
            Buffer.BlockCopy((Array)numArray, 17, (Array)a, 0, 32);
            byte[] bytes;
            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, 1000))
                bytes = rfc2898DeriveBytes.GetBytes(32);
            return ByteArraysEqual(a, bytes);
        }
        /// <summary>
        /// This method is used to Get password exist
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Return Bool Value</returns>
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (ReferenceEquals((object)a, (object)b))
                return true;
            if (a == null || b == null || a.Length != b.Length)
                return false;
            bool flag = true;
            for (int index = 0; index < a.Length; ++index)
                flag = flag & (int)a[index] == (int)b[index];
            return flag;
        }
        /// <summary>
        /// This method is used to Check passrod exist or not based on user id and password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns>Return Bool Value</returns>
        public bool PasswordStatus(string userId, string password)
        {
            bool status = false;
            try
            {
                var getUserPasswordList =FindBy(x => x.UserId == userId).OrderByDescending(x => x.CreatedDate).Take(3).ToList();
                if (getUserPasswordList.Select(item => VerifyHashedPassword(item.PasswordTrack, password)).Any(chcekPassword => chcekPassword))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                status = false;
            }

            return status;
        }
    }
}