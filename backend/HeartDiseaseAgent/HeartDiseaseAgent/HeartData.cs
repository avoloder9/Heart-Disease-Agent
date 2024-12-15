using Microsoft.ML.Data;

namespace HeartDiseaseAgent
{
    public class HeartData
    {
        [LoadColumn(0)] public float Broj_godina { get; set; }
        [LoadColumn(1)] public float Spol { get; set; }
        [LoadColumn(2)] public float Tip_boli { get; set; }
        [LoadColumn(3)] public float Krvni_pritisak { get; set; }
        [LoadColumn(4)] public float Holesterol { get; set; }
        [LoadColumn(5)] public float Secer_nataste { get; set; }
        [LoadColumn(6)] public float Ecg { get; set; }
        [LoadColumn(7)] public float Max_otkucaji { get; set; }
        [LoadColumn(8)] public float Angina { get; set; }
        [LoadColumn(9)] public float ST_depresija { get; set; }
        [LoadColumn(10)] public float Nagib { get; set; }
        [LoadColumn(11)] public float Krvni_sudovi { get; set; }
        [LoadColumn(12)] public float Talasemija { get; set; }
        [LoadColumn(13), ColumnName("Label")] public bool Rezultat { get; set; }
    }
    public class HeartPrediction
    {
        [ColumnName("Score")] public float Vjerovatnoca { get; set; }
    }
}

