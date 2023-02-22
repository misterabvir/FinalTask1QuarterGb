//чтение  строк из файла
string ReadFile() => File.ReadAllText("input.txt");

//запись в файл
void WriteFile(string output) => File.WriteAllText("output.txt", output);


//парсинг входной строки, получаем массивы массивов строк, удалены лишние пробелы
string[][] ParseInput(string input) => input
    .Split('\n')
    .Select(line => line
        .Trim(' ')
        .Split(',')
        .Select(str=>str.Trim(' ').Trim('\r'))
        .ToArray())
    .ToArray();

//заполнение
void Fill(string[] input, ref string[] output)
{
    for (int i = 0, j = 0; i < input.Length; i++)
    {
        if(input[i].Length <= 3) 
            output[j++] = input[i];
    }
}
 
//фильтруем
string[] GetOutput(string[] nonFilteredStrings)
{
    int length = GetAmountRightElements(nonFilteredStrings, (item)=> item.Length <= 3);
    string[] filteredStrings = new string[length];
    Fill(nonFilteredStrings, ref filteredStrings);       
    return  filteredStrings;
}

//считаем количество подходящих элементов
int GetAmountRightElements(string[] strings, Func<string, bool> predicate) => strings.Where(predicate).Count();

//основной цикл для формирования выходного массива массивов
string[][] GetOutputArrays(string[][] data)
{
    string[][] outputs = new string[data.Length][]; 
    for (int i = 0; i < data.Length; i++)
        outputs[i] = GetOutput(data[i]);
    return outputs;   
}

//красиво печатаем формируем строки
string GetResult(string[][] first, string[][] second)
{
    string result = string.Empty;   
    var f = first.Select(line => $"[\"{string.Join("\", \"", line)}\"]").ToArray();
    var s = second.Select(line => $"[\"{string.Join("\", \"", line)}\"]").ToArray();
    for (int i = 0; i < first.Length; i++)   
        result = ($"{result}{f[i]} --> {s[i]}\r\n"); 
    return result;  
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////

string input = ReadFile();
string[][] parsedArrays = ParseInput(input);
string[][] outputArrays = GetOutputArrays(parsedArrays);
string result = GetResult(parsedArrays, outputArrays);
WriteFile(result);
Console.WriteLine(result);

//////////////////////////////////////////////////////////////////////////////////////////////////////////////
// решить можно было и в одну строку
/*
var output = input.Split('\n')
    .Select(line => line
        .Trim(' ')
        .Split(',')
        .Select(str=>str.Trim(' ').Trim('\r'))
        .Where(item => item.Length <= 3)         // предикат
        .ToArray())
    .ToArray();
*/

