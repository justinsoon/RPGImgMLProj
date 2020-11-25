using System;
using Microsoft.ML.Data;

namespace ERPGML.Model
{
    public class ModelOutput
    {
        // ColumnName Attribute Is Used To Change The Column Name From Its Default Value, Which Is The Name Of The Field
        [ColumnName("PredictedLabel")]
        public String Prediction { get; set; }

        public float[] Score { get; set; }
    }
}
