﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface IStreetTypesRepository
    {
        IEnumerable<StreetTypes> AllStreetTypes();
       // IEnumerable<StreetTypes> DropDownBind();
        IEnumerable<StreetTypes> FindByStreetTypeId(int streetTypeId);
        IEnumerable<StreetTypes> FindStreetIdbyType(string streetType);
        IEnumerable<StreetTypes> FindStreetIdbyCode(string streetcode);
    }
}