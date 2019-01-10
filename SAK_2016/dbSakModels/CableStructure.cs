using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace NormaMeasure.SAC_APP
{
    public class CableStructure:dbBase
    {
        /*
                                    "id INT UNSIGNED AUTO_INCREMENT NOT NULL",
                                    "cable_id INT UNSIGNED DEFAULT NULL",
                                    "structure_type_id INT UNSIGNED NOT NULL",
                                    "nominal_amount INT UNSIGNED",
                                    "fact_amount INT UNSIGNED",
                                    "lead_material_id INT UNSIGNED NOT NULL",
                                    "lead_diameter FLOAT NOT NULL",
                                    "isolation_material_id INT UNSIGNED NOT NULL",
                                    "wave_resistance FLOAT NOT NULL",
                                    "u_lead_lead FLOAT",
                                    "u_lead_shield FLOAT",
                                    "test_group_work_capacity BOOL DEFAULT 0",
                                    "dr_formula_id INT UNSIGNED DEFAULT 1",
                                    "dr_bringing_formula_id INT UNSIGNED DEFAULT 1",
                                    "PRIMARY KEY (id)" 
         */
        public string isolationMaterialName, leadMaterialName, drFormulaFormula, drBringingFormulaFormula, structTypeName;
        public uint structureTypeId, nominalAmount, factAmount, leadMaterialId, isolationMaterialId, drFormulaId, drBringingFormulaId;
        public decimal leadDiameter, waveResistance, uLeadLead, uLeadShield, leadMaterialTCoeff;
        public bool testGroupWorkCapacity;
        public DataRow dr;
        public CableStructure()
        {
            this.tableName = "cable_structures";
        }
        public CableStructure(long cable_structure_id)
        {
            this.tableName = "cable_structures";
            this.id = cable_structure_id;
        }
        public CableStructure(DataRow csDataRow)
        {
            this.tableName = "cable_structures";
            this.dr = csDataRow;
            fillMainParameters();
        }

        protected override void fillMainParameters()
        {
            //integer
            this.id = ServiceFunctions.convertToUInt(dr["id"]);
            this.leadMaterialId = ServiceFunctions.convertToUInt(dr["lead_material_id"]);
            this.isolationMaterialId = ServiceFunctions.convertToUInt(dr["isolation_material_id"]);
            this.drFormulaId = ServiceFunctions.convertToUInt(dr["dr_formula_id"]);
            this.drBringingFormulaId = ServiceFunctions.convertToUInt(dr["dr_bringing_formula_id"]);
            this.structureTypeId = ServiceFunctions.convertToUInt(dr["structure_type_id"]);
            this.nominalAmount = ServiceFunctions.convertToUInt(dr["fact_amount"]);
            this.factAmount = ServiceFunctions.convertToUInt(dr["nominal_amount"]);
            //decimal
            this.leadDiameter = ServiceFunctions.convertToDecimal(dr["lead_diameter"]);
            this.waveResistance = ServiceFunctions.convertToDecimal(dr["wave_resistance"]);
            this.leadMaterialTCoeff =  ServiceFunctions.convertToDecimal(dr["lead_material_tkc"]);
            this.uLeadLead = ServiceFunctions.convertToDecimal(dr["u_lead_lead"]);
            this.uLeadShield = ServiceFunctions.convertToDecimal(dr["u_lead_shield"]);
            //string
            this.isolationMaterialName = dr["isolation_material_name"].ToString();
            this.leadMaterialName = dr["lead_material_name"].ToString();
            this.drFormulaFormula = dr["dr_formula_formula"].ToString();
            this.drBringingFormulaFormula = dr["dr_bringing_formula_formula"].ToString();
            //bool 
            this.testGroupWorkCapacity = Convert.ToBoolean(dr["test_group_work_capacity"].ToString());
            

        }


    }
}