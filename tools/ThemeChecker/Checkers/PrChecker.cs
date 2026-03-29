using System.Text.RegularExpressions;
using ThemeChecker.Models;

namespace ThemeChecker.Checkers;

/// <summary>
/// Validates a PR template body for spec compliance, checks the local theme
/// directory structure and README content inside the store repository.
/// </summary>
public class PrChecker
{
    private static readonly HashSet<string> ValidCategories =
        new(["blog", "portfolio", "documentation", "landing", "minimal", "business"],
            StringComparer.OrdinalIgnoreCase);

    public (List<CheckResult> Results, PrInfo Info) Check(string prBody, string repoRoot)
    {
        var results = new List<CheckResult>();
        var info = ParsePrBody(prBody);

        // ── Theme Name ──────────────────────────────────────────────────────
        if (string.IsNullOrWhiteSpace(info.ThemeName))
            results.Add(new CheckResult(false, "Theme Name is missing"));
        else
            results.Add(new CheckResult(true, $"Theme Name: `{info.ThemeName}`"));

        // ── Category ────────────────────────────────────────────────────────
        if (string.IsNullOrWhiteSpace(info.ThemeCategory))
            results.Add(new CheckResult(false,
                "No theme category selected — check exactly one checkbox"));
        else if (!ValidCategories.Contains(info.ThemeCategory))
            results.Add(new CheckResult(false,
                $"Invalid category: `{info.ThemeCategory}`",
                $"Valid categories: {string.Join(", ", ValidCategories)}"));
        else
            results.Add(new CheckResult(true, $"Category: `{info.ThemeCategory}`"));

        // ── Theme Directory Path ─────────────────────────────────────────────
        if (string.IsNullOrWhiteSpace(info.ThemeDirectoryPath))
        {
            results.Add(new CheckResult(false, "Theme Directory Path is missing"));
        }
        else
        {
            var normalized = info.ThemeDirectoryPath.Trim('/');
            var pathMatch = Regex.Match(normalized,
                @"^themes/([a-z][a-z0-9-]*)/([a-z0-9][a-z0-9_-]*)$");
            if (!pathMatch.Success)
            {
                results.Add(new CheckResult(false,
                    $"Theme Directory Path `{info.ThemeDirectoryPath}` does not match `themes/<category>/<theme-name>` format",
                    "Path must be lowercase with hyphens, e.g. themes/blog/my-awesome-theme"));
            }
            else
            {
                info.ThemeDirectoryPath = normalized;
                results.Add(new CheckResult(true,
                    $"Theme Directory Path: `{normalized}`"));

                // Category checkbox must match path segment
                var pathCategory = pathMatch.Groups[1].Value;
                if (!string.IsNullOrWhiteSpace(info.ThemeCategory) &&
                    !string.Equals(pathCategory, info.ThemeCategory,
                        StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(new CheckResult(false,
                        $"Category checkbox (`{info.ThemeCategory}`) does not match directory path category (`{pathCategory}`)"));
                }
            }
        }

        // ── Preview URL ──────────────────────────────────────────────────────
        if (string.IsNullOrWhiteSpace(info.PreviewUrl))
            results.Add(new CheckResult(false, "Preview URL is missing"));
        else if (!Uri.TryCreate(info.PreviewUrl, UriKind.Absolute, out var prevUri) ||
                 prevUri.Scheme is not ("http" or "https"))
            results.Add(new CheckResult(false,
                $"Preview URL is not a valid HTTP/HTTPS URL: `{info.PreviewUrl}`"));
        else
            results.Add(new CheckResult(true, $"Preview URL: {info.PreviewUrl}"));

        // ── Theme Repository URL ─────────────────────────────────────────────
        if (string.IsNullOrWhiteSpace(info.ThemeRepoUrl))
            results.Add(new CheckResult(false, "Theme Repository URL is missing"));
        else if (!Uri.TryCreate(info.ThemeRepoUrl, UriKind.Absolute, out var repoUri) ||
                 repoUri.Scheme is not ("http" or "https"))
            results.Add(new CheckResult(false,
                $"Theme Repository URL is not a valid URL: `{info.ThemeRepoUrl}`"));
        else
            results.Add(new CheckResult(true,
                $"Theme Repository URL: {info.ThemeRepoUrl}"));

        // ── Git SHA ──────────────────────────────────────────────────────────
        if (string.IsNullOrWhiteSpace(info.GitSha))
            results.Add(new CheckResult(false, "Git SHA is missing"));
        else if (!Regex.IsMatch(info.GitSha, @"^[0-9a-fA-F]{7,40}$"))
            results.Add(new CheckResult(false,
                $"Git SHA `{info.GitSha}` is not valid (expected 7–40 hex characters)"));
        else
            results.Add(new CheckResult(true, $"Git SHA: `{info.GitSha}`"));

        // ── Description ─────────────────────────────────────────────────────
        if (string.IsNullOrWhiteSpace(info.Description))
            results.Add(new CheckResult(false, "Description is missing"));
        else
            results.Add(new CheckResult(true, "Description provided"));

        // ── Checklist ────────────────────────────────────────────────────────
        if (info.ChecklistTotal == 0)
        {
            results.Add(new CheckResult(false, "Checklist section not found in PR body"));
        }
        else if (info.ChecklistChecked < info.ChecklistTotal)
        {
            results.Add(new CheckResult(false,
                $"Checklist incomplete: {info.ChecklistChecked}/{info.ChecklistTotal} items checked",
                "All checklist items must be confirmed before submission"));
        }
        else
        {
            results.Add(new CheckResult(true,
                $"All {info.ChecklistTotal} checklist items confirmed"));
        }

        // ── Local theme directory & README ───────────────────────────────────
        if (!string.IsNullOrWhiteSpace(info.ThemeDirectoryPath))
        {
            var themeFullPath = Path.Combine(repoRoot,
                info.ThemeDirectoryPath.Replace('/', Path.DirectorySeparatorChar));

            if (!Directory.Exists(themeFullPath))
            {
                results.Add(new CheckResult(false,
                    $"Theme directory `{info.ThemeDirectoryPath}` does not exist in this PR"));
            }
            else
            {
                results.Add(new CheckResult(true,
                    $"Theme directory `{info.ThemeDirectoryPath}` found"));

                var readmePath = Path.Combine(themeFullPath, "README.md");
                if (!File.Exists(readmePath))
                {
                    results.Add(new CheckResult(false,
                        "README.md not found in theme directory"));
                }
                else
                {
                    results.Add(new CheckResult(true, "README.md exists in theme directory"));
                    results.AddRange(CheckReadmeContent(File.ReadAllText(readmePath)));
                }
            }
        }

        return (results, info);
    }

    // ── Parsing helpers ──────────────────────────────────────────────────────

    private static PrInfo ParsePrBody(string body)
    {
        var info = new PrInfo
        {
            ThemeName         = ExtractSectionValue(body, "Theme Name"),
            ThemeCategory     = ExtractCheckedCategory(body),
            ThemeDirectoryPath = ExtractSectionValue(body, "Theme Directory Path"),
            PreviewUrl        = ExtractSectionValue(body, "Preview URL"),
            ThemeRepoUrl      = ExtractSectionValue(body, "Theme Repository URL"),
            Description       = ExtractSectionValue(body, "Description"),
        };

        var rawSha = ExtractSectionValue(body, "Git SHA");
        info.GitSha = NormalizeGitSha(rawSha);

        (info.ChecklistTotal, info.ChecklistChecked) = CountChecklist(body);
        return info;
    }

    /// <summary>
    /// Returns the user-supplied content of a `### &lt;headerKeyword&gt;` section,
    /// stripping HTML comments and skipping checkbox lines.
    /// </summary>
    private static string? ExtractSectionValue(string body, string headerKeyword)
    {
        var lines = body.Split('\n');
        bool inSection = false;
        bool inComment = false;
        var valueLines = new List<string>();

        foreach (var rawLine in lines)
        {
            var trimmed = rawLine.Trim();

            if (trimmed.StartsWith("### ") &&
                trimmed.Contains(headerKeyword, StringComparison.OrdinalIgnoreCase))
            {
                inSection = true;
                inComment = false;
                valueLines.Clear();
                continue;
            }

            if (!inSection) continue;

            // Stop at the next section or horizontal rule
            if (trimmed.StartsWith("## ") || trimmed.StartsWith("### ") || trimmed == "---")
                break;

            // Track HTML comment blocks
            if (trimmed.StartsWith("<!--"))
            {
                inComment = !trimmed.EndsWith("-->");
                continue;
            }
            if (inComment)
            {
                if (trimmed.EndsWith("-->")) inComment = false;
                continue;
            }

            // Skip checkbox lines (used in the category section)
            if (trimmed.StartsWith("- [")) continue;

            if (!string.IsNullOrWhiteSpace(trimmed))
                valueLines.Add(trimmed);
        }

        if (valueLines.Count == 0) return null;

        var value = string.Join(" ", valueLines).Trim();
        // Strip surrounding backticks if present
        if (value.Length > 2 && value.StartsWith('`') && value.EndsWith('`'))
            value = value[1..^1];

        return string.IsNullOrWhiteSpace(value) ? null : value;
    }

    /// <summary>Returns the single checked category, or null if 0 or 2+ are checked.</summary>
    private static string? ExtractCheckedCategory(string body)
    {
        var matches = Regex.Matches(body, @"- \[x\]\s+`?([a-z][a-z0-9-]*)`?",
            RegexOptions.IgnoreCase);

        var categories = matches
            .Select(m => m.Groups[1].Value.ToLowerInvariant())
            .Where(c => ValidCategories.Contains(c))
            .Distinct()
            .ToList();

        return categories.Count == 1 ? categories[0] : null;
    }

    /// <summary>Extracts a 7-40 char hex SHA from a raw string or commit URL.</summary>
    private static string? NormalizeGitSha(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return null;

        raw = raw.Trim();

        // Commit URL: https://github.com/.../commit/<sha>
        var urlMatch = Regex.Match(raw, @"/commit/([0-9a-fA-F]{7,40})");
        if (urlMatch.Success) return urlMatch.Groups[1].Value;

        // Bare full SHA
        var fullSha = Regex.Match(raw, @"\b([0-9a-fA-F]{40})\b");
        if (fullSha.Success) return fullSha.Groups[1].Value;

        // Bare short SHA
        var shortSha = Regex.Match(raw, @"^([0-9a-fA-F]{7,39})$");
        if (shortSha.Success) return shortSha.Groups[1].Value;

        return null;
    }

    private static (int total, int checkedCount) CountChecklist(string body)
    {
        var idx = body.IndexOf("## Checklist", StringComparison.OrdinalIgnoreCase);
        if (idx < 0)
            idx = body.IndexOf("## 检查清单", StringComparison.OrdinalIgnoreCase);
        if (idx < 0) return (0, 0);

        var section = body[idx..];
        var total   = Regex.Matches(section, @"- \[[ x]\]").Count;
        var done    = Regex.Matches(section, @"- \[x\]", RegexOptions.IgnoreCase).Count;
        return (total, done);
    }

    // ── README content checks ────────────────────────────────────────────────

    private static List<CheckResult> CheckReadmeContent(string content)
    {
        var results = new List<CheckResult>();
        var lower = content.ToLowerInvariant();

        if (content.Length < 80)
        {
            results.Add(new CheckResult(false,
                "README.md is too short — all required sections must be present",
                "Required: name/description, preview URL, screenshot, installation, configuration, license"));
            return results;
        }

        // Preview URL
        if (!Regex.IsMatch(content, @"https?://"))
            results.Add(new CheckResult(false,
                "README.md does not contain a preview URL"));
        else
            results.Add(new CheckResult(true, "README.md contains a preview URL"));

        // Screenshot
        if (!Regex.IsMatch(content, @"!\[.*?\]\(.*?\)") &&
            !Regex.IsMatch(content, @"<img\s", RegexOptions.IgnoreCase))
            results.Add(new CheckResult(false,
                "README.md does not contain a screenshot (`![alt](url)`)",
                null, IsWarning: true));
        else
            results.Add(new CheckResult(true, "README.md contains a screenshot"));

        // Installation instructions
        if (!lower.Contains("install") && !lower.Contains("安装") &&
            !lower.Contains("usage") && !lower.Contains("使用"))
            results.Add(new CheckResult(false,
                "README.md does not describe installation or usage",
                null, IsWarning: true));
        else
            results.Add(new CheckResult(true,
                "README.md contains installation/usage instructions"));

        // License mention
        if (!lower.Contains("license") && !lower.Contains("许可"))
            results.Add(new CheckResult(false,
                "README.md does not mention the license",
                null, IsWarning: true));
        else
            results.Add(new CheckResult(true, "README.md mentions the license"));

        return results;
    }
}
