using System.Text.Json;

string deneme = "{\"id\":348,\"id_home\":52,\"name\":\"strng\",\"type\":1,\"icon\":\"icon\"}";
Console.WriteLine(JsonDocument.Parse(deneme).ToString());
