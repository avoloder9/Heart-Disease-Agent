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
        public HeartData GetUserInput()
        {
            return new HeartData
            {
                Broj_godina = ReadFloat("Broj godina (18-120)", 18, 120),
                Spol = ReadInt("Spol (0: Zenski, 1: Muski)", 0, 1),
                Tip_boli = ReadInt("Tip boli u grudima (0-3)", 0, 3),
                Krvni_pritisak = ReadFloat("Krvni pritisak u mirovanju (50-250)", 50, 250),
                Holesterol = ReadFloat("Holesterol (0-10)", 0, 10),
                Secer_nataste = ReadInt("Secer nataste > 120 mg/dl (1: da, 0: ne)", 0, 1),
                Ecg = ReadInt("Elektrokardiografski rezultat u mirovanju (0-2)", 0, 2),
                Max_otkucaji = ReadFloat("Maksimalni broj otkucaja srca (50-250)", 50, 250),
                Angina = ReadInt("Angina izazvana vjezbanjem (1: da, 0: ne)", 0, 1),
                ST_depresija = ReadFloat("ST depresija izazvana vjezbanjem (0 to 6)", 0, 6),
                Nagib = ReadInt("Nagib vrsnog segmenta vjezbe (0-2)", 0, 2),
                Krvni_sudovi = ReadInt("Broj velikih krvnih sudova obojenih flurosopijom (0-3)", 0, 3),
                Talasemija = ReadInt("Talasemija (1-3)", 1, 3)
            };
        }
        private float Sigmoid(float score)
        {
            return 1 / (1 + (float)Math.Exp(-score));
        }

        private static float ReadFloat(string prompt, float minValue, float maxValue)
        {
            float result;
            do
            {
                Console.Write($"{prompt}: ");
                if (float.TryParse(Console.ReadLine(), out result) && result >= minValue && result <= maxValue)
                    return result;
                Console.WriteLine($"Molim vas unesite vrijednost između {minValue} i {maxValue}.");
            } while (true);
        }
        private static int ReadInt(string prompt, int minValue, int maxValue)
        {
            int result;
            do
            {
                Console.Write($"{prompt}: ");
                if (int.TryParse(Console.ReadLine(), out result) && result >= minValue && result <= maxValue)
                    return result;
                Console.WriteLine($"Molim vas unesite vrijednost između {minValue} i {maxValue}.");
            } while (true);
        }
        private static void AppendToCsv(string filePath, HeartData input, float prediction)
        {
            string dataLine = $"{input.Broj_godina},{input.Spol},{input.Tip_boli},{input.Krvni_pritisak},{input.Holesterol},{input.Secer_nataste},{input.Ecg},{input.Max_otkucaji},{input.Angina},{input.ST_depresija},{input.Nagib},{input.Krvni_sudovi},{input.Talasemija},{(prediction >= 0.5 ? 1 : 0)}";
            File.AppendAllText(filePath, dataLine + Environment.NewLine);
        }
    }
}
