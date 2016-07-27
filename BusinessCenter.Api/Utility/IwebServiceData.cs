using BusinessCenter.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Api.Utility
{
  public   interface IwebServiceData
  {
      List<AddressDetails> Data(string searchType);
  }
}
