using System;
using System.Collections.Generic;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class UserPasswordTrackingRepositoryData
    {
        private readonly List<UserPasswordTracking> _entities;
        public bool IsInitialized;
        public void AddUserPasswordTrackingEntity(UserPasswordTracking obj)
        {
            _entities.Add(obj);
        }

        public List<UserPasswordTracking> UserPasswordTrackingEntitiesList
        {
            get { return _entities; }
        }

        public UserPasswordTrackingRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<UserPasswordTracking>();

            AddUserPasswordTrackingEntity(new UserPasswordTracking()
            {
                PasswordTrackId = "0E6D771B-230D-444C-A60D-FC7A708AAAAA",
                UserId = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                PasswordTrack = "AKJ+o5Gpa6dGy9+vT4sAXBBihdfufIbtFLVQtIZ+GUtHfs1jKcQdK5j7qJ3BKgYxhQ==",
                CreatedDate = Convert.ToDateTime("2015-11-26 09:08:27.460")
            });

            AddUserPasswordTrackingEntity(new UserPasswordTracking()
            {
                PasswordTrackId = "F089E6C2-7E5A-41DC-9506-5D8BCB17F5F5",
                UserId = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                PasswordTrack = "AH9adMQSqjQF/RLWqihFh2lj1CJHFRbICwBypog1Id9XhljXZqwQ2g7/vTBruzrTWw==",
                CreatedDate = Convert.ToDateTime("2015-11-24 09:08:27.460")
            });
            AddUserPasswordTrackingEntity(new UserPasswordTracking()
            {
                PasswordTrackId = "D709AC51-B019-4414-A7C7-C3683622CE06",
                UserId = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                PasswordTrack = "AKZEX88rG9/N2ZpqvPu0a239RRFu2mfdMXwbyISCueI0WIL8THx8BxosJ3UfLw8ntA==",
                CreatedDate = Convert.ToDateTime("2015-11-25 09:08:27.460")
            });
            AddUserPasswordTrackingEntity(new UserPasswordTracking()
            {
                PasswordTrackId = "EB179739-DF76-4706-B985-1050C00A25CA",
                UserId = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                PasswordTrack = "AH896IikI0E/A6CmNM3e50lM95olKhNyiB9O7elACqfRhWnwRt1d18gQ/sDotYHZVQ==",
                CreatedDate = Convert.ToDateTime("2015-11-25 10:08:27.460")
            });
        }

    }
}