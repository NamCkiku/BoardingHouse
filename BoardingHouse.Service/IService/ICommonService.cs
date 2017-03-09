﻿using BoardingHouse.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardingHouse.Service.IService
{
    public interface ICommonService
    {
        IEnumerable<Province> GetAllProvince();
        IEnumerable<District> GetAllDistrict();
        IEnumerable<Ward> GetAllWard();
        void SaveChanges();
    }
}
