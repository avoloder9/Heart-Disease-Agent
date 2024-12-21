using Microsoft.ML;

namespace HeartDiseaseAgent
{
    public class HeartDiseaseService
    {
        private readonly MLContext _mlContext;
        private ITransformer _trainedModel;
        private readonly string _dataPath = "C:\\Users\\Adnan\\Desktop\\Heart Disease Agent\\backend\\heart.csv";

        public HeartDiseaseService()
        {
            _mlContext = new MLContext();
        }

        public void TrainModel(string modelPath)
        {
            var data = _mlContext.Data.LoadFromTextFile<HeartData>(
                path: _dataPath,
                hasHeader: true,
                separatorChar: ',');

            var split = _mlContext.Data.TrainTestSplit(data, testFraction: 0.2);
            var pipeline = _mlContext.Transforms.Concatenate("Features", new[]
                {
                    nameof(HeartData.Broj_godina), nameof(HeartData.Spol), nameof(HeartData.Tip_boli),
                    nameof(HeartData.Krvni_pritisak), nameof(HeartData.Holesterol), nameof(HeartData.Secer_nataste),
                    nameof(HeartData.Ecg), nameof(HeartData.Max_otkucaji), nameof(HeartData.Angina),
                    nameof(HeartData.ST_depresija), nameof(HeartData.Nagib), nameof(HeartData.Krvni_sudovi),
                    nameof(HeartData.Talasemija)
                })
                .Append(_mlContext.Transforms.NormalizeMinMax("Features"))
                .Append(_mlContext.BinaryClassification.Trainers.LbfgsLogisticRegression());

            _trainedModel = pipeline.Fit(split.TrainSet);

            var predictions = _trainedModel.Transform(split.TestSet);
            var metrics = _mlContext.BinaryClassification.Evaluate(predictions, "Label");

            _mlContext.Model.Save(_trainedModel, data.Schema, modelPath);
        }
        public float Predict(string modelPath, HeartData input)
        {
            var predictor = _mlContext.Model.Load(modelPath, out var _);
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<HeartData, HeartPrediction>(predictor);
            var prediction = predictionEngine.Predict(input);
            var probability = Sigmoid(prediction.Vjerovatnoca);
            AppendToCsv(_dataPath, input, probability);
            return probability;
        }
      
        private float Sigmoid(float score)
        {
            return 1 / (1 + (float)Math.Exp(-score));
        }

        private static void AppendToCsv(string filePath, HeartData input, float prediction)
        {
            string dataLine = $"{input.Broj_godina},{input.Spol},{input.Tip_boli},{input.Krvni_pritisak},{input.Holesterol},{input.Secer_nataste},{input.Ecg},{input.Max_otkucaji},{input.Angina},{input.ST_depresija},{input.Nagib},{input.Krvni_sudovi},{input.Talasemija},{(prediction >= 0.5 ? 1 : 0)}";
            File.AppendAllText(filePath, dataLine + Environment.NewLine);
        }
    }
}
