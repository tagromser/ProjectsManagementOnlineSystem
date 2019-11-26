using System;
using System.Collections.Generic;
using System.Text;

namespace PMOS.Logic.Common
{
    /// <summary>
    /// Результаты операций.
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// Успешно выполненная операция.
        /// </summary>
        public static OperationResult Success { get; }

        /// <summary>
        /// Провально выполнена операция.
        /// </summary>
        public static OperationResult Failed { get; }
    }
}
