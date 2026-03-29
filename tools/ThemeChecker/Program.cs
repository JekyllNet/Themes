using System.Text.Json;
using ThemeChecker.Checkers;
using ThemeChecker.Models;

if (args.Length == 0)
{
    PrintUsage();
    return 1;
}

return args[0] switch
{
    "check-pr"    => RunCheckPr(args[1..]),
    "check-theme" => RunCheckTheme(args[1..]),
    _             => PrintUnknownCommand(args[0]),
};

// ── check-pr ─────────────────────────────────────────────────────────────────

static int RunCheckPr(string[] args)
{
    string? prBodyFile = null;
    string? repoRoot   = null;
    string? outputFile = null;

    for (int i = 0; i < args.Length; i++)
    {
        switch (args[i])
        {
            case "--pr-body"   when i + 1 < args.Length: prBodyFile = args[++i]; break;
            case "--repo-root" when i + 1 < args.Length: repoRoot   = args[++i]; break;
            case "--output"    when i + 1 < args.Length: outputFile = args[++i]; break;
        }
    }

    if (prBodyFile is null || repoRoot is null)
    {
        Console.Error.WriteLine(
            "Usage: check-pr --pr-body <file> --repo-root <path> [--output <json-file>]");
        return 2;
    }

    if (!File.Exists(prBodyFile))
    {
        Console.Error.WriteLine($"PR body file not found: {prBodyFile}");
        return 2;
    }

    var prBody  = File.ReadAllText(prBodyFile);
    var checker = new PrChecker();
    var (results, info) = checker.Check(prBody, repoRoot);

    PrintSection("PR Template & Structure", results);

    if (outputFile is not null)
    {
        var json = JsonSerializer.Serialize(info,
            new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(outputFile, json);
    }

    return results.Any(r => !r.Passed && !r.IsWarning) ? 1 : 0;
}

// ── check-theme ───────────────────────────────────────────────────────────────

static int RunCheckTheme(string[] args)
{
    string? themePath  = null;
    string? outputFile = null;

    for (int i = 0; i < args.Length; i++)
    {
        switch (args[i])
        {
            case "--theme-path" when i + 1 < args.Length: themePath  = args[++i]; break;
            case "--output"     when i + 1 < args.Length: outputFile = args[++i]; break;
        }
    }

    if (themePath is null)
    {
        Console.Error.WriteLine(
            "Usage: check-theme --theme-path <path> [--output <json-file>]");
        return 2;
    }

    var checker = new JekyllChecker();
    var results = checker.Check(themePath);

    PrintSection("Jekyll Theme Structure", results);

    if (outputFile is not null)
    {
        var json = JsonSerializer.Serialize(results,
            new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(outputFile, json);
    }

    return results.Any(r => !r.Passed && !r.IsWarning) ? 1 : 0;
}

// ── Shared helpers ────────────────────────────────────────────────────────────

static void PrintSection(string title, IEnumerable<CheckResult> results)
{
    var list     = results.ToList();
    var passed   = list.Count(r => r.Passed);
    var failed   = list.Count(r => !r.Passed && !r.IsWarning);
    var warnings = list.Count(r => !r.Passed && r.IsWarning);

    var icon = failed > 0 ? "❌" : warnings > 0 ? "⚠️" : "✅";
    Console.WriteLine($"### {icon} {title}");
    Console.WriteLine();

    foreach (var r in list)
    {
        var bullet = r.Passed ? "✅" : (r.IsWarning ? "⚠️" : "❌");
        Console.WriteLine($"- {bullet} {r.Message}");
        if (!string.IsNullOrWhiteSpace(r.Detail))
            Console.WriteLine($"  > _{r.Detail}_");
    }

    Console.WriteLine();
    Console.WriteLine($"> **{passed} passed** | **{failed} failed** | **{warnings} warnings**");
    Console.WriteLine();
}

static void PrintUsage()
{
    Console.Error.WriteLine("JekyllNet ThemeChecker — C# .NET 10");
    Console.Error.WriteLine("Usage:");
    Console.Error.WriteLine(
        "  ThemeChecker check-pr --pr-body <file> --repo-root <path> [--output <json>]");
    Console.Error.WriteLine(
        "  ThemeChecker check-theme --theme-path <path> [--output <json>]");
}

static int PrintUnknownCommand(string cmd)
{
    Console.Error.WriteLine($"Unknown command: {cmd}");
    PrintUsage();
    return 2;
}
