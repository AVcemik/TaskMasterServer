Console.WriteLine();

StreamReader sr = new StreamReader("test.csv", System.Text.Encoding.Unicode);
string test = sr.ReadToEnd();
Console.WriteLine(test);
sr.Close();