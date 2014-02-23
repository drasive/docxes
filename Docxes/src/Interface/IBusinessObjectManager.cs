﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VrankenBischof.Docxes.Interface {

    public interface IBusinessObjectManager {

        bool IsEditing { get; }

        BusinessObjectManagerAction Action { get; }

    }

}