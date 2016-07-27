using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Models;
using BusinessCenter.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Implementation
{
    public class MyServiceDetails : IMyServiceDetails
    {
        protected IUserServicesRepository _repository;

        /// <summary>
        /// Initializes a new Instance of SecurityQuestionService
        /// </summary>
        /// <param name="repo"></param>
        public MyServiceDetails(IUserServicesRepository repo)
        {
            _repository = repo;
        }

        public int UserServiceAdd(UserServiceModel userService)
        {
            try
            {
                return _repository.UserServiceAdd(userService);
            }
            catch (Exception ex)
            {
                throw new Exception("Error :" + ex);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int UserSaveListCount(string userId)
        {
            int result = 0;
            try
            {
                result = _repository.UserSaveListCount(userId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error :" + ex);
            }
            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="_searchServiceInput"></param>
        /// <returns></returns>

        public IQueryable<CommonData> GetSearchData(UserServiceModel _searchServiceInput)
        {
            return _repository.GetSearchData(_searchServiceInput);
        }

        //public IEnumerable<CommonData> GetAbra(UserServiceModel _searchServiceInput)
        //{
        //    var abra = _repository.GetAbra(_searchServiceInput);
        //    return abra.ToList();
        //}

        //public IEnumerable<CommonData> GetBbl(UserServiceModel _searchServiceInput)
        //{
        //    var bbl = _repository.GetBbl(_searchServiceInput);
        //    return bbl.ToList();
        //}

        //public IEnumerable<CommonData> GetCbe(UserServiceModel _searchServiceInput)
        //{
        //    var cbe = _repository.GetCbe(_searchServiceInput);
        //    return cbe.ToList();
        //}

        //public IEnumerable<CommonData> GetCorp(UserServiceModel _searchServiceInput)
        //{
        //    var corp = _repository.GetCorp(_searchServiceInput);
        //    return corp.ToList();
        //}
        //public IEnumerable<CommonData> GetOpla(UserServiceModel _searchServiceInput)
        //{
        //    var opla = _repository.GetOpla(_searchServiceInput);
        //    return opla.ToList();
        //}

        /// <summary>
        ///
        /// </summary>
        /// <param name="searchServiceInput"></param>
        /// <returns></returns>
        public IQueryable<CommonData> GetAllData(UserServiceModel searchServiceInput)
        {
            var commandata = _repository.GetAllData(searchServiceInput);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userService"></param>
        /// <returns></returns>
        public IEnumerable<SearchDataViewModel> GetCount(UserServiceModel userService)
        {
            var commandata = _repository.GetCount(userService);
            return commandata.ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userService"></param>
        public void DeleteUserService(UserServiceModel userService)
        {
            _repository.DeleteUserService(userService);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userService"></param>
        public void DeleteUserService(List<UserServiceModel> userService)
        {
            _repository.DeleteUserService(userService);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        public void DeleteSingleUerService(string userId)
        {
            _repository.DeleteSingleUerService(userId);
        }
    }
}