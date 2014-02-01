using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VrankenBischof.Docxes.Interface {

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Cant be more than this shit
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public interface IManagementObjectInterface<T> {

        void mapObjectToInterface(T objectToMap);
        T createObjectFromInterface();

        bool checkInterfaceInput();

    }

}
