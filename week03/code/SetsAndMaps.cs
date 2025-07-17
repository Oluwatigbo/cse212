using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public static class SetsAndMaps
{
    public static string[] FindPairs(string[] words)
    {
        HashSet<string> wordSet = new HashSet<string>(words);
        List<string> pairs = new List<string>();

        foreach (var word in words)
        {
            string reversed = new string(word.Reverse().ToArray());
            if (wordSet.Contains(reversed) && word != reversed)
            {
                pairs.Add($"{word} & {reversed}");
                wordSet.Remove(word);
                wordSet.Remove(reversed);
            }
        }

        return pairs.ToArray();
    }

    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(',');
            if (fields.Length > 4)
            {
                string degree = fields[4].Trim();
                if (degrees.ContainsKey(degree))
                {
                    degrees[degree]++;
                }
                else
                {
                    degrees[degree] = 1;
                }
            }
        }
        return degrees;
    }

    public static bool IsAnagram(string word1, string word2)
    {
        var cleanedWord1 = new string(word1.Where(c => !char.IsWhiteSpace(c)).ToArray()).ToLower();
        var cleanedWord2 = new string(word2.Where(c => !char.IsWhiteSpace(c)).ToArray()).ToLower();

        if (cleanedWord1.Length != cleanedWord2.Length)
            return false;

        var charCount1 = cleanedWord1.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        var charCount2 = cleanedWord2.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());

        return charCount1.OrderBy(kv => kv.Key)
                        .SequenceEqual(charCount2.OrderBy(kv => kv.Key));
    }

    public static async Task<string[]> EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        try {
            var json = await client.GetStringAsync(uri);
            var options = new JsonSerializerOptions { 
                PropertyNameCaseInsensitive = true 
            };

            var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

            return featureCollection.Features
                .Where(f => f.Properties != null)
                .Select(f => $"{f.Properties.Place} - Mag {f.Properties.Mag}")
                .ToArray();
        }
        catch (Exception ex) {
            Console.WriteLine($"Error fetching earthquake data: {ex.Message}");
            return new string[0];
        }
    }
}
