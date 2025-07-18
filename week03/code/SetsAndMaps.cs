using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public static class SetsAndMaps
{
    public static async Task<string[]> EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        
        try 
        {
            var response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var featureCollection = JsonConvert.DeserializeObject<FeatureCollection>(json);

            if (featureCollection == null || featureCollection.Features == null || featureCollection.Features.Count == 0)
            {
                return Array.Empty<string>();
            }

            var summaries = new List<string>(featureCollection.Features.Count);
            foreach (var feature in featureCollection.Features)
            {
                if (feature != null && feature.Properties != null && feature.Properties.Place != null)
                {
                    summaries.Add($"{feature.Properties.Place} - Mag {feature.Properties.Mag:F1}");
                }
            }

            return summaries.ToArray();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP error fetching earthquake data: {ex.Message}");
            return Array.Empty<string>();
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON deserialization error: {ex.Message}");
            return Array.Empty<string>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error fetching earthquake data: {ex.Message}");
            return Array.Empty<string>();
        }
    }

    public static string[] FindPairs(string[] input)
    {
        if (input == null)
            return Array.Empty<string>();

        var seen = new HashSet<string>();
        var pairs = new List<string>();

        foreach (var str in input)
        {
            if (str == null)
                continue;

            var reverse = new string(str.Reverse().ToArray());
            if (seen.Contains(reverse))
            {
                if (Array.IndexOf(input, reverse) < Array.IndexOf(input, str))
                    pairs.Add($"{str} & {reverse}");
                else
                    pairs.Add($"{reverse} & {str}");
            }
            seen.Add(str);
        }

        return pairs.OrderBy(x => x).ToArray();
    }

    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var validEducationLevels = new HashSet<string>
        {
            "Bachelors", "HS-grad", "11th", "Masters", "9th", "Some-college",
            "Assoc-acdm", "Assoc-voc", "7th-8th", "Doctorate", "Prof-school",
            "5th-6th", "10th", "1st-4th", "Preschool", "12th"
        };

        var result = new Dictionary<string, int>();
        foreach (var level in validEducationLevels)
        {
            result[level] = 0;
        }

        try
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"File not found: {filename}");
                return result;
            }

            var lines = File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var education = line.Trim();
                    if (validEducationLevels.Contains(education))
                    {
                        result[education]++;
                    }
                }
            }

            // Log dictionary for debugging
            foreach (var kvp in result)
            {
                Console.WriteLine($"DEBUG: {kvp.Key}: {kvp.Value}");
            }

            // Sort by key to ensure consistent order
            return result.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file {filename}: {ex.Message}");
            return result;
        }
    }

    public static bool IsAnagram(string s1, string s2)
    {
        if (s1 == null || s2 == null)
            return false;

        var chars1 = s1.Replace(" ", "").ToLower().ToCharArray();
        var chars2 = s2.Replace(" ", "").ToLower().ToCharArray();

        if (chars1.Length != chars2.Length)
            return false;

        Array.Sort(chars1);
        Array.Sort(chars2);
        return chars1.SequenceEqual(chars2);
    }
}