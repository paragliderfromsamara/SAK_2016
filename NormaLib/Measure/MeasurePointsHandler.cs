using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaLib.DBControl.Tables;

namespace NormaLib.Measure
{
    public delegate void MeasurePointsHandlerDelegate(CableMeasurePoint handled_point);

    public class MeasurePointsHandler
    {
        private MeasurePointsHandlerDelegate HandlerFunction;

        public MeasurePointsHandler(MeasurePointsHandlerDelegate handler_func)
        {
            HandlerFunction = handler_func;
        }

        public void ProcessCable(Cable cable)
        {
            foreach(MeasuredParameterType p_type in cable.MeasuredParameterTypes.Rows)
            {
                foreach(CableStructure structure in cable.CableStructures.Rows)
                {
                    ProcessStructureByParameterType(structure, p_type);
                }
            }
        }

        public void ProcessStructureByParameterType(CableStructure structure, MeasuredParameterType parameter_type)
        {
            if (!structure.MeasuredParameterTypes_ids.Contains(parameter_type.ParameterTypeId)) return;
            if (parameter_type.IsFreqParameter)
            {

            }else
            {
                CableMeasurePointMap map = new CableMeasurePointMap(structure, parameter_type.ParameterTypeId);
                do
                {
                    HandlerFunction?.Invoke(map.CurrentPoint);
                } while (map.TryGetNextPoint());
            }
        }
    }
}
