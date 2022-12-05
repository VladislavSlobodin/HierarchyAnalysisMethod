namespace HierarchyAnalysisMethod
{
    public class HAM
    {
        private Matrix topLevelData;
        private Matrix[] bottomLevelData;
        private double[] normalizedTopLevelPriorityVector;
        private double[][] normalizedBottomLelvelPriorityVectors;
        private readonly double[] coefficients = { 0, 0, 0.58, 0.9, 1.12, 1.24, 1.32, 1.41, 1.45, 1.49 };
        public HAM(Matrix topLevelData, params Matrix[] bottomLevelData)
        {
            this.topLevelData = topLevelData;
            this.bottomLevelData = bottomLevelData;
        }

        public string TrySolve()
        {

            normalizedTopLevelPriorityVector = CalculatePriorityVector(-1);
            normalizedBottomLelvelPriorityVectors = new double[bottomLevelData.Length][];
            for (int i = 0; i < bottomLevelData.Length; i++)
            {
                normalizedBottomLelvelPriorityVectors[i] = CalculatePriorityVector(i);
            }

            normalizedBottomLelvelPriorityVectors = normalizedBottomLelvelPriorityVectors.Transpose();
            var result = new double[normalizedBottomLelvelPriorityVectors.Length];
            for (int i = 0; i < normalizedBottomLelvelPriorityVectors.Length; i++)
            {
                result[i] = normalizedBottomLelvelPriorityVectors[i].MultiplyElements(normalizedTopLevelPriorityVector).Sum();
            }

            //return Array.IndexOf(result, result.Max()) + 1;    
            return string.Join("\n", result);
        }

        private double[] CalculatePriorityVector(int index)
        {
            var data = index == -1 ? topLevelData : bottomLevelData[index];
            var normalizedPriorityVector = data
            .RowProducts()
            .GeometricMean()
            .Normalize();
            var sumOfProductsOfRowProductsAndColumnSums = normalizedPriorityVector
                .MultiplyElements(data.ColumnSums())
                .Sum();
            var numberOfCriteria = data.Height;
            var consistencyRelation = (sumOfProductsOfRowProductsAndColumnSums - numberOfCriteria) / (numberOfCriteria - 1) * coefficients[numberOfCriteria - 1];
            if (consistencyRelation >= 0.1)
            {
                throw new InvalidDataException($"Данным таблицы {index} нельзя доверять");
            }

            return normalizedPriorityVector;
        }
    }
}
