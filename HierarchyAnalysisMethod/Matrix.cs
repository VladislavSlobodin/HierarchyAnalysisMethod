namespace HierarchyAnalysisMethod
{
    public class Matrix
    {
        private double[,] data;
        private int height;
        private int width;

        public int Height => height;
        public int Width => width;
        public double At(int x, int y) => data[x, y];

        public Matrix(double[,] data)
        {
            this.data = data;
            height = data.GetLength(0);
            width = data.GetLength(1);
        }

        public static Matrix FromMatrixFile(string path)
        {
            var data = File.ReadAllLines(path);
            var height = data.Length;
            var width = data[0].Split(' ').Length;
            double[,] matrixData = new double[height, width];
            for (int i = 0; i < height; i++)
            {
                var line = data[i].Split(' ');
                for (int j = 0; j < width; j++)
                {
                    matrixData[i, j] = ParseElement(line[j]);
                }
            }

            return new Matrix(matrixData);
        }

        public static Matrix FromListFile(string path)
        {
            var list = new List<double>();
            File.ReadAllLines(path).ToList().ForEach(x => list.Add(double.Parse(x)));
            var size = list.Count;
            var matrixData = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrixData[i, j] = list[i] / list[j];
                }
            }

            return new Matrix(matrixData);
        }

        private static double ParseElement(string data)
        {
            if (!data.Contains('/'))
            {
                return double.Parse(data);
            }

            var numerator = double.Parse(data.Remove(data.IndexOf('/')));
            var denominator = double.Parse(data.Substring(data.IndexOf('/') + 1));
            return numerator / denominator;
        }

        public double[] RowProducts()
        {
            var result = new double[height];
            for (int i = 0; i < height; i++)
            {
                var product = 1.0;
                for (int j = 0; j < width; j++)
                {
                    product *= data[i, j];
                }

                result[i] = product;
            }

            return result;
        }

        public double[] ColumnSums()
        {
            var result = new double[width];
            for (int i = 0; i < width; i++)
            {
                result[i] = 0;
                for (int j = 0; j < height; j++)
                {
                    result[i] += data[j, i];
                }
            }

            return result;
        }
    }
}
