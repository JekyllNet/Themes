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
- Builds your site with Jekyll in production mode
- Deploys the built site to **GitHub Pages** automatically

### Before you merge

1. Make sure **GitHub Pages** is enabled in your repository settings:
   `Settings → Pages → Source → GitHub Actions`
2. If your site needs specific Jekyll plugins, verify they are listed in your `Gemfile`.
3. You can customise the `ruby-version` in the workflow if needed.

If you have any questions, please open an issue at [JekyllNet/Themes](https://github.com/JekyllNet/Themes/issues).

---

## 中文

您好！ 👋

本 PR 由 **[JekyllNet 主题商店](https://github.com/JekyllNet/Themes)** 机器人在您的主题通过所有自动检查（规范、结构、构建）后自动创建。

本 PR 向您的仓库添加了一个 **GitHub Pages 部署流水线**（`.github/workflows/jekyllnet-pages.yml`），
使您的主题始终在线且可公开访问——这是收录到 JekyllNet 主题商店的必要条件。

### 流水线功能

- 在每次推送到 `main` / `master` 分支或手动触发时运行
- 以生产模式使用 Jekyll 构建站点
- 自动将构建产物部署到 **GitHub Pages**

### 合并前请确认

1. 确保在仓库设置中启用 **GitHub Pages**：
   `Settings → Pages → Source → GitHub Actions`
2. 如果您的站点需要特定 Jekyll 插件，请确认它们已列在 `Gemfile` 中。
3. 如有需要，可以在工作流文件中自定义 `ruby-version`。

如有任何问题，欢迎在 [JekyllNet/Themes](https://github.com/JekyllNet/Themes/issues) 提交 Issue。
