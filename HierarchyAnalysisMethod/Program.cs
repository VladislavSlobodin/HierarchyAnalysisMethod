using HierarchyAnalysisMethod;

const string path = @"D:\HierarchyAnalysisMethod\table";
const string extension = ".txt";
const int numberOfMatrix = 5;
var topLevelData = Matrix.FromMatrixFile(path+extension);
var bottomLevelData = new Matrix[numberOfMatrix];

for (int i = 0; i < numberOfMatrix; i++)
{
    bottomLevelData[i] = Matrix.FromListFile(path + (i+1).ToString() + extension);
}

HAM ham = new HAM(topLevelData, bottomLevelData);

//try
//{
    Console.WriteLine(ham.TrySolve());
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
