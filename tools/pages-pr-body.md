# JekyllNet GitHub Pages / JekyllNet GitHub Pages 部署

[English](#english) | [中文](#中文)

---

## English

Hello! 👋

This PR was automatically opened by the **[JekyllNet Theme Store](https://github.com/JekyllNet/Themes)** bot
after your theme passed all automated checks (spec, structure, and build).

It adds a **GitHub Pages deployment pipeline** (`.github/workflows/jekyllnet-pages.yml`) to your
repository so your theme is always live and publicly accessible — which is a requirement for listing
in the JekyllNet Theme Store.

### What this pipeline does

- Triggers on every push to `main` / `master` and on manual dispatch
- Builds your site using **[JekyllNet](https://github.com/JekyllNet/JekyllNet)** (a C# Jekyll-style static site generator)
- Deploys the built site to **GitHub Pages** automatically

### Before you merge

1. Make sure **GitHub Pages** is enabled in your repository settings:
   `Settings → Pages → Source → GitHub Actions`
2. The pipeline uses `JekyllNet/action@v2` which installs the JekyllNet .NET tool automatically — no extra setup required.
3. Your site source is assumed to be at the repository root (`.`). If it lives elsewhere, update `source:` in the workflow.

If you have any questions, please open an issue at [JekyllNet/Themes](https://github.com/JekyllNet/Themes/issues).

---

## 中文

您好！ 👋

本 PR 由 **[JekyllNet 主题商店](https://github.com/JekyllNet/Themes)** 机器人在您的主题通过所有自动检查（规范、结构、构建）后自动创建。

本 PR 向您的仓库添加了一个 **GitHub Pages 部署流水线**（`.github/workflows/jekyllnet-pages.yml`），
使您的主题始终在线且可公开访问——这是收录到 JekyllNet 主题商店的必要条件。

### 流水线功能

- 在每次推送到 `main` / `master` 分支或手动触发时运行
- 使用 **[JekyllNet](https://github.com/JekyllNet/JekyllNet)**（C# 编写的 Jekyll 风格静态站点生成器）构建站点
- 自动将构建产物部署到 **GitHub Pages**

### 合并前请确认

1. 确保在仓库设置中启用 **GitHub Pages**：
   `Settings → Pages → Source → GitHub Actions`
2. 流水线使用 `JekyllNet/action@v2`，会自动安装 JekyllNet .NET 工具，无需额外配置。
3. 站点源目录默认为仓库根目录（`.`）。如果您的站点位于其他目录，请修改工作流中的 `source:` 参数。

如有任何问题，欢迎在 [JekyllNet/Themes](https://github.com/JekyllNet/Themes/issues) 提交 Issue。
