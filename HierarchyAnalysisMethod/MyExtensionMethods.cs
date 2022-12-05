namespace HierarchyAnalysisMethod
{
    public static class MyExtensionMethods
    {
        public static double[] Normalize(this double[] data)
        {
            double[] result = new double[data.Length];
            var sum = data.Sum();
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = data[i] / sum;
            }

            return result;
        }

        public static double[] GeometricMean(this double[] data)
        {
            double[] result = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = Math.Pow(data[i], 1.0 / (data.Length + 1));
            }

            return result;
        }

        public static double[] MultiplyElements(this double[] data, double[] otherVec)
        {
            var size = Math.Min(data.Length, otherVec.Length);
            var result = new double[size];
            for (int i = 0; i < size; i++)
            {
                result[i] = data[i] * otherVec[i];
            }

            return result;
        }

        public static double[][] Transpose(this double[][] data)
        {
            var height = data[0].Length;
            var width = data.Length;
            var result = new double[height][];
            for (int i = 0; i < height; i++)
            {
                result[i] = new double[width];
            }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    result[i][j] = data[j][i];
                }
            }

            return result;
        }
    }
}
