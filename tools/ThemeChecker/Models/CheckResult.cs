namespace ThemeChecker.Models;

/// <summary>Result of a single check item.</summary>
public record CheckResult(
    bool Passed,
    string Message,
    string? Detail = null,
    bool IsWarning = false
);
