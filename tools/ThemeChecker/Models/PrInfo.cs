namespace ThemeChecker.Models;

/// <summary>Data extracted from the PR template body.</summary>
public class PrInfo
{
    public string? ThemeName { get; set; }
    public string? ThemeCategory { get; set; }
    public string? ThemeDirectoryPath { get; set; }
    public string? PreviewUrl { get; set; }
    public string? ThemeRepoUrl { get; set; }
    public string? GitSha { get; set; }
    public string? Description { get; set; }
    public int ChecklistTotal { get; set; }
    public int ChecklistChecked { get; set; }
}
