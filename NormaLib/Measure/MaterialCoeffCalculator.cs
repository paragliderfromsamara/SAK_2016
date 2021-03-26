using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaLib.DBControl.Tables;
using NormaLib.DBControl;
using System.Data;

namespace NormaLib.Measure
{
    public class MaterialCoeffCalculator
    {
        DBEntityTable IsolationMaterialCoeffs;
        DBEntityTable LeadMaterials;

        public MaterialCoeffCalculator(DBEntityTable isolation_material_coeffs, DBEntityTable lead_materials)
        {
            IsolationMaterialCoeffs = isolation_material_coeffs;
            LeadMaterials = lead_materials;
        }

        public float CalculateCoeff(uint parameterTypeId, CableStructure cable_structure, float temperature)
        {
            float mat_coeff = 1.0f;
            DataRow[] dataRows;
            switch (parameterTypeId)
            {
                case MeasuredParameterType.Risol1:
                case MeasuredParameterType.Risol2:
                case MeasuredParameterType.Risol3:
                case MeasuredParameterType.Risol4:
                    dataRows = IsolationMaterialCoeffs.Select($"{IsolMaterialCoeffs.MaterialId_ColumnName} = {cable_structure.IsolationMaterialId} AND {IsolMaterialCoeffs.Temperature_ColumnName} = {temperature}");
                    if (dataRows.Length > 0)
                        mat_coeff = ((IsolMaterialCoeffs)dataRows[0]).Coefficient;

                    break;
                case MeasuredParameterType.Rleads:
                    dataRows = LeadMaterials.Select($"{LeadMaterial.MaterialId_ColumnName} = {cable_structure.LeadMaterialTypeId}");
                    if (dataRows.Length > 0)
                        mat_coeff = 1.0f + ((LeadMaterial)dataRows[0]).MaterialTKC * (temperature - 20.0f);// (1 + tempCoeff * (temperature - 20.0)
                    break;
            }
            return mat_coeff;
        }
    }
}
