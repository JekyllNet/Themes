using ThemeChecker.Models;

namespace ThemeChecker.Checkers;

/// <summary>
/// Validates the structure of a cloned Jekyll theme repository.
/// </summary>
public class JekyllChecker
{
    public List<CheckResult> Check(string themePath)
    {
        var results = new List<CheckResult>();

        if (!Directory.Exists(themePath))
        {
            results.Add(new CheckResult(false,
                $"Theme directory not found: `{themePath}`"));
            return results;
        }

        // ── _config.yml ──────────────────────────────────────────────────────
        var configPath = Path.Combine(themePath, "_config.yml");
        if (File.Exists(configPath))
            results.Add(new CheckResult(true, "`_config.yml` found"));
        else
            results.Add(new CheckResult(false,
                "`_config.yml` not found — required for Jekyll sites"));

        // ── _layouts/ ────────────────────────────────────────────────────────
        var layoutsDir = Path.Combine(themePath, "_layouts");
        if (!Directory.Exists(layoutsDir))
        {
            results.Add(new CheckResult(false,
                "`_layouts/` directory not found — expected for a Jekyll theme"));
        }
        else
        {
            var htmlLayouts = Directory.GetFiles(layoutsDir, "*.html",
                SearchOption.TopDirectoryOnly);
            if (htmlLayouts.Length > 0)
                results.Add(new CheckResult(true,
                    $"`_layouts/` directory found ({htmlLayouts.Length} HTML layout(s))"));
            else
                results.Add(new CheckResult(false,
                    "`_layouts/` directory exists but contains no `.html` layout files",
                    null, IsWarning: true));
        }

        // ── Entry point ──────────────────────────────────────────────────────
        var hasIndex = File.Exists(Path.Combine(themePath, "index.html")) ||
                       File.Exists(Path.Combine(themePath, "index.md"));
        if (hasIndex)
            results.Add(new CheckResult(true, "`index.html` or `index.md` found"));
        else
            results.Add(new CheckResult(false,
                "No `index.html` or `index.md` found at repository root",
                null, IsWarning: true));

        // ── Gemfile ──────────────────────────────────────────────────────────
        var gemfilePath = Path.Combine(themePath, "Gemfile");
        if (!File.Exists(gemfilePath))
        {
            results.Add(new CheckResult(false,
                "`Gemfile` not found — recommended for reproducible builds",
                null, IsWarning: true));
        }
        else
        {
            results.Add(new CheckResult(true, "`Gemfile` found"));
            var gemfileContent = File.ReadAllText(gemfilePath);
            if (!gemfileContent.Contains("jekyll", StringComparison.OrdinalIgnoreCase))
                results.Add(new CheckResult(false,
                    "`Gemfile` does not reference Jekyll",
                    null, IsWarning: true));
            else
                results.Add(new CheckResult(true, "`Gemfile` references Jekyll"));
        }

        // ── _includes/ (optional, informational) ────────────────────────────
        var includesDir = Path.Combine(themePath, "_includes");
        if (Directory.Exists(includesDir))
            results.Add(new CheckResult(true, "`_includes/` directory found"));

        // ── Assets / styles ──────────────────────────────────────────────────
        var hasAssets = Directory.Exists(Path.Combine(themePath, "assets")) ||
                        Directory.Exists(Path.Combine(themePath, "css")) ||
                        Directory.Exists(Path.Combine(themePath, "_sass"));
        if (hasAssets)
            results.Add(new CheckResult(true, "Assets / styles directory found"));
        else
            results.Add(new CheckResult(false,
                "No `assets/`, `css/`, or `_sass/` directory found",
                null, IsWarning: true));

        return results;
    }
}
